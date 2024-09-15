import { Component } from '@angular/core';
import { ActivatedRoute, RouterOutlet } from '@angular/router';
import { ItemDto } from '../../Models/Entities.model';
import { ItemsService } from '../../Services/items.service';
import { GetItemsRequest } from '../../Models/RequestDto.model';

@Component({
  selector: 'app-warehouse',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './warehouse.component.html',
  styleUrl: './warehouse.component.css'
})
export class WarehouseComponent {
  
}
