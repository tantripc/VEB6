import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { CpComponent } from './pages/cp/cp.component';
import { AccessDeniedComponent } from './pages/access-denied/access-denied.component';
import { AdminGuard } from './guards/admin.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: 'cp',
    component: CpComponent,
    canActivate: [AdminGuard], // Thêm guard tại đây
  },
  { path: 'accessDenied', component: AccessDeniedComponent }, // Route cho accessDenied
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
