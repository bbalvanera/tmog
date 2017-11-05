import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TmogSetAddComponent } from './tmog-set-add.component';

describe('TmogSetAddComponent', () => {
  let component: TmogSetAddComponent;
  let fixture: ComponentFixture<TmogSetAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TmogSetAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TmogSetAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
