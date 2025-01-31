import { Component } from '@angular/core';
import { LoginComponent } from '../login/login.component';
import { RegisterComponent } from '../register/register.component';
import { CommonModule } from '@angular/common';
import { Router, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-welcome-page',
  imports: [LoginComponent, RegisterComponent, CommonModule, RouterOutlet],
  templateUrl: './welcome-page.component.html',
  styleUrl: './welcome-page.component.css',
  standalone: true,
})
export class WelcomePageComponent {
  isRegistering: boolean = false;

  constructor(private router: Router) {
    }

  wantRegister() {
    this.isRegistering = !this.isRegistering;
  }
  
}
