import { CommonModule, NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavBarComponent } from "./nav-bar/nav-bar.component";
import { AccountService } from './_services/account.service';
import { HomeComponentComponent } from "./home-component/home-component.component";

@Component({
  selector: 'app-root',
  standalone:true,
  imports: [RouterOutlet, CommonModule, NavBarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  private accountService=inject(AccountService)

  ngOnInit(): void {
    this.setCurrentuser();
  }
  
  setCurrentuser()
  {
    const userString=localStorage.getItem('user');
    if(!userString) return;
    const user=JSON.parse(userString);
    this.accountService.CurrentUser.set(user);
  }
}
