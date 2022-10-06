<script>
//import { image } from "@/assets/images/truckexample.png";
// import Swal from "sweetalert2";

// import axios from "axios";
/**
 * Stat component
 */
export default {
  components: {},
  props: {
    src2: String,
    src3: String,
    overview: String,
  },
  data() {
    return {
      //src2: require("@/assets/images/truckexample.png"),
      //src: require("@/assets/images/truckexample.png"),
    };
  },
  methods: {
    getimg() {
      var theUrl =
        "http://" +
        window.$localurl +
        "/kenload/dashboard/grabimg.php?string=http://localhost:82/kenload/dashboard/img.php&filename=anprA.jpg&dir=202112";
      const logFileText = async (file) => {
        //alert(theUrl);
        const response = await fetch(file);
        let text = await response.text();
        //alert(text);
        this.src2 = text;
      };
      // alert(theUrl);
      logFileText(theUrl);
      this.getoverview();
      //alert(this.src2);
    },
    getoverview() {
      var theUrl =
        "http://" +
        window.$localurl +
        "/kenload/dashboard/getimg.php?string=http://192.168.5.53/jpg/image.jpg?size=3&filename=anpr.jpg&dir=dd";
      const logFileText = async (file) => {
        //alert(theUrl);
        const response = await fetch(file);
        let text = await response.text();
        //alert(text);
        this.src3 = text;
      };
      logFileText(theUrl);

      //alert(this.src2);
    },
  },
  mounted() {},
};
</script>

<template>
  <div class="row">
    <div class="col-md-6 col-xl-6">
      <!-- src="http://192.168.5.52/jpg/image.jpg?" -->
      <!-- src="@/assets/images/truckexample.png"
      :src="this.src"
       -->
      <div class="card-body">
        <img
          :src="'data:image/gif;base64,' + src2"
          class="rounded zoom"
          width="700"
          height="350"
          @click="getimg()"
        />
      </div>
    </div>
    <div class="col-md-6 col-xl-6">
      <div class="card-body">
        <img
          class="rounded zoom"
          :src="'data:image/gif;base64,' + src3"
          width="700"
          height="350"
        />
      </div>
    </div>
    <!-- end col-->
  </div>
  <!-- end row-->
</template>
<style scoped>
.zoom {
  transition: transform 0.2s; /* Animation */
  width: 700px;
  height: 400px;
  margin: 0 auto;
}

.zoom:hover {
  transform: scale(2.5); /* (150% zoom)*/
}
</style>