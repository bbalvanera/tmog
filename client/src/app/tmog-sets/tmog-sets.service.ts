import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { TMogSet } from '../models/tmog-set';

@Injectable()
export class TMogSetsService {
    private url = 'api/tmog-sets';

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
        var url = this.url + `/${setId}`;
        return this.http.get(url)
            .toPromise()
            .then(response => {
                return response.json();
            })
            .catch(this.handleError);
    }

    public saveSet(set: TMogSet): void {

    }

    private handleError(error: any): Promise<any> {
        console.error('handled error', error);

        return Promise.reject(error.message || error);
    }
}
