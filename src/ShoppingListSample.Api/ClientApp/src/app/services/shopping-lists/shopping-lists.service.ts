import {Inject, inject, Injectable} from '@angular/core';
import {catchError, throwError} from "rxjs";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ShoppingListsService {
  public http: HttpClient = inject(HttpClient)

  constructor(@Inject('BASE_URL')private baseUrl: string) {

  }

  public getShoppingList(id: string | null) {
    if (!id)
      return throwError(() => ({ message: "Id is null" }));

    return this.http.get<ShoppingListResponse>(`${this.baseUrl}shoppingLists/${id}`);
  }

  public getUserShoppingLists(page: number, pageSize: number) {
    return this.http.get<GetShoppingListsResponse>(`${this.baseUrl}shoppingLists?page=${page}&pageSize=${pageSize}`);
  }
}


export interface ShoppingListResponse {
  readonly id: string
  readonly name: string
  readonly items: ShoppingListItem[]
}

export interface ShoppingListItem {
  readonly id: string
  readonly name: string
  readonly quantity: number
}

export interface ShoppingList {
  readonly id: string
  readonly name: string
}

export interface GetShoppingListsResponse {
  readonly shoppingLists: ShoppingList[]
  readonly total: number
}
