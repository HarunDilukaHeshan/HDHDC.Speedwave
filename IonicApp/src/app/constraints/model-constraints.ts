
export class ModelConstraints
{
    static readonly nameMinLength: number = 2;
    static readonly nameMaxLength: number = 48;
    static readonly nameAllowedCharacterPattern: string = "[a-zA-Z0-9][a-zA-Z0-9\-\(\). ]*";

    static readonly descriptionMinLength: number = 8;
    static readonly descriptionMaxLength: number = 512;
    static readonly descriptionAllowedCharacterPattern: string = "[a-zA-Z0-9][a-zA-Z0-9\-\(\). ]*";
    
    static readonly addressMinLength: number = 8;
    static readonly addressMaxLength: number = 128;
    
    static readonly minItemPrice: number = 0.01;

    static readonly phoneNoPattern: RegExp = /^\b0[\d]{9}\b$/;

    static readonly passwordMinLength: number = 6;
    static readonly passwordMaxLength: number = 32;

    static readonly passwordResetTokenLength: number = 6;
}