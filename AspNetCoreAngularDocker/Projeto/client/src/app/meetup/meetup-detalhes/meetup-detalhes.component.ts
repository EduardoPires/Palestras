import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Subscription } from "rxjs/Subscription";
import { ActivatedRoute, Router } from "@angular/router";

import { Meetup } from "../models/meetup";
import { MeetupService } from "../services/meetup.service";

@Component({
  selector: 'app-meetup-detalhes',
  templateUrl: './meetup-detalhes.component.html'
})
export class MeetupDetalhesComponent implements OnInit {
  public sub: Subscription;
  public meetupId: number = 0;
  public meetup: Meetup;

  constructor(private mtService: MeetupService,
    private router: Router,
    private route: ActivatedRoute) { }

  ngOnInit() {
        this.sub = this.route.params.subscribe(
      params => {
        this.meetupId = params['id'];
        this.obterMeetup(this.meetupId);
      });
  }

  obterMeetup(id: number) {
    this.mtService.obterMeetup(id)
      .subscribe(
        meetup =>{ this.meetup = meetup.data}
      );
  }
}
