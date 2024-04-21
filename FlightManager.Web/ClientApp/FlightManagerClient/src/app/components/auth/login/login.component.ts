import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../../../services/account.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {
  @ViewChild("form", {static: true}) form: NgForm;
  serverErrorMessage: string = null;
  isLoading: boolean; 

  constructor(private _router: Router, private _accountService: AccountService) { }

  ngOnInit() {
    this.isLoading = false;
  }

  onLoginCancel() {
    this._router.navigate(["/"]);
  }

  onSubmitLogin() {
    this.serverErrorMessage = null;
    if(this.form.valid) {
      this.isLoading = true;
      this._accountService.login(this.form.value.email, this.form.value.password)
      .subscribe({
        next: resData => {
          this._router.navigate(["/"]);
        },

        error: errorData => {
          this.serverErrorMessage = errorData.message;
          this.isLoading = false;
        }
      })
    }
  }
}
