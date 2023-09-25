<script>
import {InputSizes} from "@/utils/enums";
import {InputTypes} from "@/utils/enums";

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
}
</script>

<template>
  <div :class="size">
    <div class="form-group">
      <span class="form-label">{{ label }}</span>
      <input :class="['form-control', disabled ? 'disabled-input' : '']" :value="modelValue" :type="type"
             :disabled="disabled"
             @input="$emit('update:modelValue', $event.target.value)"
             :placeholder="placeholder">
    </div>
  </div>
</template>

<style scoped>

</style>