<script>
import InputBox from "@/components/InputBox.vue";
import Button from "@/components/Button.vue";
import LoadingButton from "@/components/LoadingButton.vue";
import {ButtonTypes, InputSizes, InputTypes} from "@/utils/enums";
import {usePopUpAlertStore} from "@/store/PopUpAlertStore";
import {useUserAuthStore} from "@/store/UserAuthStore";

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
  data() {
    return {
      name: "",
      username: "",
      password: "",
      re_password: ""
    }
  },
  components: {LoadingButton, Button, InputBox},
  methods: {
    async onSubmit(e) {
      e.preventDefault();
      const valid = this.validateInputs();
      if (valid) {
        const response = await this.userStore.signUpUser({
          name: this.name,
          username: this.username,
          password: this.password
        });
        const status = response.status;
        if (status === 201) {
          this.alertStore.showPopUpAlertWithFunction(this.$t('registration_success'), this.$t('login'), () => this.routeToLogin())
        } else {
          this.alertStore.showPopUpAlert(this.$t('unknown_error'));
        }
      }
    },
    validateInputs() {
      if (!this.name && !this.username && !this.password && !this.re_password) {
        this.alertStore.showPopUpAlert(this.$t('empty_fields'));
        return false;
      }
      if (!this.matchUsername(this.username)) {
        this.alertStore.showPopUpAlert(this.$t('weak_username_strength'));
        return false;
      }
      if (!this.matchPassword(this.password)) {
        this.alertStore.showPopUpAlert(this.$t('weak_password_strength'));
        return false;
      }
      if (this.password !== this.re_password) {
        this.alertStore.showPopUpAlert(this.$t('un_matching_passwords'));
        return false;
      }
      return true;
    },
    matchPassword(password) {
      return new RegExp("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,}").test(password)
    },
    matchUsername(username) {
      return new RegExp("^(?=.*[a-zA-Z])[a-zA-Z0-9]{4,}$").test(username);
    },
    routeToLogin() {
      this.alertStore.hidePopUpAlert();
      this.$router.push({name: "login", replace: true});
    }
  },
  setup() {
    const alertStore = usePopUpAlertStore();
    const userStore = useUserAuthStore();
    return {alertStore, userStore}
  }
}

</script>

<template>
  <div class="container signup-page">
    <div class="row">
      <div class="general-form">
        <h5 class="title">{{ $t('signup') }}</h5>
        <form @submit="onSubmit">
          <div class="row">
            <InputBox v-model="name" :size="InputSizes.FULL" label="full_name" placeholder="full_name"
                      :type="InputTypes.TEXT" :required="true"/>
          </div>
          <div class="row">
            <InputBox v-model="username" :size="InputSizes.FULL" label="username" placeholder="username"
                      :type="InputTypes.TEXT" :required="true"/>
          </div>
          <div class="row">
            <InputBox v-model="password" :size="InputSizes.FULL" label="password" placeholder="password"
                      :type="InputTypes.PASSWORD" :required="true"/>
          </div>
          <div class="row">
            <InputBox v-model="re_password" :size="InputSizes.FULL" label="re_password" placeholder="password"
                      :type="InputTypes.PASSWORD" :required="true"/>
          </div>
          <div class="row">
            <div class="col-md-12">
              <Button :type="ButtonTypes.SUBMIT" text="signup"/>
            </div>
          </div>
          <div class="row">
            <div class="col-md-12">
              <router-link class="sign-in-link" :to="{ name: 'login'}">
                <div class="sign-in-label">{{ $t('have_account') }}</div>
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