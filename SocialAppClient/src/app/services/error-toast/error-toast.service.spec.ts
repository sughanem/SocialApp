import { TestBed } from '@angular/core/testing';

import { ErrorToastService } from './error-toast.service';

describe('ErrorToastService', () => {
  let service: ErrorToastService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ErrorToastService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
