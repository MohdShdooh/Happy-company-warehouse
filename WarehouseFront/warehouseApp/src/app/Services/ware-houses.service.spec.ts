import { TestBed } from '@angular/core/testing';

import { WareHousesService } from './ware-houses.service';

describe('WareHousesService', () => {
  let service: WareHousesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WareHousesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
