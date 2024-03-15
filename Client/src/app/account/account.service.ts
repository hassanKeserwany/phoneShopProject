import { Injectable } from '@angular/core';
import {  ReplaySubject, map } from 'rxjs';
import { IUser } from '../shared/models/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl;

  private currentUserSource = new ReplaySubject<IUser>(1); //it holds one user

  currentUser$ = this.currentUserSource.asObservable();
  constructor(private http: HttpClient, private router: Router) {}

 

  loadCurrentUser(token:string){
    let headers=new HttpHeaders();
    headers=headers.set('Authorization',`Bearer ${token}`);
     return this.http.get<IUser>(this.baseUrl + 'Account/GetCurrentUser',{headers}).pipe( // Account/GetCurrentUser
      map((user:IUser)=>{
        if(user){
          localStorage.setItem('token',user.token);
          this.currentUserSource.next(user);
        }
      })
     );
  }

  login(values: IUser) {
    return this.http.post<IUser>(this.baseUrl + 'Account/login', values).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  register(values: IUser) {
    return this.http.post<IUser>(this.baseUrl + 'Account/register', values).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next({ email: '', dispalyName: '', token: '' });
    this.router.navigateByUrl('/');
  }

  checkEmailExists(email: string) {
    return this.http.get(
      this.baseUrl + 'Account/checkEmailExists?email=' + email
    );
  }
}
