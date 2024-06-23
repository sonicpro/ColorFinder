import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from './../environments/environment';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Observable } from 'rxjs';

import { IPalette, IPaletteEntry } from './models/palette-response';
import { AbstractPaletteService } from './models/abstract-palette.service';

@UntilDestroy()
@Injectable()
export class PaletteService implements AbstractPaletteService {
  private apiUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) {}

  public getPalette: (sessionId: string) => Observable<IPalette> = (
    sessionId
  ) => {
    return this.http.get<IPalette>(`${this.apiUrl}?sessionId=${sessionId}`);
  };
}
