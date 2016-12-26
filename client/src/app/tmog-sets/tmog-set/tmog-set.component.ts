import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { TMogSetsService } from '../tmog-sets.service';
import { TMogSetsCacheService } from '../tmog-sets-cache.service';
import { TMogSet } from '../../core/models';

@Component({
  moduleId: module.id,
  selector: 'tmog-set',
  templateUrl: 'tmog-set.component.html',
  styleUrls: ['tmog-set.component.css'],
  providers: [TMogSetsService, TMogSetsCacheService]
})
export class TMogSetComponent implements OnInit {

    public model: any = {};

    constructor(private route: ActivatedRoute, private tmogSetsService: TMogSetsService) {

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
}
