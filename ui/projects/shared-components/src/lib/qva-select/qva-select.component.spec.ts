import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QvaSelectComponent } from './qva-select.component';

describe('QvaSelectComponent', () => {
  let component: QvaSelectComponent;
  let fixture: ComponentFixture<QvaSelectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QvaSelectComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QvaSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
