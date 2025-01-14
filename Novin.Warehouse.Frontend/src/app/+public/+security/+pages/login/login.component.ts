import { Component } from '@angular/core';
import { LoginRequestDto } from '../../../../model/login-request.dto';
import { FormsModule } from '@angular/forms';
import { LoginService } from '../../../../service/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginRequest: LoginRequestDto = {
    username: '',
    password: ''
  }

  constructor(private loginService: LoginService, private router: Router) { }

  sendLoginRequest() {
    this.loginService.login(this.loginRequest).subscribe({
      next: (response) => {
        console.log('Logged in successfully: ', response);
        this.loginRequest = { username: '', password: '' };
        this.router.navigate(['/public/inventory']);
      },
      error: (err) => {
        console.log('Error logging', err);
      }
    })
  }

}
