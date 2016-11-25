import { Component } from '@angular/core';

import { TMogSet } from '../../models/tmog-set';

@Component({
  moduleId: module.id,
  selector: 'tmog-set',
  templateUrl: 'set-detail.component.html',
  styleUrls: ['set-detail.component.css']
})
export class SetDetailComponent {
  public items: TMogSet[];
}
