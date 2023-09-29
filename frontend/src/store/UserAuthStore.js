import {defineStore} from "pinia";
import Cookies from 'js-cookie';

/*
* The store for user authentications
* */
export const useUserAuthStore = defineStore("userAuthStore", {
    state: () => {
        return {
            user: null
        };
    },
    actions: {
        async signUpUser(userData) {
            return await fetch("/api/user/signup", {
                method: "POST",
                headers: {"Content-Type": "application/json"},
                body: JSON.stringify(userData)
            });
        },
        async loginUser(userData) {
            let status = 400;
            try {
                const response = await fetch("/api/user/login", {
                    method: "POST",
                    headers: {"Content-Type": "application/json"},
                    body: JSON.stringify(userData)
                });
                status = response.status;
                this._storeUser(await response.json());
            } catch (e) {
                console.log(e);
            }
            return status;
        },
        authUserFromCookie(token) {
            this.user = { token: token }
        },
        logoutUser() {
            this.user = null;
            Cookies.remove("auth_cookie");
        },
        _storeUser(user) {
            this.user = user;
            Cookies.set('auth_cookie', user.token, {expires: 5});
        }
    },
});
