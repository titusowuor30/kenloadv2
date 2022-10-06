<template>
  <div class="loading">
    <h2>Loading Kenloadv2</h2>
    <span></span>
    <span></span>
    <span></span>
    <span></span>
    <span></span>
    <span></span>
    <span></span>
  </div>
</template>

<script>
export default {};
</script>

<style >
/*
 * Baseline styles
 */
body {
  background: white;
  text-align: center;
  padding: 20%;
}
h2 {
  color: black;
  margin: 0;
  font: 0.8em verdana;
  text-transform: uppercase;
  letter-spacing: 0.1em;
}

/*
 * Loading Dots
 * Can we use pseudo elements here instead :after?
 */
.loading span {
  display: inline-block;
  vertical-align: middle;
  width: 0.6em;
  height: 0.6em;
  margin: 0.19em;
  background: black;
  border-radius: 0.6em;
  animation: loading 1s infinite alternate;
}

/*
 * Dots Colors
 * Smarter targeting vs nth-of-type?
 */
.loading span:nth-of-type(2) {
  background: black;
  animation-delay: 0.2s;
}
.loading span:nth-of-type(3) {
  background: black;
  animation-delay: 0.4s;
}
.loading span:nth-of-type(4) {
  background: black;
  animation-delay: 0.6s;
}
.loading span:nth-of-type(5) {
  background: black;
  animation-delay: 0.8s;
}
.loading span:nth-of-type(6) {
  background: black;
  animation-delay: 1s;
}
.loading span:nth-of-type(7) {
  background: black;
  animation-delay: 1.2s;
}

/*
 * Animation keyframes
 * Use transition opacity instead of keyframes?
 */
@keyframes loading {
  0% {
    opacity: 0;
  }
  100% {
    opacity: 1;
  }
}
</style>



