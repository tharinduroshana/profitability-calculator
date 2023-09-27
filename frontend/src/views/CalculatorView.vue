<script>
import Button from "@/components/Button.vue";
import InputBox from "@/components/InputBox.vue";
import {ButtonTypes, InputSizes, InputTypes} from "@/utils/enums";
import LoadingButton from "@/components/LoadingButton.vue";
import {useCalculationResultStore} from "@/store/CalculationResultStore";

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
  components: {Button, InputBox, LoadingButton},
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
      await store.calculateProfitability(inputData);

      this.isLoading = false;
      this.$router.push({ name: "results", replace: true });
    }
  },
}
</script>

<template>
  <div class="row">
    <div class="general-form">
      <h5 class="title">Profitability Calculator</h5>
      <form @submit="onSubmit">
        <div class="row">
          <InputBox v-model="pricePerKilometre" :size="InputSizes.HALF" label="Price per Kilometre"
                    placeholder="Price charged for a kilometre"
                    :type="InputTypes.NUMBER" :required="true"/>
          <InputBox v-model="pricePerHour" :size="InputSizes.HALF" label="Price per Hour"
                    placeholder="Price charged for an hour"
                    :type="InputTypes.NUMBER" :required="true"/>
        </div>
        <div class="row">
          <InputBox v-model="noOfKilometres" :size="InputSizes.HALF" label="Kilometres"
                    placeholder="Number of kilometres"
                    :type="InputTypes.NUMBER" :required="true"/>
          <InputBox v-model="noOfHours" :size="InputSizes.HALF" label="Hours" placeholder="Number of hours"
                    :type="InputTypes.NUMBER" :required="true"/>
        </div>
        <div class="row">
          <InputBox v-model="income" :size="InputSizes.FULL" label="Income" placeholder="Total income"
                    :type="InputTypes.NUMBER" :required="true"/>
        </div>
        <div class="row" v-if="isLoading">
          <div class="col-md-12">
            <LoadingButton text="Calculating..."/>
          </div>
        </div>
        <div class="row" v-else>
          <div class="col-md-12">
            <Button :type="ButtonTypes.SUBMIT" text="Calculate Profitability"/>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<style scoped>

</style>