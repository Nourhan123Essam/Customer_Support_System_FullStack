import { Component } from '@angular/core';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import { AuthServiceService } from '../../Services/AuthService/auth-service.service';
import { CommonModule } from '@angular/common';
import { TicketServiceService } from '../../Services/TicketService/ticket-service.service';
@Component({
  selector: 'app-header',
  imports: [
    CommonModule, RouterOutlet, RouterModule
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
  standalone: true,
})
export class HeaderComponent {
  isLoggedIn: boolean = false; // Replace with actual authentication logic

  constructor(private router: Router, private authService: AuthServiceService) {
    this.isLoggedIn = authService.isLoggedIn();
  }

  logout() {
    localStorage.removeItem('authToken');
    this.isLoggedIn = false;
    this.router.navigate(['/welcome']);
  }
}
