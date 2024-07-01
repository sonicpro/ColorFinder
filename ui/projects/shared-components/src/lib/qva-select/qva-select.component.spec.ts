import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { QvaSelectComponent } from './qva-select.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('QvaSelectComponent', () => {
  let component: QvaSelectComponent;
  let fixture: ComponentFixture<QvaSelectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QvaSelectComponent ],
      imports: [ MatFormFieldModule, MatSelectModule, ReactiveFormsModule, BrowserAnimationsModule ],
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
