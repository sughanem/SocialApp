import { TestBed } from '@angular/core/testing';

import { SendingTabStatusService } from './sending-tab-status.service';

describe('SendingTabStatusService', () => {
  let service: SendingTabStatusService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SendingTabStatusService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
