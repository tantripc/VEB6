import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  username: string = '';
  password: string = '';

  constructor(private authService: AuthService) { }

  onSubmit() {
    this.authService.login(this.username, this.password).subscribe(
      (response) => {
        console.log('Login successful:', response);
        // Xử lý logic sau khi đăng nhập thành công
      },
      (error) => {
        console.error('Login failed:', error);
        // Xử lý lỗi đăng nhập
      }
    );
  }
}
