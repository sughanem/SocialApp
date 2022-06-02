import { Component, OnInit } from '@angular/core';
import { SendingTabStatusService } from 'src/app/services/sending-tab-status/sending-tab-status.service';

@Component({
  selector: 'app-messaging',
  templateUrl: './messaging.component.html',
  styleUrls: ['./messaging.component.css']
})
export class MessagingComponent implements OnInit {

  constructor(private sendingTabStatusService: SendingTabStatusService) { }

  ngOnInit(): void {
    this.sendingTabStatusService.send('messaging');
  }

}
