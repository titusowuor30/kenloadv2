<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";
import Swal from "sweetalert2";
import axios from "axios";

// import { CalendarView, CalendarViewHeader } from "vue-simple-calendar";
// import from "vue-simple-calendar/dist/style.css"
// // The next two lines are optional themes
// import from "vue-simple-calendar/static/css/default.css"
// import from "vue-simple-calendar/static/css/holidays-us.css"

export default {
  components: { Layout, PageHeader },
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
      id: "",
      showDate: new Date(),
      value: "",
      context: null,
      addshift: false,
      editshift: false,
      deleteshift: false,
      viewUsers: false,
      editmode: false,
      restrict_day: false,
      restrict_week: false,
      user: "Simon Ryles",
      shift_type: "",
      title: "Shifts",
      modaltitle: "Add Shifts",
      items: [
        {
          text: "Shifts",
        },
        {
          text: "Shifts",
          active: true,
        },
      ],
      orderData: [],
      myindex: "",
      userList: [],
      totalRows: 1,
      currentPage: 1,
      perPage: 10,
      pageOptions: [10, 25, 50, 100],
      org: [
        { value: null, text: "Select Organisation", disabled: true },
        { value: "a", text: "KeNHA" },
        { value: "b", text: "DANCA" },
        { value: "c", text: "MASTERSPACE" },
        { value: "d", text: "EBENEZER" },
      ],
      postn: [{ value: "all", text: "All Positions" }],
      orgstn: null,
      pstn: "all",

      filter: null,
      filterOn: [],
      sortBy: "age",
      sortDesc: false,
      fields: [
        {
          key: "check",
          label: "",
        },
        {
          key: "user",
        },
        {
          key: "shift_type",
        },
        "action",
      ],
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
        details: `Opened Users' list:${newValue}`,
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
        details: `Added Shift:${newValue},value:(${this.shift_type},\n${this.name},${this.email},${this.hours}\n
      ${this.position})`,
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
        details: `Edited shift:${newValue},value:(${this.shift_type},\n${this.name},${this.email},${this.hours}\n
      ${this.position})`,
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
    //calc hrs
  },
  methods: {
    setShowDate(d) {
      this.showDate = d;
    },
    onContext(ctx) {
      this.context = ctx;
    },
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
    },
    updateUsersList() {
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
        .get(window.$http + `Users`, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.userList = response.data;
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
    validate() {
      let val = false;
      this.traillerreg = this.traillerreg
        .toUpperCase()
        .replaceAll("\\s", "")
        .replaceAll("[^a-zA-Z0-9]", "")
        .replaceAll(" ", "")
        .trim();
      this.registration = this.registration
        .toUpperCase()
        .replaceAll("\\s", "")
        .replaceAll("[^a-zA-Z0-9]", "")
        .replaceAll(" ", "")
        .trim();
      if (this.permitno.trim() == "") {
        Swal.fire("Please enter permit number!");
        return val;
      }
      if (this.registration.trim() == "") {
        Swal.fire("Please enter registration!");
        return val;
      }
      if (this.traillerreg.trim() == "") {
        Swal.fire("Please enter trailer Number!");
        return val;
      }
      if (this.axleconf.trim() == "") {
        Swal.fire("Please enter Axle Configuration!");
        return val;
      }
      if (this.status.trim() == "-Choose One-") {
        Swal.fire("Please select Status !");
        return val;
      }

      val = true;
      return val;
    },
    getv(val) {
      if (val < 10) {
        val = "0" + val;
      }
      return val;
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
    add() {
      if (this.name.trim() == "") {
        Swal.fire("Please enter Name!");
        return;
      }
      var dfrom2;
      var dto2;
      if (!this.restrict_day) {
        dfrom2 = this.getcurrentdate(new Date(this.dfrom));
        dto2 = this.getcurrentdate(new Date(this.dtso));
      }
      if (!this.restrict_week) {
        dfrom2 = this.getcurrentdate(new Date(this.wfrom));
        dto2 = this.getcurrentdate(new Date(this.wto));
      }
      var orderid = this.orderData.length + 1;
      this.orderData.push({
        id: orderid,
        name: this.name,
        position: this.position,
        email: this.email,
        hours: this.hours,
        from: dfrom2,
        to: dto2,
        shift_type: this.shift_type,
      });
      const data = {
        name: this.name,
        position: this.position,
        email: this.email,
        hours: this.hours,
        from: this.dfrom2,
        to: this.dto2,
        shift_type: this.shift_type,
        restrict_day: this.restrict_day,
        restrict_week: this.restrict_week,
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
            timer: 1500,
          });
        })
        .catch((e) => {
          Swal.fire("" + e);
        });
    },
    editrec() {
      if (this.name.trim() == "") {
        Swal.fire("Please enter Name!");
        return;
      }

      //alert(this.myindex);

      const data = {
        id: this.id,
        name: this.name,
        position: this.position,
        email: this.email,
        hours: this.hours,
        from: this.dfrom2,
        to: this.dto2,
        shift_type: this.shift_type,
        restrict_day: this.restrict_day,
        restrict_week: this.restrict_week,
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
          this.orderData[this.myindex].name = this.name;
          this.orderData[this.myindex].position = this.position;
          this.orderData[this.myindex].email = this.email;
          this.orderData[this.myindex].hours = this.hours;
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
    edit(
      index,
      id,
      name,
      position,
      email,
      hours,
      from,
      to,
      shift_type,
      restrict_day,
      restrict_week
    ) {
      this.modaltitle = "Edit Shifts";
      this.editmode = true;
      this.id = id;
      this.name = name;
      this.position = position;
      this.email = email;
      this.hours = hours;
      this.from = from;
      this.to = to;
      this.shift_type = shift_type;
      this.restrict_day = restrict_day;
      this.restrict_week = restrict_week;
      this.myindex = index;
    },
    deleterec(
      index,
      id,
      name,
      position,
      email,
      hours,
      from,
      to,
      shift_type,
      restrict_day,
      restrict_week
    ) {
      this.id = id;
      this.name = name;
      this.position = position;
      this.email = email;
      this.hours = hours;
      this.from = from;
      this.to = to;
      this.shift_type = shift_type;
      this.restrict_day = restrict_day;
      this.restrict_week = restrict_week;
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
      this.name = "";
      this.position = "";
      this.email = "";
      this.hours = "";
      this.restrict_day = false;
      this.restrict_week = false;
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
  },

  middleware: "authentication",
};
</script>
<template>
  <Layout>
    <PageHeader :title="title" :items="items" />

    <div class="row">
      <div class="col-lg-12">
        <div class="card">
          <div class="card-body">
            <form @submit.prevent="handleSubmit">
              <div>
                <a
                  class="popup-form btn btn-primary"
                  href="javascript: void(0);"
                  v-b-modal.modal-1
                  @click="clearvalues()"
                  >+ Add Shift</a
                >
                <div class="row mt-4">
                  <div class="col-sm-0 col-md-2">
                    <div id="tickets-table_length" class="dataTables_length">
                      <label class="d-inline-flex align-items-center">
                        <b-form-select
                          class="m-2"
                          size="sm"
                          v-model="orgstn"
                          :options="org"
                        ></b-form-select>
                      </label>
                    </div>
                  </div>
                  <div class="col-sm-0 col-md-3">
                    <div id="tickets-table_length" class="dataTables_length">
                      <label class="d-inline-flex align-items-center">
                        <b-form-select
                          class="m-2"
                          size="sm"
                          v-model="pstn"
                          :options="postn"
                        ></b-form-select>
                      </label>
                    </div>
                  </div>
                  <div class="col-sm-0 col-md-4">
                    <div id="tickets-table_length" class="dataTables_length">
                      <calendar-view
                        :show-date="showDate"
                        @show-date-change="setShowDate"
                        class="holiday-us-traditional holiday-us-official"
                      />
                    </div>
                  </div>
                </div>

                <div class="table-responsive mb-0">
                  <b-table
                    class="table table-centered table-nowrap"
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
                                data.item.name,
                                data.item.position,
                                data.item.email,
                                data.item.hours,
                                data.item.from,
                                data.item.to,
                                data.item.shift_type,
                                data.item.restrict_day,
                                data.item.restrict_week
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
                                data.item.name,
                                data.item.position,
                                data.item.email,
                                data.item.hours,
                                data.item.from,
                                data.item.to,
                                data.item.shift_type,
                                data.item.restrict_day,
                                data.item.restrict_week
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

                <b-modal
                  id="modal-1"
                  :title="modaltitle"
                  hide-footer
                  size="lg"
                  centered
                >
                  <div class="card-body">
                    <form>
                      <div class="row">
                        <div class="col-lg-6">
                          <div class="mb-3">
                            <label for="name">Shift Type</label>

                            <select class="form-select" v-model="shift_type">
                              <option>Night Shift</option>
                              <option>Morning Shift</option>
                            </select>
                            <input type="hidden" :value="shift_type" />
                          </div>
                        </div>
                        <div class="col-lg-6">
                          <div class="mb-3">
                            <label for="name">Select User</label>
                            <select class="form-select" v-model="user">
                              <option v-for="usr in userList" :key="usr">
                                {{ usr.email }}
                              </option>
                            </select>
                            <input type="hidden" :value="shift_type" />
                          </div>
                        </div>
                      </div>
                      <div class="row">
                        <div class="col-lg-12">
                          <div class="text-end">
                            <div class="col-sm-3 mb-2">
                              <b-button
                                v-show="!editmode"
                                variant="dark"
                                @click="add()"
                                >Add {{ title }}</b-button
                              >
                              <b-button
                                v-show="editmode"
                                variant="dark"
                                @click="editrec()"
                                >Edit {{ title }}</b-button
                              >
                            </div>
                          </div>
                        </div>
                      </div>
                    </form>
                  </div>
                </b-modal>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </Layout>
</template>
