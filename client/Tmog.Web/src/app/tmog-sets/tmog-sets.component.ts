import { Component, OnInit } from '@angular/core';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

import { WowheadTooltipService } from '../core/services/wowhead-tooltip.service';
import { TMogSetsService } from './tmog-sets.service';
import { TMogSetsCacheService } from './tmog-sets-cache.service';
import { TMogSet } from '../core/models';
import { TMogSetAddComponent } from './tmog-set-add/tmog-set-add.component';

@Component({
    moduleId: module.id,
    selector: 'tmog-sets',
    templateUrl: 'tmog-sets.component.html',
    styleUrls: ['tmog-sets.component.css'],
    providers: [TMogSetsService, TMogSetsCacheService]
})
export class TMogSetsComponent implements OnInit {
    public tmogSets: TMogSet[];
    public error: { shortText: string, message: string };

    constructor(
        private tmogSetsService: TMogSetsService,
        private wowheadTooltipService: WowheadTooltipService,
        private modalService: NgbModal) { }

    public ngOnInit(): void {
        this.populateSets();
    }

    public newSet(): void {
        const options: NgbModalOptions = {
            size: 'sm'
        };

        this.modalService.open(TMogSetAddComponent, options).result.then(result => {
            if (result.ok) {
                this.populateSets();
            }
        });
    }

    public removeSet(id: number): void {
        this.tmogSetsService.removeSet(id)
            .then(proceed => {
                if (proceed) {
                    this.populateSets();
                }
            });
    }

    public isSetComplete(tmogSet: TMogSet): boolean {
        return tmogSet.completedSlots === tmogSet.totalSlots;
    }

    public isSetAlmostComplete(tmogSet: TMogSet): boolean {
        const missingSlots = tmogSet.totalSlots - tmogSet.completedSlots;

        return missingSlots > 0 && missingSlots <= 4;
    }

    public dismissTooltip(): void {
        this.wowheadTooltipService.dismissTooltip();
    }

    private populateSets(): void {
        this.tmogSetsService
            .getAll()
            .then(results => {
                this.tmogSets = results;
            }, reason => {
                if (reason.status === 500) {
                    this.error = {
                        shortText: 'Our apologies :(',
                        message: 'We tried. We really did. But looks like something is wrong with our server. We\'ll fix it. We promise.'
                    };
                }
                else {
                    this.error = {
                        shortText: 'Oh Snap!',
                        message: 'Looks like something went really really wrong. We\'ll fix it. We promise.'
                    };
                }
            });
    }
}
