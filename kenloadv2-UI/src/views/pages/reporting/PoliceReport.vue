<script scoped
  type="text/javascript"
  src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/1.0.2/Chart.min.js"
></script>
<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import jsPDF from "jspdf";

import DatePicker from "vue2-datepicker";
import Multiselect from "vue-multiselect";

import axios from "axios";
import Swal from "sweetalert2";

import reportdet from "@/components/report/header";


export default {
  page: {
    title: "Police Daily Summary Reports",
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
    DatePicker,
    Multiselect,
    reportdet,
  },
  data() {
    return {
      //test data
      mydata: [],
      totalAmount: 0,
      pdf: new jsPDF(),
      fromDate: "",
      toDate: "",
      rpt: "",
      filter: "",
      hourlydata: "",
      headers: null,
      uniqueCars: null,
      showme: true,
      pl: "",
      orderData: [],
      title: "Daily Axle Briefts",
      //data
      mode: "A",
      loadpoliceDailyDta: false,
      wstatic: "",
      wmobile: "0",
      ovEAC: "",
      ovTraffic: "",
      wEAC: "",
      wTraffic: "0",
      ndtEAC: "",
      ndtTraffic: "0",
      prevEAC: "",
      prevTraffic: "0",
      crtFine: "",
      ovFine: "",
      rlsd: "",
      //
      items: [
        {
          text: "USER: Management",
        },
        {
          text: "Reporting",
          active: true,
        },
      ],
      exceldata: [],
      fromhour: "00:00",
      hours: [
        "00:00",
        "01:00",
        "02:00",
        "03:00",
        "04:00",
        "05:00",
        "06:00",
        "07:00",
        "08:00",
        "09:00",
        "10:00",
        "11:00",
        "12:00",
        "13:00",
        "14:00",
        "15:00",
        "16:00",
        "17:00",
        "18:00",
        "19:00",
        "20:00",
        "21:00",
        "22:00",
        "23:00",
      ],

      tohour: "23:59",
      fromdate: new Date(),
      todate: new Date(),
      state: [],
      email: "",
      deck: [],
      decks: ["A", "B"],
      wb: "All",
      wbs: [],
      user: null,
    };
  },

  mounted() {
    // Set the initial number of items
    this.email = window.$userid + ",";
    this.updatefields();
    this.getpoliceDailyDta();
    this.totalRows = this.items.length;
    this.viewReports = true;
  },
  watch: {
    loadpoliceDailyDta() {
      this.getpoliceDailyDta();
    },
  },
  methods: {
    updatefields() {
      axios
        .get(window.$http + `ClusterWeighbridges`, {
          headers: window.$headers,
        })
        .then((response) => {
          this.cluster = response.data;

          response.data.forEach((e) => {
            this.wbs.push(e.cluster_name);
          });

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
    formatDate(date) {
      var d = new Date(date),
        month = "" + (d.getMonth() + 1),
        day = "" + d.getDate(),
        year = d.getFullYear();

      if (month.length < 2) month = "0" + month;
      if (day.length < 2) day = "0" + day;

      return [year, month, day].join("-");
    },
    mydatenew(d) {
      let year = d.getFullYear();
      let month = d.getMonth() + 1;
      //alert("month" + month);
      let date = d.getDate();
      date = this.getv(date);
      month = this.getv(month);

      //const msec = d.getMilliseconds();
      const datetime = year + "-" + month + "-" + date;
      return datetime;
    },
    mydate(d, day, mon, yea) {
      let year = d.getFullYear() - yea;
      let month = d.getMonth() + 1 - mon;
      //alert("month" + month);
      let date = d.getDate() - day;
      date = this.getv(date);
      month = this.getv(month);

      //const msec = d.getMilliseconds();
      const datetime = year + "-" + month + "-" + date;
      return datetime;
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
    getmonth(d) {
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
    getdatefilter() {
      //alert(this.mydate(new Date(), 0, 0, 0));
      this.todate = new Date(this.mydate(new Date(), 0, 0, 0));
      if (this.picked == "Day") {
        this.fromdate = new Date(this.mydate(new Date(), 1, 0, 0));
      }
      if (this.picked == "week") {
        this.fromdate = new Date(this.mydate(new Date(), 7, 0, 0));
      }
      if (this.picked == "month") {
        this.fromdate = new Date(this.mydate(new Date(), 0, 1, 0));
      }
      if (this.picked == "year") {
        this.fromdate = new Date(this.mydate(new Date(), 0, 0, 1));
      }
    },
    updateMode(mode) {
      this.mode = mode;
      this.loadpoliceDailyDta = true;
    },
    getpoliceDailyDta(fromdate, todate) {
      var mode = this.mode;
      var hour = "";
      try {
        hour = this.hour.trim();
      } catch (e) {
        hour = "";
        this.hour = "";
      }
      var fromdate = "";
      try {
        fromdate = this.mydatenew(new Date(this.fromdate));
      } catch (e) {
        fromdate = "";
      }
      var todate = "";

      try {
        todate = this.mydatenew(new Date(this.todate));
      } catch (e) {
        todate = "";
        this.todate = "";
      }
      var ampm = "AM";
      //alert(this.fromhour);
      if (Number(this.fromhour.substring(0, 2)) > 12) {
        ampm = "PM";
      }
      var ampm2 = "AM";
      if (Number(this.tohour.substring(0, 2)) > 12) {
        ampm2 = "PM";
      }
      fromdate =
        this.getmydate(this.fromdate) +
        " " +
        this.fromhour +
        ":00 " +
        ampm +
        "";
      todate =
        this.getmydate(this.todate) + " " + this.tohour + ":59 " + ampm2 + "";

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
        .get(
          window.$http +
            "PoliceReports/DailyHour?fromdate=" +
            fromdate.trim(" ") +
            "&todate=" +
            todate.trim(" ") +
            "&station=" +
            this.wb +
            "&mode=" +
            mode,
          { headers: window.$headers }
        )
        .then((res) => {
          res.data;
          this.mydata = res.data.pop();
          console.log("rlsd=>" + this.mydata["released"]);
          this.wstatic = this.mydata["weighedStatic"];
          console.log(this.mydata);
          this.wmobile = "0";
          this.ovEAC = this.mydata["overloadEAC"];
          this.ovTraffic = this.mydata["overloadTraffic"];
          this.wEAC = this.mydata["overloadWideloadEAC"];
          this.wTraffic = this.mydata["overloadWideloadTraffic"];
          this.prevEAC = this.mydata["prevDetainedEAC"];
          this.prevTraffic = this.mydata["prevDetainedTraffic"];
          this.ndtEAC = this.mydata["newDetainedEaC"];
          this.ndtTraffic = this.mydata["newDetainedTraffic"];
          this.crtFine = this.mydata["courtFine"];
          this.ovFine = this.mydata["overloadCharge"];
          this.rlsd = this.mydata["released"];
          this.loadpoliceDailyDta = false;
          Swal.close();
        });
      Swal.fire({
        position: "center",
        icon: "error",
        title: "" + e,
        showConfirmButton: true,
      }).then((e) => {
        Swal.close(e);
      });
    },
    handleSubmit() {
      console.log("Error on submit");
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <div class="row">
      <div class="card">
        <div class="card-body">
          <form @submit.prevent="handleSubmit">
            <div class="row">
              <div class="card col-md-12"></div>
              <div class="card col-md-12 changebg">
                <div class="card-body">
                  <!-- Station-->
                  <div class="card">
                    <div class="card-body">
                      <div class="row">
                        <div class="col-sm-6">
                          <div class="input-group mb-3">
                            <div class="input-group-text col-sm-4">
                              Station:
                            </div>
                            <div class="col-sm-8">
                              <multiselect
                                v-model="wb"
                                :options="wbs"
                                placeholder="All"
                                :multiple="true"
                              ></multiselect>
                            </div>
                          </div>
                        </div>

                        <!-- end -->
                      </div>
                    </div>
                  </div>

                  <!--Date specification -->
                  <!--Date specification -->
                  <div class="card" v-show="true">
                    <div class="card-body">
                      <!-- Date Selection -->
                      <div class="row">
                        <div class="col-sm-6">
                          <div class="input-group">
                            <div class="input-group-text col-sm-12">
                              <div class="col-sm-3">From Date:</div>
                              <div class="col-sm-9">
                                <date-picker
                                  value="2021/05/17"
                                  v-model="fromdate"
                                  :first-day-of-week="1"
                                  lang="en"
                                  placeholder="Pick a Date"
                                ></date-picker>
                              </div>
                            </div>
                          </div>
                        </div>

                        <div class="col-sm-6">
                          <div class="input-group">
                            <div class="input-group-text col-sm-12">
                              <div class="col-sm-3">To Date:</div>
                              <div class="col-sm-9">
                                <date-picker
                                  value="2021/05/17"
                                  v-model="todate"
                                  :first-day-of-week="1"
                                  lang="en"
                                  placeholder="Pick a Date"
                                ></date-picker>
                              </div>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!--new Time specification-->
                  <div class="card" v-show="true">
                    <div class="card-body">
                      <div class="row">
                        <div class="col-sm-6">
                          <div class="input-group">
                            <div class="input-group-text col-sm-12">
                              <div class="col-sm-3">From Hour:</div>
                              <div class="col-sm-9">
                                <multiselect
                                  v-model="fromhour"
                                  :options="hours"
                                  placeholder="00:00"
                                  :allow-empty="false"
                                ></multiselect>
                              </div>
                            </div>
                          </div>
                        </div>

                        <div class="col-sm-6">
                          <div class="input-group-text col-sm-12 mb-3">
                            <div class="col-sm-3">To Hour:</div>
                            <div class="col-sm-9">
                              <multiselect
                                v-model="tohour"
                                :options="hours"
                                placeholder="23:59"
                                :allow-empty="false"
                              ></multiselect>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!--new Time specification-->
                  <div class="card">
                    <div class="card-body">
                      <div class="row">
                        <div
                          @click="updateMode('M')"
                          class="btn btn-secondary col-6 text-center m-auto"
                        >
                          <i class="fa fa-search"></i>
                          Find
                        </div>
                      </div>
                    </div>
                  </div>
                  <div style="height: 0; width: 0; overflow: hidden">
                    <canvas
                      id="supercool-canvas"
                      width="1200"
                      height="600"
                    ></canvas>
                  </div>
                  <div class="card p-2 mt-2">
                    <h3 class="text-center underline">
                      {{ wb }} Daily Axle Brief Reports
                    </h3>
                    <div class="query-container mt-3">
                      <div class="row">
                        <div class="col-6">
                          <div class="downloads">
                            <div
                              @click="exportReport('pdf')"
                              class="btn btn-secondary"
                            >
                              <i class="fa fa-file-pdf-o"></i>
                              Export PDF
                            </div>
                            <div
                              @click="
                                exportReport('xls', 'maintable', 'salesreport')
                              "
                              class="btn btn-secondary"
                            >
                              <i class="fa fa-file-excel-o"></i>
                              Export XLS
                            </div>
                            <div
                              @click="
                                exportReport('csv', 'maintable', 'salesreport')
                              "
                              class="btn btn-secondary"
                            >
                              <i class="fa fa-file-o"></i>
                              Export CSV
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <br />
                    <table class="table table-hover table-bordered" id="report">
                      <thead>
                        <tr>
                          <th class="bg-warning" colspan="2">WEIGHED</th>
                          <th class="bg-warning" colspan="4">CHARGED</th>
                          <th class="bg-warning" colspan="4">DETAINED</th>
                          <th class="bg-warning" colspan="2">
                            AMOUNT PAID(KSHS)
                          </th>
                          <th colspan="2" class="bg-warning">RELEASED</th>
                        </tr>
                        <tr>
                          <td class="font-weight-bold">Static</td>
                          <td class="font-weight-bold">Mobile</td>
                          <td colspan="2" class="bg-secondary font-weight-bold">
                            Overload
                          </td>
                          <td colspan="2" class="bg-secondary font-weight-bold">
                            Wide Load
                          </td>
                          <td colspan="2" class="bg-secondary font-weight-bold">
                            Newly Detained
                          </td>
                          <td colspan="2" class="bg-secondary font-weight-bold">
                            Previously Detained
                          </td>
                          <td class="font-weight-bold">Court Fine</td>
                          <td class="font-weight-bold">Overload Charge</td>
                        </tr>
                        <tr>
                          <td></td>
                          <td></td>
                          <td class="font-weight-bold">EACVL</td>
                          <td class="font-weight-bold">Traffic</td>
                          <td class="font-weight-bold">EACVL</td>
                          <td class="font-weight-bold">Traffic</td>
                          <td class="font-weight-bold">EACVL</td>
                          <td class="font-weight-bold">Traffic</td>
                          <td class="font-weight-bold">EACVL</td>
                          <td class="font-weight-bold">Traffic</td>
                          <td></td>
                          <td></td>
                        </tr>
                      </thead>
                      <tbody>
                        <tr>
                          <td v-bind="wstatic">{{ wstatic }}</td>
                          <td v-bind="wmobile">{{ wmobile }}</td>
                          <td v-bind="ovEAC">{{ ovEAC }}</td>
                          <td v-bind="ovTraffic">{{ ovTraffic }}</td>
                          <td v-bind="wEAC">{{ wEAC }}</td>
                          <td v-bind="wTraffic">{{ wTraffic }}</td>
                          <td v-bind="ndtEAC">{{ ndtEAC }}</td>
                          <td v-bind="ndtTraffic">{{ ndtTraffic }}</td>
                          <td v-bind="prevEAC">{{ prevEAC }}</td>
                          <td v-bind="prevTraffic">{{ prevTraffic }}</td>
                          <td v-bind="crtFine">{{ crtFine }}</td>
                          <td v-bind="ovFine">{{ ovFine }}</td>
                          <td v-bind="rlsd">{{ rlsd }}</td>
                        </tr>
                      </tbody>
                    </table>
                  </div>
                  <!-- ///buttons -->
                  <div class="row mt-2" v-show="false">
                    <div class="col-sm-2">
                      <b-button
                        pill
                        variant="outline-primary"
                        v-b-modal.modal-Print
                        @click="genrpt(genReportStarted)"
                        style="margin-right: 10px"
                      >
                        Generate</b-button
                      >
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
    <b-modal id="modal-Print" :title="report" hide-footer size="bg" centered>
      <reportdet
        :title="report"
        :reportfor="report"
        :mydata="mydata"
        :pl="pl"
        :headers="headers"
        :uniqueCars="uniqueCars"
        :shome="showme"
        :printedpdf="printedpdf"
        :rpt="rpt"
        v-show="showme"
        :exceldata="exceldata"
      ></reportdet>
    </b-modal>
  </Layout>
</template>

<style scoped>
.changebg {
  background-color: rgb(240, 240, 240);
}
</style>
