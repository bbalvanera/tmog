import { TestBed, inject } from '@angular/core/testing';

import { ItemsByRegionService } from './items-by-region.service';

describe('ItemsByZoneService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ItemsByRegionService]
    });
  });

  it('should be created', inject([ItemsByRegionService], (service: ItemsByRegionService) => {
    expect(service).toBeTruthy();
  }));
});
