import { BasketService } from './basket.service';
import { IBasket, IBasketItem } from '../shared/models/basket';
import { Observable } from 'rxjs';
import { Component } from '@angular/core';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.css'],
})
export class BasketComponent {
  basket$!: Observable<IBasket|null>;

  constructor(private basketService: BasketService) {}
  
  ngOnInit() { 
    this.basket$ = this.basketService.basket$;
  }

  removeBasketItem(item:IBasketItem){
    this.basketService.RemoveItemFromBasket(item);
  }
  incrementItemQuantity(item:IBasketItem){
    this.basketService.incrementItemQuantity(item);
  }
  decrementItemQuantity(item:IBasketItem){
    this.basketService.decrementItemQuantity(item);
  }
}
