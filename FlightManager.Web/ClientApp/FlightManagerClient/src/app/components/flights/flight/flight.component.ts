import { Component, Input, OnInit } from '@angular/core';
import { FlightResponse } from '../../../models/flightResponse.model';

@Component({
  selector: 'app-flight',
  templateUrl: './flight.component.html',
  styleUrl: './flight.component.scss'
})
export class FlightComponent implements OnInit {
  @Input("flight") flight: FlightResponse;
  localDateString: string;
  localWeekDayString: string;
  localTimeString: string;

  ngOnInit() {
    const departureDateLocal = this.convertToLocalTime();
    this.localDateString = departureDateLocal.toLocaleDateString("pl-pl", {year:"numeric", month:"2-digit", day:"2-digit"});
    this.localWeekDayString = departureDateLocal.toLocaleDateString("pl-pl", {weekday:"long"});
    this.localTimeString = departureDateLocal.toLocaleTimeString("pl-pl", {hour:"2-digit", minute:"2-digit"});
  }

  private convertToLocalTime(): Date {
    let dateUTC = this.flight.departureDateUTC + "Z";
    return new Date(dateUTC);
  }
}
