<script>
import LanguageSelector from "@/components/LanguageSelector.vue";
import {useUserAuthStore} from "@/store/UserAuthStore";

export default {
  name: "Header",
  data() {
    return {
      showLogoutButton: false
    }
  },
  methods: {
    logOut() {
      const authStore = useUserAuthStore();
      authStore.logoutUser();
      this.$router.push({name: "login", replace: true});
    }
  },
  components: {LanguageSelector},
  computed: {
  },
  created() {
    const authStore = useUserAuthStore();
    this.showLogoutButton = authStore.user !== null
  }
}
</script>

<template>
  <div class="selector-div">
    <button @click="logOut()" v-show="showLogoutButton" class="log-out-btn">Log Out</button>
    <LanguageSelector />
  </div>
</template>

<style scoped>
.selector-div {
  display: flex;
  justify-content: end;
  margin-bottom: 10px;
}

.log-out-btn {
  border-radius: 8px;
  border-width: 0;
  color: #333333;
  cursor: pointer;
  display: inline-block;
  font-size: 12px;
  margin-right: 5px;
  font-weight: 500;
  line-height: 9px;
  list-style: none;
  padding: 10px 12px;
  text-align: center;
  transition: all 200ms;
  vertical-align: baseline;
  white-space: nowrap;
  user-select: none;
  -webkit-user-select: none;
  touch-action: manipulation;
}
</style>