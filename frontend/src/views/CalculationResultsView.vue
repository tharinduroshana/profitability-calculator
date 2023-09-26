<script>
import InputBox from "@/components/InputBox.vue";
import Button from "@/components/Button.vue";
import {ButtonTypes, InputSizes, InputTypes} from "@/utils/enums";

/*
* CalculationResultsView Screen
*
* This screen will display the calculation results
* */
export default {
  name: "CalculationResultsView",
  computed: {
    ButtonTypes() {
      return ButtonTypes
    },
    InputTypes() {
      return InputTypes
    },
    InputSizes() {
      return InputSizes
    }
  },
  components: {Button, InputBox},
  methods: {
    onSubmit(e) {
      e.preventDefault();
      this.$router.push({name: "calculator", replace: true});
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
          <InputBox :size="InputSizes.FULL" label="Total Distance Based Cost" :disabled="true"
                    :type="InputTypes.NUMBER"/>
        </div>
        <div class="row">
          <InputBox :size="InputSizes.FULL" label="Total Time Based Cost" :disabled="true"
                    :type="InputTypes.NUMBER"/>
        </div>
        <div class="row">
          <InputBox :size="InputSizes.FULL" label="Income" :disabled="true"
                    :type="InputTypes.NUMBER"/>
        </div>
        <div class="row">
          <InputBox :size="InputSizes.FULL" label="Profitability" :disabled="true"
                    :type="InputTypes.NUMBER"/>
        </div>
        <div class="row">
          <div class="col-md-12">
            <Button :type="ButtonTypes.SUBMIT" text="Back to Calculator"/>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<style scoped>

</style>