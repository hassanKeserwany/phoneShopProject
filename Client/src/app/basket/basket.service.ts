import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map, toArray } from 'rxjs';
import {
  Basket,
  IBasket,
  IBasketItem,
  IBasketTotlal,
} from '../shared/models/basket';
import { IProduct } from '../shared/models/product';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class BasketService {
  //baseUrl = 'https://localhost:7203/api/';
  baseUrl = environment.apiUrl;

  private basketSource = new BehaviorSubject<IBasket | null>(null); // Initial empty basket
  basket$ = this.basketSource.asObservable();

  private basketTotalSourse = new BehaviorSubject<IBasketTotlal | null>(null);
  basketTotal$ = this.basketTotalSourse.asObservable();
  constructor(private http: HttpClient) {}

  getBasket(id: string) {
    return this.http.get(this.baseUrl + 'basket?id=' + id).pipe(
      map((response: any) => {
        // Cast response to any
        const basket = response as IBasket; // Assert response is actually an IBasket
        this.basketSource.next(basket);
        this.calculateTotals();
        console.log('getCurrentBasketValue()'); ///////
        console.log(this.getCurrentBasketValue()); /////

        return basket;
      })
    );
  }

  setBasket(basket: IBasket) {
    return this.http.post(this.baseUrl + 'basket', basket).subscribe(
      (response: any) => {
        const basket = response as IBasket;
        this.basketSource.next(basket);
        this.calculateTotals();
        console.log(basket);
        console.log(`subtotal : ${this.basketTotalSourse.value?.subtotal}`); /////
        console.log(`total : ${this.basketTotalSourse.value?.total}`); /////
      },
      (error) => {
        console.log(error);
      }
    );
  }

  private calculateTotals() {
    const basket = this.getCurrentBasketValue();
    const shipping = 0;
    const subtotal =
      basket?.items.reduce(
        (sum, item) => sum + item.price * item.quantity,
        0
      ) || 0;
    const total = subtotal + shipping;
    this.basketTotalSourse.next({ shipping, subtotal, total });
  }

  incrementItemQuantity(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();

    if (basket) {
      const foundItemIndex = basket.items.findIndex((x) => x.id === item.id);

      if (foundItemIndex !== -1) {
        basket.items[foundItemIndex].quantity++;
      }

      this.setBasket(basket);
    }
  }

  decrementItemQuantity(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();

    if (basket) {
      const foundItemIndex = basket.items.findIndex((x) => x.id === item.id);

      if (foundItemIndex !== -1) {
        if (basket.items[foundItemIndex].quantity > 1) {
          basket.items[foundItemIndex].quantity--;
          this.setBasket(basket);
        } else {
          this.RemoveItemFromBasket(item);
        }
      }
    }
  }
  RemoveItemFromBasket(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();

    if (basket?.items.some((x) => x.id === item.id)) {
      basket.items = basket.items.filter((i) => i.id !== item.id);

      if (basket.items.length > 0) {
        this.setBasket(basket);
      } else {
        this.deleteBasket(basket);
      }
    }
  }
  deleteBasket(basket: IBasket) {
    return this.http.delete(this.baseUrl + 'basket?id=' + basket.id).subscribe(
      () => {
        this.basketSource.next(null);
        this.basketTotalSourse.next(null);
        localStorage.removeItem('basket_Id');
      },
      (error) => {
        console.log(error);
      }
    );
  }

  addItemToBasket(item: IProduct, quantity = 1) {
    const itemToAdd: IBasketItem = this.mapProductItemToBasketItem(
      item,
      quantity
    );
    let basket: IBasket | null;
    if (this.getCurrentBasketValue() === null) {
      basket = this.createBasket();
    } else {
      basket = this.getCurrentBasketValue();
    }

    basket!.items = this.addOrUpdateItem(basket!.items, itemToAdd, quantity);
    this.setBasket(basket!);
  }

  getCurrentBasketValue() {
    return this.basketSource.value;
  }

  private createBasket(): IBasket {
    console.log('creating a basket ..........');
    const basket = new Basket();
    localStorage.setItem('basket_Id', basket.id);

    return basket;
  }

  addOrUpdateItem(
    items: IBasketItem[],
    itemToAdd: IBasketItem,
    quantity: number
  ): IBasketItem[] {
    const index = items.findIndex((i) => i.id === itemToAdd.id);
    if (index === -1) {
      items.push(itemToAdd);
      itemToAdd.quantity = quantity;
    } else {
      items[index].quantity += quantity;
    }
    return items;
  }

  mapProductItemToBasketItem(item: IProduct, quantity: number): IBasketItem {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      pictureUrl: item.pictureUrl,
      quantity,
      brand: item.productBrand,
      type: item.productType,
    };
  }
}
