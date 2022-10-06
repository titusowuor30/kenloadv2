<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";
import reportdet from "@/components/report/header";

import axios from "axios";
import Swal from "sweetalert2";

export default {
  components: {
    reportdet,
    Layout,
    PageHeader,
  },
  page: {
    title: "Screen Access Password",
    meta: [
      {
        name: "description",
        content: appConfig.description,
      },
    ],
  },
  props: {
    nplate: String,
    cat: String,
  },
  data() {
    return {
      title: "Screen Access Password",
      link: "setscreenpassword",
      items: [
        {
          text: "USER: Admin",
        },
        {
          text: "Yard  List ",
          active: true,
        },
      ],
      pass_char: 1,
      pass_nums: 1,
      pass_min_length: 8,
      pass_small_chars: 1,
      pass_upper_chars: 1,
      orderDatanew: null,
      screenname: "",
      screenid: "",
      password: "",
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
      modaltitle: "Access Control Password",
      fields: [
        {
          key: "id",
          label: "#",
          sortable: true,
        },
        {
          key: "screenname",
          label: "Access Screen",
          sortable: true,
        },
        {
          key: "acp_pass",
          label: "ScreenPassword",
          sortable: true,
        },
        {
          key: "setpass",
          label: "Set Password",
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
    this.getPassWordPolicy();
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
      //alert(window.$http + `screens/search?email=` + window.$userid);
      axios
        .get(window.$http + `screens/search?email=` + window.$userid, {
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
    screendetails(id, screenname) {
      this.screenid = id;
      this.screenname = screenname;
    },
    getPassWordPolicy() {
      axios
        .get(window.$http + "AccessPassPolicy", { headers: window.$headers })
        .then((response) => {
          //console.log(response.data);
          // JSON responses are automatically parsed.
          //console.log(response.data[0].app_min_char);
          this.pass_char = response.data[0].app_char;
          this.pass_min_length = response.data[0].app_min_char;
          this.pass_nums = response.data[0].app_numbers;
          this.pass_upper_chars = response.data[0].app_capital;
          this.pass_small_chars = response.data[0].app_small;
        })
        .catch((e) => {
          this.errors.push(e);
          console.log(e);
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
      //     pass_char: 1,
      //   pass_nums: 1,
      //   pass_min_length: 8,
      //   pass_small_chars: 1,
      //   pass_upper_chars: 1,
      //alert(this.app_capital);
      if (this.password.trim() == "") {
        Swal.fire("Please enter password!");
        return;
      }
      if (Number(this.pass_upper_chars) == 1) {
        if (this.password.search(/[A-Z]/) < 0) {
          Swal.fire("Password must have Capital Letters!");
          return;
        }
      }
      if (Number(this.pass_small_chars) == 1) {
        if (this.password.search(/[a-z]/) < 0) {
          Swal.fire("Password must have Small Letters!");
          return;
        }
      }
      //alert(this.pass_min_length);
      if (Number(this.pass_nums) == 1) {
        if (Number(this.pass_min_length) > this.password.trim().length) {
          Swal.fire(
            "Password must be more than " + this.pass_min_length + " characters"
          );
          return;
        }
      }
      if (this.pass_char == 1) {
        if (this.password.search(/[!@#$%^&*()_+\-=[\]{};':"\\|,.<>/?]+/) < 0) {
          Swal.fire(
            "Password must contain at least one Special character e.g !@#$%^&*!"
          );
          return;
        }
      }
      //return !/[a-z]/.test(str) && /[A-Z]/.test(str);
      const data = {
        acp_pass: this.password,
        username: window.$userid,
        screenid: Number(this.screenid),
        level: "0",
      };
      axios
        .post(window.$http + `accesscontrolpasswords/`, data, {
          headers: window.$headers,
        })
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
            this.$root.$emit("bv::hide::modal", "modal-Edit");
            this.updatearray();
          });
        })
        .catch((e) => {
          Swal.fire("" + e);
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
  <Layout>
    <PageHeader :title="title" :items="items" />

    <div>
      <div ref="content">
        <div class="card">
          <div class="card-body">
            <form @submit.prevent="handleSubmit">
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
                              <template v-slot:cell(setpass)="data">
                                <button
                                  v-b-modal.modal-Edit
                                  class="
                                    btn btn-primary
                                    waves-effect waves-light
                                    uil-focus-add
                                  "
                                  @click="
                                    screendetails(
                                      data.item.screenid,
                                      data.item.screenname
                                    )
                                  "
                                >
                                  Set Passwoord {{ data.item.screenid }}
                                </button>
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
                                 --->
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
        id="modal-Edit"
        :title="modaltitle"
        hide-footer
        size="lg"
        centered
      >
        <div class="row">
          <div class="col-md-12">
            <div class="card">
              <div class="card-body">
                <form @submit.prevent="handleSubmit">
                  <div class="row">
                    <div class="input-group">
                      <div class="input-group-text col-sm-4">Screen ID :</div>

                      <input
                        class="form-control"
                        type="text"
                        disabled="disabled"
                        placeholder="id"
                        v-model="screenid"
                      />
                    </div>
                    <div class="input-group">
                      <div class="input-group-text col-sm-4">Screen Name :</div>

                      <input
                        class="form-control"
                        type="text"
                        disabled="disabled"
                        placeholder="id"
                        v-model="screenname"
                      />
                    </div>
                    <div class="input-group">
                      <div class="input-group-text col-sm-4">Password:</div>

                      <input
                        class="form-control"
                        type="password"
                        placeholder="*"
                        v-model="password"
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
            <b-button variant="dark" @click="position()"
              >Save Password</b-button
            >
          </div>
        </div>
      </b-modal>

      <b-modal
        id="modal-Print"
        title="Print PDF"
        hide-footer
        size="bg"
        centered
      >
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