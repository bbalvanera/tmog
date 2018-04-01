export interface WorldQuest {
  id: number,
  name: string,
  ending: number,
  factions: number[],
  zones: number[],
  type: number,
  rewards: { id: number, qty: number }
}