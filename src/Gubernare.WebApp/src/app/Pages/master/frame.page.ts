import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from '../../navbar/navbar.component';

import { AsyncPipe } from '@angular/common';
import {LayoutService} from '../../services/layout.service';

@Component({
  selector: 'app-frame-page',
  standalone: true,
  imports: [
    RouterOutlet,
    NavbarComponent,
    AsyncPipe
  ],
  template: `
    <app-navbar />
    <div [class]="(layoutService.sidebarCollapsed$ | async) ? 'content-collapsed' : 'content-expanded'">
      <router-outlet />
    </div>
  `,
  styles: `
    .content-expanded {
      margin-left: 250px;
      transition: margin-left 0.3s ease;
      min-height: 100vh;
    }

    .content-collapsed {
      margin-left: 70px;
      transition: margin-left 0.3s ease;
      min-height: 100vh;
    }

    @media (max-width: 960px) {
      .content-expanded, .content-collapsed {
        margin-left: 0;
        padding-top: 60px;
      }
    }
  `
})
export class FramePageComponent {
  layoutService = inject(LayoutService);
}
