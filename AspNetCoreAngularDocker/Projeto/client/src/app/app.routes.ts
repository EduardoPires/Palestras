import { Routes } from '@angular/router';
import { HomeComponent } from "./home/home.component";
import { ListaMeetupsComponent } from "./meetup/lista-meetups/lista-meetups.component";
import { AdicionarMeetupComponent } from "./meetup/adicionar-meetup/adicionar-meetup.component";
import { EditarMeetupComponent } from "./meetup/editar-meetup/editar-meetup.component";
import { MeetupDetalhesComponent } from "./meetup/meetup-detalhes/meetup-detalhes.component";
import { ExcluirMeetupComponent } from "./meetup/excluir-meetup/excluir-meetup.component";

export const rootRouterConfig: Routes = [
    { path: '', component: HomeComponent },
    { path: 'home', component: HomeComponent },
    { path: 'meetups', component: ListaMeetupsComponent },
    { path: 'novo-meetup', component: AdicionarMeetupComponent },
    { path: 'meetup-editar/:id', component: EditarMeetupComponent },
    { path: 'meetup-detalhes/:id', component: MeetupDetalhesComponent },
    { path: 'meetup-excluir/:id', component: ExcluirMeetupComponent }
];
