import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { SearchPageComponent } from './search-page/search-page.component';
import { ProductPageComponent } from './product-page/product-page.component';
import { MainPageComponent } from './main-page/main-page.component';
import { SigninPageComponent } from './signin-page/signin-page.component';
import { RegisterPageComponent } from './register-page/register-page.component';
import { AddProductPageComponent } from './add-product-page/add-product-page.component';
import { OrdersPageComponent } from './orders-page/orders-page.component';
import { AuthGuard } from "app/_guards";
import { CategoryPageComponent } from "app/category-page/category-page.component";

const routes: Routes = [
  { path: '', redirectTo: '/main', pathMatch: 'full' },
  { path: 'search',  component: SearchPageComponent },
  { path: 'category/:id',  component: CategoryPageComponent },
  { path: 'product/:id', component: ProductPageComponent },
  { path: 'main', component: MainPageComponent },
  { path: 'signin', component: SigninPageComponent },
  { path: 'register', component: RegisterPageComponent },
  { path: 'add-product', component: AddProductPageComponent, canActivate: [AuthGuard] },
  { path: 'orders', component: OrdersPageComponent, canActivate: [AuthGuard] },
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {

}
