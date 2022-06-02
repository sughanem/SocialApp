import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ErrorToastComponent } from './components/error-toast/error-toast.component';
import { GlobalHttpInterceptorService } from './services/global-http-interceptor/global-http-interceptor.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DatePipe } from '@angular/common';
import { AddPostComponent } from './components/add-post/add-post.component';
import { PostContentComponent } from './components/add-post/post-content/post-content.component';
import { MainComponent } from './components/main/main.component';
import { FeedComponent } from './components/main/feed/feed.component';
import { ProfileComponent } from './components/main/profile/profile.component';
import { MessagingComponent } from './components/main/messaging/messaging.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { SearchBoxComponent } from './components/search-box/search-box.component';
import { PostComponent } from './components/post/post.component'
import { AuthInterceptorService } from './services/auth-interceptor/auth-interceptor.service';


@NgModule({
  declarations: [
    AppComponent, 
    LoginComponent,
    SignupComponent,
    ErrorToastComponent,
    AddPostComponent,
    PostContentComponent,
    MainComponent,
    FeedComponent,
    ProfileComponent,
    MessagingComponent,
    PageNotFoundComponent,
    SpinnerComponent,
    SearchBoxComponent,
    PostComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: GlobalHttpInterceptorService, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptorService, multi: true },
    DatePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }