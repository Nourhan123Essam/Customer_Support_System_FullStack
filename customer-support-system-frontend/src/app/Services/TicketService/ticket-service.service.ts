import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateTicketDTO } from '../../Interfaces/CreateTicketDTO';
import { environment } from '../../../environments/environment.development';
import { HttpHeaders } from '@angular/common/http';
import { NoteDTO } from '../../Interfaces/NoteDTO';


@Injectable({
  providedIn: 'root'
})
export class TicketServiceService {
  private apiUrl = environment.apiUrl + 'Ticket/'; 

  constructor(private http: HttpClient) {}

  // Function to create a ticket
  createTicket(ticketData: CreateTicketDTO): Observable<any> {
    // debugger;
    const formData = new FormData();
    formData.append('title', ticketData.Title);
    formData.append('description', ticketData.Description);
    formData.append('categoryId', ticketData.CategoryId.toString());
    formData.append('customerSupportUserId', ticketData.CustomerSupportUserId);
    formData.append('status', ticketData.Status);
    formData.append('priority', ticketData.Priority);

    // Append attachments
    ticketData.Attachments.forEach((file, index) => {
      formData.append(`attachments`, file);
    });

    const token = localStorage.getItem('authToken'); // Retrieve token from storage
    const auth = `Bearer ${token}`;
    // alert(auth);
    const httpOptions = {
      headers: new HttpHeaders({
        'Authorization': auth, // Add the token
      })
    };

    return this.http.post(`${this.apiUrl}create`, formData, httpOptions);

  }

  // Function to fetch ticket notes
  getTicketNotes(ticketId: number): Observable<NoteDTO[]> {
    // debugger;
    const token = localStorage.getItem('authToken'); // Retrieve token from storage
    const auth = `Bearer ${token}`;
    // alert(auth);
    const httpOptions = {
      headers: new HttpHeaders({
        'Authorization': auth, // Add the token
      })
    };
    return this.http.get<NoteDTO[]>(`${this.apiUrl}${ticketId}/notes`, httpOptions);
  }
}
