import { HttpErrorResponse, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable, OnDestroy } from '@angular/core';
import { finalize, Subscription, tap } from 'rxjs';
import { SpinnerService } from 'src/app/components/spinner/spinner.service';
import { ErrorToastService } from 'src/app/services/error-toast/error-toast.service';
import { SendingSearchBoxStatusService } from '../sending-search-box-status/sending-search-box-status.service';

@Injectable({
  providedIn: 'root'
})
export class GlobalHttpInterceptorService implements HttpInterceptor, OnDestroy{
  private subscription: Subscription;
  private isSearchRunning:boolean = false;

  constructor(private errorToast: ErrorToastService, private spinnerService: SpinnerService, 
    private sendingSearchBoxStatusService: SendingSearchBoxStatusService) { 
    this.subscription = this.sendingSearchBoxStatusService.searchBoxStatus$.subscribe(
      status => this.isSearchRunning = status)
   }

  intercept(req: HttpRequest<any>, next: HttpHandler) {
    let msg: string;
    if (!this.isSearchRunning) {
      this.spinnerService.startRequest();
    }

    return next.handle(req)
      .pipe(
        tap({
            next: () => this.errorToast.toasts.pop(),
            error: (error) => {
              this.spinnerService.reset();
              if (error instanceof HttpErrorResponse) {
                switch (error.status) {
                  case 0: 
                      msg = "You are currently offline!"
                      break;
                  case 401:      //login
                      // this.router.navigateByUrl("/login");
                      break;
                  case 403:     //forbidden
                      // this.router.navigateByUrl("/login");
                      break;
                  default:
                      console.error(`${error.status}, body was: `, error.error);
                      msg = error.error;
                }
                this.errorToast.show(msg);
              }
            }
          }),
        finalize(() => {
          this.spinnerService.endRequest();
        })
      );
  }

  ngOnDestroy(): void {
    this.errorToast.toasts.pop();
    this.subscription.unsubscribe();
  }
}
