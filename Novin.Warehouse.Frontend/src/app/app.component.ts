import { Component, OnInit, inject } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { PublicSharedComponent } from "./+public/public-shared/public-shared.component";
import { initFlowbite } from 'flowbite';
import { AuthService } from './service/auth.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  authService: AuthService = inject(AuthService);
  constructor(private router: Router) { }
  ngOnInit(): void {
    initFlowbite();
    this.authService.autoLogin();

    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        initFlowbite();
      }
    })
  }
}
