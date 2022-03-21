import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserLogin } from 'src/app/models/login';
import { ToastService } from 'src/app/providers/toast.service';
import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass']
})
export class LoginComponent implements OnInit {
  public busy: boolean = false;
  model!: UserLogin;
  form = new FormGroup({
    'username': new FormControl('', [Validators.required]),
    'userpassword': new FormControl('', [Validators.required]),
  });

  constructor(
    public route: Router,
    public loginService: LoginService,
    public toastService: ToastService,
  ) { }

  ngOnInit(): void {
  }

  getErrorMessage() {
    if (this.form.get('username')!.hasError('required')) {
      return 'You must enter a value';
    }

    return this.form.get('username')!.hasError('username') ? 'Not a valid username' : '';
  }

  getPasswordErrorMessage() {
    if (this.form.get('userpassword')!.hasError('required')) {
      return 'Please enter a valid password';
    }

    return this.form.get('userpassword')!.hasError('password') ? 'Not a valid password' : '';
  }

  save(login: UserLogin): Promise<any> {
    return this.loginService.create(login).toPromise();
  }

  login() {
    this.busy = true;
    this.model = this.form.value as UserLogin;
    if (this.model.username === '' || this.model.userpassword === '') {
      this.toastService.warning('Fill all the mandatory fields marked with *');
      return;
    }
    this.save(this.model).then((res) => {
      if (res) {
        this.route.navigate(['/transfer']);
        this.toastService.success('Successful');
      }
    },
      (reject) => {
        this.toastService.error('Login Failed');
      })
      .catch((error) => {
        this.toastService.error('Login Failed');
      })
      .finally(() => {
        this.busy = false;
      });
  }
}
