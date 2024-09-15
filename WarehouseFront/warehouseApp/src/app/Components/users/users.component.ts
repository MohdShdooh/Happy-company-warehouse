import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Route, Router, RouterModule, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [RouterOutlet,RouterModule,CommonModule],
  templateUrl: './users.component.html',
  styleUrl: './users.component.css'
})
export class UsersComponent implements OnInit{

  constructor(private router:Router){}
  ngOnInit(): void {
    if(localStorage.getItem("Token") == null){ 
      this.router.navigate(['/login']);
    }
  }

  NavigateTo(to:string){ 
    console.log(to);
    this.router.navigate(['dashboard/users/',to]);
  }
}
