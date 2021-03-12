import { FormControl } from '@angular/forms';

export class Base64Validator
{
    constructor()
    { }

    static validator(control: FormControl): any
    {
        if (control) {
            var value = control.value as string;
            try {
                atob(value);
            }
            catch(ex)
            {
                return { 'Invalid base64 string': false }
            }
        }
    }
}