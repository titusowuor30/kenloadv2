<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";
import axios from "axios";
//import addusers from "../../pages/account/profile";
import reportdet from "@/components/report/header";
import vue2Dropzone from "vue2-dropzone";
import "vue2-dropzone/dist/vue2Dropzone.min.css";
import Multiselect from "vue-multiselect";
import Swal from "sweetalert2";

/**
 * User list component
 */
export default {
  components: {
    Layout,
    PageHeader,
    /*addusers,*/ reportdet,
    Multiselect,
    vueDropzone: vue2Dropzone,
  },
  page: {
    title: "Users",
    meta: [
      {
        name: "description",
        content: appConfig.description,
      },
    ],
  },
  data() {
    return {
      title: "User List",
      items: [
        {
          text: "Users",
        },
        {
          text: "User List",
          active: true,
        },
      ],
      userList: [],
      id: "",
      typesubmit: false,
      shift_type: "Morning Shift",
      selected: false,
      from: null,
      to: null,
      adduserlog: false,
      roles: [],
      Organization: [
        "KeNHA",
        "Gokhan-Masterspace",
        "Ebenezer",
        "Danka",
        "Interways",
      ],
      dropzoneOptions: {
        url: "https://httpbin.org/post",
        thumbnailWidth: 150,
        maxFilesize: 0.5,
        headers: {
          "My-Awesome-Header": "header value",
        },
      },
      //pass policy fields
      pass_char: 1,
      pass_nums: 1,
      pass_min_length: 8,
      pass_small_chars: 1,
      pass_upper_chars: 1,

      orderData: [],
      editmode: false,
      names: "",
      password: "",
      cPassword: "",
      email: "",
      organization: "",
      role: "",
      cell: "",
      sendsms: false,
      sendemail: false,
      headers: null,
      uniqueCars: null,
      pl: "",
      showme: true,
      deleteuserlog: false,
      viewUsers: false,
      username: "",
      totalRows: 1,
      currentPage: 1,
      perPage: 10,
      pageOptions: [5, 10, 25, 50, 100],
      filter: null,
      filterOn: [],
      sortBy: "age",
      sortDesc: false,
      fields: [
        {
          key: "id",
          label: "NO",
        },
        {
          key: "fullnames",
          label: "Full Name",
        },
        {
          key: "userRole",
          label: "UserRole",
        },
        {
          key: "phone",
        },
        {
          key: "email",
        },
        {
          key: "organization",
        },
        {
          key: "deletedstatus",
          label: "Acount Status",
        },
        "action",
      ],
    };
  },
  watch: {
    adduserlog(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      console.log();
      console.log(
        `id:${this.id} date:${currentdate} user:${curentuser} browser:${window.navigator.userAgent} edit user:${this.names} ${newValue} comp:${window.localStorage.clientip}`
      );
      axios
        .post(
          window.$http + "AuditLogs",
          {
            datetime: currentdate,
            useremail: curentuser,
            application: window.navigator.userAgent,
            details: `Add User:${newValue};\nDetails:(name:${
              this.names
            },roles[${this.role.toString()}],phone:${this.cell},organization:${
              this.organization
            })`,
            computer: window.localStorage.clientip,
          },
          { headers: window.$headers }
        )
        .then((response) => {
          console.log(response.data);
        })
        .catch((e) => {
          console.log(e);
        });
    },
    viewUsers(newValue) {
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
    printpdf(pl, p_pdf) {
      this.printReport = true;
      p_pdf = "Clicked print pdf button";
      this.clickedPrintpdf = p_pdf;
      this.pl = pl;
      const data = this.userList.map((row) => ({
        ID: row.id,
        useremail: row.email,
        fullnames: row.fullnames,
        Role: row.userRole,
        organization: row.organization,
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
    deleteuserlog(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Delete user:${newValue},user account ${this.username}`,
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
      return this.userList.length;
    },
  },
  mounted() {
    // Set the initial number of items
    this.getPassWordPolicy();
    this.updatearray();
    this.getUser();
    this.focusInput();
    // Set the initial number of items
    this.totalRows = this.items.length;
    //alert(this.pass_min_length);
  },
  created() {
    if (window.location.href != "") {
      this.viewUsers = true;
    }
    console.log("current user:" + JSON.parse(localStorage.user).email);
    axios
      .get(window.$http + "Users", { headers: window.$headers })
      .then((response) => {
        // JSON responses are automatically parsed.
        this.userList = response.data;
      })
      .catch((e) => {
        this.errors.push(e);
        console.log(e);
      });
  },
  methods: {
    //pass policy
    getPassWordPolicy() {
      axios
        .get(window.$http + "AccessPassPolicy", { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          console.log(response.data[0]);
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
    getUser() {
      axios
        .get(window.$http + "Users/" + this.$route.params.id, {
          headers: window.$headers,
        })
        .then((response) => {
          this.email = response.data.email;
          this.names = response.data.fullnames;
          this.organization = response.data.organization;
          this.role = response.data.userRole;
          this.cell = response.data.phone;
        })
        .catch((e) => {
          this.errors.push(e);
          console.log(e);
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
      //window.$http+'Users',{ headers: {"Authorization" : `Bearer ${tokenStr}`} }
      axios
        .get(window.$http + `userroles`, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.roles = [];
          response.data.forEach((e) => {
            this.roles.push(e.userrole_name);
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
    handleSubmit() {
      console.log("Error on submit");
    },
    validEmail: function(email) {
      var re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
      return re.test(email);
    },
    addRec() {
      //alert(this.$v.typeform.email.$error);
      if (!this.validEmail(this.email)) {
        Swal.fire("Please enter Email Address!");
        return;
      }

      if (this.names.trim() == "") {
        Swal.fire("Please enter full names!");
        return;
      }

      if (this.organization.trim() == "") {
        Swal.fire("Please select Organization!");
        return;
      }
      // if (this.username.trim() == "") {
      //   this.username = this.email;
      // }
      if (this.cell.trim() == "") {
        Swal.fire("Please enter Cell Phone number!");
        return;
      }
      if (this.password.trim() == "") {
        Swal.fire("Please enter password!");
        return;
      }
      if (this.cpassword.trim() == "") {
        Swal.fire("Please enter confirm password!");
        return;
      }
      if (this.cpassword.trim() != this.password.trim()) {
        Swal.fire("Confirm password and password does not match!");
        return;
      }
      //check password policy
      if (this.password.length < this.pass_min_length) {
        Swal.fire(
          "Password must be at least " +
            this.pass_min_length +
            " characters long!"
        );
        return;
      }
      if (this.pass_small_chars == 1) {
        if (this.password.search(/[a-z]/) < 0) {
          Swal.fire(
            "Password must contain at least one lowercase character e.g a-z!"
          );
          return;
        }
      }
      if (this.pass_upper_chars == 1) {
        if (this.password.search(/[A-Z]/) < 0) {
          Swal.fire(
            "Password must contain at least one Uppercase character e.g A-z!"
          );
          return;
        }
      }
      if (this.pass_nums == 1) {
        if (this.password.search(/[0-9]/) < 0) {
          Swal.fire(
            "Password must contain at least one Numeric character e.g 0-9!"
          );
          return;
        }
      }
      if (this.pass_char == 1) {
        if (this.password.search(/[!@#$%^&*]/) < 0) {
          Swal.fire(
            "Password must contain at least one Special character e.g !@#$%^&*!"
          );
          return;
        }
      }
      var orderid = this.userList.length + 1;
      this.userList.push({
        id: orderid,
        email: this.email,
        fullnames: this.names,
        organization: this.organization,
        userRole: this.role,
        phone: this.cell,
        password: this.password,
        sendmail: this.sendmail,
        sendsms: this.sendsms,
      });
      var timenow = this.gettime();
      axios
        .post(
          window.$http + "AuthManagement/Register",
          {
            username: this.email,
            email: this.email,
            password: this.password,
          },
          { headers: window.$headers }
        )
        .then(() => {
          // JSON responses are automatically parsed.
          //response.data;
          axios
            .post(
              window.$http + "Users",
              {
                fullnames: this.names,
                userpass: {
                  id: 0,
                  userpassword: this.password,
                  expirationstatus: false,
                },
                userRole: this.role.toString(),
                organization: this.organization.toString(),
                description: "string",
                //deletedstatus: 0,
                timestamp: timenow,
                email: this.email,
                phone: this.cell,
                sendmail: this.sendmail,
                sendsms: this.sendmail,
                resettime: timenow,
              },
              { headers: window.$headers }
            )
            .then((response) => {
              // JSON responses are automatically parsed.
              console.log(response.data);
              this.addshift = true;
              this.$router.push({
                name: "users-list",
                params: {},
              });
            })
            .catch((e) => {
              this.errors.push(e);
            });
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Error:  " + e,
            showConfirmButton: true,
          });
        });

      //console.log("Passed through here");

      Swal.fire({
        position: "center",
        icon: "success",
        title: "Your work has been saved. User " + this.email + " Added",
        showConfirmButton: false,
        timer: 1500,
      }).then((result) => {
        result;
        this.clearvalues();
      });
    },
    gettime() {
      //2021-06-11T09:05:53.07
      //2021-5-11T12:54.48
      var DateNow = new Date();
      var DateNowString;
      DateNow.setDate(DateNow.getDate() + 20);
      DateNowString =
        DateNow.getFullYear() +
        "-" +
        ("0" + (DateNow.getMonth() + 1)).slice(-2) +
        "-" +
        ("0" + DateNow.getDate()).slice(-2) +
        "T" +
        ("0" + DateNow.getHours()).slice(-2) +
        ":" +
        ("0" + DateNow.getMinutes()).slice(-2) +
        ":" +
        ("0" + DateNow.getSeconds()).slice(-2) +
        "." +
        ("0" + DateNow.getMilliseconds()).slice(-2);
      return DateNowString;
    },
    deleterec(id, username) {
      this.id = id;
      this.username = username;
      Swal.fire({
        title: "Are you sure, you want to disable " + username + "?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#34c38f",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, disable account!",
      }).then((result) => {
        if (result.value) {
          axios
            .delete(window.$http + "Users/" + id, {
              headers: window.$headers,
            })
            .then((response) => {
              console.log(response.data);
              this.deleteuserlog = true;
            })
            .then(() => {
              Swal.fire(
                this.username + " Disabled!",
                "User account has been disabled.",
                "success"
              );
            });
        } else {
          Swal.fire({
            title: "Please Wait !",
            html: "Checking...",
            allowOutsideClick: false,
            showConfirmButton: false,
            willOpen: () => {
              Swal.showLoading();
            },
          });
          axios
            .get(window.$http + "Users", { headers: window.$headers })
            .then((response) => {
              // JSON responses are automatically parsed.
              this.userList = response.data;
              Swal.close();
            })
            .catch((e) => {
              this.errors.push(e);
              console.log(e);
            });
        }
      });
    },
    enablerec(id, username) {
      this.id = id;
      this.username = username;
      Swal.fire({
        title: "Are you sure, you want to activate " + username + "?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#34c38f",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, activate account!",
      }).then((result) => {
        if (result.value) {
          axios
            .delete(window.$http + "Users/enable/" + id, {
              headers: window.$headers,
            })
            .then((response) => {
              console.log(response.data);
            })
            .then(() => {
              Swal.fire(
                this.username + " Activated!",
                "User account has been activated.",
                "success"
              );
            });
        }
      });
    },
    gotoprofile() {
      this.$route.push({
        name: "profile",
        params: { username: JSON.parse(localStorage.user).email },
      });
    },
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
    },
  },
  middleware: "authentication",
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />

    <div class="row">
      <div class="col-12">
        <div class="card">
          <div class="card-body">
            <div class="row">
              <div class="col-md-4">
                <div class="mb-3">
                  <a
                    href="javascript:void(0);"
                    class="btn"
                    style="background-color: black; color: white"
                    v-b-modal.modal-Edit
                    ><i class="mdi mdi-plus me-2"></i> Add New</a
                  >
                </div>
              </div>
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
            </div>
            <div class="row mt-4">
              <div class="col-sm-12 col-md-6">
                <div id="tickets-table_length" class="dataTables_length">
                  <label class="d-inline-flex align-items-center">
                    Show&nbsp;
                    <b-form-select
                      class="m-2"
                      v-model="perPage"
                      size="sm"
                      :options="pageOptions"
                    ></b-form-select
                    >&nbsp; Entries
                  </label>
                </div>
              </div>
              <!-- Search -->
              <div class="col-sm-12 col-md-6">
                <div
                  id="tickets-table_filter"
                  class="dataTables_filter text-md-end"
                >
                  <label class="d-inline-flex align-items-center">
                    <b-form-input
                      v-model="filter"
                      type="search"
                      placeholder="Search..."
                      class="form-control rounded bg-light border-0 ms-2"
                    ></b-form-input>
                  </label>
                </div>
              </div>
              <!-- End search -->
            </div>
            <!-- Table -->
            <div class="table-responsive mb-0">
              <b-table
                class="table table-centered table-nowrap"
                :items="userList"
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

                <template v-slot:cell(name)="data">
                  <img
                    v-if="data.item.profile"
                    :src="data.item.profile"
                    alt
                    class="avatar-xs rounded-circle me-2"
                  />
                  <div
                    v-if="!data.item.profile"
                    class="avatar-xs d-inline-block me-2"
                  >
                    <div
                      class="
                        avatar-title
                        bg-soft-primary
                        rounded-circle
                        text-primary
                      "
                    >
                      <i class="mdi mdi-account-circle m-0"></i>
                    </div>
                  </div>
                  <a @click="gotoprofile()" class="text-body">{{
                    data.item.name
                  }}</a>
                </template>
                <template v-slot:cell(deletedstatus)="data">
                  <b-badge
                    variant="primary"
                    v-if="data.item.deletedstatus == 0"
                    class="text-success text-bold"
                    >Active
                    <div class="form-check form-switch">
                      <input
                        class="form-check-input"
                        type="checkbox"
                        id="flexSwitchCheckDefault"
                        v-model="data.item.deletedstatus"
                        @click="deleterec(data.item.id, data.item.email)"
                      />
                    </div>
                  </b-badge>
                  <b-badge
                    variant="danger"
                    v-else
                    class="text-secondary text-bold d-inline"
                    >Disabled
                    <div class="form-check form-switch">
                      <input
                        class="form-check-input"
                        type="checkbox"
                        id="flexSwitchCheckDefault"
                        v-model="data.item.deletedstatus"
                        @click="enablerec(data.item.id, data.item.email)"
                      />
                    </div>
                  </b-badge>
                </template>

                <template v-slot:cell(action)="data">
                  <ul class="list-inline mb-0">
                    <li class="list-inline-item">
                      <router-link
                        :to="{
                          name: 'update-profile',
                          params: {
                            email: data.item.email,
                            id: data.item.id,
                            editmode: true,
                          },
                        }"
                        class="px-2 text-primary"
                        v-b-tooltip.hover
                        title="Edit"
                      >
                        <i class="uil uil-pen font-size-18"></i>
                      </router-link>
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

                      <router-link
                        :to="{
                          name: 'changepassword',
                          params: { email: data.item.email },
                        }"
                        class="dropdown-item"
                        >Reset user password</router-link
                      >
                      <router-link
                        class="dropdown-item"
                        :to="{
                          name: 'update-profile',
                          params: { email: data.item.email },
                        }"
                        >Action</router-link
                      >
                    </b-dropdown>
                  </ul>
                </template>
              </b-table>
            </div>
            <div class="row">
              <div class="col">
                <div
                  class="dataTables_paginate paging_simple_numbers float-end"
                >
                  <ul class="pagination pagination-rounded mb-0">
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
    <b-modal
      id="modal-Edit"
      :title="modaltitle"
      :pass_char="pass_char"
      :pass_nums="pass_nums"
      :pass_min_length="pass_min_length"
      :pass_small_chars="pass_small_chars"
      :pass_upper_chars="pass_upper_chars"
      hide-footer
      size="xl"
      centered
    >
      <div>
        <!-- <PageHeader :title="title" :items="items" /> -->

        <div class="row mb-4">
          <div class="col-xl-4">
            <div class="card h-100">
              <div class="card-body">
                <div class="text-center">
                  <b-dropdown
                    class="float-end"
                    variant="white"
                    right
                    menu-class="dropdown-menu-end"
                    toggle-class="font-size-16 text-body p-0"
                  >
                    <template v-slot:button-content>
                      <i class="uil uil-ellipsis-v"></i>
                    </template>
                    <a class="dropdown-item" href="#">Edit</a>
                    <a class="dropdown-item" href="#">Deactivate</a>
                  </b-dropdown>
                  <div class="clearfix"></div>
                  <div>
                    <img
                      src="@/assets/images/users/user.jpg"
                      alt
                      class="avatar-lg rounded-circle img-thumbnail"
                    />
                  </div>
                  <h5 class="mt-3 mb-1">{{ names }}</h5>
                  <p class="text-muted">{{ role }}</p>
                </div>

                <div class="text-muted">
                  <div class="table-responsive mt-2 mb-0">
                    <div>
                      <p class="mb-1">Name :</p>
                      <h5 class="font-size-16">{{ names }}</h5>
                    </div>
                    <div class="mt-4">
                      <p class="mb-1">Mobile :</p>
                      <h5 class="font-sizeaxioos-16">{{ cell }}</h5>
                    </div>
                    <div class="mt-4">
                      <p class="mb-1">E-mail :</p>
                      <h5 class="font-size-16">{{ email }}</h5>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div class="col-xl-8">
            <div class="card mb-0">
              <b-tabs content-class="p-4" justified class="nav-tabs-custom">
                <b-tab active>
                  <template v-slot:title>
                    <i class="uil uil-user-circle font-size-20"></i>
                    <span class="d-none d-sm-block">About</span>
                  </template>
                  <div>
                    <div>
                      <h5 class="font-size-16 mb-4">{{ Edit }} User(s)</h5>
                      <div class="row">
                        <div class="col-lg-8">
                          <div class="mt-4">
                            <form @submit.prevent="addRec()">
                              <div class="row">
                                <div class="col-sm-12">
                                  <b-form-group
                                    class="mb-3"
                                    label="Email"
                                    label-for="Email-input"
                                  >
                                    <input
                                      v-model="email"
                                      type="email"
                                      name="email"
                                      class="form-control"
                                      placeholder="Enter a valid e-mail"
                                    />
                                  </b-form-group>
                                </div>

                                <div class="col-sm-12">
                                  <b-form-group
                                    class="mb-3"
                                    label="Full Name"
                                    label-for="names-input"
                                  >
                                    <input
                                      class="form-control"
                                      type="text"
                                      placeholder="Enter Full name "
                                      v-model="names"
                                    />
                                  </b-form-group>
                                </div>

                                <div class="col-sm-6">
                                  <b-form-group
                                    class="mb-3"
                                    label="Organization"
                                    label-for="Organization-input"
                                  >
                                    <multiselect
                                      v-model="organization"
                                      :options="Organization"
                                    ></multiselect>
                                  </b-form-group>
                                </div>
                                <div class="col-sm-6">
                                  <b-form-group
                                    class="mb-3"
                                    label="Role"
                                    label-for="role-input"
                                  >
                                    <multiselect
                                      v-model="role"
                                      :options="roles"
                                      :multiple="true"
                                    ></multiselect>
                                  </b-form-group>
                                </div>

                                <div class="col-sm-12">
                                  <b-form-group
                                    class="mb-3"
                                    label="Cell Phone"
                                    label-for="password-input"
                                  >
                                    <input
                                      class="form-control"
                                      type="text"
                                      placeholder="i.e. 254722420407"
                                      v-model="cell"
                                    />
                                  </b-form-group>
                                </div>

                                <div class="col-sm-6">
                                  <b-form-group
                                    class="mb-3"
                                    label="Password"
                                    label-for="password-input"
                                  >
                                    <input
                                      class="form-control"
                                      type="password"
                                      placeholder="Password"
                                      v-model="password"
                                    />
                                  </b-form-group>
                                </div>

                                <div class="col-sm-6">
                                  <b-form-group
                                    class="mb-3"
                                    label="Confirm Password"
                                    label-for="confirm-input"
                                  >
                                    <input
                                      class="form-control"
                                      type="password"
                                      placeholder="Confirm Password"
                                      v-model="cpassword"
                                    />
                                  </b-form-group>
                                </div>
                              </div>
                              <div class="mt-4 mb-4">
                                <b-button type="submit" variant="primary"
                                  >Submit</b-button
                                >
                              </div>
                            </form>
                          </div>
                        </div>
                        <div class="col-lg-4">
                          <div class="border-top">
                            <vue-dropzone
                              id="dropzone"
                              ref="myVueDropzone"
                              :use-custom-slot="true"
                              :options="dropzoneOptions"
                            >
                              <div class="dropzone-custom-content">
                                <i
                                  class="
                                    display-4
                                    text-muted
                                    bx
                                    bxs-cloud-upload
                                  "
                                ></i>
                                <h4>Drop files here or click to upload.</h4>
                              </div>
                            </vue-dropzone>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </b-tab>
                <b-tab>
                  <!-- <template v-slot:title>
                <i class="uil uil-clipboard-notes font-size-20"></i>
                <span class="d-none d-sm-block">Tasks</span>
              </template> -->
                  <div>
                    <h5 class="font-size-16 mb-3">Active Shifts</h5>

                    <div class="table-responsive">
                      <table class="table table-nowrap table-centered">
                        <tbody>
                          <tr>
                            <td style="width: 60px">
                              <div
                                class="
                                  custom-control custom-checkbox
                                  text-center
                                "
                              >
                                <input
                                  type="checkbox"
                                  class="custom-control-input"
                                  id="tasks-activeCheck2"
                                />
                                <label
                                  class="custom-control-label"
                                  for="tasks-activeCheck2"
                                ></label>
                              </div>
                            </td>
                            <td>
                              <a href="#" class="fw-bold text-dark"
                                >Shift One</a
                              >
                            </td>

                            <td>27 May, 2021</td>
                            <td style="width: 160px">
                              <span class="badge bg-soft-primary font-size-12"
                                >Active</span
                              >
                            </td>
                          </tr>
                          <tr>
                            <td>
                              <div
                                class="
                                  custom-control custom-checkbox
                                  text-center
                                "
                              >
                                <input
                                  type="checkbox"
                                  class="custom-control-input"
                                  id="tasks-activeCheck1"
                                />
                                <label
                                  class="custom-control-label"
                                  for="tasks-activeCheck1"
                                ></label>
                              </div>
                            </td>
                            <td>
                              <a href="#" class="fw-bold text-dark"
                                >Shift Two</a
                              >
                            </td>

                            <td>26 May, 2020</td>
                            <td>
                              <span class="badge bg-soft-primary font-size-12"
                                >Active</span
                              >
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>

                    <h5 class="font-size-16 my-3">Completed Shifts</h5>

                    <div class="table-responsive">
                      <table class="table table-nowrap table-centered">
                        <tbody>
                          <tr>
                            <td style="width: 60px">
                              <div
                                class="
                                  custom-control custom-checkbox
                                  text-center
                                "
                              >
                                <input
                                  type="checkbox"
                                  class="custom-control-input"
                                  id="tasks-completeCheck3"
                                />
                                <label
                                  class="custom-control-label"
                                  for="tasks-completeCheck3"
                                ></label>
                              </div>
                            </td>
                            <td>
                              <a href="#" class="fw-bold text-dark"
                                >Shift One</a
                              >
                            </td>

                            <td>27 May, 2020</td>
                            <td style="width: 160px">
                              <span class="badge bg-soft-success font-size-12"
                                >Complete</span
                              >
                            </td>
                          </tr>
                          <tr>
                            <td>
                              <div
                                class="
                                  custom-control custom-checkbox
                                  text-center
                                "
                              >
                                <input
                                  type="checkbox"
                                  class="custom-control-input"
                                  id="tasks-completeCheck2"
                                />
                                <label
                                  class="custom-control-label"
                                  for="tasks-completeCheck2"
                                ></label>
                              </div>
                            </td>
                            <td>
                              <a href="#" class="fw-bold text-dark"
                                >Shift Two</a
                              >
                            </td>
                            <td>27 May, 2020</td>
                            <td>
                              <span class="badge bg-soft-success font-size-12"
                                >Completed</span
                              >
                            </td>
                          </tr>
                          <tr>
                            <td>
                              <div
                                class="
                                  custom-control custom-checkbox
                                  text-center
                                "
                              >
                                <input
                                  type="checkbox"
                                  class="custom-control-input"
                                  id="tasks-completeCheck1"
                                />
                                <label
                                  class="custom-control-label"
                                  for="tasks-completeCheck1"
                                ></label>
                              </div>
                            </td>
                            <td>
                              <a href="#" class="fw-bold text-dark"
                                >shift Three</a
                              >
                            </td>

                            <td>26 May, 2021</td>
                            <td>
                              <span class="badge bg-soft-success font-size-12"
                                >Completed</span
                              >
                            </td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </div>
                </b-tab>
              </b-tabs>
              <!-- Nav tabs -->
              <!-- Tab content -->
            </div>
          </div>
        </div>
        <!-- end row -->
      </div>
    </b-modal>
    <b-modal id="modal-Print" title="Print PDF" hide-footer size="bg" centered>
      <reportdet
        :title="title"
        :orderData="userList"
        :pl="pl"
        :headers="headers"
        :uniqueCars="uniqueCars"
        :shome="showme"
        v-show="showme"
      ></reportdet>
    </b-modal>
  </Layout>
</template>
