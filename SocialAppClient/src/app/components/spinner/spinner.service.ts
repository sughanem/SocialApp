import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SpinnerService {

  private spinner = new BehaviorSubject<string>('');
  spinner$ = this.spinner.asObservable();
  private isRunning = false;

  constructor() { }

  startRequest(){
    if (!this.isRunning) {
      this.spinner.next('start');
      this.isRunning = true;
    }
  }

  endRequest(){
    if (this.isRunning) {
      this.spinner.next('stop');
      this.isRunning = false;
    }
  }

  reset(){
    this.isRunning = false;
    this.spinner.next('stop');
  }
}
