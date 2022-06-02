import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { User } from 'src/app/models/User';
import { PostsService } from 'src/app/services/posts/posts.service';

import { PostContentComponent } from './post-content/post-content.component';


@Component({
  selector: 'app-add-post',
  templateUrl: './add-post.component.html',
  styleUrls: ['./add-post.component.css']
})
export class AddPostComponent implements OnInit {

  @Input() user!: User;

  constructor(private modalService: NgbModal) { }

  ngOnInit(): void {
  }

  openPostContentComponent(){
    const modalRef = this.modalService.open(PostContentComponent, { 
        backdrop: 'static', 
        keyboard: false
    });
    modalRef.componentInstance.user = this.user;
  }

}
