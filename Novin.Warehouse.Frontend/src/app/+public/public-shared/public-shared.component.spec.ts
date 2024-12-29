import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicSharedComponent } from './public-shared.component';

describe('PublicSharedComponent', () => {
  let component: PublicSharedComponent;
  let fixture: ComponentFixture<PublicSharedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PublicSharedComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PublicSharedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
