<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import reportdet from "@/components/report/header";

import Swal from "sweetalert2";
import axios from "axios";

import vue2Dropzone from "vue2-dropzone";
import "vue2-dropzone/dist/vue2Dropzone.min.css";
import DatePicker from "vue2-datepicker";
import Multiselect from "vue-multiselect";

//import axios from "axios";

export default {
  page: {
    title: "Calibration Certificates",
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
    vueDropzone: vue2Dropzone,
  },
  data() {
    return {
      dropzoneOptions: {
        url: "/api/file",
        addRemoveLinks: true,
        thumbnailWidth: 150,
        maxFilesize: 0.5,
        headers: {
          "My-Awesome-Header": "header value",
        },
      },
      title: "Calibration Certificates",
      addCalibrationcert: false,
      viewCalibrationcert: false,
      editCalibrationcert: false,
      deleteCalibrationcert: false,
      items: [
        {
          text: "USER: Admin",
        },
        {
          text: "Calibration Certificates List",
          active: true,
        },
      ],
      weighbridgename: "",
      weighbridgetype: "",
      weighbridgenames: ["A", "B"],
      dateissued: "",
      dateexpiry: "",
      weighbridgetypes: ["MULTI-DECK", "SINGLE AXLE"],
      orderDatanew: null,
      pl: "",
      station: "ATHIRIVER MOMBASA BOUND",
      ipaddress: "",
      systemname: "",
      status: "OFF",
      orderData: [
        {
          id: "1",
          station: "ATHIRIVER MOMBASA BOUND",
          weighbridge: "A",
          weighbridgetype: "Multi-Deck",
          status: "Active",
          startdate: "2021-06-04 09:25:27",
          expirydate: "2021-07-31 12:35:13 ",
          image: "",
        },
      ],
      id: "",
      headers: null,
      uniqueCars: null,
      showme: true,
      totalRows: 1,
      currentPage: 1,
      perPage: 10,
      pageOptions: [
        1, 2, 5, 10, 25, 50, 100, 200, 300, 500, 1000, 2000, 3000, 4000, 5000,
        6000, 9000, 10000, 20000,
      ],
      roadauthoritys: ["KeNHA", "KURA"],
      filter: null,
      filterOn: [],
      sortBy: "id",
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
          key: "weighbridge",
          label: "Weighbridge Name",
          sortable: true,
        },
        {
          key: "type",
          label: "Weighbridge Type",
          sortable: true,
        },
        {
          key: "status",
          label: "Status",
          sortable: true,
        },
        {
          key: "issued",
          label: "Date Issued",
          sortable: true,
        },
        {
          key: "expiry",
          label: "Expiry Date",
          sortable: true,
        },
        {
          key: "image",
          label: "Certificate image",
          sortable: true,
        },
        {
          key: "balance",
          label: "Days left",
          sortable: true,
        },
      ],
    };
  },
  watch: {
    viewCalibrationcert(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Opened Calibration cert:${newValue}`,
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
    addCalibrationcert(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Added Calibration cert:${newValue}`,
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
    editCalibrationcert(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Edited Calibration cert:${newValue}`,
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
    deleteCalibrationcert(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Deleted Calibration certificate:${newValue},id:${this.id}`,
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
    this.station = window.$station;
    this.updatearray();
    this.focusInput();
    this.totalRows = this.items.length;
    this.viewCalibrationcert = true;
  },
  methods: {
    focusInput() {},
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
        .get(window.$http + `calibrationcert`, { headers: window.$headers })
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
    /**
     * Export to CSV
     */
    getrpt() {
      //alert(new Date());
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
        ID: row.id,
        Health_Check_Name: row.name,
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
    printpdf(pl) {
      this.pl = pl;
      const data = this.orderData.map((row) => ({
        ID: row.id,
        Station: row.station,
        Equipment_Name: row.systemname,
        IPaddress: row.ipaddress,
        Status: row.status,
        Last_Check: row.lastcheck,
        Time_Up: row.timeup,
        Down_Time: row.downtime,
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
    edit(index, myid, station, systemname, ipaddress) {
      //alert(rolename);
      this.editmode = true;
      this.modaltitle = "Edit Calibration Certificate";
      this.id = myid;
      this.systemname = systemname;
      this.station = station;
      this.ipaddress = ipaddress;
      this.myindex = index;
      this.focusInput();

      //this.$refs.search.focus();
      //$("#search").focus();
    },
    editrec() {
      if (this.systemname.trim() == "") {
        Swal.fire("Please enter  Calibration Certificate Name!");
        return;
      }
      if (this.ipaddress.trim() == "") {
        Swal.fire("Please enter  Calibration Certificate IP Address!");
        return;
      }
      //alert(this.myindex);
      this.orderData[this.myindex].id = this.id;
      this.orderData[this.myindex].systemname = this.systemname;
      this.orderData[this.myindex].station = this.station;
      this.orderData[this.myindex].ipaddress = this.ipaddress;
      this.editCalibrationcert = true;
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
      //alert("month" + month);
      let date = d.getDate();
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
    getdaysdiff2(date1, date3) {
      var ddatesp = date3.substring(0, 10).trim().replaceAll("-", "/");
      var date2 = new Date(ddatesp);

      //Swal.fire("Date:" + date1 + "\ndate2" + date2);
      // The number of milliseconds in one day
      const ONE_DAY = 1000 * 60 * 60 * 24;

      // Calculate the difference in milliseconds
      const differenceMs = date2 - date1;

      // Convert back to days and return
      return Math.round(differenceMs / ONE_DAY);
    },

    getdaysdiff(date1, date2) {
      //Swal.fire("Date:" + date1 + "\ndate2" + date2);
      // The number of milliseconds in one day
      const ONE_DAY = 1000 * 60 * 60 * 24;

      // Calculate the difference in milliseconds
      const differenceMs = date2 - date1;

      // Convert back to days and return
      return Math.round(differenceMs / ONE_DAY);
    },
    position() {
      if (this.weighbridgename == "") {
        Swal.fire("Please select Weighbridge Name!");
        return;
      }

      if (this.weighbridgetype == "") {
        Swal.fire("Please select weighbridge type!");
        return;
      }
      if (this.dateissued == "") {
        Swal.fire("Please enter date issued !");
        return;
      }
      if (this.dateexpiry == "") {
        Swal.fire("Please enter expiry date!");
        return;
      }
      var status = 0;
      var issued = this.mydate(this.dateissued);
      var expiry = this.mydate(this.dateexpiry);
      var daydidff = this.getdaysdiff(new Date(), this.dateexpiry);

      if (daydidff <= 0) {
        Swal.fire("Certificate has expired!");
        alert("Certificate has expired!"); //return;
        status = 1;
      }
      var str =
        "<div class='col-sm-6'> Weighbridge Name:" +
        this.weighbridgename +
        "</div>" +
        "<div class='col-sm-6'> Weighbridge Type: " +
        this.weighbridgetype +
        "</div>" +
        "<div class='col-sm-6'> Date Issued:" +
        issued +
        "</div>" +
        "<div class='col-sm-6'> Expiry Date:" +
        expiry +
        "</div>";
      Swal.fire({
        title: "Do you want to add" + "?",
        html: "<pre>" + str + "</pre>",
        icon: "info",
        showCancelButton: true,
        confirmButtonColor: "#006400",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, Save it!",
      }).then((result) => {
        if (result.value) {
          const data = {
            station: window.$stationcode,
            weighbridge: this.weighbridgename,
            type: this.weighbridgetype,
            status: status,
            issued: issued,
            expiry: expiry,
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
            .post(window.$http + `calibrationcert/`, data, {
              headers: window.$headers,
            })
            .then((response) => {
              // JSON responses are automatically parsed.
              console.log(response.data);
              this.addCalibrationcert = true;
              Swal.fire({
                position: "center",
                icon: "success",
                title:
                  "Your work has been saved. Added Certificate for weigbridge: " +
                  this.weighbridgename +
                  " type " +
                  this.weighbridgetype,
                showConfirmButton: false,
                timer: 2500,
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
    deleterec(index, id, name, ipaddress) {
      //alert(rolename);
      this.id = id;
      this.systemname = name;
      this.ipaddress = ipaddress;
      Swal.fire({
        title:
          "Are you sure, you want to delete " +
          this.systemname +
          " with ip " +
          this.ipaddress +
          "?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, delete it!",
      }).then((result) => {
        if (result.value) {
          this.orderData.splice(index, 1);
          this.deleteCalibrationcert = true;
          //this.$delete(this.orderData, this.id - 1);
          Swal.fire(
            "Deleted!",
            this.systemname + " has been deleted.",
            "success"
          );
        }
      });
    },
    clearvalues() {
      this.systemname = "";
      this.ipaddress = "";

      this.editmode = false;
      this.modaltitle = "Add Calibration Certificate";
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
                  <div class="col-sm-2">
                    <button
                      v-b-modal.modal-Edit
                      class="
                        btn btn-primary
                        waves-effect waves-light
                        uil-focus-add
                      "
                      @click="clearvalues()"
                    >
                      Add {{ title }}
                    </button>
                  </div>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-sm-12">
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

                            <template v-slot:cell(balance)="data">
                              <div class="">
                                {{ getdaysdiff2(new Date(), data.item.expiry) }}
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
                                    class="px-2 text-primary"
                                    v-b-tooltip.hover
                                    title="Edit"
                                    v-b-modal.modal-Edit
                                    @click="
                                      edit(
                                        data.index,
                                        data.item.id,
                                        data.item.station,
                                        data.item.systemname,
                                        data.item.ipaddress
                                      )
                                    "
                                  >
                                    <i class="uil uil-pen font-size-18"></i>
                                  </a>
                                </li>
                                <li class="list-inline-item">
                                  <a
                                    href="javascript:void(0);"
                                    class="px-2 text-danger"
                                    v-b-tooltip.hover
                                    title="Delete"
                                    @click="
                                      deleterec(
                                        data.index,
                                        data.item.id,
                                        data.item.station,
                                        data.item.systemname
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
        <div class="col-md-8">
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
                    <div class="input-group-text col-sm-4">Station:</div>

                    <input
                      disabled="disabled"
                      class="form-control"
                      type="text"
                      placeholder="Name"
                      v-model="station"
                    />
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Weighbridge Name:
                      </div>
                      <multiselect
                        v-model="weighbridgename"
                        :options="weighbridgenames"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Weighbridge Type:
                      </div>
                      <multiselect
                        v-model="weighbridgetype"
                        :options="weighbridgetypes"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-12">
                        <div class="col-sm-5">Date Issued:</div>
                        <div class="col-sm-7">
                          <date-picker
                            value="2021-05-17"
                            v-model="dateissued"
                            :first-day-of-week="1"
                            lang="en"
                          ></date-picker>
                        </div>
                      </div>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-12">
                        <div class="col-sm-5">Expiry Date:</div>
                        <div class="col-sm-7">
                          <date-picker
                            value="2021-05-17"
                            v-model="dateexpiry"
                            :first-day-of-week="1"
                            lang="en"
                          ></date-picker>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </form>
            </div>
          </div>
        </div>

        <div class="col-sm-4">
          <!-- column 3 - upload image -->

          <h5 class="font-size-14 mb-4">
            <i class="mdi mdi-arrow-right text-primary me-1"></i>
            Upload
          </h5>

          <!-- file upload -->
          <vue-dropzone
            id="dropzone"
            ref="myVueDropzone"
            :use-custom-slot="true"
            :options="dropzoneOptions"
          >
            <div class="dropzone-custom-content">
              <i class="display-5 text-muted bx bxs-cloud-upload"></i>
              <h4>Drop image here or click to upload.</h4>
            </div>
          </vue-dropzone>
          <div class="text-center mt-2">
            <b-button size="sm" variant="danger">Delete Image</b-button>
          </div>

          <!-- col-3 end upload image -->
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
//#print {
  //height: 11in;
  //width: 8.5in;
//}
<style scoped>
.changebg {
  background-color: #f7f6ebfb;
}
</style>