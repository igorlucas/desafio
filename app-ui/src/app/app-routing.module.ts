import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ResourcelistComponent } from './pages/resources/resourcelist/resourcelist.component';
import { HomeComponent } from './pages/home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'resources/list', component: ResourcelistComponent }
  // { path: 'about', component: AboutComponent },
  // { path: 'contact', component: ContactComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
