import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from '../../core/components/header/header.component';
import { FooterComponent } from '../../core/components/footer/footer.component';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [
    RouterOutlet,
    HeaderComponent,
    FooterComponent
  ],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.css'
})
export class LayoutComponent {

}
