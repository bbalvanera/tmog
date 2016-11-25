import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { TMogSetAddComponent } from './tmog-set-add.component';

let component: TMogSetAddComponent;
let fixture: ComponentFixture<TMogSetAddComponent>;

describe('TMogSetAddComponent', () => {
    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [
                FormsModule
            ],
            declarations: [
                TMogSetAddComponent
            ],
            providers: [
                { provide: NgbActiveModal, useValue: NgbActiveModal }
            ]
        }).compileComponents().then(() => {
            fixture = TestBed.createComponent(TMogSetAddComponent);
            component = fixture.componentInstance;
        });
    }));

    it('should exist', () => {
        expect(component).toBeDefined();
    });

    describe('when saving', () => {
        it('should start with clear message', () => {
            expect(component.formErrors.setIdInput).toBeDefined();
            expect(component.formErrors.setIdInput.length).toBe(0);
        });

        it('should set error message if invalid input', () => {
            component.setId = "invalid input";

            component.save();

            expect(component.formErrors.setIdInput).toBeDefined();
            expect(component.formErrors.setIdInput.length).toBeGreaterThan(0);
        });

        it('should remove error if valid input', () => {
            // force an invalid output
            component.setId = 'invalid';
            component.save();
            expect(component.formErrors.setIdInput.length).toBeGreaterThan(0);

            // set a valid case
            component.setId = '215'; // any number is a valid input.
            component.save();

            expect(component.formErrors.setIdInput).toBeFalsy();
        });
    });

});
