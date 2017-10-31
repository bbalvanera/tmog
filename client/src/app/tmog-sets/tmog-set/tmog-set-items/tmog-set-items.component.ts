import { Component, OnInit, Input } from '@angular/core';
import { Slot } from '../../../core/models';

@Component({
  selector: 'app-tmog-set-items',
  templateUrl: './tmog-set-items.component.html',
  styleUrls: ['./tmog-set-items.component.scss']
})
export class TmogSetItemsComponent implements OnInit {
  @Input()
  public slot: Slot;

  constructor() { }

  ngOnInit() {
  }
}
