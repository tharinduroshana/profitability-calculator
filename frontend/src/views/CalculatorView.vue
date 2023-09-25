<script setup>
import Button from "@/components/Button.vue";
import InputBox from "@/components/InputBox.vue";
import {ButtonTypes, InputSizes, InputTypes} from "@/utils/enums";
import LoadingButton from "@/components/LoadingButton.vue";

/*
* ProfitabilityCalculator Screen
*
* This view shows the form component to calculate Profitability
* */
</script>

<script>
export default {
  name: "CalculatorView",
  data() {
    return {
      isLoading: false,
      isResultReady: false,
      pricePerKilometre: "",
      pricePerHour: "",
      noOfKilometres: "",
      noOfHours: "",
      income: "",
      profitability: 0
    }
  },
  methods: {
    onSubmit(e) {
      e.preventDefault();
      this.isLoading = true;
      const inputData = {
        pricePerKilometre: this.pricePerKilometre,
        pricePerHour: this.pricePerHour,
        noOfKilometres: this.noOfKilometres,
        noOfHours: this.noOfHours,
        income: this.income
      }
      //TODO: Send the request to server and update the profitability input value.
      setTimeout(() => {
        // This setTimeout replicates the behavior of the request
        this.profitability = "60.0";
        this.isLoading = false;
        this.isResultReady = true;
      }, 2000);
    }
  }
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
                    :type="InputTypes.NUMBER"/>
          <InputBox v-model="pricePerHour" :size="InputSizes.HALF" label="Price per Hour"
                    placeholder="Price charged for an hour"
                    :type="InputTypes.NUMBER"/>
        </div>
        <div class="row">
          <InputBox v-model="noOfKilometres" :size="InputSizes.HALF" label="Kilometres"
                    placeholder="Number of kilometres"
                    :type="InputTypes.NUMBER"/>
          <InputBox v-model="noOfHours" :size="InputSizes.HALF" label="Hours" placeholder="Number of hours"
                    :type="InputTypes.NUMBER"/>
        </div>
        <div class="row">
          <InputBox v-model="income" :size="InputSizes.FULL" label="Income" placeholder="Total income"
                    :type="InputTypes.NUMBER"/>
        </div>
        <div class="row" v-if="isResultReady">
          <InputBox v-model="profitability" :size="InputSizes.FULL" label="Profitability" :disabled="true"
                    :type="InputTypes.NUMBER"/>
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