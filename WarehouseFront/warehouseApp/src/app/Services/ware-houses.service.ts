import { Injectable } from '@angular/core';
import { WareHouseApi } from '../Models/ApisUrl.model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AddWareHouseRequest } from '../Models/RequestDto.model';

@Injectable({
  providedIn: 'root'
})
export class WareHousesService {


  private apiUrl = new WareHouseApi(); 

  constructor(private http: HttpClient) {}

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('Token');
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
  }
  

  GetWareHousesByUserId(): Observable<any> {
    return this.http.get<any>(this.apiUrl.GetWareHousesByUserID,{ headers: this.getAuthHeaders()});
  }

  CreateWareHouse(warehouse : AddWareHouseRequest):Observable<any> { 
    return this.http.post<any>(this.apiUrl.CreateWareHouse,warehouse, { headers: this.getAuthHeaders()} );
  }

}
