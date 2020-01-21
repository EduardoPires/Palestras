import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/usuario/services/usuario.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html'
})
export class MenuComponent {
  public token;

  constructor(private router: Router, private usuarioService: AuthService) {  }

  usuarioLogado(): boolean {
    this.token = this.usuarioService.obterTokenUsuario();    
    return this.token !== null;
  }

  logout() {
    this.usuarioService.removerTokenUsuario();
    this.router.navigate(['/home']);
  }
}
