import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { TicketServiceService } from '../../Services/TicketService/ticket-service.service';
import { AuthServiceService } from '../../Services/AuthService/auth-service.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NoteDTO } from '../../Interfaces/NoteDTO';

@Component({
  selector: 'app-ticket-notes',
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './ticket-notes.component.html',
  styleUrl: './ticket-notes.component.css'
})
export class TicketNotesComponent implements OnInit {
  ticketId!: number;
  notes: NoteDTO[] = [];
  userId: string|null = ''; // Logged-in user ID

  constructor(
    private route: ActivatedRoute,
    private ticketService: TicketServiceService,
    private authService: AuthServiceService,
  ) {}
  ngOnInit(): void {
    // debugger;
    // Get ticketId from the URL
      this.route.paramMap.subscribe(params => {
        const id = params.get('id');
        if (id) {
          this.ticketId = +id;
          this.loadNotes();
        }
      });

      // Get logged-in user ID
      this.userId = this.authService.getUserId();
  }

 

  loadNotes() {
    // debugger;
    this.ticketService.getTicketNotes(this.ticketId).subscribe({
      next: data => {
        this.notes = data;
      },
      error: err => {
        console.error('Error fetching notes:', err);
      }
    });
  }

}
