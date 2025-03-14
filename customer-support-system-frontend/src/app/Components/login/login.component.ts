import { Component } from '@angular/core';
import { LoginDTO } from '../../Interfaces/LoginDTO';
import { AuthServiceService } from '../../Services/AuthService/auth-service.service';
import { Router, RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [CommonModule, FormsModule, RouterOutlet],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
  standalone: true,
})
export class LoginComponent {
  loginData: LoginDTO = { Email: '', Password: '' };
  errorMessage: string = '';

  constructor(private authService: AuthServiceService, private router: Router) {}

  onSubmit() {
    // debugger;
    this.authService.login(this.loginData).subscribe({
      next: (response) => {
        console.log('Login successful!', response);
        // alert(JSON.stringify(response, null, 2));
        localStorage.setItem('authToken', response.message);
        this.router.navigate(['/home']);
      },
      error: (error) => {
        console.error('Login failed', error);
        this.errorMessage = 'Login failed. Please check your credentials.';
      }
    });
  }
}
