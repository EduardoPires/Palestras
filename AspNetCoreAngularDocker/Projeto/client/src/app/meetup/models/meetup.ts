export class Meetup {
    id: number;
    nome: string;
    descricao: string;
    data: Date;
    local: string;
}

export class DataReturn<T>{
    status: string;
    data: T;
}