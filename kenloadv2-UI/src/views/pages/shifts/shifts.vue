<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <div class="row overflow-auto">
      <div class="col-lg-12">
        <div class="card">
          <div class="card-body">
            <div class="box fc-header-toolbar">
              <div class="row mt-4">
                <div class="col-sm-0 col-md-2">
                  <div class="px-3 py-2">
                    <div id="tickets-table_length" class="dataTables_length">
                      <label class="d-inline-flex align-items-center">
                        <b-form-select
                          class="m-2 popup-form btn btn-primary"
                          size="sm"
                          v-model="orgstn"
                          :options="org"
                        ></b-form-select>
                      </label>
                    </div>
                  </div>
                </div>
                <div class="col-sm-0 col-md-3">
                  <div class="px-3 py-2">
                    <div id="tickets-table_length" class="dataTables_length">
                      <label class="d-inline-flex align-items-center">
                        <b-form-select
                          class="m-2 popup-form btn btn-primary"
                          size="sm"
                          v-model="pstn"
                          :options="postn"
                        ></b-form-select>
                      </label>
                    </div>
                  </div>
                </div>
                <div class="col-sm-0 col-md-4">
                  <a
                    class="popup-form btn btn-primary"
                    href="javascript: void(0);"
                    v-b-modal.modal-1
                    @click="clearvalues()"
                    >+ Add Shift</a
                  >
                </div>
                <div class="col-sm-0 col-md-4">
                  <a
                    class="popup-form btn btn-success"
                    href="javascript: void(0);"
                    v-b-modal.modal-2
                    @click="clearvalues()"
                    >Shifts Table View</a
                  >
                </div>
                <div class="col-sm-0 col-md-4">
                  <div class="row fc-toolbar-ltr m-2 d-flex">
                    <div class="col d-flex">
                      <span class="mr-2">Morning Shift </span>
                      <i class="fas fa-circle text-success m-auto"></i>
                    </div>
                    <div class="col d-flex">
                      <span class="mr-2">Night Shift</span>
                      <i class="fas fa-circle text-primary m-auto"></i>
                    </div>
                    <div class="col d-flex">
                      <span class="mr-2">On Leave</span>
                      <i class="fas fa-circle text-info m-auto"></i>
                    </div>
                    <div class="col d-flex">
                      <span class="mr-2">OFF Duty</span>
                      <i class="fas fa-circle text-danger m-auto"></i>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="card-body">
      <full-calendar :events="shiftSchedules" :config="config"></full-calendar>
    </div>
    <b-modal
      id="modal-2"
      :title="title"
      hide-footer
      scrollable
      size="xl"
      centered
    >
      <div class="row overflow-auto">
        <div class="col-lg-12">
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
                    v-b-modal.modal-1
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
          <div class="card">
            <div class="card-body">
              <div class="row">
                <div class="col-sm-12 col-md-6">
                  <div
                    id="tickets-table_length"
                    class="dataTables_length"
                    style="word-wrap: break-word;"
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
                    <label class="d-inline-flex align-items-center w-normal">
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
              <div class="row">
                <div class="col-sm-4 col-md-2 m-2">
                  <b-form-group label="Role" label-for="role-input">
                    <multiselect
                      class="form-control text-white"
                      v-model="role"
                      :options="roles"
                      :placeholder="role"
                      :multiple="true"
                      :editable="true"
                    ></multiselect>
                  </b-form-group>
                </div>
                <div class="col-sm-4 col-md-2 m-2">
                  <b-form-group label="Shift" label-for="role-input">
                    <multiselect
                      class="form-control text-white"
                      v-model="shift_type"
                      :options="shift_options"
                      :placeholder="shift_type"
                      :multiple="true"
                      :editable="true"
                    ></multiselect>
                  </b-form-group>
                </div>
                <div class="col-sm-4 col-md-2 m-2 mt-3">
                  <div id="tickets-table-date-picker">
                    <label>
                      From&nbsp;
                      <date-picker
                        class="form-control"
                        v-model="from"
                        placeholder="2022-09-27"
                        type="date"
                      ></date-picker>
                    </label>
                  </div>
                </div>
                <div class="col-sm-4 col-md-2 m-2 mt-3">
                  <div id="tickets-table-date-picker">
                    <label>
                      To&nbsp;
                      <date-picker
                        class="form-control"
                        v-model="to"
                        placeholder="2022-09-27"
                        type="date"
                      ></date-picker>
                    </label>
                  </div>
                </div>
                <div class="col-sm-4 col-md-2 mt-4 m-auto">
                  <div id="tickets-table-date-picker">
                    <b-button
                      variant="dark"
                      class="uil uil-search"
                      @click="filterShifts()"
                      >Search</b-button
                    >
                  </div>
                </div>
              </div>
              <div class="table-responsive mb-0">
                <b-table
                  fixed
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
                  <template v-slot:cell(check)="data">
                    <div class="custom-control custom-checkbox">
                      <input
                        type="checkbox"
                        class="custom-control-input"
                        :id="`contacusercheck${data.item.id}`"
                      />
                      <label
                        class="custom-control-label"
                        :for="`contacusercheck${data.item.id}`"
                      ></label>
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
                          v-b-modal.modal-1
                          @click="
                            edit(
                              data.index,
                              data.item.id,
                              data.item.position,
                              data.item.email,
                              data.item.from,
                              data.item.to,
                              data.item.shift_type
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
                              data.item.email,
                              data.item.from,
                              data.item.to,
                              data.item.shift_type
                            )
                          "
                        >
                          <i class="uil uil-trash-alt font-size-18"></i>
                        </a>
                      </li>
                      <b-dropdown
                        class="list-inline-item"
                        variant="white"
                        right
                        toggle-class="text-muted font-size-18 px-2"
                      >
                        <template v-slot:button-content>
                          <i class="uil uil-ellipsis-v"></i>
                        </template>

                        <a class="dropdown-item" href="/profile">View</a>
                        <a class="dropdown-item" href="/profile">Action</a>
                      </b-dropdown>
                    </ul>
                  </template>
                </b-table>
              </div>
            </div>
          </div>
        </div>
      </div>
    </b-modal>
    <b-modal
      id="modal-1"
      :title="modaltitle"
      scrollable
      hide-footer
      size="xl"
      centered
    >
      <div class="card-body">
        <form>
          <div class="row">
            <div class="col-sm-6 col-md-3">
              <b-form-group label="Role" label-for="role-input">
                <multiselect
                  class=""
                  v-model="shift_type"
                  :options="shift_options"
                  placeholder="Developer"
                  :multiple="false"
                  :editable="true"
                ></multiselect>
              </b-form-group>
            </div>
            <div class="col-lg-6">
              <div class="mb-3">
                <label for="name">Select User</label>
                <select
                  class="form-select"
                  v-model="user"
                  @change="getUserRole()"
                >
                  <option v-for="usr in userList" :key="usr">
                    {{ usr.email }}
                  </option>
                </select>
                <input type="hidden" :value="user" />
              </div>
            </div>
            <div class="col-lg-6">
              <div class="mb-3">
                <div class="input-group">
                  <div class="input-group-text col-sm-4">From:</div>
                  <input
                    class="form-control"
                    type="datetime-local"
                    placeholder="2022-01-01T00:00:00"
                    v-model="from"
                  />
                </div>
              </div>
            </div>
            <div class="col-lg-6">
              <div class="mb-3">
                <div class="input-group">
                  <div class="input-group-text col-sm-4">To:</div>
                  <input
                    class="form-control"
                    type="datetime-local"
                    placeholder="2022-01-01T00:00:00"
                    v-model="to"
                  />
                </div>
              </div>
            </div>
            <div class="col-lg-6">
              <div class="mb-3">
                <div class="input-group">
                  <div class="input-group-text col-sm-4">Role:</div>
                  <input class="form-control" type="text" v-model="userRole" />
                </div>
              </div>
            </div>
            <div class="col-lg-6">
              <div class="mb-3">
                <div class="input-group">
                  <div class="input-group-text col-sm-4">Repeat Daily:</div>
                  <input class="m-auto " type="checkbox" v-model="selected" />
                </div>
              </div>
            </div>
          </div>
          <div class="row">
            <div class="col-lg-6">
              <div class="text-end">
                <div class="col-sm-3 mb-2">
                  <b-button
                    v-show="!editmode"
                    variant="dark"
                    @click="add()"
                    v-b-modal.modal-1
                    >Add Shift</b-button
                  >
                  <b-button
                    v-show="editmode"
                    variant="dark"
                    @click="editrec()"
                    v-b-modal.modal-1
                    >Edit {{ title }}</b-button
                  >
                </div>
              </div>
            </div>
          </div>
        </form>
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

