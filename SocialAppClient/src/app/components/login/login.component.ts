import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthService } from 'src/app/auth/auth.service';
import { SendingUserObjectService } from 'src/app/services/sending-user-object/sending-user-object.service';
import { UsersService } from 'src/app/services/users/users.service';
import { SignupComponent } from '../signup/signup.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  emailRe = "^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
  credentials = {email: '', password: ''};
  invalidCre = false;


  constructor(private modalService: NgbModal, private usersService:UsersService,
    private authService: AuthService,private sendingUserObjectService: SendingUserObjectService, 
    private router: Router) { }

  ngOnInit(): void {
  }

  login(loginForm: NgForm){
    if (loginForm.valid) {
      this.usersService.login(this.credentials.email, this.credentials.password)
      .subscribe({
        next: (v) => {
          this.sendingUserObjectService.send(v);
        },
        error: (e) => { 
          if (e.status === 400) {
            this.invalidCre = true;
          }
        },
        complete: () => {
          this.authService.isLogin(true);
          this.router.navigateByUrl('feed');
        }
      });
    }
  }

  openSignupComponent(){
    this.modalService.open(SignupComponent, 
      { 
        centered: true,
        backdrop: 'static', 
        keyboard: false
      });
  }


}
