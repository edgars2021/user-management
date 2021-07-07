import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {UsersService} from "../users-dashboard/users.service";
import {first} from "rxjs/operators";

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {

  _userEditForm: FormGroup;
  submitted = false;
  loading = false;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private usersServices: UsersService,
    private router: Router) {

    this._userEditForm = this.formBuilder.group({
        id: [''],
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        phoneNumber: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
        email: ['', [Validators.required, Validators.email]]
      }
    );
  }

  get userEditForm() { return this._userEditForm.controls; }

  ngOnInit() {

    let currentUserId = this.route.snapshot.paramMap.get('id');
    console.log(currentUserId);
    this.usersServices.getUserBy(currentUserId).subscribe(user => {
      this._userEditForm.setValue(user);
    });

  }

  onSubmit() {
    this.submitted = true;
    if (this._userEditForm.invalid) {
      return;
    }

    this.loading = true;
    this.updateUser()
  }

  private updateUser(): void {

    this.usersServices.update(this.route.snapshot.paramMap.get('id'), this._userEditForm.value)
      .pipe(first())
      .subscribe({
        next: () => {

          this.router.navigate(['../../'], { relativeTo: this.route })
        },
        error: (error) => {
          console.log(error);
          this.loading = false;
        }
      });
  }
}
