import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { CoreModule } from './core/core.module';
import { HomeModule } from './home/home.module';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BasketModule } from './basket/basket.module';
import { AccountModule } from './account/account.module';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@NgModule( {
  declarations: [
    AppComponent,
  ],
  imports: [
    CommonModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CoreModule,
    //ShopModule, .. no longer need to add it here(because of lazy loading)
    HomeModule,
    BasketModule,
    AccountModule,
    FormsModule,
    BsDropdownModule.forRoot(),
  ],
  providers: [],
  bootstrap: [ AppComponent ]
} )
export class AppModule { }
