<template>
  <div>
    <div class="row">
      <div class="col-md-12">
        <div class="card">
          <div class="card-body">
            <form @submit.prevent="handleSubmit">
              <div class="row">
                <div class="col-sm-12">
                  <div class="input-group">
                    <div class="input-group-text col-sm-3">User:</div>
                    <multiselect
                      v-model="Useremail"
                      :options="mails"
                      class="form-control"
                    ></multiselect>
                  </div>
                </div>
                <div class="input-group">
                  <div class="input-group-text col-sm-3">Password:</div>

                  <input
                    class="form-control"
                    type="password"
                    placeholder="password"
                    v-model="password"
                  />
                </div>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
    <!----@click="position(), $bvModal.hide('modal-1') --->
    <div class="row">
      <div class="col-sm-9 mb-2"></div>
      <div class="col-sm-3 mb-2">
        <b-button variant="dark" @click="access()">Grant Access </b-button>
      </div>
    </div>
  </div>
</template>

<script>
import Swal from "sweetalert2";
import axios from "axios";

import Multiselect from "vue-multiselect";

export default {
  name: "accesscontrol",
  mounted() {
    this.updatearray();
  },
  methods: {
    handleSubmit() {
      console.log("Error on submit");
    },
    access() {
      //alert("bind");
      if (this.Useremail.trim() == "") {
        Swal.fire("Please enter email address!");
        return;
      }
      if (this.password.trim() == "") {
        Swal.fire("Please enter  password!");
        return;
      }
      axios
        .get(
          window.$http +
            `accesscontrolpasswords/grant?email=` +
            this.Useremail.trim() +
            "&pass=" +
            this.password.trim(),
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          console.log(response.data);
          //alert(response.data);
          if (response.data) {
            this.changegranted();
            //alert();
            Swal.fire("Access Granted!").then((result) => {
              result.value;
              if (this.link != "") {
                this.$router.push({
                  name: this.link,
                  params: { nplate: this.nplate, tag: true, cat: this.cat },
                });
              }
            });
          } else {
            Swal.fire({
              position: "center",
              icon: "error",
              title: "Invalid Username or password",
              showConfirmButton: true,
            }).then(() => {
              //this.updatearray();
            });
          }
        })
        .catch((e) => {
          Swal.fire("" + e);
        });

      //alert(this.link);
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
      //alert(window.$http + `accesscontrolpasswords/search?screenid=21`);
      axios
        .get(window.$http + `accesscontrolpasswords/search?screenid=21`, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          console.log(response.data);
          response.data.forEach((e) => {
            this.mails.push(e.username);
          });
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
    changegranted() {
      this.$emit("changegranted", "true", this.Useremail.trim());
      // this.$emit("changegranted", "true");
    },
  },
  props: {
    link: String,
    nplate: String,
    cat: String,
  },
  components: {
    Multiselect,
  },
  data() {
    return {
      mails: [],

      Useremail: "",
      password: "",
    };
  },
};
</script>

<style>
</style>