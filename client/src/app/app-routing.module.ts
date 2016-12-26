import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DashboardComponent } from './dashboard/dashboard.component';
import { QuestsComponent } from './quests/quests.component';
import { TMogSetsComponent } from './tmog-sets/tmog-sets.component';
import { TMogSetComponent } from './tmog-sets/tmog-set/tmog-set.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'quests', component: QuestsComponent },
  { path: 'tmog-sets', component: TMogSetsComponent },
  { path: 'tmog-set/:id', component: TMogSetComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

}
