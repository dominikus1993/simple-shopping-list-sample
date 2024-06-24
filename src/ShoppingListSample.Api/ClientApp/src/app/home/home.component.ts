import {Component, Inject, inject} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Params, Router} from "@angular/router";
import {switchMap} from "rxjs";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  private http: HttpClient = inject(HttpClient);
  private router: ActivatedRoute = inject(ActivatedRoute);

  public shoppingLists = this.router.queryParams.pipe(
    switchMap((params: Params) => this.getUserShoppingLists(params["page"] ?? 1, params["pageSize"] ?? 12))
  )

  constructor(@Inject('BASE_URL') private baseUrl: string) {
  }

  getUserShoppingLists(page: number, pageSize: number) {
    return this.http.get<ShoppingList[]>(this.baseUrl + `api/shoppingLists?page=${page}&pageSize=${pageSize}`);
  }
}


interface ShoppingList {
  readonly id: string
  readonly name: string
}
