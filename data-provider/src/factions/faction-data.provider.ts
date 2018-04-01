declare var window: any;


import { WebPage } from 'phantom';

import { BaseDataProvider } from '../base-data.provider';
import { Faction } from '../entities/faction';

export class FactionDataProvider extends BaseDataProvider<Faction> {
  
  constructor() {
    super('http://www.wowhead.com/factions');

  }

  protected async getDataFromPage(page: WebPage): Promise<Faction> {
    const dataObject = await page.evaluate(function () {
      var data = window.g_listviews["factions"].data;

      return data.map(function (faction: any) {
        return {
          id:       faction.id,
          name:     faction.name,
          category: window.g_faction_categories[faction.category2],
          side:     window.g_faction_categories[faction.category]
        };
      });
    });

    return dataObject;
  }
}
