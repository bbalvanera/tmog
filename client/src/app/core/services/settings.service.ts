import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable()
export class SettingsService {

  constructor() {
    this.apiBaseUrl = environment.apiBaseUrl || "http://localhost/tmog/api";
  }

  public apiBaseUrl: string;
}
