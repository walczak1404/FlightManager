import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FlightListComponent } from './components/flights/flight-list/flight-list.component';
import { AddUpdateFlightComponent } from './components/flights/add-update-flight/add-update-flight.component';
import { LoginComponent } from './components/auth/login/login.component';
import { isAuthorized, isUnauthorized } from './guards/auth.guard';
import { RegisterComponent } from './components/auth/register/register.component';
import { validPageNumberGuard } from './guards/valid-page-number.guard';

const routes: Routes = [
  {path: "flights/:pageNumber", component: FlightListComponent, canActivate: [validPageNumberGuard]},
  {path: "flights", redirectTo: "/flights/1", pathMatch: 'full'},
  {path: "new-flight", component: AddUpdateFlightComponent, canActivate: [isAuthorized]},
  {path: "login", component: LoginComponent, canActivate: [isUnauthorized]},
  {path: "register", component: RegisterComponent, canActivate: [isUnauthorized]},
  {path: "", redirectTo: "/flights/1", pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
