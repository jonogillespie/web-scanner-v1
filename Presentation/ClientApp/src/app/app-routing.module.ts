import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {ScansComponent} from './scans/scans.component';

const routes: Routes = [
  { path: '', component: ScansComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
