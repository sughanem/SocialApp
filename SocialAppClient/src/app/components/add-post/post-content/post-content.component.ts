import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Post } from 'src/app/models/Post';
import { User } from 'src/app/models/User';
import { PostsService } from 'src/app/services/posts/posts.service';

@Component({
  selector: 'app-post-content',
  templateUrl: './post-content.component.html',
  styleUrls: ['./post-content.component.css']
})
export class PostContentComponent implements OnInit {
  @Input() user!: User;
  submitted = false;
  createPostForm!: FormGroup;
  fullName!: string;
 
  constructor(public activeModal: NgbActiveModal, private postService: PostsService) {}

  ngOnInit(): void {
    this.createPostForm = new FormGroup({
      content: new FormControl("", [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(3000)
      ]),
      visibility: new FormControl()
    });
    this.fullName = this.user.firstName + " " + this.user.lastName;
    this.visibility?.setValue(0);
  }

  onSubmit(){
    this.submitted = true;
    if (this.createPostForm.valid) {
      const createdPost : Post = {
        userId : this.user.id,
        content : this.content?.value,
        visibility : this.visibility?.value
      };
      this.postService.create(createdPost).subscribe({
        complete: () => this.activeModal.dismiss()
      });
    }
  }

  get visibility() {return this.createPostForm.get('visibility')}
  get content() { return this.createPostForm.get('content'); }
}
