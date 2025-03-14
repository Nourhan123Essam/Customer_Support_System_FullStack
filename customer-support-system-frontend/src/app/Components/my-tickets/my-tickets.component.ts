import { Component } from '@angular/core';
import { GetTicketDTO } from '../../Interfaces/GetTicketDTO';
import { UserServiceService } from '../../Services/UserService/user-service.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { TicketStatusPriorityPipe } from '../../Piplines/ticket-status-priority.pipe';

@Component({
  selector: 'app-my-tickets',
  imports: [CommonModule, TicketStatusPriorityPipe],
  templateUrl: './my-tickets.component.html',
  styleUrl: './my-tickets.component.css'
})
export class MyTicketsComponent {
  tickets: GetTicketDTO[] = [];
  loading = true;
  errorMessage = '';

  constructor(private userService: UserServiceService, private router:Router) {}

  ngOnInit(): void {
    this.fetchUserTickets();
  }

  fetchUserTickets(): void {
    this.userService.getUserTickets().subscribe({
      next: (data) => {
        this.tickets = data;
        this.loading = false;
      },
      error: (error) => {
        this.errorMessage = 'Failed to load tickets. Please try again later.';
        this.loading = false;
        console.error('Error fetching tickets:', error);
      }
    });
  }

  viewNotes(ticketId: number) {
    this.router.navigate(['/ticket-notes', ticketId]);
  }
  
  addFeedback(ticketId: number) {
    this.router.navigate(['/ticket-feedback', ticketId]);
  }
  
}
