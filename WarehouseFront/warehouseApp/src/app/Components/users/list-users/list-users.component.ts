import { Component } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { UsersService } from '../../../Services/users.service';
import { UserDto, userPreview, UserRole, UserStatus } from '../../../Models/Entities.model';
import { GetAllUsersResponse } from '../../../Models/ResponseDto.model';
import { CommonModule } from '@angular/common';
import { DeleteUserRequest } from '../../../Models/RequestDto.model';

@Component({
  selector: 'app-list-users',
  standalone: true,
  imports: [RouterLink,RouterOutlet,CommonModule],
  templateUrl: './list-users.component.html',
  styleUrl: './list-users.component.css'
})
export class ListUsersComponent {

  getUsersResponse! : GetAllUsersResponse | undefined ;
  users!: userPreview[];

  selectedUserId!: number;
  selectedUserName!: string;
  showDeleteDialog: boolean = false;
  deleteUserRequest:DeleteUserRequest = new DeleteUserRequest();

 constructor (private usersService : UsersService,private router:Router){ 

 }

 ngOnInit(): void {
  // list all users 
  this.loadUsers();
}

loadUsers(): void {
    this.usersService.GetAllUsers().subscribe({
      next: (data: GetAllUsersResponse) => {
        this.getUsersResponse = data ; 
        this.users = this.getUsersResponse.Users.map(user => ({
          ...user,
          UserStatus: UserStatus[user.UserStatus],
          Role: UserRole[user.Role]
        }));
      },
      error: (err) => console.error('Error fetching users', err)
    });
  }


  openDeleteModal(user: userPreview): void {
    this.selectedUserId = user.Id;
    this.selectedUserName = user.Name;

    const modalElement = document.getElementById('deleteUserModal');
    if (modalElement) {
      modalElement.classList.add('show');
      modalElement.style.display = 'block';
      document.body.classList.add('modal-open');
    }
  }
  
  onDeleteConfirmed() {
    console.log("userID = ", this.selectedUserId);
    this.deleteUserRequest.Id = this.selectedUserId;
    this.usersService.DeleteUserById(this.deleteUserRequest).subscribe(
      (response) => {
        console.log("Response => ",response);

        if(response.Result){ 
          this.users = this.users.filter(user => user.Id !== this.selectedUserId);
        }else{ 
          alert(response.message);
        }
      },
      (error)=>
      {
        console.log("shdooh Error => ", error);
        alert(error.error.message);
      });
      // Hide the modal after deletion
      const modalElement = document.getElementById('deleteUserModal');
      if (modalElement) {
        modalElement.classList.remove('show');
        modalElement.style.display = 'none';
        document.body.classList.remove('modal-open');
      }
  }

  onCancel(): void {
    const modalElement = document.getElementById('deleteUserModal');
    if (modalElement) {
      modalElement.classList.remove('show');
      modalElement.style.display = 'none';
      document.body.classList.remove('modal-open');
    }
  }

}
