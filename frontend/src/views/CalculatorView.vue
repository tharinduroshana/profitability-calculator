<script setup>
import Button from "@/components/Button.vue";
import InputBox from "@/components/InputBox.vue";
import {InputSizes, InputTypes} from "@/utils/enums";

/*
* ProfitabilityCalculator Screen
*
* This view shows the form component to calculate Profitability
* */
</script>

<template>
  <div class="row">
    <div class="general-form">
      <h5 class="title">Profitability Calculator</h5>
      <form>
        <div class="row">
          <InputBox :size="InputSizes.HALF" label="Price per Kilometre" placeholder="Price charged for a kilometre"
                    :type="InputTypes.NUMBER"/>
          <InputBox :size="InputSizes.HALF" label="Price per Hour" placeholder="Price charged for an hour"
                    :type="InputTypes.NUMBER"/>
        </div>
        <div class="row">
          <InputBox :size="InputSizes.HALF" label="Kilometres" placeholder="Number of kilometres"
                    :type="InputTypes.NUMBER"/>
          <InputBox :size="InputSizes.HALF" label="Hours" placeholder="Number of hours" :type="InputTypes.NUMBER"/>
        </div>
        <div class="row">
          <InputBox :size="InputSizes.FULL" label="Income" placeholder="Total income" :type="InputTypes.NUMBER"/>
        </div>
        <div class="row">
          <div class="col-md-12">
            <Button text="Calculate Profitability"/>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<style scoped>

</style>