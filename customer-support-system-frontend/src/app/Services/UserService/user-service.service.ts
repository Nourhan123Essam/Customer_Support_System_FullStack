import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GetTicketDTO } from '../../Interfaces/GetTicketDTO';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';
import {AddNoteDTO} from '../../Interfaces/AddNoteDTO';
import { AddRatingDTO } from '../../Interfaces/AddRatingDTO';

@Injectable({
  providedIn: 'root'
})
export class UserServiceService {

  private apiUrl = environment.apiUrl + 'User/'; 

  constructor(private http: HttpClient) {}

  getUserTickets(): Observable<GetTicketDTO[]> {
    const token = localStorage.getItem('authToken'); // Retrieve token from storage
        const auth = `Bearer ${token}`;
        // alert(auth);
        const httpOptions = {
          headers: new HttpHeaders({
            'Authorization': auth, // Add the token
          })
        };
    return this.http.get<GetTicketDTO[]>(`${this.apiUrl}my-tickets`, httpOptions);
  }

  addNote(noteData: AddNoteDTO): Observable<any> {
    // debugger;
    const token = localStorage.getItem('authToken'); // Retrieve token from storage
        const auth = `Bearer ${token}`;
        // alert(auth);
        const httpOptions = {
          headers: new HttpHeaders({
            'Authorization': auth, // Add the token
          })
        };
    return this.http.post(`${this.apiUrl}add-note`, noteData, httpOptions);
  }

  addRating(ratingData: AddRatingDTO): Observable<any> {
    // debugger;
    const token = localStorage.getItem('authToken'); // Retrieve token from storage
        const auth = `Bearer ${token}`;
        // alert(auth);
        const httpOptions = {
          headers: new HttpHeaders({
            'Authorization': auth, // Add the token
          })
        };
    return this.http.post(`${this.apiUrl}add-rating`, ratingData, httpOptions);
  }
}
