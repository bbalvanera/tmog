import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { TmogSetsService } from '../tmog-sets.service';
import { TmogSet, Slot } from '../../core/models';

@Component({
  moduleId: module.id,
  selector: 'tmog-set',
  templateUrl: 'tmog-set.component.html',
  styleUrls: ['tmog-set.component.scss'],
  providers: [TmogSetsService]
})
export class TmogSetComponent implements OnInit {

    public model: TmogSet;

    constructor(private route: ActivatedRoute, private tmogSetsService: TmogSetsService) {
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
                        this.sort();
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

    private sort(): void {
        if (this.model.slots && this.model.slots.length > 0) {
            this.model.slots = this.model.slots.sort((left: Slot, right: Slot): number => {
                // sort by complete in descending order
                if (left.complete < right.complete) {
                    return -1;
                }
                else if (left.complete > right.complete) {
                    return 1;
                }

                // sort by name
                if (left.name < right.name) {
                    return -1;
                }
                else if (left.name > right.name) {
                    return 1;
                }
                else {
                    return 0;
                }
            });
        }
    }
}
