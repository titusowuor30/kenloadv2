<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";
import axios from "axios"

import Multiselect from "vue-multiselect";

export default {
  page: {
    title: "Reporting",
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
  
    Multiselect,
  },
  data() {
    return {
      title: "Reporting",
      items: [
        {
          text: "USER: Management",
        },
        {
          text: "Reporting",
          active: true,
        },
      ],
   
      report: null,
      reports: [
        "Auto Generate Reports",
        " Axle Configurations",
        " Clerk Census",
        " Daily Hour Data",
        " Daily Hour Statistics",
        " Daily & Hourly Register",
        " Daily Summary",
       
      ],
    
     
    
    
    };
  },
     watch:{
      viewReports(newValue){
      var curentuser=JSON.parse(localStorage.user).email
      var currentdate=new Date();
    const data={
      datetime:currentdate,
      useremail:curentuser,
      application:window.navigator.userAgent,
      details:`Opened Reports page:${newValue}`,
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
    // Set the initial number of items
    this.totalRows = this.items.length;
  },
  methods: {
    handleSubmit() {
      console.log("Error on submit");
    },
    /**
     * Generate Wb register report
     */
    goToWbRegisterReport() {
      this.$router.push("/wbregisterreport");
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />

    <div class="row">
      <div class="card">
        <div class="card-body">
          <form @submit.prevent="handleSubmit">
            <div class="row">
              <div class="col-sm-12">
                <div class="col-sm-12">
                  <div class="col-md-12">
                    <div class="mb-3">
                      <label class="control-label">Report For: </label>
                      <multiselect
                        v-model="report"
                        :options="reports"
                        :allow-empty="false"
                        placeholder="Select Report"
                      ></multiselect>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
  </Layout>
</template>
<style scoped>
.changebg {
  background-color: #ffffff;
}
</style>