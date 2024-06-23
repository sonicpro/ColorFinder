import {
  Component,
  ElementRef,
  Input,
  OnInit,
  OnChanges,
  Renderer2,
} from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { IPaletteColor } from './interfaces/palette-color';

@UntilDestroy()
@Component({
  selector: 'qva-palette',
  templateUrl: './palette.component.html',
  styleUrls: ['./palette.component.scss'],
})
export class PaletteComponent implements OnInit, OnChanges {
  @Input() public paletteColors: IPaletteColor[];
  @Input() public brightnessLevel: number;
  @Input() public selectedBrightnessLevel$: Observable<number>;
  @Input() public matchingColorIndex: BehaviorSubject<number>;
  public isSelected: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  /**
   * Injects the reference to the component's element itself.
   */
  constructor(private element: ElementRef, private renderer: Renderer2) {}

  ngOnInit(): void {
    this.selectedBrightnessLevel$
      .pipe(untilDestroyed(this))
      .subscribe((selectedBrightnessLevel) => {
        this.isSelected.next(selectedBrightnessLevel === this.brightnessLevel);
      });
  }

  ngOnChanges(): void {
    const domElement = this.element.nativeElement as HTMLElement;
    this.renderer.removeClass(domElement, 'selected');
    if (this.isSelected.getValue()) {
      this.renderer.addClass(domElement, 'selected');
    }
  }
}
