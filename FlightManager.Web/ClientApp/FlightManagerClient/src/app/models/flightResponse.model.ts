import { AircraftTypeResponse } from "./aircraftTypeResponse.model";

export interface FlightResponse {
   flightID: string;
   number: string;
   departureDateUTC: string;
   departureCity: string;
   arrivalCity: string;
   aircraftType: AircraftTypeResponse;
}