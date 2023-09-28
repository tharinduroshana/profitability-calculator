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
            const token = useUserAuthStore().user.token;
            const response = await fetch("/api/profitabilityCalculation", {
                method: "POST",
                headers: { "Content-Type": "application/json", "Authorization": `Bearer ${token}` },
                body: JSON.stringify(payload)
            });
            this.profitabilityCalculation = await response.json();
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
