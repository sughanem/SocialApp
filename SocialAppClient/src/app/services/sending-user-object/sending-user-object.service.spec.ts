import { TestBed } from '@angular/core/testing';

import { SendingUserObjectService } from './sending-user-object.service';

describe('SharedService', () => {
  let service: SendingUserObjectService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SendingUserObjectService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
