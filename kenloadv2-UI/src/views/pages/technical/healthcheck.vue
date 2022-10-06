@@ -1,656 +1,709 @@
<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import Swal from "sweetalert2";
import axios from "axios";
//import html2canvas from "html2canvas";
//import rptheader from "@/components/report/header.js";
import reportdet from "@/components/report/header";

export default {
  page: {
    title: "Health Check",
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
  },
  data() {
    return {
      title: "Health Check",
      items: [
        {
          text: "USER: Admin",
        },
        {
          text: "Health Check List",
          active: true,
        },
      ],
      orderDatanew: null,
      pl: "",
      station: "ATHIRIVER MOMBASA BOUND",
      ipaddress: "",
      systemname: "",
      status: 0,
      orderData: [],
      imae: require("@/assets/images/redbutton.jpg"),
      imae2: require("@/assets/images/button.png"),
      image: ["@/assets/images/redbutton.jpg", "@/assets/images/button.png"],
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
      sortBy: "id",
      sortDesc: false,
      name: "",
      roadauthority: "",
      prefix: "",
      myindex: "",
      editmode: false,
      addHealthCheck: false,
      viewHealthCheck: false,
      editHealthCheck: false,
      deleteHealthCheck: false,
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
          key: "sysname",
          label: "Equipment Name",
          sortable: true,
        },
        {
          key: "ipaddress",
          label: "IP Address",
          sortable: true,
        },
        {
          key: "status",
          label: "Status",
          sortable: true,
        },
        {
          key: "lastchecked",
          label: "Last Check",
          sortable: true,
        },

        {
          key: "uptime",
          label: "Time Up",
          sortable: true,
        },

        {
          key: "downtime",
          label: "Down Time",
          sortable: true,
        },

        {
          key: "message",
          label: "Details",
          sortable: true,
        },

        "action",
      ],
    };
  },
  watch: {
    viewHealthCheck(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Opened Health Check:${newValue}`,
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
    addHealthCheck(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Added Equipment:${newValue}`,
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
    editHealthCheck(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Edited Equipment:${newValue}`,
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
    deleteHealthCheck(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Deleted Equipment:${newValue},id:${this.id}`,
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
    this.imae = require("@/assets/images/redbutton.jpg");
    this.imae2 = require("@/assets/images/button.png");
    this.image = [this.imae, this.imae2];
    this.updatearray();
    this.focusInput();
    this.totalRows = this.items.length;
    this.viewHealthCheck = true;
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
        .get(window.$http + `healthcheck`, { headers: window.$headers })
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
        "System Name": row.sysname,
        "IP Address": row.ipaddress,
        Status: row.status,
        "Last Check": row.lastchecked,
        "Time Up": row.uptime,
        "Down Time": "0",
        Details: row.message,
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
        "Equipment Name": row.sysname,
        "IP Address": row.ipaddress,
        Status: row.status,
        "Last Check": row.lastchecked,
        "Time Up": row.uptime,
        "Down Time": row.downtime,
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
      this.modaltitle = "Edit Equipment";
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
        Swal.fire("Please enter  Equipment Name!");
        return;
      }

      if (this.ipaddress.trim() == "") {
        Swal.fire("Please enter  Equipment IP Address!");
        return;
      }
      var str =
        "<div> Equipment:" +
        this.systemname +
        "</div>" +
        "IP Address:" +
        this.ipaddress +
        "<br>";
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
            id: this.id,
            sysname: this.systemname,
            ipaddress: this.ipaddress,
          };
          Swal.fire({
            title: "Kenload V2. Please Wait!",
            html: "Editing data...", // add html attribute if you want or remove
            allowOutsideClick: false,
            showConfirmButton: false,
            willOpen: () => {
              Swal.showLoading();
            },
          });
          axios
            .put(window.$http + `healthcheck/` + this.id, data, {
              headers: window.$headers,
            })
            .then((response) => {
              // JSON responses are automatically parsed.
              console.log(response.data);
              this.editHealthCheck = true;
              Swal.fire({
                position: "center",
                icon: "success",
                title:
                  "Your work has been edit. Updated equipment name: " +
                  this.systemname +
                  " with ip " +
                  this.ipaddress,
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
    position() {
      if (this.systemname.trim() == "") {
        Swal.fire("Please enter  Equipment Name!");
        return;
      }

      if (this.ipaddress.trim() == "") {
        Swal.fire("Please enter ipaddress!");
        return;
      }
      var str =
        "<div> Equipment:" +
        this.systemname +
        "</div>" +
        "IP Address:" +
        this.ipaddress +
        "<br>";
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
            sysname: this.systemname,
            ipaddress: this.ipaddress,
            status: this.status,
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
            .post(window.$http + `healthcheck/`, data, {
              headers: window.$headers,
            })
            .then((response) => {
              // JSON responses are automatically parsed.
              console.log(response.data);
              this.addHealthCheck = true;
              Swal.fire({
                position: "center",
                icon: "success",
                title:
                  "Your work has been saved. Updated equipment name: " +
                  this.systemname +
                  " with ip " +
                  this.ipaddress,
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
    deleterec(index, id, station, sysname, ipaddress) {
      //alert(rolename);
      this.index = index;
      this.id = id;
      this.station = station;
      this.systemname = sysname;
      this.ipaddress = ipaddress;
      var str =
        "<div> Equipment:" +
        this.systemname +
        "</div>" +
        "IP Address:" +
        this.ipaddress +
        "<br>";
      Swal.fire({
        title: "Do you want to add" + "?",
        html: "<pre>" + str + "</pre>",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, delete it!",
      }).then((result) => {
        if (result.value) {
          axios
            .delete(window.$http + `healthcheck/` + this.id, {
              headers: window.$headers,
            })
            .then((response) => {
              // JSON responses are automatically parsed.
              console.log(response.data);
              this.deleteHealthCheck = true;
              Swal.fire(
                this.systemname + " Deleted!",
                "Your record has been deleted.",
                "success"
              ).then(() => {
                //this.clearvalues();
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
      this.systemname = "";
      this.ipaddress = "";

      this.editmode = false;
      this.modaltitle = "Add Equipment";
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
                      Add Equipment
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

                            <template v-slot:cell(status)="data">
                              <div class="">
                                <img
                                  class="img-fluid rounded"
                                  v-bind:src="image[data.item.status]"
                                  alt="error"
                                  width="60"
                                  height="20"
                                />
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
                                        data.item.sysname,
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
                                        data.item.sysname,
                                        data.item.ipaddress
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
                    <div class="input-group-text col-sm-4">Station:</div>

                    <input
                      disabled="disabled"
                      class="form-control"
                      type="text"
                      placeholder="Station"
                      v-model="station"
                    />
                  </div>
                  <div class="input-group">
                    <div class="input-group-text col-sm-4">Equipment Name:</div>

                    <input
                      class="form-control"
                      type="text"
                      placeholder="Eqipment Name"
                      v-model="systemname"
                    />
                  </div>
                  <div class="input-group">
                    <div class="input-group-text col-sm-4">
                      Equipment IP Address:
                    </div>

                    <input
                      class="form-control"
                      type="text"
                      placeholder="IP Address"
                      v-model="ipaddress"
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
            >Add Equipment</b-button
          >
          <b-button v-show="editmode" variant="dark" @click="editrec()" on
            >Edit Equipment</b-button
          >
        </div>
      </div>
    </b-modal>
    <b-modal id="modal-Print" title="Print PDF" hide-footer size="bg" centered>
      <reportdet
        :title="title"
        :orderData="orderData"
        :pl="pl"
        :station="station"
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
