import { Component, OnInit, OnDestroy, ViewChildren } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { NgbAccordion, NgbPanelChangeEvent } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import { Subscription } from 'rxjs/Subscription';

import { WowheadTooltipService } from '../../../services/wowhead-tooltip.service';
import { ItemsService } from '../items.service';
import { Region } from '../../../common/models';
import { dropLevelClassMap } from '../../../common/drop-level-class-map';

@Component({
  selector: 'app-tracking-gear',
  templateUrl: './gear.component.html',
  styleUrls: ['./gear.component.scss'],
  providers: [ItemsService, Location]
})
export class GearComponent implements OnInit, OnDestroy {
  private setId: number;
  private subscriptions = <Subscription[]>[];
  private fetchingData = false; // used to prevent accordion collapse by signaling the event handler and preventingDefault
  @ViewChildren(NgbAccordion) private accordions: NgbAccordion[];

  public hasRegions = new Subject<boolean>();
  public regions = <Region[]>[];

  constructor(
    private itemsService: ItemsService,
    private wowheadTooltipService: WowheadTooltipService,
    private activatedRoute: ActivatedRoute,
    private location: Location) {
  }

  ngOnInit() {
    const validParams = ['setid', 'zoneid', 'regionid', ''];

    this.subscriptions.push(
      this.activatedRoute.queryParams
        .filter(params => {
          const param = (Object.keys(params)[0] || '').toLowerCase();
          return validParams.some(valid => param === valid);
        }).flatMap(param => {
          this.fetchingData = true;

          const filter = Object.keys(param)[0];
          const value  = Object.values(param)[0];

          return this.itemsService.all(filter, value);
        }).subscribe(regions => {
          this.regions = regions;
          this.hasRegions.next(regions && regions.length > 0);

          this.fetchingData = false;
        })
    )
  }

  ngOnDestroy() {
    this.subscriptions.forEach(subscription => {
      subscription.unsubscribe();
      subscription = null;
    });

    this.subscriptions = [];
    this.hasRegions.complete();
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

  public getZoneDifficultyClass(difficulty: number): string {
    return dropLevelClassMap.get(difficulty) || 'q0';
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

  public onPanelChange($event: NgbPanelChangeEvent) {
    this.fetchingData ? $event.preventDefault() : void 0;
  }

  public goBack(): boolean {
    this.location.back();
    return false;
  }
}
