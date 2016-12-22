import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { TMogSet } from '../models/tmog-set';

@Injectable()
export class TMogSetsService {
    private url = 'api/tmog-sets';
    private header = new Headers({ 'Content-Type': 'application/json' });

    constructor(private http: Http) { }

    public getAll(): Promise<TMogSet[]> {
        return this.http.get(this.url)
            .toPromise()
            .then(response => {
                return response.json();
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

    public saveSet(setId: number): void {
        const url = this.url;
        this.http.post(url, setId.toString(), { headers: this.header })
            .toPromise()
            .then(response => {
                response.headers.has('Location');
            })
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('handled error', error);

        return Promise.reject(error.message || error);
    }
}
