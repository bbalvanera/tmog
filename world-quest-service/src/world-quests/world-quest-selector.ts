import { WorldQuest } from "./world-quest";

export class WorldQuestSelector {
  public async select(worldQuests: WorldQuest[]): Promise<WorldQuest[]> {
    return Promise.resolve([]);
  }
}

