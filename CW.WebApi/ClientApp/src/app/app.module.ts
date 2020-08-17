import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { NotificationHubService } from './services/notification-hub.service';
import { CarsWarehouseService } from './services/cars-warehouse.service';
import { JwtHttpInterceptor } from './services/authentication/jwt.httpinterceptor';
import { UnAuthorizeErrorHttpInterceptor } from './services/authentication/unauthorize-error.httpinterceptor';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './services/authentication/auth.guard';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [AuthGuard] },
      { path: 'login', component: LoginComponent },

      // otherwise redirect to home
      { path: '**', redirectTo: '' }
    ])
  ],
  providers: [
    CarsWarehouseService,
    NotificationHubService,
    {
      provide: APP_INITIALIZER,
      useFactory: (ds: NotificationHubService) => () => ds.start(),
      deps: [NotificationHubService],
      multi: true
    } ,
    { provide: HTTP_INTERCEPTORS, useClass: JwtHttpInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: UnAuthorizeErrorHttpInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