<script>
//import moment from "moment";
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";
import Swal from "sweetalert2";
import axios from "axios";

import { FullCalendar } from "vue-full-calendar";
import "../../../../node_modules/vue-simple-calendar/dist/style.css";
// The next two lines are optional themes
import "../../../../node_modules/vue-simple-calendar/static/css/default.css";
import "../../../../node_modules/vue-simple-calendar/static/css/holidays-us.css";
import reportdet from "@/components/report/header";
import Multiselect from "vue-multiselect";

export default {
  components: { Layout, PageHeader, FullCalendar, reportdet, Multiselect },
  page: {
    title: "Shifts",
    meta: [
      {
        name: "description",
        content: appConfig.description,
      },
    ],
  },
  data() {
    return {
      user: "Simon Ryles",
      end_date: null,
      bgcolor: ["green", "black", "blue", "red"],
      from: new Date(),
      to: new Date(),
      role: "All",
      roles: [],
      userRole: "CoW",
      selected: true,
      shift_type: "All",
      color: "black",
      title: "Shifts",
      modaltitle: "Add Shifts",
      addshift: false,
      editshift: false,
      deleteshift: false,
      viewUsers: false,
      editmode: false,
      shiftSchedules: [],
      filter: null,
      filterOn: [],
      sortBy: "age",
      sortDesc: false,
      items: [
        {
          text: "Shifts",
        },
        {
          text: "Shifts",
          active: true,
        },
      ],
      fields: [
        {
          key: "id",
          label: "#",
        },
        {
          key: "email",
        },
        {
          key: "shift_type",
        },
        {
          key: "from",
        },
        {
          key: "to",
        },
        {
          key: "to",
        },
        {
          key: "position",
        },
        "action",
      ],
      orderData: [],
      myindex: "",
      userList: [],
      totalRows: 1,
      currentPage: 1,
      perPage: 10,
      pageOptions: [1, 5, 10, 25, 50, 100, 500, 1000, 2000, 3000],
      showDate: new Date(),
      postn: [{ value: "all", text: "All Positions" }],
      org: [
        { value: null, text: "Select Organisation", disabled: true },
        { value: "a", text: "KeNHA" },
        { value: "b", text: "DANCA" },
        { value: "c", text: "MASTERSPACE" },
        { value: "d", text: "EBENEZER" },
      ],
      shift_options: [
        "All",
        "Morning Shift",
        "Night Shift",
        "Leave",
        "Off Duty",
      ],
      orgstn: null,
      pstn: "all",
      //to be implemented in api & db
      config: {
        defaultView: "month",
        weekends: true,
        // 	eventRender: function(event, element) {
        // console.log(event)
        // }
      },
    };
  },
  watch: {
    viewShifts(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Opened Shifts:${newValue}`,
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
    addshift(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Added Shift:${newValue},value:(${this.shift_type},${this.user},\n${this.position})`,
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
    editshift(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Edited shift:${newValue},value:(${this.shift_type},${this.user},${this.position})`,
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
    deleteshift(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Delete shift:${newValue},shift id:${this.id}`,
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
    rows() {
      return this.orderData.length;
    },
  },
  mounted() {
    // Set the initial number of items
    this.updatearray();
    this.updateUsersList();
    this.totalRows = this.items.length;
    this.shiftSchedules;
    //calc hrs
    console.log("mounted shifts");
  },
  methods: {
    filterShifts() {
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      var todate = this.mydatenew(new Date(this.to));
      var fromdate = this.mydatenew(new Date(this.from));
      axios
        .get(
          window.$http +
            "Shifts/" +
            this.role.toString() +
            "/" +
            fromdate +
            "/" +
            todate +
            "/" +
            this.shift_type.toString(),
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          //console.log(this.orderData);
          console.log(response.data);
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
    getUserRole() {
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
        .get(window.$http + "Users/byMail/" + this.user, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.userRole = response.data.userRole.split(",")[0];
          //console.log(this.orderData);
          console.log(response.data);
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
    onContext(ctx) {
      this.context = ctx;
    },
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
    },
    addDays(date, days) {
      const copy = new Date(Number(date));
      copy.setDate(date.getDate() + days);
      return copy;
    },
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
        .get(window.$http + `Shifts`, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          this.shiftSchedules.length == this.orderData.length;
          console.log("data=>");
          console.log(this.orderData[0]);
          for (var i = 0; i < this.orderData.length; i++) {
            if (this.orderData[i].shift_type == "Morning Shift") {
              this.shiftSchedules.push({
                title:
                  this.orderData[i].email + "," + this.orderData[i].shift_type,
                start: this.orderData[i].from,
                end: this.orderData[i].to,
                allDay: this.orderData[i].repeat,
                backgroundColor: this.bgcolor[0],
              });
            } else if (this.orderData[i].shift_type == "Night Shift") {
              this.shiftSchedules.push({
                title:
                  this.orderData[i].email + "," + this.orderData[i].shift_type,
                start: this.orderData[i].from,
                end: this.orderData[i].to,
                allDay: this.orderData[i].repeat,
                backgroundColor: this.bgcolor[1],
              });
            } else if (this.orderData[i].shift_type == "Leave") {
              this.shiftSchedules.push({
                title:
                  this.orderData[i].email + "," + this.orderData[i].shift_type,
                start: this.orderData[i].from,
                end: this.orderData[i].to,
                allDay: this.orderData[i].repeat,
                backgroundColor: this.bgcolor[2],
              });
            } else {
              this.shiftSchedules.push({
                title:
                  this.orderData[i].email + "," + this.orderData[i].shift_type,
                start: this.orderData[i].from,
                end: this.orderData[i].to,
                allDay: this.orderData[i].repeat,
                backgroundColor: this.bgcolor[3],
              });
            }
          }
          axios
            .get(window.$http + "UserRoles", {
              headers: window.$headers,
            })
            .then((response) => {
              // JSON responses are automatically parsed.
              response.data.forEach((item) => {
                this.roles.push(item.userrole_name);
              });
            })
            .then(() => {
              axios
                .get(window.$http + `Users`, { headers: window.$headers })
                .then((response) => {
                  // JSON responses are automatically parsed.
                  this.userList = response.data;
                  //console.log(this.orderData);
                  console.log(response.data);
                });
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
    printpdf(pl) {
      //console.log(this.tokenString);
      this.pl = pl;
      const data = this.orderData.map((row) => ({
        ID: row.id,
        Email: row.email,
        "Shift Type": row.shift_type,
        From: row.from,
        To: row.to,
        Role: row.position,
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
      //this.records = data;
      //alert(headers);
    },

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
        Email: row.email,
        "Shift Type": row.shift_type,
        From: row.from,
        To: row.to,
        Role: row.position,
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
    add() {
      if (this.user.trim() == "") {
        Swal.fire("Please select user!");
        return;
      }
      this.shiftSchedules.push({
        title: this.user + "," + this.shift_type,
        start: this.from,
        end: this.to,
        allDay: this.selected,
        backgroundColor: "gold",
      });
      var orderid = this.orderData.length + 1;
      this.orderData.push({
        id: orderid,
        position: this.userRole,
        email: this.user,
        from: this.from,
        to: this.to,
        shift_type: this.shift_type,
        repeat: this.selected,
      });
      const data = {
        email: this.user,
        shift_type: this.shift_type,
        from: this.from,
        to: this.to,
        position: this.userRole,
        repeat: this.selected,
        deletestatus: false,
      };
      axios
        .post(window.$http + `Shifts`, data, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          console.log(response.data);
          this.addshift = true;
          console.log(data);
          Swal.fire({
            position: "center",
            icon: "success",
            title: "Your work has been saved",
            showConfirmButton: false,
            timer: 5000,
          });
        })
        .catch((e) => {
          Swal.fire("" + e);
        });
    },
    editrec() {
      if (this.user.trim() == "") {
        Swal.fire("Please select a user!");
        return;
      }

      //alert(this.myindex);

      const data = {
        id: this.id,
        position: this.userRole,
        email: this.user,
        from: this.from,
        to: this.to,
        shift_type: this.shift_type,
        repeat: this.selected,
        deletestatus: false,
      };
      //alert(this.screenname);
      axios
        .put(window.$http + `Shifts/` + this.id, data, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData[this.myindex].id = this.id;
          this.orderData[this.myindex].position = this.userRole;
          this.orderData[this.myindex].email = this.user;
          this.orderData[this.myindex].from = this.from;
          this.orderData[this.myindex].to = this.to;
          this.orderData[this.myindex].shift_type = this.shift_type;

          console.log(response.data);
          this.editshift = true;
          if (response.data.trim() != "") {
            Swal.fire({
              position: "center",
              icon: "success",
              title: "Your work has been saved" + response.data,
              showConfirmButton: false,
              timer: 1500,
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
    edit(index, id, position, email, from, to, shift_type, repeat) {
      this.modaltitle = "Edit Shifts";
      this.editmode = true;
      this.id = id;
      this.userRole = position;
      this.user = email;
      this.from = from;
      this.to = to;
      this.shift_type = shift_type;
      this.repeat = repeat;
      this.myindex = index;
    },
    deleterec(index, id, position, email, from, to, shift_type) {
      this.id = id;
      this.position = position;
      this.user = email;
      this.from = from;
      this.to = to;
      this.shift_type = shift_type;
      Swal.fire({
        title: "Are you sure, you want to delete " + this.shift_type + "?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, delete it!",
      }).then((result) => {
        if (result.value) {
          axios
            .delete(window.$http + `Shifts/` + this.id, {
              headers: window.$headers,
            })
            .then((response) => {
              // JSON responses are automatically parsed.
              console.log(response.data);
              this.deleteshift = true;
              this.orderData.splice(index, 1);
              this.shiftSchedules.splice(index, 1);
              Swal.fire(
                this.shift_type + " Deleted!",
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
      this.user = "";
      this.userRole = "";
      this.email = "";
      this.repeat = false;
      this.editmode = false;
      this.modaltitle = "Add Shift";
    },
    handleSubmit() {
      console.log("Error on submit");
    },
    showImg(index) {
      this.index = index;
      this.visible = true;
    },
    handleHide() {
      this.visible = false;
    },
    showCaptiomImg(index) {
      this.index = index;
      this.visibleCaption = true;
    },
    handleCaptionHide() {
      this.visibleCaption = false;
    },
    setShowDate(d) {
      this.showDate = d;
    },
  },
};
</script>
<style scoped></style>
