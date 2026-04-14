import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { JwtInterceptor } from './interceptors/jwt';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideHttpClient(withInterceptorsFromDi()), //this allows angular to call ASP.NET Core Web API endpoints
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}, //this allows the JWT interceptor to be used for all HTTP requests
    provideRouter(routes) //this enables navivation between pages
  ]
};
