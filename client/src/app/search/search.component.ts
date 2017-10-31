import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

import { WowheadTooltipService } from '../core/services/wowhead-tooltip.service';
import { Item } from '../core/models';
import { SearchService } from './search.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
  providers: [SearchService]
})
export class SearchComponent implements OnInit, OnDestroy {
  private querySubscription: Subscription;

  public model: Item[];
  public searchTerm: string;

  constructor(
    private route: ActivatedRoute,
    private wowheadTooltipService: WowheadTooltipService,
    private searchService: SearchService) {
    this.model = [];
  }

  ngOnInit(): void {
    this.querySubscription = this.route.queryParams.subscribe((params) => {
      this.searchTerm = params['q'];
      this.performSearch(this.searchTerm);
    });
  }

  public ngOnDestroy(): void {
    if (this.querySubscription) {
      this.querySubscription.unsubscribe();
    }
  }

  public performSearch(search: string): void {
    if (search && search.length > 2) {
        this.searchService.performSearch(search)
            .then(results => this.model = results);
            // TODO: add catch here.
    }
  }
}
