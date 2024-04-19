import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { FlightListComponent } from './components/flights/flight-list/flight-list.component';
import { FlightComponent } from './components/flights/flight/flight.component';
import { AddUpdateFlightComponent } from './flights/add-update-flight/add-update-flight.component';

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FlightListComponent,
    FlightComponent,
    AddUpdateFlightComponent
  ],
  imports: [
    BrowserModule, 
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
