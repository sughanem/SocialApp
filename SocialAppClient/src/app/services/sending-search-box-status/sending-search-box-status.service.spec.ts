import { TestBed } from '@angular/core/testing';

import { SendingSearchBoxStatusService } from './sending-search-box-status.service';

describe('SendingSearchBoxStatusService', () => {
  let service: SendingSearchBoxStatusService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SendingSearchBoxStatusService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
