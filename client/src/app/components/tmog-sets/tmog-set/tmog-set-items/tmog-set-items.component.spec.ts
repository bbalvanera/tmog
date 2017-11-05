import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TmogSetItemsComponent } from '../../../../app/tmog-sets/tmog-set/tmog-set-items/tmog-set-items.component';

describe('TmogSetItemsComponent', () => {
  let component: TmogSetItemsComponent;
  let fixture: ComponentFixture<TmogSetItemsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TmogSetItemsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TmogSetItemsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
