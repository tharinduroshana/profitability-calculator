import { defineStore } from "pinia";

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
            const response = await fetch("/api/profitabilityCalculation", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(payload)
            });
            this.profitabilityCalculation = await response.json();
        }
    },
    persist: {
        enabled: true,
        strategies: [
            { storage: sessionStorage, paths: ['profitabilityCalculation'] }
        ],
    },
});
