<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import Stat from "./stat.vue";

import AxelAnalytics from "./axel-analytics";

import AxelLoad from "./axel-load";
import axios from "axios";
//import store from "@/state/store";

export default {
  page: {
    title: "Dashboard",
    meta: [
      {
        name: "description",
        content: appConfig.description,
      },
    ],
  },
  components: {
    Layout,
    PageHeader,
    Stat,
    AxelAnalytics,
    AxelLoad,
  },

  data() {
    return {
      //screens:store.getters["screens/getUserScreens"],
      title: "Dashboard",
      viewDashboard: false,
      prevRoute: null,
      time: 10000,
      files: "",
      items: [
        {
          text: "Kenload",
        },
        {
          text: "Dashboard",
          active: true,
        },
      ],
    };
  },
  watch: {
    viewDashboard(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Opened Dashboard:${newValue}`,
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
  mounted() {
    this.viewDashboard = true;
  },
  methods: {
    submitFiles() {
      axios.defaults.headers.common["Access-Control-Allow-Origin"] = "*";
      let formData = new FormData();

      /*
          Iteate over any file sent over appending the files
          to the form data.
        */
      for (var i = 0; i < this.files.length; i++) {
        let file = this.files[i];

        formData.append("files[" + i + "]", file);
      }
      axios
        .post(window.$http + "UploadFile", formData, {
          headers: {
            "Content-Type": "multipart/form-data",
            Authorization: `Bearer ${window.$tokenString}`,
          },
        })
        .then(function() {
          console.log("SUCCESS!!");
        })
        .catch((e) => {
          console.log(e);
        });
    },
    handleFilesUpload() {
      this.files = this.$refs.files.files;
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" class="d-inline" />
    <Stat />
    <div class="row">
      <AxelAnalytics />
      <div class="col-xl-4">
        <div class="card bg-primary">
          <div class="card-body">
            <div class="row align-items-center">
              <div class="col-sm-12">
                <p class="text-light font-size-18">
                  Enhance our
                  <b>Axle </b>Load Control for effective operation on
                  weighbridge
                  <i class="mdi mdi-arrow-right"></i>
                </p>
                <div class="mt-4">
                  <a
                    style="
                      background-color: yellow;
                      color: black;
                      border: none;
                      shadow: none;
                    "
                    href="javascript: void(0);"
                    class="btn btn-success waves-effect waves-light"
                    >Axle loads</a
                  >
                </div>
              </div>
            </div>
          </div>
        </div>

        <AxelLoad />
      </div>
    </div>
  </Layout>
</template>