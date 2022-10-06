<script>
//import html2canvas from "html2canvas";
//import rptheader from "@/components/report/header.js";
import reportdet from "@/components/report/header";

import axios from "axios";
import Swal from "sweetalert2";

export default {
  components: {
    reportdet,
  },
  props: {
    nplate: String,
    cat: String,
  },
  data() {
    return {
      title: "Yard List",
      link: "vehicledetails",
      items: [
        {
          text: "USER: Admin",
        },
        {
          text: "Yard  List ",
          active: true,
        },
      ],
      orderDatanew: null,
      pl: "",
      orderData: [
        {
          id: "1",
          vehreg: "KBA857V",
          status: "Manual",
          userid: "Transgressing",
          datetime: "2020-08-30 08:40:42",
          wbtno: "AT-MB-A-2021-08-0000001",
          specialrelease: "N",
          spreleaseno: "",
          station: "ATMB",
          caseid: "",
          yardtimeout: "",
          inspected: "N",
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
      modaltitle: "Access Control (Yard 's)",
      fields: [
        {
          key: "id",
          label: "#",
          sortable: true,
        },
        {
          key: "vehreg",
          label: "Registration",
          sortable: true,
        },
        {
          key: "wbtno",
          label: "Ticket",
          sortable: true,
        },
        {
          key: "station",
          label: "Station",
          sortable: true,
        },
        {
          key: "datetime",
          label: "Date In",
          sortable: true,
        },
        {
          key: "yardtimeout",
          label: "Date Out",
          sortable: true,
        },
        {
          key: "inspected",
          label: "Inspected",
          sortable: true,
        },
        {
          key: "userid",
          label: "Category",
          sortable: true,
        },
        {
          key: "specialrelease",
          label: "Released",
          sortable: true,
        },
        {
          key: "spreleaseno",
          label: "Special Release NO",
          sortable: true,
        },
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
  mounted() {
    // Set the initial number of items
    //this.focusInput();
    //this.totalRows = this.items.length;
    this.filter = this.nplate;
    this.updatearray();
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
        .get(window.$http + `yardlist/search?status=I`, {
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
        Cargo_Name: row.name,
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
        Cargo_Name: row.name,
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
    edit(index, myid, name) {
      //alert(rolename);
      this.editmode = true;
      this.modaltitle = "Edit Cargo";
      this.id = myid;
      this.name = name;
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
    position() {
      if (this.name.trim() == "") {
        Swal.fire("Please enter  Cargo Name!");
        return;
      }

      var orderid = this.orderData.length + 1;
      this.orderData.push({
        id: orderid,
        name: this.name,
      });
      this.name = "";
      Swal.fire({
        position: "center",
        icon: "success",
        title: "Your work has been saved",
        showConfirmButton: false,
        timer: 1500,
      });
    },
    deleterec(index, id, name) {
      //alert(rolename);
      this.id = id;
      this.name = name;
      Swal.fire({
        title: "Are you sure, you want to delete " + this.name + "?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, delete it!",
      }).then((result) => {
        if (result.value) {
          this.orderData.splice(index, 1);
          //this.$delete(this.orderData, this.id - 1);
          Swal.fire("Deleted!", this.name + " has been deleted.", "success");
        }
      });
    },
    clearvalues() {
      this.name = "";
      this.editmode = false;
      this.modaltitle = "Add Cargo";
    },
    Weighing() {
      //alert("");
      Swal.fire({
        title: "Are you sure, you want to cancel weighing " + this.nplate + "?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, Cancel it!",
      }).then((result) => {
        if (result.value) {
          Swal.fire(
            "Cancelled Weighing!",
            this.nplate + " cancelled weighing.",
            "success"
          );
          this.$router.push({
            name: "/weigh/" + this.cat,
            params: { nplate: this.nplate, cat: this.cat },
          });
        }
      });
    },
  },
};
</script>

<template>
  <div>
    <div ref="content">
      <div class="card">
        <div class="card-body">
          <form @submit.prevent="handleSubmit">
            <div class="card">
              <div class="card-body">
                <div class="row justify-content-between">
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
                  <div class="col-sm-2"></div>
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
                                    title="Edit"
                                    v-b-modal.modal-Edit
                                    @click="
                                      edit(
                                        data.index,
                                        data.item.id,
                                        data.item.name
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
                                        data.item.name
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
  </div>
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