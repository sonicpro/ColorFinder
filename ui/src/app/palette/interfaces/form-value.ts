import { ColorMatchingStrategy } from "src/services/models/color-matching-strategy";

export interface FormValue {
  sessionId: string;
  levelsDropDown: number;
  color: string;
  colorMatchingStrategy: ColorMatchingStrategy;
}