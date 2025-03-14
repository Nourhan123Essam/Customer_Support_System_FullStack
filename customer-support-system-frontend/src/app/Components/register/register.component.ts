import { Component } from '@angular/core';
import { AppUserDTO } from '../../Interfaces/AppUserDTO';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthServiceService } from '../../Services/AuthService/auth-service.service';
import { Router, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [CommonModule, FormsModule, RouterOutlet],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css',
  standalone: true,
})
export class RegisterComponent {
  registerData: AppUserDTO = { Id: 'String', Name: '', Email: '', Password: '', TelephoneNumber: 'string', Role: 'User' };
  confirmPassword: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthServiceService, private router: Router) {}

  onSubmit() {
    // debugger;
    if (this.registerData.Password !== this.confirmPassword) {
      this.errorMessage = 'Passwords do not match!';
      return;
    }

    this.authService.register(this.registerData).subscribe({
      next: (response) => {
        console.log('Registration successful!', response);
        this.router.navigate(['/login']);
      },
      error: (error) => {
        console.error('Registration failed', error);
        this.errorMessage = 'Registration failed. Please try again.';
      }
    });
  }
}
