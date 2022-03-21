import { Injectable } from '@angular/core';
import { HttpService } from 'src/app/providers/http.service';
import { map } from 'rxjs/operators';

import { Branch } from 'src/app/models/branch';
import { ResourceEndpointsService } from 'src/app/endpoints/resource-endpoints.service';

@Injectable({
  providedIn: 'root'
})
export class BranchService {

  constructor(
    private http: HttpService,
    private resourceEndpointsService: ResourceEndpointsService,
  ) { }

  getAll(): Promise<Branch[]> {
    return this.http.get(
      this.resourceEndpointsService.BranchUri)
      .pipe(map((response: any) => response)).toPromise();
  }
}
