import { Component, OnInit } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { WareHousesService } from '../../../Services/ware-houses.service';
import {GetWareHousesResponse } from '../../../Models/ResponseDto.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-list-warehouses',
  standalone: true,
  imports: [RouterLink,FormsModule,CommonModule],
  templateUrl: './list-warehouses.component.html',
  styleUrl: './list-warehouses.component.css'
})
export class ListWarehousesComponent implements OnInit{

  getWareHousesResponse :GetWareHousesResponse = new GetWareHousesResponse();

  constructor (private warehousesService : WareHousesService,private router:Router){ }
 
  ngOnInit(): void {
   // list all users 
   this.loadUsers();
 }
 
 loadUsers(): void {
     this.warehousesService.GetWareHousesByUserId().subscribe({
       next: (data: GetWareHousesResponse) => {
         this.getWareHousesResponse = data as GetWareHousesResponse; 
         console.log(this.getWareHousesResponse);
       },
       error: (err) => console.error('Error fetching users', err)
     });
   }
 
}
