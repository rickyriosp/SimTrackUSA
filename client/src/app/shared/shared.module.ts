import { PaginationModule } from 'ngx-bootstrap/pagination';

import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

@NgModule({
  declarations: [],
  imports: [CommonModule, PaginationModule.forRoot()],
  exports: [PaginationModule],
})
export class SharedModule {}
