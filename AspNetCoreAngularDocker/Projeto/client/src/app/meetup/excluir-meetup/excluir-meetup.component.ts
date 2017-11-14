import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Subscription } from "rxjs/Subscription";
import { ActivatedRoute, Router } from "@angular/router";

import { Meetup } from "../models/meetup";
import { MeetupService } from "../services/meetup.service";

@Component({
  selector: 'app-excluir-meetup',
  templateUrl: './excluir-meetup.component.html'
})
export class ExcluirMeetupComponent implements OnInit {
  public errors: any[] = [];
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

  public excluirMeetup() {
    this.mtService.excluirMeetup(this.meetupId)
      .subscribe(
      evento => { this.onDeleteComplete(evento) },
      error => { this.onError(error) }
      );
  }

  public onDeleteComplete(evento: any) {
    this.router.navigate(['/meetups']);
  }

  onError(serviceReturn) {
    this.errors = Object.assign([], serviceReturn.error.errors);
  }
}
