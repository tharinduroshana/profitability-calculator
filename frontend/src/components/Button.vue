<script>
/*
* Button Component
* @text: Text key for the button component
* @type: Type of the button element
* */
import {ButtonTypes} from "@/utils/enums";

export default {
  name: "Button",
  props: {
    text: String,
    type: {
      default: ButtonTypes.BUTTON
    }
  }
}
</script>

<template>
  <div class="form-btn">
    <button :type="type" @click="$emit('btn-click')" class="submit-btn">{{$t(text)}}</button>
  </div>
</template>

<style scoped>

</style>