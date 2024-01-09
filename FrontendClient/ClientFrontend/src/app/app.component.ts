import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IProduct } from './models/product';
import { IPagination } from './models/pagination';

@Component( {
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: [ './app.component.css' ]
} )
export class AppComponent implements OnInit {

  products: IProduct[] = []
  searchTerm: string = 'sam'; // Example random search term

  constructor ( private http: HttpClient ) { }


  ngOnInit (): void {

    this.http
    .get<IPagination>('https://localhost:7203/api/Product/Products')
    .subscribe(
  (response: IPagination) => {
    // Assuming 'data' property contains the array of products
    this.products = response.data;
    console.log(this.products); // Log or further process the products
  },
  (error) => {
    console.log(error);
  }
);
}}
