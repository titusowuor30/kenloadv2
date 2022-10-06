<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import VehicleTagsTable from "@/components/widgets/weigh/tagsdetails";
import addTags from "@/components/widgets/weigh/tagcreate";
import axios from "axios";

export default {
  data() {
    return {
      title: "Vehicle Tags",
      viewTag: false,
      deleteTag: false,
      addTag: false,
      items: [
        {
          text: "Mlolongo",
        },
        {
          text: "Mombasa Bound",
          active: true,
        },
      ],

      vehicleTagsData: [
        {
          id: 1,
          registration_no: "KBB000A",
          reason: "Overload",
          status: "Open",
          date: "12 April 21",
        },
        {
          id: 2,
          registration_no: "KBB000A",
          reason: "Overload",
          status: "Open",
          date: "15 May 21",
        },
        {
          id: 3,
          registration_no: "KBB000A",
          reason: "Overload",
          status: "Open",
          date: "12 April 21",
        },
      ],
    };
  },
  components: {
    Layout,
    PageHeader,
    VehicleTagsTable,
    addTags,
  },
  watch: {
    viewTag(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Opened vehicle tags list:${newValue}`,
        computer: window.localStorage.clientip,
      };
      axios
        .post(window.$http + "AuditLogs", data, { headers: window.$headers })
        .then((response) => {
          console.log(response.data);
        })
        .catch((e) => {
          console.log(e);
        });
    },
    addTag(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Added a vehicle Tag:${newValue}`,
        computer: window.localStorage.clientip,
      };
      axios
        .post(window.$http + "AuditLogs", data, { headers: window.$headers })
        .then((response) => {
          console.log(response.data);
        })
        .catch((e) => {
          console.log(e);
        });
    },
    deleteTag(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Closed a vehicle Tag:${newValue},Tag id:${this.id}`,
        computer: window.localStorage.clientip,
      };
      axios
        .post(window.$http + "AuditLogs", data, { headers: window.$headers })
        .then((response) => {
          console.log(response.data);
        })
        .catch((e) => {
          console.log(e);
        });
    },
  },
  mounted() {
    this.viewTag = true;
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <div class="card">
      <div class="card-body">
        <div class="row justify-content-between">
          <div class="col-sm-2">
            <button
              @click="printpdf('p')"
              v-b-modal.modal-Print
              class="btn btn-primary waves-effect waves-light mdi-file-pdf"
            >
              Print PDF
            </button>
          </div>
          <div class="col-sm-2"></div>
          <div class="col-sm-2">
            <button
              v-b-modal.modal-Add
              class="btn btn-primary waves-effect waves-light uil-focus-add"
            >
              Add Tag
            </button>
          </div>
        </div>
      </div>
    </div>
    <b-modal id="modal-Add" title="Add Tag" hide-footer size="xl" centered>
      <addTags />
    </b-modal>
    <VehicleTagsTable :vehicleTagsData="vehicleTagsData" />
  </Layout>
</template>