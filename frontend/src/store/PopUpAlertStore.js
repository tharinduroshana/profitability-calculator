import { defineStore } from "pinia";

/*
* The store for showing popup alerts
* */
export const usePopUpAlertStore = defineStore("popUpAlertStore", {
    state: () => {
        return {
            showAlert: false,
            message: ""
        };
    },
    actions: {
        showPopUpAlert(message) {
            this.showAlert = true;
            this.message = message;
        },
        hidePopUpAlert() {
            this.showAlert = false;
            this.message = "";
        }
    },
});
