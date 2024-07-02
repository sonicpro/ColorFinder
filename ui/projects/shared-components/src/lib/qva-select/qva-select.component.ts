import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  forwardRef,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import {
  ControlValueAccessor,
  FormBuilder,
  FormControl,
  FormGroup,
  NG_VALUE_ACCESSOR,
} from '@angular/forms';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { IQvaSelectOption } from '../qva-select-options.interface';

@Component({
  selector: 'qva-select',
  templateUrl: './qva-select.component.html',
  styleUrls: ['./qva-select.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => QvaSelectComponent),
      multi: true,
    },
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
@UntilDestroy()
export class QvaSelectComponent implements OnInit, ControlValueAccessor {
  public formGroup!: FormGroup;
  public formControl!: FormControl;
  @Input() options!: IQvaSelectOption[];
  @Input() label!: string;
  @Input() initialOption!: string;
  @Output() emitSelectionChange: EventEmitter<string | number> = new EventEmitter();
  @Output() openedChange: EventEmitter<boolean> = new EventEmitter();

  constructor(private formBuilder: FormBuilder) {}
  writeValue(value: unknown): void {
    this.formControl.patchValue(value, { emitEvent: false });
  }
  registerOnChange(fn: (value: unknown) => void) {
    this._controlValueAccessorChangeFn = fn;
  }

  registerOnTouched(fn: () => unknown) {
    this._onTouched = fn;
  }
  setDisabledState?(isDisabled: boolean): void {
    if (isDisabled) {
      this.formControl.disable();
    } else {
      this.formControl.enable();
    }
  }

  ngOnInit(): void {
    this.formControl = this.formBuilder.control('');
    this.formGroup = this.formBuilder.group({
      control: this.formControl,
    });
    this.formControl.valueChanges.pipe(untilDestroyed(this)).subscribe((v) => {
      this.emitSelectionChange.emit(v);
      this._controlValueAccessorChangeFn(v);
    });
  }

  public openedChangePassThrough: ($event: boolean) => void = ($event) => this.openedChange.emit($event);

  private _controlValueAccessorChangeFn: (value: unknown) => void = () => {};

  _onTouched: () => unknown = () => {};
}
