import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ICommitteeHead } from './../models/committeeHead';



@Injectable({providedIn: 'root'})
export class HttpService {

  constructor(private http: HttpClient){

  }

  getAllCommitteeHeads(): Observable<Array<ICommitteeHead>> {

    return this.http.get<Array<ICommitteeHead>>('https://localhost:7169/api/examinationCommitteeHead')

  }

}
