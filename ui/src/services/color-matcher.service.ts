import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from './../environments/environment';
import { Observable } from 'rxjs';
import { ColorMatchingStrategy } from './models/color-matching-strategy';

@Injectable({
  providedIn: 'root',
})
export class ColorMatcherService {
  private apiUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) {}

  public getColorName(
    sessionId: string,
    brightnessLevel: number,
    cssColor: string,
    colorMatchingStrategy: ColorMatchingStrategy
  ): Observable<string> {
    return this.http
      .get(
        `${this.apiUrl}/matching-color?sessionId=${sessionId}&BrightnessLevelValue=${brightnessLevel}&Color=${cssColor}&ColorMatchingStrategy=${colorMatchingStrategy}`,
        { responseType: 'text', observe: 'body' }
      );
  }
}
