import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FlightsService } from '../../../services/flights.service';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { PagedList } from '../../../models/pagedList.model';
import { HttpErrorResponse } from '@angular/common/http';
import { FlightResponse } from '../../../models/flightResponse.model';
import { Subscription, combineLatest } from 'rxjs';
import { AccountService } from '../../../services/account.service';

@Component({
  selector: 'app-flight-list',
  templateUrl: './flight-list.component.html',
  styleUrl: './flight-list.component.scss'
})
export class FlightListComponent implements OnInit, OnDestroy {
  flights: FlightResponse[];
  totalPagesCount: number;

  private _authenticationObserver: Subscription;
  isAuthenticated: boolean;

  isLoading: boolean = false;

  @ViewChild("sortOptions") sortOptionsElement: ElementRef;
  @ViewChild("filterOptions") filterOptionsElement: ElementRef;
  @ViewChild("filterOptionsContainer") filterOptionsContainer: ElementRef;

  filterOptionsVisible: boolean = false;
  sortOptionsVisible: boolean = false;
  pageNumber: number;
  sortType: string = "departureDateUTC";
  sortOrder: string = "ASC";
  departureCityFilter: string = "";
  arrivalCityFilter: string = "";

  paramsSubscription: Subscription;

  sortOptionsArray = [
    {text: "Data wylotu (rosnąco)", queryParams: {sortType: "departureDateUTC", sortOrder: "ASC", departureCity: this.departureCityFilter, arrivalCity: this.arrivalCityFilter}},
    {text: "Data wylotu (malejąco)", queryParams: {sortType: "departureDateUTC", sortOrder: "DESC", departureCity: this.departureCityFilter, arrivalCity: this.arrivalCityFilter}},
    {text: "Miejsce wylotu (A-Z)", queryParams: {sortType: "departureCity", sortOrder: "ASC", departureCity: this.departureCityFilter, arrivalCity: this.arrivalCityFilter}},
    {text: "Miejsce wylotu (Z-A)", queryParams: {sortType: "departureCity", sortOrder: "DESC", departureCity: this.departureCityFilter, arrivalCity: this.arrivalCityFilter}},
    {text: "Miejsce przylotu (A-Z)", queryParams: {sortType: "arrivalCity", sortOrder: "ASC", departureCity: this.departureCityFilter, arrivalCity: this.arrivalCityFilter}},
    {text: "Miejsce przylotu (Z-A)", queryParams: {sortType: "arrivalCity", sortOrder: "DESC", departureCity: this.departureCityFilter, arrivalCity: this.arrivalCityFilter}},
    {text: "Typ samolotu (A-Z)", queryParams: {sortType: "aircraftType", sortOrder: "ASC", departureCity: this.departureCityFilter, arrivalCity: this.arrivalCityFilter}},
    {text: "Typ samolotu (Z-A)", queryParams: {sortType: "aircraftType", sortOrder: "DESC", departureCity: this.departureCityFilter, arrivalCity: this.arrivalCityFilter}},
  ]

  serverError: string = null;

  constructor(private _flightsService: FlightsService, private _accountService: AccountService, private _route: ActivatedRoute, private _router: Router) { }

  ngOnInit(): void {
    this.paramsSubscription = combineLatest([
      this._route.params,
      this._route.queryParams
    ]).subscribe({
      next: ([params, queryParams]) => {
        this.pageNumber = params["pageNumber"];
        this.sortType = queryParams["sortType"] || this.sortType;
        this.sortOrder = queryParams["sortOrder"] || this.sortOrder;
        this.departureCityFilter = queryParams["departureCity"] || this.departureCityFilter;
        this.arrivalCityFilter = queryParams["arrivalCity"] || this.arrivalCityFilter;
        this.sortOptionsVisible = false;
        this.fetchFlights();
      }
    });

    this._authenticationObserver = this._accountService.isAuthenticated.subscribe({
      next: userLoggedIn => {
        this.isAuthenticated = userLoggedIn;
      }
    })
  }

  ngOnDestroy() {
    this.paramsSubscription.unsubscribe();
  }

  private fetchFlights() {
    this.isLoading = true;
    this._flightsService.getFlights(this.pageNumber, this.sortType, this.sortOrder, this.departureCityFilter, this.arrivalCityFilter).subscribe({
      next: (flightsData: PagedList) => {
        this.flights = flightsData.items;
        this.totalPagesCount = flightsData.totalPagesCount;
        this.isLoading = false;
      },

      error: (errorRes: HttpErrorResponse) => {
        if(errorRes.status === 500 || errorRes.status === 0) this.serverError = "Serwer nie odpowiada";
        else {
          this.serverError = Object.values(errorRes.error.errors)[0][0];
        }
        this.isLoading = false;
      }
    })
  }

  onFilter() {
    this.filterOptionsVisible = false;
    this._router.navigate([`/flights/${this.pageNumber}`], {queryParams: {
      sortType: this.sortType,
      sortOrder: this.sortOrder,
      departureCity: this.departureCityFilter,
      arrivalCity: this.arrivalCityFilter
    }})
  }

  toggleFilterOptions() {
    this.filterOptionsVisible = !this.filterOptionsVisible;
  }

  toggleSortOptions() {
    this.sortOptionsVisible = !this.sortOptionsVisible;
  }

  hideSortAndFilterOptions(event: Event) {
    const notInFilterDiv = this.filterOptionsContainer ? !this.filterOptionsContainer.nativeElement.contains(event.target) : true;
    if(event.target != this.sortOptionsElement.nativeElement && event.target != this.filterOptionsElement.nativeElement && notInFilterDiv) {
      this.filterOptionsVisible = false;
      this.sortOptionsVisible = false;
    }

    if(event.target == this.sortOptionsElement.nativeElement) {
      this.filterOptionsVisible = false;
    }

    if(event.target == this.filterOptionsElement.nativeElement) {
      this.sortOptionsVisible = false;
    }
  }
}
