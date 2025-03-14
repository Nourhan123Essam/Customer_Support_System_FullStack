import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment.development';
import { AppUserDTO } from '../../Interfaces/AppUserDTO';
import { LoginDTO } from '../../Interfaces/LoginDTO';


@Injectable({
  providedIn: 'root',
  
})
export class AuthServiceService {
  private apiUrl = environment.apiUrl + 'Authentication/'; // Use the environment's API URL

  constructor(private http: HttpClient) {}

  login(user: LoginDTO): Observable<any> {
    debugger;
    return this.http.post<LoginDTO>(`${this.apiUrl}login`, user);
  }

  register(user: AppUserDTO): Observable<any> {
    debugger;
    return this.http.post<AppUserDTO>(`${this.apiUrl}register`, user);
  }

  logout(): void {
    localStorage.removeItem('authToken');
  }

  isLoggedIn(): boolean {
    return !(!localStorage.getItem('authToken'));
  }

  getToken(): string | null {
    return localStorage.getItem('authToken');
  }

  getTokenClaims(token: string): any {
    try {
      const payload = token.split('.')[1];  // الحصول على الجزء الثاني من الـ token
      const decodedPayload = JSON.parse(atob(payload));  // فك تشفير الـ token إلى كائن JSON
      return decodedPayload;
    } catch (error) {
      console.error('Error decoding token:', error);
      return null;
    }
  }
  getUserNameFromToken(): string | null {
    const token = localStorage.getItem('authToken');
    if (token) {
      const claims = this.getTokenClaims(token);
      return claims?.['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'] || "null";  // التأكد من أن "Name" هو الـ Claim الصحيح
    }
    return null;
  }
  getUserId(): string | null {
    const token = localStorage.getItem('authToken');
    if (token) {
      const claims = this.getTokenClaims(token);
      return claims?.['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] || "null";  // التأكد من أن "Name" هو الـ Claim الصحيح
    }
    return null;
  }
}
