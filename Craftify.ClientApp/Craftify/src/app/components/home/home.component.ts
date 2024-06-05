import { Component, OnInit, inject } from '@angular/core';
import { HeaderComponent } from '../header/header.component';
import { FooterComponent } from '../footer/footer.component';
import { CommonModule } from '@angular/common';
import { map } from 'rxjs';
import { ProfileStore } from '../../shared/store/profile.store';
import { TokenService } from '../../services/token.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    HeaderComponent,
    FooterComponent,
    CommonModule
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{
  profileStore = inject(ProfileStore)
  tokenService = inject(TokenService)
  ngOnInit(): void {
    this.profileStore.loadAll();
  }
}
