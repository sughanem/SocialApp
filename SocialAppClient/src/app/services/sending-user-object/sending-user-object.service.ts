import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { User } from '../../models/User';

@Injectable({
  providedIn: 'root'
})
export class SendingUserObjectService {

  private userObjectSource = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('userObject') || '{}'));
  userObjectSource$ = this.userObjectSource.asObservable();

  constructor() { }

  send(userObject: User){
    localStorage.setItem('userObject', JSON.stringify(userObject));
    this.userObjectSource.next(userObject);
  }

}
