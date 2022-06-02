import { Component, OnInit, ViewChild } from '@angular/core';
import { NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { merge, Observable, of, OperatorFunction, Subject} from 'rxjs';
import {catchError, debounceTime, distinctUntilChanged, tap, switchMap, filter} from 'rxjs/operators';
import { User } from 'src/app/models/User';
import { SendingSearchBoxStatusService } from 'src/app/services/sending-search-box-status/sending-search-box-status.service';
import { UsersService } from 'src/app/services/users/users.service';

@Component({
  selector: 'app-search-box',
  templateUrl: './search-box.component.html',
  styleUrls: ['./search-box.component.css']
})
export class SearchBoxComponent implements OnInit {

  @ViewChild('instance', {static: true}) instance: NgbTypeahead | any;
  focus$ = new Subject<string>();
  keydown$ = new Subject<any>();
  searching = false;

  constructor(private usersService: UsersService, 
    private sendingSearchBoxStatusService: SendingSearchBoxStatusService) {
  }

  ngOnInit(): void {
  }

  search: OperatorFunction<string, readonly User[] | string[]> = (text$: Observable<string>) => {
    const debouncedText$ = text$.pipe(debounceTime(500), distinctUntilChanged());
    const keydown$ = this.keydown$.pipe(filter((e) => this.instance.dismissPopup()));
    const inputFocus$ = this.focus$;

    return merge(debouncedText$, keydown$, inputFocus$).pipe(
      filter((query) => {
        let matchSpaces: any = query.match(/\s*/);
        return query !== matchSpaces[0];
      }),
      tap(() =>{
        this.searching = true;
        this.sendingSearchBoxStatusService.isRunnig();
      } ), 
      switchMap(query => 
        this.usersService.searchUsers(query).pipe( 
          tap(() => 
            this.sendingSearchBoxStatusService.finished()),
          catchError(() => {
          this.searching = false;
          this.sendingSearchBoxStatusService.finished();
          return of(['error']);
          })
        )
      ),
      tap(() => {
        this.searching = false;
        this.sendingSearchBoxStatusService.finished();
      })
    )
  };
  
  formatter = (x: {name: string}) => '';
}
