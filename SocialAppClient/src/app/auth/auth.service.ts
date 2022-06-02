import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private isLoggedIn: boolean;
  redirectUrl: string = '';

  constructor(){
    const loginStatus = localStorage.getItem('loginStatus');
    this.isLoggedIn = loginStatus !== null ? JSON.parse(loginStatus) : false;
  }

  isLogin(loginStatus: boolean){
    this.isLoggedIn = loginStatus;
    localStorage.setItem('loginStatus', JSON.stringify(loginStatus))
  }

  getLoginStatus(){
    return this.isLoggedIn;
  }
  
}
