import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { environment } from '../../environments/environment';
import { PagedList } from '../models/pagedList.model';
import { FlightResponse } from '../models/flightResponse.model';

@Injectable({
  providedIn: 'root'
})
export class FlightsService {

  reloadFlights: Subject<void> = new Subject<void>();

  constructor(private _httpClient: HttpClient) { }

  getFlights(pageNumber: number = 1, sortType: string = "DepartureDateUTC", sortOrder: string = "ASC", departureCity: string = "", arrivalCity: string = ""): Observable<PagedList> {
    let searchParams = new HttpParams();
    searchParams = searchParams.append("sortType", sortType);
    searchParams = searchParams.append("sortOrder", sortOrder);
    searchParams = searchParams.append("departureCity", departureCity);
    searchParams = searchParams.append("arrivalCity", arrivalCity);

    return this._httpClient.get<PagedList>(`${environment["API_URL"]}/flights/${pageNumber}`, {
      params: searchParams
    })
  }
  
  getFlightByID(flightID: string): Observable<FlightResponse> {
    return this._httpClient.get<FlightResponse>(`${environment["API_URL"]}/flights/${flightID}`);
  }

  postFlight(number, departureDateUTC, departureCity, arrivalCity, aircraftTypeID): Observable<FlightResponse> {
    return this._httpClient.post<FlightResponse>(`${environment["API_URL"]}/flights`,
      {
        number,
        departureDateUTC,
        departureCity,
        arrivalCity,
        aircraftTypeID
      },
      {
        headers: new HttpHeaders({"Authorization": `Bearer ${localStorage.getItem("token")}`})
      }
    );
  }

  putFlight(flightID, number, departureDateUTC, departureCity, arrivalCity, aircraftTypeID): Observable<FlightResponse> {
    return this._httpClient.put<FlightResponse>(`${environment["API_URL"]}/flights`,
      {
        flightID,
        number,
        departureDateUTC,
        departureCity,
        arrivalCity,
        aircraftTypeID
      },
      {
        headers: new HttpHeaders({"Authorization": `Bearer ${localStorage.getItem("token")}`})
      }
    );
  }

  deleteFlight(flightID: string): Observable<void> {
    return this._httpClient.delete<void>(`${environment["API_URL"]}/flights/${flightID}`, {
      headers: new HttpHeaders({"Authorization": `Bearer ${localStorage.getItem("token")}`})
    });
  }
}
