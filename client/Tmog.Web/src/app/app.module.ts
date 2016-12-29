import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { LocalStorageModule } from 'angular-2-local-storage';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { services as Services } from './core/services/services';
import { NavigationComponent } from './shared/navigation/navigation.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { QuestsComponent } from './quests/quests.component';
import { components as TMogComponents } from './tmog-sets/components';
import { TMogSetAddComponent } from './tmog-sets/tmog-set-add/tmog-set-add.component';
import { SearchComponent } from './search/search.component';

import './rxjs-extensions';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        AppRoutingModule,
        NgbModule.forRoot(),
        LocalStorageModule.withConfig({ prefix: 'tmog', storageType: 'localStorage' })
    ],
    declarations: [
        AppComponent,
        NavigationComponent,
        DashboardComponent,
        QuestsComponent,
        ...TMogComponents,
        SearchComponent,
    ],
    entryComponents: [TMogSetAddComponent],
    providers: [
        ...Services
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
