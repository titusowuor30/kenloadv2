<script>
import appConfig from "@/app.config";
import { required, email } from "vuelidate/lib/validators";
import Swal from "sweetalert2";
import axios from "axios";

export default {
  page: {
    title: "Forgot Password",
    meta: [
      {
        name: "description",
        content: appConfig.description,
      },
    ],
  },
  data() {
    return {
      email: "",
      submitted: false,
      error: null,
      title: "Recover Password",
    };
  },
  validations: {
    email: {
      required,
      email,
    },
  },
  methods: {
    // Try to register the user in with the email, fullname
    // and password they provided.
    tryToReset() {
      this.submitted = true;
      // stop here if form is invalid
      this.$v.$touch();

      if (this.$v.$invalid) {
        return;
      } else {
        if (process.env.VUE_APP_DEFAULT_AUTH === "firebase") {
          this.tryingToReset = true;
          // Reset the authError if it existed.
          this.error = null;
          return (
            this.$store
              .dispatch("auth/resetPassword", {
                email: this.email,
              })
              // eslint-disable-next-line no-unused-vars
              .then((token) => {
                this.tryingToReset = false;
                this.isResetError = false;
              })
              .catch((error) => {
                this.tryingToReset = false;
                this.error = error ? error : "";
                this.isResetError = true;
              })
          );
        } else {
          this.tryingToReset = true;
          axios
            .post(window.$http + "AuthManagement/ForgotPassword/" + this.email)
            .then((response) => {
              // JSON responses are automatically parsed.
              console.log(response.data);
              Swal.fire({
                position: "center",
                icon: "success",
                title:
                  "Please check your email" +
                  "<" +
                  this.email +
                  ">" +
                  " for a password reset link!",
                html:
                  "If we take long to respond, kindly contact your Duty Manager or system admin for manual pasword reset!",
                showConfirmButton: false,
                timer: 8000,
              });
            })
            .catch((e) => {
              Swal.fire("" + e);
            });
        }
      }
    },
  },
};
</script>

<template>
  <div>
    <div class="home-btn d-none d-sm-block">
      <router-link to="/" class="text-dark">
        <i class="mdi mdi-home-variant h2"></i>
      </router-link>
    </div>
    <div class="account-pages my-5 pt-sm-5">
      <div class="container">
        <div class="row justify-content-center">
          <div class="col-md-8 col-lg-6 col-xl-5">
            <div class="text-center">
              <router-link to="/" class="mb-5 d-block auth-logo">
                <img
                  src="@/assets/images/logo-light.png"
                  alt
                  height="30"
                  class="logo"
                />
              </router-link>
              <div class="card">
                <div class="card-body p-4" style="background-color: yellow">
                  <div class="text-center mt-2">
                    <h5 class="text-primary">Reset Password</h5>
                    <p class="text-muted">Reset Password with kenloadv2.</p>
                  </div>
                  <div class="p-2 mt-4">
                    <div
                      class="alert alert-success text-center mb-4"
                      role="alert"
                    >
                      Enter your Email and instructions will be sent to you!
                    </div>
                    <form @submit.prevent="tryToReset">
                      <div class="mb-3">
                        <label for="useremail">Email</label>
                        <input
                          type="email"
                          v-model="email"
                          class="form-control"
                          id="useremail"
                          placeholder="Enter email"
                          :class="{
                            'is-invalid': submitted && $v.email.$error,
                          }"
                          style="
                            color: black;
                            background: transparent;
                            border: solid 1px black;
                          "
                        />
                        <div
                          v-if="submitted && $v.email.$error"
                          class="invalid-feedback"
                        >
                          <span v-if="!$v.email.required"
                            >Email is required.</span
                          >
                          <span v-if="!$v.email.email"
                            >Please enter valid email.</span
                          >
                        </div>
                      </div>
                      <div class="row mb-0">
                        <div class="col-12 text-end">
                          <button class="btn btn-primary w-sm" type="submit">
                            Send Request
                          </button>
                        </div>
                      </div>
                      <div class="mt-4 text-center">
                        <p class="mb-0">
                          Remember It ?
                          <router-link
                            to="/login"
                            class="fw-medium text-primary"
                            >Sign In</router-link
                          >
                        </p>
                      </div>
                    </form>
                  </div>
                </div>
                <!-- end card-body -->
              </div>
              <!-- end card -->

              <div class="mt-5 text-center text-white">
                <p>© {{ new Date().getFullYear() }} kenloadv2.</p>
              </div>
            </div>
            <!-- end col -->
          </div>
        </div>
      </div>
    </div>
    <!-- end row -->
  </div>
</template>

<style lang="scss" module>
input:-webkit-autofill,
input:-webkit-autofill:hover,
input:-webkit-autofill:focus,
input:-webkit-autofill:active {
  transition: background-color 5000s;
  -webkit-text-fill-color: black !important;
}
</style>
