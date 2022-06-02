import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SendingTabStatusService {

  private tabStatus = new BehaviorSubject<string>('');
  tabStatus$ = this.tabStatus.asObservable();

  constructor() { }

  send(tabStatus: string){
    this.tabStatus.next(tabStatus);
  }
}
