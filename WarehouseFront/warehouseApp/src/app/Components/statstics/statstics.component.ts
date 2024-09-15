import { Component, OnInit } from '@angular/core';
import { ItemsService } from '../../Services/items.service';
import { ItemDto } from '../../Models/Entities.model';
import { GetItemsResponse } from '../../Models/ResponseDto.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-statstics',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './statstics.component.html',
  styleUrl: './statstics.component.css'
})
export class StatsticsComponent implements OnInit{
  topItems: ItemDto[] = [];
  lowItems: ItemDto[] = [];
  getTopItemsResponse :GetItemsResponse = new GetItemsResponse() ; 
  getLessItemsResponse : GetItemsResponse = new GetItemsResponse();

  constructor(private itemsService: ItemsService) {}

  ngOnInit(): void {
    this.fetchTopItems();
    this.fetchLessItems();
    console.log("statstics");
  }

  fetchTopItems(): void {
    this.itemsService.GetTopItems().subscribe(
      (response)=> { 
        console.log("topItems=> ", response);
        if(response.Result){ 
          console.log("topItems=> ", response.items);
        this.getTopItemsResponse.items = response.items
        console.log("GetTopItemsResponse=> ", this.getTopItemsResponse.items);
        }else { 
          this.getTopItemsResponse.items = [{Id:0 ,Name : "there are no items ",Quantity:0,Cost:0}];
        }
      },
      (error)=>{ 
        console.log(error); 
        alert(error.error.Message);
      }
    );
  }

  fetchLessItems():void { 
    this.itemsService.GetLessItems().subscribe(
      (response)=>{ 
        if(response.Result){ 

        this.getLessItemsResponse.items = response.items
        
        }else { 
          this.getTopItemsResponse.items = [{Id:0 ,Name : "there are no items ",Quantity:0,Cost:0}];
        }
      },
      (error)=>{ 
        console.log(error); 
      }
    );
  }
}
