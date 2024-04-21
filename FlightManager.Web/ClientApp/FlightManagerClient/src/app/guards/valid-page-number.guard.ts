import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const validPageNumberGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);

  const pageNumber = +route.paramMap.get("pageNumber");

  if(pageNumber >= 1) return true;
  else {
    router.navigate(["/flights", "1"]);
    return false;
  }
};
