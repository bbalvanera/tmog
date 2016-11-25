import { Component } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
    moduleId: module.id,
    selector: 'tmog-set-add',
    templateUrl: './tmog-set-add.component.html',
    styleUrls: ['tmog-set-add.component.css']
})
export class TMogSetAddComponent {
    public setId: string;
    public formErrors = {
        setIdInput: ''
    };

    constructor(public modal: NgbActiveModal) { }

    public save(): void {
        if (!this.isValid(this.setId)) {
            return;
        }


    }

    private isValid(value: string): boolean {
        const result = parseInt(this.setId, 10);
        if (Number.isNaN(result) || result <= 0) {
            this.formErrors.setIdInput = 'Please type a valid set id.';
            return false;
        }

        this.formErrors.setIdInput = null;
        return true;
    }
}
