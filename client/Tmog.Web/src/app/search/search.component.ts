import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';

import { WowheadTooltipService } from '../core/services/wowhead-tooltip.service';
import { Item } from '../core/models';
import { SearchService } from './search.service';

@Component({
    moduleId: module.id,
    selector: 'tmog-search',
    templateUrl: 'search.component.html',
    styleUrls: ['search.component.css'],
    providers: [SearchService]
})
export class SearchComponent implements OnInit, OnDestroy {
    private queryStringSubscription: Subscription;

    public model: Item[];
    public searchTerm: string;

    constructor(
        private route: ActivatedRoute,
        private wowheadTooltipService: WowheadTooltipService,
        private searchService: SearchService) {
        this.model = [];
    }

    public ngOnInit(): void {
        this.queryStringSubscription = this.route.queryParams.subscribe((params: any) => {
            this.searchTerm = params.q;
            this.performSearch();
        });
    }

    public ngOnDestroy(): void {
        if (this.queryStringSubscription) {
            this.queryStringSubscription.unsubscribe();
        }
    }

    public performSearch(): void {
        if (this.searchTerm && this.searchTerm.length > 2) {
            this.searchService.performSearch(this.searchTerm)
                .then(results => this.model = results);
        }
    }

    public dismissTooltip(): void {
        this.wowheadTooltipService.dismissTooltip();
    }

    public updateSlot(setId: number, slotName: string, complete: boolean): void {
        // const slot = this.getSlot(slotName);

        // if (slot) {
        //    this.tmogSetsService.updateSetSlot(this.model.id, slot.name, complete)
        //        .then(updated => {
        //            if (updated) {
        //                slot.complete = complete;
        //            }
        //        });
        // }
    }



    // private getSlot(slotName: string): Slot {
    //    if (this.model && this.model.slots) {
    //        for (const slot of this.model.slots) {
    //            if (slot.name === slotName) {
    //                return slot;
    //            }
    //        }
    //    }

    //    return null;
    // }
}

