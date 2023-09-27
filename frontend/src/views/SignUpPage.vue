<script>
import InputBox from "@/components/InputBox.vue";
import Button from "@/components/Button.vue";
import LoadingButton from "@/components/LoadingButton.vue";
import {ButtonTypes, InputSizes, InputTypes} from "@/utils/enums";

export default {
  name: "SignUpPage",
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
  <div class="container signup-page">
    <div class="row">
      <div class="general-form">
        <h5 class="title">Sign Up</h5>
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
            <InputBox :size="InputSizes.FULL" label="Re-Password" placeholder="Password"
                      :type="InputTypes.PASSWORD" :required="true"/>
          </div>
          <div class="row">
            <div class="col-md-12">
              <Button :type="ButtonTypes.SUBMIT" text="Sign Up"/>
            </div>
          </div>
          <div class="row">
            <div class="col-md-12">
              <router-link class="sign-in-link" :to="{ name: 'login'}">
                <div class="sign-in-label">Already have an account? Login</div>
              </router-link>
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<style scoped>
.signup-page {
  max-width: 500px;
}

.signup-page .general-form .form-btn {
  margin-top: 10px;
}

.sign-in-link {
  text-decoration: none;
}

.sign-in-label {
  margin-top: 15px;
  margin-left: 20px;
  margin-bottom: 5px;
  text-transform: uppercase;
  font-size: 12px;
  color: white;
}

.sign-in-label:hover {
  color: #ffb3b3;
}
</style>