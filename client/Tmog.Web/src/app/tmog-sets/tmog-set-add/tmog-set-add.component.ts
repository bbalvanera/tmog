import { Component } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

import { TMogSetsService } from '../tmog-sets.service';
import { TMogSetsCacheService } from '../tmog-sets-cache.service';

@Component({
    moduleId: module.id,
    selector: 'tmog-set-add',
    templateUrl: './tmog-set-add.component.html',
    styleUrls: ['tmog-set-add.component.css'],
    providers: [TMogSetsService, TMogSetsCacheService]
})
export class TMogSetAddComponent {
    public setId: string;
    public formErrors = {
        setIdInput: ''
    };

    constructor(public modal: NgbActiveModal, private tmogSetsService: TMogSetsService) { }

    public save(): void {
        if (!this.isValid(this.setId)) {
            return;
        }

        try {
            this.tmogSetsService.createSet(+this.setId)
                .then(success => {
                    this.modal.close({ ok: true, tmogSetId: this.setId });
                }, error => {
                    if (error.status === 400 && error.reason === '461') { // expect 461 from server when tmog set does not exist
                        this.formErrors.setIdInput = 'The requested transmog set doesn\'t exist.';
                    }
                });
        }
        catch (e) {
            console.log(e);
        }
    }

    private isValid(value: string): boolean {
        if (!this.setId) {
            return false;
        }

        const digits = /^[0-9]+$/;
        if (!this.setId.match(digits)) {
            this.formErrors.setIdInput = 'Please type a valid set id.';
            return false;
        }

        this.formErrors.setIdInput = null;
        return true;
    }
}
