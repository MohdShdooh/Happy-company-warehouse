import { Component } from '@angular/core';
import { LookupItem, UserRole, UserStatus } from '../../../Models/Entities.model';
import { LookUpService } from '../../../Services/look-up.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AddUserRequest } from '../../../Models/RequestDto.model';
import { UsersService } from '../../../Services/users.service';
import { BaseResponse } from '../../../Models/ResponseDto.model';

@Component({
  selector: 'app-add-user',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './add-user.component.html',
  styleUrl: './add-user.component.css'
})
export class AddUserComponent {

  userStatuses:  LookupItem[] = [];
  userRoles:  LookupItem[] = [];
  selectedUserStatus!: UserStatus;
  selectedUserRole!: UserRole;
  addUserRequest: AddUserRequest = new AddUserRequest(); // Initialize AddUserRequest model
  createUserResponse!: BaseResponse ;
  errorMessage = '';
  errorMessageColor = '';

  constructor(private lookupService: LookUpService, private router: Router, private usersService : UsersService) { }

  ngOnInit(): void {
    this.userStatuses = this.lookupService.getUserStatuses();
    this.userRoles = this.lookupService.getUserRoles();
  }

  onSubmit(): void {
    if(this.isValidForm()){ 
      this.addUserRequest.Role = parseInt(this.addUserRequest.Role.toString()) ;
      this.addUserRequest.status = parseInt(this.addUserRequest.status.toString()) ;
  
      console.log(this.addUserRequest); 

      this.usersService.CreateNewUser(this.addUserRequest).subscribe(
        (response)=> { 
          this.createUserResponse = response as BaseResponse
          console.log(response);
          if(response.result){ 
            this.errorMessage = response.message;
            this.errorMessageColor = 'green';
            this.router.navigate(['/dashboard/users']);
          }else{ 
            this.errorMessage = response.message ;
            this.errorMessageColor = 'red';
          }
        }, 
        (error) => { 
          console.log(error);
          if(error.error.errorCode == 101){ 
            this.errorMessage = error.error.message;
            this.errorMessageColor = 'red';

          }else{ 
              this.errorMessage = error.error.Message;
              this.errorMessageColor = 'red';
          }
        }
      );
    }else { 
      this.errorMessage = "Please Fill All Fields " ;
      this.errorMessageColor = 'red';
    }
  }
  goBack(){ 
    this.router.navigate(['/dashboard/users']);
  }
  isValidForm (){ 
    if(this.addUserRequest.Email != null &&
      this.addUserRequest.Email !='' &&
      this.addUserRequest.Name != null && 
      this.addUserRequest.Name != '' &&
      this.addUserRequest.Password != null &&  
      this.addUserRequest.Password != '' && 
      this.addUserRequest.Role != undefined && 
      this.addUserRequest.Role >= 0 && 
      this.addUserRequest.status != undefined && 
      this.addUserRequest.status >= 0     
    )
    { 
      return true ;
    }
    return false;
  }
}
