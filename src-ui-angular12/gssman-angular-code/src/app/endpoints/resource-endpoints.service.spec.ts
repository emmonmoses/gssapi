import { TestBed } from '@angular/core/testing';

import { ResourceEndpointsService } from './resource-endpoints.service';

describe('ResourceEndpointsService', () => {
  let service: ResourceEndpointsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ResourceEndpointsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
