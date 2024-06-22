import {Component, Inject, OnInit, Signal, signal} from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {catchError, combineLatest, map, Observable, of} from "rxjs";
import {toSignal} from "@angular/core/rxjs-interop";

interface FetchDataComponentForecastData {
  readonly forecasts: WeatherForecast[];
  readonly errors: HttpErrorResponse | null;
}

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html',
})
export class FetchDataComponent {
  public data: Signal<FetchDataComponentForecastData> = toSignal(this.getWeatherForecasts(), { initialValue: { forecasts: [], errors: null} });
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  private getWeatherForecasts() {
    return this.http.get<WeatherForecast[]>(this.baseUrl + 'weatherforecast').pipe(
      map((response) => ({ forecasts: response, errors: null })),
      catchError((error) => of({ forecasts: [], errors: error }))
    );
  }
}

interface WeatherForecast {
  readonly date: string;
  readonly temperatureC: number;
  readonly temperatureF: number;
  readonly summary: string;
}
