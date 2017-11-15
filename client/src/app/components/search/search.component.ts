import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';
import { Subject } from 'rxjs/Subject';

import { WowheadTooltipService } from '../../services/wowhead-tooltip.service';
import { Item } from '../../common/models';
import { SearchService } from './search.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss'],
  providers: [SearchService]
})
export class SearchComponent implements OnInit, OnDestroy {
  private querySubscription: Subscription;

  public model = <Item[]>[];
  public searchTerm = '';
  public hasSearchResults = new Subject<boolean>();

  constructor(
    private route: ActivatedRoute,
    private wowheadTooltipService: WowheadTooltipService,
    private searchService: SearchService) {

  }

  ngOnInit(): void {
    this.querySubscription = this.route.queryParams
      .map(params => {
        this.hasSearchResults.next(null);
        return params['q'];
      })
      .flatMap((query: string) => {
        this.searchTerm = query || '';
        return this.searchService.performSearch(query);
      })
      .subscribe(searchResults => {
        this.model = searchResults;
        const hasResults = this.model && this.model.length > 0;
        this.hasSearchResults.next(hasResults);
      });
  }

  public ngOnDestroy(): void {
    if (this.querySubscription) {
      this.querySubscription.unsubscribe();
    }

    this.hasSearchResults.complete();
  }
}
