<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";
//import reportdet from "@/components/report/header";
//import Hswimlists from "@/components/widgets/weigh/hswimlist";
//import Hswimimage from "@/components/widgets/weigh/hswimimages";
//import Hswimimages from "@/components/widgets/weigh/hswimimages";
import Hswimlineimages from "@/components/widgets/weigh/hswimlineimages";
//import DatePicker from "vue2-datepicker";
import DatePicker from "vue2-datepicker";
import Multiselect from "vue-multiselect";
import reportdet from "@/components/report/header";

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
  components: {
    Layout,
    PageHeader,
    // Hswimlists,
    DatePicker,
    //Hswimimages,
    Hswimlineimages,
    Multiselect,
    reportdet,
  },
  data() {
    return {
      uniqueCars: null,
      showme: true,
      headers: null,
      pl: "",
      stoped: false,
      title: "HSWIM",
      items: [
        {
          text: "HSWIM",
        },
        {
          text: "HSWIM",
          active: true,
        },
      ],
      cluster: [],
      station: "",
      stations: [],
      truck: "",
      trucks: ["", "Trucks"],
      state: true,
      timerrun: 3,
      secs: [1, 3, 5, 8, 10],
      statusscan: "",
      STARTSTOP: "START",
      bstyle: "color: yellow; background-color: black;padding: 13px",
      imag: false,
      list: false,
      line: false,
      view: "Line Image",
      views: ["List", "Images", "Line Image"],
      fromdate: "",
      todate: "",
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
      hswima: [],
      hswimb: [],
      hswimc: [],
      orderData: [
        //   {
        //     id: 1,
        //     cid: 10499,
        //     station: "ATHIRIVER MOMBASA BOUND",
        //     datetime: "2020-10-25 23:59:46",
        //     wim: "6",
        //     vehicle_class: "kcm433h",
        //     velocity: 44,
        //     length: 55,
        //     lp: "",
        //     lp_back: "",
        //     anprassist: "kcm433h",
        //     axles_count: "",
        //     axleconfig: "6c",
        //     axle_1_load: 3772,
        //     axle_2_load: 5444,
        //     axle_3_load: 3434,
        //     axle_4_load: 8773,
        //     axle_5_load: 4566,
        //     axle_6_load: 6445,
        //     axle_7_load: 0,
        //     axle_8_load: 0,
        //     axle_9_load: 0,
        //     total_weight: 0,
        //     Axl_Legal_Weight_1: 0,
        //     Axl_Legal_Weight_2: 0,
        //     Axl_Legal_Weight_3: 0,
        //     Axl_Legal_Weight_4: 0,
        //     Axl_Legal_Weight_5: 0,
        //     Axl_Legal_Weight_6: 0,
        //     Axl_Legal_Weight_7: 0,
        //     Axl_Legal_Weight_8: 0,
        //     Axl_Legal_Weight_9: 0,
        //     permissible: 0,
        //     AVW: 0,
        //     GVW: 0,
        //     overweighting: 0,
        //     dual_tire: 0,
        //   },
      ],
      totalRows: 1,
      src: null,
      currentPage: 1,
      perPage: 10,
      pageOptions: [
        1, 2, 5, 10, 25, 50, 100, 200, 300, 500, 1000, 2000, 3000, 4000, 5000,
        6000, 9000, 10000, 20000,
      ],
      lanes: ["", "WIM", "Lane 1", "Lane 2"],
      lane: "WIM",
      filter: "",
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
          label: "NoPlate",
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
        "action",
      ],
    };
  },
  computed: {
    /**
     * Total no. of records
     */
    rows() {
      return this.orderData.length;
    },
  },
  created() {},
  mounted() {
    this.fromdate = new Date();
    this.fromdate.setDate(this.fromdate.getDate() - 10);
    this.todate = new Date();
    this.todate.setDate(this.todate.getDate() + 1);
    // Set the initial number of items
    this.imae = require("@/assets/images/redbutton.jpg");
    this.imae2 = require("@/assets/images/button.png");
    this.image = [this.imae, this.imae2];
    this.truckimae = require("@/assets/images/truckcalledin.jpg");
    this.truckimae2 = require("@/assets/images/truckpass.jpg");
    this.truckimage = [this.truckimae, this.truckimae2];

    //this.updatearray(true);
    this.totalRows = this.items.length;
    this.startstop();
    this.getview();
    this.getstart();
    this.getstation();
    this.station = localStorage.hswimstation;
    this.getresults(false);
  },
  methods: {
    updatearray(state) {
      //screen and roles combination
      //alert(window.$http);
      if (state) {
        Swal.fire({
          title: "Please Wait !",
          html: "Loading data...", // add html attribute if you want or remove
          allowOutsideClick: false,
          showConfirmButton: false,
          willOpen: () => {
            Swal.showLoading();
          },
        });
      }
      //alert(window.$http + `virtualtickets?limit=1`);
      axios
        .get(window.$http + `virtualtickets/search?limit=1&asc=date`, {
          headers: window.$headers,
        })
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

    getstation() {
      this.stations = [];
      axios
        .get(window.$http + `ClusterWeighbridges`, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          // this.cargos = response.data.cargo_name;
          this.cluster = response.data;
          response.data.forEach((e) => {
            this.stations.push(e.cluster_name.toUpperCase());
          });
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

    getmonth(d) {
      // const monthNames = [
      //   "January",
      //   "February",
      //   "March",
      //   "April",
      //   "May",
      //   "June",
      //   "July",
      //   "August",
      //   "September",
      //   "October",
      //   "November",
      //   "December",
      // ];
      const monthNames = [
        "Jan",
        "Feb",
        "Mar",
        "Apr",
        "May",
        "Jun",
        "Jul",
        "Aug",
        "Sep",
        "Oct",
        "Nov",
        "Dec",
      ];
      return monthNames[d];
    },
    getv(val) {
      if (val < 10) {
        val = "0" + val;
      }
      return val;
    },
    getmydate(mydate) {
      let d = new Date(mydate);
      let year = d.getFullYear();
      let month = this.getmonth(d.getMonth());
      let date = d.getDate();
      date = this.getv(date);
      //month = this.getv(month);

      //const msec = d.getMilliseconds();
      const datetime = date + "/" + month + "/" + year;
      return datetime;
    },
    getStationcode(name) {
      var code = "";
      //alert(this.cluster.length);
      var stat = this.cluster;
      for (var i = 0; i < stat.length; i++) {
        if (name == stat[i].cluster_name) {
          code = stat[i].cluster_code;
        }
      }
      return code;
    },
    getresults(state) {
      var fromdate = "";
      var todate = "";
      var mylane = "";
      var lane = "";
      var station = "";
      var stationcode = "";

      try {
        fromdate = this.fromdate;
      } catch (e) {
        fromdate = "";
      }
      try {
        todate = this.todate;
      } catch (e) {
        todate = "";
      }

      try {
        mylane = this.lane;
      } catch (e) {
        mylane = "";
      }
      // if (fromdate <= 0) {
      //   return;
      // }
      // if (todate <= 0) {
      //   return;
      // }
      fromdate = this.getmydate(this.fromdate) + " 12:00:00 AM";
      todate = this.getmydate(this.todate) + " 11:59:59 PM";
      var truck = "";
      try {
        truck = this.truck.trim();
      } catch (e) {
        console.log(e);
      }
      if (truck.length > 0) {
        truck = "&trucks=" + truck;
      }
      if (state) {
        Swal.fire({
          title: "Please Wait !",
          html: "Loading data...", // add html attribute if you want or remove
          allowOutsideClick: false,
          showConfirmButton: false,
          willOpen: () => {
            Swal.showLoading();
          },
        });
      }
      var fil = "";
      try {
        station = this.station.trim();
        stationcode = this.getStationcode(station);
      } catch (e) {
        station = "";
      }

      if (station.length > 0) {
        station = "&station=" + stationcode;
      }

      try {
        fil = this.filter.trim();
      } catch (e) {
        console.log(e);
      }
      var reg = "";

      if (fil.length > 0) {
        reg = "&veh_reg=" + this.filter;
      }

      if (mylane == "WIM") {
        // if (window.$hswim == "Camea") {
        //   lane = "&wim=GI-P1-W1";
        // }
        // if (window.$hswim == "Bosvark") {
        //   lane = "&wim=GL-NK-W1";
        // }
        lane = "&wim=W2,W1,W3";
      }
      if (mylane == "Lane 1") {
        if (window.$hswim == "Camea") {
          //lane = "&wim=GI-P1-W2";
          lane = "&wim=W2";
        }
        if (window.$hswim == "Bosvark") {
          //lane = "&wim=GL-NK-L1";
          lane = "&wim=L1";
        }
      }

      axios
        .get(
          window.$http +
            `virtualtickets/search?fromdate=` +
            fromdate +
            "&asc=date&todate=" +
            todate +
            "&tag=0&limit=" +
            this.perPage +
            "" +
            truck +
            lane +
            "" +
            reg +
            station,
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          // JSON responses are automatically parsed.

          //this.ticketdata = response.data;
          this.orderData = response.data;
          this.getview();
          this.statusscan = "";
          Swal.close();
        })
        .catch((e) => {
          this.statusscan = "";
          Swal.fire({
            position: "center",
            icon: "error",
            title: "" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });
          Swal.close();
          //throw e;
        });
    },
    getview() {
      //alert(this.view);
      this.list = false;
      this.imag = false;
      this.line = false;

      this.hswima = [];
      this.hswimb = [];
      this.hswimc = [];

      if (this.view == "List") {
        this.list = true;
        this.hswima = this.orderData;
      }
      if (this.view == "Images") {
        this.hswimb = this.orderData;
      }
      if (this.view == "Line Image") {
        this.line = true;
        this.hswimc = this.orderData;
      }
      //this.getresults(true);
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
    sswalp() {
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
    },
    startstop() {
      //alert(this.STARTSTOP);
      if (this.STARTSTOP == "START") {
        this.STARTSTOP = "STOP";
        this.bstyle = "color: yellow; background-color: red;padding: 13px";
        this.stoped = true;
        return;
      }
      if (this.STARTSTOP == "STOP") {
        this.STARTSTOP = "START";
        this.bstyle = "color: yellow; background-color: black;padding: 13px";
        this.stoped = false;
        return;
      }
    },
    getstart() {
      setInterval(() => {
        //
        //alert(this.timerrun * 1000 + "" + this.stoped);
        if (this.stoped) {
          if (this.STARTSTOP == "STOP") {
            this.statusscan = "Scanning data..." + this.timerrun + new Date();
            this.getresults(false);
          }
        } else {
          //this.timerrun = 1000000;
        }
      }, this.timerrun * 2000);
    },
    setstation() {
      localStorage.hswimstation = this.station;
    },
  },
  destroyed() {
    this.stoped = false;
  },
  beforeDestroy() {
    this.stoped = false;
    // alert(this.timer);
  },
  Unmounted() {
    //alert(this.timerrun);
    this.timerrun = 10000000;
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <div class="card" style="font-size: 11px; font-weight: bold">
      <div class="col-sm-12">
        <div class="row">
          <div class="col-sm-1">
            <span>From:</span>
            <date-picker
              @blur="getresults(true)"
              :value="new Date()"
              v-model="fromdate"
              :first-day-of-week="1"
              lang="en"
            ></date-picker>
          </div>
          <div class="col-sm-1">
            <span>To:</span>
            <date-picker
              @blur="getresults(true)"
              :value="new Date()"
              v-model="todate"
              :first-day-of-week="1"
              lang="en"
            ></date-picker>
          </div>
          <div class="col-md-1">
            <span> <i class="uil uil-refresh"></i>Refresh Period</span>

            <div class="col-md-10">
              <multiselect
                @change="getresults(true)"
                style="font-size: 12px"
                v-model="timerrun"
                :options="secs"
                class="form-control"
              ></multiselect>
            </div>
          </div>
          <div class="col-sm-1">
            <span> <i class="uil uil-eye"></i>View</span>

            <div class="col-md-10">
              <div class="col-md-10">
                <b-form-select
                  @change="getview()"
                  v-model="view"
                  :options="views"
                ></b-form-select>
              </div>
            </div>
          </div>
          <div class="col-sm-1">
            <span> <i class="uil uil-eye"></i>Lane</span>

            <div class="col-md-10">
              <b-form-select
                @change="getresults(true)"
                v-model="lane"
                :options="lanes"
              ></b-form-select>
            </div>
          </div>

          <div class="col-sm-1">
            <span> <i class="uil uil-eye"></i>Vehicle</span>

            <div class="col-md-10">
              <b-form-select
                @change="getresults(true)"
                v-model="truck"
                :options="trucks"
              ></b-form-select>
            </div>
          </div>

          <div class="col-sm-1">
            <span> <i class="uil uil-eye"></i>Show</span>

            <div class="col-md-10">
              <b-form-select
                @change="getresults(true)"
                v-model="perPage"
                :options="pageOptions"
              ></b-form-select>
            </div>
          </div>

          <div class="col-sm-1">
            <span> <i class="uil uil-eye"></i>HSWIM Bound</span>

            <div class="col-md-10">
              <multiselect
                @input="setstation()"
                v-model="station"
                :options="stations"
                class="form-control"
              ></multiselect>
            </div>
          </div>

          <div class="col-sm-1">
            <span> <i class="uil uil-eye"></i>Search</span>

            <div class="col-md-10">
              <b-form-input
                @change="getresults(true)"
                v-model="filter"
                type="search"
                placeholder="Search..."
                class="form-control form-control-sm ms-2"
              ></b-form-input>
            </div>
          </div>

          <div class="col-sm-2">
            <div class="btn-group mt-2" role="group" aria-label="Basic example">
              <button type="button" :style="bstyle" @click="startstop()">
                {{ STARTSTOP }}
              </button>
            </div>
          </div>
          {{ statusscan }}
        </div>
      </div>
    </div>

    <!-- <Hswimlists v-show="list" :orderData="hswima" /> -->
    <!-- <Hswimimages v-show="imag" :hswimData="hswimb" /> -->
    <Hswimlineimages v-show="line" :hswimData="hswimc" />

    <b-modal id="modal-Print" title="Print PDF" hide-footer size="bg" centered>
      <reportdet
        :title="title"
        :orderData="orderData"
        :pl="pl"
        :headers="headers"
        :uniqueCars="uniqueCars"
        :shome="showme"
        v-show="showme"
      ></reportdet>
    </b-modal>
  </Layout>
</template>
<style scoped>
/* ::v-deep/ .table > tbody > tr > td { */
.table > tbody > tr > td {
  padding: 0px;
  margin: 0px;
  height: 2px;
}
</style>