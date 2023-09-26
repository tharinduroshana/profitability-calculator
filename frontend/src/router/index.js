import { createRouter, createWebHistory } from "vue-router";
import CalculationResultsView from "@/views/CalculationResultsView.vue";
import CalculatorView from "@/views/CalculatorView.vue";

/*
* The declaration of routes
* */
const routes = [
    {
        path: "/",
        name: "calculator",
        component: CalculatorView,
    },
    {
        path: "/results",
        name: "results",
        component: CalculationResultsView,
    },
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;
