import {Component, Inject, inject} from '@angular/core';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {ActivatedRoute, Params, Router} from "@angular/router";
import {catchError, map, Observable, of, switchMap} from "rxjs";
import {GetShoppingListsResponse, ShoppingListsService} from "../services/shopping-lists/shopping-lists.service";

interface FetchDataComponentShoppingListsData {
  readonly data: GetShoppingListsResponse | null;
  readonly errors: HttpErrorResponse | null;
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  private activeRouter: ActivatedRoute = inject(ActivatedRoute);
  private shoppingListsService = inject(ShoppingListsService)
  public shoppingLists$: Observable<FetchDataComponentShoppingListsData> = this.activeRouter.queryParams.pipe(
    switchMap((params: Params) => this.shoppingListsService.getUserShoppingLists(params["page"] ?? 1, params["pageSize"] ?? 12)),
    map((response) => ({ data: response, errors: null })),
    catchError((error) => of({ data: null, errors: error }))
  )

  constructor() {
  }
}
