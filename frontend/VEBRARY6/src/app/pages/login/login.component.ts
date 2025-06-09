import { Component } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email = '';
  password = '';

  onSubmit() {
    if (this.email === 'admin@example.com' && this.password === '123456') {
      alert('Đăng nhập thành công!');
    } else {
      alert('Sai tài khoản hoặc mật khẩu.');
    }
  }
}
