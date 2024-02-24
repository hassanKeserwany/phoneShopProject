import { Component, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasketItem } from 'src/app/shared/models/basket';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css'],
})
export class ProductDetailsComponent implements OnInit {
  product!: IProduct;
  quantity = 1;
  disabled = false;

  constructor(
    private shopService: ShopService,
    private activeRoute: ActivatedRoute,
    private bcService: BreadcrumbService,
    private basketService: BasketService
  ) {}
  //ActivatedRoute used to get the {id} from the root url
  ngOnInit(): void {
    this.loadProduct();
  }
  addItemToBasket() {
    this.basketService.addItemToBasket(this.product, this.quantity);
  }
  incrementQuantity() {
    this.disabled=false;
    this.quantity++;
  }
  decrementQuantity() {
    if (this.quantity > 1) {
      this.disabled=false;
      this.quantity--;
    } else {
      this.disabled = true;
    }
  }

  loadProduct() {
    const productIdParam = this.activeRoute.snapshot.paramMap.get('id');

    // Check if productIdParam is not null before trying to convert to number
    if (productIdParam !== null) {
      const productId = +productIdParam;

      // Check if productId is not NaN before making the service call
      if (!isNaN(productId)) {
        this.shopService.getSingleProduct(productId).subscribe(
          (response) => {
            this.product = response;
            this.bcService.set('@productDetails', this.product.name);
          },
          (error) => {
            console.log(error);
          }
        );
      } else {
        console.log('Product ID is not a valid number');
      }
    } else {
      console.log('Product ID is null');
    }
  }
}
