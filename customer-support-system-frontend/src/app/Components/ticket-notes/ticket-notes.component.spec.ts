import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketNotesComponent } from './ticket-notes.component';

describe('TicketNotesComponent', () => {
  let component: TicketNotesComponent;
  let fixture: ComponentFixture<TicketNotesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TicketNotesComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TicketNotesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
