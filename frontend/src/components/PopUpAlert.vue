<script>
import {usePopUpAlertStore} from "@/store/PopUpAlertStore";
import Button from "@/components/Button.vue";
import {ButtonTypes} from "@/utils/enums";

export default {
  name: "PopUpAlert",
  computed: {
    ButtonTypes() {
      return ButtonTypes
    }
  },
  components: {Button},
  data() {
    return {
      message: "",
      onClose: Function,
      buttonText: ""
    }
  },
  created() {
    this.message = this.alertStore.message
    this.onClose = this.alertStore.onClose ? this.alertStore.onClose : () => this.alertStore.hidePopUpAlert();
    this.buttonText = this.alertStore.buttonText;
  },
  setup() {
    const alertStore = usePopUpAlertStore();
    return { alertStore }
  }
}
</script>

<template>
  <div class="popup">
    <div class="popup-inner">
      <div class="container signup-page">
        <div class="row">
          <div class="general-form">
            <p>{{ message }}</p>
            <div class="row">
              <div class="col-md-12">
                <Button @btn-click="onClose()" :type="ButtonTypes.SUBMIT" :text="buttonText"/>
              </div>
            </div>
          </div>
        </div>
      </div>

    </div>
  </div>
</template>

<style scoped>
.popup {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  z-index: 99;
  background-color: rgb(93 93 93 / 80%);
  color: white;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 10px;
}

.general-form .form-btn {
  margin-top: 5px;
}

.popup-inner .general-form {
  background: rgba(0, 0, 0, 0.7);
  padding: 25px;
  border-radius: 6px;
}

.general-form .submit-btn {
  height: 25px;
}
</style>