import { FlightResponse } from "./flightResponse.model";

export class PagedList {
   items: FlightResponse[];
   page: number;
   totalPagesCount: number;
}