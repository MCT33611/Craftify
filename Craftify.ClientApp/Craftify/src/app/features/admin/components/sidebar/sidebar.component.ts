import { Component } from '@angular/core';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
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
    // {
    //   title: "Category Management",
    //   route: 'category/list',
    //   iconSrc: 'assets/icons/category.svg'
    // },
    {
      title: "Plan Management",
      route: 'plan/list',
      iconSrc: 'assets/icons/subscriptions-plan.svg'
    },
    {
      title: "Service Management",
      route: 'service/list',
      iconSrc: 'assets/icons/service.svg'
    },
    {
      title: "Bookings Management",
      route: 'bookings/list',
      iconSrc: 'assets/icons/service.svg'
    },
    {
      title: "Settings",
      route: 'settings',
      iconSrc: 'assets/icons/settings.svg'
    }
  ];
}
