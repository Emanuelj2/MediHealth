import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})

export class AuthService {
  private apiUrl = 'https://localhost:7214/api/auth'; 
  constructor(private http: HttpClient, private router: Router) { }

  register(data: {email: string, password: string, confirmPassword: string}) {
    return this.http.post(`${this.apiUrl}/register`, data);
  }

  login(data: {email: string, password: string}) {
    return this.http.post<{ token: string; email: string }>(`${this.apiUrl}/login`, data).pipe(
      tap(res =>{
        localStorage.setItem('token', res.token);
        localStorage.setItem('email', res.email);
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('email');
    this.router.navigate(['/login']);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

}
