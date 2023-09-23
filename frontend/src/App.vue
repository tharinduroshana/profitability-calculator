<script>
import CalculatorView from "./views/CalculatorView.vue";

export default {
  name: "App",
  components: {CalculatorView},
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
      <div class="container">
        <CalculatorView />
      </div>
    </div>
  </div>
</template>

<style scoped>

</style>
