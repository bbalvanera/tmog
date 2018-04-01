declare var window: any;

import { WebPage } from 'phantom';

import { BaseDataProvider } from '../base-data.provider';
import { WorldQuest } from '../entities/world-quest';

export class WorldQuestDataProvider extends BaseDataProvider<WorldQuest> {
  
  constructor() {
    super('http://www.wowhead.com/world-quests/na');

  }

  protected async getDataFromPage(page: WebPage): Promise<WorldQuest> {
    const dataObject = await page.evaluate(function () {
      return window.lvWorldQuests.data.map(function (quest: any) {
        return {
          id:       quest.id,
          name:     quest.__tr.innerText.split('\n')[0],
          ending:   quest.ending,
          factions: quest.factions,
          zones:    quest.zones,
          type:     quest.worldquesttype,
          rewards:  quest.rewards ? quest.rewards.items : []
        };
      });
    });

    return dataObject;
  }
}
