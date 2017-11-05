import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { Services } from './services';
import { Settings } from './core/settings';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { NavigationComponent } from './components/shared/navigation/navigation.component';
import { SearchComponent } from './components/search/search.component';
import { TmogSetsComponent } from './components/tmog-sets/tmog-sets.component';
import { TmogSetAddComponent } from './components/tmog-sets/tmog-set-add/tmog-set-add.component';
import { TmogSetComponent } from './components/tmog-sets/tmog-set/tmog-set.component';
import { GearComponent } from './components/tracking/gear/gear.component';

import './rxjs-extensions';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    NavigationComponent,
    SearchComponent,
    TmogSetsComponent,
    TmogSetAddComponent,
    TmogSetComponent,
    GearComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    FormsModule,
    HttpModule,
    NgbModule.forRoot()
  ],
  entryComponents: [TmogSetAddComponent],
  providers: [...Services, Settings],
  bootstrap: [AppComponent]
})
export class AppModule { }
