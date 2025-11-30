import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UpperCasePipe } from '../../../../pipes/upper-case.pipe';
import { CurrencyVNPipe } from '../../../../pipes/currency-vn.pipe';
import { NgFor } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ProductItems } from '../../../../types/productItem';

@Component({
  selector: 'app-product-item',
  standalone: true,
  imports: [FormsModule, UpperCasePipe, CurrencyVNPipe, NgFor, RouterLink],
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.css'
})
export class ProductItemComponent {
  @Input() products: ProductItems[] = [];
  // Truyền data từ con lên cha
  @Output() dataEvent = new EventEmitter<number>();

  // Xử lý sự kiện click delete
  handleDelete = (id: number) => {
    this.dataEvent.emit(id);
  }

  // Getter
  get totalPrice(): string {
    const sum = this.products.reduce((total, item) => { return total + item.price }, 0);
    return `Total price: ${new CurrencyVNPipe().transform(sum)}`;
  }
}
