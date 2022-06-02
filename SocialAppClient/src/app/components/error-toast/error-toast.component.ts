import { animate, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { ErrorToastService } from '../../services/error-toast/error-toast.service';

@Component({
  selector: 'app-error-toast',
  templateUrl: './error-toast.component.html',
  styleUrls: ['./error-toast.component.css'],
  animations: [
    trigger('fade', [
      // state('in', style({ transform: 'translateY(0)' })),

      transition(':enter', [
        style({ transform: 'translateY(-100%)'}),
        animate(300)
      ]),
      // transition(':leave', [
      //   animate(2000, style({ transform: 'translateY(100%)' }))
      // ])

    ]),

  ]
})
export class ErrorToastComponent implements OnInit {

  constructor(public errorToastService: ErrorToastService) { }

  ngOnInit(): void {
  }

}
