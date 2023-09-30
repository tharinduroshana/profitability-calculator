<script>
import {InputSizes, InputTypes} from "@/utils/enums";
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
  computed: {
    InputTypes() {
      return InputTypes
    }
  },
  components: {InputError},
  data() {
    return {
      invalidData: false
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
    modelValue: String,
    required: false,
    needValidation: false
  },
  methods: {
    onInput(event) {
      const value = event.target.value;
      this.invalidData = this.needValidation && (event.target.validity.badInput || event.target.value === "" || event.target.value < 0);
      this.$emit('update:modelValue', value)
    }
  },
}
</script>

<template>
  <div :class="size">
    <div class="form-group checkbox-group" v-if="type === InputTypes.CHECKBOX">
      <input
          :type="type"
          :disabled="disabled"
          name="checkbox"
          @input="onInput($event)"
          :placeholder="placeholder" :required="required">
      <label class="form-checkbox">{{ label }}</label>
    </div>
    <div class="form-group" v-else>
      <span class="form-label">{{ label }}</span>
      <input
          :class="['form-control', disabled ? 'disabled-input' : '', needValidation && invalidData ? 'error-input-box' : '']"
          :value="modelValue" :type="type"
          :disabled="disabled"
          @input="onInput($event)"
          :placeholder="placeholder" :required="required">
      <InputError v-show="needValidation && invalidData" message="Invalid Input: Please enter a valid number"/>
    </div>
  </div>
</template>

<style scoped>
.general-form .form-checkbox {
  color: white;
  padding: 0 10px;
  text-transform: uppercase;
  font-size: 12px;
}

.checkbox-group {
  display: flex;
  align-items: center;
  margin-left: 25px;
}
</style>