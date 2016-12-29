import { Injectable } from '@angular/core';
import { Http, URLSearchParams } from '@angular/http';

import 'rxjs/add/operator/toPromise';

import { Item } from '../core/models';

@Injectable()
export class SearchService {
    private url = 'api/search';

    constructor(private http: Http) { }

    public performSearch(searchTerm: string): Promise<Item[]> {
        if (searchTerm && searchTerm.length > 0) {
            const params = new URLSearchParams();
            params.set('q', searchTerm);

            return this.http.get(this.url, { search: params })
                .toPromise()
                .then(response => {
                    return response.json();
                });
        }
    }
}
