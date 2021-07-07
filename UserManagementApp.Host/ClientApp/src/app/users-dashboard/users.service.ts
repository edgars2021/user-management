import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {BehaviorSubject, Observable, of} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  private _users$ = new BehaviorSubject<User[]>([]);

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  get users$() { return this._users$.asObservable(); }

  loadAllUsers(): void {
    this.http.get<User[]>(this.baseUrl + 'api/users')
      .subscribe(users => this._users$.next(users));
  }

  getUserBy(id: string): Observable<User> {
    return this.http.get<User>(this.baseUrl + 'api/users/' + id);
  }

  create(user: any): Observable<User> {
    return this.http.post<User>(this.baseUrl + 'api/users', user);
  }

  update(id: string, user: any): Observable<User> {
    return this.http.put<User>(this.baseUrl + 'api/users/' + id, user);
  }

  delete(id: string): Observable<any> {
    return this.http.delete(this.baseUrl + 'api/users/' + id);
  }
}
