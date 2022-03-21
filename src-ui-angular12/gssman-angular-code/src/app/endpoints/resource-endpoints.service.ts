import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ResourceEndpointsService {

  // url = "http://192.168.1.75:8082";
  url = "http://aitechn.com/gsstransferapi";

  get LoginUri(): string {
    return `${this.url}/login`;
  }

  get BranchUri(): string {
    return `${this.url}/branch`;
  }

  StudentUri(id: String): string {
    return `${this.url}/student/${id}`;
  }

  TransferUri(studentId: String, sourceBranch: String, destinationBranch: String, academicyear: String): string {
    return `${this.url}/bulk/transfer?studentid=${studentId}&academicyear=${academicyear}&srcBranch=${sourceBranch}&destBranch=${destinationBranch}`;
  }
}
