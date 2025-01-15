import { Component, OnDestroy, OnInit } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { LoginResponseDto } from '../../model/login-response.dto';
import { AuthService } from '../../service/auth.service';
import { User } from '../../model/user.model';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs'

@Component({
  selector: 'app-public-shared',
  imports: [RouterOutlet, RouterLink, CommonModule],
  templateUrl: './public-shared.component.html',
  styleUrl: './public-shared.component.css'
})
export class PublicSharedComponent implements OnInit, OnDestroy {
  isLoggedIn: boolean = false;
  private userSubject: Subscription | null = null;

  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.userSubject = this.authService.user.subscribe((user: User|null) => {
      this.isLoggedIn = user ? true : false;
    })
  }

  ngOnDestroy() {
    if (this.userSubject != null)
    {
      this.userSubject.unsubscribe();
    }
  }

}
