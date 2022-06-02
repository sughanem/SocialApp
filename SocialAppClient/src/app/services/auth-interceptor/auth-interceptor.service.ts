import { Injectable, OnDestroy } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, Subscription } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from 'src/app/models/User';
import { SendingUserObjectService } from '../sending-user-object/sending-user-object.service';
import { AuthService } from 'src/app/auth/auth.service';


@Injectable()
export class AuthInterceptorService implements HttpInterceptor, OnDestroy{

  private user: User | any;
  private subscription: Subscription;

  constructor(private sendingUserObjectService: SendingUserObjectService, 
    private authService: AuthService) { 
    this.subscription = this.sendingUserObjectService.userObjectSource$.subscribe(
      user => this.user = user
    )
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const isLoggedIn = this.authService.getLoginStatus();
    const isApiUrl = request.url.startsWith(environment.API_URL);
    if (isLoggedIn && isApiUrl) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${this.user.token}`
        }
      });
    }
  return next.handle(request);
}

  ngOnDestroy(): void {
  this.subscription.unsubscribe();
  }
}