import { defineStore } from "pinia";
import {useUserAuthStore} from "@/store/UserAuthStore";

/*
* The store for Calculate results
* */
export const useCalculationResultStore = defineStore("calculationResultStore", {
    state: () => {
        return {
            profitabilityCalculation: null,
        };
    },
    actions: {
        async calculateProfitability(payload) {
            let status = 401;
            try {
                const token = useUserAuthStore().user.token;
                const response = await fetch("/api/profitabilityCalculation", {
                    method: "POST",
                    headers: { "Content-Type": "application/json", "Authorization": `Bearer ${token}` },
                    body: JSON.stringify(payload)
                });
                status = response.status;
                this.profitabilityCalculation = await response.json();
            } catch (e) {
                console.log(e);
            }
            return status;
        },
        resetResults() {
            this.profitabilityCalculation = null;
            sessionStorage.removeItem("profitabilityCalculation");
        }
    },
    persist: {
        enabled: true,
        strategies: [
            { storage: sessionStorage, paths: ['profitabilityCalculation'] }
        ],
    },
});
