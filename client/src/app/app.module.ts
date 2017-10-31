import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { Services } from './core/services';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NavigationComponent } from './shared/navigation/navigation.component';
import { SearchComponent } from './search/search.component';
import { TmogSetsComponent } from './tmog-sets/tmog-sets.component';
import { TmogSetAddComponent } from './tmog-sets/tmog-set-add/tmog-set-add.component';
import { TmogSetComponent } from './tmog-sets/tmog-set/tmog-set.component';
import { TmogSetItemsComponent } from './tmog-sets/tmog-set/tmog-set-items/tmog-set-items.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    NavigationComponent,
    SearchComponent,
    TmogSetsComponent,
    TmogSetAddComponent,
    TmogSetComponent,
    TmogSetItemsComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    FormsModule,
    HttpModule,
    NgbModule.forRoot()
  ],
  entryComponents: [TmogSetAddComponent],
  providers: [...Services],
  bootstrap: [AppComponent]
})
export class AppModule { }
