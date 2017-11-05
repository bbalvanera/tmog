import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http';

import { TmogSet } from '../../core/models';
import { Settings } from '../../core/settings';

@Injectable()
export class TmogSetsService {
  private endpoint: string;
  private header = new Headers({ 'Content-Type': 'application/json' });

  constructor(private http: Http, settingsService: Settings) {
    this.endpoint = `${settingsService.apiBaseUrl}/tmog-sets`;
  }

  public getAll(): Promise<TmogSet[]> {
    return this.http.get(this.endpoint)
      .toPromise()
      .then(response => {
        const result = response.json();
        return result.sets;
      })
      .catch(this.handleError);
  }

  public getById(setId: number): Promise<TmogSet> {
    const url = this.endpoint + `/${setId}`;
    return this.http.get(url)
      .toPromise()
      .then(response => {
        return response.json();
      })
      .catch(this.handleError);
  }

  public createSet(setId: number): Promise<TmogSet> {
    const url = this.endpoint;
    return this.http.post(url, setId.toString(), { headers: this.header })
      .toPromise()
      .then(response => {
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
    const url = this.endpoint + `/${setId}/slot/${slotName}`;
    return this.http.put(url, value.toString(), { headers: this.header })
      .toPromise()
      .then(response => true)
      .catch(this.handleError);
  }

  public removeSet(setId: number): Promise<boolean> {
    const url = this.endpoint;
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
