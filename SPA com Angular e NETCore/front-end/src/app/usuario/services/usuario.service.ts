import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { BaseService } from '../../utils/base.service';
import { HttpClient } from '@angular/common/http';
import { Usuario } from '../models/usuario';

import { Observable } from "rxjs";
import { catchError, map } from "rxjs/operators";

@Injectable()
export class AuthService extends BaseService implements CanActivate {
    public token: string;

    constructor(private router: Router, private http: HttpClient) { super() }

    canActivate(): boolean {
        this.token = super.obterTokenUsuario();

        if (!this.token) {
            this.router.navigate(['/entrar'])
            return false;
        }       

        return true;
    }

    registrarUsuario(usuario: Usuario) : Observable<Usuario> {

        let response = this.http
            .post(this.UrlServiceV1 + "auth/nova-conta", usuario, super.ObterHeaderJson())
            .pipe(
                map(super.extractData),
                catchError(super.serviceError));

        return response;
    }
    
    login(usuario: Usuario) : Observable<Usuario> {

        let response = this.http
            .post(this.UrlServiceV1 + "auth/entrar", usuario, super.ObterHeaderJson())
            .pipe(
                map(super.extractData),
                catchError(super.serviceError));

        return response;
    }
}