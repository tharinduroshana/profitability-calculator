import {defineStore} from "pinia";

/*
* The store for showing popup alerts
* */
export const usePopUpAlertStore = defineStore("popUpAlertStore", {
    state: () => {
        return {
            showAlert: false,
            message: "",
            onClose: null,
            buttonText: "close"
        };
    },
    actions: {
        showPopUpAlert(message) {
            this.showAlert = true;
            this.message = message;
        },
        showPopUpAlertWithFunction(message, buttonText, onClose) {
            this.showAlert = true;
            this.message = message;
            this.onClose = onClose;
            this.buttonText = buttonText;
        },
        hidePopUpAlert() {
            this.showAlert = false;
            this.message = "";
            this.onClose = null;
        }
    },
});
