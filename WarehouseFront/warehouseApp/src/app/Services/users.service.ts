import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UsersApi } from '../Models/ApisUrl.model';
import { AddUserRequest, changePasswordRequest, DeleteUserRequest, EditUserRequest } from '../Models/RequestDto.model';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

   private apiUrl = new UsersApi(); 

  constructor(private http: HttpClient) {}

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('Token');
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
  }
  
  GetAllUsers(): Observable<any> {
    return this.http.get<any>(this.apiUrl.GetAllUsers,{ headers: this.getAuthHeaders()});
  }

  CreateNewUser(user : AddUserRequest):Observable<any> { 
    return this.http.post<any>(this.apiUrl.CreateUser, user, { headers: this.getAuthHeaders()} );
  }
  
  getUserById(userId: number): Observable<any> {
    return this.http.get<any>(this.apiUrl.GetUserById, {
      headers: this.getAuthHeaders(),
      params: { id: userId }  
    });
  }

  updateUser(user: EditUserRequest): Observable<any> {
    return this.http.post<any>(this.apiUrl.UpdateUser, user, {headers: this.getAuthHeaders()});
  }

  changeUserPassword(request : changePasswordRequest): Observable<any> {
    return this.http.post<any>(this.apiUrl.ChangePasswordById, request, {
      headers: this.getAuthHeaders(),
    });
  }

  DeleteUserById(request : DeleteUserRequest): Observable<any> {
    return this.http.post<any>(this.apiUrl.DeleteUser, request, {
      headers: this.getAuthHeaders(),
    });
  }
}
