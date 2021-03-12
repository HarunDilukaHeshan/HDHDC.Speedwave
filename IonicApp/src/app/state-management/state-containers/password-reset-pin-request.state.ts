import { Injectable } from "@angular/core";
import { State, Store, Action, StateContext } from '@ngxs/store';
import { SendPasswordResetPinRequest } from "../actions/SendPasswordResetPinRequest.action";
import { ConfigStateService } from '@abp/ng.core';
import { OAuthService } from 'angular-oauth2-oidc';

export interface PasswordResetPinRequestStateModel {
    userName: string;
    requestedTime: Date;
}

@State<PasswordResetPinRequestStateModel>({
    name: 'passwordResetPinRequest',
    defaults: {
        userName: null,
        requestedTime: null
    }
})
@Injectable()
export class PasswordResetPinRequestState {

    @Action(SendPasswordResetPinRequest)
    sendPasswordResetPinRequest(ctx: StateContext<PasswordResetPinRequestStateModel>, action: SendPasswordResetPinRequest) {
        ctx.setState({ userName: action.userName, requestedTime: new Date(Date.now()) });
    }
}