import { Routes } from '@angular/router';
import { HomeComponentComponent } from './home-component/home-component.component';
import { MemberListComponent } from './member/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { MemberDetailComponent } from './member/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { authGaurdGuard } from './_Gaurds/auth-gaurd.guard';

export const routes: Routes = [
  { path: '', component: HomeComponentComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGaurdGuard],
    children: [
      { path: 'members', component: MemberListComponent },
      { path: 'messages', component: MessagesComponent },
      { path: 'lists', component: ListsComponent },
      { path: 'member/:id', component: MemberDetailComponent },
    ],
  },
];
