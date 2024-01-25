import { Component, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';


@Component( {
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: [ './product-details.component.css' ]
} )
export class ProductDetailsComponent implements OnInit {
  product!: IProduct;

  constructor ( private shopService: ShopService, private activeRoute: ActivatedRoute,
    private bcService:BreadcrumbService ) { }
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
            this.bcService.set('@productDetails',this.product.name)
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
