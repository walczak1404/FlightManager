import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HeaderComponent } from './components/header/header.component';
import { FlightListComponent } from './components/flights/flight-list/flight-list.component';
import { AddUpdateFlightComponent } from './components/flights/add-update-flight/add-update-flight.component';
import { LoginComponent } from './components/auth/login/login.component';
import { isAuthorized, isUnauthorized } from './guards/auth.guard';
import { RegisterComponent } from './components/auth/register/register.component';

const routes: Routes = [
  {path: "", component: HeaderComponent, children: [
    {path: "", component: FlightListComponent},
    {path: "new-flight", component: AddUpdateFlightComponent, canActivate: [isAuthorized]}
  ]},
  {path: "login", component: LoginComponent, canActivate: [isUnauthorized]},
  {path: "register", component: RegisterComponent, canActivate: [isUnauthorized]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
