<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import Swal from "sweetalert2";
import axios from "axios";
var CryptoJS = require("crypto-js");

export default {
  page: {
    title: "Roles",
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
  },
  props: {
  },
  data() {
    return {
      headers: null,
      uniqueCars: null,
      showme: true,
      pl: "",
      rname: "",
      description: "",
      selectedlists: [],
      title: "Dit Roles",
      items: [
        {
          text: "USER: Admin",
        },
        {
          text: "Role List",
          active: true,
        },
      ],
      orderData: [],
      editroleslogs:false,
      tokenString: CryptoJS.AES.decrypt(
        JSON.parse(localStorage.user).token,
        "mnopqr"
      )
        .toString(CryptoJS.enc.Utf8)
        .trim(), //
      myid: "test id",
      totalRows: 1,
      currentPage: 1,
      perPage: 10,
      pageOptions: [1, 10, 25, 50, 100],
      filter: null,
      filterOn: [],
      sortBy: "id",
      sortDesc: false,
      fields: [
        {
          key: "id",
          label: "#",
          sortable: true,
        },
        {
          key: "userrole_name",
          label: "Role Name",
          sortable: true,
        },
        {
          key: "userrole_desc",
          label: "Description",
          sortable: true,
        },

        {
          key: "screens",
          label: "Screen Access",
          sortable: true,
        },
        "action",
      ],
    lists: [],
    };
  },
    watch:{
     editroleslogs(newValue){
      var curentuser=JSON.parse(localStorage.user).email
      var currentdate=new Date();
    const data={
      datetime:currentdate,
      useremail:curentuser,
      application:window.navigator.userAgent,
      details:`Edit role:${newValue},\nrole id:${this.$route.params.id}`,
      computer:window.localStorage.clientip
    }
     axios
     .post(window.$http + "AuditLogs",data, { headers: window.$headers })
     .then(response=>{
       console.log(response.data)
     })
     .catch(e=>{
       console.log(e)
     })
    }
  },
  computed: {
    /**
     * Total no. of records
     */
    rows() {
      return this.orderData.length;
    },
  },
  created(){
  },
    mounted() {
    // Set the initial number of items
    this.focusInput();
    this.totalRows = this.items.length;
    this.fetchSelectedScreens();
    this.fetchscreens();
  },
  methods: {
      async fetchSelectedScreens() {
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
      await axios
        .get(window.$http + `UserRoles/${this.$route.params.id}`,{ headers: {"Authorization" : `Bearer ${CryptoJS.AES.decrypt(JSON.parse(localStorage.user).token,"mnopqr").toString(CryptoJS.enc.Utf8).trim()}`} })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.screenlist = response.data.screens.toString().split(',').map(s => s.trim());
          this.selectedlists=response.data.screens.toString().split(',').map(s => s.trim());
          this.rname=response.data.userrole_name
          this.description=response.data.userrole_desc
          this.editroleslogs=true;
          //console.log(response.data.screens.toString()).split(',').map(s => s.trim());

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
    fetchscreens() {
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
        .get(window.$http + `Screens`,{ headers: {"Authorization" : `Bearer ${CryptoJS.AES.decrypt(JSON.parse(localStorage.user).token,"mnopqr").toString(CryptoJS.enc.Utf8).trim()}`} })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.lists = [
             "dashboard",
             "weighMultideck",
             "weighLswim",
             "weighbridgeTickets",
             "hswim",
             "auditEvents",
             "tags",
             "yardList",
             "scaleTest",
             "manualWeighing",
             "setAllowableTolerance",
             "anprAssist",
             "roles",
             "passwordPolicy",
             "usersList",
             "changePassword",
             "auditLogs",
             "backupDatabase",
             "prosecution",
             "census",
             "wideLoadInspection",
             "yardVehicleInspection",
             "specialReleaseYardList",
             "specialRelease",
             "roadAuthority",
             "clusterWeighbridge",
             "cargo",
             "vehicles",
             "transporters",
             "drivers",
             "originDestination",
             "shifts",
             "vehicleMake",
             "axleConfiguration",
             "routes",
             "screens",
             "dollarrate",
             "reports",
             "cameraSettings",
             "prosecutionSettings",
             "permitSettings",
             "systemSettings",
             "healthCheck",
             "autoweigh",
             "calibrationCertificate"];
          this.screenlist = response.data;

          response.data.forEach((e) => {
            this.lists.push(e.screen_name);
          });
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
    focusInput() {
      this.$refs.rname.focus();
    },
    gettime() {
      //2021-06-11T09:05:53.07
      //2021-5-11T12:54.48
      var DateNow = new Date();
      var DateNowString;
      DateNow.setDate(DateNow.getDate() + 20);
      DateNowString =
        DateNow.getFullYear() +
        "-" +
        ("0" + (DateNow.getMonth() + 1)).slice(-2) +
        "-" +
        ("0" + DateNow.getDate()).slice(-2) +
        "T" +
        ("0" + DateNow.getHours()).slice(-2) +
        ":" +
        ("0" + DateNow.getMinutes()).slice(-2) +
        ":" +
        ("0" + DateNow.getSeconds()).slice(-2) +
        "." +
        ("0" + DateNow.getMilliseconds()).slice(-2);
      return DateNowString;
    },
     async edit() {
      await axios
        .put(window.$http + "UserRoles/"+this.$route.params.id, {
          id: this.$route.params.id,
          userrole_name: this.rname,
          userrole_desc: this.description,
          userrole_deletedstatus: true,
          screens:this.selectedlists.toString()
        },{ headers: {"Authorization" : `Bearer ${CryptoJS.AES.decrypt(JSON.parse(localStorage.user).token,"mnopqr").toString(CryptoJS.enc.Utf8).trim()}`} })
        .then(function (response) {
          //this.screenlist = response;
           console.log(response);
          Swal.fire({
            position: "center",
            icon: "success",
            title: "Your work has been saved",
            showConfirmButton: false,
            timer: 1500,
          }).then((result) => {
            result;
            this.clearvalues();
            var orderid = this.orderData.length + 1;
            this.orderData.push({
              id: orderid,
              name: this.rname,
              desc: this.description,
              screens: this.selectedlists,
            });
          });
          console.log(response);
        })
        .catch(function (error) {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Error on submmission. Check Servers.",
            showConfirmButton: false,
            timer: 1500,
          }).then((result) => {
            result;
            this.clearvalues();
          });
          console.log(error);
        });
    },
    //remove or add screens from tray methods
    oneToRight() {
      var select = document.getElementById("lists").value;
      if (select == undefined) {
        return;
      }

      if (select != "") {
        this.selectedlists.push(select);
        var del = this.lists.indexOf(select);
        this.lists.splice(del, 1);
      }
    },
    oneToLeft() {
      var select = document.getElementById("selectedlists").value;
      if (select == undefined) {
        return;
      }
      if (select != "") {
        this.lists.push(select);
        var del = this.selectedlists.indexOf(select);
        this.selectedlists.splice(del, 1);
      }
    },
    allToRight() {
      var del = this.lists.length;
      if (this.lists[0] == undefined) {
        return;
      }
      for (var i = 0; i <= this.lists.length; i++) {
        this.selectedlists.push(this.lists[i]);
      }
      this.lists.splice(0, del);
    },
    allToLeft() {
      if (this.selectedlists[0] == undefined) {
        return;
      }

      var del = this.selectedlists.length;
      for (var i = 0; i < this.selectedlists.length; i++) {
        this.lists.push(this.selectedlists[i]);
      }
      this.selectedlists.splice(0, del);
    },
    populate() {
      if (this.selectedlists.length <= 0) {
        Swal.fire({ title: "Please select a screen!" });
      }
      if (this.rname == "") {
        Swal.fire({ title: "Please enter role name!" });
      }

      if (this.description == "") {
        Swal.fire({ title: "Please enter description!" });
      }

      Swal.fire({
        position: "center",
        icon: "success",
        title: "Role name added" + this.rname,
        showConfirmButton: false,
        timer: 3500,
      });
      //this.selectedlists = this.lists;
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <div>
      <form @submit.prevent="edit()">
        <div class="card">
          <div class="card-body">
            <div class="card">
              <div class="card-body">
                <div class="col-sm-12">
                  <div class="input-group">
                    <div class="input-group-text col-sm-4">Role Name:</div>
                    <input
                      disabled="true"
                      ref="rname"
                      class="form-control"
                      placeholder="role Name"
                      v-model="rname"
                    />
                  </div>
                </div>

                <div class="col-sm-12">
                  <div class="input-group">
                    <div class="input-group-text col-sm-4">
                      Role Description:
                    </div>
                    <input
                      type="text"
                      class="form-control"
                      placeholder="Description"
                      v-model="description"
                    />
                  </div>
                </div>
                <div class="mt-3"></div>
                <div class="row">
                  <div class="col-sm">
                    <div class="">Select Screens / Windows / Page:</div>

                    <select class="form-control" size="15" id="lists" multiple>
                      <option
                        :id="index"
                        :value="list"
                        v-for="(list, index) in lists"
                        :key="list"
                      >
                        {{ list }}
                      </option>
                    </select>
                  </div>
                  <div class="col-sm">
                    <div class="row mb-4"></div>
                    <div class="row mb-4"></div>
                    <div class="row mb-4"></div>
                    <div class="row mb-2">
                      <button class="btn btn-secondary" @click="allToRight()">
                        Add All &raquo;
                      </button>
                    </div>

                    <div class="row mb-4">
                      <button class="btn btn-secondary" @click="oneToRight()">
                        Add Selected&rsaquo;
                      </button>
                    </div>
                    <div class="row mb-4"></div>
                    <div class="row mb-2">
                      <button class="btn btn-danger" @click="oneToLeft()">
                        &lsaquo; Remove Selected
                      </button>
                    </div>
                    <div class="row">
                      <button class="btn btn-danger" @click="allToLeft()">
                        &laquo; Remove All
                      </button>
                    </div>
                  </div>

                  <div class="col-sm">
                    <div>Selected Screens / Windows / Page:</div>
                    <select
                      class="form-control"
                      size="15"
                      id="selectedlists"
                      multiple
                    >
                      <option
                        :id="index"
                        v-for="(selectedlist, index) in selectedlists"
                        :value="selectedlist"
                        :key="selectedlist"
                      >
                        {{ selectedlist }}
                      </option>
                    </select>
                  </div>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-sm-10"></div>
              <div class="col-sm-2">
                <button
                  class="btn btn-primary"
                 type="submit"
                >
                  Edit Role
                </button>
              </div>
            </div>
          </div>
        </div>
      </form>
    </div>
  </Layout>
</template>
//#print {
  //height: 11in;
  //width: 8.5in;
//}
<style scoped>
.changebg {
  background-color: #f7f6ebfb;
}
.alert-custom {
  background-color: yellow;
}
.swal-overlay {
  background-color: rgba(43, 165, 137, 0.45);
}
.swal-modal {
  background-color: rgba(63, 255, 106, 0.69);
  border: 3px solid white;
}
</style>