import { Component, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
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
  ngOnInit(): void {
    initFlowbite();
    this.authService.autoLogin();
  }
}
