import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MeetupService } from "../services/meetup.service";
import { Meetup } from "../models/meetup";

@Component({
  selector: 'app-lista-meetups',
  templateUrl: './lista-meetups.component.html'
})
export class ListaMeetupsComponent implements OnInit {
  public meetups: any
  errorMessage: string;

  constructor(private mtService: MeetupService) { }

  ngOnInit() {
    this.mtService.obterTodos()
      .subscribe(
      meetups => {
        this.meetups = meetups.data,
          console.log(meetups)
      },
      error => { 
        this.errorMessage = error, 
        console.log(error) 
      });
  }
}
