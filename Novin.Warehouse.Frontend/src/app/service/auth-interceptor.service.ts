// import { Injectable, inject } from '@angular/core';
// import { HttpInterceptor, HttpRequest, HttpHandler, HttpEventType, HttpParams } from '@angular/common/http';
// import { exhaustMap, take, tap } from 'rxjs/operators';
// import { AuthService } from './auth.service';

// @Injectable()
// export class AuthInterceptorService implements HttpInterceptor {
  
//   constructor(private authService: AuthService) {
//     console.log("interceptor constructor")
//    }

//   intercept(req: HttpRequest<any>, next: HttpHandler) {
//     console.log("I enetered")
//     return this.authService.user.pipe(take(1), exhaustMap(user => {
//       if (!user) {
//         console.log("entered exhaust map");
//         return next.handle(req);
//       }
//       const modifiedReq = req.clone({
//         headers: req.headers.set('Authorization', `Bearer ${user.token}`)
//       });
//       console.log(user.token);
//       return next.handle(modifiedReq);
//     }));
//   }
// }

import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from './auth.service';
import { exhaustMap, take } from 'rxjs/operators';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);

  return authService.user.pipe(
    take(1),
    exhaustMap(user => {
      if (!user) {
        return next(req);
      }
      const modifiedReq = req.clone({
        headers: req.headers.set('Authorization', `Bearer ${user.token}`),
      });
      return next(modifiedReq);
    })
  );
};