<script>
// import Layout from "../../layouts/main";
// import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";
import Hswimimages from "@/components/widgets/weigh/hswimimages";
import Swal from "sweetalert2";

import axios from "axios";

export default {
  page: {
    title: "HSWIM ",
    meta: [
      {
        name: "hswim",
        content: appConfig.description,
      },
    ],
  },
  components: {
    // Layout,
    // PageHeader,
    Hswimimages,
  },
  watch: {
    viewHSWIMImages() {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Loaded HSWIM Images`,
        computer: window.localStorage.clientip,
      };
      axios
        .post(window.$http + "AuditLogs", data, { headers: window.$headers })
        .then((response) => {
          console.log(response.data);
        })
        .catch((e) => {
          console.log(e);
        });
    },
  },
  data() {
    return {
      title: "HSWIM",
      viewHSWIMImages: false,
      src: "@/assets/images/truckexample.png",
      items: [
        {
          text: "Mlolongo",
        },
        {
          text: "Mombasa Bound",
          active: true,
        },
      ],
      hswimData: [
        // {
        //   annpassist: "KBB000A",
        //   lp: "KBB000A",
        //   lp_back: "KBB000A",
        //   velocity: "20",
        //   gap: "25",
        //   axles: 2,
        //   cid: 253456,
        //   overload: true,
        // },
        // {
        //   vehiclePlateNo: "KBC000B",
        //   speed: "33",
        //   gap: "15",
        //   axles: 4,
        //   refID: 253457,
        //   overload: false,
        // },
        // {
        //   vehiclePlateNo: "KBD000L",
        //   speed: "22",
        //   gap: "24",
        //   axles: 2,
        //   refID: 253455,
        //   overload: false,
        // },
        // {
        //   vehiclePlateNo: "KBJ000Z",
        //   speed: "23",
        //   gap: "14",
        //   axles: 3,
        //   refID: 253459,
        //   overload: false,
        // },
      ],
    };
  },
  computed: {},
  mounted() {
    //this.updatearray();
    this.viewHSWIMImages = true;
  },
  methods: {
    updatearray() {
      //screen and roles combination
      //alert(window.$http);
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      axios
        .get(window.$http + `virtualtickets`, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.hswimData = response.data;
          this.viewHSWIMImages = true;
          //console.log(this.orderData);

          Swal.close();
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
    },
  },
  middleware: "authentication",
};
</script>

<template>
  <!-- <Layout>
    <PageHeader :title="title" :items="items" /> -->
  <Hswimimages :hswimData="hswimData" :src="src" />
  <!-- </Layout> -->
</template>

