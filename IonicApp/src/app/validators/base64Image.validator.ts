import { FormControl, ValidationErrors } from '@angular/forms';
import { Observable } from 'rxjs';

export class Base64ImageValidator
{
    constructor()
    { }

    static validator(control: FormControl): any
    {
        if (control) 
        try {
            var img = document.createElement('img', {  });
            img.src = control.value;

            img.onerror = (err) => {
                return { 'Invalid base64 string': false }
            };            
        }
        catch(ex)
        {
            return { 'Invalid base64 string': false }
        }    
    }

    static asyncValidator(control: FormControl) : Observable<ValidationErrors | null>
    {
        return new Observable<ValidationErrors | null>(ob => {
            
            if (!control) { 
                ob.next(null); 
                ob.complete();
            }
            
            var img = document.createElement('img');

            img.src = control.value;            
            img.onerror = () => {
                ob.next({ 'InvalidBase64Image': true });                
                ob.complete();
            };

            img.onload = () => {
                ob.next(null);
                ob.complete();
            };
        });
    }
}