import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, NgForm, ReactiveFormsModule, Validators } from '@angular/forms';
import { ItemsService } from '../../../Services/items.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateItemRequest } from '../../../Models/RequestDto.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-item',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './add-item.component.html',
  styleUrl: './add-item.component.css'
})
export class AddItemComponent {

  warehouseId!: number;
  addItemRequest: CreateItemRequest = new CreateItemRequest();

  constructor(
    private itemService: ItemsService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    // Get the warehouse ID from the route parameters
    this.route.params.subscribe(params => {
      this.warehouseId = +params['id'];
    });
  }

  onSubmit(form: NgForm): void {
    if (form.valid) {
      this.addItemRequest = form.value as CreateItemRequest;
      this.addItemRequest.WarehouseId = this.warehouseId;
      console.log("this.addItemRequest => ",this.addItemRequest);
      this.itemService.CreateNewUser(this.addItemRequest).subscribe(
        (response)=> { 
          console.log(response);
         if(response.Result){ 
          this.router.navigate(['/dashboard/warehouses/items/list', this.warehouseId]);
         }else { 
          alert(response.Message);
         }
        },
        (error) => { 
          alert(error.error.message);
        }

      );
    }
  }
  goBack(){ 
    this.router.navigate(['/dashboard/warehouses/items/list', this.warehouseId]);
  }
}
