import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DashboardComponent } from './components/dashboard/dashboard.component';
import { SearchComponent } from './components/search/search.component';
import { TmogSetsComponent } from './components/tmog-sets/tmog-sets.component';
import { TmogSetComponent } from './components/tmog-sets/tmog-set/tmog-set.component';
import { GearComponent } from './components/tracking/gear/gear.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'search', component: SearchComponent },
  { path: 'tmog-sets', component: TmogSetsComponent },
  { path: 'tmog-set/:id', component: TmogSetComponent },
  { path: 'tracking/gear', component: GearComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

}
