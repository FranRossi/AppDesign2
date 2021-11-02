import {HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';


export class AuthInterceptorService implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler) {
    if (req.url != 'http://localhost:4200/#/login' ){
      const modifiedRequest = req.clone({headers: req.headers.append('token', localStorage.getItem('userToken'))});
      return next.handle(modifiedRequest);
    }
    else
      return next.handle(req);
  }
}
