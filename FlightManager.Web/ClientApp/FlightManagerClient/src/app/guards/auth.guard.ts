import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountService } from '../services/account.service';

export const isAuthorized: CanActivateFn = () => {
  const accountService = inject(AccountService);
  const router = inject(Router);
  if(accountService.isAuthenticated.getValue()) return true;
  else {
    router.navigate(["login"]);
    return false;
  }
};

export const isUnauthorized: CanActivateFn = () => {
  const accountService = inject(AccountService);
  const router = inject(Router);
  if(!accountService.isAuthenticated.getValue()) return true;
  else {
    router.navigate(["/"]);
    return false;
  }
}