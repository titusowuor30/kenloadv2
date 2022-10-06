<script>
import Layout from "../../layouts/main";
//import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";
import tagsdetails from "@/components/widgets/weigh/tagsdetails";
import axios from "axios"
//import Deckweights from "@/components/widgets/weigh/deckweights";
//import Vehicledetailsform from "@/components/widgets/weigh/vehicledetailsform";

export default {
  page: {
    title: "Tags",
    meta: [
      {
        name: "description",
        content: appConfig.description,
      },
    ],
  },

  components: {
    Layout,
    tagsdetails,
    //Deckweights,
    //Vehicledetailsform,
  },
  created() {
    this.nplate = this.$route.params.nplate;
    this.cat = this.$route.params.cat;
    this.mode = this.$route.params.mode;
    this.startdatetime = this.$route.params.startdatetime;
    this.WBTNO = this.$route.params.WBTNO;
    this.nodata = this.$route.params.nodata;
    this.inyard = this.$route.params.inyard;
    this.vehicles = this.$route.params.vehicles;
    this.reweighno = this.$route.params.reweighno;
    this.yard = this.$route.params.yard;
    //alert(this.nplate);
  },
  data() {
    return {
      nplate: "",
      viewTag:false,
      deleteTag:false,
      cat: "",
      mode: "",
      startdatetime: "",
      WBTNO: "",
      nodata: "",
      inyard: "",
      vehicles: "",
      reweighno: "",
      yard: "",
      title: "Tags",
      items: [
        {
          text: "USER: ",
        },
        {
          text: "Tags",
          active: true,
        },
      ],
    };
  },
    watch:{
      viewTag(newValue){
      var curentuser=JSON.parse(localStorage.user).email
      var currentdate=new Date();
    const data={
      datetime:currentdate,
      useremail:curentuser,
      application:window.navigator.userAgent,
      details:`Opened tags list:${newValue}`,
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
     deleteTag(newValue){
      var curentuser=JSON.parse(localStorage.user).email
      var currentdate=new Date();
    const data={
      datetime:currentdate,
      useremail:curentuser,
      application:window.navigator.userAgent,
      details:`Closed Tag:${newValue},Tag id:${this.id}`,
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
    }
  },
  mounted() {
    this.viewTag=true;
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <h1>TAGS for {{ nplate }}</h1>
    <div></div>
    {{ nplate }}
    <tagsdetails
      :nplate="nplate"
      :cat="cat"
      :startdatetime="startdatetime"
      :WBTNO="WBTNO"
      :nodata="nodata"
      :inyard="inyard"
      :vehicles="vehicles"
      :reweighno="reweighno"
      :yard="yard"
      :mode="mode"
    />
  </Layout>
</template>
