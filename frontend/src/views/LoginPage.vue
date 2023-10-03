<script xmlns="http://www.w3.org/1999/html">
import InputBox from "@/components/InputBox.vue";
import Button from "@/components/Button.vue";
import LoadingButton from "@/components/LoadingButton.vue";
import {ButtonTypes, InputSizes, InputTypes} from "@/utils/enums";
import {useUserAuthStore} from "@/store/UserAuthStore";
import {usePopUpAlertStore} from "@/store/PopUpAlertStore";
import Header from "@/components/Header.vue";

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
  data() {
    return {
      username: "",
      password: "",
      rememberMe: false
    }
  },
  components: {Header, LoadingButton, Button, InputBox},
  methods: {
    async onSubmit(e) {
      e.preventDefault();
      if (this.validateUserInputs()) {
        const status = await this.userStore.loginUser({
          username: this.username,
          password: this.password
        });
        if (status === 200) {
          this.$router.push({name: "calculator", replace: true});
        } else if (status === 500) {
          this.alertStore.showPopUpAlert("internal_server_error");
        } else {
          this.alertStore.showPopUpAlert("auth_failure");
        }
      } else {
        this.alertStore.showPopUpAlert("fields_cannot_be_empty");
      }
    },
    validateUserInputs() {
      return !(!this.username && !this.password);
    }
  },
  setup() {
    const userStore = useUserAuthStore();
    const alertStore = usePopUpAlertStore();
    return { userStore, alertStore }
  }
}

</script>

<template>
  <div class="container login-page">
    <div class="row">
      <div class="general-form">
        <Header />
        <h5 class="title">{{ $t('login') }}</h5>
        <form @submit="onSubmit">
          <div class="row">
            <InputBox v-model="username" :size="InputSizes.FULL" label="username" placeholder="username"
                      :type="InputTypes.TEXT" :required="true"/>
          </div>
          <div class="row">
            <InputBox v-model="password" :size="InputSizes.FULL" label="password" placeholder="password"
                      :type="InputTypes.PASSWORD" :required="true"/>
          </div>
          <div class="row">
            <div class="col-md-12">
              <Button :type="ButtonTypes.SUBMIT" text="log_in"/>
            </div>
          </div>
          <div class="row">
            <div class="col-md-12">
              <router-link class="sign-up-link" :to="{ name: 'signup'}">
                <div class="sign-up-label">{{ $t('signup_text') }}</div>
              </router-link>
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
  margin-top: 20px;
}

.sign-up-label {
  margin-top: 15px;
  margin-left: 20px;
  margin-bottom: 5px;
  text-transform: uppercase;
  font-size: 12px;
  color: white;
}

.sign-up-label:hover {
  color: #ffb3b3;
}

.sign-up-link {
  text-decoration: none;
}
</style>