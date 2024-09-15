import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StatsticsComponent } from './statstics.component';

describe('StatsticsComponent', () => {
  let component: StatsticsComponent;
  let fixture: ComponentFixture<StatsticsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StatsticsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StatsticsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
