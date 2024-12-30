import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-public-shared',
  imports: [RouterOutlet, RouterLink],
  templateUrl: './public-shared.component.html',
  styleUrl: './public-shared.component.css'
})
export class PublicSharedComponent {

}
