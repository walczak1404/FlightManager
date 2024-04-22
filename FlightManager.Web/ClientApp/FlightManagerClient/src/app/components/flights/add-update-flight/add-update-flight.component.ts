import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { FlightsService } from '../../../services/flights.service';
import { FlightResponse } from '../../../models/flightResponse.model';
import { HttpErrorResponse } from '@angular/common/http';
import { AircraftTypeResponse } from '../../../models/aircraftTypeResponse.model';
import { AircraftTypesService } from '../../../services/aircraft-types.service';
import { NgForm } from '@angular/forms';
import { Location } from '@angular/common';
import { DateService } from '../../../services/date.service';

@Component({
  selector: 'app-add-update-flight',
  templateUrl: './add-update-flight.component.html',
  styleUrl: './add-update-flight.component.scss'
})
export class AddUpdateFlightComponent implements OnInit, OnDestroy {
  routeSubscription: Subscription;
  paramSubscription: Subscription;
  isUpdate: boolean;
  isDateValid: boolean;
  updateFlightID: string;
  updateFlightNumber: string;
  updateFlightDepartureDate: string;
  updateFlightDepartureCity: string;
  updateFlightArrivalCity: string;
  updateFlightAircraftTypeID: string;
  isLoading: boolean;
  serverErrorMessage: string = null;
  aircraftTypeOptions: AircraftTypeResponse[];
  @ViewChild("form") form: NgForm;

  constructor(private _route: ActivatedRoute, private _flightsService: FlightsService, private _aircraftTypesService: AircraftTypesService, private _location: Location, private _dateService: DateService) {}

  ngOnInit() {
    this.isLoading = true;
    this.routeSubscription = this._route.url.subscribe(segments => {
      if(segments.some(segment => segment.path === "edit")) this.isUpdate = true;
      else this.isUpdate = false;
    });

    this.paramSubscription = this._route.params.subscribe(params => {
      this.updateFlightID = params["flightID"];
    });

    this._aircraftTypesService.getAircraftTypes().subscribe({
      next: (aircraftTypesRes: AircraftTypeResponse[]) => {
        this.aircraftTypeOptions = aircraftTypesRes;
      },

      error: (errorRes: HttpErrorResponse) => {
        this.serverErrorMessage = "Nie udało się wczytać typów samolotu";
      }
    })

    if(this.isUpdate) {
      this._flightsService.getFlightByID(this.updateFlightID).subscribe({
        next: (flightResponse: FlightResponse) => {
          this.updateFlightNumber = flightResponse.number;
          this.updateFlightDepartureDate = this._dateService.toLocalTimeInputString(flightResponse.departureDateUTC);
          this.updateFlightDepartureCity = flightResponse.departureCity;
          this.updateFlightArrivalCity = flightResponse.arrivalCity;
          this.updateFlightAircraftTypeID = flightResponse.aircraftType.aircraftTypeID;
          this.isLoading = false;
          this.validateFutureDate(this.updateFlightDepartureDate)
        },

        error: (errorRes: HttpErrorResponse) => {
          this.serverErrorMessage = errorRes.error.detail;
        }
      });
    } else {
      this.isLoading = false;
    }
  }

  ngOnDestroy() {
    this.routeSubscription.unsubscribe();
    this.paramSubscription.unsubscribe();
  }

  validateFutureDate(dateStr: string) {
    this.isDateValid = new Date(dateStr) <= new Date() ? false : true;
  }

  onFormSubmit() {
    if(this.form.valid && this.isDateValid) {
      const departureDateUTC = this._dateService.toISOString(this.form.value.departureDate);
      let tempObs: Observable<FlightResponse>;
      if(this.isUpdate) {
        tempObs = this._flightsService.putFlight(this.form.value.flightID, this.form.value.number, departureDateUTC, this.form.value.departureCity, this.form.value.arrivalCity, this.form.value.aircraftTypeID);
      } else {
        tempObs = this._flightsService.postFlight(this.form.value.number, departureDateUTC, this.form.value.departureCity, this.form.value.arrivalCity, this.form.value.aircraftTypeID);
      }

      tempObs.subscribe({
        next: (flightResponse: FlightResponse) => {
          this._location.back();
        },

        error: (errorRes: HttpErrorResponse) => {
          console.log(errorRes);
        }
      })
    };
  }

  onReturnToLastPage() {
    this._location.back();
  }
}
