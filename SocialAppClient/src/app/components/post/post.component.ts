import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Post } from 'src/app/models/Post';
import { User } from 'src/app/models/User';
import { PostsService } from 'src/app/services/posts/posts.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {
  @Input() user: User | any;
  userPosts$: Observable<Post[]> | any;

  constructor(private postsService: PostsService, public dateFormatPipe: DatePipe) {
   }

  ngOnInit(): void {
    this.userPosts$ = this.postsService.get(this.user.id);
  }

}
