import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SendingSearchBoxStatusService {

  private searchBoxStatus= new BehaviorSubject<boolean>(false);
  searchBoxStatus$ = this.searchBoxStatus.asObservable();

  constructor() { }

  isRunnig (){
    this.searchBoxStatus.next(true);
  }

  finished (){
    this.searchBoxStatus.next(false);
  }
}
