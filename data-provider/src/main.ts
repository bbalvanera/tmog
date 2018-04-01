import { WorkCommand } from "./work.command";
import { ZonesCommand } from "./zones/zones.command";
import { WorldQuestCommand } from "./world-quests/world-quest.command";
import { FactionCommand } from "./factions/faction.command";

const availableCommands: { [key: string]: WorkCommand } = {
  ['--zones']: new ZonesCommand(),
  ['--wq']: new WorldQuestCommand(),
  ['--factions']: new FactionCommand()
}

const cmd = getCommand();

if (cmd) {
  console.log('executing command')
  cmd.execute();
} else {
  console.log('Invalid command supplied');
}

function getCommand(): WorkCommand | null {
  for(let i = 0; i < process.argv.length; i++) {
    const arg = process.argv[i];
    const cmd = availableCommands[arg];

    if (cmd)
      return cmd;
    
  }

  return null;
}