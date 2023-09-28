import {defineStore} from "pinia";

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
        }
    },
});
