import { TestBed } from '@angular/core/testing';

import { SharedMediaStreamerApiService } from './sharedmediastreamer-api.service';

describe('SharedMediaStreamerApiService', () => {
  let service: SharedMediaStreamerApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SharedMediaStreamerApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
