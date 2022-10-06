<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import Multiselect from "vue-multiselect";
import Swal from "sweetalert2";
import axios from "axios";
import store from "@/state/store";

/**
 * Elements component
 */
export default {
  page: {
    title: "System Settings",
    meta: [
      {
        name: "description",
        content: appConfig.description,
      },
    ],
  },
  components: { Layout, PageHeader, Multiselect },
  data() {
    return {
      origins: [],
      origin: "",
      destination: "",
      hswim: "",
      hswims: ["Camea", "Bosvark"],
      id: 1,
      station: "",
      stations: [],
      station2: "",
      camera: "",
      cameras: ["hikvision"],
      viewSystemSettings: false,
      editSystemSettings: false,
      title: "System Settings",
      items: [
        {
          text: "System",
          href: "/",
        },
        {
          text: "System Settings",
          active: true,
        },
      ],
      checked: true,
      smchecked: true,
      lgchecked: true,
      selectedToogle: "A",
      selectedDefault: "a",
      rightcheck: "accepted",
      status: "accepted",
      orderData: [],
    };
  },
  middleware: "authentication",
  watch: {
    viewSystemSettings(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Opened system settings:${newValue}`,
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
    editSystemSettings(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Edited system settings:${newValue},value:(${this.station})`,
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
    //alert(window.$permiturl);
    // Show a pop up if it works
    // alert(location.host);

    // Set the initial number of items
    this.updatearray();
    this.getorigin();
    this.viewSystemSettings = true;
  },
  methods: {
    getorigin() {
      this.origins = [];
      //alert(window.$http + `Origins/search?limit=100000`);
      axios
        .get(window.$http + `Origins/search?limit=100000`, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          // this.cargos = response.data.cargo_name;
          response.data.forEach((e) => {
            this.origins.push(e.origin_name.toUpperCase());
          });

          Swal.close();
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Origins" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
    },
    getstation() {
      this.stations = [];
      axios
        .get(window.$http + `ClusterWeighbridges`, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          // this.cargos = response.data.cargo_name;
          response.data.forEach((e) => {
            this.stations.push(e.cluster_name.toUpperCase());
          });

          this.station = window.$station;
          //alert(window.$station2);
          this.station2 = window.$station2;
          Swal.close();
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "cluster " + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
    },
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
      this.getstation();
      this.origin = window.$origin;
      this.destination = window.$destination;
      this.hswim = window.$hswim;
      this.camera = store.getters["screens/getcameratype"];

      //Swal.close;
    },
    handleSubmit() {
      console.log("Error on submit");
    },

    async save() {
      var stationcode = "";
      var stationcode2 = "";
      window.$stationcode2 = "";
      window.$station2 = "";
      // alert(this.station2);

      stationcode2 = await axios
        .get(
          window.$http +
            `ClusterWeighbridges/search?cluster_name=` +
            this.station2,
          {
            headers: window.$headers,
          }
        )
        .then((resp) => {
          stationcode2 = resp.data[0].cluster_code;
          window.$stationcode2 = stationcode2;
          window.$station2 = this.station2;

          return stationcode2;
        })
        .catch(() => {
          // alert("error" + this.stationcode2);
          return "";
        });
      // alert(" --- " + stationcode2);
      stationcode = await axios
        .get(
          window.$http +
            `ClusterWeighbridges/search?cluster_name=` +
            this.station,
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          stationcode = response.data[0].cluster_code;
          window.$stationcode = stationcode;
          window.$station = this.station;
          return stationcode;
        })
        .catch(() => {
          return "";
        });

      const data = {
        id: this.id,
        stationcode: stationcode,
        stationcode2: stationcode2,
        origin: this.origin,
        destination: this.destination,
        hswim: this.hswim,
        camera: this.camera,
      };

      console.log(data);
      this.editSystemSettings = true;
      //alert(window.$http);
      Swal.fire({
        title: "Kenload V2 . Please Wait !",
        html: "Saving Data!", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      axios
        .put(window.$http + `systemsettings/` + this.id, data, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.

          //   console.log(response.data);
          //   window.$permiturl = this.permiturl;
          Swal.close();
          Swal.fire({
            position: "center",
            icon: "success",
            title: "Your work has been saved" + response.data,
            showConfirmButton: false,
            timer: 2000,
            allowOutsideClick: false,
          }).then(() => {
            this.$router.push("systemsettings");
            this.clearvalues();
            this.updatearray();
          });
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "" + e,
            showConfirmButton: true,
          }).then(() => {
            Swal.close();
          });
        });
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <div class="row mt-4">
      <div class="col-12">
        <div class="card">
          <div class="card-body">
            <div class="row">
              <div class="col-12">
                <form class="form-horizontal" role="form">
                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text textred col-sm-3">
                        Default Station:
                      </div>
                      <multiselect
                        @input="save()"
                        v-model="station"
                        :options="stations"
                        class="form-control"
                      ></multiselect>
                      <b-button
                        v-b-modal.modal-Edit
                        pill
                        variant="primary"
                        @click="getstation()"
                        >...
                      </b-button>
                    </div>
                  </div>
                  <!--  -->
                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text textred col-sm-3">
                        Bi-direction Station:
                      </div>
                      <multiselect
                        @input="save()"
                        v-model="station2"
                        :options="stations"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text textred col-sm-3">
                        Default Origin:
                      </div>
                      <multiselect
                        v-model="origin"
                        :options="origins"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text textred col-sm-3">
                        Default Destination:
                      </div>
                      <multiselect
                        v-model="destination"
                        :options="origins"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>
                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text textred col-sm-3">
                        HSWIM Name:
                      </div>
                      <multiselect
                        v-model="hswim"
                        :options="hswims"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>
                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text textred col-sm-3">
                        Camera:
                      </div>
                      <multiselect
                        v-model="camera"
                        :options="cameras"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>
                  <div class="col-12" style="width: 95%">
                    <b-button
                      @click="save()"
                      variant="success"
                      style="
                        background-color: green;
                        color: yellow;
                        float: right;
                      "
                      ref="Save"
                    >
                      Save
                      <i class="uil uil-angle-double-right ms-2"></i>
                    </b-button>
                  </div>
                </form>
                <div class="mt-4">
                  <!-- <button @click="save()" class="btn btn-primary w-md">
                    Save
                  </button> -->
                </div>
              </div>
            </div>
            <!-- end row -->
          </div>
        </div>
        <!-- end card -->
      </div>
      <!-- end col -->
    </div>
    <!-- end row -->
  </Layout>
</template>
