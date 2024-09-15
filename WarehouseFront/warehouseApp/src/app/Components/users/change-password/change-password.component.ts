import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule, RouterOutlet } from '@angular/router';
import { UsersService } from '../../../Services/users.service';
import { changePasswordRequest } from '../../../Models/RequestDto.model';

@Component({
  selector: 'app-change-password',
  standalone: true,
  imports: [RouterOutlet,RouterModule,CommonModule,FormsModule],
  templateUrl: './change-password.component.html',
  styleUrl: './change-password.component.css'
})
export class ChangePasswordComponent implements OnInit{

  password: string = '';
  confirmPassword: string = '';
  userId!: number;
  changePassReq : changePasswordRequest = new changePasswordRequest();

  constructor(
    private route: ActivatedRoute,
    private userService: UsersService, 
    private router : Router
  ) {}

  ngOnInit(): void {
    // Retrieve the userId from the route parameters
    this.userId = +this.route.snapshot.paramMap.get('id')!;
  }

  onSubmit() {
    if (this.password !== this.confirmPassword) {
      alert('Passwords do not match!');
      return;
    }
    this.changePassReq.Id = this.userId;
    this.changePassReq.Password = this.password; 

    this.userService.changeUserPassword(this.changePassReq).subscribe({
      next: (response) => {
        console.log('Password changed successfully', response);
        this.router.navigate(['/dashboard/users']);
      },
      error: (err) => {
        console.error('Error changing password', err);
      }
    });
  }
}
