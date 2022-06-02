import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { User } from 'src/app/models/User';
import { SendingTabStatusService } from 'src/app/services/sending-tab-status/sending-tab-status.service';
import { SendingUserObjectService } from 'src/app/services/sending-user-object/sending-user-object.service';

@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.css']
})
export class FeedComponent implements OnInit,OnDestroy {
  user: User | any;
  subscription: Subscription;

  constructor(private sendingUserObjectService: SendingUserObjectService, 
    private sendingTabStatusService: SendingTabStatusService) { 
    this.subscription = sendingUserObjectService.userObjectSource$.subscribe(
      user => this.user = user
    );
   }

  ngOnInit(): void {
    this.sendingTabStatusService.send('feed');
   
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
