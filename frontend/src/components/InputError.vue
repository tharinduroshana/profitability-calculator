<script>
export default {
  name: "InputError",
  props: {
    message: String
  }
}
</script>

<template>
  <div class="row">
    <div class="col-md-12">
      <span class="error-input">{{ message }}</span>
    </div>
  </div>
</template>

<style scoped>

</style>