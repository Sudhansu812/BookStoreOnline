import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CartTableComponent } from './Components/cart-table/cart-table.component';
import { CategoriesComponent } from './Components/categories/categories.component';
import { HomeComponent } from './Components/home/home.component';
import { LogInComponent } from './Components/log-in/log-in.component';
import { UploadComponent } from './Components/upload/upload.component';
import { AuthGuardService } from './services/auth-guard.service';

const routes: Routes = [
  { path: '', component: HomeComponent},
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LogInComponent },
  { path: 'upload', component: UploadComponent, canActivate: [AuthGuardService] },
  { path: 'categories', component: CategoriesComponent },
  { path: 'cart', component: CartTableComponent, canActivate: [AuthGuardService] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
