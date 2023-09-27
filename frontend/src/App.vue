<script>
import CalculatorView from "./views/CalculatorView.vue";
import CalculationResultsView from "@/views/CalculationResultsView.vue";

export default {
  name: "App",
  components: {CalculationResultsView, CalculatorView},
  data() {
    return {
      isMobile: false
    }
  },
  methods: {
    /*
    * setMobileView function checks if the screen width is lower than 540px
    * This functionality is added to fit the application nicely to the mobile screens
    * */
    setMobileView() {
      if (window.innerWidth < 540) {
        this.isMobile = true;
      }
    }
  },
  created() {
    this.setMobileView();
    window.addEventListener('resize', this.setMobileView)
  },
  beforeDestroy() {
    window.removeEventListener('resize', this.setMobileView)
  },
}
</script>

<template>
  <div id="body-area" class="section">
    <div :class="this.isMobile ? 'section-center-mobile' : 'section-center'">
      <router-view v-slot="{ Component }">
        <transition name="route" mode="out-in">
          <component :is="Component"></component>
        </transition>
      </router-view>
    </div>
  </div>
</template>

<style scoped>
.route-enter-active {
  transition: all 0.3s ease-out;
}

.route-leave-active {
  transition: all 0.3s ease-in;
}

.route-enter-from {
  opacity: 0;
  transform: translateX(100px);
}

.route-leave-to {
  opacity: 0;
  transform: translateX(-100px);
}
</style>
