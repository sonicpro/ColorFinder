import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatRadioModule } from '@angular/material/radio';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { ErrorHandlingInterceptor } from '../interceptors/error-handling.interceptor';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedComponentsModule } from '@shared-components';
import { PaletteComponent } from './palette/palette.component';
import { ColorNameColumnComponent } from './color-name-column/color-name-column.component';
import { ColorViewComponent } from './color-view/color-view.component';
import { AbstractPaletteService } from 'src/services/models/abstract-palette.service';
import { PaletteService } from 'src/services/palette.service';

@NgModule({
  declarations: [
    AppComponent,
    PaletteComponent,
    ColorNameColumnComponent,
    ColorViewComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    SharedComponentsModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatRadioModule,
    HttpClientModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      multi: true,
      useClass: ErrorHandlingInterceptor,
    },
    { provide: AbstractPaletteService, useClass: PaletteService }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
