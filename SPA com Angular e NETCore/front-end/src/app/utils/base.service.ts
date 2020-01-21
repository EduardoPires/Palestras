import { HttpHeaders } from "@angular/common/http";

import { throwError } from "rxjs";

export abstract class BaseService {
  
     protected UrlServiceV1: string = "https://localhost:44327/api/";

    protected ObterHeaderJson(){
        return {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };
    }

    protected ObterAuthHeaderJson(){
        return {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${this.obterTokenUsuario()}`
            })
        };
    }

    public obterTokenUsuario(): string {
        return localStorage.getItem('demo.token');
    }

    public salvarTokenUsuario(token: string) {
        localStorage.setItem('demo.token', token);
    }

    public removerTokenUsuario() {
        localStorage.removeItem('demo.token');
    }
    
    protected extractData(response: any){
        return response.data || {};
    }

    protected serviceError(error: Response | any){
        console.error(error);
        return throwError(error);
    }
}