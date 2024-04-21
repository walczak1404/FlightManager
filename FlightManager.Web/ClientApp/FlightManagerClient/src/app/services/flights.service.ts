import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SortType } from '../enums/SortType';
import { SortOrder } from '../enums/SortOrder';
import { Observable } from 'rxjs';
import { FlightResponse } from '../models/flightResponse.model';
import { environment } from '../../environments/environment';
import { PagedList } from '../models/pagedList.model';

@Injectable({
  providedIn: 'root'
})
export class FlightsService {

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
}
