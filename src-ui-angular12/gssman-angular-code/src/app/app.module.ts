import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './pages/login/login.component';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AvatarModule } from 'ngx-avatar';
import { ToastrModule } from 'ngx-toastr';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatNativeDateModule } from '@angular/material/core';
import { HttpClientModule } from '@angular/common/http';
import { MaterialExampleModule } from '../material.module';
import { TransferPageComponent } from './pages/transfer-page/transfer-page.component';
import { SearchStudentDialogComponent } from './pages/search-student-dialog/search-student-dialog.component';
import { LoginService } from './pages/login/login.service';
import { ToastService } from './providers/toast.service';
import { TransferService } from './pages/transfer-page/transfer.service';
import { BranchService } from './pages/transfer-page/branch.service';
import { StudentService } from './pages/transfer-page/student.service';
import { BroadcastService } from './providers/broadcast.service';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    PageNotFoundComponent,
    TransferPageComponent,
    SearchStudentDialogComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatNativeDateModule,
    MaterialExampleModule,
    AvatarModule,
    CommonModule,
    MatDialogModule,
    ToastrModule.forRoot(),
  ],
  providers: [LoginService, ToastService, TransferService, BranchService, StudentService, BroadcastService,
    { provide: MAT_DIALOG_DATA, useValue: {} },
    { provide: MatDialogRef, useValue: {} }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
