import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal, NgbCalendar, NgbDateAdapter, NgbDateNativeAdapter, NgbInputDatepickerConfig } from '@ng-bootstrap/ng-bootstrap';
import { UsersService } from 'src/app/services/users/users.service';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth/auth.service';
import { SendingUserObjectService } from 'src/app/services/sending-user-object/sending-user-object.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
  providers: [NgbInputDatepickerConfig,
    {provide: NgbDateAdapter, useClass: NgbDateNativeAdapter}]
})
export class SignupComponent implements OnInit {

  private emailRe = "^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";
  private passwordRe = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$";
  signupForm!: FormGroup;
  popoverText!: string;
  submitted = false;
  calendar: NgbCalendar;


  constructor(public activeModal: NgbActiveModal, dateConfig: NgbInputDatepickerConfig,
    calendar: NgbCalendar, private usersService:UsersService, private authService: AuthService, 
    private sendingUserObjectService: SendingUserObjectService, private router: Router) {
      dateConfig.autoClose = 'outside';
      dateConfig.placement = ['top-start'];
      this.calendar = calendar;
   }


  ngOnInit(): void {
    this.signupForm = new FormGroup({
      firstName: new FormControl("", [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(50),
      ]),
      lastName: new FormControl("", [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(50),
      ]),
      email: new FormControl("", [
        Validators.required,
        Validators.maxLength(255),
        Validators.pattern(this.emailRe),
      ]),
      password: new FormControl("", [
        Validators.required,
        Validators.minLength(8),
        Validators.maxLength(60),
        Validators.pattern(this.passwordRe),
      ]),
      DOB: new FormControl("", [
        Validators.required
      ]),
      gender: new FormControl("", [
        Validators.required
      ])
    });
  }

  onSubmit(){
    this.submitted = true;
    if (this.signupForm.valid) {
      this.usersService.signup(this.signupForm.value)
      .subscribe({
        next: (v) => {
          this.sendingUserObjectService.send(v);
        },
        error: (e) => {
          if (e.status === 409) {
            this.email?.setErrors({
              uniqueEmail: true
            });
          }
        },
        complete: () => {
          this.authService.isLogin(true);
          this.router.navigateByUrl('feed');
          this.activeModal.dismiss();
        }
      });
    }
  }

  get firstName() { return this.signupForm.get('firstName'); }
  get lastName() { return this.signupForm.get('lastName'); }
  get email() { return this.signupForm.get('email'); }
  get password() { return this.signupForm.get('password'); }
  get DOB() { return this.signupForm.get('DOB'); }
  get gender() { return this.signupForm.get('gender'); }
}