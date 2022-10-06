<script>
export default {
  props: {
    title: {
      type: String,
      default: "",
    },
    data() {
      return {};
    },
    items: {
      type: Array,
      default: () => {
        return [];
      },
    },
  },
  computed: {
    getUserEmail() {
      return JSON.parse(localStorage.user).email;
    },
  },
  mounted() {
    window.$userid = JSON.parse(localStorage.user).email;
  },
  methods: {},
};
</script>

<template>
  <!-- start page title -->
  <div class="row m-auto">
    <div class="col-12 m-auto">
      <div
        class="page-title-box d-flex align-items-center justify-content-between"
      >
        <h4 class="mb-0">{{ title }}</h4>

        <div class="page-title-right d-flex bg-light mr-2">
          <div class="mr-2">
            <router-link
              :to="{ name: 'update-profile', params: { email: getUserEmail } }"
              v-b-modal.modal-1
              class="text-info border-2 rounded mr-auto fw-bolder blink_me"
              >View <span class="text-danger">Active</span> Shift
            </router-link>
          </div>
          &nbsp;
          <b-breadcrumb :items="items" class="ml-2"></b-breadcrumb>
        </div>
      </div>
    </div>
  </div>
  <!-- end page title -->
</template>
<style scoped>
.blink_me {
  animation: blinker 1s linear infinite;
}

@keyframes blinker {
  50% {
    opacity: 0;
  }
}
</style>
