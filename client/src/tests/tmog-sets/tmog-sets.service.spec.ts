import { TestBed, inject } from '@angular/core/testing';

import { TmogSetsService } from './tmog-sets.service';

describe('TmogSetsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TmogSetsService]
    });
  });

  it('should be created', inject([TmogSetsService], (service: TmogSetsService) => {
    expect(service).toBeTruthy();
  }));
});
