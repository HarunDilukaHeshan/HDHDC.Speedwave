export class SendPasswordResetPinRequest {
    private readonly _userName: string = '';

    constructor(userName: string) {
        if (!userName || userName.trim() == '') throw Error('UserName cannot be null or empty');
        this._userName = userName;
    }

    public static readonly type = '[ForgotPassword] SendPasswordResetPinRequest';

    public get userName() { return this._userName; }
}