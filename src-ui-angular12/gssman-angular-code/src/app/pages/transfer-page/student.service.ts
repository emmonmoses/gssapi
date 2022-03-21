import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ResourceEndpointsService } from 'src/app/endpoints/resource-endpoints.service';
import { Student } from 'src/app/models/student';
import { HttpService } from 'src/app/providers/http.service';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  constructor(
    private http: HttpService,
    private resourceEndpointsService: ResourceEndpointsService,
  ) { }

  get(id: String): Observable<Student> {
    return this.http.get(
      this.resourceEndpointsService.StudentUri(id))
      .pipe(map((response: any) => response));
  }
}
