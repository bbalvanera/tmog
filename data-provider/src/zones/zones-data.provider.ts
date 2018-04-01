declare var window: any;

import { WebPage } from 'phantom';

import { BaseDataProvider } from '../base-data.provider';
import { Zone } from '../entities/zone';

export class ZonesDataProvider extends BaseDataProvider<Zone[]> {

  constructor() {
    super('http://www.wowhead.com/zones');
  }

  protected async getDataFromPage(page: WebPage): Promise<Zone[]>  {
    const dataObject = await page.evaluate(function () {

      var zones = window.zonedata.zones.map(function (item: any) {
        var type = item.__tr.querySelector('td:last-child span:last-child');
        return {
            id:       item.id,
            name:     item.name,
            category: window.g_zone_categories[item.category],
            type:     type ? type.innerHTML: 'Zone'
        };
      });

      return <Zone[]>zones;
    });

    return dataObject;
  }
}
