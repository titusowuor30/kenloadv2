<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import Swal from "sweetalert2";
import axios from "axios";

export default {
  page: {
    title: "Audit Logs",
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
      title: "Audit Logs",
      from_date: "12/27/2021",
      to_date: "12/28/2021",
      items: [
        {
          text: "USER: Admin",
        },
        {
          text: "Audit Logs",
          active: true,
        },
      ],

      orderData: [
        {
          id: "#1",
          datetime: "2021-07-18T17:23:16",
          useremail: "admin@masterspace.com",
          screen: "Prosecution",
          desc: "Opened Prosecution list",
          print: "1",
          comp: "192.168.0.10",
        },
      ],
      log_id: "",
      totalRows: 1,
      currentPage: 1,
      perPage: 10,
      pageOptions: [1, 10, 25, 50, 100, 500],
      filter: null,
      filterOn: [],
      sortBy: "id",
      sortDesc: true,
      fields: [
        {
          key: "id",
          label: "id",
          sortable: true,
        },
        {
          key: "datetime",
          label: "Date",
          sortable: true,
        },
        {
          key: "useremail",
          label: "User",
          sortable: true,
        },
        {
          key: "application",
          label: "Application",
          sortable: true,
        },
        {
          key: "details",
          label: "Details",
          sortable: true,
        },
        {
          key: "computer",
          label: "Computer",
          sortable: true,
        },
        "Action",
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
    this.from_date = new Date();
    this.to_date = new Date();
    // Set the initial number of items
    this.getrec();
    this.totalRows = this.items.length;
  },
  methods: {
    getmydate(mydate) {
      let d = new Date(mydate);
      let year = d.getFullYear();
      let month = this.getmonth(d.getMonth());
      let date = d.getDate();
      date = this.getv(date);
      //month = this.getv(month);

      //const msec = d.getMilliseconds();
      const datetime = date + "/" + month + "/" + year;
      return datetime;
    },
    getrec() {
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      //alert();
      axios
        .get(window.$http + `AuditLogs/search?limit=` + this.perPage, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.

          this.orderData = response.data;

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
     * Search the table data with search input
     */
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
    },
    editrec() {
      Swal.fire(
        "Operation not allowed!" +
          "\nLog entry editing is not allowed for any user account"
      );
    },
    //delete logs
    deleterec(index, log_id, useremail) {
      //alert(rolename);
      this.log_id = log_id;
      this.useremail = useremail;
      Swal.fire({
        title:
          "Are you sure, you want to delete log entry " +
          this.log_id +
          " for " +
          this.useremail +
          "?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, delete it!",
      }).then((result) => {
        if (result.value) {
          this.orderData.splice(index, 1);
          axios
            .delete(window.$http + `AuditLogs/` + this.log_id, {
              headers: window.$headers,
            })
            .then((response) => {
              // JSON responses are automatically parsed.
              console.log(response.data);
              this.orderData.splice(index, 1);
              Swal.fire(
                this.useremail + " log entry " + this.log_id + " Deleted!",
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
    handleSubmit() {
      console.log("Error on submit");
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />

    <div class="card">
      <div class="card-body overflow-auto">
        <form @submit.prevent="handleSubmit">
          <div class="row">
            <div class="col-sm-12">
              <div class="row">
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
                      <div class="col-sm-12 col-md-12">
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
                              @change="getrec()"
                              :options="pageOptions"
                            ></b-form-select
                            >&nbsp;entries
                          </label>
                        </div>
                      </div>
                      <div class="col-sm-2">
                        <span>From:</span>
                        <date-picker
                          @blur="getresults()"
                          :value="new Date()"
                          v-model="from_date"
                          :first-day-of-week="1"
                          lang="en"
                        ></date-picker>
                      </div>
                      <div class="col-sm-2">
                        <span>To:</span>
                        <date-picker
                          @blur="getresults()"
                          :value="new Date()"
                          v-model="to_date"
                          :first-day-of-week="1"
                          lang="en"
                        ></date-picker>
                      </div>
                      <!-- Search -->
                      <div class="col-sm-12 col-md-6">
                        <div
                          id="tickets-table_filter"
                          class="dataTables_filter text-md-end"
                        >
                          <label
                            class="d-inline-flex align-items-center fw-normal"
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
                              @click="editrec()"
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
                                  data.item.useremail
                                )
                              "
                            >
                              <i class="uil uil-trash-alt font-size-18"></i>
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
        </form>
      </div>
    </div>
  </Layout>
</template>
<style scoped>
.changebg {
  background-color: #f7f6ebfb;
}
</style>
