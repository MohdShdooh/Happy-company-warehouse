import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ItemsApi } from '../Models/ApisUrl.model';
import { CreateItemRequest, GetItemsRequest } from '../Models/RequestDto.model';

@Injectable({
  providedIn: 'root'
})
export class ItemsService {

  private apiUrl = new ItemsApi(); 

  constructor(private http: HttpClient) {}

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('Token');
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
  }
  
  GetItems(request: GetItemsRequest): Observable<any> {
    return this.http.get<any>(this.apiUrl.GetItems, {
      headers: this.getAuthHeaders(),
      params: { WarehouseId: request.WarehouseId , PageNumber : request.PageNumber , PageSize: request.PageSize }  
    });
  }

  
  CreateNewUser(request : CreateItemRequest):Observable<any> { 
    return this.http.post<any>(this.apiUrl.CreateItem, request, { headers: this.getAuthHeaders()} );
  }
  
  GetTopItems(): Observable<any> {
    return this.http.get<any>(this.apiUrl.GetTopItems, {
      headers: this.getAuthHeaders()
    });
  }
  
  GetLessItems(): Observable<any> {
    return this.http.get<any>(this.apiUrl.GetLessItems, {
      headers: this.getAuthHeaders()
    });
  }

}
