import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TmogSetsComponent } from './tmog-sets.component';

describe('TmogSetsComponent', () => {
  let component: TmogSetsComponent;
  let fixture: ComponentFixture<TmogSetsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TmogSetsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TmogSetsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
