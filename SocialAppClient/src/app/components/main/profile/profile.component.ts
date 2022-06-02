import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { UsersService } from 'src/app/services/users/users.service';
import { switchMap } from 'rxjs/operators';
import { User } from 'src/app/models/User';
import { Observable, Subscription } from 'rxjs';
import { SendingUserObjectService } from 'src/app/services/sending-user-object/sending-user-object.service';
import { SendingTabStatusService } from 'src/app/services/sending-tab-status/sending-tab-status.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit, OnDestroy {
  appUser!: User;
  requestedUser$!: Observable<User>;
  subscription: Subscription;
  isTheSameUser = true;
  

  constructor( private route: ActivatedRoute, private router: Router,
    private usersService: UsersService, private sendingUserObjectService: SendingUserObjectService,
    private sendingTabStatusService: SendingTabStatusService) { 
      this.subscription = this.sendingUserObjectService.userObjectSource$.subscribe(user => this.appUser = user);
  }

  ngOnInit(): void {
    this.requestedUser$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) => 
    this.usersService.get(params.get('userName')!)));

    this.requestedUser$.subscribe({
      next: (user) => {
        if (!(user.userName == this.appUser.userName)) {
          this.isTheSameUser = false;
          this.sendingTabStatusService.send('deactivate');
        }else {
          this.sendingTabStatusService.send('profile');
        }
      },
      error: (e) => { 
        this.router.navigateByUrl('not-found');
      },
      complete: () => {
      }
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
