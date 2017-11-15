import { Injectable } from '@angular/core';
import { Http, URLSearchParams } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import { Item } from '../../common/models';
import { Settings } from '../../common/settings';

@Injectable()
export class SearchService {
  private endpoint: string;

  constructor(private http: Http, settingsService: Settings) {
    this.endpoint = `${settingsService.apiBaseUrl}/search`;
  }

  public performSearch(searchTerm: string): Observable<Item[]> {
    if (!searchTerm || searchTerm.length === 0) {
      return Observable.of(<Item[]>[]).delay(100);
    }

    const search = new URLSearchParams();
    search.set('q', searchTerm);

    return this.http.get(this.endpoint, { search })
      .map(response => {
        if (response.status === 204) {
          return undefined;
        } else {
          const result = response.json();
          return <Item[]>result.results;
        }
      });
  }
}
