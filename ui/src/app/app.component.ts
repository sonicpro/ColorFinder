import {
  Component,
  AfterViewInit,
  Renderer2,
  ViewChild,
  ElementRef,
  OnInit,
} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Observable, Subject, BehaviorSubject } from 'rxjs';
import { filter, concatMap, map } from 'rxjs/operators';
import { IQvaSelectOption } from '@shared-components';
import { AbstractPaletteService } from '../services/models/abstract-palette.service';
import { IPalette } from '../services/models/palette-response';
import { IPaletteColor, FormValue } from './palette/interfaces';
import { ColorMatcherService } from 'src/services/color-matcher.service';
import { ColorMatchingStrategy } from 'src/services/models/color-matching-strategy';

@UntilDestroy()
@Component({
  selector: 'qva-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit, AfterViewInit {
  public sessionIdLabel = 'Enter a Session Id (any GUID matches)';
  public colorMatchingStrategyLabel = 'Color matching strategy:';
  public brightnessDropDownLabel = 'Choose a brightness level:';
  public colorInputLabel = 'Enter a color to match the palette';
  public brightnessLevels: IQvaSelectOption[];
  public fg: FormGroup;
  public sessionId$: Observable<string>;
  public color$: Observable<string>;
  public selectedBrightnessLevel: Subject<number> = new Subject<number>();
  public matchingColorIndex: BehaviorSubject<number> =
    new BehaviorSubject<number>(-1);
  public readonly redMean = ColorMatchingStrategy.RedMean;
  public readonly hue = ColorMatchingStrategy.Hue;
  public readonly rgbSpace = ColorMatchingStrategy.RgbSpace;
  @ViewChild('brightnessLevelLabel', { static: true })
  public brightnessLevelLabel!: ElementRef;
  private palette: IPalette;

  /**
   * Gets a palette service and color matcher service instances through DI.
   */
  constructor(
    private paletteService: AbstractPaletteService,
    private colorMatcherService: ColorMatcherService,
    private fb: FormBuilder,
    private renderer: Renderer2
  ) {
    this.buildForm();
    this.sessionId$ = this.getValueChangesObservable('sessionId');
    this.sessionId$.pipe(
      untilDestroyed(this),
      filter((sessionId: string) => {
        return sessionId !== '';
      })
    )
    .subscribe((sessionId: string) => this.initializePalette(sessionId));
    this.color$ = this.getValueChangesObservable('color');
  }

  public ngOnInit(): void {
    this.fg.valueChanges
      .pipe(
      untilDestroyed(this),
        filter(
          (formValue: FormValue) =>
            this.fg.valid && formValue.levelsDropDown !== null
        ),
        concatMap((formValue) =>
          this.colorMatcherService.getColorName(
            formValue.sessionId,
            formValue.levelsDropDown,
            formValue.color,
            formValue.colorMatchingStrategy
          )
        ),
      map((colorName) => {
          return this.colorNames.findIndex((name) => {
          return name === colorName;
        });
      })
      )
      .subscribe((matchingColorIndex: number) => {
      this.matchingColorIndex.next(matchingColorIndex);
    });
  }

  public ngAfterViewInit(): void {
    this.getValueChangesObservable('levelsDropDown')
      .pipe(untilDestroyed(this))
      .subscribe((selectedBrightnessLevel: number) => {
      this.selectedBrightnessLevel.next(selectedBrightnessLevel);
    });
  }

  public resetColor() {
    this.fg.patchValue({
      color: '',
    });
  }

  public resetSessionId() {
    this.fg.patchValue({
      sessionId: '',
    });
  }

  public onOpenedChange($event: boolean) {
    if (!$event) {
      const control = this.fg.get('levelsDropDown');
      const domElement = this.brightnessLevelLabel.nativeElement as HTMLElement;
      if (control.value === null) {
        this.renderer.addClass(domElement, 'invalid');
      } else {
        this.renderer.removeClass(domElement, 'invalid');
      }
    }
  }

  private buildForm() {
    this.fg = this.fb.group({
      sessionId: this.fb.control('', {
        validators: [Validators.required],
      }),
      colorMatchingStrategy: this.fb.control(ColorMatchingStrategy.RedMean, {
        validators: [Validators.required],
      }),
      levelsDropDown: this.fb.control(
        {
        value: null,
        disabled: false,
        },
        {
        validators: [Validators.required],
        }
      ),
      color: this.fb.control('', {
        validators: [
          Validators.required,
          Validators.pattern(/^(?:[0-9A-Fa-f]{3})?[0-9A-Fa-f]{3}$/),
        ],
      }),
    });
  }

  public getPaletteAtAGivenBrightnessLevel: (
    brightnessLevel: number
  ) => IPaletteColor[] = (brightnessLevel) => {
    const sameBrightnessLevelPalette: IPaletteColor[] = [];
    for (const colorName in this.palette) {
      for (const brightness in this.palette[colorName]) {
        if (+brightness === brightnessLevel) {
            sameBrightnessLevelPalette.push({
              rgb: this.palette[colorName][brightness],
            });
        }
      }
    }

    return sameBrightnessLevelPalette;
  };

  public get colorNames(): string[] {
    return Object.keys(this.palette ?? {});
  }

  private brightnessLevelToQvaSelectOptionProjector: (
    brightnessLevel: string
  ) => IQvaSelectOption = (brightnessLevel) => ({
    label: brightnessLevel,
    value: +brightnessLevel,
  });

  private getValueChangesObservable(propName: string) {
    const control = this.fg.get(propName);
    if (!control) {
      throw new Error(`${propName} not avaialble in form`);
    }
    return control.valueChanges;
  }

  private initializePalette(sessionId: string) {
    this.paletteService
      .getPalette(sessionId)
      .pipe(
        untilDestroyed(this),
        filter(Boolean)
      )
      .subscribe((palette) => {
        this.palette = palette;
        const colorName = Object.keys(this.palette).length
          ? Object.keys(this.palette)[0]
          : null;
        if (colorName !== null) {
          this.brightnessLevels = Object.keys(this.palette[colorName]).map(
            this.brightnessLevelToQvaSelectOptionProjector
          );
        }
      });
  }
}
