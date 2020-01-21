import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';

import { Observable } from "rxjs";
import { catchError, map } from "rxjs/operators";

import { BaseService } from '../../utils/base.service';
import { Filme } from '../models/filme';

@Injectable()
export class FilmeService extends BaseService {
    
    constructor(private http: HttpClient) { super() }

    obterFilmes(): Observable<Filme[]> {
        return this.http
            .get<Filme[]>(this.UrlServiceV1 + "filmes/todos-os-filmes", super.ObterAuthHeaderJson())
            .pipe(
              catchError(super.serviceError));
    }
}