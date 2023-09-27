import { createRouter, createWebHistory } from "vue-router";
import CalculationResultsView from "@/views/CalculationResultsView.vue";
import CalculatorView from "@/views/CalculatorView.vue";
import LoginPage from "@/views/LoginPage.vue";

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
    {
        path: "/login",
        name: "login",
        component: LoginPage,
    },
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;
