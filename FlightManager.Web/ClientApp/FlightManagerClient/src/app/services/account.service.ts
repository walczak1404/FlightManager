import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { BehaviorSubject, Observable, catchError, tap, throwError } from 'rxjs';
import { AuthenticationResponse } from '../models/authenticationResponse.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  isAuthenticated = new BehaviorSubject<boolean>(false);
  tokenRefreshTimer = null;

  constructor(private _httpClient: HttpClient) { }

  login(email: string, password: string): Observable<AuthenticationResponse> {
    return this._httpClient.post<AuthenticationResponse>(`${environment["API_URL"]}/Account/login`, {
      email,
      password
    }).pipe(catchError(this.handleError), tap((resData: AuthenticationResponse) => this.handleAuthentication(resData)));
  }

  register(email: string, password: string, confirmPassword: string) {
    return this._httpClient.post<AuthenticationResponse>(`${environment["API_URL"]}/account/register`, {
      email,
      password,
      confirmPassword
    }).pipe(catchError(this.handleError), tap((resData: AuthenticationResponse) => this.handleAuthentication(resData)));
  }

  refreshToken() {
    return this._httpClient.put<AuthenticationResponse>(`${environment["API_URL"]}/account/refresh-token`, {
      token: localStorage.getItem("token"),
      refreshToken: localStorage.getItem("refreshToken")
    }).subscribe({
      next: (resData: AuthenticationResponse) => {
        this.handleAuthentication(resData);
      },

      error: (errorRes: HttpErrorResponse) => {
        console.log(errorRes.message);
        this.logout();
      }
    })
  }

  logout() {
    
  }

  private handleAuthentication(resData: AuthenticationResponse) {
    localStorage.setItem("token", resData.token);
    localStorage.setItem("refreshToken", resData.refreshToken);
    this.isAuthenticated.next(true);

    clearTimeout(this.tokenRefreshTimer);

    this.tokenRefreshTimer = setTimeout(() => {
      this.refreshToken();
    }, resData.tokenExpiresInMinutes*60000-5000);
  }

  private handleError(errorRes: HttpErrorResponse) {
    console.log(errorRes);
    return throwError(() => new Error(errorRes.error.detail));
  }
}
