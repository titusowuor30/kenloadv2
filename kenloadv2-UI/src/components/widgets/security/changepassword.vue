<script>
import axios from "axios";
import Swal from "sweetalert2";

export default {
  components: {},
  data() {
    return {
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

      email: "",
      role: [{ name: "Administrator" }, { name: "Prosecuter" }],
      userName: "",
      Organization: "KeNHA",
      send: [],
    };
  },
  method: {
    //pass policy
    getPassWordPolicy() {
      axios
        .get(window.$http + "AccessPassPolicy", { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          console.log(response.data);
          this.pass_char = response.data.app_char;
          this.pass_min_length = response.data.app_min_char;
          this.pass_nums = response.data.app_numbers;
          this.pass_upper_chars = response.data.app_capital;
          this.pass_small_chars = response.data.app_small;
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
    handleSubmit() {
      console.log("Error on submit");
    },
  },
};
</script>

<template>
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
                <div class="input-group-text col-sm-4">Enter New Password:</div>

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
            <div class="row mt-2">
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
</template>
