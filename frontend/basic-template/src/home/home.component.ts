import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ProductItemComponent } from "../app/shared/product/product-item/product-item.component";
import { ProductItems } from '../types/productItem';
import { BlogService } from '../services/BlogService';
import { map, Subscription } from 'rxjs';
@Component({
  selector: 'app-home',
  standalone: true,
  imports: [FormsModule, ProductItemComponent],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy {
  getAPIBlogs: Subscription;

  constructor(private blogService: BlogService) {
    this.getAPIBlogs = new Subscription();
  }
  ngOnInit(): void {
    // Example of HTTP GET request
    this.getAPIBlogs = this.blogService.getBlogs()
      .pipe(map(({ data }) => data.map((item: any, index: number) => {
        return { ...item, id: ++index, name: item.title, price: parseInt(item.body) }
      })
      ))
      .subscribe((res) => {
        this.products = res;
      });
  }
  ngOnDestroy(): void {
    if (this.getAPIBlogs)
      this.getAPIBlogs.unsubscribe();
  }

  isVisible: boolean = false;
  products: ProductItems[] = [
    { id: 1, name: 'Sản phẩm 1', price: 100000, isVisible: true },
    { id: 2, name: 'Sản phẩm 2', price: 120000, isVisible: false },
    { id: 3, name: 'Sản phẩm 3', price: 1250000, isVisible: true },
    { id: 4, name: 'Sản phẩm 4', price: 200000, isVisible: false },
  ];

  handleDelete = (id: number) => {
    console.log('ID sản phẩm cần xóa:', id);
    this.products = this.products.filter(product => product.id !== id);
  }
}
