<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import Swal from "sweetalert2";
//import html2canvas from "html2canvas";
//import rptheader from "@/components/report/header.js";
import reportdet from "@/components/report/header";

import axios from "axios";

export default {
  page: {
    title: "Routes",
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
    reportdet,
  },
  data() {
    return {
      title: "Routes",
      addroutes:false,
      editroutes:false,
      deleteroutes:false,
      viewRoutes:false,
      clickedPrintpdf:"",
      printedpdf:true,
      generatedExel:"",
      printReport:false,
      filteredValues:"",
      items: [
        {
          text: "USER: Admin",
        },
        {
          text: "Screens List",
          active: true,
        },
      ],
      orderDatanew: null,
      pl: "",
      orderData: [
        //     {
        //       id: "1",
        //       screenname: "TEST",
        //       active: 0,
        //     },
      ],
      id: "",
      errors: "",
      headers: null,
      uniqueCars: null,
      showme: true,
      totalRows: 1,
      currentPage: 1,
      perPage: 10,
      pageOptions: [
        1, 2, 5, 10, 25, 50, 100, 200, 300, 500, 1000, 2000, 3000, 4000, 5000,
        6000, 9000, 10000, 20000,
      ],
      roadauthoritys: ["KeNHA", "KURA"],
      filter: null,
      filterOn: [],
      sortBy: "id",
      sortDesc: false,
      route_name: "",
      roadauthority: "",
      prefix: "",
      myindex: "",
      editmode: false,
      modaltitle: "Add",
      fields: [
        {
          key: "id",
          label: "#",
          sortable: true,
        },
        {
          key: "route_name",
          label: "Routes Name",
          sortable: true,
        },

        "action",
      ],
    };
  },
     watch:{
      viewRoutes(newValue){
      var curentuser=JSON.parse(localStorage.user).email
      var currentdate=new Date();
    const data={
      datetime:currentdate,
      useremail:curentuser,
      application:window.navigator.userAgent,
      details:`Opened Routes:${newValue}`,
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
    printReport(){
     var curentuser=JSON.parse(localStorage.user).email
      var currentdate=new Date();
    const data={
      datetime:currentdate,
      useremail:curentuser,
      application:window.navigator.userAgent,
      details:`Routes reports:${this.clickedPrintpdf},${this.generatedExel},${this.filteredValues}`,
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
     addroutes(newValue){
      var curentuser=JSON.parse(localStorage.user).email
      var currentdate=new Date();
    const data={
      datetime:currentdate,
      useremail:curentuser,
      application:window.navigator.userAgent,
      details:`Add routes:${newValue},value:${this.route_name}`,
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
     editroutes(newValue){
      var curentuser=JSON.parse(localStorage.user).email
      var currentdate=new Date();
    const data={
      datetime:currentdate,
      useremail:curentuser,
      application:window.navigator.userAgent,
      details:`Edit routes:${newValue},value:${this.route_name}`,
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
     deleteroutes(newValue){
      var curentuser=JSON.parse(localStorage.user).email
      var currentdate=new Date();
    const data={
      datetime:currentdate,
      useremail:curentuser,
      application:window.navigator.userAgent,
      details:`Delete routes:${newValue},routes id:${this.id}`,
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
  created(){
   if(window.location.href !=""){
     this.viewRoutes=true;
   }
  },
  computed: {
    /**
     * Total no. of records
     */
    rows() {
      return this.orderData.length;
    },
  },
  mounted() {
    // Set the initial number of items
    this.updatearray();
    this.totalRows = this.items.length;
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
        .get(window.$http + `routes`, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
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
    /**
     * Export to CSV
     */
    getrpt(p_excel) {
      this.printReport=true;
      p_excel="Generated an excel document";
      this.generatedExel=p_excel;
      //alert(new Date());
      const d = new Date();
      const year = d.getFullYear();
      const month = d.getMonth();
      const date = d.getDate();

      const hour = d.getHours();
      const min = d.getMinutes();
      const sec = d.getSeconds();
      const msec = d.getMilliseconds();
      const filename =
        year +
        "-" +
        month +
        "-" +
        date +
        "-" +
        hour +
        "-" +
        min +
        "-" +
        sec +
        "-" +
        msec;
      //alert(filename);
      const data = this.orderData.map((row) => ({
        ID: row.id,
        route_name: row.route_name,
      }));
      //alert("");
      const csvRows = [];
      //get headers
      const headers = Object.keys(data[0]);

      csvRows.push(headers.join(","));
      //alert(csvRows);
      //loop over the headers
      for (const row of data) {
        const values = headers.map((header) => {
          const escaped = ("" + row[header]).replace(/"/g, '\\"');
          // alert(escaped);
          return '"' + escaped + '"'; //'" + escaped + "';
        });
        csvRows.push(values.join(","));
      }
      //alert(csvData);
      const csvData = csvRows.join("\n");
      //alert(csvData);

      const blob = new Blob([csvData], { type: "textcsv" });
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement("a");
      a.setAttribute("hidden", "");
      a.setAttribute("href", url);
      a.setAttribute("download", this.title + filename + ".csv");
      document.body.appendChild(a);
      a.click();
      document.body.removeChild(a);
    },

    /**
     * Search the table data with search input
     */
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
      this.printReport=true;
      this.filteredValues=this.filter;
    },
    handleSubmit() {
      console.log("Error on submit");
    },
    printpdf(pl,p_pdf) {
      this.printReport=true;
      p_pdf="Clicked print pdf button";
      this.clickedPrintpdf=p_pdf;
      this.pl = pl;
      const data = this.orderData.map((row) => ({
        ID: row.id,
        route_name: row.route_name,
      }));

      //get headers
      const headers = Object.keys(data[0]);
      const cars = [];
      Object.entries(data).forEach((val) => {
        const [key, value] = val;
        console.log(key, value);
        cars.push(Object.values(data[key]));
      });

      const uniqueCars = Array.from(new Set(cars));
      this.headers = headers;
      this.uniqueCars = uniqueCars;
      //alert(headers);
    },
    edit(index, myid, route_name) {
      //alert(rolename);
      this.editmode = true;
      this.modaltitle = "Edit Screens";
      this.id = myid;
      this.route_name = route_name;
      this.myindex = index;
      //this.focusInput();

      //this.$refs.search.focus();
      //$("#search").focus();
    },
    editrec() {
      if (this.route_name.trim() == "") {
        Swal.fire("Please enter  Screens Name!");
        return;
      }

      //alert(this.myindex);

      const data = {
        id: this.id,
        route_name: this.route_name,
        active: 0,
      };
      //alert(this.screenname);
      axios
        .put(window.$http + `routes/` + this.id, data, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData[this.myindex].id = this.id;
          this.orderData[this.myindex].route_name = this.route_name;
          this.orderData[this.myindex].active = this.active;
          console.log(response.data);
          this.editroutes=true;
          if (response.data.trim() != "") {
            Swal.fire({
              position: "center",
              icon: "success",
              title: "Your work has been saved" + response.data,
              showConfirmButton: false,
              timer: 1500,
            });
          }
        })
        .catch((e) => {
          Swal.fire("" + e);
          //this.errors.push(e);
        });

      Swal.fire({
        position: "center",
        icon: "success",
        title: "Your work has been saved",
        showConfirmButton: false,
        timer: 1500,
      });
    },
    position() {
      if (this.route_name.trim() == "") {
        Swal.fire("Please enter  Screens Name!");
        return;
      }

      var orderid = this.orderData.length + 1;
      this.orderData.push({
        id: orderid,
        route_name: this.route_name,
      });
      const data = {
        route_name: this.route_name,
        active: 0,
      };
      axios
        .post(window.$http + `routes/`, data, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          console.log(response.data);
          this.addroutes=true;
          Swal.fire({
            position: "center",
            icon: "success",
            title: "Your work has been saved",
            showConfirmButton: false,
            timer: 1500,
          });
        })
        .catch((e) => {
          Swal.fire("" + e);
        });

    },
    deleterec(index, id, route_name) {
      //alert(rolename);
      this.id = id;
      this.route_name = route_name;
      Swal.fire({
        title: "Are you sure, you want to delete " + this.route_name + "?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, delete it!",
      }).then((result) => {
        if (result.value) {
          axios
            .delete(window.$http + `Routes/` + this.id, {
              headers: window.$headers,
            })
            .then((response) => {
              // JSON responses are automatically parsed.
              console.log(response.data);
              this.deleteroutes=true;
              this.orderData.splice(index, 1);
              Swal.fire(
                this.route_name + " Deleted!",
                "Your record has been deleted.",
                "success"
              );
            })
            .catch((e) => {
              Swal.fire("" + e);
            });
        }
      });
    },
    clearvalues() {
      this.name = "";
      this.editmode = false;
      this.modaltitle = "Add Screens";
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <div ref="content">
      <div class="card">
        <div class="card-body">
          <form @submit.prevent="handleSubmit">
            <div class="card">
              <div class="card-body">
                <div class="row justify-content-between">
                  <div class="col-sm-6">
                    <button
                      class="
                        btn btn-primary
                        waves-effect waves-light
                        uil-export
                      "
                      @click="getrpt(p_excel)"
                    >
                      Export to CSV
                    </button>
                  </div>

                  <div class="col-sm-2">
                    <button
                      @click="printpdf('p',p_pdf)"
                      v-b-modal.modal-Print
                      class="
                        btn btn-primary
                        waves-effect waves-light
                        mdi-file-pdf
                      "
                    >
                      Print PDF
                    </button>
                  </div>
                  <div class="col-sm-2">
                    <button
                      v-b-modal.modal-Edit
                      class="
                        btn btn-primary
                        waves-effect waves-light
                        uil-focus-add
                      "
                      @click="clearvalues()"
                    >
                      Add {{ title }}
                    </button>
                  </div>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-sm-12">
                <div class="card">
                  <div class="card-body changebg">
                    <div class="row" id="print">
                      <div class="col-12">
                        <div>
                          <div class="float-end">
                            <form class="d-inline-flex mb-3"></form>
                          </div>
                        </div>
                        <div
                          class="
                            table table-centered
                            datatable
                            dt-responsive
                            nowrap
                            table-card-list
                            dataTable
                            no-footer
                            dtr-inline
                          "
                        >
                          <div class="row">
                            <div class="col-sm-12 col-md-6">
                              <div
                                id="tickets-table_length"
                                class="dataTables_length"
                              >
                                <label
                                  class="
                                    d-inline-flex
                                    align-items-center
                                    fw-normal
                                  "
                                >
                                  Show&nbsp;
                                  <b-form-select
                                    v-model="perPage"
                                    size="sm"
                                    :options="pageOptions"
                                  ></b-form-select
                                  >&nbsp;entries
                                </label>
                              </div>
                            </div>
                            <!-- Search -->
                            <div class="col-sm-12 col-md-6">
                              <div
                                id="tickets-table_filter"
                                class="dataTables_filter text-md-end"
                              >
                                <label
                                  class="
                                    d-inline-flex
                                    align-items-center
                                    fw-normal
                                  "
                                >
                                  Search:
                                  <b-form-input
                                    v-model="filter"
                                    type="search"
                                    placeholder="Search..."
                                    class="form-control form-control-sm ms-2"
                                  ></b-form-input>
                                </label>
                              </div>
                            </div>
                            <!-- End search -->
                          </div>
                          <!-- Table -->

                          <b-table
                            table-class="table table-centered datatable table-card-list"
                            thead-tr-class="bg-transparent"
                            :items="orderData"
                            :fields="fields"
                            responsive="sm"
                            :per-page="perPage"
                            :current-page="currentPage"
                            :sort-by.sync="sortBy"
                            :sort-desc.sync="sortDesc"
                            :filter="filter"
                            :filter-included-fields="filterOn"
                            @filtered="onFiltered"
                          >
                            <template v-slot:cell(screens)="data">
                              <a
                                href="javascript: void(0);"
                                class="text-dark fw-bold"
                                v-for="routes in data.item.route_name"
                                :key="routes"
                                >{{ routes }} <br
                              /></a>
                            </template>
                            <!----
                          <template v-slot:cell(check)="data">
                            <div class="">
                              <input
                                type="button"
                                class="custom-control-input"
                                :id="`contacusercheck${data.item.id}`"
                                value="Submit"
                              />
                              <label
                                class="custom-control-label"
                                :for="`contacusercheck${data.item.id}`"
                              ></label>
                            </div>
                          </template>
                          <template v-slot:cell(id)="data">
                            <a
                              href="javascript: void(0);"
                              class="text-dark fw-bold"
                              >{{ data.item.id }}</a
                            >
                          </template>

                          <template v-slot:cell(name)="data">
                            <a href="#" class="text-body">{{
                              data.item.name
                            }}</a>
                          </template>
                          <template v-slot:cell(status)="data">
                            <div
                              class="badge bg-pill bg-soft-success font-size-12"
                              :class="{
                                'bg-soft-danger':
                                  data.item.status === 'Chargeback',
                                'bg-soft-warning':
                                  data.item.status === 'unpaid',
                              }"
                            >
                              {{ data.item.status }}
                            </div>
                          </template>
                          --->
                            <template v-slot:cell(action)="data">
                              <ul class="list-inline mb-0">
                                <li class="list-inline-item">
                                  <a
                                    href="javascript:void(0);"
                                    class="px-2 text-primary"
                                    v-b-tooltip.hover
                                    title="Edit"
                                    v-b-modal.modal-Edit
                                    @click="
                                      edit(
                                        data.index,
                                        data.item.id,
                                        data.item.route_name
                                      )
                                    "
                                  >
                                    <i class="uil uil-pen font-size-18"></i>
                                  </a>
                                </li>
                                <li class="list-inline-item">
                                  <a
                                    href="javascript:void(0);"
                                    class="px-2 text-danger"
                                    v-b-tooltip.hover
                                    title="Delete"
                                    @click="
                                      deleterec(
                                        data.index,
                                        data.item.id,
                                        data.item.route_name
                                      )
                                    "
                                  >
                                    <i
                                      class="uil uil-trash-alt font-size-18"
                                    ></i>
                                  </a>
                                </li>
                              </ul>
                            </template>
                          </b-table>
                        </div>
                        <div class="row">
                          <div class="col">
                            <div
                              class="
                                dataTables_paginate
                                paging_simple_numbers
                                float-end
                              "
                            >
                              <ul class="pagination pagination-rounded">
                                <!-- pagination -->
                                <b-pagination
                                  v-model="currentPage"
                                  :total-rows="rows"
                                  :per-page="perPage"
                                ></b-pagination>
                              </ul>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
    <b-modal id="modal-Edit" :title="modaltitle" hide-footer size="lg" centered>
      <div class="row">
        <div class="col-md-12">
          <div class="card">
            <div class="card-body">
              <form @submit.prevent="handleSubmit">
                <div class="row">
                  <div class="input-group">
                    <div v-show="editmode" class="input-group-text col-sm-4">
                      ID:
                    </div>

                    <input
                      v-show="editmode"
                      class="form-control"
                      type="text"
                      disabled="disabled"
                      placeholder="id"
                      v-model="id"
                    />
                  </div>
                  <div class="input-group">
                    <div class="input-group-text col-sm-4">Routes Name:</div>

                    <input
                      class="form-control"
                      type="text"
                      placeholder="Name"
                      v-model="route_name"
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
          <b-button v-show="!editmode" variant="dark" @click="position()"
            >Add {{ title }}</b-button
          >
          <b-button v-show="editmode" variant="dark" @click="editrec()"
            >Edit {{ title }}</b-button
          >
        </div>
      </div>
    </b-modal>
    <b-modal id="modal-Print" title="Print PDF" hide-footer size="bg" centered>
      <reportdet
        :title="title"
        :orderData="orderData"
        :pl="pl"
        :headers="headers"
        :uniqueCars="uniqueCars"
        :shome="showme"
        v-show="showme"
      ></reportdet>
    </b-modal>
  </Layout>
</template>
//#print {
  //height: 11in;
  //width: 8.5in;
//}
<style scoped>
.changebg {
  background-color: #f7f6ebfb;
}
</style>