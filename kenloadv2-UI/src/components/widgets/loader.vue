<script>
/**
 * Stat component
 */
export default {
  components: {},
  data() {
    return {};
  },
  methods: {
    close() {
      this.$emit("close");
    },

    validateLoginForm() {
      //Login Form Validations go here
    },

    clearAllLoginValidationErrors() {
      this.loginValidationAlerts = [];
    },

    attemptLogin() {
      // Your login code
    },
  },
};
</script>

<template>
  <div class="row">
    <b-modal
      id="modal-loading"
      :title="modaltitle"
      hide-footer
      size="lg"
      centered
      v-show="isModalVisible"
    >
      <div class="loader">
        <span>L</span>
        <span>O</span>
        <span>A</span>
        <span>D</span>
        <span>I</span>
        <span>N</span>
        <span>G</span>

        <div class="covers">
          <span></span>
          <span></span>
          <span></span>
          <span></span>
          <span></span>
          <span></span>
          <span></span>
        </div>
      </div>
    </b-modal>
  </div>
  <!-- end row-->
</template>
<style scoped>
@import url(https://fonts.googleapis.com/css?family=Roboto+Condensed);

* {
  box-sizing: border-box;
  overflow: hidden;
}

body {
  padding-top: 10em;
  text-align: center;
}

.loader {
  position: relative;
  margin: auto;
  width: 175px;
  color: yellow;
  font-family: "Roboto Condensed", sans-serif;
  font-size: 100%;
  background: black;
  box-shadow: inset 0 5px 20px black;
  text-shadow: 5px 5px 5px rgba(0, 0, 0, 0.3);
}

.loader:after {
  content: "";
  display: table;
  clear: both;
}

span {
  float: left;
  height: 50px;
  line-height: 70px;
  width: 25px;
}

.loader > span {
  border-left: 1px solid #444;
  border-right: 1px solid #222;
}

.covers {
  position: absolute;
  height: 100%;
  width: 100%;
}

.covers span {
  background: linear-gradient(180deg, white 0, #ddd 100%);
  animation: up 2s infinite;
}

@keyframes up {
  0% {
    margin-bottom: 0;
  }
  16% {
    margin-bottom: 100%;
    height: 20px;
  }
  50% {
    margin-bottom: 0;
  }
  100% {
    margin-bottom: 0;
  }
}

.covers span:nth-child(2) {
  animation-delay: 0.142857s;
}
.covers span:nth-child(3) {
  animation-delay: 0.285714s;
}
.covers span:nth-child(4) {
  animation-delay: 0.428571s;
}
.covers span:nth-child(5) {
  animation-delay: 0.571428s;
}
.covers span:nth-child(6) {
  animation-delay: 0.714285s;
}
.covers span:nth-child(7) {
  animation-delay: 0.857142s;
}
</style>
