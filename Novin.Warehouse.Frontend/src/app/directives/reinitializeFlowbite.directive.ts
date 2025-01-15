import { Directive, AfterViewInit, OnChanges } from '@angular/core';
import { initFlowbite } from 'flowbite';

@Directive({
  selector: '[appReinitializeFlowbite]',
  standalone: true,
})
export class ReinitializeFlowbiteDirective implements AfterViewInit, OnChanges {
  ngAfterViewInit() {
    initFlowbite();
  }

  ngOnChanges() {
    initFlowbite();
  }
}