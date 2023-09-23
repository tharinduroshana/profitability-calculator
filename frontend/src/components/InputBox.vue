<script>
import {InputSizes} from "@/utils/enums";
import {InputTypes} from "@/utils/enums";

/*
* InputBox Component
* @label: Label for the Input Box
* @size: Applies as the class name to declare size of the input field
* @type: Type of the input field
* @placeholder: Placeholder text for the component
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
    placeholder: String
  }
}
</script>

<template>
  <div :class="size">
    <div class="form-group">
      <span class="form-label">{{ label }}</span>
      <input class="form-control" :type="type" :placeholder="placeholder">
    </div>
  </div>
</template>

<style scoped>

</style>