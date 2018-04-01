import * as fs from 'fs';

import { WorkCommand } from "../work.command";
import { FactionDataProvider } from './faction-data.provider';

export class FactionCommand implements WorkCommand {

  private dataProvider: FactionDataProvider;

  constructor() {
    this.dataProvider = new FactionDataProvider();
  }

  public async execute(): Promise<void> {
    const data = await this.dataProvider.getData();
    
    if (data) {
      fs.writeFile('factions.json', JSON.stringify(data), err => {
        if (err) {
          console.log('error while saving file', err);
        }

        console.log('done running command');
      });
    }
  }
}