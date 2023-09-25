<script>
import Button from "@/components/Button.vue";

/*
* Loading button
* @text: Text for the loading button component
*
* This component visualizes a loading button to show the users something's working in the background
* */
export default {
  name: "LoadingButton",
  props: {
    text: {
      type: String,
    }
  },
  components: {Button}
}
</script>

<template>
  <div class="form-btn">
    <button class="submit-btn">
      <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
      {{ text }}
    </button>
  </div>
</template>

<style scoped>

</style>