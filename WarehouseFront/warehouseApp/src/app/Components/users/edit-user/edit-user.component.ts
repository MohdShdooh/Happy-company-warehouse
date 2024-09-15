import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LookUpService } from '../../../Services/look-up.service';
import { UsersService } from '../../../Services/users.service';
import { LookupItem } from '../../../Models/Entities.model';
import { EditUserRequest } from '../../../Models/RequestDto.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-edit-user',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './edit-user.component.html',
  styleUrl: './edit-user.component.css'
})
export class EditUserComponent {

  userStatuses: LookupItem[] = [];
  userRoles: LookupItem[] = [];
  editUserRequest!: EditUserRequest ;
  userId!: number;
  errorMessage = '' ;
  errorMessageColor = 'red';

  constructor(
    private route: ActivatedRoute,
    private lookupService: LookUpService,
    private userService: UsersService,
    private router: Router
  ) {}

  ngOnInit(): void {
    // Fetch the available statuses and roles
    this.userStatuses = this.lookupService.getUserStatuses();
    this.userRoles = this.lookupService.getUserRoles();
    console.log(this.userStatuses);
    // Get user ID from route parameters
    this.route.paramMap.subscribe(params => {
      this.userId = Number(params.get('id'));
      if (this.userId) {
        this.loadUserData(this.userId);
      }
    });
  }

  // Load the user data by user ID
  loadUserData(userId: number): void {
    this.userService.getUserById(userId).subscribe({
      next: (user) => {
        this.editUserRequest = {
          Id: user.Id,
          Name: user.Name,
          Email: user.Email,
          Role: user.Role,
          status: user.Status,
          Password:''
        };
      },
      error: (err) => console.error('Error fetching user data', err)
    });
  }

  // Update the user data
  updateUser(): void {
    if(this.isValidForm()){ 
      this.errorMessage ='';
      this.editUserRequest.Role = parseInt(this.editUserRequest.Role.toString()) ;
      this.editUserRequest.status = parseInt(this.editUserRequest.status.toString()) ;
  
      this.userService.updateUser(this.editUserRequest).subscribe({
        next: () => {
          console.log('User updated successfully');
          this.router.navigate(['/dashboard/users/list']); 
        },
        error: (err) => console.error('Error updating user', err)
      });
    }else{ 
      this.errorMessage = "please Fill all Fileds ."
    }
  }

  // Navigate back
  goBack(): void {
    this.router.navigate(['/dashboard/users/list']);
  }

  isValidForm (){ 
    if(this.editUserRequest.Email != null &&
      this.editUserRequest.Email !='' &&
      this.editUserRequest.Name != null && 
      this.editUserRequest.Name != '' &&
      this.editUserRequest.Role != undefined && 
      this.editUserRequest.Role >= 0 && 
      this.editUserRequest.status != undefined && 
      this.editUserRequest.status >= 0     
    )
    { 
      return true ;
    }
    return false;
  }
  
}
