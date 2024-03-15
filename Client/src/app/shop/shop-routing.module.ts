import { NgModule } from '@angular/core';
import { ShopComponent } from './shop.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { RouterModule, Routes } from '@angular/router';


const routes: Routes = [

  { path: '', component: ShopComponent  },
  //{ path: 'shop', component: ShopComponent },
  //we dont need this,
  //because it is the root of shopModule component
  { path: ':id', component: ProductDetailsComponent ,data:{breadcrumb:{alias:'productDetails'}}}
];

@NgModule( {
  declarations: [],
  imports: [
   // CommonModule ,// we dont need these to be available in app module, but only in shopModule
   RouterModule.forChild(routes)
  ],
  exports:[RouterModule]
} )
export class ShopRoutingModule { }
