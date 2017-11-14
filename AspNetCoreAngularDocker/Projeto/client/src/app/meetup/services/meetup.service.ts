import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";

import { Observable } from "rxjs/Observable";
import { of } from 'rxjs/observable/of';
import { catchError, map, tap } from 'rxjs/operators';

import { Meetup, DataReturn } from "../models/meetup";

const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
const UrlServiceV1: string = "http://localhost:57331/api/";

@Injectable()
export class MeetupService {

    constructor(private http: HttpClient) { }

    obterTodos(): Observable<DataReturn<Meetup[]>> {
        return this.http
            .get<DataReturn<Meetup[]>>(UrlServiceV1 + "meetup")
            .pipe(
                tap(meetups => this.log(`obter meetups`))
            );
    }

    obterMeetup(id: number): Observable<DataReturn<Meetup>> {
        return this.http
            .get<DataReturn<Meetup>>(UrlServiceV1 + "meetup/" + id)
            .pipe(
                tap(meetups => this.log(`obter meetup id=`+ id))
            );
    }

    registrarMeetup(meetup: Meetup): Observable<DataReturn<Meetup>> {
        return this.http
            .post<DataReturn<Meetup>>(UrlServiceV1 + "meetup", meetup, httpOptions)
            .pipe(
                tap((meetup: DataReturn<Meetup>) => this.log(`adicionado meetup`))
            );
    }

    editarMeetup(meetup: Meetup): Observable<DataReturn<Meetup>> {
        return this.http
            .put<DataReturn<Meetup>>(UrlServiceV1 + "meetup/" + meetup.id, meetup, httpOptions)
            .pipe(
                tap((meetupRet: DataReturn<Meetup>) => this.log(`editado meetup id=`+meetup.id))
            );
    }

    excluirMeetup(id: number): Observable<DataReturn<Meetup>> {
        return this.http
            .delete<DataReturn<Meetup>>(UrlServiceV1 + "meetup/" + id, httpOptions)
            .pipe(
                tap((meetupRet: DataReturn<Meetup>) => this.log(`excluido meetup id=`+id))
            );
    }

    private log(message: string) {
        console.log(message);
    }
} 