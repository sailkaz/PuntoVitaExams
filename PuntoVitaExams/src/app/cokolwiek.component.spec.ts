import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CokolwiekComponent } from './cokolwiek.component';

describe('CokolwiekComponent', () => {
  let component: CokolwiekComponent;
  let fixture: ComponentFixture<CokolwiekComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CokolwiekComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CokolwiekComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
