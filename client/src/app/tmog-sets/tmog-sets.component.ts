import { Component, OnInit } from '@angular/core';
import { NgbModal, ModalDismissReasons, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

import { TMogSetsService } from './tmog-sets.service';
import { TMogSet } from '../models/tmog-set';
import { TMogSetAddComponent } from './tmog-set-add/tmog-set-add.component';

@Component({
    moduleId: module.id,
    selector: 'tmog-sets',
    templateUrl: 'tmog-sets.component.html',
    styleUrls: ['tmog-sets.component.css'],
    providers: [TMogSetsService]
})
export class TMogSetsComponent implements OnInit {
    public tmogSets: TMogSet[];

    constructor(private tmogSetsService: TMogSetsService, private modalService: NgbModal) { }

    public ngOnInit(): void {
        this.populateSets();
    }

    public newSet(): void {
        const options: NgbModalOptions = {
            size: 'sm'
        };

        this.modalService.open(TMogSetAddComponent, options).result.then(result => {
            console.log(`Closed with ${result}`);
        }, reason => {
            console.log(`Dismissed ${reason}`);
        });
    }

    public isSetComplete(tmogSet: TMogSet): boolean {
        return tmogSet.completedSlots === tmogSet.totalSlots;
    }

    public isSetAlmostComplete(tmogSet: TMogSet): boolean {
        const missingSlots = tmogSet.totalSlots - tmogSet.completedSlots;

        return missingSlots > 0 && missingSlots <= 4;
    }

    private populateSets() {
        this.tmogSetsService
            .getAll()
            .then(results => {
                this.tmogSets = results;
            });
    }
}
