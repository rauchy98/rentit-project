import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { AppRoutingModule } from './app-routing.module';
import { Debounce } from 'angular2-debounce';

import { AppComponent } from './app.component';
import { AddProductPageComponent } from './add-product-page/add-product-page.component';
import { CommentsComponent } from './comments/comments.component';
import { MainPageComponent } from './main-page/main-page.component';
import { ProductCardComponent } from './product-card/product-card.component';
import { ProductPageComponent } from './product-page/product-page.component';
import { RegisterPageComponent } from './register-page/register-page.component';
import { SearchComponent } from './search/search.component';
import { SearchPageComponent } from './search-page/search-page.component';
import { SigninPageComponent } from './signin-page/signin-page.component';
import { CategoryListComponent } from './category-list/category-list.component';
import { OrdersPageComponent } from './orders-page/orders-page.component';

import { AuthGuard } from './_guards/index';
import { AuthenticationService, UserService, CategoryService, SearchService, CommentService, ProductService, OrderService } from './_services/index';

import { SimpleNotificationsModule } from 'angular2-notifications';
import { PickOrderDatePageComponent } from './pickorderdate-page/pickorderdate-page.component';
import { HttpErrorHandler } from 'app/_services/http-error.handler';
import { TokenProvider } from 'app/_services/token.provider';
import { CategoryPageComponent } from 'app/category-page/category-page.component';
import { FilterListComponent } from './filters-list/filters-list.component';
import { FilterService } from './_services/filter.service';
import { CharactericticService } from "app/_services/characteristic.service";

@NgModule({
  declarations: [
    AppComponent,
    AddProductPageComponent,
    CommentsComponent,
    MainPageComponent,
    ProductCardComponent,
    ProductPageComponent,
    RegisterPageComponent,
    SearchComponent,
    SearchPageComponent,
    SigninPageComponent,
    CategoryListComponent,
    OrdersPageComponent,
    Debounce,
    PickOrderDatePageComponent,
    CategoryPageComponent,
    FilterListComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    AppRoutingModule,
    SimpleNotificationsModule.forRoot(),
  ],
  providers: [ProductService,        
              AuthGuard,
              AuthenticationService,
              UserService,
              CategoryService,
              SearchService,
              CommentService,
              OrderService,
              HttpErrorHandler,
              TokenProvider,
              FilterService,
              CharactericticService ],
  bootstrap: [AppComponent]
})
export class AppModule { }
