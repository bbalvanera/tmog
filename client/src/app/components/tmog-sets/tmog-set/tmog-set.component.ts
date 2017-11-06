import { Component, OnInit, ViewChild } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { NgbAccordion, NgbPanelChangeEvent } from '@ng-bootstrap/ng-bootstrap';

import { TmogSetsService } from '../tmog-sets.service';
import { TmogSet, Slot } from '../../../common/models';
import { dropLevelClassMap } from '../../../common/drop-level-class-map';

@Component({
  selector: 'app-tmog-set',
  templateUrl: 'tmog-set.component.html',
  styleUrls: ['tmog-set.component.scss'],
  providers: [TmogSetsService, Location]
})
export class TmogSetComponent implements OnInit {
  @ViewChild('slots') private slots: NgbAccordion;
  private slotChangingInternally = false;

  public model: TmogSet;

  constructor(private route: ActivatedRoute, private tmogSetsService: TmogSetsService, private location: Location) {
    this.model = {};
  }

  public ngOnInit(): void {
    this.route.params.forEach(params => {
      const id = +params['id'];
      this.tmogSetsService
        .getById(id)
        .then(result => {
          this.model = result;
        });
    });
  }

  public onSlotChange(event: NgbPanelChangeEvent): void {
    if (this.slotChangingInternally) {
      this.slotChangingInternally = false;
      return;
    }

    this.updateSlot(parseInt(event.panelId), !event.nextState);
    event.preventDefault();
  }

  public updateSlot(slotNumber: number, complete: boolean): void {
    const slot = this.getSlot(slotNumber);

    if (slot) {
      this.tmogSetsService.updateSetSlot(this.model.id, slot.slotName, complete)
        .then(updated => {
          if (updated) {
            slot.complete = complete;
            this.slotChangingInternally = true; /* when calling toggle, the change event gets fire. Prevent processing with this flag */
            this.slots.toggle(slot.slotNumber.toString());
            this.sort();
          }
        });
    }
  }

  public getIncompleteSlots(): string[] {
    if (this.model && this.model.slots) {
      return this.model.slots
        .filter(s => !s.complete)
        .map(s => s.slotNumber.toString());
    }

    return [];
  }

  public back(): boolean {
    this.location.back();
    return false;
  }

  public getDropLevelClass(dropLevel: number): string {
    return dropLevelClassMap.get(dropLevel) || 'q0';
  }

  private getSlot(slotNumber: number): Slot {
    if (this.model && this.model.slots) {
      for (const slot of this.model.slots) {
        if (slot.slotNumber === slotNumber) {
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
        } else if (left.complete > right.complete) {
          return 1;
        }

        // sort by name
        if (left.slotNumber < right.slotNumber) {
          return -1;
        } else if (left.slotNumber > right.slotNumber) {
          return 1;
        } else {
          return 0;
        }
      });
    }
  }
}
