<script>
import InputBox from "@/components/InputBox.vue";
import Button from "@/components/Button.vue";
import LoadingButton from "@/components/LoadingButton.vue";
import {ButtonTypes, InputSizes, InputTypes} from "@/utils/enums";

export default {
  name: "LoginPage",
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
  components: {LoadingButton, Button, InputBox},
  methods: {
    onSubmit(e) {

    }
  }
}

</script>

<template>
  <div class="container login-page">
    <div class="row">
      <div class="general-form">
        <h5 class="title">Login</h5>
        <form @submit="onSubmit">
          <div class="row">
            <InputBox :size="InputSizes.FULL" label="Username" placeholder="Username"
                      :type="InputTypes.TEXT" :required="true"/>
          </div>
          <div class="row">
            <InputBox :size="InputSizes.FULL" label="Password" placeholder="Password"
                      :type="InputTypes.PASSWORD" :required="true"/>
          </div>
          <div class="row">
            <InputBox :size="InputSizes.FULL" label="Remember me"
                      :type="InputTypes.CHECKBOX"/>
          </div>
          <div class="row">
            <div class="col-md-12">
              <Button :type="ButtonTypes.SUBMIT" text="Log In"/>
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<style>
.login-page {
  max-width: 500px;
}

.login-page .form-btn {
  margin-top: 0;
}
</style>