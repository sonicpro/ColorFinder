import { Observable } from "rxjs";
import { IPalette } from "./palette-response";

export abstract class AbstractPaletteService {
  public abstract getPalette(sessionId: string): Observable<IPalette>;
}