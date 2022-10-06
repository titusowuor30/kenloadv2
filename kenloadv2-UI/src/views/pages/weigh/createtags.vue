<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import CreateVehicleTag from "@/components/widgets/weigh/tagcreate";
import axios from "axios";

export default {
  data() {
    return {
      title: "Create Tag",
      addTag: false,
      items: [
        {
          text: "Mlolongo",
        },
        {
          text: "Mombasa Bound",
          active: true,
        },
      ],
    };
  },
  components: {
    Layout,
    PageHeader,
    CreateVehicleTag,
  },
  watch: {
    addTag(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Added a Tag:${newValue}`,
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
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <CreateVehicleTag />
  </Layout>
</template>