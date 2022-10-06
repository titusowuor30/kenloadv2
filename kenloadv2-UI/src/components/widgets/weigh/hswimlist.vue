<script>
//import Layout from "../../layouts/main";
//import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

//import DatePicker from "vue2-datepicker";

import axios from "axios";
import Swal from "sweetalert2";

export default {
  page: {
    title: "HSWIM",
    meta: [
      {
        name: "description",
        content: appConfig.description,
      },
    ],
  },
  props: {
    orderData: Array,
    filter: String,
  },
  components: {
    //Layout,
    //PageHeader,
    //DatePicker,
  },
  data() {
    return {
      title: "HSWIM",
      viewHSWIM: false,
      items: [
        {
          text: "HSWIM",
        },
        {
          text: "HSWIM",
          active: true,
        },
      ],
      img: "",
      imae: require("@/assets/images/redbutton.jpg"),
      imae2: require("@/assets/images/redbutton.jpg"),
      image: [this.imae, this.imae2],
      truckimae: require("@/assets/images/redbutton.jpg"),
      truckimae2: require("@/assets/images/redbutton.jpg"),
      truckimage: [this.truckimae, this.truckimae2],
      autoClose: "",
      shift: null,
      route: null,
      hour: null,
      shifts: ["Shift A", "Shift B", "Shift C"],
      routes: ["Nairobi-Mombasa", "Mombasa-Nairobi"],
      hours: [
        "00",
        "01",
        "02",
        "03",
        "04",
        "05",
        "06",
        "07",
        "08",
        "09",
        "10",
        "11",
        "12",
        "13",
        "14",
        "15",
        "16",
        "17",
        "18",
        "19",
        "20",
        "21",
        "22",
        "23",
      ],

      totalRows: 1,
      currentPage: 1,
      perPage: 10,
      pageOptions: [1, 10, 25, 50, 100],

      filterOn: [],
      sortBy: "id",
      sortDesc: false,
      fields: [
        {
          key: "cid",
          label: "#",
          sortable: true,
        },
        {
          key: "date_time_local",
          label: "Date Time",
          sortable: true,
        },
        {
          key: "wim",
          label: "station",
          sortable: true,
        },
        {
          key: "date_time_local",
          label: "Datetime",
          sortable: true,
        },
        "pictogram",
        {
          key: "lp",
          label: "Registration",
          sortable: true,
        },
        "anprpic",
        {
          key: "axle_conf",
          label: "Axle",
          sortable: true,
        },
        {
          key: "gvw",
          label: "GVW (Kg)",
          sortable: true,
        },
        {
          key: "axle_1_load",
          label: "Axle 1(Kg)",
          sortable: true,
        },
        {
          key: "axle_2_load",
          label: "Axle 2(Kg)",
          sortable: true,
        },
        {
          key: "axle_3_load",
          label: "Axle 3(Kg)",
          sortable: true,
        },
        {
          key: "axle_4_load",
          label: "Axle 4(Kg)",
          sortable: true,
        },
        {
          key: "axle_5_load",
          label: "Axle 5(Kg)",
          sortable: true,
        },
        {
          key: "axle_6_load",
          label: "Axle 6(Kg)",
          sortable: true,
        },
        {
          key: "axle_7_load",
          label: "Axle 7[Kg]",
          sortable: true,
        },

        {
          key: "total_weight",
          label: "GVW",
          sortable: true,
        },
        {
          key: "calledin",
          label: "calledin",
          sortable: true,
        },
        {
          key: "gap",
          label: "Gap",
          sortable: true,
        },
        {
          key: "calledinreason",
          label: "Called In Reason",
          sortable: true,
        },

        "action",
      ],
    };
  },
  watch: {
    viewHSWIM() {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Opened HSWIM List`,
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
    this.viewHSWIM = true;
    // Set the initial number of items
    this.imae = require("@/assets/images/button.png");
    this.imae2 = require("@/assets/images/redbutton.jpg");
    this.image = [this.imae, this.imae2];
    this.truckimae = require("@/assets/images/truckpass.jpg");
    this.truckimae2 = require("@/assets/images/truckcalledin.jpg");
    this.truckimage = [this.truckimae, this.truckimae2];

    this.updatearray();
    this.totalRows = this.items.length;
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
          this.orderData = response.data;
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
    getimage(date, no, position) {
      var server = window.$url; // arr[0] + "//" + arr2[0] + ":82";
      var year = date.substring(0, 4);
      var month = date.substring(5, 7);
      var day = date.substring(8, 10);
      var hour = date.substring(11, 13);

      var dir = year + month + "/" + day + "/" + hour + "/";

      var theUrl = "";
      if (window.$hswim == "Camea") {
        if (position == "F") {
          position = "LF";
        }
        theUrl = server + "/" + dir + position + no + ".jpg";
      }

      if (window.$hswim == "Bosvark") {
        server = window.$url; //arr[0] + "//" + arr2[0] + ":82";
        year = date.substring(0, 4);
        month = date.substring(5, 7);
        day = date.substring(8, 10);
        hour = date.substring(11, 13);
        var min = date.substring(14, 16);
        var sec = date.substring(17, 19);
        dir = year + month + "/" + day + "/" + hour + "/";
        var number = "";
        if (position == "F") {
          number = "WIM1ANPR";
        }
        if (position == "O") {
          number = "WIM1SV";
        }
        theUrl =
          server +
          "/kenload/dashboard/imgs/" +
          dir +
          number +
          year.substring(2, 4) +
          "-" +
          month +
          "-" +
          day +
          "_" +
          hour +
          "-" +
          min +
          "-" +
          sec +
          "-00" +
          ".jpg";
      }
      //this.truckimae2;
      //alert("List " + theUrl);
      return theUrl;
    },
    /**
     * Search the table data with search input
     */
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
    },
    handleSubmit() {
      console.log("Error on submit");
    },
  },
};
</script>

<template>
  <div class="card" style="font-size: 11px; font-weight: bold">
    <!-- <div class="col-sm-2">
                    <img
                      class="zoom"
                      :src="
                        getimage(data.item.date_time_local, data.item.cid, 'LF')
                      "
                    />
                  </div> -->
    <div class="col-sm-12">
      <div class="row" style="font-size: 11px; font-weight: bold">
        <div class="col-12">
          <div>
            <!-- Table -->

            <b-table
              striped
              hover
              :items="orderData"
              :fields="fields"
              :current-page="currentPage"
              :per-page="perPage"
              :filter="filter"
            >
              <template v-slot:cell(registration)="data">
                <ul class="list-inline mb-0">
                  <li class="list-inline-item">
                    <img
                      class="zoom"
                      :src="
                        getimage(data.item.date_time_local, data.item.cid, 'LP')
                      "
                    />
                  </li>
                </ul>
              </template>
              <template v-slot:cell(calledin)="data">
                <ul class="list-inline mb-0">
                  <li class="list-inline-item">
                    <img
                      v-bind:src="image[data.item.calledin]"
                      alt
                      height="20"
                    />
                  </li>
                </ul>
              </template>
              <template v-slot:cell(pictogram)="data">
                <ul class="list-inline mb-0">
                  <li class="list-inline-item">
                    <img
                      v-bind:src="truckimage[data.item.calledin]"
                      alt
                      height="20"
                    />
                  </li>
                </ul>
              </template>
              <template v-slot:cell(anprpic)="data">
                <div class="input-group">
                  <div class="col-sm-6">
                    <img
                      class="zoom"
                      :src="
                        getimage(data.item.date_time_local, data.item.cid, 'F')
                      "
                    />
                  </div>
                  <div class="col-sm-6">
                    <img
                      class="zoom"
                      :src="
                        getimage(data.item.date_time_local, data.item.cid, '0')
                      "
                    />
                  </div>
                </div>
              </template>
              <template v-slot:cell(action)>
                <ul class="list-inline mb-0">
                  <li class="list-inline-item">
                    <button
                      @click="generateReceipt()"
                      v-b-modal.modal-Print
                      style="
                        color: yellow;
                        background-color: black;
                        border-radius: 5px;
                      "
                    >
                      <i class="uil uil-eye me-2"></i>
                      View
                    </button>
                  </li>
                </ul>
              </template>
            </b-table>
          </div>
          <div class="row">
            <div class="col">
              <div class="dataTables_paginate paging_simple_numbers float-end">
                <ul class="pagination pagination-rounded">
                  <!-- pagination -->
                  <b-pagination
                    v-model="currentPage"
                    :total-rows="rows"
                    :per-page="perPage"
                  ></b-pagination>
                </ul>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<style scoped>
/* ::v-deep/ .table > tbody > tr > td { */
.table > tbody > tr > td {
  padding: 0px;
  margin: 0px;
  height: 2px;
}
.zoom {
  transition: transform 0.2s; /* Animation */
  width: 40px;
  height: 40px;
  margin: 0 auto;
}

.zoom:hover {
  transform: scale(15); /* (150% zoom)*/
}
</style>