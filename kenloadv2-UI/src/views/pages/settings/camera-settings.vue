<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import Swal from "sweetalert2";
//import html2canvas from "html2canvas";
//import rptheader from "@/components/report/header.js";
import reportdet from "@/components/report/header";

import Multiselect from "vue-multiselect";

import axios from "axios";

export default {
  page: {
    title: "Camera Settings",
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
    Multiselect,
  },
  data() {
    return {
      wbs: ["A", "B"],
      anprcamera: 0,
      anprcount: 0,
      nocameras: 0,
      position: "",
      positions: ["Front", "Rear", "Side"],
      cameraname: "",
      ipaddress: "",
      port: 80,
      weighbridge: "",
      protocol: "",
      protocols: ["http", "https", "rtsp", "rtp", "udp"],
      title: "Cargo",
      items: [
        {
          text: "USER: Admin",
        },
        {
          text: "Cargo List",
          active: true,
        },
      ],
      orderDatanew: null,
      pl: "",
      orderData: [
        //     {
        //       id: "1",
        //       screenname: "TEST",
        //       active: 0,
        //     },
      ],
      id: "",
      addCamSettings: false,
      editCamSettings: false,
      deleteCamSettings: false,
      viewCamSettings: false,
      errors: "",
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
      cargo_name: "",
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
          key: "position",
          label: "Position",
          sortable: true,
        },
        {
          key: "weighbridge",
          label: "Weighbridge",
          sortable: true,
        },
        {
          key: "cameraname",
          label: "Camera Name",
          sortable: true,
        },
        {
          key: "ipaddress",
          label: "IP Address",
          sortable: true,
        },
        {
          key: "port",
          label: "Port",
          sortable: true,
        },
        {
          key: "file",
          label: "Image Path",
          sortable: true,
        },
        {
          key: "protocol",
          label: "Protocol",
          sortable: true,
        },
        "action",
      ],
    };
  },
  watch: {
    viewCamSettings(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Opened Camera settings:${newValue}`,
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
    addCamSettings(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Added Camera settings:${newValue}\n, values(${this.cameraname},${this.position},${this.weighbridge},${this.ipaddress},${this.port},${this.protocol})`,
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
    editCamSettings(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Edited Camera Settings:${newValue}\n values(${this.cameraname},${this.position},${this.weighbridge},${this.ipaddress},${this.port},${this.protocol})`,
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
    deleteCamSettings(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Deleted Camera Settings:${newValue},Camera id:${this.id}`,
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
    this.totalRows = this.items.length;
    this.viewCamSettings = true;
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
        .get(window.$http + `Camera`, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          Swal.close();
          //console.log(this.orderData);
          this.nocameras = response.data.length;
          for (let i = 0; i < response.data.length; i++) {
            //alert(response.data[i].position);
            if (
              response.data[i].position == "Front" ||
              response.data[i].position == "Rear"
            ) {
              this.anprcamera++;
            }
          }
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
        Cargo_Name: row.cargo_name,
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
        Cargo_Name: row.screenname,
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
    edit(
      index,
      myid,
      position,
      weighbridge,
      cameraname,
      ipaddress,
      port,
      file,
      protocol
    ) {
      //alert(rolename);
      this.editmode = true;
      this.modaltitle = "Edit Camera";
      this.id = myid;
      this.position = position;
      this.myindex = index;
      this.weighbridge = weighbridge;
      this.cameraname = cameraname;
      this.ipaddress = ipaddress;
      this.port = port;
      this.file = file;
      this.protocol = protocol;
      //this.focusInput();

      //this.$refs.search.focus();
      //$("#search").focus();
    },
    editrec() {
      var position = "";
      try {
        position = this.position;
      } catch (e) {
        position = "";
      }

      if (position.length <= 0) {
        Swal.fire("Select Position!");
        return;
      }
      var weighbridge = "";
      try {
        weighbridge = this.weighbridge;
      } catch (e) {
        weighbridge = "";
      }

      if (weighbridge.length <= 0) {
        Swal.fire("Please select Weighbridge!");
        return;
      }
      var cameraname = "";
      try {
        cameraname = this.cameraname;
      } catch (e) {
        cameraname = "";
      }

      if (cameraname.length <= 0) {
        Swal.fire("Please enter Camera Name!");
        return;
      }
      var ipaddress = "";
      try {
        ipaddress = this.ipaddress;
      } catch (e) {
        ipaddress = "";
      }

      if (ipaddress.length <= 0) {
        Swal.fire("Please enter Ip Address!");
        return;
      }
      var port = "";
      try {
        port = Number(this.port);
      } catch (e) {
        port = "";
      }

      if (port <= 0) {
        Swal.fire("Please enter port number!");
        return;
      }
      var file = "";
      try {
        file = this.file;
      } catch (e) {
        file = "";
      }

      if (file.length <= 0) {
        Swal.fire("Please enter file name!");
        return;
      }
      var protocol = "";
      try {
        protocol = this.protocol;
      } catch (e) {
        protocol = "";
      }

      if (protocol.length <= 0) {
        Swal.fire("Please select protocol!");
        return;
      }

      const data = {
        id: this.id,
        position: this.position,
        weighbridge: this.weighbridge,
        cameraname: this.cameraname,
        ipaddress: this.ipaddress,
        port: this.port,
        file: this.file,
        protocol: this.protocol,
      };

      // alert(this.id);
      axios
        .put(window.$http + `Camera/` + this.id, data, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          if (response.data.trim() != "") {
            Swal.fire({
              position: "center",
              icon: "success",
              title: "Your work has been saved" + response.data,
              showConfirmButton: false,
              timer: 1500,
            }).then(() => {
              this.updatearray();
              this.editCamSettings = true;
            });
          }
        })
        .catch((e) => {
          Swal.fire("" + e);
          //this.errors.push(e);
        });

      Swal.fire({
        position: "center",
        icon: "success",
        title: "Your work has been saved",
        showConfirmButton: false,
        timer: 1500,
      });
    },
    savedata() {
      var position = "";
      try {
        position = this.position;
      } catch (e) {
        position = "";
      }

      if (position.length <= 0) {
        Swal.fire("Select Position!");
        return;
      }
      var weighbridge = "";
      try {
        weighbridge = this.weighbridge;
      } catch (e) {
        weighbridge = "";
      }

      if (weighbridge.length <= 0) {
        Swal.fire("Please select Weighbridge!");
        return;
      }
      var cameraname = "";
      try {
        cameraname = this.cameraname;
      } catch (e) {
        cameraname = "";
      }

      if (cameraname.length <= 0) {
        Swal.fire("Please enter Camera Name!");
        return;
      }
      var ipaddress = "";
      try {
        ipaddress = this.ipaddress;
      } catch (e) {
        ipaddress = "";
      }

      if (ipaddress.length <= 0) {
        Swal.fire("Please enter Ip Address!");
        return;
      }
      var port = "";
      try {
        port = Number(this.port);
      } catch (e) {
        port = "";
      }

      if (port <= 0) {
        Swal.fire("Please enter port number!");
        return;
      }
      var file = "";
      try {
        file = this.file;
      } catch (e) {
        file = "";
      }

      if (file.length <= 0) {
        Swal.fire("Please enter file name!");
        return;
      }
      var protocol = "";
      try {
        protocol = this.protocol;
      } catch (e) {
        protocol = "";
      }

      if (protocol.length <= 0) {
        Swal.fire("Please select protocol!");
        return;
      }

      const data = {
        position: this.position,

        weighbridge: this.weighbridge,
        cameraname: this.cameraname,
        ipaddress: this.ipaddress,
        port: this.port,
        file: this.file,
        protocol: this.protocol,
      };
      axios
        .post(window.$http + `Camera/`, data, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          console.log(response.data);
          Swal.fire({
            position: "center",
            icon: "success",
            title: "Your work has been saved",
            showConfirmButton: false,
            timer: 1500,
          }).then(() => {
            this.updatearray();
            this.addCamSettings = true;
          });
        })
        .catch((e) => {
          Swal.fire("" + e);
        });

      this.screenname = "";
    },
    deleterec(
      index,
      id,
      position,
      weighbridge,
      cameraname,
      ipaddress,
      port,
      file,
      protocol
    ) {
      //alert(rolename);
      this.id = id;
      this.cameraname = cameraname;
      Swal.fire({
        title:
          "Are you sure, you want to delete Camera Name:" +
          cameraname +
          "Position:" +
          position +
          "ipaddress:" +
          ipaddress +
          "Weighbridge:" +
          weighbridge +
          "Port:" +
          port +
          "Image path:" +
          file +
          "protocol:" +
          protocol +
          "?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, delete it!",
      }).then((result) => {
        if (result.value) {
          axios
            .delete(window.$http + `Camera/` + this.id, {
              headers: window.$headers,
            })
            .then((response) => {
              // JSON responses are automatically parsed.
              console.log(response.data);
              this.deleteCamSettings = true;
              this.orderData.splice(index, 1);
              Swal.fire(
                this.cargo_name + " Deleted!",
                "Your record has been deleted.",
                "success"
              );
            })
            .catch((e) => {
              Swal.fire("" + e);
            });
        }
      });
    },
    clearvalues() {
      this.name = "";
      this.editmode = false;
      this.modaltitle = "Add Camera";
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <div ref="content">
      <div class="row">
        <div class="col-md-4">
          <b-card bg-variant="primary" class="text-white-50">
            <h5 class="mt-0 mb-4 text-white">
              <i class="mdi mdi-bullseye-arrow me-3"></i>{{ anprcamera }}
            </h5>
            <p class="text-white">ANPR Cameras</p>
          </b-card>
        </div>
        <div class="col-md-4">
          <b-card bg-variant="warning" class="text-black-50">
            <h5 class="mt-0 mb-4 text-black">
              <i class="mdi mdi-alert-outline me-3"></i>{{ nocameras }}
            </h5>
            <p class="text-black">Number of Camera(s)</p>
          </b-card>
        </div>
        <div class="col-md-4">
          <b-card bg-variant="info" class="text-white-50">
            <h5 class="mt-0 mb-4 text-white">
              <i class="mdi mdi-alert-circle-outline me-3"></i>
              {{ anprcount }}
            </h5>
            <p class="text-white">Number of plates</p>
          </b-card>
        </div>
      </div>
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
                      v-b-tooltip.hover
                      v-b-modal.modal-AddCamera
                      @click="clearvalues()"
                      class="
                        btn btn-primary
                        waves-effect waves-light
                        mdi-file-pdf
                      "
                    >
                      Add Camera
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
                            <template v-slot:cell(cargo)="data">
                              <a
                                href="javascript: void(0);"
                                class="text-dark fw-bold"
                                v-for="cargo in data.item.cargo_name"
                                :key="cargo"
                                >{{ cargo }} <br
                              /></a>
                            </template>
                            <!----
                          <template v-slot:cell(check)="data">
                            <div class="">
                              <input
                                type="button"
                                class="custom-control-input"
                                :id="`contacusercheck${data.item.id}`"
                                value="Submit"
                              />
                              <label
                                class="custom-control-label"
                                :for="`contacusercheck${data.item.id}`"
                              ></label>
                            </div>
                          </template>
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
                                    v-b-modal.modal-AddCamera
                                    title="Edit"
                                    @click="
                                      edit(
                                        data.index,
                                        data.item.id,
                                        data.item.position,
                                        data.item.weighbridge,
                                        data.item.cameraname,
                                        data.item.ipaddress,
                                        data.item.port,
                                        data.item.file,
                                        data.item.protocol
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
                                        data.item.position,
                                        data.item.weighbridge,
                                        data.item.cameraname,
                                        data.item.ipaddress,
                                        data.item.port,
                                        data.item.file,
                                        data.item.protocol
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
    <b-modal
      id="modal-AddCamera"
      ref="dialog"
      :title="modaltitle"
      hide-footer
      size="xl"
      centered
    >
      <div class="card">
        <div class="card-body">
          <div class="row">
            <div class="col-xl-5">
              <div class="mt-4 mt-xl-3 pl-xl-4">
                <h4 class="font-size-20 mb-3">{{ cameraname }}</h4>
                <div class="text-muted">
                  <span class="badge bg-success font-size-14 me-1"
                    ><i class="mdi mdi-star"></i> {{ positione }} View</span
                  >
                </div>

                <div class="mt-4">
                  <div class="col-lg-12 ms-lg-auto">
                    <div class="mt-5 mt-lg-4">
                      <form>
                        <div class="input-group mb-1">
                          <div class="input-group-text row col-sm-6">
                            Camera Position:
                          </div>
                          <multiselect
                            v-model="position"
                            :options="positions"
                            class="form-control"
                          ></multiselect>
                        </div>

                        <div class="input-group mb-1">
                          <div class="input-group-text row col-sm-6">
                            Weighbridge Name:
                          </div>
                          <multiselect
                            v-model="weighbridge"
                            :options="wbs"
                            class="form-control"
                          ></multiselect>
                        </div>

                        <div class="input-group mb-1">
                          <div class="input-group-text row col-sm-6">
                            Camera Name:
                          </div>
                          <input
                            v-model="cameraname"
                            type="text"
                            class="form-control"
                            id="horizontal-firstname-input"
                            placeholder="Camera Name"
                          />
                        </div>

                        <div class="input-group mb-1">
                          <div class="input-group-text row col-sm-6">
                            IP Address:
                          </div>
                          <!-- v-mask="'###.###.###.###'" -->
                          <input
                            v-model="ipaddress"
                            type="text"
                            class="form-control"
                            id="horizontal-firstname-input"
                            placeholder="192.168.2.100"
                          />
                        </div>

                        <div class="input-group mb-1">
                          <div class="input-group-text row col-sm-6">Port:</div>
                          <input
                            v-model="port"
                            type="number"
                            class="form-control"
                            id="horizontal-firstname-input"
                            placeholder="554"
                          />
                        </div>

                        <div class="input-group mb-1">
                          <div class="input-group-text row col-sm-6">File:</div>
                          <input
                            v-model="file"
                            type="text"
                            class="form-control"
                            id="horizontal-firstname-input"
                            placeholder="/jpg/image.jpg?size=3"
                          />
                        </div>

                        <div class="input-group mb-1">
                          <div class="input-group-text row col-sm-6">
                            Protocol:
                          </div>
                          <multiselect
                            v-model="protocol"
                            :options="protocols"
                            class="form-control"
                          ></multiselect>
                        </div>

                        <div class="row justify-content-end">
                          <div class="row">
                            <div class="col-sm-8"></div>
                            <div class="col-sm-2">
                              <b-button
                                v-show="!editmode"
                                variant="dark"
                                @click="savedata()"
                                class="btn btn-primary w-md"
                              >
                                Add Camera
                              </b-button>
                              <b-button
                                v-show="editmode"
                                variant="dark"
                                @click="editrec()"
                                class="btn btn-primary w-md"
                              >
                                Edit Camera
                              </b-button>
                            </div>
                          </div>
                        </div>
                      </form>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="col-xl-7">
              <div class="product-detail">
                <b-tabs pills vertical nav-wrapper-class="col-3">
                  <b-tab>
                    <template v-slot:title>
                      <img
                        src="@/assets/images/settings/cameraview.jpg"
                        alt
                        class="img-fluid mx-auto d-block tab-img rounded"
                      />
                    </template>
                    <div class="product-img">
                      <img
                        src="@/assets/images/settings/cameraview.jpg"
                        alt
                        class="img-fluid mx-auto d-block"
                      />
                    </div>
                  </b-tab>
                  <b-tab>
                    <template v-slot:title>
                      <img
                        src="@/assets/images/settings/cameraview.jpg"
                        alt
                        class="img-fluid mx-auto d-block tab-img rounded"
                      />
                    </template>
                    <div class="product-img">
                      <img
                        src="@/assets/images/settings/cameraview.jpg"
                        alt
                        class="img-fluid mx-auto d-block"
                      />
                    </div>
                  </b-tab>
                  <b-tab>
                    <template v-slot:title>
                      <img
                        src="@/assets/images/settings/cameraview.jpg"
                        alt
                        class="img-fluid mx-auto d-block tab-img rounded"
                      />
                    </template>
                    <div class="product-img">
                      <img
                        src="@/assets/images/settings/cameraview.jpg"
                        alt
                        class="img-fluid mx-auto d-block"
                      />
                    </div>
                  </b-tab>
                  <b-tab>
                    <template v-slot:title>
                      <img
                        src="@/assets/images/settings/cameraview.jpg"
                        alt
                        class="img-fluid mx-auto d-block tab-img rounded"
                      />
                    </template>
                    <div class="product-img">
                      <img
                        src="@/assets/images/settings/cameraview.jpg"
                        alt
                        class="img-fluid mx-auto d-block"
                      />
                    </div>
                  </b-tab>
                  <div class="row">
                    <div class="col-sm-2">
                      <button type="submit" class="btn btn-primary w-md">
                        Test View Camera
                      </button>
                    </div>
                  </div>
                </b-tabs>
              </div>
            </div>
          </div>
          <!-- end row -->
        </div>
      </div>
      <!-- end card -->
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