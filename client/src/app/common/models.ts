
  export interface Item {
    id?: number;
    name?: string;
    quality?: number;
    sets?: TmogSet[];
    source?: Source;
  }
  export interface Region {
    id?: number;
    name?: string;
    zones?: ZoneItems[];
  }
  export interface Slot {
    complete?: boolean;
    items?: Item[];
    slotName?: string;
    slotNumber?: number;
  }
  export interface Source {
    description?: string;
    dropLevel?: number;
    dropLevelName?: string;
    id?: number;
    subType?: string;
    type?: string;
    wowheadId?: number;
    zone?: Zone;
  }
  export interface TmogSet {
    completedSlots?: number;
    id?: number;
    name?: string;
    slots?: Slot[];
    totalSlots?: number;
  }
  export interface Zone {
    id?: number;
    name?: string;
    parentId?: number;
    parentZoneName?: string;
  }
  export interface ZoneItem {
    itemId?: number;
    itemName?: string;
    itemQuality?: number;
    regionId?: number;
    regionName?: string;
    setId?: number;
    setName?: string;
    setSlots?: string;
    slot?: string;
    source?: string;
    sourceId?: number;
    sourceSubType?: string;
    sourceType?: string;
    zoneDifficulty?: number;
    zoneId?: number;
    zoneName?: string;
  }
  export interface ZoneItems {
    difficulty?: number;
    id?: number;
    items?: ZoneItem[];
    name?: string;
  }
