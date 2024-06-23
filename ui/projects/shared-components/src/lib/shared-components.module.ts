import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedComponentsComponent } from './shared-components.component';
import { QvaSelectComponent } from './qva-select/qva-select.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    SharedComponentsComponent,
    QvaSelectComponent
  ],
  imports: [
    MatTooltipModule,
    MatFormFieldModule,
    MatSelectModule,
    ReactiveFormsModule,
    CommonModule
  ],
  exports: [
    QvaSelectComponent
  ]
})
export class SharedComponentsModule { }
