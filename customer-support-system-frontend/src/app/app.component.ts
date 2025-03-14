import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { HeaderComponent } from './Components/header/header.component';
import { AuthServiceService } from './Services/AuthService/auth-service.service';
import { WelcomePageComponent } from './Components/welcome-page/welcome-page.component';
import { HomeComponent } from './Components/home/home.component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent, WelcomePageComponent, HomeComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'customer-support-system-frontend';
  
  isLoggedIn: boolean = false;
  
    constructor(private authService: AuthServiceService, private router: Router) {
      this.isLoggedIn = this.authService.isLoggedIn(); // Check login status from AuthService
    }
}
