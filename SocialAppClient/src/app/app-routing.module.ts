import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { AuthGuard } from './auth/auth.guard';
import { MainComponent } from './components/main/main.component';
import { FeedComponent } from './components/main/feed/feed.component';
import { ProfileComponent } from './components/main/profile/profile.component';
import { MessagingComponent } from './components/main/messaging/messaging.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';


const routes: Routes = [
  {
    path:'login', component: LoginComponent
  },
  {
    path: '', redirectTo: '/feed', pathMatch: 'full'
  },
  {
    path: '', component: MainComponent, canActivate: [AuthGuard],
    children: [
      {
        path: 'feed',
        component: FeedComponent
      },
      {
        path: 'messaging',
        component: MessagingComponent
      },
      { 
        path: 'not-found', component: PageNotFoundComponent 
      },
      {
        path: ':userName',
        component: ProfileComponent
      }
    ]
  },  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
