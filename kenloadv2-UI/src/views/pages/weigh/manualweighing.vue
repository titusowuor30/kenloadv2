<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

//import html2canvas from "html2canvas";
//import rptheader from "@/components/report/header.js";
import reportdet from "@/components/report/header";

import DatePicker from "vue2-datepicker";
import Multiselect from "vue-multiselect";

import axios from "axios";
import Swal from "sweetalert2";

export default {
  page: {
    title: "Manual Weighing",
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
    reportdet,
    DatePicker,
    Multiselect,
  },
  data() {
    return {
      stationcode: "ATMB",
      mdate: "",
      addManualWeighing: false,
      viewManualWeighing: false,
      hour: "",
      ntickets: "",
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
      deckA: 0,
      deckB: 0,
      deckC: 0,
      deckD: 0,
      buttona: true,
      buttonb: true,
      buttonc: true,
      buttond: true,
      date: "",
      title: "Manual Weighing",
      items: [
        {
          text: "USER: Admin",
        },
        {
          text: "Manual Weighing List",
          active: true,
        },
      ],
      orderDatanew: null,
      pl: "",
      orderData: [
        // {
        //   id: "1",
        //   station: "ATHIRIVER MOMBASA BOUND STATION",
        //   date: "2021-02-09",
        //   userid: "Michael@micna.co.ke",
        //   hour: "00",
        //   tickets: "20",
        // },
      ],
      Weighbridge: window.$station,
      Load: "",
      username: window.$userid,
      id: "",
      headers: null,
      uniqueCars: null,
      showme: true,
      totalRows: 1,
      currentPage: 1,
      perPage: 10,
      pageOptions: [
        1,
        2,
        5,
        10,
        25,
        50,
        100,
        200,
        300,
        500,
        1000,
        2000,
        3000,
        4000,
        5000,
        6000,
        9000,
        10000,
        20000,
      ],
      roadauthoritys: ["KeNHA", "KURA"],
      filter: null,
      filterOn: [],
      sortBy: "date",
      sortDesc: false,
      name: "",
      roadauthority: "",
      prefix: "",
      myindex: "",
      editmode: false,
      modaltitle: "Add",
      fields: [
        {
          key: "id",
          label: "#",
          sortable: true,
        },
        {
          key: "station",
          label: "Station",
          sortable: true,
        },
        {
          key: "date",
          label: "Date",
          sortable: true,
        },
        {
          key: "userid",
          label: "User",
          sortable: true,
        },
        {
          key: "hour",
          label: "Hour",
          sortable: true,
        },
        {
          key: "tickets",
          label: "No. of Tickets",
          sortable: true,
        },

        "action",
      ],
    };
  },
  watch: {
    viewManualWeighing(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Opened Manual Weighing:${newValue}`,
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
    addManualWeighing(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Added Manual Weighing:${newValue},value:(${this.weighbridge},\n${this.mdate},${this.hour},${this.ntickets})`,
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
    deleteManualWeighing(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Deleted Manual Weighing:${newValue},Weigh id:${this.id}`,
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
    this.viewManualWeighing = true;
    this.Weighbridge = window.$station;
    this.stationcode = window.$stationcode;

    this.totalRows = this.items.length;
  },
  methods: {
    updatearray() {
      //screen and roles combination
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
        .get(window.$http + `manualweighing/search?deletestus=0`, {
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
    focusInput() {
      this.$refs.mydate.focus();
    },
    /**
     * Export to CSV
     */
    getrpt() {
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

      const data = this.orderData.map((row) => ({
        ID: row.id,
        weighbridge: row.station,
        date: row.datetime,
        username: row.userid,
        hour: row.hour,
        ntickets: row.tickets,
      }));

      const csvRows = [];
      //get headers
      const headers = Object.keys(data[0]);

      csvRows.push(headers.join(","));

      //loop over the headers
      for (const row of data) {
        const values = headers.map((header) => {
          const escaped = ("" + row[header]).replace(/"/g, '\\"');
          // alert(escaped);
          return '"' + escaped + '"'; //'" + escaped + "';
        });
        csvRows.push(values.join(","));
      }

      const csvData = csvRows.join("\n");

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
    printpdf(pl) {
      this.pl = pl;
      const data = this.orderData.map((row) => ({
        ID: row.id,
        Station: row.station,
        Date: row.datetime,
        User_Name: row.userid,
        Hour: row.hour,
        No_of_tickets: row.tickets,
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
    },
    edit(index, myid, name) {
      this.editmode = true;
      this.modaltitle = "Edit Manual Weighing";
      this.id = myid;
      this.name = name;
      this.myindex = index;
      this.focusInput();

      //this.$refs.search.focus();
      //$("#search").focus();
    },
    editrec() {
      if (this.name.trim() == "") {
        Swal.fire("Please enter  Manual Weighing Name!");
        return;
      }

      this.orderData[this.myindex].id = this.id;
      this.orderData[this.myindex].name = this.name;
      this.orderData[this.myindex].prefix = this.prefix;
      Swal.fire({
        position: "center",
        icon: "success",
        title: "Your work has been saved",
        showConfirmButton: false,
        timer: 1500,
      });
    },
    mydate(d) {
      let year = d.getFullYear();
      let month = d.getMonth() + 1;
      let date = d.getDate();
      date = this.getv(date);
      month = this.getv(month);

      //const msec = d.getMilliseconds();
      const datetime = year + "-" + month + "-" + date;
      return datetime;
    },
    getcurrentdate() {
      let d = new Date();
      let year = d.getFullYear();
      let month = d.getMonth();
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
    position() {
      if (this.mdate == "") {
        Swal.fire("Please select date!");
        return;
      }
      if (this.hour == "") {
        Swal.fire("Please  select hour !");
        return;
      }
      if (Number(this.ntickets) == 0) {
        Swal.fire("Please enter number of ticket!");
        return;
      }
      Swal.fire({
        title: "Do you want to save \n" + "?",
        text: "",
        icon: "info",
        showCancelButton: true,
        confirmButtonColor: "#006400",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, Save it!",
      }).then((result) => {
        if (result.value) {
          // alert(this.mdate + " " + this.hour);
          var datet =
            this.mydate(new Date(this.mdate)) +
            "T" +
            this.hour.substring(0, 2) +
            ":" +
            "00:00";

          const data = {
            station: this.stationcode,
            date: datet,
            userid: window.$userid,
            hour: this.hour,
            tickets: this.ntickets,
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
            .post(window.$http + `manualweighing/`, data, {
              headers: window.$headers,
            })
            .then((response) => {
              // JSON responses are automatically parsed.
              console.log(response.data);
              this.addManualWeighing = true;
              Swal.fire({
                position: "center",
                icon: "success",
                title: "Your work has been saved",
                showConfirmButton: false,
                timer: 1500,
              }).then(() => {
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
        }
      });
    },
    deleterec(data, id, details) {
      //alert(rolename);
      //alert(rolename);
      this.id = id;

      Swal.fire({
        title: "Are you sure, you want to delete for \n" + details + "?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, delete it!",
      }).then((result) => {
        if (result.value) {
          Swal.fire({
            title: "Kenload V2. Please Wait!",
            html: "Saving data...", // add html attribute if you want or remove
            allowOutsideClick: false,
            showConfirmButton: false,
            willOpen: () => {
              Swal.showLoading();
            },
          });
          data.deletestatus = 1;
          data.hour = data.hour + data.id;
          axios
            .put(window.$http + `manualweighing/` + this.id, data, {
              headers: window.$headers,
            })
            .then((response) => {
              // JSON responses are automatically parsed.
              console.log(response.data);
              //this.orderData.splice(index, 1);
              this.updatearray();
              Swal.close();
              Swal.fire(
                details + " Deleted!",
                "Your record has been deleted.",
                "success"
              ).then(() => {
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
        }
      });
    },
    clearvalues() {
      this.name = "";
      this.deckA = 0;
      this.deckB = 0;
      this.deckC = 0;
      this.deckD = 0;
      this.buttona = true;
      this.buttonb = true;
      this.buttonc = true;
      this.buttond = true;
      this.modaltitle = "Add Manual Weighing";
    },
    getdatet(dat) {
      var dates = "";
      dates = dat.substring(0, 10);
      return dates;
    },
    setdeck(deck) {
      if (deck == "deck1") {
        if (this.$refs.child.deck1 == 0) {
          Swal.fire("Please  Assign Number greater than zero!");
          return;
        }
        this.buttona = false;
        this.deckA = Number(this.$refs.child.deck1);
        //this.deck1 = Number(2000);
      }
      if (deck == "deck2") {
        if (this.$refs.child.deck2 == 0) {
          Swal.fire("Please  Assign Number greater than zero!");
          return;
        }
        this.buttonb = false;
        this.deckB = Number(this.$refs.child.deck2);
      }
      if (deck == "deck3") {
        if (this.$refs.child.deck3 == 0) {
          Swal.fire("Please  Assign Number greater than zero!");
          return;
        }
        this.buttonc = false;
        this.deckC = Number(this.$refs.child.deck3);
      }
      if (deck == "deck4") {
        if (this.$refs.child.deck4 == 0) {
          Swal.fire("Please  Assign Number greater than zero!");
          return;
        }
        this.buttond = false;
        this.deckD = Number(this.$refs.child.deck4);
      }
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />

    <div ref="content">
      <div class="card">
        <div class="card-body">
          <form @submit.prevent="handleSubmit">
            <div class="card">
              <div class="card-body">
                <div class="row justify-content-between">
                  <div class="col-sm-6">
                    <button
                      class="
                        btn btn-primary
                        waves-effect waves-light
                        uil-export
                      "
                      @click="getrpt()"
                    >
                      Export to CSV
                    </button>
                  </div>

                  <div class="col-sm-2">
                    <button
                      @click="printpdf('p')"
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
                  <div class="col-sm-2"></div>
                </div>
              </div>
            </div>

            <div class="row">
              <div class="col-sm-6">
                <div class="card">
                  <div class="card-body changebg">
                    <div class="card">
                      <div class="card-body changebg">
                        <div class="row" id="print">
                          <form @submit.prevent="handleSubmit">
                            <div class="row">
                              <div class="input-group">
                                <div class="input-group-text col-sm-4">
                                  Weighbridge:
                                </div>

                                <input
                                  class="form-control"
                                  type="text"
                                  disabled="disabled"
                                  placeholder="weighbridge"
                                  v-model="Weighbridge"
                                />
                              </div>
                              <div class="input-group">
                                <div class="input-group-text col-sm-12">
                                  <div class="col-sm-4">Date :</div>
                                  <div class="col-sm-8">
                                    <date-picker
                                      ref="mydate"
                                      value="2021-05-17"
                                      v-model="mdate"
                                      :first-day-of-week="1"
                                      lang="en"
                                    ></date-picker>
                                  </div>
                                </div>
                              </div>

                              <div class="input-group">
                                <div class="input-group-text col-sm-3">
                                  Hour:
                                </div>
                                <multiselect
                                  v-model="hour"
                                  :options="hours"
                                  class="form-control"
                                ></multiselect>
                              </div>

                              <div class="col-sm-12">
                                <div class="input-group">
                                  <div class="input-group-text col-sm-3">
                                    No. Of Tickets:
                                  </div>
                                  <input
                                    class="form-control"
                                    type="number"
                                    placeholder="0"
                                    v-model="ntickets"
                                  />
                                </div>
                              </div>
                            </div>
                          </form>
                        </div>
                      </div>
                    </div>
                    <div class="row">
                      <div class="col-sm-8">
                        <button
                          v-b-tooltip.hover
                          title="Clear Values"
                          class="btn btn-danger"
                          @click="clearvalues"
                        >
                          Clear
                        </button>
                      </div>
                      <div class="col-sm-4">
                        <button
                          v-b-tooltip.hover
                          title="Add Manual Weighing"
                          class="btn btn-primary uil-export"
                          @click="position()"
                        >
                          Add Manual Weighing
                        </button>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="col-sm-6">
                <div class="card">
                  <div class="card-body changebg">
                    <div class="row" id="print">
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
                            <template v-slot:cell(screens)="data">
                              <a
                                href="javascript: void(0);"
                                class="text-dark fw-bold"
                                v-for="screen in data.item.screens"
                                :key="screen"
                                >{{ screen }} <br
                              /></a>
                            </template>

                            <template v-slot:cell(date)="data">
                              <div class="">
                                {{ getdatet(data.item.date) }}
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
                                        'record for Date:' +
                                          data.item.date +
                                          ' Hour:' +
                                          data.item.hour
                                      )
                                    "
                                  >
                                    <i
                                      class="uil uil-trash-alt font-size-18"
                                    ></i>
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
                    <div class="input-group-text col-sm-4">
                      Manual Weighing Name:
                    </div>

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
//#print { //height: 11in; //width: 8.5in; //}
<style scoped>
.changebg {
  background-color: #f7f6ebfb;
}
</style>