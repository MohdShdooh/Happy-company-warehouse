import { Component } from '@angular/core';
import { ItemDto } from '../../../Models/Entities.model';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { ItemsService } from '../../../Services/items.service';
import { GetItemsRequest } from '../../../Models/RequestDto.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-list-items',
  standalone: true,
  imports: [FormsModule,CommonModule,RouterLink],
  templateUrl: './list-items.component.html',
  styleUrl: './list-items.component.css'
})
export class ListItemsComponent {

  warehouseId!: number;
  items: ItemDto[] = [];
  pageNumber: number = 1;
  pageSize: number = 10;
  totalFetchedItems: number = 0;
  loading: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private itemService: ItemsService
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.warehouseId = +params['id']; 
      this.loadItems();
    });
  }

  loadItems(): void {
    this.loading = true;
    const request: GetItemsRequest = {
      WarehouseId: this.warehouseId,
      PageNumber: this.pageNumber,
      PageSize: this.pageSize
    };
    
    this.itemService.GetItems(request).subscribe({
      next: (response) => {
        console.log(response);
        this.items = response.items;
        this.totalFetchedItems = response.items.length; // Adjust as necessary
        this.loading = false;
      },
      error: (err) => {
        console.error('Error fetching items', err);
        this.loading = false;
      }
    });
  }

  nextPage(): void {
    this.pageNumber++;
    this.loadItems();
  }

  previousPage(): void {
    if (this.pageNumber > 1) {
      this.pageNumber--;
      this.loadItems();
    }
  }
}
