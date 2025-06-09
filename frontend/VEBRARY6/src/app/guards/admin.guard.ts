import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AdminGuard implements CanActivate {
  constructor(private router: Router) { }

  canActivate(): boolean {
    const isAdmin = this.checkAdminRights(); // Kiểm tra quyền quản trị
    if (!isAdmin) {
      this.router.navigate(['/accessDenied']); // Chuyển hướng đến trang accessDenied
      return false;
    }
    return true;
  }

  private checkAdminRights(): boolean {
    // Thay thế bằng logic kiểm tra quyền thực tế (ví dụ: kiểm tra token hoặc role)
    const user = JSON.parse(localStorage.getItem('user') || '{}');
    return user && user.role === 'admin';
  }
}
