import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {UsersService} from "../users-dashboard/users.service";
import {first} from "rxjs/operators";

@Component({
  selector: 'app-user-add',
  templateUrl: './user-add.component.html',
  styleUrls: ['./user-add.component.css']
})
export class UserAddComponent implements OnInit {

  _userAddForm: FormGroup;
  submitted = false;
  loading = false;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private usersServices: UsersService,
    private router: Router) {

      this._userAddForm = this.formBuilder.group({
          id: [''],
          firstName: ['', Validators.required],
          lastName: ['', Validators.required],
          phoneNumber: ['', [Validators.required, Validators.pattern("^[0-9]*$")]],
          email: ['', [Validators.required, Validators.email]]
        }
      );
  }

  get userAddForm() { return this._userAddForm.controls; }

  ngOnInit() { }

  onSubmit() {
    this.submitted = true;
    if (this._userAddForm.invalid) {
      console.log('invalid useraddform');
      return;
    }

    this.loading = true;

    this.addUser()
  }

  private addUser(): void {

    this.usersServices.create(this._userAddForm.value)
      .pipe(first())
      .subscribe({
          next: () => {

            this.router.navigate(['../'], { relativeTo: this.route })
          },
          error: (error) => {
            console.log(error);
            this.loading = false;
          }
      });
  }
}
