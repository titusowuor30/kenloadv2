<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import Multiselect from "vue-multiselect";

import Swal from "sweetalert2";
import axios from "axios";
/**
 * Elements component
 */
export default {
  page: {
    title: "Prosecution Settings",
    meta: [
      {
        name: "description",
        content: appConfig.description,
      },
    ],
  },
  components: { Layout, Multiselect, PageHeader },
  data() {
    return {
      isModalVisible: false,
      viewProsecutionSettings: false,
      editProsecutionSettings: false,
      title: "Prosecution Settings",
      items: [
        {
          text: "Prosecution",
          href: "/",
        },
        {
          text: "Prosecution Settings",
          active: true,
        },
      ],
      orderData: [],
      checked: true,
      smchecked: true,
      lgchecked: true,
      selectedToogle: "A",
      selectedDefault: "a",
      rightcheck: "accepted",
      status: "accepted",
      polices: ["Select", "Paul Mutumba PC", "Paul PC"],
      id: 1,
      alongroad: "",
      district: "",
      weighbridge: "",
      county: "",
      court: "",
      complainant: "",
      police: "",
      inspector: "",
      movevehicle: "",
      VIs: [],
    };
  },
  watch: {
    viewProsecutionSettings(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Opened prosecution settings:${newValue}`,
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
    editProsecutionSettings(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Edited prosecution settings:${newValue}`,
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
  computed: {
    /**
     * Total no. of records
     */
    rows() {
      return this.orderData.length;
    },
  },
  mounted() {
    // Show a pop up if it works
    // alert(location.host);

    // Set the initial number of items
    this.updatearray();
    this.viewProsecutionSettings = true;
    this.fetchusers();
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
        .get(window.$http + `ProsecutionSettings`, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          console.log(this.orderData);
          //alert(this.orderData[0].id);
          this.id = this.orderData[0].id;
          this.alongroad = this.orderData[0].alongroad;
          this.district = this.orderData[0].district;
          this.weighbridge = this.orderData[0].weighbridge;
          this.county = this.orderData[0].county;
          this.court = this.orderData[0].court;
          this.complainant = this.orderData[0].complainant;
          this.police = this.orderData[0].police;
          this.inspector = this.orderData[0].inspector;
          this.movevehicle = this.orderData[0].movevehicle;
          Swal.close();
        })
        .catch((e) => {
          Swal.fire("Error!\n " + e);
          Swal.close();
          //throw e;
        });
    },
    handleSubmit() {
      console.log("Error on submit");
    },

    save() {
      //alert(this.alongroad);
      const data = {
        id: this.id,
        alongroad: this.alongroad,
        district: this.district,
        weighbridge: this.weighbridge,
        county: this.county,
        court: this.court,
        complainant: this.complainant,
        police: this.police,
        inspector: this.inspector,
        movevehicle: this.movevehicle,
      };
      console.log(data);
      //alert(window.$http);
      Swal.fire({
        title: "Kenload V2. Please Wait !",
        html: "Saving Data!", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      axios
        .put(window.$http + `prosecutionsettings/` + this.id, data, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          console.log(response.data);
          this.editProsecutionSettings = true;
          Swal.close();
          Swal.fire({
            position: "center",
            icon: "success",
            title: "Your work has been saved" + response.data,
            showConfirmButton: false,
            timer: 2000,
            allowOutsideClick: false,
          }).then(() => {
            this.clearvalues();
            this.updatearray();
          });
        })
        .catch((e) => {
          Swal.fire("" + e);
          //this.errors.push(e);
        });
    },
    fetchusers() {
      // Swal.fire({
      //   title: "Please Wait !",
      //   html: "Loading data...", // add html attribute if you want or remove
      //   allowOutsideClick: false,
      //   showConfirmButton: false,
      //   willOpen: () => {
      //     Swal.showLoading();
      //   },
      // });
      var UserRole = "Ins";
      // this.VIs = [];
      axios
        .get(window.$http + `Users/search?UserRole=` + UserRole, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          console.log(response);
          response.data.forEach((e) => {
            this.VIs.push(e.fullnames.toUpperCase());
          });
          //Swal.close();
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
        });
      UserRole = "Police";
      axios
        .get(window.$http + `Users/search?UserRole=` + UserRole, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          //this.tpolices = response.data;
          response.data.forEach((e) => {
            this.polices.push(e.fullnames.toUpperCase());
          });
          //Swal.close();
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
        });
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <div class="row">
      <div class="col-12">
        <div class="card">
          <div class="card-body">
            <h4 class="card-title">Location Details</h4>

            <div class="row">
              <div class="col-12">
                <form @submit.prevent="handleSubmit">
                  <b-form-group
                    class="mb-3"
                    id="example text"
                    label-cols-sm="2"
                    label-cols-lg="2"
                    label="Vehicle used along Road"
                    label-for="text"
                  >
                    <b-form-input
                      for="text"
                      value="A109"
                      v-model="alongroad"
                    ></b-form-input>
                  </b-form-group>

                  <b-form-group
                    id="example-search"
                    label-cols-sm="2"
                    label-cols-lg="2"
                    label="District"
                    label-for="search"
                    class="mb-3"
                  >
                    <b-form-input
                      id="search"
                      value="KILIFI DISTRICT"
                      v-model="district"
                      type="search"
                      name="search"
                    ></b-form-input>
                  </b-form-group>

                  <b-form-group
                    id="example-url"
                    label-cols-sm="2"
                    label-cols-lg="2"
                    label="County"
                    label-for="url"
                    class="mb-3"
                  >
                    <b-form-input
                      id="url"
                      value="KILIFI"
                      type="url"
                      name="url"
                      v-model="county"
                    ></b-form-input>
                  </b-form-group>
                </form>
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
    <div class="row mt-4">
      <div class="col-12">
        <div class="card">
          <div class="card-body">
            <h4 class="card-title">Court Details</h4>
            <div class="row">
              <div class="col-12">
                <form class="form-horizontal" role="form">
                  <b-form-group
                    class="mb-3"
                    id="example text"
                    label-cols-sm="2"
                    label-cols-lg="2"
                    label="Court"
                    label-for="text"
                  >
                    <b-form-input for="text" v-model="court"></b-form-input>
                  </b-form-group>

                  <b-form-group
                    id="example-search"
                    label-cols-sm="2"
                    label-cols-lg="2"
                    label="Complaint(Police)"
                    label-for="search"
                    class="mb-3"
                  >
                    <b-form-input
                      id="search"
                      value="REP THROUGH MTWAPA WEIGHBRIDGE POLICE"
                      type="search"
                      name="search"
                      v-model="complainant"
                    ></b-form-input>
                  </b-form-group>

                  <b-form-group
                    id="example-email"
                    label-cols-sm="2"
                    label-cols-lg="2"
                    label="Weigh Bridge"
                    label-for="email"
                    class="mb-3"
                  >
                    <b-form-input
                      id="email"
                      value="MTWAPA WEIGHBRIDGE"
                      v-model="weighbridge"
                    ></b-form-input>
                  </b-form-group>

                  <div class="mb-3 row">
                    <label class="col-md-2 col-form-label"
                      >Police / Traffic Officer</label
                    >

                    <div class="col-md-10">
                      <multiselect
                        v-model="police"
                        :options="polices"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>
                </form>
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
    <div class="row mt-4">
      <div class="col-12">
        <div class="card">
          <div class="card-body">
            <h4 class="card-title">Prohibitation Details</h4>

            <div class="row">
              <div class="col-12">
                <form class="form-horizontal" role="form">
                  <div class="mb-3 row">
                    <label class="col-md-2 col-form-label"
                      >Vehicle Inspector</label
                    >
                    <div class="col-md-10">
                      <multiselect
                        v-model="inspector"
                        :options="VIs"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>
                  <b-form-group
                    id="example-email"
                    label-cols-sm="2"
                    label-cols-lg="2"
                    label="Move Vehicle to"
                    label-for="email"
                    class="mb-3"
                  >
                    <b-form-input
                      id="email"
                      value="MTWAPA WEIGHBRIDGE"
                      v-model="movevehicle"
                    ></b-form-input>
                  </b-form-group>
                  <div class="mt-4"></div>
                </form>
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

    <button
      class="btn btn-primary waves-effect waves-light uil-save mb-3"
      @click="save()"
    >
      Save
    </button>
  </Layout>
</template>
