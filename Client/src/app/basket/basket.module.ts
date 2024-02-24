import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BasketRoutingModule } from './basket-routing.module';
import { BasketComponent } from './basket.component';
import { BrowserModule } from '@angular/platform-browser';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    BasketComponent,
  ],
  imports: [
    CommonModule,
    BasketRoutingModule,
    SharedModule
  ],
exports:[BasketComponent]
})
export class BasketModule { }
