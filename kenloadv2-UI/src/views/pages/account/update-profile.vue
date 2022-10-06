<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";
import vue2Dropzone from "vue2-dropzone";
import "vue2-dropzone/dist/vue2Dropzone.min.css";
import Swal from "sweetalert2";
import axios from "axios";

import Multiselect from "vue-multiselect";
import { FullCalendar } from "vue-full-calendar";
import "../../../../node_modules/vue-simple-calendar/dist/style.css";
// The next two lines are optional themes
import "../../../../node_modules/vue-simple-calendar/static/css/default.css";
import "../../../../node_modules/vue-simple-calendar/static/css/holidays-us.css";

/**axioos
 * Profile component
 */
export default {
  components: {
    Multiselect,
    vueDropzone: vue2Dropzone,
    PageHeader,
    Layout,
    FullCalendar,
  },
  props: {},
  page: {
    title: "Profile",
    meta: [
      {
        name: "description",
        content: appConfig.description,
      },
    ],
  },
  data() {
    return {
      tokenString: JSON.parse(localStorage.getItem("user")).token,
      typesubmit: false,
      title: "Profile",
      modaltitle: "Change Password",
      bgcolor: ["green", "black", "blue", "red"],
      roles: [],
      //active shifts
      allshifts: [],
      shiftSchedules: [],
      active_shift: "Unassigned Shift",
      active_shift1: "Unassigned Shift",
      active_shift2: "Unassigned Shift",
      from: null,
      to: null,
      leave_from: null,
      leave_to: null,
      off_from: null,
      off_to: null,
      //pass policy fields
      pass_char: 1,
      pass_nums: 1,
      pass_min_length: 8,
      pass_small_chars: 1,
      pass_upper_chars: 1,

      //change pass
      c_email: "",
      oldpassword: "",
      npassword: "",
      cpassword: "",

      config: {
        defaultView: "month",
        weekends: true,
        // 	eventRender: function(event, element) {
        // console.log(event)
        // }
      },

      Organization: ["KeNHA", "Gokhan-Masterspace", "Ebenezer", "Danka","Interways"],
      dropzoneOptions: {
        url: "https://httpbin.org/post",
        thumbnailWidth: 150,
        maxFilesize: 0.5,
        headers: {
          "My-Awesome-Header": "header value",
        },
      },
      editmode: this.$route.params.editmode,
      editlog: false,
      changedValues: [],
      originalValues: [],
      email: this.$route.params.email,
      names: "",
      organization: "",
      role: "",
      cell: "",
      sendsms: false,
      sendemail: false,
      items: [
        {
          text: "Profile",
        },
        {
          text: "Profile",
          active: true,
        },
      ],
    };
  },
  watch: {
    editlog(newValue) {
      var id = 1;
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      console.log();
      console.log(
        `id:${id} date:${currentdate} user:${curentuser} browser:${window.navigator.userAgent} edit user:${this.names} ${newValue} comp:${window.localStorage.clientip}`
      );
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Edit User:${newValue},\nNew Values:(name:${
          this.names
        },roles[${this.roles.toString()}],phone:${this.cell},organization:${
          this.organization
        })`,
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
  created() {
    this.fieldsChanged();
  },
  mounted() {
    this.getPassWordPolicy();
    // Set the initial number of items
    this.updatearray();
    this.getUser();
    this.getUserShift();
  },
  computed: {},
  methods: {
    //active shifts
    getUserShift() {
      axios
        .get(window.$http + "Shifts", {
          headers: window.$headers,
        })
        .then((response) => {
          this.allshifts = response.data;
          this.allshifts = this.allshifts.filter((item) => {
            return item.email === this.$route.params.email;
          });
          this.shiftSchedules.length == this.allshifts.length;
          console.log("data=>");
          console.log(this.allshifts);
          for (var i = 0; i < this.allshifts.length; i++) {
            if (this.allshifts[i].shift_type == "Morning Shift") {
              this.shiftSchedules.push({
                title:
                  this.allshifts[i].email + "," + this.allshifts[i].shift_type,
                start: this.allshifts[i].from,
                end: this.allshifts[i].to,
                allDay: this.allshifts[i].repeat,
                backgroundColor: this.bgcolor[0],
              });
            } else if (this.allshifts[i].shift_type == "Night Shift") {
              this.shiftSchedules.push({
                title:
                  this.allshifts[i].email + "," + this.allshifts[i].shift_type,
                start: this.allshifts[i].from,
                end: this.allshifts[i].to,
                allDay: this.allshifts[i].repeat,
                backgroundColor: this.bgcolor[1],
              });
            } else if (this.allshifts[i].shift_type == "Leave") {
              this.shiftSchedules.push({
                title:
                  this.allshifts[i].email + "," + this.allshifts[i].shift_type,
                start: this.allshifts[i].from,
                end: this.allshifts[i].to,
                allDay: this.allshifts[i].repeat,
                backgroundColor: this.bgcolor[2],
              });
            } else {
              this.shiftSchedules.push({
                title:
                  this.allshifts[i].email + "," + this.allshifts[i].shift_type,
                start: this.allshifts[i].from,
                end: this.allshifts[i].to,
                allDay: this.allshifts[i].repeat,
                backgroundColor: this.bgcolor[3],
              });
            }
          }
          this.active_shift = this.allshifts[0].shift_type;
          this.from = this.allshifts[0].from;
          this.to = this.allshifts[0].to;
          this.active_shift1 = this.allshifts[1].shift_type;
          this.leave_from = this.allshifts[1].from;
          this.leave_to = this.allshifts[1].to;
          this.active_shift2 = this.allshifts[2].shift_type;
          this.off_from = this.allshifts[2].from;
          this.off_to = this.allshifts[2].to;
        })
        .catch((e) => {
          this.errors.push(e);
          console.log(e);
        });
    },
    //pass policy
    getPassWordPolicy() {
      axios
        .get(window.$http + "AccessPassPolicy", { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          console.log(response.data[0]);
          this.pass_char = response.data[0].app_char;
          this.pass_min_length = response[0].data.app_min_char;
          this.pass_nums = response.data[0].app_numbers;
          this.pass_upper_chars = response.data[0].app_capital;
          this.pass_small_chars = response.data[0].app_small;
        })
        .catch((e) => {
          this.errors.push(e);
          console.log(e);
        });
    },
    changePass() {
      if (this.npassword.length < this.pass_min_length) {
        Swal.fire(
          "Password must be at least " +
            this.pass_min_length +
            " characters long!"
        );
        return;
      }
      if (this.pass_small_chars == 1) {
        if (this.npassword.search(/[a-z]/) < 0) {
          Swal.fire(
            "Password must contain at least one lowercase character e.g a-z!"
          );
          return;
        }
      }
      if (this.pass_upper_chars == 1) {
        if (this.npassword.search(/[A-Z]/) < 0) {
          Swal.fire(
            "Password must contain at least one Uppercase character e.g A-z!"
          );
          return;
        }
      }
      if (this.pass_nums == 1) {
        if (this.npassword.search(/[0-9]/) < 0) {
          Swal.fire(
            "Password must contain at least one Numeric character e.g 0-9!"
          );
          return;
        }
      }
      if (this.pass_char == 1) {
        if (this.npassword.search(/[!@#$%^&*]/) < 0) {
          Swal.fire(
            "Password must contain at least one Special character e.g !@#$%^&*!"
          );
          return;
        }
      }
      var data = {
        email: JSON.parse(localStorage.user).email,
        currentPassword: this.oldpassword,
        newPassword: this.npassword,
        confirmPassword: this.cpassword,
      };
      axios
        .post(window.$http + "AuthManagement/ChangePassword", data, {
          headers: window.$headers,
        })
        .then((response) => {
          console.log(response.data);
          Swal.fire({
            position: "center",
            icon: "success",
            title: "Password Changed successfully!",
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });
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
    fieldsChanged() {
      var cFields = document.getElementsByTagName("INPUT");
      for (var i = 0; i < cFields.length; i++) {
        cFields[i].on("change", () => {
          console.log("field changed to " + cFields[i].value);
        });
      }
    },
    getUser() {
      axios
        .get(window.$http + "Users/byMail/" + this.$route.params.email, {
          headers: window.$headers,
        })
        .then((response) => {
          this.email = this.$route.params.email;
          this.names = response.data.fullnames;
          this.organization = response.data.organization;
          this.role = response.data.userRole
            .toString()
            .split(",")
            .map((s) => s.trim());
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
        .get(window.$http + `userroles`, { headers: window.$headers }) //https://localhost:44365/userroles
        .then((response) => {
          // JSON responses are automatically parsed.
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
    editRec() {
      var timenow = this.gettime();
      axios
        .put(
          window.$http + "Users/" + this.$route.params.id,
          {
            id: this.$route.params.id,
            username: this.username,
            fullnames: this.names,
            organization: this.organization,
            userRole: this.role.toString(),
            deletedstatus: false,
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
          this.role = response.data;
          console.log(response.data);
          this.editlog = true;
        })
        .catch((e) => {
          this.errors.push(e);
        });

      Swal.fire({
        position: "center",
        icon: "success",
        title: "Your work has been saved",
        showConfirmButton: false,
        timer: 1500,
      }).then((result) => {
        result;
        this.clearvalues();
      });
    },
  },
};
</script>

<template>
  <div>
    <Layout>
      <PageHeader :title="title" :items="items" />

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
                <div class="text-muted  ml-5">
                  <div class="table-responsive mt-2 mb-0">
                    <div class="container">
                      <button class="btn btn-primary" v-b-modal.modal-1>
                        View Active Shifts
                      </button>
                    </div>
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
                    <h5 class="font-size-16 mb-4">Update Profile</h5>
                    <div class="row">
                      <div class="col-lg-8">
                        <div class="mt-4">
                          <form @submit.prevent="editRec()">
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
                                    @change="fieldsChanged()"
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
                            </div>
                            <div class="row">
                              <div class="col-sm-0 col-md-3">
                                <b-button type="submit" variant="primary"
                                  >Update</b-button
                                >
                              </div>
                              <div class="col-sm-0 col-md-6">
                                <b-button
                                  variant="primary"
                                  v-b-modal.modal-changepass
                                  >Change Password</b-button
                                >
                              </div>
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
                                class="display-4 text-muted bx bxs-cloud-upload"
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
                              class="custom-control custom-checkbox text-center"
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
                            <a href="#" class="fw-bold text-dark">Shift One</a>
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
                              class="custom-control custom-checkbox text-center"
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
                            <a href="#" class="fw-bold text-dark">Shift Two</a>
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
                              class="custom-control custom-checkbox text-center"
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
                            <a href="#" class="fw-bold text-dark">Shift One</a>
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
                              class="custom-control custom-checkbox text-center"
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
                            <a href="#" class="fw-bold text-dark">Shift Two</a>
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
                              class="custom-control custom-checkbox text-center"
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
      <b-modal
        id="modal-changepass"
        :title="modaltitle"
        hide-footer
        size="lg"
        centered
      >
        <div>
          <div class="row">
            <div class="card">
              <div class="card-body">
                <form @submit.prevent="changePass()">
                  <div class="row">
                    <div class="input-group mb-3">
                      <div class="input-group-text col-sm-4">
                        Enter Current Password:
                      </div>

                      <input
                        class="form-control"
                        type="password"
                        placeholder="Current Password"
                        v-model="oldpassword"
                      />
                    </div>

                    <div class="input-group mb-3">
                      <div class="input-group-text col-sm-4">
                        Enter New Password:
                      </div>

                      <input
                        class="form-control"
                        type="password"
                        placeholder="New Password"
                        v-model="npassword"
                      />
                    </div>

                    <div class="input-group">
                      <div class="input-group-text col-sm-4">
                        Confirm New Password:
                      </div>

                      <input
                        class="form-control"
                        type="password"
                        placeholder="Confirm New  Password"
                        v-model="cpassword"
                      />
                    </div>
                  </div>
                  <div class="row">
                    <div class="col-sm-9"></div>
                    <div class="col-sm-3">
                      <b-button type="submit" variant="dark"
                        >Change Password</b-button
                      >
                    </div>
                  </div>
                </form>
              </div>
            </div>
          </div>
        </div>
      </b-modal>
      <b-modal
        id="modal-1"
        title="My Active Shift Schedules"
        hide-footer
        scrollable
        size="xl"
        centered
      >
        <div class="card-body">
          <div class="row fc-toolbar-ltr m-2 d-flex bg-light p-2">
            <div class="col d-flex">
              <span class="mr-2">Morning Shift </span>
              <i class="fas fa-circle text-success m-auto"></i>
            </div>
            <div class="col d-flex">
              <span class="mr-1">Night Shift</span>
              <i class="fas fa-circle text-primary m-auto"></i>
            </div>
            <div class="col d-flex">
              <span class="mr-1">On Leave</span>
              <i class="fas fa-circle text-info m-auto"></i>
            </div>
            <div class="col d-flex">
              <span class="mr-1">OFF Duty</span>
              <i class="fas fa-circle text-danger m-auto"></i>
            </div>
          </div>
          <full-calendar
            :events="shiftSchedules"
            :config="config"
          ></full-calendar>
        </div>
      </b-modal>
    </Layout>
  </div>
</template>
