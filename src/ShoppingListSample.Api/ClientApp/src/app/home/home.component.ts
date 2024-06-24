import {Component, Inject, inject} from '@angular/core';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {ActivatedRoute, Params, Router} from "@angular/router";
import {catchError, map, Observable, of, switchMap} from "rxjs";

interface FetchDataComponentShoppingListsData {
  readonly data: GetShoppingListsResponse | null;
  readonly errors: HttpErrorResponse | null;
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  private http: HttpClient = inject(HttpClient);
  private activeRouter: ActivatedRoute = inject(ActivatedRoute);

  public shoppingLists: Observable<FetchDataComponentShoppingListsData> = this.activeRouter.queryParams.pipe(
    switchMap((params: Params) => this.getUserShoppingLists(params["page"] ?? 1, params["pageSize"] ?? 12)),
    map((response) => ({ data: response, errors: null })),
    catchError((error) => of({ data: null, errors: error }))
  )

  constructor(@Inject('BASE_URL') private baseUrl: string) {
  }

  getUserShoppingLists(page: number, pageSize: number) {
    return this.http.get<GetShoppingListsResponse>(this.baseUrl + `shoppingLists?page=${page}&pageSize=${pageSize}`);
  }
}


interface ShoppingList {
  readonly id: string
  readonly name: string
}

interface GetShoppingListsResponse {
  readonly shoppingLists: ShoppingList[]
  readonly total: number
}
