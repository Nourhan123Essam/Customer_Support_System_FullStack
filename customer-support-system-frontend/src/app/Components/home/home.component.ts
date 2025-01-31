import { Component, OnInit } from '@angular/core';
import { AuthServiceService } from '../../Services/AuthService/auth-service.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common'; // If you're using ngIf, etc.
import { RouterModule } from '@angular/router'; // If you're using routing

@Component({
  selector: 'app-home',
  imports: [CommonModule, RouterModule], // Import needed modules here
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  standalone: true,
})
export class HomeComponent implements OnInit {
  userName: any|null = '';

  constructor(private authService: AuthServiceService, private router: Router) {
    
  }
  ngOnInit(): void {
    // debugger;
    this.userName= this.authService.getUserNameFromToken();
  }
}
