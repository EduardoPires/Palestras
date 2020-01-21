import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { APP_BASE_HREF } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule }   from '@angular/forms';

import { NgBrazil } from 'ng-brazil'
import { TextMask } from 'ng-brazil';
import { CustomFormsModule } from 'ng2-validation'

import { AppComponent } from './app.component';
import { MenuComponent } from './navegacao/menu/menu.component';
import { HomeComponent } from './navegacao/home/home.component';
import { FooterComponent } from './navegacao/footer/footer.component';
import { SobreComponent } from './institucional/sobre/sobre.component';
import { rootRouterConfig } from './app.routes';

import { LoginComponent } from './usuario/login/login.component';
import { CadastroComponent } from './usuario/cadastro/cadastro.component';

import { AuthService } from './usuario/services/usuario.service';
import { FilmesComponent } from './filmes/filmes.component';
import { FilmeService } from './filmes/services/filme.service';

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    HomeComponent,
    FooterComponent,
    SobreComponent,
    CadastroComponent,
    LoginComponent,
    FilmesComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    TextMask.TextMaskModule,
    HttpClientModule,
    NgBrazil,
    CustomFormsModule,
    [RouterModule.forRoot(rootRouterConfig, { useHash: false})]
  ],
  providers: [
    AuthService,
    FilmeService,
    { provide: APP_BASE_HREF, useValue: '/'  }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
