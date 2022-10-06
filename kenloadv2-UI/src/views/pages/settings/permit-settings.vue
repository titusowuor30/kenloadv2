<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import Swal from "sweetalert2";
import axios from "axios";

/**
 * Elements component
 */
export default {
  page: {
    title: "Permit Settings",
    meta: [
      {
        name: "description",
        content: appConfig.description,
      },
    ],
  },
  components: { Layout, PageHeader },
  data() {
    return {
      id: 1,
      permiturl: "",
      viewPermitSettings:false,
      editPermitSettings:false,
      title: "Permit Settings",
      items: [
        {
          text: "Permit",
          href: "/",
        },
        {
          text: "Permit Settings",
          active: true,
        },
      ],
      checked: true,
      smchecked: true,
      lgchecked: true,
      selectedToogle: "A",
      selectedDefault: "a",
      rightcheck: "accepted",
      status: "accepted",
      orderData: [],
    };
  },
  middleware: "authentication",
    watch:{
      viewPermitSettings(newValue){
      var curentuser=JSON.parse(localStorage.user).email
      var currentdate=new Date();
    const data={
      datetime:currentdate,
      useremail:curentuser,
      application:window.navigator.userAgent,
      details:`Opened permit settings:${newValue}`,
      computer:window.localStorage.clientip
    }
     axios
     .post(window.$http + "AuditLogs",data, { headers: window.$headers })
     .then(response=>{
       console.log(response.data)
     })
     .catch(e=>{
       console.log(e)
     })
    },
     editPermitSettings(newValue){
      var curentuser=JSON.parse(localStorage.user).email
      var currentdate=new Date();
    const data={
      datetime:currentdate,
      useremail:curentuser,
      application:window.navigator.userAgent,
      details:`Edited permit settings:${newValue},value:(${this.permiturl})`,
      computer:window.localStorage.clientip
    }
     axios
     .post(window.$http + "AuditLogs",data, { headers: window.$headers })
     .then(response=>{
       console.log(response.data)
     })
     .catch(e=>{
       console.log(e)
     })
    },
  },
  mounted() {
    //alert(window.$permiturl);
    // Show a pop up if it works
    // alert(location.host);

    // Set the initial number of items
    this.updatearray();
    this.viewPermitSettings=true;
  },
  methods: {
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
        .get(window.$http + `permiturl`, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          console.log(this.orderData);
          //alert(this.orderData[0].id);
          this.permiturl = this.orderData[0].permiturl;
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

    save() {
      //alert(this.alongroad);
      const data = {
        id: this.id,
        permiturl: this.permiturl,
      };
      console.log(data);
      //alert(window.$http);
      Swal.fire({
        title: "Kenload V2 . Please Wait !",
        html: "Saving Data!", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      axios
        .put(window.$http + `permiturl/` + this.id, data, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.

          console.log(response.data);
          this.editPermitSettings=true;
          window.$permiturl = this.permiturl;
          Swal.close();
          Swal.fire({
            position: "center",
            icon: "success",
            title: "Your work has been saved" + response.data,
            showConfirmButton: false,
            timer: 2000,
            allowOutsideClick: false,
          }).then(() => {
            this.clearvalues();
            this.updatearray();
          });
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "" + e,
            showConfirmButton: true,
          }).then(() => {
            Swal.close();
          });
        });
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <div class="row mt-4">
      <div class="col-12">
        <div class="card">
          <div class="card-body">
            <div class="row">
              <div class="col-12">
                <form class="form-horizontal" role="form">
                  <b-form-group
                    id="example-url"
                    label-cols-sm="2"
                    label-cols-lg="2"
                    label="PERMIT URL"
                    label-for="url"
                    class="mb-3"
                  >
                    <b-form-input
                      ref="load"
                      id="url"
                      value="https://kenloadv2.com"
                      type="url"
                      name="url"
                      v-model="permiturl"
                    ></b-form-input>
                  </b-form-group>
                </form>
                <div class="mt-4">
                  <button @click="save()" class="btn btn-primary w-md">
                    Save
                  </button>
                </div>
              </div>
            </div>
            <!-- end row -->
          </div>
        </div>
        <!-- end card -->
      </div>
      <!-- end col -->
    </div>
    <!-- end row -->
  </Layout>
</template>
