import { Component, Input, Output, EventEmitter, HostListener, forwardRef } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';

const UI_SWITCH_CONTROL_VALUE_ACCESSOR: any = {
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => UiSwitchDirective),
    multi: true
};

@Component({
    moduleId: module.id,
    selector: 'ui-switch',
    templateUrl: 'ui-switch.component.html',
    styleUrls: ['ui-switch.component.css'],
    providers: [UI_SWITCH_CONTROL_VALUE_ACCESSOR]
})
export class UiSwitchDirective implements ControlValueAccessor {
    private _checked: boolean;
    private _disabled: boolean;
    private _reverse: boolean;

    @Output()
    public change = new EventEmitter<boolean>();

    @Input()
    public size = 'medium';

    @Input()
    public color = 'rgb(100, 189, 99)';

    @Input()
    public switchOffColor = '';

    @Input()
    public switchColor = '#fff';

    public defaultBgColor = '#fff';
    public defaultBoColor = '#dfdfdf';

    @Input()
    public set checked(v: boolean) {
        this._checked = v !== false;
    }

    public get checked(): boolean {
        return this._checked;
    }

    @Input()
    public set disabled(v: boolean) {
        this._disabled = v !== false;
    };

    public get disabled(): boolean {
        return this._disabled;
    }

    @Input()
    public set reverse(v: boolean) {
        this._reverse = v !== false;
    };

    public get reverse() {
        return this._reverse;
    }

    public getColor(flag: string) {
        if (flag === 'borderColor') {
            return this.defaultBoColor;
        }

        if (flag === 'switchColor') {
            if (this.reverse) {
                return !this.checked ? this.switchColor : this.switchOffColor || this.switchColor;
            }

            return this.checked ? this.switchColor : this.switchOffColor || this.switchColor;
        }

        if (this.reverse) {
            return !this.checked ? this.color : this.defaultBgColor;
        }

        return this.checked ? this.color : this.defaultBgColor;
    }

    @HostListener('click')
    public onToggle() {
        if (this.disabled) {
            return;
        }

        this.checked = !this.checked;
        this.change.emit(this.checked);
        this.onChangeCallback(this.checked);
        this.onTouchedCallback(this.checked);
    }

    public writeValue(obj: any): void {
        if (obj !== this.checked) {
            this.checked = !!obj;
        }
    }

    public registerOnChange(fn: any) {
        this.onChangeCallback = fn;
    }

    public registerOnTouched(fn: any) {
        this.onTouchedCallback = fn;
    }

    private onTouchedCallback = (v: any) => {

    };
    private onChangeCallback = (v: any) => {

    };
}
