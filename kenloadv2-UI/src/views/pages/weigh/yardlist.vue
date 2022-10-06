<script>
import Layout from "../../layouts/main";
//import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";
import yarddetails from "@/components/widgets/weigh/yarddetails";
import axios from "axios";
//import Deckweights from "@/components/widgets/weigh/deckweights";
//import Vehicledetailsform from "@/components/widgets/weigh/vehicledetailsform";

export default {
  page: {
    title: "Yard List",
    meta: [
      {
        name: "description",
        content: appConfig.description,
      },
    ],
  },

  components: {
    Layout,
    yarddetails,
    //Deckweights,
    //Vehicledetailsform,
  },
  watch: {
    viewYardList(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Opened yard list:${newValue}`,
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
  created() {
    this.nplate = this.$route.params.nplate;
    this.viewYardList = true;
    //alert(this.nplate);
  },
  data() {
    return {
      nplate: "",
      viewYardList: false,
      title: "Yard List",
      items: [
        {
          text: "USER: ",
        },
        {
          text: "Yard List",
          active: true,
        },
      ],
    };
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <h1>Yard List for {{ nplate }}</h1>
    <div></div>
    <yarddetails :nplate="nplate" />
  </Layout>
</template>