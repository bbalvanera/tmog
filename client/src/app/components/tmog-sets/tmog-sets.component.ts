import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

import { WowheadTooltipService } from '../../services/wowhead-tooltip.service';
import { TmogSetsService } from './tmog-sets.service';
import { TmogSet } from '../../core/models';
import { TmogSetAddComponent } from './tmog-set-add/tmog-set-add.component';

@Component({
    selector: 'app-tmog-sets',
    templateUrl: 'tmog-sets.component.html',
    styleUrls: ['tmog-sets.component.scss'],
    providers: [TmogSetsService]
})
export class TmogSetsComponent implements OnInit {
    public tmogSets: TmogSet[];
    public error: { shortText: string, message: string };

    constructor(
        private tmogSetsService: TmogSetsService,
        private wowheadTooltipService: WowheadTooltipService,
        private router: Router,
        private modalService: NgbModal) { }

    public ngOnInit(): void {
        this.populateSets();
    }

    public newSet(): void {
        const options: NgbModalOptions = {
            size: 'sm'
        };

        this.modalService.open(TmogSetAddComponent, options).result.then(result => {
            if (result.ok) {
                this.populateSets();
            }
        }, reason => {
            // don't care about cancel.
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

    public isSetComplete(tmogSet: TmogSet): boolean {
        return tmogSet.completedSlots === tmogSet.totalSlots;
    }

    public isSetAlmostComplete(tmogSet: TmogSet): boolean {
        try {
            return (tmogSet.completedSlots / tmogSet.totalSlots) >= 0.5 && !this.isSetComplete(tmogSet);
        } catch (e) {
            return false;
        }
    }

    public dismissTooltip(event: Event, id: number): boolean {
        this.wowheadTooltipService.dismissTooltip();
        return false;
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
                } else {
                    this.error = {
                        shortText: 'Oh Snap!',
                        message: 'Looks like something went really really wrong. We\'ll fix it. We promise.'
                    };
                }
            });
    }
}
