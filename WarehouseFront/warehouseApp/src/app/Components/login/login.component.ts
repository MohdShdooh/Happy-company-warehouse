import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { signInDto } from '../../Models/RequestDto.model';
import { isEmpty } from 'rxjs';
import { AuthenticateService } from '../../Services/authenticate.service';
import { AuthenticateResponse } from '../../Models/ResponseDto.model';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule,CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  signInObj: signInDto = new signInDto();
  responseData: AuthenticateResponse | undefined;
  errorMessage: string = '';
  constructor(private authService : AuthenticateService, private router: Router){ 
  }

  ngOnInit(): void {
    // Initialization logic if needed
    localStorage.removeItem("Name");
    localStorage.removeItem("Token");
    localStorage.removeItem("userId");

  }
  
  onSignIn() { 

    if(this.isValidForm(this.signInObj)){ 
      this.errorMessage = '';
      var response = this.authService.signIn(this.signInObj).subscribe(
        (response : AuthenticateResponse) => {
          this.responseData = response as AuthenticateResponse;

          if(this.responseData.Result && this.responseData.Token != null ){ 
            console.log(this.responseData);
            this.errorMessage = ''; 
            localStorage.setItem("userName", this.responseData.User.Name);
            localStorage.setItem("userId", this.responseData.User.Id.toString());
            localStorage.setItem("Token", this.responseData.Token); 
            this.router.navigate(['/dashboard']);
          }else { 
            this.errorMessage = "login faild . Try Agin "
          }
        }, 
        (error) => {
          this.responseData = error
          this.errorMessage = "login faild . Try Agin "

        }
      );
   }else { 
    this.errorMessage = 'Please Enter your Email and password .'
   }
  }

  isValidForm = (signInObj :signInDto)=>{ 
    if(signInObj.Email != null &&
       signInObj.Email != '' &&
       signInObj.Password != null &&
       signInObj.Password != ''  )
      { 
       return true ;
      }
      return false ;
  }

}
