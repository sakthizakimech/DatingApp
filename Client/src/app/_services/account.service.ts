import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { User } from '../_Models/User';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private http=inject(HttpClient);
  BaseUrl='http://localhost:5241/api/';
  CurrentUser=signal<User | null>(null);
  Register(model:any)
  {

    return this.http.post<User>(this.BaseUrl+"Account/Register",model).pipe(
      map(
          user=>
          {
          if(user)
            {
             localStorage.setItem('user',JSON.stringify(user));
             this.CurrentUser.set(user);
            }
            return user;
          }
      )
    )
  }
  
  login(model:any)
  {

    return this.http.post<User>(this.BaseUrl+"Account/Login",model).pipe(
      map(
          user=>
          {
          if(user)
            {
             localStorage.setItem('user',JSON.stringify(user));
             this.CurrentUser.set(user);
            }
          }
      )
    )
  }
  
  logOut()
  {
    localStorage.removeItem('user');
    this.CurrentUser.set(null);
  }
}
