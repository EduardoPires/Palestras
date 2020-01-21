import { Routes } from '@angular/router';
import { HomeComponent } from './navegacao/home/home.component';
import { SobreComponent } from './institucional/sobre/sobre.component';
import { CadastroComponent } from './usuario/cadastro/cadastro.component';
import { LoginComponent } from './usuario/login/login.component';
import { AuthService } from './usuario/services/usuario.service';
import { FilmesComponent } from './filmes/filmes.component';

export const rootRouterConfig: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full'},
    { path: 'home', component: HomeComponent},
    { path: 'sobre', component: SobreComponent },
    { path: 'cadastro', component: CadastroComponent },
    { path: 'entrar', component: LoginComponent },
    { path: 'filmes', canActivate: [AuthService], component: FilmesComponent }
];