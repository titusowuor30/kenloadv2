<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

//import { tableData } from "./dataAdvancedtable";

export default {
  page: {
    title: "Prohibit",
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
      title: "Prohibit Vehicle Not Weighed in System",
      items: [
        {
          text: "USER: COP",
        },
        {
          text: "Prohibit Vehicle Not Weighed in System",
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
          prosecute: 0,
          status: "prohibited",
        },
        {
          ticketno: "WBT0002",
          registration: "KAA445",
          date: "2019/07/13",
          GVW: "49000",
          excess: 590,
          prosecute: 0,
          status: "prohibited",
        },
        {
          ticketno: "WBT0003",
          registration: "KAD444",
          date: "2019/04/08",
          GVW: "19000",
          excess: 520,
          prosecute: 0,
          status: "prohibited",
        },
        {
          ticketno: "WBT0004",
          registration: "KCY190",
          date: "2018/12/14",
          GVW: "28000",
          excess: 500,
          prosecute: 0,
          status: "unpaid",
        },
        {
          ticketno: "WBT0005",
          registration: "KCA190",
          date: "2019/06/19",
          GVW: "19000",
          excess: 46,
          prosecute: 0,
          status: "prohibited",
        },
      ],
      totalRows: 1,
      currentPage: 1,
      perPage: 2,
      pageOptions: [1, 2, 5, 10, 25, 50, 100],
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
          key: "prosecute",
          label: "Prosecute",
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
      <div class="card-body">
        <b-tabs content-class="p-3 text-muted">
          <b-tab>
            <template v-slot:title>
              <span class="d-inline-block d-sm-none">
                <i class="fas fa-cog"></i>
              </span>
              <span class="d-none d-sm-inline-block"
                >Vehicle Details</span
              >
            </template>

            <div class="input-group">
              <div class="input-group-text col-sm-3">Registration Number:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Registration Number"
                id="Tnames"
                name="Tnames"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Trailer Reg. No:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Trailer Reg. No"
                id="Taddress"
                name="Taddress"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Vehicle Make:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Vehicle Make"
                id="Taddress"
                name="Taddress"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Axle Type:</div>
              <select name="dgender" id="dgender">
                <option value="Male">2A</option>
                <option value="Female">3A</option>
                <option value="Male">3C</option>
                <option value="Female">4A</option>
                <option value="Male">4D</option>
                <option value="Female">5C</option>
                <option value="Male">6C</option>
                <option value="Female">7A</option>
              </select>
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Prohibition Reason:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Prohibition Reason"
                id="Taddress"
                name="Taddress"
                required
              />
            </div>
          </b-tab>
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
              <span class="d-none d-sm-inline-block">Prohibition Details</span>
            </template>

            <div class="input-group">
              <div class="input-group-text col-sm-3">Vehicle Inspector:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Vehicle Inspector"
                id="vi"
                name="vi"
              />
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
                <b-button v-on:click="goToProhibition()">
                        Generate Prohibition Order</b-button>
              </div>
            </div>
          </div>
        </form>
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