import { Injectable } from "@angular/core";
import { State, Store, Action, StateContext } from '@ngxs/store';
import { Observable } from "rxjs";
import { PasswordResetPinConstraints } from "src/app/constraints/password-reset-pin-constriants";
import { UserService } from "src/app/proxy/controllers";
import { SendPasswordResetPinRequest } from "src/app/state-management/actions/SendPasswordResetPinRequest.action";
import { PasswordResetPinRequestStateModel } from "src/app/state-management/state-containers/password-reset-pin-request.state";

@Injectable()
export class PasswordResetPinRequestService {

    constructor(
        private userService: UserService,
        private store: Store) {
    }

    getCurrentRequest() {
        return this.store
            .selectOnce<PasswordResetPinRequestStateModel>(state => state.passwordResetPinRequest)        
    }

    canRequest() {
        return new Observable<boolean>(ob => {
            this.store.selectOnce(state => state.passwordResetPinRequest)
                .subscribe((state: PasswordResetPinRequestStateModel) => {
                    var rTime = new Date(state.requestedTime);
                    console.log(rTime);
                    if (rTime) {
                        var t = rTime.getTime() / 1000;
                        var now = Date.now() / 1000;
                        var intvl = PasswordResetPinConstraints.interval * 60;

                        ob.next((t + intvl < now));
                        ob.complete();
                    }
                    else { 
                        ob.next(true); 
                        ob.complete();
                    }
                }, err => {
                    ob.error(err);
                });
        });
    }

    sendRequest(userName: string) {

        if (!userName || userName.trim() == '') throw Error('Invalid parameters');

        if (!(this.canRequest().toPromise())) Error('Cannot request a new pin at the moment');        

        return new Observable<void>(ob => {
            this.userService.sendPasswordResetCodeByDto({
                userName: userName,
                passwordResetToken: null,
                newPassword: null
            })
                .subscribe(() => {
                    this.store.dispatch(new SendPasswordResetPinRequest(userName)).subscribe();
                    ob.next();
                    ob.complete();
                }, err => {
                    ob.error(err);
                }, () => { 
                    ob.complete(); 
                });
        });
    }
}