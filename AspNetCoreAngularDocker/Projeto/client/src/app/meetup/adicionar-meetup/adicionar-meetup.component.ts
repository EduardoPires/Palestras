import { Component, OnInit, ViewEncapsulation, AfterViewInit, ElementRef, ViewChildren } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControlName } from "@angular/forms";
import { Router } from "@angular/router";

import { Observable } from "rxjs/Observable";
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/observable/fromEvent';
import 'rxjs/add/observable/merge';

import { GenericValidator } from "../../commom/generic.form.validator";
import { Meetup } from "../models/meetup"
import { MeetupService } from "../services/meetup.service";

@Component({
  selector: 'app-adicionar-meetup',
  templateUrl: './adicionar-meetup.component.html'
})
export class AdicionarMeetupComponent implements OnInit, AfterViewInit {
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  public errors: any[] = [];
  public meetupForm: FormGroup;
  public meetup: Meetup;
  public displayMessage: { [key: string]: string } = {};
  private validationMessages: { [key: string]: { [key: string]: string } };
  private genericValidator: GenericValidator;

  constructor(private fb: FormBuilder,
              private mtService: MeetupService,
              private router: Router) {

    this.validationMessages = {
      nome: {
        required: 'O Nome é requerido.',
        minlength: 'O Nome precisa ter no mínimo 3 caracteres',
        maxlength: 'O Nome precisa ter no máximo 100 caracteres'
      },
      descricao: {
        required: 'A Descrição é requerida.',
        minlength: 'A Descrição precisa ter no mínimo 10 caracteres',
        maxlength: 'A Descrição precisa ter no máximo 1000 caracteres'
      },
      local: {
        required: 'O Local é requerido.',
        minlength: 'O Local precisa ter no mínimo 10 caracteres',
        maxlength: 'O Local precisa ter no máximo 1000 caracteres'
      },
      data: {
        required: 'Informe a data'
      }
    };

    this.genericValidator = new GenericValidator(this.validationMessages);
    this.meetup = new Meetup();
  }

  ngOnInit() {
    this.meetupForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      descricao: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(1000)]],
      data: ['', Validators.required],
      local: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(150)]],
    });
  }

  ngAfterViewInit(): void {
    let controlBlurs: Observable<any>[] = this.formInputElements
      .map((formControl: ElementRef) => Observable.fromEvent(formControl.nativeElement, 'blur'));

    Observable.merge(...controlBlurs).subscribe(value => {
      this.displayMessage = this.genericValidator.processMessages(this.meetupForm);
    });
  }

  adicionarMeetup() {
    if (this.meetupForm.dirty && this.meetupForm.valid) {

      let p = Object.assign({}, this.meetup, this.meetupForm.value);
     
      this.mtService.registrarMeetup(p)
        .subscribe(
        result => { this.onSaveComplete() },
        error => {
          this.onError(error);
        });
    }    
  }

  onError(serviceReturn) {
    this.errors = Object.assign([], serviceReturn.error.errors);
  }

  onSaveComplete(): void {
    this.meetupForm.reset();
    this.errors = [];
    
    this.router.navigate(['/meetups']);
  }
}
