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
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html'
})
export class CadastroComponent implements OnInit, AfterViewInit {
  
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  cadastroForm: FormGroup;
  formResult: string = '';
  usuario: Usuario;
  MASKS = utilsBr.MASKS;
  erro: string = '';

  validationMessages: ValidationMessages;
  genericValidator: GenericValidator;
  displayMessage: DisplayMessage = {};

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
      },
      senhaConfirmacao: {
        required: 'Informe a senha novamente',
        rangeLength: 'A senha deve possuir entre 6 e 15 caracteres',
        equalTo: 'As senhas não conferem'
      }
    };

    this.genericValidator = new GenericValidator(this.validationMessages);
   }

  ngOnInit() {
    let senha = new FormControl('', [Validators.required, CustomValidators.rangeLength([6,15])]);
    let senhaConfirm = new FormControl('', [Validators.required, CustomValidators.rangeLength([6,15]), CustomValidators.equalTo(senha)]);

    this.cadastroForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      senha: senha,
      senhaConfirmacao: senhaConfirm
    });
  }

  ngAfterViewInit(): void {
    let controlBlurs: Observable<any>[] = this.formInputElements
    .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

    merge(...controlBlurs).subscribe(() => {
      this.displayMessage = this.genericValidator.processarMensagens(this.cadastroForm);
    });
  }

  adicionarUsuario() {
    if (this.cadastroForm.dirty && this.cadastroForm.valid) {
      this.usuario = Object.assign({}, this.usuario, this.cadastroForm.value);
      this.formResult = JSON.stringify(this.cadastroForm.value);

      this.usuarioService.registrarUsuario(this.usuario)  
        .subscribe(
          result => this.sucesso(result),
          fail => this.falha(fail));

    }
    else {
      this.formResult = "Não submeteu!!!"
    }    
  }

  sucesso(response: any): void {
    this.cadastroForm.reset();
    this.erro = '';

    this.usuarioService.salvarTokenUsuario(response);

    this.router.navigate(['/home']);
  }

  falha(fail: any) {
    this.erro = JSON.stringify(fail.error);
  }
}
