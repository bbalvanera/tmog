
  export interface Item {
    id?: number;
    name?: string;
    quality?: number;
    sets?: TMogSet[];
    source?: Source;
  }
  export interface Slot {
    complete?: boolean;
    items?: Item[];
    name?: string;
  }
  export interface Source {
    description?: string;
    id?: number;
    subType?: string;
    type?: string;
    wowheadId?: number;
    zone?: Zone;
  }
  export interface TMogSet {
    completedSlots?: number;
    id?: number;
    name?: string;
    slots?: Slot[];
    totalSlots?: number;
  }
  export interface Zone {
    id?: number;
    locationId?: number;
    locationName?: string;
    name?: string;
  }
