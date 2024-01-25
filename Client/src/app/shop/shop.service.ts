import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { IPagination } from '../shared/models/pagination';
import { IProductBrand } from '../shared/models/productBrand';
import { IProductType } from '../shared/models/productType';
import { map } from 'rxjs/operators';
import { ShopParams } from '../shared/models/shopParams';
import { IProduct } from '../shared/models/product';

@Injectable( {
  providedIn: 'root'
} )
export class ShopService {

  baseUrl = 'https://localhost:7203/api/';

  constructor ( private http: HttpClient ) { }

  getProducts ( shopParams:ShopParams ) {
    let params = new HttpParams();

    if ( shopParams.brandId !==0 ) {
      params = params.append( 'brandId', shopParams.brandId.toString() );
    }
    if ( shopParams.typeId !==0) {
      params = params.append( 'typeId', shopParams.typeId.toString() );
    }
    if ( shopParams.search ) {
      params = params.append( 'search', shopParams.search.toString() );
    }
    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber);
    params = params.append('pageSize', shopParams.pageSize);
    


    return this.http
      .get<IPagination>( this.baseUrl + 'Product/Products', { observe: 'response', params } )
      .pipe(
        map( ( response ) => {
          return response.body;
        } )
      );
  }
  getBrands () {

    return this.http
      .get<IProductBrand[]>( this.baseUrl + 'Product/Brands' );
  }
  getTypes () {

    return this.http
      .get<IProductType[]>( this.baseUrl + 'Product/Types' );
  }

  getSingleProduct(id:number){
    return this.http
    .get<IProduct>(this.baseUrl + 'Product/'+ id);
  }

}