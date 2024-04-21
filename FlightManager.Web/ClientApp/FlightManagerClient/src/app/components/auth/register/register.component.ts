import { Component, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../../../services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {

  @ViewChild("form", {static: true}) form: NgForm;
  serverErrorMessage: string = null;
  isLoading: boolean;

  constructor(private _router: Router, private _accountService: AccountService) { }

  onRegisterCancel() {
    this._router.navigate(["/"]);
  }

  onSubmitRegister() {
    this.serverErrorMessage = null;
    if(this.form.valid) {
      console.log(this.form.value.repeatPassword);
      this.isLoading = true;
      this._accountService.register(this.form.value.email, this.form.value.password, this.form.value.repeatPassword).subscribe({
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
