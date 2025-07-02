import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

export const authGaurdGuard: CanActivateFn = (route, state) => {
  const accountservice = inject(AccountService);
  const toast = inject(ToastrService);
  if (accountservice.CurrentUser()) {
    return true;
  } else {
    toast.error("You are not Authorized");
    return false;
  }
};
