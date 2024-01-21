import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { ShopService } from './shop.service';
import { IPagination } from '../shared/models/pagination';
import { IProductType } from '../shared/models/productType';
import { IProductBrand } from '../shared/models/productBrand';
import { ShopParams } from '../shared/models/shopParams';
@Component( {
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: [ './shop.component.css' ]
} )
export class ShopComponent implements OnInit {
  @ViewChild( 'search', { static: false } ) searchTerm!: ElementRef;



  products: IProduct[] = [];
  productBrands: IProductBrand[] = [];
  productTypes: IProductType[] = [];
  shopParams = new ShopParams();
  totalCounts: number = 0;

  sortOptions = [
    { name: "Alphabetical A to Z", value: "name" },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price:   High to Low', value: 'priceDesc' }
  ];



  constructor ( private shopService: ShopService ) { }
  ngOnInit (): void {

    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  //get products
  getProducts () {
    this.shopService.getProducts( this.shopParams ).subscribe(
      response => {
        if ( response ) {
          this.products = response.data;
          this.shopParams.pageNumber = response.pageIndex;
          this.shopParams.pageSize = response.pageSize;
          this.totalCounts = response.count;
        }

      },
      ( error ) => {
        console.log( error );
      }
    );
  }

  //get brands
  getBrands () {
    this.shopService.getBrands().subscribe(

      ( response ) => {
        this.productBrands = [ { id: 0, name: "All" }, ...response ];
      },
      ( error ) => {
        console.log( error );
      } );
  }

  //get types
  getTypes () {

    this.shopService.getTypes().subscribe(

      ( response ) => {
        this.productTypes = [ { id: 0, name: "All" }, ...response ];
      },
      ( error ) => {
        console.log( error );
      } );

  }

  onBrand ( brandId: number ) {
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber=1;
    this.getProducts();
  }
  onType ( typeId: number ) {
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber=1;
    this.getProducts();
  }
  onSort ( event: Event ) {
    const Value = ( event.target as HTMLSelectElement ).value;
    this.shopParams.sort = Value;
    this.getProducts();
  }

  onPageChanged ( event: any ) {

    if(this.shopParams.pageNumber !==event){
      this.shopParams.pageNumber = event;
    this.getProducts();
    }
  }


  onSearch () {
    // const params = this.shopService.getShopParams();
    // params.search = this.searchTerm?.nativeElement.value;
    // params.pageNumber = 1;
    // this.shopService.setShopParams(params);
    // this.shopParams = params;
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber=1;
    this.getProducts();
  }

  onReset () {
    if (this.searchTerm) this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    // this.shopService.setShopParams(this.shopParams);
    this.getProducts();
  }

 
}