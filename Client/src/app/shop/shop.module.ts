import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopRoutingModule } from './shop-routing.module';
import { ShopComponent } from './shop.component';
import { ProductItemComponent } from './product-item/product-item.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { SharedModule } from '../shared/shared.module';



@NgModule( {
  declarations: [
    ShopComponent,
    ProductItemComponent,
    ProductDetailsComponent,


  ],
  imports: [
    CommonModule, PaginationModule, SharedModule, ShopRoutingModule
  ],
  exports: [
    // ShopComponent  =>no need to export it (because of lazy loading)
  ]
} )
export class ShopModule { }
