import {
  Component,
  AfterViewInit,
  Input,
  Renderer2,
  ElementRef
} from '@angular/core';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Observable } from 'rxjs';
import { withLatestFrom } from 'rxjs/operators';
import { IPaletteColor } from '../palette/interfaces';

@UntilDestroy()
@Component({
  selector: 'qva-color-view',
  templateUrl: './color-view.component.html',
  styleUrls: ['./color-view.component.scss']
})
export class ColorViewComponent implements AfterViewInit {
  @Input() public color: IPaletteColor;
  @Input() public index: number;
  @Input() public matchingColorIndex$: Observable<number>;
  @Input() public isFromSelectedPalette$: Observable<boolean>;

  constructor(private element: ElementRef, private renderer: Renderer2) { }

  ngAfterViewInit(): void {    this.matchingColorIndex$.pipe(
      untilDestroyed(this),
      withLatestFrom(this.isFromSelectedPalette$)
    ).subscribe(this.hightlightMatchingColor);
  }

  private hightlightMatchingColor: (tupleOfValues: [number, boolean]) => void = (tupleOfValues) => {
    const matchingColorIndex = tupleOfValues[0];
    const isFromSelectedPalette = tupleOfValues[1];
    if (matchingColorIndex === -1 ||
      !isFromSelectedPalette ||
      this.index !== matchingColorIndex) {
        return;
    } else {
      const domElement = this.element.nativeElement;
      this.renderer.addClass(domElement, 'matching-color');
    }
  }
}
