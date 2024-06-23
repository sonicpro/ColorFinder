import {
  HttpErrorResponse,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpResponse
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of, throttleTime, throwError } from 'rxjs';

@Injectable()
export class ErrorHandlingInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(req).pipe(
      catchError((error: unknown) => {
        if (error instanceof HttpErrorResponse) {
          console.log(`Error ${error.name}: ${error.message}.`);
          return of(new HttpResponse({statusText: error.message}))
        }

        return throwError(() => error);
      })
    );
  }
}