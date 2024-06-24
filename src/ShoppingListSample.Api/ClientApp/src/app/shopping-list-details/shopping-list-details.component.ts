import {Component, Inject, inject, OnInit, signal} from '@angular/core';
import {ActivatedRoute, Params} from "@angular/router";
import {catchError, map, Observable, of, switchMap, throwError} from "rxjs";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";

interface Error {
  message: string
}
interface FetchComponentShoppingListsData {
  readonly data: ShoppingListResponse | null;
  readonly errors: Error | null;
}

@Component({
  selector: 'app-shopping-list-details',
  standalone: true,
  imports: [],
  templateUrl: './shopping-list-details.component.html',
  styleUrl: './shopping-list-details.component.css'
})
export class ShoppingListDetailsComponent implements OnInit {
  private router = inject(ActivatedRoute)
  private http = inject(HttpClient)

  public id = signal<string | null>(null)

  public shoppingLists: Observable<FetchComponentShoppingListsData> = this.router.queryParams.pipe(
    switchMap((params: Params) => this.getShoppingList(params["id"])),
    map((response) => ({ data: response, errors: null })),
    catchError((error) => of({ data: null, errors: error }))
  )
  constructor(@Inject('BASE_URL')private baseUrl: string) {
  }

  ngOnInit(): void {
    this.router.paramMap.pipe(
      map(params => params.get('id'))
    ).subscribe(next=> this.id.set(next))
  }


  getShoppingList(id: string | null) {
    if (id === null)
      return throwError(() => ({ message: "Id is null" }))

    return this.http.get<ShoppingListResponse>(this.baseUrl + `shoppingLists/${id}`);
  }

}

interface ShoppingListResponse {
  readonly id: string
  readonly name: string
  readonly items: ShoppingListItem[]
}

interface ShoppingListItem {
  readonly id: string
  readonly name: string
  readonly quantity: number
}
