import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AircraftTypeResponse } from '../models/aircraftTypeResponse.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AircraftTypesService {

  constructor(private _httpClient: HttpClient) { }

  getAircraftTypes(): Observable<AircraftTypeResponse[]> {
    return this._httpClient.get<AircraftTypeResponse[]>(`${environment["API_URL"]}/aircraft-types`);
  }
}
