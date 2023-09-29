import { createRouter, createWebHistory } from "vue-router";
import CalculationResultsView from "@/views/CalculationResultsView.vue";
import CalculatorView from "@/views/CalculatorView.vue";
import LoginPage from "@/views/LoginPage.vue";
import SignUpPage from "@/views/SignUpPage.vue";
import Cookies from 'js-cookie';
import {useCalculationResultStore} from "@/store/CalculationResultStore";
import {useUserAuthStore} from "@/store/UserAuthStore";

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
    {
        path: "/signup",
        name: "signup",
        component: SignUpPage,
    },
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

router.beforeEach((to, from, next) => {
    const isAuthenticated = !!Cookies.get('auth_cookie');

    if (to.name === 'login' || to.name === 'signup') {
        if (isAuthenticated) {
            next({ name: 'calculator' });
        } else {
            next();
        }
    } else if (isAuthenticated) {
        const cookieData = Cookies.get('auth_cookie');
        const authStore = useUserAuthStore();
        authStore.authUserFromCookie(cookieData);
        if (to.name === 'results') {
            const store = useCalculationResultStore();
            if (store.profitabilityCalculation == null) {
                next({ name: "calculator" });
            } else {
                next();
            }
        } else {
            next();
        }
    } else {
        next({ name: 'login' });
    }
});

export default router;
