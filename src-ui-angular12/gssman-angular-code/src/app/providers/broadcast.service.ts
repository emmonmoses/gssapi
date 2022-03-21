import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BroadcastService {

  subject = new Subject<any>();

  constructor(
  ) { }

  broadcastTask(message: any): void {
    this.subject.next(message);
  }

  subscribeTask(): Observable<any> {
    return this.subject.asObservable();
  }
}
