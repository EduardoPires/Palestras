import { Component, OnInit, AfterViewInit, ElementRef, ViewChildren } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, FormControlName } from '@angular/forms';

import { Usuario } from './../models/usuario';

import { utilsBr } from 'js-brasil';
import { CustomValidators } from 'ng2-validation';
import { ValidationMessages, GenericValidator, DisplayMessage } from '../../utils/generic-form-validation';
import { Observable, fromEvent, merge } from 'rxjs';
import { AuthService } from '../services/usuario.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit, AfterViewInit {
  
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  loginForm: FormGroup;
  formResult: string = '';
  usuario: Usuario;
  MASKS = utilsBr.MASKS;

  validationMessages: ValidationMessages;
  genericValidator: GenericValidator;
  displayMessage: DisplayMessage = {};
  erro: string = '';

  constructor(
    private fb: FormBuilder, 
    private usuarioService: AuthService, 
    private router: Router) {
    
    this.validationMessages = {
     email: {
        required: 'Informe o e-mail',
        email: 'Email inválido'
      },
      senha: {
        required: 'Informe a senha',
        rangeLength: 'A senha deve possuir entre 6 e 15 caracteres'
      }
    };

    this.genericValidator = new GenericValidator(this.validationMessages);
   }

  ngOnInit() {
    
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      senha: ['', [Validators.required, CustomValidators.rangeLength([6,15])]]    
    });
  }

  ngAfterViewInit(): void {
    let controlBlurs: Observable<any>[] = this.formInputElements
    .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

    merge(...controlBlurs).subscribe(() => {
      this.displayMessage = this.genericValidator.processarMensagens(this.loginForm);
    });
  }

  loginUsuario() {
    if (this.loginForm.dirty && this.loginForm.valid) {
      this.usuario = Object.assign({}, this.usuario, this.loginForm.value);
      this.formResult = JSON.stringify(this.loginForm.value);

      this.usuarioService.login(this.usuario)  
        .subscribe(
          result => this.sucesso(result),
          fail => this.falha(fail));
    }
    else {
      this.formResult = "Não submeteu!!!"
    }
  }

  sucesso(response: any): void {
    this.loginForm.reset();
    this.erro = '';

    this.usuarioService.salvarTokenUsuario(response);

    this.router.navigate(['/home']);
  }

  falha(fail: any) {
    this.erro = JSON.stringify(fail.error);
  }
}
