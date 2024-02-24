import { Component,OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  title = 'Client';

  constructor(private basketService:BasketService){}

  ngOnInit():void {
    const basketId =localStorage.getItem('basket_Id');
    if(basketId){
      this.basketService.getBasket(basketId).subscribe(()=>{
        console.log("initialized basket");
        console.log(`basketService.basket$: ${this.basketService.basket$}`)
      },error=>{
        console.log(error);
      }
      )
    }

  }

}
