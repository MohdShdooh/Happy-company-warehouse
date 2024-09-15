import { Injectable } from '@angular/core';
import { Countries, LookupItem, UserRole, UserStatus } from '../Models/Entities.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { WareHouseApi } from '../Models/ApisUrl.model';

@Injectable({
  providedIn: 'root'
})
export class LookUpService {

  private apiUrl = new WareHouseApi(); 

  constructor(private http: HttpClient) {}
  
  getUserStatuses() : LookupItem[]{
    return Object.keys(UserStatus).filter(key => isNaN(Number(key))).map(key => ({
      value: UserStatus[key as keyof typeof UserStatus],
      label: key
    }));
  }

  getUserRoles() : LookupItem[] {
    return Object.keys(UserRole).filter(key => isNaN(Number(key))).map(key => ({
      value: UserRole[key as keyof typeof UserRole],
      label: key
    }));
  }

  getCountries() : any {
    return this.http.get<any>(this.apiUrl.GetAllCountries);
  }
}
