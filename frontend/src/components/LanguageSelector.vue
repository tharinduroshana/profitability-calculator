<script>
import {messages} from "@/utils/languages";

export default {
  name: "LanguageSelector",
  data() {
    return {
      locale: String
    }
  },
  computed: {
    messages() {
      return messages
    }
  },
  methods: {
    onLocaleClick(localeItem) {
      this.locale = localeItem;
      this.$i18n.locale = localeItem;
      sessionStorage.setItem("locale", localeItem);
    }
  },
  created() {
    const currentLocale = sessionStorage.getItem("locale") || "EN";
    this.locale = currentLocale;
    this.$i18n.locale = currentLocale;
  }
}
</script>

<template>
  <div class="dropdown">
    <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown"
            aria-expanded="false">
      {{ locale || 'EN' }}
    </button>
    <ul class="dropdown-menu">
      <li v-for="localeItem in Object.keys(messages)"><a @click="onLocaleClick(localeItem)" class="dropdown-item"
                                                         href="#">{{ localeItem }}</a></li>
    </ul>
  </div>
</template>

<style scoped>
.dropdown-menu {
  min-width: 60px !important;
  font-size: 10px !important;
}

.dropdown-toggle {
  background: white;
  color: #191a1e;
  font-size: 10px;
}
</style>