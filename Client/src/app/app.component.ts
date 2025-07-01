import { CommonModule, NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavBarComponent } from "./nav-bar/nav-bar.component";

@Component({
  selector: 'app-root',
  standalone:true,
  imports: [RouterOutlet, CommonModule, NavBarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  http=inject(HttpClient);
  title = 'Client';
  users:any;

  ngOnInit(): void {
    this.http.get('http://localhost:5241/api/User').subscribe({
      next:response=> this.users=response,
      error:error=>console.log(error),
      complete:()=>console.log('Request has Completed')
    })
  }
}
