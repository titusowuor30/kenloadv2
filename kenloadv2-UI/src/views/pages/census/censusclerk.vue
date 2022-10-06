<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import DatePicker from "vue2-datepicker";
import Multiselect from "vue-multiselect";

import Swal from "sweetalert2";

import axios from "axios";
//import html2canvas from "html2canvas";
//import rptheader from "@/components/report/header.js";
import reportdet from "@/components/report/header";
//import { filter } from "vue/types/umd";

export default {
  page: {
    title: "Census Clerk",
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
      title: "Census Clerk",
      items: [
        {
          text: "USER: Clerk",
        },
        {
          text: "Census Clerk",
          active: true,
        },
      ],
      id: "",
      name: "",
      pl: "",
      modaltitle: "",
      viewCensusClerk: false,
      printReport: false,
      clickedPrintpdf: "",
      filteredValues: "",
      printedpdf: true,
      generatedExel: "",
      addCensusClerk: false,
      editCensusClerk: false,
      deleteCensusClerk: false,
      editmode: false,
      headers: null,
      uniqueCars: null,
      showme: true,
      date: "",
      shift: "",
      route: "",
      hour: "",
      Trucks7T: 0,
      Trucks35: 0,
      Buses: 0,
      autoClose: "",
      shifts: ["Shift A", "Shift B", "Shift C"],
      routes: ["Nairobi-Mombasa", "Mombasa-Nairobi"],
      hours: [
        "0000",
        "0100",
        "0200",
        "0300",
        "0400",
        "0500",
        "0600",
        "0700",
        "0800",
        "0900",
        "1000",
        "1100",
        "1200",
        "1300",
        "1400",
        "1500",
        "1600",
        "1700",
        "1800",
        "1900",
        "2000",
        "2100",
        "2200",
        "2300",
      ],

      orderData: [
        // {
        //   id: "1",
        //   datetime: "10 May, 2021",
        //   shift: "Shift A",
        //   route: "Nairobi-Mombasa",
        //   hour: "00",
        //   Trucks7T: 5,
        //   Trucks35: 5,
        //   Buses: 0,
        // },
      ],
      totalRows: 1,
      currentPage: 1,
      perPage: 24,
      pageOptions: [1, 6, 10, 12, 24, 25, 50, 100],
      filter: "",
      filterOn: [],
      sortBy: "hour",
      sortDesc: false,
      fields: [
        {
          key: "index",
          label: "No.",
          sortable: true,
        },
        {
          key: "id",
          label: "#",
          sortable: true,
        },
        {
          key: "date",
          label: "Date",
          sortable: true,
        },
        {
          key: "shift",
          label: "Shift",
          sortable: true,
        },
        {
          key: "route",
          label: "Route",
          sortable: true,
        },
        {
          key: "hour",
          label: "Hour",
          sortable: true,
        },
        {
          key: "truck7t",
          label: "Trucks > 7T",
          sortable: true,
        },
        {
          key: "truck357t",
          label: "Trucks 3.5 > 7",
          sortable: true,
        },
        {
          key: "buses",
          label: "Buses",
          sortable: true,
        },
        {
          key: "userid",
          label: "User",
          sortable: true,
        },
        {
          key: "station",
          label: "Station",
          sortable: true,
        },
        "action",
      ],
    };
  },
  watch: {
    viewCensusClerk(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Opened census list:${newValue}`,
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
    printReport() {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Census reports:${this.clickedPrintpdf},${this.generatedExel},${this.filteredValues}`,
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
    addCensusClerk(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Added Census:${newValue}`,
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
    deleteCensusClerk(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Deleted Census record:${newValue},id:${this.id}`,
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
    // Set the initial number of items
    this.updatearray();
    this.updateroutes();
    this.totalRows = this.items.length;
    this.viewCensusClerk = true;
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
        .get(window.$http + `census/search?deletestatus=0`, {
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
    setfilter() {
      this.filter = this.mydate(this.autoClose);
    },
    updateroutes() {
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
      this.routes = [];
      axios
        .get(window.$http + `routes`, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          //this.routes = response.data;
          //console.log(this.orderData);
          response.data.forEach((e) => {
            this.routes.push(e.route_name);
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
    /**
     * Export to CSV
     */
    getrpt(p_excel) {
      //alert(new Date());
      this.printReport = true;
      p_excel = "Generated excel report";
      this.generatedExel = p_excel;
      const d = new Date();
      const year = d.getFullYear();
      const month = d.getMonth();
      const date = d.getDate();

      const hour = d.getHours();
      const min = d.getMinutes();
      const sec = d.getSeconds();
      const msec = d.getMilliseconds();
      const filename =
        year +
        "-" +
        month +
        "-" +
        date +
        "-" +
        hour +
        "-" +
        min +
        "-" +
        sec +
        "-" +
        msec;
      //alert(filename);
      const data = this.orderData.map((row) => ({
        "No.": row.id,
        Date: row.date,
        Shift: row.shift,
        Route: row.route,
        Hour: row.hour,
        "Trucks ≥ 7T": row.truck7t,
        "Trucks 3.5 ≥ ≤ 7T": row.truck357t,
        Buses: row.buses,
        Clerk: row.userid,
        Cluster: row.station,
      }));
      //alert("");
      const csvRows = [];
      //get headers
      const headers = Object.keys(data[0]);

      csvRows.push(headers.join(","));
      //alert(csvRows);
      //loop over the headers
      for (const row of data) {
        const values = headers.map((header) => {
          const escaped = ("" + row[header]).replace(/"/g, '\\"');
          // alert(escaped);
          return '"' + escaped + '"'; //'" + escaped + "';
        });
        csvRows.push(values.join(","));
      }
      //alert(csvData);
      const csvData = csvRows.join("\n");
      //alert(csvData);

      const blob = new Blob([csvData], { type: "textcsv" });
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement("a");
      a.setAttribute("hidden", "");
      a.setAttribute("href", url);
      a.setAttribute("download", this.title + filename + ".csv");
      document.body.appendChild(a);
      a.click();
      document.body.removeChild(a);
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
    printpdf(pl, p_pdf) {
      this.pl = pl;
      this.printReport = true;
      p_pdf = "Clicked print pdf button";
      this.clickedPrintpdf = p_pdf;
      const data = this.orderData.map((row) => ({
        "No.": row.id,
        Date: row.date,
        Shift: row.shift,
        Route: row.route,
        Hour: row.hour,
        "Trucks ≥ 7T": row.truck7t,
        "Trucks 3.5 ≥ ≤ 7T": row.truck357t,
        Buses: row.buses,
        Clerk: row.userid,
        Cluster: row.station,
      }));

      //get headers
      const headers = Object.keys(data[0]);
      const cars = [];
      Object.entries(data).forEach((val) => {
        const [key, value] = val;
        console.log(key, value);
        cars.push(Object.values(data[key]));
      });

      const uniqueCars = Array.from(new Set(cars));
      this.headers = headers;
      this.uniqueCars = uniqueCars;
      //alert(headers);
    },
    edit(index, myid, date, shift, route, hour, Trucks7T, Trucks35, Buses) {
      //alert(rolename);
      this.editmode = true;
      this.modaltitle = "Edit Census";
      this.id = myid;
      this.date = date;
      this.shift = shift;
      this.route = route;
      this.hour = hour;
      this.Trucks7T = Trucks7T;
      this.Trucks35 = Trucks35;
      this.Buses = Buses;
      this.myindex = index;
      this.focusInput();

      //this.$refs.search.focus();
      //$("#search").focus();
    },
    editrec() {
      if (this.name.trim() == "") {
        Swal.fire("Please enter  Cargo Name!");
        return;
      }

      //alert(this.myindex);
      this.orderData[this.myindex].id = this.id;
      this.orderData[this.myindex].date = this.autoClose;
      this.orderData[this.myindex].shift = this.shift;
      this.orderData[this.myindex].route = this.route;
      this.orderData[this.myindex].hour = this.hour;
      this.orderData[this.myindex].Trucks7T = this.Trucks7T + "";
      this.orderData[this.myindex].Trucks35 = this.Trucks35 + "";
      this.orderData[this.myindex].Buses = this.Buses + "";
      Swal.fire({
        position: "center",
        icon: "success",
        title: "Your work has been saved",
        showConfirmButton: false,
        timer: 1500,
      });
      this.editCensusClerk = true;
    },
    mydate(d) {
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

    position() {
      if (this.autoClose == "") {
        Swal.fire("Please select / enter  Date!");
        return;
      }
      //alert(this.shift.length);
      if (this.shift == "") {
        Swal.fire("Please select shift!");
        return;
      }
      //alert(this.shift);
      if (this.route == "") {
        Swal.fire("Please select route!");
        return;
      }

      if (this.hour == "") {
        Swal.fire("Please select the hour!");
        return;
      }

      // if (this.Trucks7T <= 0) {
      //   Swal.fire("Enter enter number of Trucks > 7T " + this.Trucks7T);
      //   return;
      // }

      // if (this.Trucks35 <= 0) {
      //   Swal.fire("Please enter number Trucks 3.5 > 7T !");
      //   return;
      // }
      // if (this.Buses <= 0) {
      //   Swal.fire("Please enter number of buses:");
      //   return;
      // }
      var datet =
        this.mydate(new Date(this.autoClose)) +
        "T" +
        this.hour.substring(0, 2) +
        ":" +
        "00:00";
      var str =
        "<div> Date:" +
        datet +
        "</div>" +
        "Shift:" +
        this.shift +
        "<br>" +
        "Route: " +
        this.route +
        "<br>" +
        "<div> Hour:" +
        this.hour +
        "</div>" +
        "<div> Trucks &#8804; 7T:" +
        this.Trucks7T +
        "</div>" +
        "<div> Trucks 3.5T &#8805; &#8804;7T:" +
        this.Trucks35 +
        "</div>" +
        "<div> Buses:" +
        this.Buses +
        "</div>";

      Swal.fire({
        title: "Do you want to save allowable tolerance \n" + "?",
        html: "<pre>" + str + "</pre>",

        icon: "info",
        showCancelButton: true,
        confirmButtonColor: "#006400",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, Save it!",
      }).then((result) => {
        if (result.value) {
          // const datetime = this.getcurrentdate();
          var pref = "CEN";
          //alert(window.$tokenString);
          // alert(
          //   window.$http +
          //     `census/max?censusid=` +
          //     pref +
          //     window.$stationcode +
          //     this.ticketdate()
          // );

          axios
            .get(
              window.$http +
                `census/max?censusid=` +
                pref +
                window.$stationcode +
                this.ticketdate(),
              { headers: window.$headers }
            )
            .then((response) => {
              //alert("tt");
              let censusid = "";

              let ticketmax = Number(response.data);
              ticketmax++;
              var ticketno = this.padLeadingZeros(ticketmax, 6);
              censusid =
                pref + window.$stationcode + this.ticketdate() + ticketno;

              const data = {
                date: datet,
                censusid: censusid,
                shift: this.shift,
                route: this.route,
                hour: this.hour,
                truck7t: this.Trucks7T + "",
                truck357t: this.Trucks35 + "",
                Buses: this.Buses + "",
                station: window.$stationcode,
                userid: window.$userid,
              };
              Swal.fire({
                title: "Kenload V2. Please Wait!",
                html: "Saving data...", // add html attribute if you want or remove
                allowOutsideClick: false,
                showConfirmButton: false,
                willOpen: () => {
                  Swal.showLoading();
                },
              });

              axios
                .post(window.$http + `census/`, data, {
                  headers: window.$headers,
                })
                .then((response) => {
                  // JSON responses are automatically parsed.
                  console.log(response.data);
                  this.addCensusClerk = true;
                  Swal.close();
                  Swal.fire({
                    position: "center",
                    icon: "success",
                    title: "Your work has been saved",
                    showConfirmButton: false,
                    timer: 1500,
                  }).then(() => {
                    this.clearvalues();
                    this.updatearray();
                    this.addCensusClerk = true;
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
        }
      });
    },
    padLeadingZeros(num, size) {
      var s = num + "";
      while (s.length < size) s = "0" + s;
      return s;
    },
    getcurrentdate() {
      let d = new Date();
      let year = d.getFullYear();
      let month = d.getMonth() + 1;
      let date = d.getDate();
      date = this.getv(date);
      month = this.getv(month);

      let hour = d.getHours();
      let min = d.getMinutes();
      let sec = d.getSeconds();
      hour = this.getv(hour);
      min = this.getv(min);
      sec = this.getv(sec);

      //const msec = d.getMilliseconds();
      const datetime =
        year + "-" + month + "-" + date + "T" + hour + ":" + min + ":" + sec;
      return datetime;
    },
    getv(val) {
      if (val < 10) {
        val = "0" + val;
      }
      return val;
    },
    ticketdate() {
      let d = new Date();
      let year = d.getFullYear();
      let month = d.getMonth() + 1;
      month = this.getv(month);
      //const msec = d.getMilliseconds();
      const datetime = year + "" + month + "";
      return datetime;
    },

    deleterec(data, id, shift, route, hour, date) {
      //alert(rolename);

      //this.id = id;
      this.name = date + "" + hour + shift + "" + route;
      this.id = id;
      var str =
        "<div> Date:" +
        date +
        "</div>" +
        "Shift:" +
        shift +
        "<br>" +
        "Route: " +
        route +
        "<br>" +
        "<div> Hour:" +
        hour +
        "</div>";
      Swal.fire({
        title: "Are you sure, you want to delete ?",
        text: "You won't be able to revert this!",
        html: "<pre>" + str + "</pre>",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, delete it!",
      }).then((result) => {
        if (result.value) {
          Swal.fire({
            title: "Kenload V2. Please Wait!",
            html: "Deleting data...", // add html attribute if you want or remove
            allowOutsideClick: false,
            showConfirmButton: false,
            willOpen: () => {
              Swal.showLoading();
            },
          });
          data.deletestatus = 1;
          data.hour = data.hour + data.censusid;
          axios
            .put(window.$http + `census/` + this.id, data, {
              headers: window.$headers,
            })
            .then((response) => {
              // JSON responses are automatically parsed.
              console.log(response.data);
              this.deleteCensusClerk = true;
              //this.orderData.splice(index, 1);
              this.updatearray();
              Swal.close();
              Swal.fire(
                " Deleted!",
                "Your record has been deleted.",
                "success"
              ).then(() => {
                this.updatearray();
                this.clearvalues();
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
        }
      });
    },
    clearvalues() {
      this.Buses = 0;
      this.Trucks35 = 0;
      this.Trucks7T = 0;
      // this.editmode = false;
      // this.modaltitle = "Add Cargo";
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />

    <div class="card">
      <div class="card-body">
        <form @submit.prevent="handleSubmit">
          <div class="card changebg">
            <div class="card-body">
              <div class="row justify-content-between">
                <div class="col-sm-6">
                  <button
                    class="btn btn-primary waves-effect waves-light uil-export"
                    @click="getrpt(p_excel)"
                  >
                    Export to CSV
                  </button>
                </div>

                <div class="col-sm-2">
                  <button
                    @click="printpdf('p', p_pdf)"
                    v-b-modal.modal-Print
                    class="
                      btn btn-primary
                      waves-effect waves-light
                      mdi-file-pdf
                    "
                  >
                    Print PDF
                  </button>
                </div>
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col-sm-5">
              <div class="card">
                <div class="card-body changebg">
                  <div class="card">
                    <div class="card-body">
                      <div class="col-sm-12">
                        <div class="input-group">
                          <div class="input-group-text col-sm-12 mb-3">
                            <div class="col-sm-1">Date :</div>
                            <div class="col-sm-2"></div>
                            <div class="col-sm-9">
                              <date-picker
                                @change="setfilter()"
                                value="2021-05-17"
                                v-model="autoClose"
                                :first-day-of-week="1"
                                lang="en"
                              ></date-picker>
                            </div>
                          </div>
                        </div>
                      </div>

                      <div class="col-sm-12 mb-3">
                        <div class="input-group">
                          <div class="input-group-text col-sm-5">Shift:</div>
                          <multiselect
                            v-model="shift"
                            :options="shifts"
                            class="form-control"
                          ></multiselect>
                        </div>
                      </div>

                      <div class="col-sm-12">
                        <div class="input-group">
                          <div class="input-group-text col-sm-5">Route:</div>
                          <multiselect
                            v-model="route"
                            :options="routes"
                            class="form-control"
                          ></multiselect>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="card">
                    <div class="card-body">
                      <div class="col-sm-12 mb-3">
                        <div class="input-group">
                          <div class="input-group-text col-sm-5">Hour:</div>
                          <multiselect
                            v-model="hour"
                            :options="hours"
                            class="form-control"
                          ></multiselect>
                        </div>
                      </div>

                      <div class="col-sm-12 mb-3">
                        <div class="input-group">
                          <div class="input-group-text col-sm-5">
                            Trucks &#8805; 7T:
                          </div>
                          <input
                            class="form-control"
                            type="text"
                            placeholder=""
                            v-model="Trucks7T"
                          />
                        </div>
                      </div>

                      <div class="col-sm-12 mb-3">
                        <div class="input-group">
                          <div class="input-group-text col-sm-5">
                            Trucks 3.5 &#8805; &#8804; 7T:
                          </div>
                          <input
                            class="form-control"
                            type="text"
                            placeholder=""
                            v-model="Trucks35"
                          />
                        </div>
                      </div>

                      <div class="col-sm-12">
                        <div class="input-group">
                          <div class="input-group-text col-sm-5">Buses:</div>
                          <input
                            class="form-control"
                            type="text"
                            placeholder=""
                            v-model="Buses"
                          />
                        </div>
                      </div>
                    </div>
                  </div>

                  <div class="col-sm-12"><br /></div>
                  <div class="row">
                    <div class="col-sm-4">&nbsp;</div>
                    <div class="col-sm-8">
                      <b-button variant="dark" @click="position()"
                        >Add Census >></b-button
                      >
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <div class="col-sm-7">
              <div class="card">
                <div class="card-body changebg">
                  <b-card-title>
                    <h5 class="card-title">Route ({{ route }});</h5>
                  </b-card-title>

                  <div class="row">
                    <div class="col-12">
                      <div>
                        <div class="float-end">
                          <form class="d-inline-flex mb-3"></form>
                        </div>
                      </div>
                      <div
                        class="
                          table table-centered
                          datatable
                          dt-responsive
                          nowrap
                          table-card-list
                          dataTable
                          no-footer
                          dtr-inline
                        "
                      >
                        <div class="row">
                          <div class="col-sm-12 col-md-6">
                            <div
                              id="tickets-table_length"
                              class="dataTables_length"
                            >
                              <label
                                class="
                                  d-inline-flex
                                  align-items-center
                                  fw-normal
                                "
                              >
                                Show&nbsp;
                                <b-form-select
                                  v-model="perPage"
                                  size="sm"
                                  :options="pageOptions"
                                ></b-form-select
                                >&nbsp;entries
                              </label>
                            </div>
                          </div>
                          <!-- Search -->
                          <div class="col-sm-12 col-md-6">
                            <div
                              id="tickets-table_filter"
                              class="dataTables_filter text-md-end"
                            >
                              <label
                                class="
                                  d-inline-flex
                                  align-items-center
                                  fw-normal
                                "
                              >
                                Search:
                                <b-form-input
                                  v-model="filter"
                                  type="search"
                                  placeholder="Search..."
                                  class="form-control form-control-sm ms-2"
                                ></b-form-input>
                              </label>
                            </div>
                          </div>
                          <!-- End search -->
                        </div>
                        <!-- Table -->

                        <b-table
                          table-class="table table-centered datatable table-card-list"
                          thead-tr-class="bg-transparent"
                          :items="orderData"
                          :fields="fields"
                          responsive="sm"
                          :per-page="perPage"
                          :current-page="currentPage"
                          :sort-by.sync="sortBy"
                          :sort-desc.sync="sortDesc"
                          :filter="filter"
                          :filter-included-fields="filterOn"
                          @filtered="onFiltered"
                        >
                          <template v-slot:cell(date)="data">
                            <div class="">
                              {{ data.item.date.substring(0, 10) }}
                            </div>
                          </template>
                          <template v-slot:cell(index)="data">
                            <div class="">
                              {{ data.index }}
                            </div>
                          </template>
                          <!----
                          <template v-slot:cell(id)="data">
                            <a
                              href="javascript: void(0);"
                              class="text-dark fw-bold"
                              >{{ data.item.id }}</a
                            >
                          </template>

                          <template v-slot:cell(name)="data">
                            <a href="#" class="text-body">{{
                              data.item.name
                            }}</a>
                          </template>
                          <template v-slot:cell(status)="data">
                            <div
                              class="badge bg-pill bg-soft-success font-size-12"
                              :class="{
                                'bg-soft-danger':
                                  data.item.status === 'Chargeback',
                                'bg-soft-warning':
                                  data.item.status === 'unpaid',
                              }"
                            >
                              {{ data.item.status }}
                            </div>
                          </template>
                          --->
                          <template v-slot:cell(action)="data">
                            <ul class="list-inline mb-0">
                              <!--  <li class="list-inline-item">
                                <a
                                  href="javascript:void(0);"
                                  class="px-2 text-primary"
                                  v-b-tooltip.hover
                                  title="Edit"
                                  v-b-modal.modal-Edit
                                  @click="
                                    edit(
                                      data.index,
                                      data.item.id,
                                      data.item.date,
                                      data.item.shift,
                                      data.item.route,
                                      data.item.hour,
                                      data.item.Trucks7T,
                                      data.item.Trucks35,
                                      data.item.Buses
                                    )
                                  "
                                >
                                  <i class="uil uil-pen font-size-18"></i>
                                </a>
                              </li> -->
                              <li class="list-inline-item">
                                <a
                                  href="javascript:void(0);"
                                  class="px-2 text-danger"
                                  v-b-tooltip.hover
                                  title="Delete"
                                  @click="
                                    deleterec(
                                      data.item,
                                      data.item.id,
                                      data.item.shift,
                                      data.item.route,
                                      data.item.hour,
                                      data.item.date
                                    )
                                  "
                                >
                                  <i class="uil uil-trash-alt font-size-18"></i>
                                </a>
                              </li>
                            </ul>
                          </template>
                        </b-table>
                      </div>
                      <div class="row">
                        <div class="col">
                          <div
                            class="
                              dataTables_paginate
                              paging_simple_numbers
                              float-end
                            "
                          >
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
            </div>
          </div>
        </form>
      </div>
    </div>
    <b-modal id="modal-Edit" :title="modaltitle" hide-footer size="lg" centered>
      <div class="row">
        <div class="col-md-12">
          <div class="card">
            <div class="card-body">
              <form @submit.prevent="handleSubmit">
                <div class="row">
                  <div class="input-group">
                    <div v-show="editmode" class="input-group-text col-sm-4">
                      ID:
                    </div>

                    <input
                      v-show="editmode"
                      class="form-control"
                      type="text"
                      disabled="disabled"
                      placeholder="id"
                      v-model="id"
                    />
                  </div>
                  <div class="input-group">
                    <div class="input-group-text col-sm-4">Cargo Name:</div>

                    <input
                      class="form-control"
                      type="text"
                      placeholder="Name"
                      v-model="name"
                    />
                  </div>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
      <!----@click="position(), $bvModal.hide('modal-1') --->
      <div class="row">
        <div class="col-sm-9 mb-2"></div>
        <div class="col-sm-3 mb-2">
          <b-button v-show="!editmode" variant="dark" @click="position()"
            >Add {{ title }}</b-button
          >
          <b-button v-show="editmode" variant="dark" @click="editrec()"
            >Edit {{ title }}</b-button
          >
        </div>
      </div>
    </b-modal>
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
.changebg {
  background-color: rgb(240, 240, 240);
}
</style>