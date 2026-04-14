import { Component } from '@angular/core';
import { AuthService } from '../../services/auth';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router'; 

@Component({
  selector: 'app-register',
  imports: [FormsModule, CommonModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class RegisterComponent {
  email: string = '';
  password: string = ''
  confirmPassword: string = '';
  error: string = '';

  constructor(private auth : AuthService, private router : Router) {}

  onSubmit() {
    if(this.password !== this.confirmPassword) {
      this.error = 'Passwords do not match.';
      return;
    }

    this.auth.register({ email: this.email, password: this.password, confirmPassword: this.confirmPassword }).subscribe({
      next: () => {
        this.router.navigate(['/login']);
      },
      error: (err) => {
        this.error = 'Registration failed. Please try again.';
        console.error('Registration error:', err);
      }
    });
  }
  
}
