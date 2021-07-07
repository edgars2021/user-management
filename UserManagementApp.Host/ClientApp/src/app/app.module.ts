import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { UsersDashboardComponent } from './users-dashboard/users-dashboard.component';
import { UsersService } from './users-dashboard/users.service';
import { UserEditComponent } from './user-edit/user-edit.component';
import { UserAddComponent } from './user-add/user-add.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    UsersDashboardComponent,
    UserEditComponent,
    UserAddComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: '', component: UsersDashboardComponent},
      {path: 'edit/:id', component: UserEditComponent},
      {path: 'add', component: UserAddComponent}
    ]),
    ReactiveFormsModule
  ],
  providers: [UsersService],
  bootstrap: [AppComponent]
})
export class AppModule { }
