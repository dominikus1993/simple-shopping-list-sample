import {Component, Inject, inject, OnInit, signal} from '@angular/core';
import {ActivatedRoute, ParamMap, Params} from "@angular/router";
import {catchError, map, Observable, of, switchMap} from "rxjs";
import {ShoppingListResponse, ShoppingListsService} from "../services/shopping-lists/shopping-lists.service";
import {AsyncPipe} from "@angular/common";

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
  imports: [
    AsyncPipe
  ],
  templateUrl: './shopping-list-details.component.html',
  styleUrl: './shopping-list-details.component.css'
})
export class ShoppingListDetailsComponent implements OnInit {
  private router = inject(ActivatedRoute)
  private shoppingListsService = inject(ShoppingListsService)

  public id = signal<string | null>(null)

  public shoppingList: Observable<FetchComponentShoppingListsData> = this.router.paramMap.pipe(
    switchMap((params: ParamMap) => this.shoppingListsService.getShoppingList(params.get("id"))),
    map((response) => ({ data: response, errors: null })),
    catchError((error) => of({ data: null, errors: error }))
  )
  constructor() {
  }

  ngOnInit(): void {
    this.router.paramMap.pipe(
      map(params => params.get('id'))
    ).subscribe(next=> this.id.set(next))
  }
}
