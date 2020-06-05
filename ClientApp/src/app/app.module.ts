import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CoffeeFetchComponent } from './coffee-fetch/coffee-fetch.component';
import {CoffeeFetchComponentCreateDialog, CoffeeFetchComponentDialog} from './coffee-fetch/coffee-fetch.component';
import {TeaFetchComponentCreateDialog, TeaFetchComponentDialog} from './tea-fetch/tea-fetch.component';
import {MilkFetchComponentCreateDialog, MilkFetchComponentDialog} from './milk-fetch/milk-fetch.component';
import {ToppingFetchComponentCreateDialog, ToppingFetchComponentDialog} from './topping-fetch/topping-fetch.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import {MatButtonModule, MatCardModule, MatIconModule, MatDialogModule, MatFormFieldModule, MatInputModule,MatSnackBarModule} from '@angular/material';
import { TeaFetchComponent } from './tea-fetch/tea-fetch.component';
import { MilkFetchComponent } from './milk-fetch/milk-fetch.component';
import { ToppingFetchComponent } from './topping-fetch/topping-fetch.component';
import { AuthenticationComponent } from './authentication/authentication.component';
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CoffeeFetchComponent,
    CoffeeFetchComponentDialog,
    CoffeeFetchComponentCreateDialog,
    TeaFetchComponent,
    AuthenticationComponent,
    TeaFetchComponentDialog,
    TeaFetchComponentCreateDialog,
    MilkFetchComponent,
    MilkFetchComponentCreateDialog,
    MilkFetchComponentDialog,
    ToppingFetchComponent,
    ToppingFetchComponentCreateDialog,
    ToppingFetchComponentDialog,
    AuthenticationComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    MatButtonModule,
    MatCardModule,MatSnackBarModule,
    MatDialogModule,
    MatFormFieldModule, MatInputModule,
    MatIconModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'authentication-tab', component: AuthenticationComponent },
      { path: 'coffee-tab', component: CoffeeFetchComponent},
      { path: 'tea-tab', component: TeaFetchComponent},
      { path: 'milk-tab', component: MilkFetchComponent},
      { path: 'topping-tab', component: ToppingFetchComponent},
      { path: 'app-coffee-fetch-dialog', component: CoffeeFetchComponentDialog},
      { path: 'app-coffee-fetch-dialog-create', component: CoffeeFetchComponentCreateDialog},
      { path: 'app-tea-fetch-dialog', component: TeaFetchComponentDialog},
      { path: 'app-tea-fetch-dialog-create', component: TeaFetchComponentCreateDialog},
      { path: 'app-milk-fetch-dialog', component: MilkFetchComponentDialog},
      { path: 'app-milk-fetch-dialog-create', component: MilkFetchComponentCreateDialog},
      { path: 'app-topping-fetch-dialog', component: ToppingFetchComponentDialog},
      { path: 'app-topping-fetch-dialog-create', component: ToppingFetchComponentCreateDialog}
    ]),
    NoopAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
