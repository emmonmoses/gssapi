import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './pages/login/login.component';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { TransferPageComponent } from './pages/transfer-page/transfer-page.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'index', component: LoginComponent },
  { path: 'transfer', component: TransferPageComponent },
  { path: '**', redirectTo: '/page-not-found', pathMatch: 'full' },
  { path: 'page-not-found', component: PageNotFoundComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
