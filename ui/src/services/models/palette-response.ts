export interface IPaletteEntry {
  [brightnessIndex: number]: string;
}

export interface IPalette {
  [colorName: string]: IPaletteEntry;
}