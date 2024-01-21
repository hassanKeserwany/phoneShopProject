import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterModule, Routes } from '@angular/router';
import { ShopComponent } from './shop.component';
import { ProductDetailsComponent } from './product-details/product-details.component';

const routes: Routes = [

  { path: '', component: ShopComponent },
  //{ path: 'shop', component: ShopComponent },//we dont need this,
  //because it the root of shopModule component
  { path: 'shop/:id', component: ProductDetailsComponent }


];

@NgModule( {
  declarations: [],
  imports: [
   // CommonModule ,// we dont need these to be available in app module, but only in shopModule
   RouterModule.forChild(routes)
  ],exports:[RouterModule]
} )
export class ShopRoutingModule { }
