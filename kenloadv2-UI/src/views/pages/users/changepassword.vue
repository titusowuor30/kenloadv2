<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";
import axios from "axios";
import Swal from "sweetalert2";

export default {
  page: {
    title: "Change Password",
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
  },
  data() {
    return {
      title: "Reset password for " + this.$route.params.email,
      //initialize pass policy fields
      pass_char: 1,
      pass_nums: 1,
      pass_min_length: 8,
      pass_small_chars: 1,
      pass_upper_chars: 1,

      //reset pass
      email: this.$route.params.email,
      userrole_name: "",
      userroles: [],
      password: "",
      cpassword: "",

      items: [
        {
          text: "USER: Admin",
        },
        {
          text: "Change Password",
          active: true,
        },
      ],
      role: [{ name: "Administrator" }, { name: "Prosecuter" }],
      userName: "",
      Organization: "KeNHA",
      send: [],
    };
  },
  mounted() {
    this.getPassWordPolicy();
    this.getUser();
    console.log(this.userrole_name);
    //var authorizedRole = JSON.parse(localStorage.user);
  },
  methods: {
    //check user access level
    getUser() {
      axios
        .get(window.$http + "Users/byMail/" + this.$route.params.email, {
          headers: window.$headers,
        })
        .then((response) => {
          console.log(response.data);
          this.userrole_name = response.data.userRole;
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
          console.log(response.data);
          // JSON responses are automatically parsed.
          console.log(response.data[0].app_min_char);
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
    resetPass() {
      //var authorizedRole = JSON.parse(localStorage.user).role;
      //alert(authorizedRole);

      if (this.password.trim("") != this.cpassword.trim("")) {
        Swal.fire("Passwords don't match!");
        return;
      }
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
      var data = {
        email: this.email,
        newPassword: this.password,
      };
      axios
        .post(window.$http + "AuthManagement/AdminUserForgotPassword", data, {
          headers: window.$headers,
        })
        .then((response) => {
          console.log(response.data);
          Swal.fire({
            position: "center",
            icon: "success",
            title: "Password for " + this.email + " Changed successfully!",
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
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <div>
      <div class="row">
        <div class="card">
          <div class="card-body">
            <form @submit.prevent="resetPass()">
              <div class="row">
                <div class="input-group mb-3">
                  <div class="input-group-text col-sm-4" v-show="false">
                    Enter User Email:
                  </div>

                  <input
                    class="form-control"
                    hidden
                    type="email"
                    placeholder="Enter Registered user email"
                    v-model="email"
                  />
                </div>

                <div class="input-group mb-3">
                  <div class="input-group-text col-sm-4">
                    Enter New Password:
                  </div>
                  <input
                    class="form-control"
                    type="password"
                    placeholder="New  Password"
                    v-model="password"
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
              <div class="row mt-2">
                <div class="col-sm-9"></div>
                <div class="col-sm-3">
                  <b-button type="submit" variant="dark"
                    >Reset Password</b-button
                  >
                </div>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </Layout>
</template>
