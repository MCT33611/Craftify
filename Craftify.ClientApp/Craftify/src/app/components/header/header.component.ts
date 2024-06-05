import { Component, OnInit, inject } from '@angular/core';
import { MaterialModule } from '../../shared/material/material.module';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../features/authentication/services/auth.service';
import { ProfileStore } from '../../shared/store/profile.store';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    MaterialModule,
    RouterLink,
    CommonModule
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit {

  authService = inject(AuthService);
  router = inject(Router);
  profileStore = inject(ProfileStore)
  
  ngOnInit(): void {
    this.profileStore.loadAll();
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/auth/sign-in']);
  }
}
