import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { NgIf } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav-bar',
  imports: [FormsModule,BsDropdownModule,RouterLink,RouterLinkActive],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css'
})
export class NavBarComponent {
  accountService=inject(AccountService);
  toast=inject(ToastrService)
  router=inject(Router);
model:any={};
login()
{
this.accountService.login(this.model).subscribe({
  next:()=>{
    this.router.navigateByUrl('/members')
  },
  error:error=>this.toast.error(error.error)
}
)
}
logout()
{
  this.accountService.logOut();
  this.router.navigateByUrl('/');
}
}

