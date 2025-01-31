import { Routes } from '@angular/router';
import { RegisterComponent } from './Components/register/register.component';
import { LoginComponent } from './Components/login/login.component';
import { HomeComponent } from './Components/home/home.component';
import { WelcomePageComponent } from './Components/welcome-page/welcome-page.component';
import { CreateTicketComponent } from './Components/create-ticket/create-ticket.component';
import { MyTicketsComponent } from './Components/my-tickets/my-tickets.component';
import { TicketNotesComponent } from './Components/ticket-notes/ticket-notes.component';
import { AddFeedbackComponent } from './Components/add-feedback/add-feedback.component';
import { CreateNoteComponent } from './Components/create-note/create-note.component';

export const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'home', component: HomeComponent },
    { path: 'welcome', component: WelcomePageComponent },
    { path: 'create-ticket', component: CreateTicketComponent },
    { path: 'my-tickets', component: MyTicketsComponent },
    { path: 'ticket-notes/:id', component: TicketNotesComponent },
    { path: 'ticket-feedback/:id', component: AddFeedbackComponent },
    { path: 'create-note/:id', component: CreateNoteComponent },
    { path: '', redirectTo: '/home', pathMatch: 'full' }
];
