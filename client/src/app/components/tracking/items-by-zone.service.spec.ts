import { TestBed, inject } from '@angular/core/testing';

import { ItemsByZoneService } from './items-by-zone.service';

describe('ItemsByZoneService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ItemsByZoneService]
    });
  });

  it('should be created', inject([ItemsByZoneService], (service: ItemsByZoneService) => {
    expect(service).toBeTruthy();
  }));
});
