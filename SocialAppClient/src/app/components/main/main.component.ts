import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { User } from 'src/app/models/User';
import { SendingTabStatusService } from 'src/app/services/sending-tab-status/sending-tab-status.service';
import { SendingUserObjectService } from 'src/app/services/sending-user-object/sending-user-object.service';
import { SpinnerService } from '../spinner/spinner.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit, OnDestroy {
  activeTab = "FeedComponent";
  user!: User;
  subscription: Subscription;
  tabStatusSubscription: Subscription | any;
  spinnerIsRunning = false;

  constructor(private sendingUserObjectService: SendingUserObjectService, 
    private spinnerService: SpinnerService, private cdRef:ChangeDetectorRef
    ,private sendingTabStatusService: SendingTabStatusService, private router: Router) { 
    this.subscription = this.sendingUserObjectService.userObjectSource$.subscribe(
      user => this.user = user
    );
  }

  ngOnInit(): void {
    this.spinnerService.spinner$.subscribe( status => {
      this.spinnerIsRunning = status == 'start';
      this.cdRef.detectChanges();
    });
    this.tabStatusSubscription = this.sendingTabStatusService.tabStatus$.subscribe(
      activeTab => { 
        this.activeTab = activeTab;
        this.cdRef.detectChanges();
      });
    this.router.navigateByUrl(this.router.url); 
  }

 setActiveTab(activeTab: string) {
   this.activeTab = activeTab;
 }

 
 logout(){
  localStorage.removeItem('userObject');
  localStorage.removeItem('loginStatus');
  this.tabStatusSubscription.unsubscribe();
  this.subscription.unsubscribe();
 }

 ngOnDestroy(): void {
  this.tabStatusSubscription.unsubscribe();
  this.subscription.unsubscribe();
 }
}
