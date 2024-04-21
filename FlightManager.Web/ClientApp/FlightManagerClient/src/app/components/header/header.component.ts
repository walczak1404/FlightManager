import { BreakpointObserver, BreakpointState } from '@angular/cdk/layout';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AccountService } from '../../services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit, OnDestroy {
  private _widthObserver: Subscription;
  private _authenticationObserver: Subscription;
  biggerWidth: boolean;
  isAuthenticated: boolean;

  constructor(private _breakpointObserver: BreakpointObserver, private _accountService: AccountService, private _router: Router) {}

  ngOnInit(): void {
    this._widthObserver = this._breakpointObserver.observe(['(min-width: 500px)'])
    .subscribe((state: BreakpointState) => {
      this.biggerWidth = state.matches ? true : false;
    });

    this._authenticationObserver = this._accountService.isAuthenticated.subscribe({
      next: userLoggedIn => {
        this.isAuthenticated = userLoggedIn;
      }
    })
  }

  ngOnDestroy(): void {
      this._widthObserver.unsubscribe();
      this._authenticationObserver.unsubscribe();
  }

  onRedirectToMainPage() {
    this._router.navigate(["/"]);
  }

  onLogout() {
    this._accountService.logout().subscribe({
      next: () => {
        this._router.navigate(["/"]);
      }
    });
  }
}
