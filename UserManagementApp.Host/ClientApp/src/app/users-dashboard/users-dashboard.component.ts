import {Component, OnInit} from '@angular/core';
import {BehaviorSubject} from 'rxjs';
import { UsersService } from './users.service';
import {first} from "rxjs/operators";

@Component({
  selector: 'app-users-dashboard',
  templateUrl: './users-dashboard.component.html',
  styleUrls: ['./users-dashboard.component.css']
})
export class UsersDashboardComponent implements OnInit {

  private _users$: BehaviorSubject<User[]> = new BehaviorSubject<User[]>(null);

  loading: boolean = false;

  constructor(
    private usersService: UsersService) {
    this.usersService.users$.subscribe(u => {
      this._users$.next(u)
      this.loading = false;
    });
  }

  get users$() { return this._users$.asObservable(); }


  ngOnInit() {
     this.loadUser();
  }

  loadUser() {
    this.loading = true;
    this.usersService.loadAllUsers();
  }

  onEditUser(user: User): void {
    console.log(user);
  }

  onDeleteUser(id: string): void {
    this.usersService.delete(id)
      .pipe(first())
      .subscribe(() => this.loadUser());
  }
}
