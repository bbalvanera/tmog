import { Component } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  private routerSubscription: Subscription;

  public title = 'app';

  constructor(private router: Router) {
    this.routerSubscription = this.router.events
    .filter(e => e instanceof NavigationEnd)
    .pairwise()
    .subscribe((e) => console.log(e));
  }
}
