import { Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../../../services/account.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  @ViewChild("form", {static: true}) form: NgForm;
  serverErrorMessage: string = null;

  constructor(private _router: Router, private _accountService: AccountService) { }

  onLoginCancel() {
    this._router.navigate(["/"]);
  }

  onSubmitLogin() {
    if(this.form.valid) {
      this._accountService.login(this.form.value.email, this.form.value.password)
      .subscribe({
        next: resData => {
          console.log("GIT");
          console.log(resData);
        },

        error: errorData => {
          this.serverErrorMessage = errorData.message;
        }
      })
    }
  }
}
