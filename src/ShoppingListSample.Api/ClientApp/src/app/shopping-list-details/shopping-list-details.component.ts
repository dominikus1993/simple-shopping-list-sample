import {Component, inject, OnInit, signal} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {map} from "rxjs";

@Component({
  selector: 'app-shopping-list-details',
  standalone: true,
  imports: [],
  templateUrl: './shopping-list-details.component.html',
  styleUrl: './shopping-list-details.component.css'
})
export class ShoppingListDetailsComponent implements OnInit {
  private router = inject(ActivatedRoute)
  public id = signal<string | null>(null)

  ngOnInit(): void {
    this.router.paramMap.pipe(
      map(params => params.get('id'))
    ).subscribe(next=> this.id.set(next))
  }


}
