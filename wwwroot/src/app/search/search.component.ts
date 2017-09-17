import { Component, OnInit } from '@angular/core';
 import { Router } from '@angular/router';
 import { Location } from '@angular/common';
 import { SearchService } from "app/_services";
 import { Category, Product, AvailableStatus } from "app/_models";

@Component({
  selector: 'search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  query : string = "";
  condition = true;

    AvailableStatus : typeof AvailableStatus = AvailableStatus;

  categories: Category[] = []; 
  products: Product[] = [];
  noResults = false;
  
  constructor(location: Location, 
  private router: Router,
  private searchService : SearchService) {
    router.events.forEach((event) => {
      if (location.prepareExternalUrl(location.path()) == '/main') {
        this.condition = false;
      } else {
        this.condition = true;
      }
    });

  }

  ngOnInit() {
  }

  onChange() {
    if(this.query.length >= 3) {
      this.searchService.search(this.query)
        .subscribe(result => { 
          this.categories = result.categories; 
          this.products = result.products;
          if(this.products.length == 0 && this.categories.length == 0) {
            this.noResults = true;
          } else {
            this.noResults = false;
          }
        })
    } else {
      this.categories = [];
      this.products = [];
      this.noResults = false;
    }
  }

  clearInput() {
    this.query = "";
    this.categories = [];
    this.products = [];
    this.noResults = false;
  }

}
