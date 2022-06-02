import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../../models/User';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private API_URL = environment.API_URL;

  constructor(private http: HttpClient) {
  }

  login(email: string, password: string) {
    return this.http.post<User>(`${this.API_URL}/users/login`, { email, password });
  }

  signup(user: User) {
    return this.http.post<User>(`${this.API_URL}/users/register`, user);
  }

  get(userName: string) {
    return this.http.get<User>(`${this.API_URL}/users/${userName}`);
  }

  searchUsers(query: string) {
    return this.http.get<Array<User>>(`${this.API_URL}/users/search/${query}`);
  }

  getAll(){
    return this.http.get<User[]>(`${this.API_URL}/users`);
  }
}
