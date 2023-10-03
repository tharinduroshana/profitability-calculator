<script>
import Button from "@/components/Button.vue";
import InputBox from "@/components/InputBox.vue";
import {ButtonTypes, InputSizes, InputTypes} from "@/utils/enums";
import LoadingButton from "@/components/LoadingButton.vue";
import {useCalculationResultStore} from "@/store/CalculationResultStore";
import {usePopUpAlertStore} from "@/store/PopUpAlertStore";
import {useUserAuthStore} from "@/store/UserAuthStore";
import Header from "@/components/Header.vue";

/*
* ProfitabilityCalculator Screen
*
* This view shows the form component to calculate Profitability
* */

export default {
  name: "CalculatorView",
  computed: {
    ButtonTypes() {
      return ButtonTypes
    },
    InputSizes() {
      return InputSizes
    },
    InputTypes() {
      return InputTypes
    }
  },
  data() {
    return {
      isLoading: false,
      pricePerKilometre: "",
      pricePerHour: "",
      noOfKilometres: "",
      noOfHours: "",
      income: "",
    }
  },
  components: {Header, Button, InputBox, LoadingButton},
  methods: {
    async onSubmit(e) {
      e.preventDefault();
      this.isLoading = true;
      const inputData = {
        pricePerKilometre: this.pricePerKilometre,
        pricePerHour: this.pricePerHour,
        noOfKilometres: this.noOfKilometres,
        noOfHours: this.noOfHours,
        income: this.income
      }

      const store = useCalculationResultStore();
      const status = await store.calculateProfitability(inputData);

      if (status === 401) {
        this.alertStore.showPopUpAlertWithFunction("authorization_failure", "authorize", () => this.routeToLogin())
      } else if (status === 400) {
        this.alertStore.showPopUpAlert("calculation_failed_wrong_args");
        this.isLoading = false;
      } else if (status === 200) {
        this.isLoading = false;
        this.$router.push({name: "results", replace: true});
      } else if (status === 500) {
        this.alertStore.showPopUpAlert("internal_server_error");
        this.isLoading = false;
      } else {
        this.alertStore.showPopUpAlert("unknown_error");
        this.isLoading = false;
      }
    },
    routeToLogin() {
      this.alertStore.hidePopUpAlert();
      this.authStore.logoutUser();
      this.$router.push({name: "login", replace: true});
    }
  },
  setup() {
    const calcStore = useCalculationResultStore();
    const alertStore = usePopUpAlertStore();
    const authStore = useUserAuthStore();
    calcStore.resetResults();
    return { alertStore, authStore }
  }
}
</script>

<template>
  <div class="container">
    <div class="row">
      <div class="general-form">
        <Header />
        <h5 class="title">{{ $t('profitability_calculator') }}</h5>
        <form @submit="onSubmit">
          <div class="row">
            <InputBox v-model="pricePerKilometre" :size="InputSizes.HALF" label="price_per_km"
                      :needValidation="true"
                      placeholder="price_charged_per_km"
                      :type="InputTypes.NUMBER" :required="true"/>
            <InputBox v-model="pricePerHour" :size="InputSizes.HALF" label="price_per_hour" :needValidation="true"
                      placeholder="price_charged_per_hour"
                      :type="InputTypes.NUMBER" :required="true"/>
          </div>
          <div class="row">
            <InputBox v-model="noOfKilometres" :size="InputSizes.HALF" label="kilometres"
                      placeholder="no_of_kms" :needValidation="true"
                      :type="InputTypes.NUMBER" :required="true"/>
            <InputBox v-model="noOfHours" :size="InputSizes.HALF" :needValidation="true" label="hours"
                      placeholder="no_of_hours"
                      :type="InputTypes.NUMBER" :required="true"/>
          </div>
          <div class="row">
            <InputBox v-model="income" :size="InputSizes.FULL" :needValidation="true" label="income"
                      placeholder="total_income"
                      :type="InputTypes.NUMBER" :required="true"/>
          </div>
          <div class="row" v-if="isLoading">
            <div class="col-md-12">
              <LoadingButton text="calculating_btn_text"/>
            </div>
          </div>
          <div class="row" v-else>
            <div class="col-md-12">
              <Button :type="ButtonTypes.SUBMIT" text="calculate_profitability"/>
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<style scoped>

</style>