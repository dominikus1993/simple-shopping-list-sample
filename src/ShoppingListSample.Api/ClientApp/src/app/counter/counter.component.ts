import {Component, computed, signal} from '@angular/core';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  public currentCount = signal(0);

  public powCounter = computed(() => this.currentCount() ** 2);
  public async incrementCounter() {
    this.currentCount.set(this.currentCount() + 1)
  }

  public async decrementCounter() {
    this.currentCount.set(this.currentCount() - 1)
  }
}
