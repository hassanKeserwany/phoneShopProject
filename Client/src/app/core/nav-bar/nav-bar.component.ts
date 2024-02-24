import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/models/basket';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent {
  basket$!: Observable<IBasket|null>;
  basketLength: number = 0;

  // ...
  constructor(private basketService:BasketService){}
  
  ngOnInit(){
    this.basket$=this.basketService.basket$;
  this.getBasketLength();


}

getBasketLength(){
  this.basket$.subscribe((basket) => {
    if (basket && basket.items) {
      this.basketLength = (basket as IBasket).items.length;
    } else {
      this.basketLength = 0;
    }
  });
}

}
