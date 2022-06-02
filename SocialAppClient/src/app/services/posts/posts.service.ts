import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Post } from '../../models/Post';

@Injectable({
  providedIn: 'root'
})
export class PostsService {
  private API_URL = environment.API_URL;

  constructor(private http:HttpClient) { }

  create(post: Post){
    return this.http.post<Post>(`${this.API_URL}/posts/create`, post);
  }

  get(userId: number): Observable<Post[]>{
    return this.http.get<Post[]>(`${this.API_URL}/posts/${userId}`);
  }
}
