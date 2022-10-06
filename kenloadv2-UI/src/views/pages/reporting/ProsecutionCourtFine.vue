<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import Swal from "sweetalert2";

export default {
  page: {
    title: "Prohibited Report",
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
      title: "Reporting",
      items: [
        {
          text: "USER: Management",
        },
        {
          text: "Prohibited Report",
          active: true,
        },
      ],
      reporttitle: "PROHIBITED REPORT",
      date: "2021-05-23",
      time: "21:30:05",
      orderDatanew: null,
      orderData: [
        {
          no: "1",
          reg: "KBA456G",
          ticket: "SR0001",
          status: "Closed",
          wb: "AthiRiver Weighbridge",
          user: "COW",
          transporter: "Kaburu",
          shift: "Shift A",
          axle: "6C",
          cargo: "Petrol",
        },
       {
          no: "2",
          reg: "KBA456G",
          ticket: "SR0002",
          status: "Charged",
          wb: "Isinya Weighbridge",
          user: "COW",
          transporter: "Kaburu",
          shift: "Shift A",
          axle: "6C",
          cargo: "Petrol",
        },
        {
          no: "3",
          reg: "KBA456G",
          ticket: "SR0003",
          status: "Prohibited",
          wb: "Juja Weighbridge",
          user: "COW",
          transporter: "Kaburu",
          shift: "Shift A",
          axle: "6C",
          cargo: "Petrol",
        },
        {
          no: "4",
          reg: "KBA456G",
          ticket: "SR0004",
          status: "Closed",
          wb: "Rongo Weighbridge",
          user: "COW",
          transporter: "Kaburu",
          shift: "Shift A",
          axle: "6C",
          cargo: "Petrol",
        },
        {
          no: "5",
          reg: "KBA456G",
          ticket: "SR0005",
          status: "Released",
          wb: "Busia Weighbridge",
          user: "COW",
          transporter: "Kaburu",
          shift: "Shift A",
          axle: "6C",
          cargo: "Petrol",
        },
        {
          no: "6",
          reg: "KBA456G",
          ticket: "SR0006",
          status: "Closed",
          wb: "Mariakani Weighbridge",
          user: "COW",
          transporter: "Kaburu",
          shift: "Shift A",
          axle: "6C",
          cargo: "Petrol",
        },
        {
          no: "7",
          reg: "KBA456G",
          ticket: "SR0007",
          status: "Prohibited",
          wb: "Dongo Kundu Weighbridge",
          user: "COW",
          transporter: "Kaburu",
          shift: "Shift A",
          axle: "6C",
          cargo: "Petrol",
        },
        {
          no: "8",
          reg: "KBA456G",
          ticket: "SR0008",
          status: "Closed",
          wb: "Mariakani Mombasa Bound Weighbridge",
          user: "COW",
          transporter: "Kaburu",
          shift: "Shift A",
          axle: "6C",
          cargo: "Petrol",
        },
        {
          no: "9",
          reg: "KBA456G",
          ticket: "SR0009",
          status: "Charged",
          wb: "Mariakani Nairobi Bound Weighbridge",
          user: "COW",
          transporter: "Kaburu",
          shift: "Shift A",
          axle: "6C",
          cargo: "Petrol",
        },
        {
          no: "10",
          reg: "KBA456G",
          ticket: "SR00010",
          status: "Closed",
          wb: "Webuye Weighbridge",
          user: "COW",
          transporter: "Kaburu",
          shift: "Shift A",
          axle: "6C",
          cargo: "Petrol",
        },
        {
          no: "11",
          reg: "KBA456G",
          ticket: "SR00011",
          status: "Charged",
          wb: "Maai Mahiu Weighbridge",
          user: "COW",
          transporter: "Kaburu",
          shift: "Shift A",
          axle: "6C",
          cargo: "Petrol",
        },
        {
          no: "12",
          reg: "KBA456G",
          ticket: "SR00012",
          status: "Released",
          wb: "Rongo Weighbridge",
          user: "COW",
          transporter: "Kaburu",
          shift: "Shift A",
          axle: "6C",
          cargo: "Petrol",
        },
        {
          no: "13",
          reg: "KBA456G",
          ticket: "SR00013",
          status: "Special release",
          wb: "Juja Nairobi Bound Weighbridge",
          user: "COW",
          transporter: "Kaburu",
          shift: "Shift A",
          axle: "6C",
          cargo: "Petrol",
        },
        {
          no: "14",
          reg: "KBA456G",
          ticket: "SR00014",
          status: "Special release",
          wb: "Juja Thika Bound Weighbridge",
          user: "COW",
          transporter: "Kaburu",
          shift: "Shift A",
          axle: "6C",
          cargo: "Petrol",
        },
        {
          no: "15",
          reg: "KBA456G",
          ticket: "SR00015",
          status: "Charged",
          wb: "Isinya Weighbridge",
          user: "COW",
          transporter: "Kaburu",
          shift: "Shift A",
          axle: "6C",
          cargo: "Petrol",
        },
        {
          no: "16",
          reg: "KBA456G",
          ticket: "SR00016",
          status: "Closed",
          wb: "AthiRiver Mombasa Bound Weighbridge",
          user: "COW",
          transporter: "Kaburu",
          shift: "Shift A",
          axle: "6C",
          cargo: "Petrol",
        },
        {
          no: "17",
          reg: "KBA456G",
          ticket: "SR00017",
          status: "Closed",
          wb: "AthiRiver Nairobi Bound Weighbridge",
          user: "COW",
          transporter: "Kaburu",
          shift: "Shift A",
          axle: "6C",
          cargo: "Petrol",
        },
        {
          no: "18",
          reg: "KBA456G",
          ticket: "SR00018",
          status: "Charged",
          wb: "Busia Weighbridge",
          user: "COW",
          transporter: "Kaburu",
          shift: "Shift A",
          axle: "6C",
          cargo: "Petrol",
        },
        {
          no: "19",
          reg: "KBA456G",
          ticket: "SR00019",
          status: "Closed",
          wb: "Isinya Weighbridge",
          user: "COW",
          transporter: "Kaburu",
          shift: "Shift A",
          axle: "6C",
          cargo: "Petrol",
        },
        {
          no: "20",
          reg: "KBA456G",
          ticket: "SR00020",
          status: "Released",
          wb: "Juja Weighbridge",
          user: "COW",
          transporter: "Kaburu",
          shift: "Shift A",
          axle: "6C",
          cargo: "Petrol",
        },
      ],
      id: "",
      myid: "test id",
      totalRows: 1,
      currentPage: 1,
      perPage: 10,
      pageOptions: [
        1,
        2,
        5,
        10,
        25,
        
      ],
      filter: null,
      filterOn: [],
      sortBy: "id",
      sortDesc: false,
      roadname: "",
      code: "",
      myindex: "",
      editmode: false,
      modaltitle: "Add",
      fields: [
        {
          key: "no",
          label: "No",
          sortable: true,
        },
        {
          key: "reg",
          label: "Vehicle Registration",
          sortable: true,
        },
        {
          key: "ticket",
          label: "Ticket Number",
          sortable: true,
        },
        {
          key: "status",
          label: "Status",
          sortable: true,
        },
        {
          key: "wb",
          label: "Weighbridge",
          sortable: true,
        },
        {
          key: "user",
          label: "User",
          sortable: true,
        },
        {
          key: "transporter",
          label: "Transporter",
          sortable: true,
        },
        {
          key: "shift",
          label: "Shift",
          sortable: true,
        },
        {
          key: "axle",
          label: "Axle Type",
          sortable: true,
        },
        {
          key: "cargo",
          label: "Cargo",
          sortable: true,
        },
      ],
    };
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
    this.totalRows = this.items.length;
  },
  methods: {
    /**
     * Go back to report page
     */
    goToReporting() {
      this.$router.push("/reporting");
    },
    /**
     * Search the table data with search input
     */
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
    },
    handleSubmit() {
      console.log("Error on submit");
    },
    edit(index, myid, code, roadname) {
      //alert(rolename);
      this.editmode = true;
      this.modaltitle = "Edit Road Authority";
      this.id = myid;
      this.roadname = roadname;
      this.code = code;
      this.myindex = index;
    },
    editrec() {
      if (this.code.trim() == "") {
        Swal.fire("Please enter Road Authority Code!");
        return;
      }
      if (this.roadname.trim() == "") {
        Swal.fire("Please enter Road Authority Name!");
        return;
      }
      //alert(this.myindex);
      this.orderData[this.myindex].id = this.id;
      this.orderData[this.myindex].name = this.code;
      this.orderData[this.myindex].desc = this.roadname;
      Swal.fire({
        position: "center",
        icon: "success",
        title: "Your work has been saved",
        showConfirmButton: false,
        timer: 1500,
      });
    },
    position() {
      if (this.code.trim() == "") {
        Swal.fire("Please enter Road Authority Code!");
        return;
      }
      if (this.roadname.trim() == "") {
        Swal.fire("Please enter Road Authority Name!");
        return;
      }

      var orderid = this.orderData.length + 1;
      this.orderData.push({
        id: orderid,
        name: this.code,
        desc: this.roadname,
      });
      this.roadname = "";
      this.code = "";
      Swal.fire({
        position: "center",
        icon: "success",
        title: "Your work has been saved",
        showConfirmButton: false,
        timer: 1500,
      });
    },
    deleterec(index, myid, code, roadname) {
      this.id = myid;
      this.roadname = roadname;
      this.code = code;
      Swal.fire({
        title: "Are you sure, you want to delete " + this.roadname + "?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, delete it!",
      }).then((result) => {
        if (result.value) {
          this.orderData.splice(index, 1);
          //this.$delete(this.orderData, this.id - 1);
          Swal.fire(
            "Deleted!",
            this.roadname + " has been deleted.",
            "success"
          );
        }
      });
    },
    clearvalues() {
      this.roadname = "";
      this.code = "";
      this.editmode = false;
      this.modaltitle = "Add Road Authority";
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />

    <div class="card">
      <div class="card-body">
        <form @submit.prevent="handleSubmit">
         
              <div class="row justify-content-between">
                <div class="col-sm-8">
                  <div class="row">
                    <div class="col-sm-3">
                      <b-button variant="success" v-on:click="goToWbRegisterReport()">
                        Export To Excel</b-button>
                    </div>
                  </div>
                </div>

                <div class="col-sm-2">
                  <div class="row">
                    <div class="col-sm-8">
                      <b-button variant="success" v-on:click="goToWbRegisterReport()"> <a  href="javascript:window.print()"
                    class=" me-1" style="color:white;">Print</a>
                        </b-button>
                    </div>
                  </div>
                </div>
                <div class="col-sm-2">
                  <div class="row">
                    <div class="col-sm-8">
                      <b-button variant="danger" v-on:click="goToReporting()">
                        Close</b-button>
                    </div>
                  </div>
              
            </div>
          </div>
          <!-- Kenha logo -->
           <div align="center" class="mb-3 mt-3">
      <img
        src="@/assets/images/prosecution/Coatofarmskenya.png"
        style="width: 90px; height: 85px;"
      />
    </div>
    <div align="center">
      <img src="@/assets/images/prosecution/kenhalogo.png" />
      <p>
        <font size="1">
          Blueshield Towers, Hospital Road, Upper Hill P.O. Box 49712 - 00100
          Nairobi <br />
          Tel: 020-801342 Email:dg@kenha.co.ke / info@kenha.co.ke
          Website:www.kenha.co.ke
        </font>
      </p>
    </div>
    <!-- Title -->
    <div>
      <p align="center" style="font-size: 23px">
        <br /><strong> {{reporttitle}} </strong> <br />
      </p>
      <p align="center">Report Date: <b>{{ date }} </b> Time:<b>{{ time }}</b></p>
      <hr>
    </div>
          <div class="row">
            <div class="col-sm-12">
            
                <div class=" changebg">
                  <div class="row">
                    <div class="col-12">
                      <div>
                        <div class="float-end">
                          <form class="d-inline-flex mb-3"></form>
                        </div>
                      </div>
                      <div
                        class="table table-centered datatable dt-responsive nowrap table-card-list dataTable no-footer dtr-inline"
                      >
                        <div class="row">
                          <div class="col-sm-12 col-md-6">
                            <div
                              id="tickets-table_length"
                              class="dataTables_length"
                            >
                              <label
                                class="d-inline-flex align-items-center fw-normal"
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
                              v-for="screen in data.item.screens"
                              :key="screen"
                              >{{ screen }} <br
                            /></a>
                          </template>
                        </b-table>
                      </div>
                      <div class="row">
                        <div class="col">
                          <div
                            class="dataTables_paginate paging_simple_numbers float-end"
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
        </form>
      </div>
    </div>
  </Layout>
</template>
<style scoped>
.changebg {
  background-color: #FFFFFF;
}
</style> 