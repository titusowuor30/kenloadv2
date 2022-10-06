<script>
import vue2Dropzone from "vue2-dropzone";
import "vue2-dropzone/dist/vue2Dropzone.min.css";
import Multiselect from "vue-multiselect";
import Swal from "sweetalert2";
import axios from "axios";
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
var CryptoJS = require("crypto-js");

export default {
  props: {
    //pass policy fields
    pass_char: Number,
    pass_nums: Number,
    pass_min_length: Number,
    pass_small_chars: Number,
    pass_upper_chars: Number,
  },
  components: {
    Multiselect,
    vueDropzone: vue2Dropzone,
    PageHeader,
    Layout,
  },
  data() {
    return {
      editmode: false,
      email: "",
      names: "",
      organization: "",
      roles: "",
      cell: "",
      sendmail: false,
      sendsms: false,
      dropzoneOptions: {
        url: "/api/file",
        addRemoveLinks: true,
        thumbnailWidth: 150,
        maxFilesize: 0.5,
        headers: {
          "My-Awesome-Header": "header value",
        },
      },
      title: "Add Users",

      items: [
        {
          text: "USER: Admin",
        },
        {
          text: "Add Users",
          active: true,
        },
      ],
      role: ["COW"],
      organizations: ["KeNHA", "Danka", "Ebenezer","Gokhan-Masterspace","Interways"],
      password: "",
      cpassword: "",
      username: "",
    };
  },
  mounted() {
    this.fetchroles;
    this.fetchusers();
    console.log(this.pass_min_length);
  },
  methods: {
    fetchusers() {
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
          this.orderData = response.data;
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
    },
    fetchroles() {
      //screen and roles combination
      axios
        .get(window.http + `UserRoles`, {
          headers: {
            Authorization: `Bearer ${CryptoJS.AES.decrypt(
              JSON.parse(localStorage.user).token,
              "mnopqr"
            )
              .toString(CryptoJS.enc.Utf8)
              .trim()}`,
          },
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.role = response.data;

          console.log(response.data);
        })
        .catch((e) => {
          this.errors.push(e);
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
    handleSubmit() {
      console.log("Error on submit");
    },
    addRec() {
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
      if (this.username.trim() == "") {
        this.username = this.email;
      }
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
      if (this.password.trim().length() < this.pass_min_length) {
        Swal.fire(
          "Password must be at leat " + this.pass_min_length + " characters!"
        );
        return;
      }
      var orderid = this.orderData.length + 1;
      this.orderData.push({
        id: orderid,
        email: this.email,
        name: this.names,
        organization: this.organization,
        roles: this.roles,
        cell: this.cell,
        password: this.password,
        sendmail: this.sendmail,
        sendsms: this.sendsms,
      });
      var timenow = this.gettime();
      axios
        .post(
          window.$http + "Users",
          {
            id: 0,
            username: this.username,
            fullnames: this.names,
            userpass: {
              id: 0,
              userpassword: this.password,
              expirationstatus: false,
            },
            organization: this.organization,
            description: "string",
            deletedstatus: 0,
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
          this.role = response.data;
          console.log(response.data);
        })
        .catch((e) => {
          this.errors.push(e);
        });

      Swal.fire({
        position: "center",
        icon: "success",
        title: "Your work has been saved. Role " + this.email + " Added",
        showConfirmButton: false,
        timer: 1500,
      }).then((result) => {
        result;
        this.clearvalues();
      });
    },
    editRec() {
      alert("");
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
      if (this.username.trim() == "") {
        this.username = this.email;
      }
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
        roles: this.roles,
        cell: this.cell,
        password: this.password,
        sendmail: this.sendmail,
        sendsms: this.sendsms,
      });
      var timenow = this.gettime();
      axios
        .put(
          window.$http + "Users" + orderid,
          {
            id: 0,
            username: this.username,
            fullnames: this.names,
            userpass: {
              id: 0,
              userpassword: this.password,
              expirationstatus: false,
            },
            description: "string",
            deletedstatus: "",
            organization: this.organization,
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
          this.role = response.data;
          console.log(response.data);
        })
        .catch((e) => {
          this.errors.push(e);
        });

      Swal.fire({
        position: "center",
        icon: "success",
        title: "Your work has been saved. Role " + this.email + " Added",
        showConfirmButton: false,
        timer: 1500,
      }).then((result) => {
        result;
        this.clearvalues();
      });
    },
    edit() {
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
      if (this.cell.trim() == "") {
        Swal.fire("Please enter Cell Phone number!");
        return;
      }
      if (this.password.trim() != "") {
        if (this.cpassword.trim() == "") {
          Swal.fire("Please enter confirm password!");
          return;
        }
        if (this.cpassword.trim() != this.password.trim()) {
          Swal.fire("Confirm password and password does not match!");
          return;
        }
        this.orderData[this.myindex].password = this.password;
      }

      alert(this.myindex);
      this.orderData[this.myindex].id = this.id;
      this.orderData[this.myindex].email = this.email;
      this.orderData[this.myindex].names = this.names;
      this.orderData[this.myindex].organization = this.organization;
      this.orderData[this.myindex].roles = this.roles;
      this.orderData[this.myindex].cell = this.cell;
      this.orderData[this.myindex].sendmail = this.sendmail;
      this.orderData[this.myindex].sendsms = this.sendsms;

      Swal.fire({
        position: "center",
        icon: "success",
        title: "Your work has been saved",
        showConfirmButton: false,
        timer: 1500,
      });
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />

    <div class="row">
      <div class="col-sm-2"></div>
      <div class="col-sm-8">
        <div class="card">
          <div class="card-body">
            <form @submit.prevent="addRec()">
              <div class="row">
                <div class="col-sm-6">
                  <div class="row">
                    <div class="input-group">
                      <div class="input-group-text col-sm-4">Email:</div>

                      <input
                        class="form-control"
                        type="text"
                        placeholder="Enter Email user@user.co.ke"
                        v-model="email"
                      />
                    </div>
                    <div class="col-sm-12">
                      <div class="input-group">
                        <div class="input-group-text col-sm-4">Names:</div>

                        <input
                          class="form-control"
                          type="text"
                          placeholder="Enter Full Name "
                          v-model="names"
                        />
                      </div>
                    </div>
                    <div class="col-sm-12">
                      <div class="input-group">
                        <div class="input-group-text col-sm-4">Username:</div>

                        <input
                          class="form-control"
                          type="text"
                          placeholder="Enter Username "
                          v-model="username"
                        />
                      </div>
                    </div>
                    <div class="col-sm-12">
                      <div class="input-group">
                        <div class="input-group-text col-sm-4">
                          Organization:
                        </div>
                        <multiselect
                          v-model="organization"
                          :options="organizations"
                          class="form-control"
                        ></multiselect>
                      </div>
                    </div>

                    <div class="col-sm-12">
                      <div class="input-group">
                        <div class="input-group-text col-sm-4">Role:</div>
                        <div class="col-sm-8">
                          <multiselect
                            v-model="roles"
                            :options="role"
                            :multiple="true"
                          ></multiselect>
                        </div>
                      </div>
                    </div>

                    <div class="col-sm-12">
                      <div class="input-group">
                        <div class="input-group-text col-sm-4">Cell Phone:</div>

                        <input
                          class="form-control"
                          type="text"
                          placeholder="i.e. 254722420407"
                          v-model="cell"
                        />
                      </div>
                    </div>

                    <div class="col-sm-12">
                      <div class="input-group">
                        <div class="input-group-text col-sm-4">Password:</div>

                        <input
                          class="form-control"
                          type="password"
                          placeholder="Password"
                          v-model="password"
                        />
                      </div>
                    </div>

                    <div class="col-sm-12">
                      <div class="input-group">
                        <div class="input-group-text col-sm-4">
                          Confirm Password:
                        </div>

                        <input
                          class="form-control"
                          type="password"
                          placeholder="Confirm Password"
                          v-model="cpassword"
                        />
                      </div>
                    </div>

                    <div class="col-sm-6"></div>

                    <div class="col-sm-6">
                      <label> Send Email:</label>
                      <input
                        type="checkbox"
                        value="email"
                        v-model="sendemail"
                      />

                      <label> Send SMS:</label>
                      <input type="checkbox" value="sms" v-model="sendsms" />
                    </div>
                  </div>
                </div>
                <div class="col-sm-6">
                  <!-- column 3 - upload image -->

                  <h5 class="font-size-14 mb-4">
                    <i class="mdi mdi-arrow-right text-primary me-1"></i>
                    Upload
                  </h5>

                  <!-- file upload -->
                  <vue-dropzone
                    id="dropzone"
                    ref="myVueDropzone"
                    :use-custom-slot="true"
                    :options="dropzoneOptions"
                  >
                    <div class="dropzone-custom-content">
                      <i class="display-5 text-muted bx bxs-cloud-upload"></i>
                      <h4>Drop image here or click to upload.</h4>
                    </div>
                  </vue-dropzone>
                  <div class="text-center mt-2">
                    <b-button size="sm" variant="danger">Delete Image</b-button>
                  </div>

                  <!-- col-3 end upload image -->
                </div>
              </div>
            </form>
          </div>
        </div>
        <div class="row">
          <div class="col-sm-10"></div>
          <div class="col-sm-2">
            <button class="btn btn-primary" type="submit">Add User</button>
          </div>
        </div>
      </div>
    </div>
  </Layout>
</template>
