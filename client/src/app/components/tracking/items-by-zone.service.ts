import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import { Region } from '../../common/models';
import { Settings } from '../../common/settings';

@Injectable()
export class ItemsByZoneService {
  private endpoint: string;

  constructor(private http: Http, settings: Settings) {
    this.endpoint = `${settings.apiBaseUrl}/itemsbyregion`;
  }

  public all(setId?: number): Observable<Region[]> {
    return this.http.get(this.endpoint)
      .map(response => {
        return response.json();
      })
      .map(result => {
        return result.regions;
      });
  }
}
