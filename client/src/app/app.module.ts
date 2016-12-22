import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { NavigationComponent } from './shared/navigation/navigation.component';
import { UiSwitchComponent } from './shared/ui-switch/ui-switch.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { QuestsComponent } from './quests/quests.component';
import { components as TMogComponents } from './tmog-sets/components';
import { TMogSetAddComponent } from './tmog-sets/tmog-set-add/tmog-set-add.component';

import './rxjs-extensions';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        AppRoutingModule,
        NgbModule.forRoot()
    ],
    declarations: [
        AppComponent,
        NavigationComponent,
        UiSwitchComponent,
        DashboardComponent,
        QuestsComponent,
        ...TMogComponents,
    ],
    entryComponents: [TMogSetAddComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }
