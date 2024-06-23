import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ColorNameColumnComponent } from './color-name-column.component';

describe('ColorNameColumnComponent', () => {
  let component: ColorNameColumnComponent;
  let fixture: ComponentFixture<ColorNameColumnComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ColorNameColumnComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ColorNameColumnComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
