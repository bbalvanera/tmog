import * as fs from 'fs';

import { WorkCommand } from "../work.command";
import { ZonesDataProvider } from "./zones-data.provider";
import { ZoneData } from './zone.data';

export class ZonesCommand implements WorkCommand {

  private dataProvider: ZonesDataProvider;

  constructor() {
    this.dataProvider = new ZonesDataProvider();
  }

  public async execute(): Promise<void> {
    const data = await this.dataProvider.getData();
    
    if (data) {
      const zonesTable = new ZoneData();
      data.forEach(zone => zonesTable.add(zone));
      await zonesTable.saveChanges();

      console.log('done executing commmand');
    }
  }
}