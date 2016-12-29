import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { TMogSetsService } from '../tmog-sets.service';
import { TMogSetsCacheService } from '../tmog-sets-cache.service';
import { TMogSet, Slot } from '../../core/models';

@Component({
  moduleId: module.id,
  selector: 'tmog-set',
  templateUrl: 'tmog-set.component.html',
  styleUrls: ['tmog-set.component.css'],
  providers: [TMogSetsService, TMogSetsCacheService]
})
export class TMogSetComponent implements OnInit {

    public model: TMogSet;

    constructor(private route: ActivatedRoute, private tmogSetsService: TMogSetsService) {
        this.model = {};
    }

    public ngOnInit(): void {
        this.route.params.forEach(params => {
            let id = +params['id'];
            this.tmogSetsService
                .getById(id)
                .then(result => {
                    this.model = result;
                });
        });
    }

    public updateSlot(slotName: string, complete: boolean): void {
        const slot = this.getSlot(slotName);

        if (slot) {
            this.tmogSetsService.updateSetSlot(this.model.id, slot.name, complete)
                .then(updated => {
                    if (updated) {
                        slot.complete = complete;
                    }
                });
        }
    }

    private getSlot(slotName: string): Slot {
        if (this.model && this.model.slots) {
            for (const slot of this.model.slots) {
                if (slot.name === slotName) {
                    return slot;
                }
            }
        }

        return null;
    }
}
