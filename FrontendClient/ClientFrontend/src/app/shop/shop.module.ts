import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ProductItemComponent } from './product-item/product-item.component';

import { PaginationModule } from 'ngx-bootstrap/pagination';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    ShopComponent,
    ProductItemComponent,
    
    
  ],
  imports: [
    CommonModule,PaginationModule,SharedModule
  ],
  exports:[ShopComponent]
})
export class ShopModule { }
