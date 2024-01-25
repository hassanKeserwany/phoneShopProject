import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ProductDetailsComponent } from './shop/product-details/product-details.component';



const routes: Routes = [
  { path: '', component: HomeComponent, data: { breadcrumb: 'home' } },
  {
    path: 'shop', loadChildren: () => import( "./shop/shop.module" ).then( mod => mod.ShopModule )
    , data: { breadcrumb: 'Shop' }
  },//to
  //let them load the children ===> lazy loading
  //so the shop module will only be activated and loaded only whenwe access the shop path
  { path: 'shop/:id', component: ProductDetailsComponent, },
  { path: 'home', component: HomeComponent, },
  { path: '**', redirectTo: '', pathMatch: 'full' },

];

@NgModule( {
  imports: [ RouterModule.forRoot( routes ) ],
  exports: [ RouterModule ]
} )
export class AppRoutingModule { }
