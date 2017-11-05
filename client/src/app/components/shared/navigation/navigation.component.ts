import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navigation',
  templateUrl: 'navigation.component.html',
  styleUrls: ['navigation.component.scss']
})
export class NavigationComponent {
    public model = {
      searchTerm: ''
    };

    constructor(private router: Router) {
    }

    public performSearch(): void {
        if (this.model.searchTerm.length > 0) {
            this.router.navigate(['/search'], { queryParams: { q: this.model.searchTerm } });
        }
    }
}
