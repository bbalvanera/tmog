import * as fs from 'fs';

import { WorkCommand } from "../work.command";
import { WorldQuestDataProvider } from './world-quest-data.provider';


export class WorldQuestCommand implements WorkCommand {

  private dataProvider: WorldQuestDataProvider;

  constructor() {
    this.dataProvider = new WorldQuestDataProvider();
  }

  public async execute(): Promise<void> {
    const data = await this.dataProvider.getData();
    
    if (data) {
      fs.writeFile('wq.json', JSON.stringify(data), err => {
        if (err) {
          console.log('error while saving file', err);
        }

        console.log('done running command');
      });
    }
  }
}