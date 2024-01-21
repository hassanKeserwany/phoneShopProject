import { Component, Input, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';

@Component( {
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: [ './product-details.component.css' ]
} )
export class ProductDetailsComponent implements OnInit {
  product!: IProduct;

  constructor ( private shopService: ShopService, private activeRoute: ActivatedRoute ) { }
  //ActivatedRoute used to get the {id} from the root url
  ngOnInit (): void {
    this.loadProduct();
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
            console.log(this.product);
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
