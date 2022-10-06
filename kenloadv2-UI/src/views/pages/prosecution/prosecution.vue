<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import { tableData } from "./dataAdvancedtable";
import Swal from "sweetalert2";
import reportdet from "@/components/report/header";

export default {
  name: "Prosecution",
  page: {
    title: "Prosecution",
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
      title: "Prosecution",
      items: [
        {
          text: "USER: COP",
        },
        {
          text: "Overload list",
          active: true,
        },
      ],
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

      tableData: tableData,
      //
      headers: null,
      uniqueCars: null,
      showme: true,
      roadauthoritys: ["KeNHA", "KURA"],
      roadauthority: "",
      ticketno: "",
      registration: "",
      date: "",
      GVW: "",
      excess: "",
      prosecute: "",

      driverid: "",
      dnames: "",
      dsurname: "",
      dgender: "",
      dnationality: "",
      dage: "",
      daddress: "",
      oid: "",
      onames: "",
      osurname: "",
      ogender: "",
      onationality: "",
      oage: "",
      oaddress: "",
      vroad: "",
      district: "",
      weighbridge: "",
      county: "",
      court: "",
      police: "",
      tpolice: "",
      vi: "",
      moveto: "",
      rneeded: "",
      offload: "",
      Tnames: "",
      Taddress: "",
      index: "",
      // prosecutiondata: "",

      //
      totalRows: 1,
      currentPage: 1,
      perPage: 10,
      pageOptions: [1, 10, 25, 50, 100],
      filter: null,
      filterOn: [],
      sortBy: "ticketno",
      sortDesc: false,
      fields: [
        {
          key: "ticketno",
          sortable: true,
        },
        {
          key: "registration",
          sortable: true,
        },
        {
          key: "date",
          sortable: true,
        },
        {
          key: "GVW",
          sortable: true,
        },
        {
          key: "excess",
          sortable: true,
        },
        {
          key: "prosecute",
          label: "Action",
        },
      ],
    };
  },
  computed: {
    /**
     * Total no. of records
     */
    rows() {
      return this.tableData.length;
    },
  },
  mounted() {
    // Set the initial number of items
    this.totalRows = this.items.length;
    this.defaultvals();
  },

  methods: {
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
      var UserRole = "Vehicle Inspector";
      axios
        .get(window.$http + `Users/search?UserRole=` + UserRole, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.VIs = response.data;
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
        });
      UserRole = "Police";
      axios
        .get(window.$http + `Users/search?UserRole=` + UserRole, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.tpolices = response.data;
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
    defaultvals() {
      alert();
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
        .get(window.$http + `ProsecutionSettings`)
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          console.log(this.orderData);
          this.vroad = this.orderData[0].alongroad;
          this.district = this.orderData[0].district;
          this.Weighbridge = this.orderData[0].weighbridge;
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
    /**
     * Search the table data with search input
     */
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
    },
    doProsecute(index, ticketno, registration, date, gvw, excess, prosecute) {
      this.index = index;
      this.title =
        "Case Details for Reg. " + registration + " Ticket No. " + ticketno;
      this.ticketno = ticketno;
      this.registration = registration;
      this.date = date;
      this.gvw = gvw;
      this.excess = excess;
      this.prosecute = prosecute;
      this.defaultvals();
      this.focusInput();
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

      const data = this.tableData.map((row) => ({
        "Ticket No.": row.ticketno,
        "Registration No.": row.registration,
        Date: row.date,
        GVW: row.GVW,
        Excess: row.excess,
        Prosecute: row.prosecute,
      }));

      const csvRows = [];
      //get headers
      const headers = Object.keys(data[0]);

      csvRows.push(headers.join(","));

      //loop over the headers
      for (const row of data) {
        const values = headers.map((header) => {
          const escaped = ("" + row[header]).replace(/"/g, '\\"');
          return '"' + escaped + '"';
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
     * Export to PDF
     */
    printpdf(pl) {
      this.pl = pl;

      const data = this.tableData.map((row) => ({
        "Ticket No.": row.ticketno,
        "Registration No.": row.registration,
        Date: row.date,
        GVW: row.GVW,
        Excess: row.excess,
        Prosecute: row.prosecute,
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

    prosecuteVehicle() {
      // DRIVER DETAILS
      if (this.driverid.trim() == "") {
        Swal.fire("Please enter driver's ID/Passport No!");
        return;
      } else if (this.dnames.trim() == "") {
        Swal.fire("Please enter driver's name!");
        return;
      } else if (this.dsurname.trim() == "") {
        Swal.fire("Please enter driver's surname!");
        return;
      } else if (this.dgender.trim() == "") {
        Swal.fire("Please enter driver's gender!");
        return;
      } else if (this.dnationality.trim() == "") {
        Swal.fire("Please enter driver's nationality!");
        return;
      } else if (this.dage.trim() == "") {
        Swal.fire("Please enter driver's age!");
        return;
      } else if (this.daddress.trim() == "") {
        Swal.fire("Please enter driver's address!");
        return;
      }
      //OWNER DETAILS
      else if (this.oid.trim() == "") {
        Swal.fire("Please enter owner's ID/Passport No!");
        return;
      } else if (this.onames.trim() == "") {
        Swal.fire("Please enter owner's name!");
        return;
      } else if (this.osurname.trim() == "") {
        Swal.fire("Please enter owner's surname!");
        return;
      } else if (this.ogender.trim() == "") {
        Swal.fire("Please enter owner's gender!");
        return;
      } else if (this.onationality.trim() == "") {
        Swal.fire("Please enter owner's nationality!");
        return;
      } else if (this.oage.trim() == "") {
        Swal.fire("Please enter owner's age!");
        return;
      } else if (this.oaddress.trim() == "") {
        Swal.fire("Please enter owner's address!");
        return;
      }

      // LOCATION DETAILS
      else if (this.vroad.trim() == "") {
        Swal.fire("Please enter road!");
        return;
      } else if (this.district.trim() == "") {
        Swal.fire("Please enter district!");
        return;
      } else if (this.weighbridge.trim() == "") {
        Swal.fire("Please enter weighbridge!");
        return;
      } else if (this.county.trim() == "") {
        Swal.fire("Please enter county!");
        return;
      }

      // COURT DETAIL
      else if (this.court.trim() == "") {
        Swal.fire("Please enter court!");
        return;
      } else if (this.police.trim() == "") {
        Swal.fire("Please enter complainant police!");
        return;
      } else if (this.tpolice.trim() == "") {
        Swal.fire("Please enter traffic police!");
        return;
      }

      // PROHIBITION DETAILS
      else if (this.vi.trim() == "") {
        Swal.fire("Please enter vehicle inspector!");
        return;
      } else if (this.moveto.trim() == "") {
        Swal.fire("Please enter where vehicle is moved to!");
        return;
      } else if (this.rneeded.trim() == "") {
        Swal.fire("Please enter the repairs needed!");
        return;
      } else if (this.offload.trim() == "") {
        Swal.fire(
          "Please enter registration number where excess is offloaded to!"
        );
        return;
      }

      // TRANSPORTER DETAILS
      else if (this.Tnames.trim() == "") {
        Swal.fire("Please enter transporter names!");
        return;
      } else if (this.Taddress.trim() == "") {
        Swal.fire("Please enter transporter address!");
        return;
      }

      this.tableData[this.index].data.driverid = this.driverid;
      this.tableData[this.index].data.dnames = this.dnames;
      this.tableData[this.index].data.dsurname = this.dsurname;
      this.tableData[this.index].data.dgender = this.dgender;
      this.tableData[this.index].data.dnationality = this.dnationality;
      this.tableData[this.index].data.dage = this.dage;
      this.tableData[this.index].data.daddress = this.daddress;
      this.tableData[this.index].data.oid = this.oid;
      this.tableData[this.index].data.onames = this.onames;
      this.tableData[this.index].data.osurname = this.osurname;
      this.tableData[this.index].data.ogender = this.ogender;
      this.tableData[this.index].data.onationality = this.onationality;
      this.tableData[this.index].data.oage = this.oage;
      this.tableData[this.index].data.oaddress = this.oaddress;
      this.tableData[this.index].data.vroad = this.vroad;
      this.tableData[this.index].data.district = this.district;
      this.tableData[this.index].data.weighbridge = this.weighbridge;
      this.tableData[this.index].data.county = this.county;
      this.tableData[this.index].data.court = this.court;
      this.tableData[this.index].data.police = this.police;
      this.tableData[this.index].data.tpolice = this.tpolice;
      this.tableData[this.index].data.vi = this.vi;
      this.tableData[this.index].data.moveto = this.moveto;
      this.tableData[this.index].data.rneeded = this.rneeded;
      this.tableData[this.index].data.offload = this.offload;
      this.tableData[this.index].data.Tnames = this.Tnames;
      this.tableData[this.index].data.Taddress = this.Taddress;

      Swal.fire({
        position: "center",
        icon: "success",
        title: "Your work has been saved",
        showConfirmButton: false,
        timer: 1500,
      });
    },
  },
  middleware: "authentication",
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />

    <form @submit.prevent="handleSubmit">
      <div class="card">
        <div class="card-body">
          <div class="row justify-content-between">
            <div class="col-sm-6">
              <button
                class="btn btn-primary waves-effect waves-light uil-export"
                @click="getrpt()"
              >
                Export to CSV
              </button>
            </div>

            <div class="col-sm-2">
              <button
                @click="printpdf('p')"
                v-b-modal.modal-Print
                class="btn btn-primary waves-effect waves-light mdi-file-pdf"
              >
                Print PDF
              </button>
            </div>
            <div class="col-sm-2">&nbsp;</div>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col-sm-12">
          <div class="card">
            <div class="card-body changebg">
              <b-card-title>
                <h5 class="card-title"></h5>
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
                            class="d-inline-flex align-items-center fw-normal"
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
                            class="d-inline-flex align-items-center fw-normal"
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
                      :items="tableData"
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
                      <template v-slot:cell(prosecute)="data">
                        <div class="button">
                          <input
                            type="button"
                            variant="primary"
                            class="btn btn-primary waves-effect waves-light"
                            :id="`contacusercheck${data.item.id}`"
                            value="prosecute"
                            v-b-modal.modal-1
                            @click="
                              doProsecute(
                                data.index,
                                data.item.ticketno,
                                data.item.registration,
                                data.item.date,
                                data.item.gvw,
                                data.item.excess,
                                data.item.prosecute
                              )
                            "
                          />
                        </div>
                      </template>

                      <template v-slot:cell(id)="data">
                        <a
                          href="javascript: void(0);"
                          class="text-dark fw-bold"
                          >{{ data.item.id }}</a
                        >
                      </template>

                      <template v-slot:cell(registration)="data">
                        <a
                          href="#"
                          :click="{
                            'goToProhibition()':
                              data.item.registration === 'KBA857V',
                            'goToProhibition()':
                              data.item.registration === 'unpaid',
                          }"
                          class=""
                        >
                          {{ data.item.registration }}
                        </a>
                      </template>
                      <template v-slot:cell(prosecution)="data">
                        <div
                          class="badge bg-pill bg-soft-success font-size-12"
                          :class="{
                            'bg-soft-danger': data.item.action === 'prohibited',
                            'bg-soft-warning': data.item.action === 'unpaid',
                          }"
                          :click="{
                            'goToProhibition()':
                              data.item.action === 'prohibited',
                            'goToProhibition()': data.item.action === 'unpaid',
                          }"
                        >
                          {{ data.item.action }}
                        </div>
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

    <!-- MODAL START-->
    <b-modal id="modal-1" :title="title" hide-footer size="xl" centered>
      <div class="card-body">
        <b-tabs content-class="p-3 text-muted">
          <b-tab active class="border-0">
            <template v-slot:title>
              <span class="d-inline-block d-sm-none">
                <i class="fas fa-home"></i>
              </span>
              <span class="d-none d-sm-inline-block">Driver Details</span>
            </template>

            <div class="input-group">
              <div class="input-group-text col-sm-3">
                Driver ID / Passport No.
              </div>
              <input
                type="text"
                class="form-control"
                placeholder="ID"
                v-model="driverid"
                name="driverid"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Driver Full Names.</div>
              <input
                type="text"
                class="form-control"
                placeholder="Driver Full Names"
                v-model="dnames"
                name="dnames"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">
                Driver Surname/Father's name:
              </div>
              <input
                type="text"
                class="form-control"
                placeholder="Driver Surname"
                v-model="dsurname"
                name="dsurname"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Driver Gender:</div>
              <select class="form-control" name="dgender" v-model="dgender">
                <option value="">-Choose One-</option>
                <option value="Male">Male</option>
                <option value="Female">Female</option>
              </select>
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Nationality:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Driver Nationality"
                v-model="dnationality"
                name="dnationality"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Age:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Driver Age"
                v-model="dage"
                name="dage"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Driver Address:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Driver Address"
                v-model="daddress"
                name="daddress"
                required
              />
            </div>
          </b-tab>

          <b-tab>
            <template v-slot:title>
              <span class="d-inline-block d-sm-none">
                <i class="far fa-user"></i>
              </span>
              <span class="d-none d-sm-inline-block">Owners Details</span>
            </template>

            <div class="input-group">
              <div class="input-group-text col-sm-3">
                Owner ID / Passport No.
              </div>
              <input
                type="text"
                class="form-control"
                placeholder="Owners id"
                v-model="oid"
                name="oid"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Owner Full Names.</div>
              <input
                type="text"
                class="form-control"
                placeholder="Owners Name"
                v-model="onames"
                name="onames"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">
                Owner Surname/Fathers name.
              </div>
              <input
                type="text"
                class="form-control"
                placeholder="Owners Surname"
                v-model="osurname"
                name="osurname"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Owner Gender:</div>
              <select class="form-control" name="ogender" v-model="ogender">
                <option value="">-Choose One-</option>
                <option value="Male">Male</option>
                <option value="Female">Female</option>
              </select>
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Owner Nationality:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Owners Nationality"
                v-model="onationality"
                name="onationality"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Owner Age:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Owners Age"
                v-model="oage"
                name="oage"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Owner Address:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Owner Address"
                v-model="oaddress"
                name="oaddress"
                required
              />
            </div>
          </b-tab>

          <b-tab>
            <template v-slot:title>
              <span class="d-inline-block d-sm-none">
                <i class="far fa-envelope"></i>
              </span>
              <span class="d-none d-sm-inline-block">Location Details</span>
            </template>

            <div class="input-group">
              <div class="input-group-text col-sm-3">
                Vehicle Used Along Road:
              </div>
              <input
                type="text"
                class="form-control"
                placeholder="Vehicle Used Along Road"
                v-model="vroad"
                name="vroad"
                required
              />
            </div>

            <div class="input-group">
              <div class="input-group-text col-sm-3">District</div>
              <input
                type="text"
                class="form-control"
                placeholder="District"
                v-model="district"
                name="district"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Weighbridge:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Weighbridge"
                v-model="weighbridge"
                name="weighbridge"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">County:</div>
              <input
                type="text"
                class="form-control"
                placeholder="county"
                v-model="county"
                name="county"
                required
              />
            </div>
          </b-tab>

          <b-tab>
            <template v-slot:title>
              <span class="d-inline-block d-sm-none">
                <i class="fas fa-cog"></i>
              </span>
              <span class="d-none d-sm-inline-block">Court Details</span>
            </template>

            <div class="input-group">
              <div class="input-group-text col-sm-3">Court:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Court"
                v-model="court"
                name="court"
                required
              />
            </div>

            <div class="input-group">
              <div class="input-group-text col-sm-3">Complainant (Police):</div>
              <input
                type="text"
                class="form-control"
                placeholder="Complainant (Police)"
                v-model="police"
                name="police"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">
                Police / Traffic Officer:
              </div>
              <!-- <input
                type="text"
                class="form-control"
                placeholder="Police / Traffic Officer"
                v-model="tpolice"
                name="tpolice"
                required
              /> -->
              <multiselect
                v-model="tpolice"
                :options="tpolices"
                class="form-control"
              ></multiselect>
            </div>
          </b-tab>

          <b-tab>
            <template v-slot:title>
              <span class="d-inline-block d-sm-none">
                <i class="fas fa-cog"></i>
              </span>
              <span class="d-none d-sm-inline-block">Prohibition Details</span>
            </template>

            <div class="input-group">
              <div class="input-group-text col-sm-3">Vehicle Inspector: 5</div>
              <!-- <input
                type="text"
                class="form-control"
                placeholder="Vehicle Inspector"
                v-model="vi"
                name="vi"
              /> -->
              <multiselect
                v-model="vi"
                :options="VIs"
                class="form-control"
              ></multiselect>
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Move Vehicle to:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Move Vehicle to"
                v-model="moveto"
                name="moveto"
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Repairs Needed:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Repair's Needed"
                v-model="rneeded"
                name="rneeded"
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">
                Offload Excess to Reg. No.:
              </div>
              <input
                type="text"
                class="form-control"
                placeholder="Offload Excess to Reg. No."
                v-model="offload"
                name="offload"
              />
            </div>
          </b-tab>

          <b-tab>
            <template v-slot:title>
              <span class="d-inline-block d-sm-none">
                <i class="fas fa-cog"></i>
              </span>
              <span class="d-none d-sm-inline-block"
                >Transporter's Details</span
              >
            </template>

            <div class="input-group">
              <div class="input-group-text col-sm-3">Transporter Names:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Transporter Names"
                v-model="Tnames"
                name="Tnames"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Transporter Address:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Transporter Address"
                v-model="Taddress"
                name="Taddress"
                required
              />
            </div>
          </b-tab>
        </b-tabs>

        <form>
          <div class="row">
            <div class="col-lg-12">
              <div class="text-end">
                <b-button variant="dark" @click="prosecuteVehicle()"
                  >Prosecute</b-button
                >
                <!--- <button type="submit" class="btn btn-primary" @click="prosecuteVehicle(data.index);">Submit</button>-->&nbsp;&nbsp;&nbsp;
              </div>
            </div>
          </div>
        </form>
        <div class="row"></div>
      </div>
    </b-modal>
    <b-modal id="modal-Print" title="Print PDF" hide-footer size="bg" centered>
      <reportdet
        :title="title"
        :tableData="tableData"
        :pl="pl"
        :headers="headers"
        :uniqueCars="uniqueCars"
        :shome="showme"
        v-show="showme"
      ></reportdet>
    </b-modal>
    <!-- MODAL END-->
  </Layout>
</template>

<style scoped>
.changebg {
  background-color: #f7f6ebfb;
}
</style>