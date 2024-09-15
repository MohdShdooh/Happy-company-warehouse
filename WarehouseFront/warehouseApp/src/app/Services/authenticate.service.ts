import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthenticateApi } from '../Models/ApisUrl.model';
import { signInDto } from '../Models/RequestDto.model';

@Injectable({
  providedIn: 'root'
})
export class AuthenticateService {

  private apiUrl = new AuthenticateApi(); 

  constructor(private http: HttpClient) {}

  signIn(signInObj:signInDto): Observable<any> {
    return this.http.post<any>(this.apiUrl.signInApi, signInObj);
  }
}
