<script>
//import Layout from "../../layouts/main";
//import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";
import vue2Dropzone from "vue2-dropzone";
import "vue2-dropzone/dist/vue2Dropzone.min.css";
import Swal from "sweetalert2";
import axios from "axios";

import Multiselect from "vue-multiselect";

/**axioos
 * Profile component
 */
export default {
  components: { Multiselect, vueDropzone: vue2Dropzone },
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
      shift_type: "Morning Shift",
      selected: false,
      from: null,
      to: null,
      adduserlog: false,
      roles: [],
      Organization: ["KeNHA", "Gokhan-Masterspace", "Ebenezer", "Danka","Interways"],
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
  },
  mounted() {
    // Set the initial number of items
    this.getPassWordPolicy();
    this.updatearray();
    this.getUser();
    this.focusInput();
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
      var orderid = this.orderData.length + 1;
      this.orderData.push({
        id: orderid,
        email: this.email,
        name: this.names,
        organization: this.organization,
        role: this.roles,
        cell: this.cell,
        password: this.password,
        sendmail: this.sendmail,
        sendsms: this.sendsms,
      });
      var timenow = this.gettime();
      alert();
      axios
        .post(
          window.$http + "AuthManagement/",
          {
            username: this.email,
            email: this.email,
            password: this.password,
          },
          { headers: window.$headers }
        )
        .then((res) => {
          alert(res);
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
              this.errors.push(e+"=>or user already exists!");
            });
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Error:" +e+"=> or User account already exists! Please check the details and try again!",
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
    editRec() {
      if (this.email.trim() == "") {
        Swal.fire("Please enter Email Address!");
        return;
      }
      if (this.names.trim() == "") {
        Swal.fire("Please enter names!");
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
      var orderid = this.orderData.length + 1;
      this.orderData.push({
        id: orderid,
        email: this.email,
        name: this.names,
        organization: this.organization,
        role: this.roles,
        cell: this.cell,
        password: this.password,
        sendmail: this.sendmail,
        sendsms: this.sendsms,
      });
      var timenow = this.gettime();
      axios
        .put(
          window.$http + "Users/" + this.$route.params.id,
          {
            id: this.$route.params.id,
            username: this.username,
            fullnames: this.names,
            organization: this.organization,
            userRole: this.role,
            description: "string",
            deletedstatus: 0,
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
                          <a href="#" class="fw-bold text-dark">shift Three</a>
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
</template>
