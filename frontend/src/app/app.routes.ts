import { Routes } from '@angular/router';
import { LoginComponent } from './component/login/login';
import { RegisterComponent } from './component/register/register';
import { DashboardComponent } from './component/dashboard/dashboard';
import { authGuard } from './guards/auth-guard';
export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'dashboard', component: DashboardComponent, canActivate: [authGuard] },
    { path: '', redirectTo: 'login', pathMatch: 'full' }
];
