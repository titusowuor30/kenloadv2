<script
  type="text/javascript"
  src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/1.0.2/Chart.min.js"
></script>
<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import jsPDF from "jspdf";

import DatePicker from "vue2-datepicker";
import Multiselect from "vue-multiselect";

import axios from "axios";
import Swal from "sweetalert2";

import reportdet from "@/components/report/header";

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
    DatePicker,
    Multiselect,
    reportdet,
  },
  data() {
    return {
      hour: "",
      //watch user logs
      viewReports: false,
      printedpdf: true,
      reportfor: "",
      genReportStarted: "",
      printReport: false,

      rpt: "",
      filter: "",
      hourlydata: "",
      headers: null,
      uniqueCars: null,
      showme: true,
      pl: "",
      orderData: [],
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
      exceldata: [],
      route: "",
      routes: ["Nairobi-Mombasa", "Mombasa-Nairobi"],
      hswim: false,
      showthreshold: false,
      threshold: 3000,
      stationshowdata: true,
      showdata: false,
      censusshowdata: false,
      showtickets: false,
      showhour: false,
      showdate: false,
      autoClose: "",
      autoClose2: "",
      mail_checkbox: "",
      day_checkbox: "",
      week_checkbox: "",
      month_checkbox: "",
      year_checkbox: "",
      specify_checkbox: "",
      time_checkbox: "",
      reg_checkbox: "",
      ticket_checkbox: "",
      status_checkbox: "",
      wb_checkbox: "",
      user_checkbox: "",
      transporter_checkbox: "",
      shift_checkbox: "",
      axle_checkbox: "",
      cargo_checkbox: "",
      report: null,
      mod: "",
      picked: "",
      modules: [
        "All",
        "Weighing",
        "Prosecution",
        "Security",
        "Census",
        "Vehicle Inspection",
        "Data",
        "Techinical",
        "HSWIM",
      ],
      weighing: [
        "Axle Configurations",
        "Daily Hour Data",
        "Daily Hour Statistics",
        "Daily Monthly Statistics",
        "Daily & Hourly Register",
        "Daily Summary",
        "Manual Weighing",
        "Overloaded",
        "Re Weighed Statement",
        "Overloaded Above Allowance",
        "Scale Test",
        "Static Configuration",
        "Time Turn Report",
        "Transporter Statement",
        "Vehicle",
        "Weighbridge Picture Tickets",
        "Weighbridge Register",
        "Weighbridge Register Compressed",
        "Weighbridge Tickets",
        "Wide Load",
        "Weighbridge Status Report",
        "Special Release",
        "Excess GVW & Axle",
        "Excess Per Axle",
        "Vehicle Weighed per Axle",
        "Overloaded and Chargeed Per Configuration",
      ],
      hswim: [
        "HSWIM Configuration",
        "HSWIM List",
        "HSWIM Register",
        "Static vs HSWIM",
        "HSWIM Hourly",
        "HSWIM vs Scale Evaluation",
      ],
      prosecution: [
        "Impounded And Prohibited",
        "Impounded And Overloaded",
        "Prosecution Process List",
        "Prosecution Court Fine List",
        "Daily Charged",
        "Payment List",
        "Habitual Offenders",
      ],
      security: ["Shifts"],
      census: [],
      vehicleinspection: ["Wide Load", "Heavy Vehicle Inspection"],
      data: [],
      technical: [],
      reports: [
        "Auto Generate Reports",
        "Axle Configurations",
        "Census Report",
        "Daily Hour Data",
        "Daily Hour Statistics",
        "Daily Monthly Statistics",
        "Daily & Hourly Register",
        "Daily Summary",
        "Impounded And Prohibited",
        "Impounded And Overloaded",
        "Manual Weighing",
        "Overloaded",
        "Prosecution Process List",
        "Prosecution Court Fine List",
        "Re Weighed Statement",
        "Overloaded Above Allowance",
        "Shifts",
        "Scale Test",
        "Static Configuration",
        "Time Turn Report",
        "Transporter Statement",
        "Vehicle",
        "Weighbridge Picture Tickets",
        "Weighbridge Register",
        "Weighbridge Picture",
        "Weighbridge Register Compressed",
        "Weighbridge Tickets",
        "Wide Load",
        "Heavy Vehicle Inspection",
        "HSWIM Configuration",
        "HSWIM Register",
        "Static vs HSWIM",
        "HSWIM vs Scale Evaluation",
        "Tagged",
        "Weighbridge Status Report",
        "Special Release",
        "Excess GVW & Axle",
        "Excess Per Axle",
        "Vehicle Weighed per Axle",
        "Overloaded and Chargeed Per Configuration",
        "Daily Charged",
        "Payment List",
        "Habitual Offenders",
      ],
      reg: [],
      regs: [],
      fromdate: new Date(),
      todate: new Date(),
      ticket: null,
      tickets: [
        "SR00001",
        "SR00002",
        "SR00003",
        "SR00004",
        "SR00005",
        "SR00006",
        "SR00007",
        "SR00008",
        "SR00009",
        "SR000010",
        "SR000011",
        "SR000012",
        "SR000013",
        "SR000014",
        "SR000015",
      ],
      status: [],
      statuses: ["ALL", "Legal", "Overloaded", "Warned"],
      state: [],
      states: [
        "ALL",
        "Charged",
        "Redistributed",
        "Special Release",
        "To Redistribute",
      ],
      email: "",
      deck: [],
      decks: ["A", "B"],
      wb: [],
      wbs: [],
      user: null,
      users: [],
      transporter: [],
      transporters: [],
      shift: null,
      shifts: ["Day Shift", "Night Shift"],
      axle: null,
      axles: [],
      cargo: [],
      shifts: [],
      cargos: [
        "Petrol",
        "Cement",
        "Livestock",
        "Sand",
        "Ballast",
        "Maize",
        "Grains",
        "Rice",
        "Others",
      ],
      lanes: ["ALL", "WIM", "LANE1", "LANE2", "LANE3"],
      lane: "",
      fromhour: "00:00",
      hours: [
        "00:00",
        "01:00",
        "02:00",
        "03:00",
        "04:00",
        "05:00",
        "06:00",
        "07:00",
        "08:00",
        "09:00",
        "10:00",
        "11:00",
        "12:00",
        "13:00",
        "14:00",
        "15:00",
        "16:00",
        "17:00",
        "18:00",
        "19:00",
        "20:00",
        "21:00",
        "22:00",
        "23:00",
      ],

      tohour: "23:59",
    };
  },
  watch: {
    viewReports(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Opened Reports page:${newValue}`,
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
    printReport() {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Reporting:${this.genReportStarted}`,
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
    // Set the initial number of items
    this.email = window.$userid + ",";
    this.updatefields();
    this.totalRows = this.items.length;
    this.viewReports = true;
  },
  methods: {
    getcurrentdate(mydate) {
      let d = new Date(mydate);
      let year = d.getFullYear();
      let month = this.getmonth(d.getMonth() + 1);
      let date = d.getDate();
      date = this.getv(date);
      //month = this.getv(month);

      let hour = d.getHours();
      let min = d.getMinutes();
      let sec = d.getSeconds();
      hour = this.getv(hour);
      min = this.getv(min);
      sec = this.getv(sec);

      //const msec = d.getMilliseconds();
      const datetime =
        date + "/" + month + "/" + year + " " + hour + ":" + min + ":" + sec;
      return datetime;
    },
    updatefields() {
      axios
        .get(window.$http + `Transporters?limit=1000000000`, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          // this.cargos = response.data.cargo_name;
          response.data.forEach((e) => {
            this.transporters.push(e.transporter_name);
          });
          axios
            .get(window.$http + `Shifts`, { headers: window.$headers })
            .then((response) => {
              this.shifts = response.data;
            });
          Swal.close();
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Transporters" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });

      axios
        .get(window.$http + `users`, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          // this.cargos = response.data.cargo_name;
          response.data.forEach((e) => {
            this.users.push(e.email);
          });

          Swal.close();
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Transporters" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });

      axios
        .get(window.$http + `ClusterWeighbridges`, {
          headers: window.$headers,
        })
        .then((response) => {
          this.cluster = response.data;

          response.data.forEach((e) => {
            this.wbs.push(e.cluster_name);
          });

          Swal.close();
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Transporters" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });

      axios
        .get(window.$http + `cargos`, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          // this.cargos = response.data.cargo_name;
          response.data.forEach((e) => {
            this.cargos.push(e.cargo_name);
          });

          Swal.close();
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Cargo" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });

      axios
        .get(window.$http + `vehicles?limit=1000000000`, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          // this.cargos = response.data.cargo_name;
          response.data.forEach((e) => {
            this.regs.push(e.vehicle_reg);
          });

          Swal.close();
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "vehicle" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
      // Axle Config

      axios
        .get(window.$http + `AxleWeightConfig`, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          // this.cargos = response.data.cargo_name;
          response.data.forEach((e) => {
            this.axles.push(e.axle_code);
          });

          Swal.close();
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Axle" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
      this.updateroutes();
    },
    updateroutes() {
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
      this.routes = [];
      axios
        .get(window.$http + `routes`, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          //this.routes = response.data;
          //console.log(this.orderData);
          response.data.forEach((e) => {
            this.routes.push(e.route_name);
          });

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
    censusdata() {
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
      //alert(window.$tokenString);
      //alert(window.$http + `census/search?` + this.filter);
      axios
        .get(
          window.$http +
            `census/search?` +
            this.filter +
            "&limit=1000000000&deletestatus=0",
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          this.censusreport();
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
    formatDate(date) {
      var d = new Date(date),
        month = "" + (d.getMonth() + 1),
        day = "" + d.getDate(),
        year = d.getFullYear();

      if (month.length < 2) month = "0" + month;
      if (day.length < 2) day = "0" + day;

      return [year, month, day].join("-");
    },
    gethabitualoffenders() {
      const urlString =
        "WeighbridgeTransactions/report/habitualOffenders?" + this.filter;
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      console.log(this.filter);

      axios
        .get(window.$http + urlString, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.

          this.orderData = response.data;
          var no = 9;
          var headerrpt = new Array(no);
          var headerspace = new Array(no);

          headerrpt[0] = "NO";
          headerrpt[1] = "Transporter";
          headerrpt[2] = " Address";
          headerrpt[3] = "Overloaded";
          headerrpt[4] = "Charged";
          headerrpt[5] = "Cleared";

          headerspace[0] = 5; //2
          headerspace[1] = 10; //7
          headerspace[2] = 10; //7
          headerspace[3] = 5; //9
          headerspace[4] = 5; //7
          headerspace[5] = 5; //7

          /*          {
    "hourDate": "11/20/2021 2:00:00 AM",
    "hourTime": "11/20/2021 2:00:00 AM",
    "multideck": "0",
    "singleAxle": "0",
    "manually": null,
    "hswim": "0",
    "calledIn": null,
    "totalWeighed": "0"
  },*/

          const data = this.orderData.map((row) => ({
            NO: row.itemNo,
            Transporter: row.transporterName,
            Address: row.transporteradd,
            Overloaded: row.overLoaded,
            Charged: row.charged,
            Cleared: row.cleared,
          }));
          this.orderData = data;
          this.pl = "l"; //"p" for portrait
          this.datareport(headerrpt, headerspace, data);

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
      this.report = "Habitual Offenders";
    },

    getexcessperaxle(threshold) {
      const urlString =
        "WeighbridgeTransactions/report/weighedunderthreshold?" +
        this.filter +
        "&threshold=" +
        threshold;
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      console.log(this.filter);

      axios
        .get(window.$http + urlString, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          var no = 9;
          var headerrpt = new Array(no);
          var headerspace = new Array(no);

          headerrpt[0] = "NO";
          headerrpt[1] = "Registration";
          headerrpt[2] = "Axle Configuration";
          headerrpt[3] = "Transporter";
          headerrpt[4] = "Excess Axle";
          headerrpt[5] = "Excess GVW";

          headerspace[0] = 3; //2
          headerspace[1] = 3; //7
          headerspace[2] = 3; //7
          headerspace[3] = 21; //9
          headerspace[4] = 5; //7
          headerspace[5] = 5; //7

          /*          {
    "hourDate": "11/20/2021 2:00:00 AM",
    "hourTime": "11/20/2021 2:00:00 AM",
    "multideck": "0",
    "singleAxle": "0",
    "manually": null,
    "hswim": "0",
    "calledIn": null,
    "totalWeighed": "0"
  },*/

          const data = this.orderData.map((row) => ({
            NO: row.itemNo,
            Registration: row.veh_reg,
            "Axle Configuration": row.axleConfiguration,
            Transporter: row.transporter,
            "Excess Axle": row.excessAxle,
            "Excess GVW": row.gvwLoad,
          }));
          this.orderData = data;
          this.pl = "l"; //"p" for portrait
          this.datareport(headerrpt, headerspace, data);

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
      alert(this.threshold);
      this.report = "Excess Per Axle From" + " " + threshold + "KG";
    },

    getvehicleperaxle() {
      const urlString =
        "WeighbridgeTransactions/report/vehicleweighedperaxle?" + this.filter;
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      console.log(this.filter);

      axios
        .get(window.$http + urlString, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          var no = 9;
          var headerrpt = new Array(no);
          var headerspace = new Array(no);

          headerrpt[0] = "NO";
          headerrpt[1] = "Axle Configuration";
          headerrpt[2] = "NO Of Records";

          headerspace[0] = 4; //2
          headerspace[1] = 18; //7
          headerspace[2] = 18; //9

          /*          {
    "hourDate": "11/20/2021 2:00:00 AM",
    "hourTime": "11/20/2021 2:00:00 AM",
    "multideck": "0",
    "singleAxle": "0",
    "manually": null,
    "hswim": "0",
    "calledIn": null,
    "totalWeighed": "0"
  },*/

          const data = this.orderData.map((row) => ({
            NO: row.itemNo,
            "Axle Configuration": row.axleConfig,
            "NO Of Records": row.divertedPerAxle,
          }));
          this.orderData = data;
          this.pl = "l"; //"p" for portrait
          this.datareport(headerrpt, headerspace, data);

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
    getcourtfine() {
      const urlString = "CaseResults/report/courtfines?" + this.filter;
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      console.log(this.filter);

      axios
        .get(window.$http + urlString, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          var no = 9;
          var headerrpt = new Array(no);
          var headerspace = new Array(no);

          headerrpt[0] = "Payment Date";
          headerrpt[1] = "Registration";
          headerrpt[2] = "Transporter Name";
          headerrpt[3] = "Axle";
          headerrpt[4] = "Date Charged";
          headerrpt[5] = "Excess Axle";
          headerrpt[6] = "Excess GVW";
          headerrpt[7] = "Owner Charge";
          headerrpt[8] = "Driver Charge";
          headerrpt[9] = "Transporter Charge";
          headerrpt[10] = "Parking Charge";
          headerrpt[11] = "Total Charge";
          headerrpt[12] = "Receipts";
          headerrpt[13] = "WBT-NO";
          headerrpt[14] = "ProhibitionOrder";

          headerspace[0] = 3; //2
          headerspace[1] = 3; //7
          headerspace[2] = 3; //9
          headerspace[3] = 2; //13
          headerspace[4] = 3; //16
          headerspace[5] = 2; //19
          headerspace[6] = 2; //22
          headerspace[7] = 2; //25
          headerspace[8] = 2; //31
          headerspace[9] = 2; //22
          headerspace[10] = 2; //25
          headerspace[11] = 2; //31
          headerspace[12] = 3; //31
          headerspace[13] = 4; //31
          headerspace[14] = 6; //31

          /*          {
    "hourDate": "11/20/2021 2:00:00 AM",
    "hourTime": "11/20/2021 2:00:00 AM",
    "multideck": "0",
    "singleAxle": "0",
    "manually": null,
    "hswim": "0",
    "calledIn": null,
    "totalWeighed": "0"
  },*/

          const data = this.orderData.map((row) => ({
            "Payment Date": row.paymentDate,
            NOPlate: row.noPlate,
            "Transporter Name": row.transporter,
            Axle: row.axle,
            "Date Charged": row.dateCharged,
            "Excess Axle": row.excessAxle,
            "Excess GVW": row.excessGVW,
            "Owner Charge": row.ownerCharge,
            "Driver Charge": row.driverCharge,
            "Transporter Charge": row.transporterCharge,
            "Parking Charge": row.parkingCharge,
            "Total Charge": row.totalCharge,
            Receipts: row.receiptNo,
            "WBT-NO": row.wbtTicketNo,
            ProhibitionOrder: row.prohibitionOrderNo,
          }));
          this.orderData = data;
          this.pl = "l"; //"p" for portrait
          this.datareport(headerrpt, headerspace, data);

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
    gethourlydata() {
      const urlString =
        "WeighbridgeTransactions/report/hourstatistics?" + this.filter; //PoliceReports/DailyHour
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      console.log(this.filter);

      //alert(window.$http + `weighbridgetransactions/search?` + this.filter);
      axios
        .get(window.$http + urlString, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          var no = 9;
          var headerrpt = new Array(no);
          var headerspace = new Array(no);

          headerrpt[0] = "No.";
          headerrpt[1] = "Date";
          headerrpt[2] = "Time";
          headerrpt[3] = "MultiDeck[D]";
          headerrpt[4] = "SingleAxle[S]";
          headerrpt[5] = "Manually[M]";
          headerrpt[6] = "HSWIM Total[H]";
          headerrpt[7] = "HSWIM[Called In]Q=H-C";
          headerrpt[8] = "Total Weighed x=[D+S+M+Q]";

          headerspace[0] = 4; //2
          headerspace[1] = 4; //7
          headerspace[2] = 4; //9
          headerspace[3] = 4; //13
          headerspace[4] = 4; //16
          headerspace[5] = 5; //19
          headerspace[6] = 5; //22
          headerspace[7] = 5; //25
          headerspace[8] = 5; //31

          /*          {
    "hourDate": "11/20/2021 2:00:00 AM",
    "hourTime": "11/20/2021 2:00:00 AM",
    "multideck": "0",
    "singleAxle": "0",
    "manually": null,
    "hswim": "0",
    "calledIn": null,
    "totalWeighed": "0"
  },*/

          const data = this.orderData.map((row) => ({
            "No.": row.id,
            Date: row.hourDate,
            Time: row.hourTime + "00",
            "MultiDeck[D]": row.multideck,
            "SingleAxle[S]": row.singleAxle,
            "Manually[M]": row.manually,
            "HSWIM Total[H]": row.hswim,
            "HSWIM[Called In]Q=H-C": row.calledIn,
            "Total Weighed x=[D+S+M+Q]": row.totalWeighed,
          }));
          this.orderData = data;
          this.pl = "p"; //"p" for portrait
          this.datareport(headerrpt, headerspace, data);

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
    getimpoundedandoverloaded() {
      const urlString =
        "CaseDetails/report/impoundedAndOverloaded?" + this.filter;
      //alert(urlString);
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      console.log(this.filter);

      //alert(window.$http + `weighbridgetransactions/search?` + this.filter);
      axios
        .get(window.$http + urlString, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          var no = 23;
          var headerrpt = new Array(no);
          var headerspace = new Array(no);

          headerrpt[0] = "No.";
          headerrpt[1] = "Date Weighed";
          headerrpt[2] = "Last Date Weighed";
          headerrpt[3] = "Time Taken";
          headerrpt[4] = "TicketNo";
          headerrpt[5] = "Transporter";
          headerrpt[6] = "Registration";
          headerrpt[7] = "Axle Config";
          headerrpt[8] = "Last Axle Config";
          headerrpt[9] = "Cargo";
          headerrpt[10] = "Source";
          headerrpt[11] = "Destination";
          headerrpt[12] = "Axle Overload";
          headerrpt[13] = "GVW Overload";
          headerrpt[14] = "Computer Operator";
          headerrpt[15] = "Status";
          headerrpt[16] = "No. of Re-Weigh";

          headerrpt[17] = "Last Axle Overload";
          headerrpt[18] = "Last GVW Overload";
          headerrpt[19] = "Last Computer Operator";
          headerrpt[20] = "State";

          headerrpt[21] = "Last State";
          headerrpt[22] = "Reason";

          headerspace[0] = 2; //2
          headerspace[1] = 2; //7
          headerspace[2] = 2; //2
          headerspace[3] = 2; //13
          headerspace[4] = 1; //16 //////////must total to 40
          headerspace[5] = 2; //19
          headerspace[6] = 2; //22
          headerspace[7] = 2; //25
          headerspace[8] = 2; //31
          headerspace[9] = 2; //2
          headerspace[10] = 2; //7
          headerspace[11] = 2; //7
          headerspace[12] = 2; //9
          headerspace[13] = 3; //13
          headerspace[14] = 2; //16
          headerspace[15] = 2; //19
          headerspace[16] = 2; //19
          headerspace[17] = 1; //19
          headerspace[18] = 1; //19
          headerspace[19] = 1; //19
          headerspace[20] = 1; //19
          headerspace[21] = 1; //19
          headerspace[22] = 1; //19

          /*          {
    "hourDate": "11/20/2021 2:00:00 AM",
    "hourTime": "11/20/2021 2:00:00 AM",
    "multideck": "0",
    "singleAxle": "0",
    "manually": null,
    "hswim": "0",
    "calledIn": null,
    "totalWeighed": "0"
  },*/

          const data = this.orderData.map((row) => ({
            "No.": row.id,
            DateWeighed: row.dateWeighed.replace("T", " "),
            lastdateWeighed: row.lastdateWeighed.replace("T", " "),
            TimeTaken: row.timeDiff,
            TicketNo: row.ticketNO,
            Transporter: row.transporter,
            VehicleReg: row.regNo,
            AxleConfig: row.axleConfig,
            LastAxleConfig: row.lastaxleConfig,

            Cargo: row.cargo,
            Source: row.destination,
            Destination: row.source,
            AxleOverload: row.axleoverload,
            GVWOverload: row.gvwaxleload,
            ComputerOperator: row.computerOperator,
            Status: row.status,
            "No. of Re-Weigh": row.reweighno,

            LastAxleOverload: row.lastaxleoverload,
            LastGVWOverload: row.lastgvwaxleload,
            LastComputerOperator: row.computerOperator,
            state: row.state,
            LastState: row.laststate,
            Vardict: row.vardict,
          }));
          this.orderData = data;
          this.pl = "l"; //"p" for portrait
          this.datareport(headerrpt, headerspace, data);

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
    getimpoundedandprohibited() {
      const urlString =
        "CaseDetails/report/impoundedAndProhibited?limit=1000000000&" +
        this.filter;
      //alert(urlString);
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      console.log(this.filter);

      //alert(window.$http + `weighbridgetransactions/search?` + this.filter);
      axios
        .get(window.$http + urlString, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          var no = 16;
          var headerrpt = new Array(no);
          var headerspace = new Array(no);

          headerrpt[0] = "No.";
          headerrpt[1] = "Date Weighed";
          headerrpt[2] = "Date Prohibited";
          headerrpt[3] = "Time Taken";
          headerrpt[4] = "TicketNo";
          headerrpt[5] = "Transporter";
          headerrpt[6] = "Registration";
          headerrpt[7] = "Axle Config";
          headerrpt[8] = "Cargo";
          headerrpt[9] = "Source";
          headerrpt[10] = "Destination";
          headerrpt[11] = "Axle Overload";
          headerrpt[12] = "GVW Overload";
          headerrpt[13] = "Status";
          headerrpt[14] = "Prohibition Order";
          headerrpt[15] = "Prosecutor";
          headerrpt[16] = "Computer Operator";

          headerspace[0] = 2; //2
          headerspace[1] = 2; //7
          headerspace[2] = 2; //9
          headerspace[3] = 2; //13
          headerspace[4] = 3; //16 //////////must total to 40
          headerspace[5] = 4; //19
          headerspace[6] = 3; //22
          headerspace[7] = 2; //25
          headerspace[8] = 2; //31
          headerspace[9] = 2; //2
          headerspace[10] = 2; //7
          headerspace[11] = 3; //7
          headerspace[12] = 2; //9
          headerspace[13] = 3; //13
          headerspace[14] = 3; //16
          headerspace[15] = 3; //19

          /*          {
    "hourDate": "11/20/2021 2:00:00 AM",
    "hourTime": "11/20/2021 2:00:00 AM",
    "multideck": "0",
    "singleAxle": "0",
    "manually": null,
    "hswim": "0",
    "calledIn": null,
    "totalWeighed": "0"
  },*/

          const data = this.orderData.map((row) => ({
            "No.": row.id,
            DateWeighed: row.dateWeighed,
            DateProhibited: row.dateProhibited,
            TimeTaken: row.timeDiff,
            TicketNo: row.ticketNO,
            Transporter: row.transporter,
            VehicleReg: row.regNo,
            AxleConfig: row.axleConfig,
            Cargo: row.cargo,
            Source: row.destination,
            Destination: row.source,
            AxleOverload: row.axleoverload,
            GVWOverload: row.gvwaxleload,
            Status: row.status,
            ProhibitionOrder: row.prohibitionorder,
            Prosecutor: row.prosecutor,
            ComputerOperator: row.computerOperator,
          }));
          this.orderData = data;
          this.pl = "l"; //"p" for portrait
          this.datareport(headerrpt, headerspace, data);

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
    gethswimregister() {
      const urlString =
        "virtualtickets/search?wim=W1&limit=1000000000&" + this.filter;
      //alert(urlString);
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      console.log(this.filter);

      //alert(window.$http + `weighbridgetransactions/search?` + this.filter);
      axios
        .get(window.$http + urlString, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          var no = 15;
          var headerrpt = new Array(no);
          var headerspace = new Array(no);

          headerrpt[0] = "No.";
          headerrpt[1] = "Ticket No.";
          headerrpt[2] = "Date Time";
          headerrpt[3] = "HSWIM Conf";
          headerrpt[4] = "System Conf";
          headerrpt[5] = "Registration";
          headerrpt[6] = "Axle 1";
          headerrpt[7] = "Axle 2";
          headerrpt[8] = "Axle 3";
          headerrpt[9] = "Axle 4";
          headerrpt[10] = "Axle 5";
          headerrpt[11] = "Axle 6";
          headerrpt[12] = "Axle 7";
          headerrpt[13] = "Axle 8";
          headerrpt[14] = "Axle 9";
          headerrpt[15] = "Permissible GVW";
          headerrpt[16] = "HSWIM GVW";
          headerrpt[17] = "Called In Reason";
          headerrpt[18] = "Called In";

          headerspace[0] = 2; //2
          headerspace[1] = 2; //7
          headerspace[2] = 4; //9
          headerspace[3] = 2; //13
          headerspace[4] = 2; //16
          headerspace[5] = 4; //19
          headerspace[6] = 2; //22
          headerspace[7] = 2; //25
          headerspace[8] = 2; //31
          headerspace[9] = 2; //19
          headerspace[10] = 2; //22
          headerspace[11] = 2; //25
          headerspace[12] = 2; //25
          headerspace[13] = 2; //31
          headerspace[14] = 2; //31
          headerspace[15] = 2; //31
          headerspace[16] = 2; //31
          headerspace[17] = 2; //31
          headerspace[18] = 2; //31
          /*          {
    "hourDate": "11/20/2021 2:00:00 AM",
    "hourTime": "11/20/2021 2:00:00 AM",
    "multideck": "0",
    "singleAxle": "0",
    "manually": null,
    "hswim": "0",
    "calledIn": null,
    "totalWeighed": "0"
  },*/
          var id = 1;
          const data = this.orderData.map((row) => ({
            "ID.": id++,
            "HSWIM Ticket No.": row.cid,
            "HSWIM Date Time": row.date_time_local.replace("T", " "),
            "HSWIM Vehicle Class": row.vehicle_class,
            "HSWIM Axle Conf": row.axle_conf,
            "HSWIM Registration": row.lp,
            "Axle 1": row.axle_1_load,
            "Axle 2": row.axle_2_load,
            "Axle 3": row.axle_3_load,
            "Axle 4": row.axle_4_load,
            "Axle 5": row.axle_5_load,
            "Axle 6": row.axle_6_load,
            "Axle 7": row.axle_7_load,
            "Axle 8": row.axle_8_load,
            "Axle 9": row.axle_9_load,
            "Permissible GVW":
              row.Axl_Legal_Weight_1 +
              row.Axl_Legal_Weight_2 +
              row.Axl_Legal_Weight_3 +
              row.Axl_Legal_Weight_4 +
              row.Axl_Legal_Weight_5 +
              row.Axl_Legal_Weight_6 +
              row.Axl_Legal_Weight_7 +
              row.Axl_Legal_Weight_8 +
              row.Axl_Legal_Weight_9,
            "HSWIM GVW":
              row.axle_1_load +
              row.axle_2_load +
              row.axle_3_load +
              row.axle_4_load +
              row.axle_5_load +
              row.axle_6_load +
              row.axle_7_load +
              row.axle_8_load +
              row.axle_9_load,
            "Called In Reason": row.calledinreason,
            "Called In": row.calledin,
          }));
          this.orderData = data;
          this.pl = "l"; //"p" for portrait
          this.datareport(headerrpt, headerspace, data);

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
    gethswimconfig() {
      const urlString =
        "virtualtickets/report/vehicleweighedperaxle?wim=W1&limit=1000000000&" +
        this.filter;
      //alert(urlString);
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      console.log(this.filter);

      //alert(window.$http + `weighbridgetransactions/search?` + this.filter);
      axios
        .get(window.$http + urlString, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          var no = 15;
          var headerrpt = new Array(no);
          var headerspace = new Array(no);

          headerrpt[0] = "No.";
          headerrpt[1] = "Configuration";
          headerrpt[2] = "Total Weighed";
          headerrpt[3] = "Total Called In";

          headerspace[0] = 10; //2
          headerspace[1] = 10; //7
          headerspace[2] = 10; //9
          headerspace[3] = 10; //13
          /*          {
    "hourDate": "11/20/2021 2:00:00 AM",
    "hourTime": "11/20/2021 2:00:00 AM",
    "multideck": "0",
    "singleAxle": "0",
    "manually": null,
    "hswim": "0",
    "calledIn": null,
    "totalWeighed": "0"
  },*/
          var id = 1;
          const data = this.orderData.map((row) => ({
            "ID.": id++,
            Configuration: row.axleConfig,
            "Total Weighed": row.weighedPerAxle,
            "Total Called In": row.calledinPerAxle,
          }));
          this.orderData = data;
          this.pl = "p"; //"p" for portrait
          this.datareport(headerrpt, headerspace, data);

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
    getstaticvshswimeva() {
      const urlString =
        "WeighbridgeTransactions/report/staticvshswim?wim=W1&calledin=1&" +
        this.filter;
      //alert(urlString);
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      console.log(this.filter);

      //alert(window.$http + `weighbridgetransactions/search?` + this.filter);
      axios
        .get(window.$http + urlString, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          var no = 15;
          var headerrpt = new Array(no);
          var headerspace = new Array(no);

          headerrpt[0] = "No.";
          headerrpt[1] = "Ticket No.";
          headerrpt[2] = "Date Time";
          headerrpt[3] = "HSWIM Conf";
          headerrpt[4] = "System Conf";
          headerrpt[5] = "Registration";
          headerrpt[6] = "Axle 1";
          headerrpt[7] = "Axle 2";
          headerrpt[8] = "Axle 3";
          headerrpt[9] = "Axle 4";
          headerrpt[10] = "Axle 5";
          headerrpt[11] = "Axle 6";
          headerrpt[12] = "Axle 7";
          headerrpt[13] = "Axle 8";
          headerrpt[14] = "Axle 9";
          headerrpt[15] = "Called In";
          headerrpt[16] = "Weighbridge Date";
          headerrpt[17] = "Ticket No.";
          headerrpt[18] = "Station";
          headerrpt[19] = "Source";
          headerrpt[20] = "Destination";
          headerrpt[21] = "Cargo";
          headerrpt[22] = "Group 1";
          headerrpt[23] = "Group 2";
          headerrpt[24] = "Group 3";
          headerrpt[25] = "Group 4";
          headerrpt[26] = "Status";
          headerrpt[27] = "Deck GVW";
          headerrpt[28] = "HSWIM GVW";

          headerspace[0] = 1; //2
          headerspace[1] = 1; //7
          headerspace[2] = 1; //9
          headerspace[3] = 1; //13
          headerspace[4] = 1; //16
          headerspace[5] = 1; //19
          headerspace[6] = 1; //22
          headerspace[7] = 1; //25
          headerspace[8] = 1; //31
          headerspace[9] = 1; //19
          headerspace[10] = 2; //22
          headerspace[11] = 2; //25
          headerspace[12] = 2; //25
          headerspace[13] = 2; //31
          headerspace[14] = 2; //31
          headerspace[15] = 1; //31
          headerspace[16] = 1; //31
          headerspace[17] = 1; //31
          headerspace[18] = 1; //31
          headerspace[19] = 1; //31
          headerspace[20] = 1; //31
          headerspace[21] = 1; //31
          headerspace[22] = 1; //31
          headerspace[23] = 1; //31
          headerspace[24] = 1; //31
          headerspace[25] = 1; //31
          headerspace[26] = 1; //31
          headerspace[27] = 1; //31
          headerspace[28] = 1; //31

          /*          {
    "hourDate": "11/20/2021 2:00:00 AM",
    "hourTime": "11/20/2021 2:00:00 AM",
    "multideck": "0",
    "singleAxle": "0",
    "manually": null,
    "hswim": "0",
    "calledIn": null,
    "totalWeighed": "0"
  },*/
          var id = 1;
          const data = this.orderData.map((row) => ({
            "ID.": id++,
            "HSWIM Ticket No.": row.cid,
            "HSWIM Date Time": row.date_time_local.replace("T", " "),
            "HSWIM Vehicle Class": row.vehicle_class,
            "HSWIM Axle Conf": row.axle_conf,
            "HSWIM Registration": row.lp,
            "Axle 1": row.axle_1_load + "",
            "Axle 2": row.axle_2_load + "",
            "Axle 3": row.axle_3_load + "",
            "Axle 4": row.axle_4_load + "",
            "Axle 5": row.axle_5_load + "",
            "Axle 6": row.axle_6_load + "",
            "Axle 7": row.axle_7_load + "",
            "Axle 8": row.axle_8_load + "",
            "Axle 9": row.axle_9_load + "",
            "Called In": row.calledin,

            "Static Date": row.wbrg_ticket_date.replace("T", " "),
            "Ticket No": row.wbrg_ticket_no.trim(),
            Station: row.wbrg_station,
            Registation: row.veh_reg,
            Source: row.wbrg_ticket_source,
            Destination: row.wbrg_ticket_destination,
            Cargo: row.wbrg_product,
            "Group 1": row.axle1_weight + "",
            "Group 2": row.axle2_weight + "",
            "Group 3": row.axle3_weight + "",
            "Group 4": row.axle4_weight + "",
            Status: row.status + "",
            State: row.state,
            "Static GVW": row.wbrg_ticket_grossweight,
            "HSWIM GVW": row.hswim_grossweight,
            Difference: row.Diff,
            "Called in Reason": row.calledinreason,
          }));
          this.orderData = data;
          this.pl = "l"; //"p" for portrait
          this.datareport(headerrpt, headerspace, data);

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
    getstaticvshswim() {
      const urlString =
        "WeighbridgeTransactions/report/staticvshswim?wim=W1&calledin=1&" +
        this.filter;
      //alert(urlString);
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      console.log(this.filter);

      //alert(window.$http + `weighbridgetransactions/search?` + this.filter);
      axios
        .get(window.$http + urlString, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          var no = 15;
          var headerrpt = new Array(no);
          var headerspace = new Array(no);

          headerrpt[0] = "No.";
          headerrpt[1] = "Ticket No.";
          headerrpt[2] = "Date Time";
          headerrpt[3] = "HSWIM Conf";
          headerrpt[4] = "System Conf";
          headerrpt[5] = "Registration";
          headerrpt[6] = "Axle 1";
          headerrpt[7] = "Axle 2";
          headerrpt[8] = "Axle 3";
          headerrpt[9] = "Axle 4";
          headerrpt[10] = "Axle 5";
          headerrpt[11] = "Axle 6";
          headerrpt[12] = "Axle 7";
          headerrpt[13] = "Axle 8";
          headerrpt[14] = "Axle 9";
          headerrpt[15] = "Called In";
          headerrpt[16] = "Weighbridge Date";
          headerrpt[17] = "Ticket No.";
          headerrpt[18] = "Station";
          headerrpt[19] = "Source";
          headerrpt[20] = "Destination";
          headerrpt[21] = "Cargo";
          headerrpt[22] = "Group 1";
          headerrpt[23] = "Group 2";
          headerrpt[24] = "Group 3";
          headerrpt[25] = "Group 4";
          headerrpt[26] = "Status";
          headerrpt[27] = "Deck GVW";
          headerrpt[28] = "HSWIM GVW";

          headerspace[0] = 1; //2
          headerspace[1] = 1; //7
          headerspace[2] = 1; //9
          headerspace[3] = 1; //13
          headerspace[4] = 1; //16
          headerspace[5] = 1; //19
          headerspace[6] = 1; //22
          headerspace[7] = 1; //25
          headerspace[8] = 1; //31
          headerspace[9] = 1; //19
          headerspace[10] = 2; //22
          headerspace[11] = 2; //25
          headerspace[12] = 2; //25
          headerspace[13] = 2; //31
          headerspace[14] = 2; //31
          headerspace[15] = 1; //31
          headerspace[16] = 1; //31
          headerspace[17] = 1; //31
          headerspace[18] = 1; //31
          headerspace[19] = 1; //31
          headerspace[20] = 1; //31
          headerspace[21] = 1; //31
          headerspace[22] = 1; //31
          headerspace[23] = 1; //31
          headerspace[24] = 1; //31
          headerspace[25] = 1; //31
          headerspace[26] = 1; //31
          headerspace[27] = 1; //31
          headerspace[28] = 1; //31

          /*          {
    "hourDate": "11/20/2021 2:00:00 AM",
    "hourTime": "11/20/2021 2:00:00 AM",
    "multideck": "0",
    "singleAxle": "0",
    "manually": null,
    "hswim": "0",
    "calledIn": null,
    "totalWeighed": "0"
  },*/
          var id = 1;
          const data = this.orderData.map((row) => ({
            "ID.": id++,
            "HSWIM Ticket No.": row.cid,
            "HSWIM Date Time": row.date_time_local.replace("T", " "),
            "HSWIM Vehicle Class": row.vehicle_class,
            "HSWIM Axle Conf": row.axle_conf,
            "HSWIM Registration": row.lp,
            "Axle 1": row.axle_1_load + "",
            "Axle 2": row.axle_2_load + "",
            "Axle 3": row.axle_3_load + "",
            "Axle 4": row.axle_4_load + "",
            "Axle 5": row.axle_5_load + "",
            "Axle 6": row.axle_6_load + "",
            "Axle 7": row.axle_7_load + "",
            "Axle 8": row.axle_8_load + "",
            "Axle 9": row.axle_9_load + "",
            "Called In": row.calledin,

            "Static Date": row.wbrg_ticket_date.replace("T", " "),
            "Ticket No": row.wbrg_ticket_no,
            Station: row.wbrg_station,
            Registation: row.veh_reg,
            Source: row.wbrg_ticket_source,
            Destination: row.wbrg_ticket_destination,
            Cargo: row.wbrg_product,
            "Group 1": row.axle1_weight + "",
            "Group 2": row.axle2_weight + "",
            "Group 3": row.axle3_weight + "",
            "Group 4": row.axle4_weight + "",
            Status: row.status + "",
            State: row.state,
            "Static GVW": row.wbrg_ticket_grossweight,
            "HSWIM GVW": row.hswim_grossweight,
            Difference: row.diff,
          }));
          //wbrg_ticket_grossweight - row.hswim_grossweight
          this.orderData = data;
          this.pl = "l"; //"p" for portrait
          this.datareport(headerrpt, headerspace, data);

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
    getmonthlystatistics() {
      const urlString =
        "WeighbridgeTransactions/report/dailystatistics?" + this.filter;
      //alert(urlString);
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      console.log(this.filter);

      //alert(window.$http + `weighbridgetransactions/search?` + this.filter);
      axios
        .get(window.$http + urlString, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          var no = 15;
          var headerrpt = new Array(no);
          var headerspace = new Array(no);

          headerrpt[0] = "No.";
          headerrpt[1] = "Date";
          headerrpt[2] = "MultiDeck[D]";
          headerrpt[3] = "SingleAxle[S]";
          headerrpt[4] = "Manually[M]";
          headerrpt[5] = "HSWIM Total[H]";
          headerrpt[6] = "HSWIM Cleared Q=[H-C]";
          headerrpt[7] = "Total Weighed x=[D+S+M+Q]";
          headerrpt[8] = "CalledIn[C]";
          headerrpt[9] = "WarnedTucks[A]";
          headerrpt[10] = "ChargedInCourt[Z]";
          headerrpt[11] = "SpecialRelease[G]";
          headerrpt[12] = "Redistributed[R]";
          headerrpt[13] = "Impounded & Prohibited P=[Z+R]";

          headerspace[0] = 3; //2
          headerspace[1] = 3; //7
          headerspace[2] = 3; //9
          headerspace[3] = 3; //13
          headerspace[4] = 3; //16
          headerspace[5] = 3; //19
          headerspace[6] = 3; //22
          headerspace[7] = 3; //25
          headerspace[8] = 3; //31
          headerspace[9] = 2; //19
          headerspace[10] = 2; //22
          headerspace[11] = 3; //25
          headerspace[12] = 2; //25
          headerspace[13] = 4; //31

          /*          {
    "hourDate": "11/20/2021 2:00:00 AM",
    "hourTime": "11/20/2021 2:00:00 AM",
    "multideck": "0",
    "singleAxle": "0",
    "manually": null,
    "hswim": "0",
    "calledIn": null,
    "totalWeighed": "0"
  },*/

          const data = this.orderData.map((row) => ({
            "No.": row.id,
            Date: row.hourDate,
            "MultiDeck[D]": row.multideck,
            "SingleAxle[S]": row.singleAxle,
            "Manually[M]": row.manually,
            "HSWIM Total[H]": row.hswim,
            "HSWIM[Called In]Q=H-C": row.calledInLess,
            "Total Weighed x=[D+S+M+Q]": row.totalWeighed,
            "CalledIn[C]": row.calledIn,
            "WarnedTucks[A]": row.warned,
            "ChargedInCourt[Z]": row.charged,
            "SpecialRelease[G]": row.specialRelease,
            "Redistributed[R]": row.redistributed,
            "Impounded & Prohibited P=[Z+R]": row.impoundedandprohibited,
          }));
          this.orderData = data;
          this.pl = "l"; //"p" for portrait
          this.datareport(headerrpt, headerspace, data);

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
    getstatistics() {
      const urlString =
        "WeighbridgeTransactions/report/hourstatistics?" + this.filter;
      //alert(urlString);
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      console.log(this.filter);

      //alert(window.$http + `weighbridgetransactions/search?` + this.filter);
      axios
        .get(window.$http + urlString, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          var no = 15;
          var headerrpt = new Array(no);
          var headerspace = new Array(no);

          headerrpt[0] = "No.";
          headerrpt[1] = "Date";
          headerrpt[2] = "Time";
          headerrpt[3] = "MultiDeck[D]";
          headerrpt[4] = "SingleAxle[S]";
          headerrpt[5] = "Manually[M]";
          headerrpt[6] = "HSWIM Total[H]";
          headerrpt[7] = "HSWIM Cleared Q=[H-C]";
          headerrpt[8] = "Total Weighed x=[D+S+M+Q]";
          headerrpt[9] = "CalledIn[C]";
          headerrpt[10] = "WarnedTucks[A]";
          headerrpt[11] = "ChargedInCourt[Z]";
          headerrpt[12] = "SpecialRelease[G]";
          headerrpt[13] = "Redistributed[R]";
          headerrpt[14] = "Impounded & Prohibited P=[Z+R]";

          headerspace[0] = 3; //2
          headerspace[1] = 3; //7
          headerspace[2] = 3; //9
          headerspace[3] = 3; //13
          headerspace[4] = 3; //16
          headerspace[5] = 3; //19
          headerspace[6] = 3; //22
          headerspace[7] = 3; //25
          headerspace[8] = 3; //31
          headerspace[9] = 2; //19
          headerspace[10] = 2; //22
          headerspace[11] = 3; //25
          headerspace[12] = 2; //25
          headerspace[13] = 2; //31
          headerspace[14] = 2; //31

          /*          {
    "hourDate": "11/20/2021 2:00:00 AM",
    "hourTime": "11/20/2021 2:00:00 AM",
    "multideck": "0",
    "singleAxle": "0",
    "manually": null,
    "hswim": "0",
    "calledIn": null,
    "totalWeighed": "0"
  },*/

          const data = this.orderData.map((row) => ({
            "No.": row.id,
            Date: row.hourDate,
            Time: row.hourTime + "00",
            "MultiDeck[D]": row.multideck,
            "SingleAxle[S]": row.singleAxle,
            "Manually[M]": row.manually,
            "HSWIM Total[H]": row.hswim,
            "HSWIM[Called In]Q=H-C": row.calledInLess,
            "Total Weighed x=[D+S+M+Q]": row.totalWeighed,
            "CalledIn[C]": row.calledIn,
            "WarnedTucks[A]": row.warned,
            "ChargedInCourt[Z]": row.charged,
            "SpecialRelease[G]": row.specialRelease,
            "Redistributed[R]": row.redistributed,
            "Impounded & Prohibited P=[Z+R]": row.impoundedandprohibited,
          }));
          this.orderData = data;
          this.pl = "l"; //"p" for portrait
          this.datareport(headerrpt, headerspace, data);

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
    gethourlystatistics(fromdate, todate) {
      const urlString =
        "WeighbridgeTransactions/report/hourlystatistics?fromdate=" + //WeighbridgeTransactions/report/hourlystatistics
        fromdate +
        "&todate=" +
        todate;
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      console.log(this.filter);

      //alert(window.$http + `weighbridgetransactions/search?` + this.filter);
      axios
        .get(window.$http + urlString, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          var no = 9;
          var headerrpt = new Array(no);
          var headerspace = new Array(no);

          headerrpt[0] = "No.";
          headerrpt[1] = "Ticket No";
          headerrpt[2] = "Station";
          headerrpt[3] = "Vehicle Reg";
          headerrpt[4] = "Source";
          headerrpt[5] = "Destination";
          headerrpt[6] = "Date";
          headerrpt[7] = "Axle 1";
          headerrpt[8] = "Axle 2";
          headerrpt[9] = "Axle 3";
          headerrpt[10] = "Axle 4";
          headerrpt[11] = "Axle 5";
          headerrpt[12] = "Axle 6";
          headerrpt[13] = "GrossWeight";
          headerrpt[14] = "GVW";
          headerrpt[15] = "DateOut";
          headerrpt[16] = "Charge No";
          headerrpt[17] = "Type";

          headerspace[0] = 2; //2
          headerspace[1] = 2; //7
          headerspace[2] = 2; //9
          headerspace[3] = 2; //13
          headerspace[4] = 2; //16
          headerspace[5] = 2; //19
          headerspace[6] = 3; //22
          headerspace[7] = 2; //25
          headerspace[8] = 2; //31
          headerspace[9] = 2; //2
          headerspace[10] = 2; //7
          headerspace[11] = 2; //9
          headerspace[12] = 2; //13
          headerspace[13] = 2; //16
          headerspace[14] = 2; //19
          headerspace[15] = 3; //22
          headerspace[16] = 6; //25
          headerspace[17] = 2; //31

          /*              id = rowid.ToString(),
                wbrg_ticket_no = "",
                wbrg_station = "",
                veh_reg = "",
                wbrg_ticket_source = "",
                wbrg_ticket_destination ="",
                wbrg_ticket_date = "",
                axle1_weight = "",
                axle2_weight = "",
                axle3_weight = "",
                axle4_weight ="",
                axle5_weight = "",
                axle6_weight ="",
                wbrg_ticket_grossweight = "",
                wbrg_ticket_gvwload ="",
                wbrg_ticket_dateout = "",
                wbrg_ticket_charge_no = "",
                wbrg_ticket_type = ""*/

          const data = this.orderData.map((row) => ({
            "No.": row.id,
            "Ticket No": row.wbrg_ticket_no,
            Station: row.wbrg_station,
            "Vehicle Reg": row.veh_reg,
            Source: row.wbrg_ticket_source,
            Destination: row.wbrg_ticket_destination,
            Date: row.wbrg_ticket_date,
            "Axle 1": row.axle1_weight,
            "Axle 2": row.axle2_weight,
            "Axle 3": row.axle3_weight,
            "Axle 4": row.axle4_weight,
            "Axle 5": row.axle5_weight,
            "Axle 6": row.axle6_weight,
            GrossWeight: row.wbrg_ticket_grossweight,
            GVW: row.wbrg_ticket_gvwload,
            DateOut: row.wbrg_ticket_dateout,
            "Charge No": row.wbrg_ticket_charge_no,
            Type: row.wbrg_ticket_type,
          }));
          this.orderData = data;
          this.pl = "l"; //"p" for portrait
          this.datareport(headerrpt, headerspace, data);

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
    censusreport() {
      //alert();

      var headerrpt = new Array(10);
      var headerspace = new Array(10);

      headerrpt[0] = "No.";
      headerrpt[1] = "Date";
      headerrpt[2] = "Shift";
      headerrpt[3] = "Route";
      headerrpt[4] = "Hour";
      headerrpt[5] = "Trucks ≥ 7T";
      headerrpt[6] = "Trucks 3.5 ≥ ≤ 7T";
      headerrpt[7] = "Buses";
      headerrpt[8] = "Clerk";
      headerrpt[9] = "Cluster";

      headerspace[0] = 2; //2
      headerspace[1] = 5; //7
      headerspace[2] = 2; //9
      headerspace[3] = 4; //13
      headerspace[4] = 3; //16
      headerspace[5] = 3; //19
      headerspace[6] = 3; //22
      headerspace[7] = 3; //25
      headerspace[8] = 6; //31
      headerspace[9] = 9; //40
      var id = 1;
      const data = this.orderData.map((row) => ({
        No: id++,
        Date: row.date,
        Shift: row.shift,
        Route: row.route,
        Hour: row.hour + "H",
        Trucks7T: row.truck7t,
        Trucks357T: row.truck357t,
        Buses: row.buses,
        Clerk: row.userid,
        Cluster: row.station,
      }));
      this.orderData = data;
      //get headers
      //const headers = Object.keys(data[0]);
      const cars = [];
      Object.entries(data).forEach((val) => {
        const [key, value] = val;
        console.log(key, value);
        cars.push(Object.values(data[key]));
      });

      const uniqueCars = Array.from(new Set(cars));

      var head = [];
      var body = [];
      for (let i = 0; i < 40; i++) {
        head.push({
          content: "",
          colSpan: 1,
          styles: {
            halign: "left",
            fillColor: [255, 255, 255],
            textColor: [0, 0, 0],
            cellWidth: "auto",
            lineWidth: 0,
          },
        });
      }

      // var tt = 0;
      var row = [];

      for (var g = 0; g < headerrpt.length; g++) {
        // tt += headerspace[g];
        //alert(tt);
        row.push({
          content: headerrpt[g],
          colSpan: headerspace[g],
          styles: {
            halign: "center",
            fillColor: [204, 204, 204],
            textColor: [0, 0, 0],
            fontStyle: "bold",
            fontSize: 7,
            lineWidth: 0,
            valign: "middle",
          },
        });
      }
      body.push(Object.values(row));
      //alert(uniqueCars.length);
      var Trucks7T = 0;
      var Trucks357T = 0;
      var Buses = 0;
      for (var k = 0; k < uniqueCars.length; k++) {
        row = [];
        for (g = 0; g < headerrpt.length; g++) {
          // tt += headerspace[g];
          //alert(tt);
          if (g == 5) {
            Trucks7T += Number(uniqueCars[k][g]);
          }
          if (g == 6) {
            Trucks357T += Number(uniqueCars[k][g]);
          }
          if (g == 7) {
            Buses += Number(uniqueCars[k][g]);
          }
          row.push({
            content: uniqueCars[k][g],
            colSpan: headerspace[g],
            styles: {
              halign: "center",
              fillColor: [255, 255, 255],
              textColor: [0, 0, 0],
              fontStyle: "bold",
              fontSize: 7,
              lineWidth: 0.1,
            },
          });
        }
        body.push(Object.values(row));
      }

      row = [];
      var total = "";
      var lineWidth = 0;
      for (g = 0; g < headerrpt.length; g++) {
        // tt += headerspace[g];
        //alert(tt);
        total = "";
        lineWidth = 0;
        if (g == 5) {
          total = Trucks7T;
          lineWidth = 0.5;
        }
        if (g == 6) {
          total = Trucks357T;
          lineWidth = 0.5;
        }
        if (g == 7) {
          total = Buses;
          lineWidth = 0.5;
        }
        row.push({
          content: total,
          colSpan: headerspace[g],
          styles: {
            halign: "center",
            fillColor: [255, 255, 255],
            textColor: [0, 0, 0],
            fontStyle: "bold",
            fontSize: 7,
            lineWidth: lineWidth,
          },
        });
      }
      body.push(Object.values(row));
      this.pl = "p";
      this.headers = head;
      this.uniqueCars = body;
      var doc = new jsPDF();
      doc.autoTable({
        head: [head],
        body: body,
        // didDrawCell: function (data) {
        //   if (data.column.index === 5 && data.cell.section === "body") {
        //     var td = data.cell.raw;
        //     // Assuming the td cells have an img element with a data url set (<td><img src="data:image/jpeg;base64,/9j/4AAQ..."></td>)
        //     //var kenhaimg = new Image();
        //     var kenhaimg = td.getElementsByTagName("img")[0];
        //     kenhaimg.src = this.kenhaimglogo;
        //     var dim = data.cell.height - data.cell.padding("vertical");
        //     var textPos = data.cell.textPos;
        //     doc.addImage(kenhaimg, textPos.x, textPos.y, dim, dim);
        //   }
        // },
        startY: 0,
        margin: { horizontal: 1 },
        styles: {
          columnWidth: "wrap",
          fontSize: 7,
          overflow: "linebreak",
          cellWidth: "wrap",
        },
        columnStyles: {
          text: { columnWidth: "auto" },
          nil: { halign: "right" },
          tgl: { halign: "right" },
        },
        headerStyles: {
          halign: "left",
          fillColor: [255, 255, 255],
          textColor: [0, 0, 0],
        },
        bodyStyles: { lineColor: [0, 0, 0] },
        theme: "grid",
      });
      //doc.save(certno + "-" + oi.item.caseregno + ".pdf");
      //doc.output("dataurlnewwindow");
    },
    shiftsreport() {
      //alert();

      var headerrpt = new Array(12);
      var headerspace = new Array(12);

      headerrpt[0] = "ID.";
      headerrpt[1] = "Shift Type";
      headerrpt[2] = "Email";
      headerrpt[3] = "Role";
      headerrpt[4] = "From";
      headerrpt[5] = "To";

      headerspace[0] = 2; //2
      headerspace[1] = 2; //4
      headerspace[2] = 2; //6
      headerspace[3] = 2; //8
      headerspace[4] = 2; //10
      headerspace[5] = 2; //10

      var id = 1;
      const data = this.shifts.map((row) => ({
        ID: id++,
        Email: row.email,
        "Shift Type": row.shift_type,
        Role: row.position,
        From: row.from,
        To: row.to,
      }));
      this.orderData = data;
      //get headers
      //const headers = Object.keys(data[0]);
      const cars = [];
      Object.entries(data).forEach((val) => {
        const [key, value] = val;
        console.log(key, value);
        cars.push(Object.values(data[key]));
      });

      const uniqueCars = Array.from(new Set(cars));

      var head = [];
      var body = [];
      for (let i = 0; i < 12; i++) {
        head.push({
          content: "",
          colSpan: 1,
          styles: {
            halign: "left",
            fillColor: [255, 255, 255],
            textColor: [0, 0, 0],
            cellWidth: "auto",
            lineWidth: 0,
          },
        });
      }

      // var tt = 0;
      var row = [];

      for (var g = 0; g < headerrpt.length; g++) {
        // tt += headerspace[g];
        //alert(tt);
        row.push({
          content: headerrpt[g],
          colSpan: headerspace[g],
          styles: {
            halign: "center",
            fillColor: [204, 204, 204],
            textColor: [0, 0, 0],
            fontStyle: "bold",
            fontSize: 7,
            lineWidth: 0,
            valign: "middle",
          },
        });
      }
      body.push(Object.values(row));
      //alert(uniqueCars.length);
      for (var k = 0; k < uniqueCars.length; k++) {
        row = [];
        for (g = 0; g < headerrpt.length; g++) {
          row.push({
            content: uniqueCars[k][g],
            colSpan: headerspace[g],
            styles: {
              halign: "center",
              fillColor: [255, 255, 255],
              textColor: [0, 0, 0],
              fontStyle: "bold",
              fontSize: 7,
              lineWidth: 0.1,
            },
          });
        }
        body.push(Object.values(row));
      }

      row = [];
      var total = "";
      var lineWidth = 0;
      for (g = 0; g < headerrpt.length; g++) {
        row.push({
          content: total,
          colSpan: headerspace[g],
          styles: {
            halign: "center",
            fillColor: [255, 255, 255],
            textColor: [0, 0, 0],
            fontStyle: "bold",
            fontSize: 7,
            lineWidth: lineWidth,
          },
        });
      }
      body.push(Object.values(row));
      this.pl = "p";
      this.headers = head;
      this.uniqueCars = body;
      var doc = new jsPDF();
      doc.autoTable({
        head: [head],
        body: body,
        startY: 0,
        margin: { horizontal: 1 },
        styles: {
          columnWidth: "wrap",
          fontSize: 7,
          overflow: "linebreak",
          cellWidth: "wrap",
        },
        columnStyles: {
          text: { columnWidth: "auto" },
          nil: { halign: "right" },
          tgl: { halign: "right" },
        },
        headerStyles: {
          halign: "left",
          fillColor: [255, 255, 255],
          textColor: [0, 0, 0],
        },
        bodyStyles: { lineColor: [0, 0, 0] },
        theme: "grid",
      });
      //doc.save(certno + "-" + oi.item.caseregno + ".pdf");
      //doc.output("dataurlnewwindow");
    },
    weighbridgeregister() {
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      //alert(window.$http + `weighbridgetransactions/search?` + this.filter);
      axios
        .get(
          window.$http +
            `weighbridgetransactions/search?limit=1000000000&` +
            this.filter,
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          var no = 19;
          var headerrpt = new Array(no);
          var headerspace = new Array(no);

          headerrpt[0] = "No.";
          headerrpt[1] = "Date Time";
          headerrpt[2] = "Ticket No.";
          headerrpt[3] = "Station";
          headerrpt[4] = "Registration";
          headerrpt[5] = "Axle";
          headerrpt[6] = "Transporter";
          headerrpt[7] = "Cargo";
          headerrpt[8] = "Make";
          headerrpt[9] = "Origin";
          headerrpt[10] = "Destination";
          headerrpt[11] = "Deck A[KG]";
          headerrpt[12] = "Deck B[KG]";
          headerrpt[13] = "Deck C[KG]";
          headerrpt[14] = "Deck D[KG]";
          headerrpt[15] = "GVW [KG]";
          headerrpt[16] = "Excess";
          headerrpt[17] = "Excess [KG]";
          headerrpt[18] = "Status";
          headerrpt[19] = "State";

          headerspace[0] = 2; //2
          headerspace[1] = 3; //7
          headerspace[2] = 3; //9
          headerspace[3] = 2; //13
          headerspace[4] = 3; //16
          headerspace[5] = 2; //19
          headerspace[6] = 1; //22
          headerspace[7] = 2; //25
          headerspace[8] = 2; //31
          headerspace[9] = 2; //40
          headerspace[10] = 2; //40
          headerspace[11] = 2; //40
          headerspace[12] = 2; //40
          headerspace[13] = 2; //40
          headerspace[14] = 2; //16
          headerspace[15] = 2; //19
          headerspace[16] = 2; //22
          headerspace[17] = 2; //25
          headerspace[18] = 1; //25
          headerspace[19] = 1; //25

          const data = this.orderData.map((row) => ({
            "ID.": row.id,
            "Date Time": row.wbrg_ticket_date,
            "Ticket No.": row.wbrg_ticket_no,
            Station: row.wbrg_station,
            Registration: row.veh_reg,
            Axle: row.wbrg_ticket_axel,
            Transporter: row.wbrg_tx_code,
            Cargo: row.wbrg_product,
            Make: row.wbrg_ticket_make,
            Origin: row.wbrg_ticket_source,
            Destination: row.wbrg_ticket_destination,
            "Deck A[KG]": row.axle1_weight.toLocaleString(),
            "Deck B[KG]": row.axle2_weight.toLocaleString(),
            "Deck C[KG]": row.axle3_weight.toLocaleString(),
            "Deck D[KG]": row.axle4_weight.toLocaleString(),
            "GVW [KG]": (
              Number(row.axle1_weight) +
              Number(row.axle2_weight) +
              Number(row.axle3_weight) +
              Number(row.axle4_weight)
            ).toLocaleString(),
            Excess: row.wbrg_ticket_excessload,
            "Excess [KG]": row.wbrg_ticket_excessweight,
            Status: row.wbrg_ticket_status,
            State: row.wbrg_ticket_state,
          }));
          this.orderData = data;
          this.pl = "l"; //"p" for portrait
          this.datareport(headerrpt, headerspace, data);

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
    excessgvwaxle() {
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      //alert(window.$http + `weighbridgetransactions/search?` + this.filter);
      axios
        .get(
          window.$http +
            `weighbridgetransactions/search?limit=1000000000&` +
            this.filter,
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          var no = 19;
          var headerrpt = new Array(no);
          var headerspace = new Array(no);

          headerrpt[0] = "No.";
          headerrpt[1] = "Date Time";
          headerrpt[2] = "Ticket No.";
          headerrpt[3] = "Station";
          headerrpt[4] = "Registration";
          headerrpt[5] = "Axle";
          headerrpt[6] = "Transporter";
          headerrpt[7] = "Excess Axle[KG]";
          headerrpt[8] = "Excess GVW [KG]";

          headerspace[0] = 4; //2
          headerspace[1] = 4; //7
          headerspace[2] = 4; //9
          headerspace[3] = 4; //13
          headerspace[4] = 4; //16
          headerspace[5] = 4; //19
          headerspace[6] = 4; //22
          headerspace[7] = 4; //25
          headerspace[8] = 8; //31

          const data = this.orderData.map((row) => ({
            "ID.": row.id,
            "Date Time": row.wbrg_ticket_date,
            "Ticket No.": row.wbrg_ticket_no,
            Station: row.wbrg_station,
            Registration: row.veh_reg,
            Axle: row.wbrg_ticket_axel,
            Transporter: row.wbrg_tx_code,
            "Excess Axle[KG]": row.wbrg_ticket_axleload,
            "Excess GVW [KG]": row.wbrg_ticket_gvwload,
          }));
          this.orderData = data;
          this.pl = "l"; //"p" for portrait
          this.datareport(headerrpt, headerspace, data);

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

    specialrelease() {
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      //alert(
      //   window.$http +
      //     `specialrelease/reports?` +
      //     this.filter +
      //     "&limit=1000000000"
      // );
      axios
        .get(
          window.$http +
            `specialrelease/reports?` +
            this.filter +
            "&limit=1000000000",
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          var headerrpt = new Array(18);
          var headerspace = new Array(18);

          headerrpt[0] = "Index";
          headerrpt[1] = "Serial Number";
          headerrpt[2] = "Station";
          headerrpt[3] = "Ticket";
          headerrpt[4] = "Weigh Date";
          headerrpt[5] = "Vehicle Reg";
          headerrpt[6] = "Transporter";
          headerrpt[7] = "Cargo";
          headerrpt[8] = "Reason";
          headerrpt[9] = "Other Reason";
          headerrpt[10] = "Excess Load";
          headerrpt[11] = "Release Date";
          headerrpt[12] = "Released By";
          headerrpt[13] = "Weighed By";

          headerspace[0] = 1; //2
          headerspace[1] = 3; //7
          headerspace[2] = 3; //9
          headerspace[3] = 3; //13
          headerspace[4] = 3; //16
          headerspace[5] = 3; //19
          headerspace[6] = 3; //22
          headerspace[7] = 3; //25
          headerspace[8] = 3; //31
          headerspace[9] = 3; //40
          headerspace[10] = 3; //25
          headerspace[11] = 3; //31
          headerspace[12] = 4; //40
          headerspace[13] = 2; //40
          const data = this.orderData.map((row) => ({
            Index: row.id,
            "Serial Number": row.specialid,
            Station: row.weighbridge,
            Ticket: row.ticketno,
            "Weigh Date": row.weighdate,
            "Vehicle Reg": row.regNo,
            Transporter: row.transporter,
            Cargo: row.cargo,
            Reason: row.reason,
            "Other Reason": row.description,
            "Excess Load": row.excessload,
            "Release Date": row.realeasedate,
            "Released By": row.createdby,
            "Weighed By": row.weighedby,
          }));
          this.orderData = data;
          this.pl = "l";
          this.datareport(headerrpt, headerspace, data);

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
    heavyload() {
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
        .get(
          window.$http +
            `yardinspection/search?limit=1000000000&deletestatus=0&` +
            this.filter,
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          var headerrpt = new Array(30);
          var headerspace = new Array(30);
          headerrpt[0] = "No";
          headerrpt[1] = "Inspection Date";
          headerrpt[2] = "Registration";
          headerrpt[3] = "traillerreg";
          headerrpt[4] = "Model";
          headerrpt[5] = "Origin";
          headerrpt[6] = "Destination";
          headerrpt[7] = "Axleconf";
          headerrpt[8] = "Inspsticker";
          headerrpt[9] = "InsuSticker";
          headerrpt[10] = "Cargo";
          headerrpt[12] = "Height";
          headerrpt[13] = "Length_";
          headerrpt[14] = "width_";
          headerrpt[15] = "Total tyres";
          headerrpt[16] = "firstAkit";
          headerrpt[17] = "fireext";
          headerrpt[18] = "drunkDriver";
          headerrpt[19] = "dlicensestate";
          headerrpt[20] = "inspstick";
          headerrpt[21] = "insuaranceStic";
          headerrpt[22] = "SteeringOL";
          headerrpt[23] = "SteeringBP";
          headerrpt[24] = "rodend";
          headerrpt[25] = "balljoints";
          headerrpt[26] = "wheelallignment";
          headerrpt[27] = "lightstatus";
          headerrpt[28] = "sterringdef";
          headerrpt[29] = "fhBreak";
          headerrpt[30] = "aoDefects";
          headerrpt[31] = "Status";

          headerspace[0] = 1; //2
          headerspace[1] = 1; //7
          headerspace[2] = 2; //9
          headerspace[3] = 2; //13
          headerspace[4] = 2; //16
          headerspace[5] = 2; //19
          headerspace[6] = 2; //22
          headerspace[7] = 2; //25
          headerspace[8] = 2; //31
          headerspace[9] = 3; //40
          headerspace[10] = 1; //40
          headerspace[11] = 1; //40
          headerspace[12] = 1; //40
          headerspace[13] = 1; //40
          headerspace[14] = 1; //16
          headerspace[15] = 1; //19
          headerspace[16] = 1; //22
          headerspace[17] = 1; //25
          headerspace[18] = 1; //25
          headerspace[19] = 1; //25
          headerspace[20] = 1; //25
          headerspace[21] = 1; //25
          headerspace[22] = 1; //25
          headerspace[23] = 1; //25
          headerspace[24] = 1; //25
          headerspace[25] = 1; //25
          headerspace[26] = 1; //25
          headerspace[27] = 1; //25
          headerspace[28] = 1; //25
          headerspace[29] = 1; //25
          headerspace[30] = 1; //25
          var id = 1;
          const data = this.orderData.map((row) => ({
            No: id++,
            "Inspection Date": row.datetime.replace("T", " "),
            Registration: row.registration,
            "Trailler Reg.": row.traillerreg,
            Transporter: row.transp,
            Model: row.model,
            Origin: row.origin,
            Destination: row.destination,
            "Axle Conf": row.axleconf,
            Cargo: row.loaddisc,
            "Insuarance Sticker": row.inspstick,
            "First Aid Kit": row.firstAkit,
            "Fire Extinguisher": row.fireext,
            "Drunk Driver": row.drunkDriver,
            "Drivers License Status": row.dlicensestate,
            "Inspection Sticker State": row.inspstick,
            "Insurance Sticker State": row.insuaranceStic,
            "Steering Oil Level": row.SteeringOL,
            "Steering Box & Pipes": row.SteeringBP,
            "The Rod End": row.rodend,
            "Ball Joints": row.balljoints,
            "Wheel Allignment": row.wheelallignment,
            "Lights Status": row.lightstatus,
            "Steering Defects": row.sterringdef,
            "Foot and Hand Brake Defects": row.fhBreak,
            "Any Other Defect(s):": row.aoDefects,
            "Total Tyres": row.totaltyres,
            "Tyre(s) % Conf": row.perconf,
            "Tyre(s) % Non-Conf": row.nonperconf,
            "Ticket No.": row.ticketno,
            remarks: row.remarks,
            Height: row.height_,
            Length: row.length_,
            Width: row.width_,
            status: row.status,
          }));
          this.orderData = data;
          this.pl = "l";
          this.datareport(headerrpt, headerspace, data);

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
    wideload() {
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      //alert(window.$http + `wideload/search?` + this.filter);
      axios
        .get(
          window.$http +
            `wideload/search?limit=1000000000&deletestatus=0&` +
            this.filter,
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          var headerrpt = new Array(18);
          var headerspace = new Array(18);

          headerrpt[0] = "Inspection Date";
          headerrpt[1] = "Registration";
          headerrpt[2] = "Transporter";
          headerrpt[3] = "Model";
          headerrpt[4] = "Origin";
          headerrpt[5] = "Destination";
          headerrpt[6] = "Axleconf";
          headerrpt[7] = "Inspsticker";
          headerrpt[8] = "InsuSticker";
          headerrpt[9] = "Cargo";
          headerrpt[10] = "Permit issue date";
          headerrpt[11] = "Height";
          headerrpt[12] = "Length_";
          headerrpt[13] = "width_";
          headerrpt[14] = "Abnormal Load Permit";
          headerrpt[15] = "Total tyres";
          headerrpt[16] = "Load Weight";
          headerrpt[17] = "Authorized Weight";
          headerrpt[18] = "Permit No.";
          headerrpt[19] = "Date of Travel";
          headerrpt[20] = "Start Date";
          headerrpt[21] = "End Date";
          headerrpt[22] = "Status";

          headerspace[0] = 1; //2
          headerspace[1] = 1; //7
          headerspace[2] = 2; //9
          headerspace[3] = 2; //13
          headerspace[4] = 2; //16
          headerspace[5] = 2; //19
          headerspace[6] = 2; //22
          headerspace[7] = 2; //25
          headerspace[8] = 2; //31
          headerspace[9] = 4; //40
          headerspace[10] = 2; //40
          headerspace[11] = 2; //40
          headerspace[12] = 2; //40
          headerspace[13] = 2; //40
          headerspace[14] = 2; //16
          headerspace[15] = 2; //19
          headerspace[16] = 2; //22
          headerspace[17] = 1; //25
          headerspace[18] = 1; //25
          headerspace[19] = 1; //25
          headerspace[20] = 1; //25
          headerspace[21] = 1; //25
          headerspace[22] = 1; //25
          var id = 1;
          const data = this.orderData.map((row) => ({
            No: id++,
            "Inspection Date": row.permitdatetime.replace("T", " "),
            registration: row.registration,
            Transp: row.transp,
            Model: row.model,
            Origin: row.origin,
            destination: row.destination,
            Axleconf: row.axleconf,
            Inspstick: row.inspstick,
            InsuaranceStic: row.insuaranceStic,
            Cargo: row.loaddisc,
            Dpermitissu: row.dpermitissu,
            Height: row.height_,
            Length: row.length_,
            Width: row.width_,
            AbnormalLPermit: row.abnormalLPermit,
            Totaltyres: row.totaltyres,
            weighofload: row.weighofload,
            Authweight: row.authweight,
            "Permit No.": row.permitno,
            "Date of Travel": row.dateofTravel.replace("T", " "),
            "Start Date": row.pStartD.replace("T", " "),
            "End Date": row.pEndD.replace("T", " "),
            status: row.status,
          }));
          this.orderData = data;
          this.pl = "l";
          this.datareport(headerrpt, headerspace, data);

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

    manualweighing() {
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      //alert(window.$http + `weighbridgetransactions/search?` + this.filter);
      axios
        .get(
          window.$http +
            `manualweighing/search?limit=1000000000&` +
            this.filter,
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          var headerrpt = new Array(6);
          var headerspace = new Array(6);

          headerrpt[0] = "ID";
          headerrpt[1] = "Station";
          headerrpt[2] = "Date";
          headerrpt[3] = "User";
          headerrpt[4] = "hour";
          headerrpt[5] = "tickets";

          headerspace[0] = 2; //2
          headerspace[1] = 6; //7
          headerspace[2] = 12; //9
          headerspace[3] = 10; //13
          headerspace[4] = 5; //16
          headerspace[5] = 5; //19

          const data = this.orderData.map((row) => ({
            ID: row.id,
            Station: this.getStation(row.station),
            Date: row.date,
            User: row.userid,
            hour: row.hour,
            tickets: row.tickets,
          }));
          this.orderData = data;
          this.pl = "p";
          this.datareport(headerrpt, headerspace, data);

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
    scaletest() {
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      //alert(window.$http + `weighbridgetransactions/search?` + this.filter);
      axios
        .get(
          window.$http + `scaletest/search?limit=1000000000&` + this.filter,
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          var no = 11;
          var headerrpt = new Array(no);
          var headerspace = new Array(no);

          headerrpt[0] = "ID";
          headerrpt[1] = "Station";
          headerrpt[2] = "Date Time";
          headerrpt[3] = "Scale";
          headerrpt[4] = "Load Used";
          headerrpt[5] = "Weighed By";
          headerrpt[6] = "Deck 1";
          headerrpt[7] = "Deck 2";
          headerrpt[8] = "Deck 3";
          headerrpt[9] = "Deck 4";
          headerrpt[10] = "Results";

          headerspace[0] = 2; //2
          headerspace[1] = 7; //7
          headerspace[2] = 5; //9
          headerspace[3] = 2; //13
          headerspace[4] = 5; //16
          headerspace[5] = 6; //19
          headerspace[6] = 3; //7
          headerspace[7] = 3; //9
          headerspace[8] = 3; //13
          headerspace[9] = 3; //16
          headerspace[10] = 1; //19

          const data = this.orderData.map((row) => ({
            ID: row.id,
            Station: this.getStation(row.station),
            "Date Time": row.scaledatetime.replace("T", " "),
            Scale: row.scalename,
            scalevehreg: row.scalevehreg,
            scaleweighedby: row.scaleweighedby,
            scaledeck1: row.scaledeck1.toLocaleString(),
            scaledeck2: row.scaledeck2.toLocaleString(),
            scaledeck3: row.scaledeck3.toLocaleString(),
            scaledeck4: row.scaledeck4.toLocaleString(),
            result: row.result,
          }));
          this.orderData = data;
          this.pl = "p";
          this.datareport(headerrpt, headerspace, data);

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
    axleConfig() {
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      //alert(window.$http + `weighbridgetransactions/search?` + this.filter);
      axios
        .get(
          window.$http + `AxleWeightConfig/report/axleconfig?limit=1000000000`,
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          var no = 5;
          var headerrpt = new Array(no);
          var headerspace = new Array(no);

          headerrpt[0] = "ID";
          headerrpt[1] = "Axle Configuration Name";
          headerrpt[2] = "Permissible Grouping Deck[A,B,C,D]";
          headerrpt[3] = "Permissible Axle [KG]";
          headerrpt[4] = "Permissible GVW [KG]";

          headerspace[0] = 1; //1
          headerspace[1] = 1; //1
          headerspace[2] = 1; //1
          headerspace[3] = 1; //1
          headerspace[4] = 1; //1

          const data = this.orderData.map((row) => ({
            ID: row.id,
            "Axle Configuration Name": row.axle_name,
            "Permissible Grouping Deck": row.axle_deckgrouping,
            "Permissible Axle": row.axle_legalweight,
            "Permissible GVW": row.axle_group,
          }));
          this.orderData = data;
          this.pl = "p";
          this.datareport(headerrpt, headerspace, data);
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
    getStation(code) {
      var name = "";
      //alert(this.cluster.length);
      var stat = this.cluster;
      for (var i = 0; i < stat.length; i++) {
        if (code == stat[i].cluster_code) {
          name = stat[i].cluster_name;
        }
      }
      return name;
    },
    getStationcode(name) {
      var code = "";
      //alert(this.cluster.length);
      var stat = this.cluster;
      for (var i = 0; i < stat.length; i++) {
        if (name == stat[i].cluster_name) {
          code = stat[i].cluster_code;
        }
      }
      return code;
    },
    datareport(headerrpt, headerspace, data) {
      //get headers
      //const headers = Object.keys(data[0]);
      // alert(this.orderData.length);
      const cars = [];
      Object.entries(data).forEach((val) => {
        const [key, value] = val;
        console.log(key, value);
        cars.push(Object.values(data[key]));
      });

      const uniqueCars = Array.from(new Set(cars));

      var head = [];
      var body = [];
      for (let i = 0; i < 40; i++) {
        head.push({
          content: "",
          colSpan: 1,
          styles: {
            halign: "left",
            fillColor: [255, 255, 255],
            textColor: [0, 0, 0],
            cellWidth: "auto",
            lineWidth: 0,
          },
        });
      }
      // var tt = 0;
      var row = [];

      for (var g = 0; g < headerrpt.length; g++) {
        // tt += headerspace[g];
        //alert(tt);
        row.push({
          content: headerrpt[g],
          colSpan: headerspace[g],
          styles: {
            halign: "center",
            fillColor: [204, 204, 204],
            textColor: [0, 0, 0],
            fontStyle: "bold",
            fontSize: 7,
            lineWidth: 0,
            valign: "middle",
          },
        });
      }
      body.push(Object.values(row));

      for (var k = 0; k < uniqueCars.length; k++) {
        row = [];
        for (g = 0; g < headerrpt.length; g++) {
          var fillColor = [255, 255, 255];
          if (headerrpt[17] == "Excess [KG]") {
            if (Number(uniqueCars[k][17]) > 0) {
              fillColor[0] = 255;
              fillColor[1] = 153;
              fillColor[2] = 153;
            }
          }
          if (headerrpt[15] == "Status") {
            //alert(Number(uniqueCars[k][17]));
            if (
              Number(uniqueCars[k][17]) > 200 ||
              Number(uniqueCars[k][18]) > 200
            ) {
              //alert(Number(uniqueCars[k][17]));
              fillColor[0] = 255;
              fillColor[1] = 153;
              fillColor[2] = 153;
            }
            if (
              Number(uniqueCars[k][17]) <= 0 ||
              Number(uniqueCars[k][18]) <= 0
            ) {
              fillColor[0] = 0;
              fillColor[1] = 204;
              fillColor[2] = 102;
            }
          }
          if (headerrpt[10] == "Results") {
            //alert(headerrpt[10]);
            if (uniqueCars[k][10] == "F") {
              fillColor[0] = 255;
              fillColor[1] = 153;
              fillColor[2] = 153;
            }
            if (uniqueCars[k][10] == "P") {
              fillColor[0] = 0;
              fillColor[1] = 204;
              fillColor[2] = 102;
            }
          }
          var val = "";
          try {
            val = uniqueCars[k][g] + "";
          } catch (e) {
            val = "";
          }
          if (val == "null") {
            val = "";
          }
          row.push({
            content: val,
            colSpan: headerspace[g],
            styles: {
              halign: "center",
              fillColor: fillColor,
              textColor: [0, 0, 0],
              fontStyle: "bold",
              fontSize: 7,
              lineWidth: 0.1,
            },
          });
        }
        body.push(Object.values(row));
      }

      row = [];
      var total = "";
      var lineWidth = 0;
      for (g = 0; g < headerrpt.length; g++) {
        // tt += headerspace[g];
        //alert(tt);
        total = "";
        lineWidth = 0;

        row.push({
          content: total,
          colSpan: headerspace[g],
          styles: {
            halign: "center",
            fillColor: [255, 255, 255],
            textColor: [0, 0, 0],
            fontStyle: "bold",
            fontSize: 7,
            lineWidth: lineWidth,
          },
        });
      }
      body.push(Object.values(row));
      this.exceldata = data;
      this.headers = head;
      this.uniqueCars = body;
      this.title;
      var doc = new jsPDF();
      doc.autoTable({
        head: [head],
        body: body,
        // didDrawCell: function (data) {
        //   if (data.column.index === 5 && data.cell.section === "body") {
        //     var td = data.cell.raw;
        //     // Assuming the td cells have an img element with a data url set (<td><img src="data:image/jpeg;base64,/9j/4AAQ..."></td>)
        //     //var kenhaimg = new Image();
        //     var kenhaimg = td.getElementsByTagName("img")[0];
        //     kenhaimg.src = this.kenhaimglogo;
        //     var dim = data.cell.height - data.cell.padding("vertical");
        //     var textPos = data.cell.textPos;
        //     doc.addImage(kenhaimg, textPos.x, textPos.y, dim, dim);
        //   }
        // },
        startY: 0,
        margin: { horizontal: 1 },
        styles: {
          columnWidth: "wrap",
          fontSize: 7,
          overflow: "linebreak",
          cellWidth: "wrap",
        },
        columnStyles: {
          text: { columnWidth: "auto" },
          nil: { halign: "right" },
          tgl: { halign: "right" },
        },
        headerStyles: {
          halign: "left",
          fillColor: [255, 255, 255],
          textColor: [0, 0, 0],
        },
        bodyStyles: { lineColor: [0, 0, 0] },
        theme: "grid",
      });
      //doc.save(certno + "-" + oi.item.caseregno + ".pdf");
      //doc.output("dataurlnewwindow");
    },

    mydatenew(d) {
      let year = d.getFullYear();
      let month = d.getMonth() + 1;
      //alert("month" + month);
      let date = d.getDate();
      date = this.getv(date);
      month = this.getv(month);

      //const msec = d.getMilliseconds();
      const datetime = year + "-" + month + "-" + date;
      return datetime;
    },
    mydate(d, day, mon, yea) {
      let year = d.getFullYear() - yea;
      let month = d.getMonth() + 1 - mon;
      //alert("month" + month);
      let date = d.getDate() - day;
      date = this.getv(date);
      month = this.getv(month);

      //const msec = d.getMilliseconds();
      const datetime = year + "-" + month + "-" + date;
      return datetime;
    },
    getv(val) {
      if (val < 10) {
        val = "0" + val;
      }
      return val;
    },
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
    getmonth(d) {
      const monthNames = [
        "Jan",
        "Feb",
        "Mar",
        "Apr",
        "May",
        "Jun",
        "Jul",
        "Aug",
        "Sep",
        "Oct",
        "Nov",
        "Dec",
      ];
      return monthNames[d];
    },
    genrpt(genReportStarted) {
      this.printReport = true;
      genReportStarted =
        "Generating reports for " +
        this.mod +
        " module(" +
        this.report +
        ") started";
      this.genReportStarted = genReportStarted;
      //alert(this.report);
      var filter = "";
      var rpt = "";
      var route = [];
      try {
        route = this.route;
      } catch (e) {
        route = [];
        this.route = [];
      }

      var hour = "";
      try {
        hour = this.hour.trim();
      } catch (e) {
        hour = "";
        this.hour = "";
      }
      //alert(this.cargo.length);
      var cargo = [];
      try {
        cargo = this.cargo;
      } catch (e) {
        cargo = [];
        this.cargo = [];
      }
      var transporter = [];
      try {
        transporter = this.transporter;
      } catch (e) {
        transporter = [];
        this.transporter = [];
      }
      station;
      var station = [];
      try {
        station = this.wb;
      } catch (e) {
        station = [];
        this.station = [];
      }
      var state = [];
      try {
        state = this.state;
      } catch (e) {
        state = [];
        this.state = [];
      }

      var deck = [];
      try {
        deck = this.deck;
      } catch (e) {
        deck = [];
        this.deck = [];
      }

      var status = [];
      try {
        status = this.status;
      } catch (e) {
        status = [];
        this.status = [];
      }

      var reg = [];
      try {
        reg = this.reg;
      } catch (e) {
        reg = [];
        this.reg = [];
      }

      var fromdate = "";
      try {
        fromdate = this.mydatenew(new Date(this.fromdate));
      } catch (e) {
        fromdate = "";
      }
      var todate = "";

      try {
        todate = this.mydatenew(new Date(this.todate));
      } catch (e) {
        todate = "";
        this.todate = "";
      }
      var ampm = "AM";
      var ampm2 = "AM";
      //alert(this.fromhour);
      if (Number(this.fromhour.substring(0, 2)) > 12) {
        ampm = "PM";
      }

      if (Number(this.tohour.substring(0, 2)) > 12) {
        ampm2 = "PM";
      }
      fromdate =
        this.getmydate(this.fromdate) +
        " " +
        this.fromhour +
        ":00 " +
        ampm +
        "";
      todate =
        this.getmydate(this.todate) + " " + this.tohour + ":59 " + ampm2 + "";
      //alert(todate);
      if (fromdate.length > 0) {
        filter += "fromdate=" + fromdate + "&todate=" + todate;

        rpt += " From Date:" + fromdate + "And To Date=" + todate;
      }

      if (route.length > 0) {
        filter += "&route=";
        for (var r = 0; r < route.length; r++) {
          filter += "" + route[r] + ",";
          rpt += " and " + route[r];
        }
        filter = filter.substring(0, filter.length - 1);
      }
      //alert(this.cargo.length);
      if (cargo.length > 0) {
        filter += "&cargo=";
        rpt += " Cargo";
        for (r = 0; r < cargo.length; r++) {
          filter += "" + cargo[r] + ",";
          rpt += "  " + cargo[r] + " or ";
        }

        filter = filter.substring(0, filter.length - 1);
      }
      if (transporter.length > 0) {
        filter += "&transporter=";
        rpt += " Transporter";
        for (r = 0; r < transporter.length; r++) {
          filter += "" + transporter[r] + ",";
          rpt += "  " + transporter[r] + " or ";
        }

        filter = filter.substring(0, filter.length - 1);
      }

      if (station.length > 0) {
        filter += "&station=";
        rpt += " Station";
        for (r = 0; r < station.length; r++) {
          filter += "" + this.getStationcode(station[r]) + ",";
          rpt += "  " + station[r] + " or ";
        }

        filter = filter.substring(0, filter.length - 1);
      }

      if (deck.length > 0) {
        filter += "&weighbrigeside=";
        rpt += " Weighbridge Side:";
        for (r = 0; r < deck.length; r++) {
          filter += "" + deck[r] + ",";
          rpt += "  " + deck[r] + " or ";
        }

        filter = filter.substring(0, filter.length - 1);
      }

      if (state.length > 0) {
        filter += "&state=";
        rpt += " State";
        for (r = 0; r < state.length; r++) {
          var st = "";
          if (state[r] == "Charged") {
            st = "C";
          }
          if (state[r] == "Redistributed") {
            st = "R";
          }
          if (state[r] == "To Redistribute") {
            st = "T";
          }
          if (state[r] == "Special Release") {
            st = "S";
          }
          filter += "" + st + ",";
          rpt += "  " + state[r] + " or ";
        }

        filter = filter.substring(0, filter.length - 1);
      }

      if (status.length > 0) {
        filter += "&Status=";
        rpt += " Status";
        for (r = 0; r < status.length; r++) {
          var st = "";
          if (status[r] == "Legal") {
            st = "L";
          }
          if (status[r] == "Overloaded") {
            st = "O";
          }
          if (status[r] == "Warned") {
            st = "W";
          }

          filter += "" + st + ",";
          rpt += "  " + status[r] + " or ";
        }

        filter = filter.substring(0, filter.length - 1);
      }

      if (reg.length > 0) {
        filter += "&veh_reg=";
        rpt += " Registration:";
        for (r = 0; r < reg.length; r++) {
          filter += "" + reg[r] + ",";
          rpt += "  " + reg[r] + " or ";
        }
        if (reg.length % 2 !== 0) {
          filter += "" + reg[reg.length - 1] + "";
        } else {
          filter = filter.substring(0, filter.length - 1);
        }
      }

      this.filter = filter;
      this.rpt = rpt;
      this.orderData = [];
      if (this.report == "Census Report") {
        this.censusdata();
      }
      if (this.report == "Shifts") {
        this.shiftsreport();
      }
      if (this.report == "Daily Hour Data") {
        this.gethourlydata();
      }
      if (this.report == "Prosecution Court Fine List") {
        this.getcourtfine();
      }
      if (this.report == "Impounded And Prohibited") {
        this.getimpoundedandprohibited();
      }
      if (this.report == "Impounded And Overloaded") {
        this.getimpoundedandoverloaded();
      }
      if (this.report == "Habitual Offenders") {
        this.gethabitualoffenders();
      }

      if (this.report == "Daily Hour Statistics") {
        this.getstatistics();
      }

      //Excess Per Axel over 3000KG
      if (this.report == "Excess Per Axle") {
        this.getexcessperaxle(this.threshold);
      }
      if (this.report == "Daily Monthly Statistics") {
        this.getmonthlystatistics();
      }
      if (this.report == "Static vs HSWIM") {
        this.getstaticvshswim();
      }

      if (this.report == "HSWIM Configuration") {
        this.gethswimconfig();
      }

      if (this.report == "HSWIM Register") {
        this.gethswimregister();
      }

      if (this.report == "HSWIM vs Scale Evaluation") {
        this.getstaticvshswimeva();
      }

      if (this.report == "Vehicle Weighed per Axle") {
        this.getvehicleperaxle();
      }
      if (this.report == "Daily & Hourly Register") {
        this.gethourlystatistics();
      }
      if (this.report == "Weighbridge Register") {
        this.weighbridgeregister();
      }
      if (this.report == "Excess GVW & Axle") {
        this.excessgvwaxle();
      }

      if (this.report == "Wide Load") {
        this.wideload();
      }
      if (this.report == "Heavy Vehicle Inspection") {
        this.heavyload();
      }
      if (this.report == "Special Release") {
        this.specialrelease();
      }
      if (this.report == "Manual Weighing") {
        this.manualweighing();
      }
      if (this.report == "Scale Test") {
        this.scaletest();
      }
      if (this.report == "Axle Configurations") {
        this.axleConfig();
      }
    },
    getdatefilter() {
      //alert(this.mydate(new Date(), 0, 0, 0));
      this.todate = new Date(this.mydate(new Date(), 0, 0, 0));
      if (this.picked == "Day") {
        this.fromdate = new Date(this.mydate(new Date(), 1, 0, 0));
      }
      if (this.picked == "week") {
        this.fromdate = new Date(this.mydate(new Date(), 7, 0, 0));
      }
      if (this.picked == "month") {
        this.fromdate = new Date(this.mydate(new Date(), 0, 1, 0));
      }
      if (this.picked == "year") {
        this.fromdate = new Date(this.mydate(new Date(), 0, 0, 1));
      }
    },
    getrptstate() {
      console.log(this.module);
      //alert(this.report);
      this.hswim = false;
      this.showdata = false;
      this.showtickets = false;
      this.showhour = false;
      this.showdate = false;
      this.censusshowdata = false;
      this.showthreshold = false;

      if (this.report == "Census Report") {
        this.showdate = true;
        this.censusshowdata = true;
        this.showhour = true;
      }
      if (
        this.report == "Wide Load" ||
        this.report == "Heavy Vehicle Inspection"
      ) {
        this.showdate = true;

        this.showhour = true;
      }
      if (this.report == "Special Release") {
        this.showdate = true;
        this.showhour = true;
      }

      if (
        this.report == "Auto Generate Reports" ||
        this.report == "Daily Hour Data" ||
        this.report == "Daily Hour Statistics" ||
        this.report == "Daily Monthly Statistics" ||
        this.report == "Daily & Hourly Register" ||
        this.report == "Daily Summary" ||
        this.report == "Prosecution Court Fine List" ||
        this.report == "Vehicle Weighed per Axle" ||
        this.report == "Excess Per Axle" ||
        this.report == "Habitual Offenders"
      ) {
        this.showhour = true;
        this.showdate = true;
      }
      if (
        this.report == "Static vs HSWIM" ||
        this.report == "WIM Ticket" ||
        this.report == "HSWIM Configuration" ||
        this.report == "HSWIM Register" ||
        this.report == "HSWIM Hourly" ||
        this.report == "HSWIM vs Scale Evaluation"
      ) {
        this.showhour = true;
        this.showdate = true;
        this.hswim = true;
      }
      if (this.report == "Auto Generate Reports") {
        this.showdata = true;
        this.showhour = true;
        this.showdate = true;
      }
      if (this.report == "Manual Weighing") {
        this.showhour = true;
        this.showdate = true;
      }
      if (this.report == "Excess Per Axle") {
        this.showthreshold = true;
      }
      if (this.report == "Impounded And Prohibited") {
        this.showhour = true;
        this.showdate = true;
      }
      if (this.report == "Prosecution Court Fine List") {
        this.showhour = true;
        this.showdate = true;
      }
      if (this.report == "Prosecution Process List") {
        this.showhour = true;
        this.showdate = true;
      }
      if (this.report == "Impounded And Overloaded") {
        this.showhour = true;
        this.showdate = true;
      }
      if (this.report == "Scale Test") {
        this.showdate = true;
      }
      if (
        this.report == "Weighbridge Register" ||
        this.report == "Excess GVW & Axle"
      ) {
        this.showhour = true;
        this.showdate = true;
        this.showdata = true;
      }
    },
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
              <div class="card col-md-2"></div>
              <div class="card col-md-8 changebg">
                <div class="card-body">
                  <div class="card">
                    <div class="card-body">
                      <div class="col-sm-12">
                        <div class="input-group">
                          <div class="input-group-text col-sm-2 mb-3">
                            Module:
                          </div>
                          <div class="col-sm-10">
                            <multiselect
                              @input="getrptstate()"
                              v-model="mod"
                              :options="modules"
                              placeholder="Select Report"
                            ></multiselect>
                          </div>
                        </div>
                      </div>

                      <div class="col-sm-12">
                        <div class="input-group">
                          <div class="input-group-text col-sm-2 mb-3">
                            Report For:
                          </div>
                          <div class="col-sm-10">
                            <multiselect
                              @input="getrptstate()"
                              v-model="report"
                              :options="reports"
                              placeholder="Select Report"
                            ></multiselect>
                          </div>
                        </div>
                      </div>

                      <div class="col-sm-12">
                        <div class="input-group">
                          <div class="input-group-text col-sm-2">
                            <input
                              type="checkbox"
                              id="mail_checkbox"
                              v-model="mail_checkbox"
                            />
                            <label for="checkbox"></label>
                            Send Mail:
                          </div>
                          <input
                            class="form-control"
                            type="text"
                            placeholder="Type in Your Email Address"
                            v-model="email"
                          />
                        </div>
                      </div>
                    </div>
                  </div>

                  <!-- Station-->
                  <div class="card" v-show="stationshowdata">
                    <div class="card-body">
                      <div class="row">
                        <div class="col-sm-6">
                          <div class="input-group mb-3">
                            <div class="input-group-text col-sm-4">
                              Station:
                            </div>
                            <div class="col-sm-8">
                              <multiselect
                                v-model="wb"
                                :options="wbs"
                                placeholder="All"
                                :multiple="true"
                              ></multiselect>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!-- end -->
                  <!-- Threshold-->
                  <div class="card" v-show="showthreshold">
                    <div class="card-body">
                      <div class="row">
                        <div class="col-sm-6">
                          <div class="input-group mb-3">
                            <div class="input-group-text col-sm-4">
                              Threshold:
                            </div>
                            <div class="col-sm-8">
                              <input
                                v-model="threshold"
                                placholder="3000"
                                value="3000"
                                type="number"
                              />
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!-- end -->

                  <!--Date specification -->
                  <div class="card" v-show="showdate">
                    <div class="card-body">
                      <div class="row col-sm-12">
                        <div class="input-group">
                          <div class="col-sm-2">
                            <input
                              type="radio"
                              id="Day"
                              value="Day"
                              v-model="picked"
                              @change="getdatefilter()"
                            />
                            <label class="form-check-label">Day</label>
                          </div>

                          <div class="col-sm-2">
                            <input
                              type="radio"
                              id="week"
                              value="week"
                              v-model="picked"
                              @change="getdatefilter()"
                            />
                            <label>Week</label>
                          </div>

                          <div class="col-sm-2">
                            <input
                              type="radio"
                              id="month"
                              value="month"
                              v-model="picked"
                              @change="getdatefilter()"
                            />
                            <label for="checkbox">Month</label>
                          </div>

                          <div class="col-sm-2">
                            <input
                              type="radio"
                              id="year"
                              value="year"
                              v-model="picked"
                              @change="getdatefilter()"
                            />
                            <label>Year</label>
                          </div>

                          <div class="col-sm-2">
                            <input
                              type="radio"
                              id="specify"
                              value="specify"
                              v-model="picked"
                              @change="getdatefilter()"
                            />
                            <label for="checkbox">Specify</label>
                            {{ picked }}
                          </div>
                        </div>
                      </div>
                      <!-- Date Selection -->
                      <div class="row">
                        <div class="col-sm-6">
                          <div class="input-group">
                            <div class="input-group-text col-sm-12">
                              <div class="col-sm-3">From Date:</div>
                              <div class="col-sm-9">
                                <date-picker
                                  value="2021/05/17"
                                  v-model="fromdate"
                                  :first-day-of-week="1"
                                  lang="en"
                                  placeholder="Pick a Date"
                                ></date-picker>
                              </div>
                            </div>
                          </div>
                        </div>

                        <div class="col-sm-6">
                          <div class="input-group">
                            <div class="input-group-text col-sm-12">
                              <div class="col-sm-3">To Date:</div>
                              <div class="col-sm-9">
                                <date-picker
                                  value="2021/05/17"
                                  v-model="todate"
                                  :first-day-of-week="1"
                                  lang="en"
                                  placeholder="Pick a Date"
                                ></date-picker>
                              </div>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!--new Time specification-->
                  <div class="card" v-show="showhour">
                    <div class="card-body">
                      <div class="row">
                        <div class="col-sm-6">
                          <div class="input-group">
                            <div class="input-group-text col-sm-12">
                              <div class="col-sm-3">From Hour:</div>
                              <div class="col-sm-9">
                                <multiselect
                                  v-model="fromhour"
                                  :options="hours"
                                  placeholder="00:00"
                                  :allow-empty="false"
                                ></multiselect>
                              </div>
                            </div>
                          </div>
                        </div>

                        <div class="col-sm-6">
                          <div class="input-group-text col-sm-12 mb-3">
                            <div class="col-sm-3">To Hour:</div>
                            <div class="col-sm-9">
                              <multiselect
                                v-model="tohour"
                                :options="hours"
                                placeholder="23:59"
                                :allow-empty="false"
                              ></multiselect>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>

                  <div class="card" v-show="showtickets">
                    <div class="card-body">
                      <!-- Ticket Number  -->
                      <div class="col-sm-6">
                        <div class="input-group">
                          <div class="input-group-text col-sm-5">
                            Ticket No:
                          </div>

                          <input
                            style="font-size: 12px"
                            class="form-control"
                            list="TicketList"
                            id="TicketList"
                            placeholder="Enter Ticket Number"
                            v-model="ticket"
                          />
                          <datalist
                            id="TicketList"
                            v-for="ticket in tickets"
                            :key="ticket"
                          >
                            <option
                              v-for="ticket in tickets"
                              :key="ticket"
                              :value="ticket"
                            ></option>
                          </datalist>
                        </div>
                      </div>
                    </div>
                  </div>

                  <!--Registration specification -->

                  <div class="card" v-show="showdata">
                    <div class="card-body">
                      <div class="row">
                        <div class="col-sm-6">
                          <div class="input-group mb-3">
                            <div class="input-group-text col-sm-3">Cargo:</div>
                            <div class="col-sm-9">
                              <multiselect
                                v-model="cargo"
                                :options="cargos"
                                placeholder="All"
                                :multiple="true"
                              ></multiselect>
                            </div>
                          </div>
                        </div>

                        <!-- registration -->
                        <div class="col-sm-6">
                          <div class="input-group mb-3">
                            <div class="input-group-text col-sm-4">
                              Registration:
                            </div>
                            <div class="col-sm-8">
                              <multiselect
                                v-model="reg"
                                :options="regs"
                                placeholder="All"
                                :multiple="true"
                              ></multiselect>
                            </div>
                          </div>
                        </div>
                      </div>

                      <!-- Row 2 -->

                      <div class="row">
                        <div class="col-sm-6">
                          <div class="input-group mb-3">
                            <div class="input-group-text col-sm-3">Status:</div>
                            <div class="col-sm-9">
                              <multiselect
                                v-model="status"
                                :options="statuses"
                                placeholder="ALL"
                                :multiple="true"
                              ></multiselect>
                            </div>
                          </div>
                        </div>

                        <!-- registration -->
                        <div class="col-sm-6">
                          <div class="input-group mb-3">
                            <div class="input-group-text col-sm-4">
                              Weighbridge Side:
                            </div>
                            <div class="col-sm-8">
                              <multiselect
                                v-model="deck"
                                :options="decks"
                                placeholder="All"
                                :multiple="true"
                              ></multiselect>
                            </div>
                          </div>
                        </div>
                      </div>

                      <div class="row">
                        <div class="col-sm-6">
                          <div class="input-group mb-3">
                            <div class="input-group-text col-sm-3">Users:</div>
                            <div class="col-sm-9">
                              <multiselect
                                v-model="user"
                                :options="users"
                                placeholder="ALL"
                                :multiple="true"
                              ></multiselect>
                            </div>
                          </div>
                        </div>

                        <div class="col-sm-6">
                          <div class="input-group mb-3">
                            <div class="input-group-text col-sm-4">
                              Transporter:
                            </div>
                            <div class="col-sm-8">
                              <multiselect
                                v-model="transporter"
                                :options="transporters"
                                placeholder="All"
                                :multiple="true"
                              ></multiselect>
                            </div>
                          </div>
                        </div>
                      </div>

                      <div class="row">
                        <div class="col-sm-6">
                          <div class="input-group mb-3">
                            <div class="input-group-text col-sm-3">Shift:</div>
                            <div class="col-sm-9">
                              <multiselect
                                v-model="shift"
                                :options="shifts"
                                placeholder="ALL"
                                :multiple="true"
                              ></multiselect>
                            </div>
                          </div>
                        </div>

                        <div class="col-sm-6">
                          <div class="input-group mb-3">
                            <div class="input-group-text col-sm-4">
                              Axle Type:
                            </div>
                            <div class="col-sm-8">
                              <multiselect
                                v-model="axle"
                                :options="axles"
                                placeholder="ALL"
                                :multiple="true"
                              ></multiselect>
                            </div>
                          </div>
                        </div>
                      </div>
                      <div class="row">
                        <div class="col-sm-6">
                          <div class="input-group mb-3">
                            <div class="input-group-text col-sm-3">State:</div>
                            <div class="col-sm-9">
                              <multiselect
                                v-model="state"
                                :options="states"
                                placeholder="ALL"
                                :multiple="true"
                              ></multiselect>
                            </div>
                          </div>
                        </div>

                        <!-- end -->
                      </div>
                    </div>
                  </div>

                  <!-- dfdf -->
                  <div class="card" v-show="censusshowdata">
                    <div class="card-body">
                      <div class="row">
                        <div class="col-sm-6">
                          <div class="input-group mb-3">
                            <div class="input-group-text col-sm-3">Route:</div>
                            <div class="col-sm-9">
                              <multiselect
                                v-model="route"
                                :options="routes"
                                placeholder="ALL"
                                :multiple="true"
                              ></multiselect>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>

                  <!-- ////////////0000 -->
                  <div class="card" v-show="hswim">
                    <div class="card-body">
                      <div class="col-sm-6">
                        <div class="input-group">
                          <div class="input-group-text col-sm-4">
                            HSWIM Lane:
                          </div>
                          <div class="col-sm-7">
                            <multiselect
                              v-model="lane"
                              :options="lanes"
                              class="form-control"
                              placeholder="ALL"
                              :multiple="true"
                            ></multiselect>
                          </div>
                        </div>
                      </div>

                      <div class="row col-sm-12">
                        <div class="input-group">
                          <div class="col-sm-2">
                            <input
                              type="checkbox"
                              class="form-check-input"
                              v-model="Calledin"
                            />
                            <label class="form-check-label">Called In</label>
                          </div>

                          <div class="col-sm-2">
                            <input
                              type="checkbox"
                              class="form-check-input"
                              v-model="Overloaded"
                            />
                            <label>Overloaded</label>
                          </div>

                          <div class="col-sm-2">
                            <input
                              type="checkbox"
                              class="form-check-input"
                              v-model="month_checkbox"
                            />
                            <label for="checkbox">Overspeeding</label>
                          </div>

                          <div class="col-sm-2">
                            <input
                              type="checkbox"
                              class="form-check-input"
                              id="year_checkbox"
                              v-model="Trucks"
                            />
                            <label for="checkbox">Trucks Only</label>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div style="height: 0; width: 0; overflow: hidden">
                    <canvas
                      id="supercool-canvas"
                      width="1200"
                      height="600"
                    ></canvas>
                  </div>
                  <!-- ///buttons -->
                  <div class="row">
                    <div class="col-sm-10 text-right">{{ report }}</div>

                    <div class="col-sm-2">
                      <b-button
                        pill
                        variant="outline-primary"
                        v-b-modal.modal-Print
                        @click="genrpt(genReportStarted)"
                        style="margin-right: 10px"
                      >
                        Generate</b-button
                      >
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
    <b-modal id="modal-Print" :title="report" hide-footer size="bg" centered>
      <reportdet
        :title="report"
        :reportfor="report"
        :orderData="orderData"
        :pl="pl"
        :headers="headers"
        :uniqueCars="uniqueCars"
        :shome="showme"
        :printedpdf="printedpdf"
        :rpt="rpt"
        v-show="showme"
        :exceldata="exceldata"
      ></reportdet>
    </b-modal>
  </Layout>
</template>
<style scoped>
.changebg {
  background-color: rgb(240, 240, 240);
}
</style>
