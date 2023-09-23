<script>
/*
* Button Component
* @text: Text for the button component
* */
export default {
  name: "Button",
  props: {
    text: String
  }
}
</script>

<template>
  <div class="form-btn">
    <button class="submit-btn">{{text}}</button>
  </div>
</template>

<style scoped>

</style>