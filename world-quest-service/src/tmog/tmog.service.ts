import * as request from 'request-promise-native';

import { WorldQuest } from '../world-quests/world-quest';

export class TmogService {
  constructor(private endPoint: string) {

  }
  public async uploadWorldQuests(worldQuests: WorldQuest[]): Promise<{ processedRecords: number }> {

    const options = this.getRequestOptions();
    options.body  = worldQuests;

    const result = await request(options);
    return result;
  }

  private getRequestOptions(): request.Options {
    return {
      method: 'POST',
      url: this.endPoint,
      json: true
    };
  }
}