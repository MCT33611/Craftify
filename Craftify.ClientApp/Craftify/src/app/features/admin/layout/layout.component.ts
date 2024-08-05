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
      title: "Profiles",
      route: 'profiles',
      iconSrc: 'assets/icons/profiles.svg'
    },
    {
      title: "Plan Management",
      route: 'plan/list',
      iconSrc: 'assets/icons/subscriptions-plan.svg'
    },
    {
      title: "Bookings Management",
      route: 'request/list',
      iconSrc: 'assets/icons/service.svg'
    },
    {
      title: "Settings",
      route: 'settings',
      iconSrc: 'assets/icons/settings.svg'
    }
  ];
}
