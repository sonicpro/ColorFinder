import { Component, Input } from '@angular/core';

@Component({
  selector: 'qva-color-name-column',
  templateUrl: './color-name-column.component.html',
  styleUrls: ['./color-name-column.component.scss']
})
export class ColorNameColumnComponent {
  @Input() public colorNames: string[];
}
