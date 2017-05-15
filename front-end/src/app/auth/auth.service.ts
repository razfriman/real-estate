import { Injectable } from '@angular/core';
import { tokenNotExpired } from 'angular2-jwt';
import { myConfig } from './auth.config';
import { Router } from '@angular/router';
import Auth0Lock from 'auth0-lock';

@Injectable()
export class AuthService {
    lock = new Auth0Lock(myConfig.clientID, myConfig.domain, {});

    constructor(private router: Router) {
        this.lock.on('authenticated', (authResult) => {
            localStorage.setItem('id_token', authResult.idToken);
        });
    }

    public login() {
        this.lock.show();
    };

    public authenticated() {
        return tokenNotExpired('id_token');
    };

    public logout() {
        localStorage.removeItem('id_token');
        this.router.navigate(['']);
    };
}
