<script>
import Layout from "../../layouts/main";
//import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";
//import Axleweights from "@/components/widgets/weigh/axleweights";
//import Deckweights from "@/components/widgets/weigh/deckweights";
import Noplatecapture from "@/components/widgets/weigh/vehiclenoplate";
import changeweighbridge from "@/components/widgets/weigh/changeweighbridge";
import axios from "axios";
// import Deckimages from "@/components/widgets/weigh/deckimages";

//import Swal from "sweetalert2";

export default {
  nplate: "tttt",
  deck1: 0,
  page: {
    title: "Weigh",
    meta: [
      {
        name: "description",
        content: appConfig.description,
      },
    ],
  },
  components: {
    Layout,
    Noplatecapture,
    changeweighbridge,
    // Deckweights,
    // Deckimages,
    //Axleweights,
  },
  data() {
    return {
      stoped: false,
      viewWeigh: false,
      weightype: "",
      decktimerun: 10000000000,
      axletimerun: 10000000000,
      cat: this.$route.params.cat,
      title: "Weight Capture",
      modaltitle: "Select Weighbridge",
      wb: "",
      wbs: ["A", "B"],
      items: [
        {
          text: "USER: Cow",
        },
        {
          text: "Weigh",
          active: true,
        },
      ],
    };
  },
  watch: {
    viewWeigh(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Opened Weighing screen:${newValue},selected ${this.weightype} weighing`,
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
  renderTracked() {
    //alert(this.$route.params.cat);
    this.cat = this.$route.params.cat;
  },
  created() {
    //alert(this.$route.params.cat);
    this.cat = this.$route.params.cat;
  },
  mounted() {
    this.viewWeigh = true;
    this.wb = window.$weighb;
    if (window.$weighb == "") {
      alert("Select Weighbridge!");
      //"dialog".modal("show");
      this.$refs.dialog.Modal;
    }
    //alert(this.$route.params.cat);
    this.cat = this.$route.params.cat;
    if (this.cat == "multideck") {
      this.decktimerun = 1000;
      this.weightype = "Multideck";
    }
    if (this.cat == "lswim") {
      this.axletimerun = 100;
      this.weightype = "LSWIM";
    }
  },

  // beforeUpdate: function () {
  //   alert(this.$route.params.cat);
  // },
  // Update: function () {
  //   alert(this.$route.params.cat);
  // },
  // beforeDestroy: function () {
  //   alert(this.$route.params.cat);
  // },
};
</script>

<template>
  <Layout>
    <!-- <Deckweights
      name="Deckweights"
      v-show="cat == 'multideck'"
      :timerun="decktimerun"
    />
    <Axleweights
      v-show="cat == 'lswim'"
      :timerun="axletimerun"
      :stoped="stoped"
    /> -->
    <!-- <Deckimages /> -->

    <Noplatecapture :cat="cat" />
    <changeweighbridge />
  </Layout>
</template>