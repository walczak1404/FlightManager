import { Component, Input, OnInit } from '@angular/core';
import { FlightResponse } from '../../../models/flightResponse.model';
import { DateService } from '../../../services/date.service';

@Component({
  selector: 'app-flight',
  templateUrl: './flight.component.html',
  styleUrl: './flight.component.scss'
})
export class FlightComponent implements OnInit {
  @Input("flight") flight: FlightResponse;
  @Input("isAuthenticated") isAuthenticated: boolean;
  localDateString: string;
  localWeekDayString: string;
  localTimeString: string;

  constructor(private _dateService: DateService) {}

  ngOnInit() {
    const departureDateLocal = this._dateService.toLocalTime(this.flight.departureDateUTC);
    this.localDateString = departureDateLocal.toLocaleDateString("pl-pl", {year:"numeric", month:"2-digit", day:"2-digit"});
    this.localWeekDayString = departureDateLocal.toLocaleDateString("pl-pl", {weekday:"long"});
    this.localTimeString = departureDateLocal.toLocaleTimeString("pl-pl", {hour:"2-digit", minute:"2-digit"});
  }
}
