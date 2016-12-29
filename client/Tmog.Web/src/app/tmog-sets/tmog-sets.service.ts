import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { TMogSetsCacheService } from './tmog-sets-cache.service';
import { TMogSet } from '../core/models';

@Injectable()
export class TMogSetsService {
    private url = 'api/tmog-sets';
    private header = new Headers({ 'Content-Type': 'application/json' });

    constructor(private http: Http, private cache: TMogSetsCacheService) { }

    public getAll(): Promise<TMogSet[]> {
        return this.http.get(this.url)
            .toPromise()
            .then(response => {
                const results = response.json();
                this.cache.set(results);

                return results;
            })
            .catch(this.handleError);
    }

    public getById(setId: number): Promise<TMogSet> {
        const url = this.url + `/${setId}`;
        return this.http.get(url)
            .toPromise()
            .then(response => {
                return response.json();
            })
            .catch(this.handleError);
    }

    public createSet(setId: number): Promise<TMogSet> {
        const url = this.url;
        return this.http.post(url, setId.toString(), { headers: this.header })
            .toPromise()
            .then(response => {
                // since this means a new set has been added, clear existing cache
                this.cache.clear();
                return response.json();
            }, error => {
                const reason = error.json();
                return Promise.reject(
                    {
                        status: error.status || 400,
                        reason: reason.message || ''
                    }
                );
            });
    }

    public updateSetSlot(setId: number, slotName: string, value: boolean): Promise<boolean> {
        const url = this.url + `/${setId}/slot/${slotName}`;
        return this.http.put(url, value.toString(), { headers: this.header })
            .toPromise()
            .then(response => true)
            .catch(this.handleError);
    }

    public removeSet(setId: number): Promise<boolean> {
        const url = this.url;
        return this.http.delete(url, { body: setId, headers: this.header })
            .toPromise()
            .then(response => true)
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        if (error.status === 500) {
            return Promise.reject({ status: error.status, message: 'Unable to contact server' });
        }

        return Promise.reject(error.message || error);
    }
}
