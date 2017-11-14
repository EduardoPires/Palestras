import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from "@angular/router";
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from "@angular/forms";

// 3s
import { CollapseModule } from 'ngx-bootstrap/collapse';

// components
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { MenuSuperiorComponent } from "./navigation/menu-superior/menu-superior.component";
import { FooterComponent } from "./navigation/footer/footer.component";
import { ListaMeetupsComponent } from "./meetup/lista-meetups/lista-meetups.component";
import { AdicionarMeetupComponent } from './meetup/adicionar-meetup/adicionar-meetup.component';
import { EditarMeetupComponent } from './meetup/editar-meetup/editar-meetup.component';
import { MeetupDetalhesComponent } from './meetup/meetup-detalhes/meetup-detalhes.component';
import { ExcluirMeetupComponent } from './meetup/excluir-meetup/excluir-meetup.component';

// services
import { MeetupService } from "./meetup/services/meetup.service";

// config
import { rootRouterConfig } from "./app.routes";

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    MenuSuperiorComponent,
    FooterComponent,
    ListaMeetupsComponent,
    AdicionarMeetupComponent,
    EditarMeetupComponent,
    MeetupDetalhesComponent,
    ExcluirMeetupComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,
    CollapseModule.forRoot(),
    RouterModule.forRoot(rootRouterConfig, { useHash: false })
  ],
  providers: [
    MeetupService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
