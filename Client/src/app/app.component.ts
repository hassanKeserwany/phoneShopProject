import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';
import { AccountService } from './account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'Client';

  constructor(
    private basketService: BasketService,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.loadBasket();
    this.loadUser();
  }

  loadBasket() {
    const basketId = localStorage.getItem('basket_Id');
    if (basketId) {
      this.basketService.getBasket(basketId).subscribe(
        () => {
          console.log('initialized basket');
          console.log(`basketService.basket$: ${this.basketService.basket$}`);
        },
        (error) => {
          console.log(error);
        }
      );
    }
  }

  loadUser() {
    const userToken = localStorage.getItem('token');
    console.log(`token is: ${userToken}`);

    if (userToken) {
      this.accountService.loadCurrentUser(userToken).subscribe(
        () => {
          console.log('loaded user');
          console.log(
            `accountservice.user$: ${this.accountService.currentUser$}`
          );
        },
        (error) => {
          console.log(error);
        }
      );
    }
  }
}
