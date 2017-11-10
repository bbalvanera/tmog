import { Injectable } from '@angular/core';
import { Http, RequestOptionsArgs } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import { Region } from '../../common/models';
import { Settings } from '../../common/settings';

@Injectable()
export class ItemsService {
  private endpoint: string;

  constructor(private http: Http, settings: Settings) {
    this.endpoint = `${settings.apiBaseUrl}/items`;
  }

  public all(filterBy: string, filterValue: string): Observable<Region[]> {
    const params = {
      [filterBy]: filterValue
    };

    return this.http.get(this.endpoint, { params })
      .map(response => {
        return response.json();
      })
      .map(result => {
        return result.regions;
      });
  }
}
