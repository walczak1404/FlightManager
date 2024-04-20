import { BreakpointObserver, BreakpointState } from '@angular/cdk/layout';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit, OnDestroy {
  private _widthObserver: Subscription;
  biggerWidth: boolean;
  isAuthenticated: boolean;

  constructor(private _breakpointObserver: BreakpointObserver, private _accountService: AccountService) {}

  ngOnInit(): void {
    this._widthObserver = this._breakpointObserver.observe(['(min-width: 500px)'])
    .subscribe((state: BreakpointState) => {
      this.biggerWidth = state.matches ? true : false;
    });

    this._accountService.isAuthenticated.subscribe({
      next: userLoggedIn => {
        this.isAuthenticated = userLoggedIn;
      }
    })
  }

  ngOnDestroy(): void {
      this._widthObserver.unsubscribe();
  }
}
