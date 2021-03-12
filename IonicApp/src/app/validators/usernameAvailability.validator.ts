import { Injectable, Injector } from '@angular/core';
import { AbstractControl, AsyncValidator, AsyncValidatorFn, FormControl, ValidationErrors } from '@angular/forms';
import { Observable } from 'rxjs';
import { UserService } from '../proxy/controllers';

export class UsernameAvailabilityValidator { 
    static getAsyncValidator(userService: UserService) {
        return (control: AbstractControl): Promise<ValidationErrors> | Observable<ValidationErrors> => {
            if (control) {
                var username = control.value as string;
                if (username && username != '')
                    return new Observable<ValidationErrors>(ob => {
                        userService.getByUsername(username)
                            .subscribe(dto => {
                                ob.next({ "Unavailable": true } as ValidationErrors);
                                ob.complete();
                            }, err => {                                
                                ob.complete();
                            }, () => { ob.complete(); });
                    });
            }
        };
    }
}