import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { FlightListComponent } from './components/flights/flight-list/flight-list.component';
import { FlightComponent } from './components/flights/flight/flight.component';
import { AddUpdateFlightComponent } from './components/flights/add-update-flight/add-update-flight.component';
import { LayoutModule } from '@angular/cdk/layout';
import { LoginComponent } from './components/auth/login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RegisterComponent } from './components/auth/register/register.component';
import { LoadingSpinnerComponent } from './components/loading-spinner/loading-spinner.component';
import { ItemDisabledMessageComponent } from './components/item-disabled-message/item-disabled-message.component';
import { DeleteFlightComponent } from './components/flights/delete-flight/delete-flight.component';
import { BackdropComponent } from './components/backdrop/backdrop.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FlightListComponent,
    FlightComponent,
    AddUpdateFlightComponent,
    LoginComponent,
    RegisterComponent,
    LoadingSpinnerComponent,
    ItemDisabledMessageComponent,
    DeleteFlightComponent,
    BackdropComponent
  ],
  imports: [
    BrowserModule, 
    AppRoutingModule,
    LayoutModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
