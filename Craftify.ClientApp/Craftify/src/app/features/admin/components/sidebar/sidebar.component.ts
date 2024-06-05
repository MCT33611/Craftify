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
    {
      title: "Category Management",
      route: 'category/list',
      iconSrc: 'assets/icons/category.svg'
    },
    {
      title: "Settings",
      route: 'settings',
      iconSrc: 'assets/icons/settings.svg'
    }
  ];
}
