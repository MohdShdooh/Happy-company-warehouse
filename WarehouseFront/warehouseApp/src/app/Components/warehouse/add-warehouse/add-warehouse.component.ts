import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { WareHousesService } from '../../../Services/ware-houses.service';
import { LookUpService } from '../../../Services/look-up.service';
import { AddWareHouseRequest } from '../../../Models/RequestDto.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-warehouse',
  standalone: true,
  imports: [RouterLink,FormsModule,CommonModule],
  templateUrl: './add-warehouse.component.html',
  styleUrl: './add-warehouse.component.css'
})
export class AddWarehouseComponent {

 addWarehouseRequest :AddWareHouseRequest = new AddWareHouseRequest();
  countries: string[] = [];

  constructor(
    private warehouseService: WareHousesService,
    private countryService: LookUpService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadCountries();
  }

  loadCountries(): void {
    this.countryService.getCountries().subscribe({
      next: (data : any) => {
        this.countries = data as string[]; 
      },
      error: (err : any) => console.error('Error fetching countries', err)
    });
  }

  onSubmit(): void {
    this.warehouseService.CreateWareHouse(this.addWarehouseRequest).subscribe(
      (response)=> { 
       console.log(response); 
       if(response.Result){ 
        this.router.navigate(['/dashboard/warehouses']);
       }
      },
      (error)=>{ 
          console.log(error);
          alert(error.error.message);
        }
    );
  }

  goBack(){ 
    this.router.navigate(['/dashboard/warehouses']);
  }
}
