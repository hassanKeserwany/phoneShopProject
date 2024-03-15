import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/models/basket';
import { IUser } from 'src/app/shared/models/user';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css'],
})
export class NavBarComponent {
  currentUser$!: Observable<IUser>;

  basket$!: Observable<IBasket | null>;
  basketLength: number = 0;

  // ...
  constructor(
    private basketService: BasketService,
    private accountService: AccountService
  ) {}

  ngOnInit() {
    this.currentUser$=this.accountService.currentUser$;

    this.basket$ = this.basketService.basket$;
    this.getBasketLength();
  }

logout(){
  this.accountService.logout();
}



  getBasketLength() {
    this.basket$.subscribe((basket) => {
      if (basket && basket.items) {
        this.basketLength = (basket as IBasket).items.length;
      } else {
        this.basketLength = 0;
      }
    });
  }
}
