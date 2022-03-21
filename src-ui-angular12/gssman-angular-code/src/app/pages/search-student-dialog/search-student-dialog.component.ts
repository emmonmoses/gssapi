import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { BroadcastMessage } from 'src/app/models/broadcast-message';
import { Student } from 'src/app/models/student';
import { BroadcastService } from 'src/app/providers/broadcast.service';
import { ToastService } from 'src/app/providers/toast.service';
import { StudentService } from '../transfer-page/student.service';


@Component({
  selector: 'app-search-student-dialog',
  templateUrl: './search-student-dialog.component.html',
  styleUrls: ['./search-student-dialog.component.sass']
})
export class SearchStudentDialogComponent implements OnInit {
  busy: boolean = false;
  // students: Student[] = [];
  student!: Student;
  searchText: string = '';
  form = new FormGroup({
    'searchText': new FormControl(''),
  });

  constructor(
    public studentService: StudentService,
    public toastService: ToastService,
    public broadcastService: BroadcastService,
    public dialog: MatDialog,
    public dialogRef: MatDialogRef<SearchStudentDialogComponent>,
  ) { }

  ngOnInit(): void {
  }

  public searchStudent(searchText: string): Promise<Student> {
    return this.studentService.get(searchText)
      .toPromise();
  }

  search(): void {
    this.busy = true;
    if (
      this.form.get('searchText')!.value === ''
    ) {
      this.toastService.warning('Student Id is mandatory');
      return;
    } else {
      this.searchText = this.form.get('searchText')?.value;
    }
    this.searchStudent(this.searchText).then((result: Student) => {
      this.student = result;
    }, (reject) => {
      this.toastService.error('Operation Failed');
    })
      .catch((error) => {
        this.toastService.error('Operation Failed');
      })
      .finally(() => {
        this.busy = false;
      });
  }

  select(student: Student) {

    const message = new BroadcastMessage('student-sender', student);
    this.broadcastService.broadcastTask(message);
    this.dialogRef.close();
  }



}
