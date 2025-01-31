import { Component } from '@angular/core';
import { UserServiceService } from '../../Services/UserService/user-service.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AddNoteDTO } from '../../Interfaces/AddNoteDTO';

@Component({
  selector: 'app-create-note',
  imports: [CommonModule,FormsModule, RouterModule],
  templateUrl: './create-note.component.html',
  styleUrl: './create-note.component.css'
})
export class CreateNoteComponent {
  ticketId!: number;
  noteContent: string = '';
  errorMessage: string = '';

  constructor(
    private userService: UserServiceService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    // Get ticketId from the route parameters
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.ticketId = +id;
      }
    });
  }

  addNote() {
    // debugger;
    if (!this.noteContent.trim()) {
      this.errorMessage = 'يرجى إدخال محتوى الملاحظة.';
      return;
    }

    const noteData:AddNoteDTO = {
      ticketId: this.ticketId,
      content: this.noteContent
    };

    this.userService.addNote(noteData).subscribe({
      next: () => {
        // Redirect to ticket notes page after adding a note
        this.router.navigate([`/ticket-notes/${this.ticketId}`]);
      },
      error: err => {
        console.error('Error adding note:', err);
        this.errorMessage = 'حدث خطأ أثناء إضافة الملاحظة.';
      }
    });
  }
}
