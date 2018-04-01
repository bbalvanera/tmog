import * as yargs from 'yargs';
import * as fs from 'fs';

import { logger, log } from './logging/logger'

import { WorldQuest } from './world-quests/world-quest';
import { WorldQuestsService } from './world-quests/world-quests.service';
import { TmogService } from './tmog/tmog.service';

const argv = yargs.alias('u', 'posturl').argv;

class Program {
  private static postUrl: string;

  static async main(args: string[]) {
    log('starting process');
    this.processArguments();

    log('downloading world quests from wowhead');
    const wq = await this.downloadWorldQuests();

    if (!wq || wq.length === 0) {
      log(`no available world quests to upload`);
    } else {
      log(`uploading world quests to ${this.postUrl}`);
      await this.postToTmog(wq);
    }

    log('finished process');
    log('-------------------------------------------------');
  }

  private static processArguments(): void {
    if (!argv.posturl || argv.posturl === '') {
      log('No post url was specified. Exiting program...');
      process.exit(1);
    }

    this.postUrl = argv.posturl;
  }

  private static async downloadWorldQuests(): Promise<WorldQuest[]> {
    let worldQuests = <WorldQuest[]>[];

    try {
      const worldQuestsService = new WorldQuestsService();
      worldQuests = await worldQuestsService.getActiveWorldQuests();
  
      log(`downloaded [${worldQuests.length}] world quests from wowhead`);
  
      return worldQuests;
    } catch (reason) {
      log(`could not download world quests due to: ${reason}`);
      return [];
    }
  }

  private static async postToTmog(worldQuests: WorldQuest[]) {
    try {
      const tmogService = new TmogService('http://localhost/tmogwebapi/world-quests/upload');
      const response = await tmogService.uploadWorldQuests(worldQuests);
  
      log(`uploaded to tmog. [${response.processedRecords}] proccessed at the end`);
    } catch (reason) {
      log(`could not upload to tmog: ${reason}`)
      await this.dumpToFile(worldQuests);
    }
  }

  private static async dumpToFile(worldQuests: WorldQuest[]): Promise<void> {
    if (!worldQuests || worldQuests.length === 0) {
      return;
    }

    log(`dumping to file`);
    try {
      const now = new Date();
      const today = `${now.getFullYear()}.${now.getMonth()}.${now.getDate()}-${now.getHours()}.${now.getMinutes()}.${now.getSeconds()}`;
      const filename = `wq-${today}.json`;
      fs.writeFileSync(filename, JSON.stringify(worldQuests));
      log(`dumped to file ${filename}`);
    } catch (err) {
      log(`could not save to file due to: ${err}`);
    }
  }
}

Program.main(process.argv);