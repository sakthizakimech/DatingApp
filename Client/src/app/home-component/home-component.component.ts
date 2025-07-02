import { Component, inject, OnInit } from '@angular/core';
import { RegisterComponentComponent } from "../register-component/register-component.component";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home-component',
  imports: [RegisterComponentComponent],
  templateUrl: './home-component.component.html',
  styleUrl: './home-component.component.css'
})
export class HomeComponentComponent implements OnInit {
registerMode=false;
http=inject(HttpClient);
users:any;
ngOnInit(): void {
    this.GetUsers();
  }
register()
{
  this.registerMode=!this.registerMode;
}
CancelRegisterToggle(event:boolean)
{
this.registerMode=event;
}
GetUsers()
  {
      this.http.get('http://localhost:5241/api/User').subscribe({
      next:response=> this.users=response,
      error:error=>console.log(error),
      complete:()=>console.log('Request has Completed')
    })
  }
}
