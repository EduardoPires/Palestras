import { Component, OnInit } from '@angular/core';
import { FilmeService } from './services/filme.service';
import { Filme } from './models/filme';

@Component({
  selector: 'app-filmes',
  templateUrl: './filmes.component.html'
})
export class FilmesComponent implements OnInit {

  filmes: Filme[];
  error: string;
  constructor(private filmeService: FilmeService) { }

  ngOnInit() {
    this.filmeService.obterFilmes()
      .subscribe(
        filmes => this.filmes = filmes,
        error => this.error = JSON.stringify(error))
  }
}
