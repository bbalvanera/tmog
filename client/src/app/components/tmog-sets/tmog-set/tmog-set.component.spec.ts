import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TmogSetComponent } from './tmog-set.component';

describe('TmogSetComponent', () => {
  let component: TmogSetComponent;
  let fixture: ComponentFixture<TmogSetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TmogSetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TmogSetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
