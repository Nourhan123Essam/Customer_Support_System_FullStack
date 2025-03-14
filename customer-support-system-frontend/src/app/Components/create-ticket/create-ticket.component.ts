import { Component } from '@angular/core';
import { TicketServiceService } from '../../Services/TicketService/ticket-service.service';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CreateTicketDTO } from '../../Interfaces/CreateTicketDTO';

@Component({
  selector: 'app-create-ticket',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './create-ticket.component.html',
  styleUrl: './create-ticket.component.css'
})
export class CreateTicketComponent {
  ticket: CreateTicketDTO = {
    Title: '',
    Description: '',
    CategoryId: 1, // Default category
    CustomerSupportUserId: 'jklds', // Set dynamically
    Status: '0', // Default status
    Priority: '0', // Default priority
    Attachments: [] as File[]
  };

  constructor(private ticketService: TicketServiceService, private router: Router) {}

  onFileSelected(event: any) {
    this.ticket.Attachments = Array.from(event.target.files);
  }

  submitTicket() {
     debugger;
    this.ticketService.createTicket(this.ticket).subscribe({
      next: (response) => {
        alert('Ticket created successfully!');
        this.router.navigate(['/my-tickets']); // Redirect to home
      },
      error: (error) => {
        console.error('Error creating ticket:', error);
        alert('Failed to create ticket.');
      }
    });
  }
}
