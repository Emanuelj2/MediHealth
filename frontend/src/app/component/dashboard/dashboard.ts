import { Component } from '@angular/core';
import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class DashboardComponent {
  email = localStorage.getItem('email');

  constructor(private auth: AuthService) {}

  logout() {
    this.auth.logout();
  }
}
