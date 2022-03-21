import { Injectable } from '@angular/core';
import { HttpService } from 'src/app/providers/http.service';

import { User } from 'src/app/models/user';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs/operators';
import { UserLogin } from 'src/app/models/login';
import { ResourceEndpointsService } from 'src/app/endpoints/resource-endpoints.service';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(
    private http: HttpService,
    private resourceEndpointsService: ResourceEndpointsService,
  ) { }

  create(login: UserLogin): Observable<User> {
    return this.http.post(this.resourceEndpointsService.LoginUri, login)
      .pipe(map((response: any) => response));
  }
}
