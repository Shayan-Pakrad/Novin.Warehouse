import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { PublicSharedComponent } from "./+public/public-shared/public-shared.component";
import { initFlowbite } from 'flowbite';

@Component({
  selector: 'app-root',
  imports: [PublicSharedComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  ngOnInit(): void {
    initFlowbite();
  }
}
