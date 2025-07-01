import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { NgIf } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@Component({
  selector: 'app-nav-bar',
  imports: [FormsModule,BsDropdownModule],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.css'
})
export class NavBarComponent {
  private accountService=inject(AccountService);
  loggedIn=false;
model:any={};
login()
{
this.accountService.login(this.model).subscribe({
  next:response=>{
    console.log(response),
    this.loggedIn=true;
  },
  error:error=>console.log(error)
}
)
}
logout()
{
this.loggedIn=false
}
}
