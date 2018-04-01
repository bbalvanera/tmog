import * as phantom from 'phantom';

import { Browser } from '../common/browser';
import { WorldQuest } from './world-quest';

declare var window: any;

export class WorldQuestsService {
  private browser: phantom.PhantomJS;
  
    constructor() {
  
    }
  
    public async getActiveWorldQuests(): Promise<WorldQuest[]> {
      const browser = new Browser();
      const page    = await browser.openUrl('http://www.wowhead.com/world-quests/na');
      const result  = await this.getDataFromPage(page);

      browser.dispose();

      return result;
    }
  
    private async getDataFromPage(page: phantom.WebPage): Promise<WorldQuest[]> {
      // use function (), lambdas do not work with page.evaluate
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