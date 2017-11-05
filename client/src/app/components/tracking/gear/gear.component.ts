import { Component, OnInit, OnDestroy, ViewChildren } from '@angular/core';
import { NgbAccordion } from '@ng-bootstrap/ng-bootstrap';
import { Subject } from 'rxjs/Subject';

import { WowheadTooltipService } from '../../../services/wowhead-tooltip.service';
import { ItemsByZoneService } from '../items-by-zone.service';
import { Region } from '../../../core/models';

@Component({
  selector: 'app-gear',
  templateUrl: './gear.component.html',
  styleUrls: ['./gear.component.scss'],
  providers: [ItemsByZoneService]
})
export class GearComponent implements OnInit, OnDestroy {
  private unsubscribe = new Subject<void>();
  @ViewChildren(NgbAccordion) private accordions: NgbAccordion[];

  public regions: Region[];

  constructor(private itemsService: ItemsByZoneService, private wowheadTooltipService: WowheadTooltipService) { }

  ngOnInit() {
    this.itemsService.all()
      .takeUntil(this.unsubscribe)
      .subscribe((regions) => this.regions = regions);
  }

  ngOnDestroy() {
    this.unsubscribe.next();
    this.unsubscribe.complete();
  }

  public getZoneDifficultyName(difficulty: number): string {
    switch (difficulty) {
      case -1: return 'Normal';
      case -2: return 'Heroic';
      case 0: return 'Unknown';
      case 1: return '10 Normal';
      case 2: return '25 Normal';
      case 3: return '10 Heroic';
      case 4: return '25 Heroic';
    }
  }

  public getZoneDifficultyClasses(difficulty: number): {} {
    return {
      'q0': difficulty === 0,
      'q3': difficulty === -1,
      'q4': difficulty === -2,
      'q5': difficulty === 1 || difficulty === 2,
      'q10': difficulty === 3 || difficulty === 4,
    };
  }

  public getAllDOMIds(items: { id: number }[], prefix: string): string {
    if (!items || items.length === 0) {
      return '';
    }

    const ids = items.map(item => `${prefix}-${item.id}`);
    return ids.join(',');
  }

  public getDOMId(item: { id: number }, prefix: string): string {
    return `${prefix}-${item.id}`;
  }

  public dismissTooltip(event: Event, id: number): boolean {
    this.wowheadTooltipService.dismissTooltip();
    return false;
  }

  public collapseAllPanels(): void {
    this.accordions.forEach(accordion => {
      accordion.panels.forEach(panel => panel.isOpen = false);
      accordion.activeIds = '';
    });
  }

  public expandAllPanels(): void {
    this.accordions.forEach(accordion => {
      const allIds: string[] = [];
      accordion.panels.forEach(panel => {
        panel.isOpen = true;
        allIds.push(panel.id);
      });
      accordion.activeIds = allIds;
    });
  }
}
