import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'currencyVN',
  standalone: true
})
export class CurrencyVNPipe implements PipeTransform {

  transform(value: number): string {
    return new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(value);
  }

}
