import { Injectable } from '@angular/core';
import { HttpService } from 'src/app/providers/http.service';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs/internal/Observable';
import { ResourceEndpointsService } from 'src/app/endpoints/resource-endpoints.service';

@Injectable({
  providedIn: 'root'
})
export class TransferService {

  constructor(
    private http: HttpService,
    private resourceEndpointsService: ResourceEndpointsService,
  ) { }

  create(studentId: String, sourceBranch: String, destinationBranch: String, academicyear: String): Observable<any> {
    return this.http.post(this.resourceEndpointsService.TransferUri(studentId, sourceBranch, destinationBranch, academicyear), null, { responseType: 'text' })
      .pipe(map((response: any) => response));
  }

}
