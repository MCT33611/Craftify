import { Component, inject } from '@angular/core';
import { ProfileStore } from '../../../shared/store/profile.store';
import { TokenService } from '../../../services/token.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.css'
})
export class LayoutComponent {
  profileStore = inject(ProfileStore)
  tokenService = inject(TokenService)
  ngOnInit(): void {
    this.profileStore.loadAll();
  }

  menuItems = [
    {
      title: "Dashboard",
      route: 'dashboard',
      iconSrc: 'assets/icons/dashboard.svg'
    },
    {
      title: "Edit Images & Details ",
      route: 'images',
      iconSrc: 'assets/icons/images.svg'
    },
    {
      title: "Bookings Management",
      route: 'requests',
      iconSrc: 'assets/icons/service.svg'
    },
    {
      title: "Settings",
      route: 'settings',
      iconSrc: 'assets/icons/settings.svg'
    }
  ];
}
