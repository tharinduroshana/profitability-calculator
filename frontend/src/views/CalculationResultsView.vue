<script>
import InputBox from "@/components/InputBox.vue";
import Button from "@/components/Button.vue";
import {ButtonTypes, InputSizes, InputTypes} from "@/utils/enums";
import {useCalculationResultStore} from "@/store/CalculationResultStore";
import LanguageSelector from "@/components/LanguageSelector.vue";

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
  components: {LanguageSelector, Button, InputBox},
  methods: {
    onSubmit(e) {
      e.preventDefault();
      this.$router.push({name: "calculator", replace: true});
    }
  },
  setup() {
    const store = useCalculationResultStore();
    return {store}
  }
}
</script>

<template>
  <div class="container">
    <div class="row">
      <div class="general-form">
        <LanguageSelector />
        <h5 class="title">{{ $t('profitability_calculator') }}</h5>
        <form @submit="onSubmit">
          <div class="row">
            <InputBox :model-value="store.profitabilityCalculation.totalDistanceBasedCost.toString()"
                      :size="InputSizes.FULL" label="total_distance_based_costs" :disabled="true"
                      :type="InputTypes.NUMBER"/>
          </div>
          <div class="row">
            <InputBox :model-value="store.profitabilityCalculation.totalTimeBasedCost.toString()"
                      :size="InputSizes.FULL" label="total_time_based_costs" :disabled="true"
                      :type="InputTypes.NUMBER"/>
          </div>
          <div class="row">
            <InputBox :model-value="store.profitabilityCalculation.income.toString()" :size="InputSizes.FULL"
                      label="income" :disabled="true"
                      :type="InputTypes.NUMBER"/>
          </div>
          <div class="row">
            <InputBox :model-value="store.profitabilityCalculation.profitability.toString()" :size="InputSizes.FULL"
                      label="profitability" :disabled="true"
                      :type="InputTypes.NUMBER"/>
          </div>
          <div class="row">
            <div class="col-md-12">
              <Button :type="ButtonTypes.SUBMIT" text="back_to_calculator"/>
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<style scoped>

</style>