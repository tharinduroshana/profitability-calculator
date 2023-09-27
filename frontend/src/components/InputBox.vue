<script>
import {InputSizes} from "@/utils/enums";
import {InputTypes} from "@/utils/enums";
import InputError from "@/components/InputError.vue";

/*
* InputBox Component
* @label: Label for the Input Box
* @size: Applies as the class name to declare size of the input field
* @type: Type of the input field
* @disabled: Sets disabled property for input element
* @placeholder: Placeholder text for the component
* @modelValue: Takes v-model value from parent component (Input value for input element).
* */

export default {
  name: "InputBox",
  components: {InputError},
  data() {
    return {
      invalidData: false,
      needValidation: false
    }
  },
  props: {
    label: String,
    size: {
      type: String,
      default: InputSizes.FULL
    },
    type: {
      type: String,
      default: InputTypes.TEXT
    },
    disabled: {
      type: Boolean,
      default: false
    },
    placeholder: String,
    modelValue: String
  },
  methods: {
    onInput(event) {
      const value = event.target.value;
      this.invalidData = event.target.validity.badInput || event.target.value === "";
      this.$emit('update:modelValue', value)
    }
  }
}
</script>

<template>
  <div :class="size">
    <div class="form-group">
      <span class="form-label">{{ label }}</span>
      <input :class="['form-control', disabled ? 'disabled-input' : '', invalidData ? 'error-input-box' : '']" :value="modelValue" :type="type"
             :disabled="disabled"
             @input="onInput($event)"
             :placeholder="placeholder">
      <InputError v-show="invalidData" message="Invalid Input: Please enter a valid number" />
    </div>
  </div>
</template>

<style scoped>

</style>