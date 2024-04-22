import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, Params } from '@angular/router';
import { Subscription } from 'rxjs';
import { FlightsService } from '../../../services/flights.service';

@Component({
  selector: 'app-delete-flight',
  templateUrl: './delete-flight.component.html',
  styleUrl: './delete-flight.component.scss'
})
export class DeleteFlightComponent implements OnInit, OnDestroy {

  @ViewChild("deleteAlert", {static: true}) deleteAlert: ElementRef;

  flightID: string;
  paramSubscription: Subscription;

  constructor(private _location: Location, private _route: ActivatedRoute, private _flightsService: FlightsService) {}

  ngOnInit() {
    this.paramSubscription = this._route.params.subscribe({
      next: (params: Params) => {
        this.flightID = params["flightID"];
        console.log(this.flightID);
      }
    });
  }

  ngOnDestroy() {

  }

  onDeleteFlight() {
    this._flightsService.deleteFlight(this.flightID).subscribe({
      next: () => {
        this._location.back();
      }
    })
  }

  onReturnToLastPage() {
    this._location.back();
  }
}
