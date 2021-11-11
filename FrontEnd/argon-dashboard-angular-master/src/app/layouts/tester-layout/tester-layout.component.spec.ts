import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TesterLayoutComponent } from './tester-layout.component';

describe('TesterLayoutComponent', () => {
  let component: TesterLayoutComponent;
  let fixture: ComponentFixture<TesterLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TesterLayoutComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TesterLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
