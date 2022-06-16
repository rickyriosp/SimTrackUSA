import { PaginationModule } from 'ngx-bootstrap/pagination';

import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { PagingHeaderComponent } from './components/paging-header/paging-header.component';

@NgModule({
  declarations: [PagingHeaderComponent],
  imports: [CommonModule, PaginationModule.forRoot()],
  exports: [PaginationModule, PagingHeaderComponent],
})
export class SharedModule {}
