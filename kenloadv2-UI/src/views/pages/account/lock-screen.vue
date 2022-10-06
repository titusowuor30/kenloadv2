<script>
import appConfig from "@/app.config";
import { required, email } from "vuelidate/lib/validators";

/**
 * Lock-screen component
 */
export default {
  page: {
    title: "Lock screen",
    meta: [
      {
        name: "description",
        content: appConfig.description,
      },
    ],
  },

  mounted() {
    document.body.classList.add("authentication-bg");
  },
  created(){
    this.unsetUserDetails();
  },
  methods:{
    unsetUserDetails(){
      localStorage.removeItem('user');
    },
     tryToLogIn() {
      
      this.submitted = true;
      // stop here if form is invalid
     

     if (process.env.VUE_APP_DEFAULT_AUTH === "kenloadv2auth") {
        const { email, password } = this;
          if (email && password) {
            this.tryingToLogIn = true;
            this.$store.dispatch("authfack/login", {
              email,
              password,
            });
         
        }
      }
    }

  },

  data() {
    return {
      title: "Lock screen",
      showemail:false,
      currentUsername:
        JSON.parse(localStorage.getItem("user"))
          .username.charAt(0)
          .toUpperCase() +
        JSON.parse(localStorage.getItem("user"))
          .username.slice(1)
          .split("@")[0],
      userEmail:JSON.parse(localStorage.getItem("user"))
          .username,
      email: JSON.parse(localStorage.getItem("user"))
          .username,
      password: "",
      tryingToLogIn: false,
      submitted:false,
      validations: {
    email: {
      required,
      email,
    },
    password: {
      required,
    },
  },
    };
  },
};
</script>

<template>
  <div>
    
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
                    <h5 class="text-primary">Lock screen</h5>
                    <p>Enter your password to unlock the screen!</p>
                  </div>
                  <div class="p-2 mt-4">
                    <div class="user-thumb text-center mb-4">
                      <img
                        src="@/assets/images/users/user.jpg"
                        class="rounded-circle img-thumbnail avatar-lg" 
                        alt="thumbnail"
                      />
                      <h5 class="font-size-15 mt-3">{{currentUsername}}</h5>
                    </div>
                    <form @submit.prevent="tryToLogIn">
                    <b-form-group v-show="showemail"
                      id="input-group-1"
                      class="mb-3"
                      label="Email"
                      label-for="input-1"
                    >
                      <b-form-input
                        id="input-1"
                        v-model="email"
                        type="text"
                        style="
                          color: black;
                          background: transparent;
                          border: solid 1px black;
                        "
                      ></b-form-input>
                      
                    </b-form-group>
                    <b-form-group id="input-group-2" class="mb-3">
                      <label for="input-2">Password</label>
                      <b-form-input
                        id="input-2"
                        v-model="password"
                        type="password"
                        placeholder="Enter password"
                        style="
                          color: black;
                          background: transparent;
                          border: solid 1px black;
                        "
                      ></b-form-input>
                      <div
                        v-if="submitted && !$v.password.required"
                        class="invalid-feedback"
                      >
                        Password is required.
                      </div>
                    </b-form-group>

                      <div class="mt-3 text-end">
                        
                          <b-button type="submit" variant="primary" class="w-sm"
                        >
                       
                          Unlock
                        </b-button>
                      </div>

                      <div class="mt-4 text-center">
                        <p class="mb-0">
                          Not you ? return
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
              </div>
              <div class="mt-5 text-center text-white">
                <p>© {{ new Date().getFullYear() }} kenloadv2</p>
              </div>
            </div>
          </div>
        </div>
        <!-- end row -->
      </div>
      <!-- end container -->
    </div>
  </div>
</template>