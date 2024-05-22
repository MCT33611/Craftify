import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TabelWithFilterComponent } from './tabel-with-filter.component';

describe('TabelWithFilterComponent', () => {
  let component: TabelWithFilterComponent;
  let fixture: ComponentFixture<TabelWithFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TabelWithFilterComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TabelWithFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
