import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ShopComponent } from './shop.component';
import { ProductItemComponent } from './product-item/product-item.component';

@NgModule({
  declarations: [ShopComponent, ProductItemComponent],
  imports: [CommonModule],
  exports: [ShopComponent],
})
export class ShopModule {}
