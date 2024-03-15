import { Component } from '@angular/core';
import {
  AsyncValidatorFn,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';
import {  map, of, switchMap, timer } from 'rxjs';
import { ApiError } from 'src/app/shared/models/ApiErrors';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  registrationForm!: FormGroup;
  EmailExist = false;
  errorEmailInUse: string='';

  // Inside your component class
  apiErrors: ApiError[] = [];
  


  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private router: Router
  ) {}

  ngOnInit() {
    this.createRegistrationForm();
  }

  createRegistrationForm() {
    this.registrationForm = this.fb.group({
      displayName: [null, [Validators.required]],
      email: [
        null,
        [
          Validators.required,
          Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$'),
        ],
        this.validateEmailNotTaken(), // Remove the extra square brackets
      ],
      password: [null, [Validators.required, Validators.minLength(6)]],
    });
  }

  

  onSubmit() {
    const emailValue = this.registrationForm.get('email')?.value; //check email exist

    this.accountService.register(this.registrationForm.value).subscribe(
      () => {
        if (this.registrationForm.valid) {
          console.log('user Registered successfully ...');
          this.router.navigateByUrl('/shop');
        }
      },
      (error) => {
        console.log(error);

        if (error.error && error.error.errors) {
          console.log('error.error && error.error.errors')

          // Store the errors in the apiErrors variable
          this.apiErrors = error.error.errors;
        } else if(error.error){
          console.log('error.error')
          console.log(typeof error.error)
          this.errorEmailInUse=error.error
        }
        
        else {
          // Handle other types of errors, if needed
          this.apiErrors = [{ code: 'unknown', description: 'An unexpected error occurred.' }];
        }
      }
    );
  }


  validateEmailNotTaken(): AsyncValidatorFn { //to validate the email sync
    return (control) => {
      return timer(500).pipe(
        switchMap(() => {
          if (!control.value) {
            return of(null);
          }
          return this.accountService.checkEmailExists(control.value).pipe(
            map((res) => {
              return res ? { EmailExists: true } : null;
            })
          );
        })
      );
    };
  }
}
