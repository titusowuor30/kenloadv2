<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";
import axios from "axios";

//import { tableData } from "./dataAdvancedtable";

export default {
  page: {
    title: "Prosecute",
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
      title: "Prosecute",
      items: [
        {
          text: "USER: COP",
        },
        {
          text: "Prosecute",
          active: true,
        },
      ],
      autoClose: "",
      shift: null,
      route: null,
      hour: null,
      tpolice: "",
      tpolices: [],
      vi: "",
      VIs: [],
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
          prohibit: 0,
          status: "prohibited",
        },
        {
          ticketno: "WBT0002",
          registration: "KAA445",
          date: "2019/07/13",
          GVW: "49000",
          excess: 590,
          prohibit: 0,
          status: "prohibited",
        },
        {
          ticketno: "WBT0003",
          registration: "KAD444",
          date: "2019/04/08",
          GVW: "19000",
          excess: 520,
          prohibit: 0,
          status: "prohibited",
        },
        {
          ticketno: "WBT0004",
          registration: "KCY190",
          date: "2018/12/14",
          GVW: "28000",
          excess: 500,
          prohibit: 0,
          status: "unpaid",
        },
        {
          ticketno: "WBT0005",
          registration: "KCA190",
          date: "2019/06/19",
          GVW: "19000",
          excess: 46,
          prohibit: 0,
          status: "prohibited",
        },
        {
          ticketno: "WBT0006",
          registration: "KAB458G",
          date: "2019/06/19",
          GVW: "19000",
          excess: 46,
          prohibit: 0,
          status: "unpaid",
        },
        {
          ticketno: "WBT0007",
          registration: "KAG235J",
          date: "2019/06/19",
          GVW: "19000",
          excess: 46,
          prohibit: 0,
          status: "prohibited",
        },
        {
          ticketno: "WBT0008",
          registration: "KAM567R",
          date: "2019/06/19",
          GVW: "19000",
          excess: 46,
          prohibit: 0,
          status: "prohibited",
        },
        {
          ticketno: "WBT0009",
          registration: "KAN576D",
          date: "2019/06/19",
          GVW: "19000",
          excess: 46,
          prohibit: 0,
          status: "prohibited",
        },
        {
          ticketno: "WBT0010",
          registration: "KCP234Y",
          date: "2019/06/19",
          GVW: "19000",
          excess: 46,
          prohibit: 0,
          status: "prohibited",
        },
      ],
      totalRows: 1,
      viewProsecuted: false,
      currentPage: 1,
      perPage: 2,
      pageOptions: [1, 2, 5, 10, 25, 50, 100, 200],
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
          key: "prohibit",
          label: "Action",
        },
      ],
    };
  },
  watch: {
    viewProsecuted(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Opened prosecuted vehicles list:${newValue}`,
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
    this.viewProsecuted = true;
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
      // this.$router.push("/prohibition");
      // let routeData = this.$router.resolve({name: "routeName", query: {data: "someData"}});
      let routeData = this.$router.resolve({
        name: "Prohibition",
        path: "/prohibition",
        query: { ticketno: "WBT0001" },
      });
      window.open(routeData.href, "_blank");
    },
    /**
     * Load correction memo
     */
    goToLcm() {
      let routeData = this.$router.resolve({
        name: "LCM",
        path: "/lcm",
        query: { ticketno: "WBT0001" },
      });
      window.open(routeData.href, "_blank");
    },
    /**
     * Conditional Load correction memo
     */
    goToClcm() {
      let routeData = this.$router.resolve({
        name: "CLCM",
        path: "/clcm",
        query: { ticketno: "WBT0001" },
      });
      window.open(routeData.href, "_blank");
    },
    /**
     * Charge Sheet Owner
     */
    goToChargesheet() {
      let routeData = this.$router.resolve({
        name: "Owner Charge Sheet",
        path: "/ownerchargesheet",
        query: { ticketno: "WBT0001" },
      });
      window.open(routeData.href, "_blank");
    },
    /**
     * Charge Sheet Driver
     */
    goToChargesheetdriver() {
      let routeData = this.$router.resolve({
        name: "Driver Charge Sheet",
        path: "/chargesheet",
        query: { ticketno: "WBT0001" },
      });
      window.open(routeData.href, "_blank");
    },
    /**
     * EAC WB Certificate
     */
    goToEaccert() {
      let routeData = this.$router.resolve({
        name: "EAC Certificate",
        path: "/eaccert",
        query: { ticketno: "WBT0001" },
      });
      window.open(routeData.href, "_blank");
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
                      <template v-slot:cell(prohibit)="data">
                        <div class="button">
                          <input
                            type="button"
                            variant="primary"
                            class="btn btn-primary waves-effect waves-light"
                            :id="`contacusercheck${data.item.id}`"
                            value="prohibit"
                            v-b-modal.modal-1
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
                  <div class="row">
                    <button
                      class="btn btn-primary waves-effect waves-light col-sm-1"
                      @click="goToProhibition()"
                    >
                      Prohibition
                    </button>
                    <button
                      class="btn btn-primary waves-effect waves-light col-sm-2"
                      @click="goToChargesheet()"
                    >
                      Owner Chargesheet</button
                    >&nbsp;&nbsp;&nbsp;
                    <button
                      class="btn btn-primary waves-effect waves-light col-sm-2"
                      @click="goToChargesheetdriver()"
                    >
                      Driver Chargesheet
                    </button>
                    <button
                      class="btn btn-primary waves-effect waves-light col-sm-2"
                      @click="goToLcm()"
                    >
                      Load Correction Memo</button
                    >&nbsp;&nbsp;&nbsp;
                    <button
                      class="btn btn-primary waves-effect waves-light col-sm-3"
                      @click="goToClcm()"
                    >
                      Conditional Load Correction Memo
                    </button>
                    <button
                      class="btn btn-primary waves-effect waves-light col-sm-2"
                      @click="goToEaccert()"
                    >
                      EAC Certificate
                    </button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </form>
    <b-modal
      id="modal-1"
      title="Case Details for Reg. KBA857V Ticket No. WB0001"
      hide-footer
      size="xl"
      centered
    >
      <div class="card-body">
        <b-tabs content-class="p-3 text-muted">
          <b-tab active class="border-0">
            <template v-slot:title>
              <span class="d-inline-block d-sm-none">
                <i class="fas fa-home"></i>
              </span>
              <span class="d-none d-sm-inline-block">Driver Details</span>
            </template>

            <div class="input-group">
              <div class="input-group-text col-sm-3">
                Driver ID / Passport No.
              </div>
              <input
                type="text"
                class="form-control"
                placeholder="ID"
                id="driverid"
                name="driverid"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Driver Full Names.</div>
              <input
                type="text"
                class="form-control"
                placeholder="Driver Full Names"
                id="dnames"
                name="dnames"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">
                Driver Surname/Father's name:
              </div>
              <input
                type="text"
                class="form-control"
                placeholder="Driver Surname"
                id="dsurname"
                name="dsurname"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Driver Gender:</div>
              <select name="dgender" id="dgender">
                <option value="Male">Male</option>
                <option value="Female">Female</option>
              </select>
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Nationality:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Driver Nationality"
                id="dnationality"
                name="dnationality"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Age:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Driver Age"
                id="dage"
                name="dage"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Driver Address:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Driver Address"
                id="daddress"
                name="daddress"
                required
              />
            </div>
          </b-tab>

          <b-tab>
            <template v-slot:title>
              <span class="d-inline-block d-sm-none">
                <i class="far fa-user"></i>
              </span>
              <span class="d-none d-sm-inline-block">Owners Details</span>
            </template>

            <div class="input-group">
              <div class="input-group-text col-sm-3">
                Owner ID / Passport No.
              </div>
              <input
                type="text"
                class="form-control"
                placeholder="Owners id"
                id="oid"
                name="oid"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Owner Full Names.</div>
              <input
                type="text"
                class="form-control"
                placeholder="Owners Name"
                id="onames"
                name="onames"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">
                Owner Surname/Fathers name.
              </div>
              <input
                type="text"
                class="form-control"
                placeholder="Owners Surname"
                id="osurname"
                name="osurname"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Owner Gender:</div>
              <select name="ogender" id="ogender">
                <option value="Male">Male</option>
                <option value="Female">Female</option>
              </select>
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Owner Nationality:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Owners Nationality"
                id="onationality"
                name="onationality"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Owner Age:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Owners Age"
                id="oage"
                name="oage"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Owner Address:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Owner Address"
                id="oaddress"
                name="oaddress"
                required
              />
            </div>
          </b-tab>
          <b-tab>
            <template v-slot:title>
              <span class="d-inline-block d-sm-none">
                <i class="far fa-envelope"></i>
              </span>
              <span class="d-none d-sm-inline-block">Location Details</span>
            </template>

            <div class="input-group">
              <div class="input-group-text col-sm-3">
                Vehicle Used Along Road:
              </div>
              <input
                type="text"
                class="form-control"
                placeholder="Vehicle Used Along Road"
                id="vroad"
                name="vroad"
                required
              />
            </div>

            <div class="input-group">
              <div class="input-group-text col-sm-3">District</div>
              <input
                type="text"
                class="form-control"
                placeholder="District"
                id="district"
                name="district"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Weighbridge:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Weighbridge"
                id="weighbridge"
                name="weighbridge"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">County:</div>
              <input
                type="text"
                class="form-control"
                placeholder="county"
                id="county"
                name="county"
                required
              />
            </div>
          </b-tab>
          <b-tab>
            <template v-slot:title>
              <span class="d-inline-block d-sm-none">
                <i class="fas fa-cog"></i>
              </span>
              <span class="d-none d-sm-inline-block">Court Details</span>
            </template>

            <div class="input-group">
              <div class="input-group-text col-sm-3">Court:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Court"
                id="court"
                name="court"
                required
              />
            </div>

            <div class="input-group">
              <div class="input-group-text col-sm-3">Complainant (Police):</div>
              <input
                type="text"
                class="form-control"
                placeholder="Complainant (Police)"
                id="police"
                name="police"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">
                Police / Traffic Officer:
              </div>
              <multiselect
                v-model="tpolice"
                :options="tpolices"
                class="form-control"
              ></multiselect>
            </div>
          </b-tab>
          <b-tab>
            <template v-slot:title>
              <span class="d-inline-block d-sm-none">
                <i class="fas fa-cog"></i>
              </span>
              <span class="d-none d-sm-inline-block">Prohibition Details</span>
            </template>

            <div class="input-group">
              <div class="input-group-text col-sm-3">Vehicle Inspector:</div>
              <multiselect
                v-model="vi"
                :options="VIs"
                class="form-control"
              ></multiselect>
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Move Vehicle to:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Move Vehicle to"
                id="moveto"
                name="moveto"
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Repair's Needed:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Repair's Needed"
                id="rneeded"
                name="rneeded"
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">
                Offload Excess to Reg. No.:
              </div>
              <input
                type="text"
                class="form-control"
                placeholder="Offload Excess to Reg. No."
                id="offload"
                name="offload"
              />
            </div>
          </b-tab>
          <b-tab>
            <template v-slot:title>
              <span class="d-inline-block d-sm-none">
                <i class="fas fa-cog"></i>
              </span>
              <span class="d-none d-sm-inline-block"
                >Transporter's Details</span
              >
            </template>

            <div class="input-group">
              <div class="input-group-text col-sm-3">Transporter Names:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Transporter Names"
                id="Tnames"
                name="Tnames"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Transporter Address:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Transporter Address"
                id="Taddress"
                name="Taddress"
                required
              />
            </div>
          </b-tab>
        </b-tabs>

        <form>
          <div class="row">
            <div class="col-lg-12">
              <div class="text-end">
                <button type="submit" class="btn btn-primary">Submit</button
                >&nbsp;&nbsp;&nbsp;
              </div>
            </div>
          </div>
        </form>
        <div class="row"></div>
      </div>
    </b-modal>
  </Layout>
</template>
<style scoped>
.changebg {
  background-color: #f7f6ebfb;
}
</style>