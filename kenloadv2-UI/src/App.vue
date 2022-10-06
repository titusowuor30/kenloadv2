<template>
  <div id="app">
    <router-view />
    <ModalIdle v-if="isIdle" />
  </div>
</template>

<script>
import appConfig from "@/app.config";
import { notificationMethods } from "@/state/helpers";
import ModalIdle from "@/components/modalIdle";
import store from "@/state/store";

export default {
  name: "app",
  components: {
    ModalIdle,
  },
  page: {
    // All subcomponent titles will be injected into this template.
    titleTemplate(title) {
      title = typeof title === "function" ? title(this.$store) : title;
      return title ? `${title} | ${appConfig.title}` : appConfig.title;
    },
  },
  data() {
    return {
      time: 10000,
    };
  },
  methods: {
    clearNotification: notificationMethods.clear,
    removeUser() {
      store.dispatch("authfack/logout");
    },
  },
  watch: {
    /**
     * Clear the alert message on route change
     */
    // eslint-disable-next-line no-unused-vars
    $route(to, from) {
      // clear alert on location change
      this.clearNotification();
    },
  },
  created() {
    console.log("created");
    window.addEventListener("beforeunload", this.removeUser);
  },
  computed: {
    isIdle() {
      return this.$store.state.idleVue.isIdle;
    },
  },
  mounted() {
     this.$store.dispatch("screens/clearScreens");

    // document.getElementsByTagName("html")[0].setAttribute("dir", "rtl");
  },
};
</script>
<style>
@font-face {
  font-family: "digital-7";
  src: local("digital-7"), url(/src/assets/fonts/modern_lcd-7.ttf),
    format("truetype");
}
.col-12{
  word-wrap: break-word;
}
#app {
  font-family: Roboto, Helvetica, Arial, sans-serif;
}
</style>