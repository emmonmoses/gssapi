import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { SearchStudentDialogComponent } from '../search-student-dialog/search-student-dialog.component';
import { BranchService } from './branch.service';
import { Branch } from 'src/app/models/branch';
import { ToastService } from 'src/app/providers/toast.service';
import { BroadcastService } from 'src/app/providers/broadcast.service';
import { BroadcastMessage } from 'src/app/models/broadcast-message';
import { Student } from 'src/app/models/student';
import { TransferService } from './transfer.service';

@Component({
  selector: 'app-transfer-page',
  templateUrl: './transfer-page.component.html',
  styleUrls: ['./transfer-page.component.sass']
})
export class TransferPageComponent implements OnInit {
  studentDisabled = false;
  branches: Branch[] = [{ id: 1, name: 'BOLE' }, { id: 2, name: 'CMC' }, { id: 3, name: 'MEG' }, { id: 4, name: 'MEK' }, { id: 5, name: 'SARBET' }, { id: 6, name: 'LAFTO' }, { id: 7, name: 'KOLFE' }];
  busy: boolean = false;
  student!: Student;
  subscription: Subscription;

  form = new FormGroup({
    'studentId': new FormControl({ value: '', disabled: false }, [Validators.required]),
    'sourceBranch': new FormControl({ value: '' }, [Validators.required]),
    'destinationBranch': new FormControl({ value: '' }),
    'academicYear': new FormControl({ value: '' }, [Validators.required])
  });

  constructor(
    public route: Router,
    public dialog: MatDialog,
    public branchService: BranchService,
    public toastService: ToastService,
    public broadcastService: BroadcastService,
    public transferService: TransferService
  ) {
    this.subscription = this.broadcastService.subscribeTask()
      .subscribe((message: BroadcastMessage) => {
        if (message.sender === 'student-sender') {
          this.student = message.data;
          this.form.get('studentId')?.setValue(this.student.studentid);
        }
      });
  }

  ngOnInit(): void {
    // this.getAllBranches();
    this.branches;
  }

  save(studentId: String, sourceBranch: String, destinationBranch: String, academicYear: String): any {
    return this.transferService.create(studentId, sourceBranch, destinationBranch, academicYear);
  }

  create() {
    this.busy = true;
    if (this.form.get('studentId')?.value === '' || this.form.get('sourceBranch')?.value === '' || this.form.get('destinationBranch')?.value === '' || this.form.get('academicYear')?.value == '') {
      this.toastService.warning('One or More Mandatory Fields are Empty');
      return;
    }
    if (this.form.get('sourceBranch')?.value === this.form.get('destinationBranch')?.value) {
      this.toastService.warning('Source and Destination Branch cannot be the Same');
      return;
    }
    this.save(this.form.get('studentId')?.value, this.form.get('sourceBranch')?.value, this.form.get('destinationBranch')?.value, this.form.get('academicYear')?.value)
      .subscribe((res: any) => {
        if (res) {
          this.form.reset();
          this.toastService.success('Marks Successfully Transfered');
        }
        this.busy = false;
      },
      );
  }

  search(): void {
    const dialogRef = this.dialog.open(SearchStudentDialogComponent, {
      width: '550px',
      data: { show: true, },
      disableClose: true
    });

    dialogRef.afterClosed().subscribe(result => {
      // dialog closed
    });

    dialogRef.backdropClick().subscribe(() => {
      dialogRef.close();
    });
  }


  signOut() {
    this.route.navigate(['/index']);
  }

  // public getAllBranches() {
  //   this.busy = true;
  //   this.branchService.getAll()
  //     .then((result) => {
  //       this.branches = result;
  //     }, (reject) => {
  //       this.toastService.error('Technical Error');
  //     })
  //     .catch((error) => {
  //       this.toastService.error('Technical Error');
  //     })
  //     .finally(() => {
  //       this.busy = false;
  //     });
  // }

}
