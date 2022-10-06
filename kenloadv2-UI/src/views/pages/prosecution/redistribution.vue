<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

//import { tableData } from "./dataAdvancedtable";

export default {
  page: {
    title: "Redistribution",
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
    //tableData,
  },
  data() {
    return {
      title: "Vehicle Load Redistribution",
      items: [
        {
          text: "USER: COP",
        },
        {
          text: "Load Redistribution",
          active: true,
        },
      ],
      autoClose: "",
      shift: null,
      route: null,
      hour: null,
      shifts: ["Shift A", "Shift B", "Shift C"],
      routes: ["Nairobi-Mombasa", "Mombasa-Nairobi"],
      hours: [
        "00",
        "01",
        "02",
        "03",
        "04",
        "05",
        "06",
        "07",
        "08",
        "09",
        "10",
        "11",
        "12",
        "13",
        "14",
        "15",
        "16",
        "17",
        "18",
        "19",
        "20",
        "21",
        "22",
        "23",
      ],
      orderData: [
        {
          ticketno: "WBT0001",
          registration: "KBA857V",
          date: "2018/07/29",
          GVW: "48000",
          excess: 1000,
          memo: 0,
          status: "Axle Overload",
        },
        {
          ticketno: "WBT0002",
          registration: "KAA445",
          date: "2019/07/13",
          GVW: "49000",
          excess: 590,
          memo: 0,
          status: "Axle Overload",
        },
        {
          ticketno: "WBT0003",
          registration: "KAD444",
          date: "2019/04/08",
          GVW: "19000",
          excess: 520,
          memo: 0,
          status: "Axle Overload",
        },
        {
          ticketno: "WBT0004",
          registration: "KCY190",
          date: "2018/12/14",
          GVW: "28000",
          excess: 500,
          memo: 0,
          status: "Axle Overload",
        },
        {
          ticketno: "WBT0005",
          registration: "KCA190",
          date: "2019/06/19",
          GVW: "19000",
          excess: 46,
          memo: 0,
          status: "Axle Overload",
        },
      ],
      totalRows: 1,
      currentPage: 1,
      perPage: 5,
      pageOptions: [5, 10, 25, 50, 100, 200],
      filter: null,
      filterOn: [],
      sortBy: "id",
      sortDesc: false,
      fields: [
        {
          key: "ticketno",
          sortable: true,
        },
        {
          key: "registration",
          sortable: true,
        },
        {
          key: "date",
          sortable: true,
        },
        {
          key: "GVW",
          sortable: true,
        },
        {
          key: "excess",
          sortable: true,
        },
        {
          key: "status",
          sortable: true,
        },
        {
          key: "memo",
          label: "Conditional Memo",
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
    goToProhibition() {
      this.$router.push("/prohibition");
    },
    /**
     * Load correction memo
     */
    goToLcm() {
      this.$router.push("/lcm");
    },
    /**
     * Conditional Load correction memo
     */
    goToClcm() {
      this.$router.push("/clcm");
    },
    /**
     * Charge Sheet Owner
     */
    goToChargesheet() {
      this.$router.push("/ownerchargesheet");
    },
    /**
     * Charge Sheet Driver
     */
    goToChargesheetdriver() {
      this.$router.push("/chargesheet");
    },
    /**
     * EAC WB Certificate
     */
    goToEaccert() {
      this.$router.push("/eaccert");
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />

    <form @submit.prevent="handleSubmit">
      <div class="row">
        <div class="col-sm-12">
          <div class="card">
            <div class="card-body changebg">
              <b-card-title>
                <h5 class="card-title"></h5>
              </b-card-title>

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
                      <template v-slot:cell(memo)="data">
                        <div class="button">
                          <input
                            type="button"
                            variant="primary"
                            class="btn btn-primary waves-effect waves-light"
                            :id="`contacusercheck${data.item.id}`"
                            value="Generate Memo"
                            @click="goToClcm()"
                          />
                        </div>
                      </template>

                      <template v-slot:cell(id)="data">
                        <a
                          href="javascript: void(0);"
                          class="text-dark fw-bold"
                          >{{ data.item.id }}</a
                        >
                      </template>

                      <template v-slot:cell(registration)="data">
                        <a
                          href="#"
                          :click="{
                            'goToProhibition()':
                              data.item.registration === 'KBA857V',
                            'goToProhibition()':
                              data.item.registration === 'unpaid',
                          }"
                          class=""
                        >
                          {{ data.item.registration }}
                        </a>
                      </template>
                      <template v-slot:cell(action)="data">
                        <div
                          class="badge bg-pill bg-soft-success font-size-12"
                          :class="{
                            'bg-soft-danger': data.item.action === 'prohibited',
                            'bg-soft-warning': data.item.action === 'unpaid',
                          }"
                          :click="{
                            'goToProhibition()':
                              data.item.action === 'prohibited',
                            'goToProhibition()': data.item.action === 'unpaid',
                          }"
                        >
                          {{ data.item.action }}
                        </div>
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
      </div>
    </form>
  </Layout>
</template>
<style scoped>
.changebg {
  background-color: #f7f6ebfb;
}
</style>