// app.module.ts
@NgModule({
    providers: [
      { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
    ],
  })
  export class AppModule {}
  