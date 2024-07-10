import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExaminationCommitteeHeadComponent } from './examination-committee-head.component';

describe('ExaminationCommitteeHeadComponent', () => {
  let component: ExaminationCommitteeHeadComponent;
  let fixture: ComponentFixture<ExaminationCommitteeHeadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExaminationCommitteeHeadComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExaminationCommitteeHeadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
