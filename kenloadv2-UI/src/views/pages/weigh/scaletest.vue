<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";
import jsPDF from "jspdf";

import Swal from "sweetalert2";
import axios from "axios";
//import html2canvas from "html2canvas";
//import rptheader from "@/components/report/header.js";
import reportdet from "@/components/report/header";
import changeweighbridge from "@/components/widgets/weigh/changeweighbridge";

import Deckweights from "@/components/widgets/weigh/deckweights";

export default {
  page: {
    title: "Scale Test",
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
    Deckweights,
    changeweighbridge,
  },
  data() {
    return {
      stoped2: true,
      deckA: 0,
      deckB: 0,
      deckC: 0,
      deckD: 0,
      buttona: true,
      buttonb: true,
      buttonc: true,
      buttond: true,
      title: "Scale Test",
      items: [
        {
          text: "USER: Admin",
        },
        {
          text: "Scale Test List",
          active: true,
        },
      ],
      orderDatanew: null,
      pl: "",
      orderData: [
        // {
        //   id: "1",
        //   scaleid: "",
        //   scaledatetime: "2021-02-09 18:18:46.0",
        //   scalename: "ATHIRIVER MOMBASA BOUND B",
        //   scalevehreg: "MERCEDES BENZ",
        //   scaleweighedby: "Michael@micna.co.ke",
        //   scaledeck1: "2000",
        //   scaledeck2: "2000",
        //   scaledeck3: "2000",
        //   scaledeck4: "2000",
        //   result: "Passed",
        // },
      ],

      station: "",
      viewitem: "",
      viewScaleTest: false,
      addScaleTest: false,
      stationname: "",
      Weighbridge: "ATHIRIVER MOMBASA BOUND A",
      Load: "",
      username: window.$userid,
      id: "",
      headers: null,
      uniqueCars: null,
      showme: true,
      totalRows: 1,
      currentPage: 1,
      perPage: 10,
      pageOptions: [
        1,
        2,
        5,
        10,
        25,
        50,
        100,
        200,
        300,
        500,
        1000,
        2000,
        3000,
        4000,
        5000,
        6000,
        9000,
        10000,
        20000,
      ],
      roadauthoritys: ["KeNHA", "KURA"],
      filter: null,
      filterOn: [],
      sortBy: "id",
      sortDesc: true,
      name: "",
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
          key: "scaledatetime",
          label: "Date Time",
          sortable: true,
        },
        {
          key: "station",
          label: "Station",
          sortable: true,
        },
        {
          key: "scalename",
          label: "Scale Name",
          sortable: true,
        },
        {
          key: "scalevehreg",
          label: "Load Used",
          sortable: true,
        },
        {
          key: "scaleweighedby",
          label: "Weighed By",
          sortable: true,
        },
        {
          key: "scaledeck1",
          label: "Deck 1",
          sortable: true,
        },
        {
          key: "scaledeck2",
          label: "Deck 2",
          sortable: true,
        },
        {
          key: "scaledeck3",
          label: "Deck 3",
          sortable: true,
        },
        {
          key: "scaledeck4",
          label: "Deck 4",
          sortable: true,
        },
        {
          key: "result",
          label: "Results",
          sortable: true,
        },

        "action",
      ],
    };
  },
  watch: {
    viewScaleTest(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Opened Scale Test list:${newValue},${this.viewitem}`,
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
    addScaleTest(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Took a Scale Test:${newValue} value(${this.scalevehreg})`,
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
  updated() {
    this.Weighbridge = window.$weighb;
  },
  mounted() {
    // Show a pop up if it works
    ////alert(location.host);

    // Set the initial number of items
    // console.log("*mounted", this.$refs);
    this.viewScaleTest = true;
    this.stationname = window.$stationname;
    this.station = window.$stationcode;
    this.Weighbridge = window.$weighb;

    axios
      .get(
        window.$http +
          `ClusterWeighbridges/search?cluster_code=` +
          this.station,
        { headers: window.$headers }
      )
      .then((response) => {
        this.stationname = response.data[0].cluster_name;
      });
    this.updatearray();
    this.focusInput();
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
      //alert(window.$http + `scaletest?search?limit=` + this.perPage);
      axios
        .get(window.$http + `scaletest?search?limit=` + this.perPage, {
          headers: window.$headers,
        })
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
    focusInput() {
      // this.$refs.load.focus();
    },
    async getticket(
      id,
      result,
      station,
      weighbridge,
      load,
      username,
      datetime,
      deck1,
      deck2,
      deck3,
      deck4
    ) {
      var weigbri = "";
      try {
        weigbri = await axios
          .get(
            window.$http + `ClusterWeighbridges/search?cluster_code=` + station,
            {
              headers: window.$headers,
            }
          )
          .then((response) => {
            return response.data[0].cluster_name;
          })
          .catch(() => {});
      } catch (Exception) {
        console.log();
      }

      try {
        var suser = await axios
          .get(window.$http + `users/search?email=` + username, {
            headers: window.$headers,
          })
          .then((resp) => {
            return resp.data;
            //alert(suser[0].fullnames);
          })
          .catch((e) => {
            Swal.fire("Users - Ticket" + e);
            //this.errors.push(e);
          });
      } catch (Exception) {
        console.log();
      }
      //let pdfName = 'WeighbridgeReceipt';
      var doc = new jsPDF();
      const cdatetime = this.getformatdate(new Date());
      //This is a key for printing
      doc.setFillColor(255, 255, 255);
      doc.roundedRect(5, 5, 200, 40, 1, 1, "FD");
      doc.setTextColor(0, 0, 0);
      doc.setFontSize(10);
      //doc.addFont("Tahoma", "Tahoma", "Tahoma");
      doc.setFont("Tahoma");
      doc.text(60, 10, "KENYA NATIONAL HIGHWAYS AUTHORITY");
      doc.text(70, 15, "Quality Highways, Better Connections");
      doc.text(80, 25, "Axle Load Control");
      doc.text(150, 30, "Print Date: " + cdatetime);
      var res = "Passed";
      if (result == "F") {
        res = "Failed";
      }
      doc.text(10, 40, "Scale Test (Multideck)");
      doc.text(70, 40, "Weighbridge Name:" + weigbri);
      doc.setFontSize(14);
      doc.addFont("Tahoma", "Tahoma", "bold");
      doc.setFont("Tahoma", "Tahoma", "bold");
      doc.text(180, 40, res);
      doc.setFontSize(12);

      doc.setFillColor(255, 255, 255);

      doc.roundedRect(5, 47, 43, 5, 1, 1, "FD");
      doc.roundedRect(50, 47, 100, 5, 1, 1, "FD");

      doc.roundedRect(5, 55, 43, 5, 1, 1, "FD");
      doc.roundedRect(50, 55, 100, 5, 1, 1, "FD");

      doc.roundedRect(5, 60, 43, 5, 1, 1, "FD");
      doc.roundedRect(50, 60, 100, 5, 1, 1, "FD");

      doc.roundedRect(5, 65, 43, 5, 1, 1, "FD");
      doc.roundedRect(50, 65, 100, 5, 1, 1, "FD");

      doc.roundedRect(5, 70, 43, 5, 1, 1, "FD");
      doc.roundedRect(50, 70, 100, 5, 1, 1, "FD");

      doc.roundedRect(5, 80, 40, 15, 1, 1, "FD");
      doc.roundedRect(50, 80, 40, 15, 1, 1, "FD");
      doc.roundedRect(95, 80, 40, 15, 1, 1, "FD");
      doc.roundedRect(140, 80, 40, 15, 1, 1, "FD");

      doc.roundedRect(5, 93, 40, 5, 1, 1, "FD");
      doc.roundedRect(50, 93, 40, 5, 1, 1, "FD");
      doc.roundedRect(95, 93, 40, 5, 1, 1, "FD");
      doc.roundedRect(140, 93, 40, 5, 1, 1, "FD");

      doc.text(7, 51, "Ticket No.: ");
      doc.text(55, 51, "ATMB" + id);

      doc.text(7, 59, "Weighbridge: ");
      doc.text(55, 59, "" + weighbridge);

      doc.text(7, 64, "Load Used: ");
      doc.text(55, 64, "" + load);
      var operator = "";
      try {
        operator = suser[0].fullnames.toUpperCase();
      } catch (Exception) {
        console.log();
      }
      doc.text(7, 69, "User Name: ");
      doc.text(55, 69, "" + operator);

      doc.text(7, 74, "Date Time: ");
      doc.text(55, 74, "" + this.getformatdate(datetime));

      doc.setFontSize(30);

      doc.text(15, 90, "" + deck1);
      doc.text(58, 90, "" + deck2);
      doc.text(105, 90, "" + deck3);
      doc.text(150, 90, "" + deck4);

      doc.setFontSize(8);

      doc.text(16, 96, "DECK 1 [KG]");
      doc.text(62, 96, "DECK 2 [KG]");
      doc.text(106, 96, "DECK 3 [KG]");
      doc.text(152, 96, "DECK 4 [KG]");

      doc.line(5, 77, 205, 77);
      doc.line(5, 100, 205, 100);
      const logo = require("@/assets/images/client.png");
      var imgLogo = new Image();
      imgLogo.src = logo;
      // doc.addImage(imgLogo, "PNG", 10, 10);
      doc.addImage(imgLogo, "PNG", 10, 7, 40, 30);

      doc.output("dataurlnewwindow");
    },
    /**
     * Export to CSV
     */
    getrpt() {
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
        datetime: row.datetime,
        Weighbridge_Name: row.scalename,
        Load_Used: row.scalevehreg,
        User_Name: row.scaleweighedby,
        Deck_1: row.scaledeck1,
        Deck_2: row.scaledeck2,
        Deck_3: row.scaledeck3,
        Deck_4: row.scaledeck4,
        Result: row.result,
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
          ////alert(escaped);
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
    },
    handleSubmit() {
      console.log("Error on submit");
    },
    printpdf(pl) {
      this.pl = pl;
      this.viewitem = "Viewed " + this.scalevehreg;
      const data = this.orderData.map((row) => ({
        ID: row.id,
        datetime: row.scaledatetime,
        Weighbridge_Name: row.scalename,
        Load_Used: row.scalevehreg,
        User_Name: row.scaleweighedby,
        Deck_1: row.scaledeck1,
        Deck_2: row.scaledeck2,
        Deck_3: row.scaledeck3,
        Deck_4: row.scaledeck4,
        Result: row.result,
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
    edit(index, myid, name) {
      //alert(rolename);
      this.editmode = true;
      this.modaltitle = "Edit Scale Test";
      this.id = myid;
      this.name = name;
      this.myindex = index;
      this.focusInput();

      //this.$refs.search.focus();
      //$("#search").focus();
    },
    editrec() {
      if (this.name.trim() == "") {
        Swal.fire("Please enter  Scale Test Name!");
        return;
      }

      //alert(this.myindex);
      this.orderData[this.myindex].id = this.id;
      this.orderData[this.myindex].scalename = this.scalename;
      this.orderData[this.myindex].prefix = this.prefix;
      Swal.fire({
        position: "center",
        icon: "success",
        title: "Your work has been saved",
        showConfirmButton: false,
        timer: 1500,
      });
    },
    getv(val) {
      if (val < 10) {
        val = "0" + val;
      }
      return val;
    },
    getmonth(d) {
      // const monthNames = [
      //   "January",
      //   "February",
      //   "March",
      //   "April",
      //   "May",
      //   "June",
      //   "July",
      //   "August",
      //   "September",
      //   "October",
      //   "November",
      //   "December",
      // ];
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
    getformatdate(mydate) {
      let d = new Date(mydate);
      let year = d.getFullYear();
      let month = this.getmonth(d.getMonth());
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
    getcurrentdate(mydate) {
      let d = new Date(mydate);
      let year = d.getFullYear();
      let month = d.getMonth() + 1;
      let date = d.getDate();
      date = this.getv(date);
      month = this.getv(month);

      let hour = d.getHours();
      let min = d.getMinutes();
      let sec = d.getSeconds();
      hour = this.getv(hour);
      min = this.getv(min);
      sec = this.getv(sec);

      //const msec = d.getMilliseconds();
      const datetime =
        year + "-" + month + "-" + date + "T" + hour + ":" + min + ":" + sec;
      return datetime;
    },
    position() {
      if (this.Load.trim() == "") {
        Swal.fire("Please enter  Load Used!");
        return;
      }
      if (this.deckA == 0) {
        Swal.fire("Please  Assign Deck 1!");
        return;
      }
      if (this.deckB == 0) {
        Swal.fire("Please  Assign Deck 2!");
        return;
      }
      if (this.deckC == 0) {
        Swal.fire("Please Assign Deck 3!");
        return;
      }
      if (this.deckD == 0) {
        Swal.fire("Please  Assign Deck 4!");
        return;
      }
      var str =
        "<div> Deck A:" +
        this.deckA +
        "</div>" +
        "Deck B:" +
        this.deckB +
        "<br>" +
        "Deck C: " +
        this.deckC +
        "<br>" +
        "Deck D: " +
        this.deckD +
        "<br>";
      Swal.fire({
        title: "Do you want to save \n" + "?",
        html: "<pre>" + str + "</pre>",
        icon: "info",
        showCancelButton: true,
        confirmButtonColor: "#006400",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, Save it!",
      }).then((res) => {
        if (res.value) {
          //const msec = d.getMilliseconds();
          const datetime = this.getcurrentdate(new Date());
          // +
          // "." +
          // msec;
          var results = "F";
          var result = "Failed";
          if (
            this.deckA == this.deckB &&
            this.deckB == this.deckC &&
            this.deckC == this.deckD
          ) {
            result = "Passed";
            results = "P";
          }

          const data = {
            scaleid: datetime,
            scaledatetime: datetime,
            station: window.$stationcode,
            scalename: this.Weighbridge,
            scalevehreg: this.Load.trim(),
            scaleweighedby: window.$userid,
            scaledeck1: this.deckA,
            scaledeck2: this.deckB,
            scaledeck3: this.deckC,
            scaledeck4: this.deckD,
            result: results,
          };
          axios
            .post(window.$http + `scaletest/`, data, {
              headers: window.$headers,
            })
            .then((response) => {
              // JSON responses are automatically parsed.
              console.log(response.data);
              this.addScaleTest = true;
              var orderid = this.orderData.length + 1;
              this.orderData.push({
                id: orderid,
                datetime: datetime,
                station: this.$stationcode,
                scalename: this.Weighbridge,
                registration: this.Load,
                username: this.username,
                deck1: this.deckA,
                deck2: this.deckB,
                deck3: this.deckC,
                deck4: this.deckD,
                result: result,
              });

              Swal.fire({
                position: "center",
                icon: "success",
                title: "Your work has been saved",
                showConfirmButton: false,
                timer: 1500,
              }).then((e) => {
                this.getticket(
                  response.data.id,
                  response.data.result,
                  response.data.station,
                  response.data.scalename,
                  response.data.scalevehreg,
                  response.data.scaleweighedby,
                  response.data.scaledatetime,
                  response.data.scaledeck1,
                  response.data.scaledeck2,
                  response.data.scaledeck3,
                  response.data.scaledeck4
                );
                this.clearvalues();
                this.updatearray();
                Swal.close(e);
              });
            })
            .catch((e) => {
              Swal.fire("" + e);
            });
        }
      });
    },
    deleterec(index, id, name) {
      //alert(rolename);
      this.id = id;
      this.name = name;
      Swal.fire({
        title: "Are you sure, you want to delete " + this.name + "?",
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
          Swal.fire("Deleted!", this.name + " has been deleted.", "success");
        }
      });
    },
    clearvalues() {
      this.Load = "";
      this.deckA = 0;
      this.deckB = 0;
      this.deckC = 0;
      this.deckD = 0;
      this.buttona = true;
      this.buttonb = true;
      this.buttonc = true;
      this.buttond = true;
      this.modaltitle = "Add Scale Test";
    },
    setdeck(deck) {
      if (deck == "deck1") {
        if (this.$refs.Deckweights.deck1 == 0) {
          Swal.fire("Please  Assign Number greater than zero!");
          return;
        }
        this.buttona = false;
        this.deckA = Number(this.$refs.Deckweights.deck1);
        //this.deck1 = Number(2000);
      }
      if (deck == "deck2") {
        if (this.$refs.Deckweights.deck2 == 0) {
          Swal.fire("Please  Assign Number greater than zero!");
          return;
        }
        this.buttonb = false;
        this.deckB = Number(this.$refs.Deckweights.deck2);
      }
      if (deck == "deck3") {
        if (this.$refs.Deckweights.deck3 == 0) {
          Swal.fire("Please  Assign Number greater than zero!");
          return;
        }
        this.buttonc = false;
        this.deckC = Number(this.$refs.Deckweights.deck3);
      }
      if (deck == "deck4") {
        if (this.$refs.Deckweights.deck4 == 0) {
          Swal.fire("Please  Assign Number greater than zero!");
          return;
        }
        this.buttond = false;
        this.deckD = Number(this.$refs.Deckweights.deck4);
      }
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
                      @click="getrpt()"
                    >
                      Export to CSV
                    </button>
                  </div>

                  <div class="col-sm-2">
                    <button
                      @click="printpdf('p')"
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
                      v-b-modal.modal-scale
                      class="
                        btn btn-primary
                        waves-effect waves-light
                        mdi-file-pdf
                      "
                    >
                      Take Scale Test
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
                                    @change="updatearray()"
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
                                v-for="screen in data.item.screens"
                                :key="screen"
                                >{{ screen }} <br
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
                              --->
                            <template v-slot:cell(result)="data">
                              <div
                                class="
                                  badge
                                  bg-pill bg-soft-success
                                  font-size-12
                                "
                                :class="{
                                  'bg-soft-danger': data.item.result === 'F',
                                }"
                              >
                                {{ data.item.result }}
                              </div>
                            </template>

                            <template v-slot:cell(action)="data">
                              <button
                                v-b-tooltip.hover
                                title="view"
                                class="
                                  btn btn-primary
                                  waves-effect waves-light
                                  uil-export
                                "
                                @click="
                                  getticket(
                                    data.item.id,
                                    data.item.result,
                                    data.item.station,
                                    data.item.scalename,
                                    data.item.scalevehreg,
                                    data.item.scaleweighedby,
                                    data.item.scaledatetime,
                                    data.item.scaledeck1,
                                    data.item.scaledeck2,
                                    data.item.scaledeck3,
                                    data.item.scaledeck4
                                  )
                                "
                              >
                                View
                              </button>
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
    <b-modal
      id="modal-scale"
      :title="modaltitle"
      hide-footer
      size="xl"
      centered
    >
      <changeweighbridge />
      <div class="row">
        <Deckweights id="Deckweights" ref="Deckweights" :stoped2="stoped2" />
        <Deckimages />
        <div class="col-sm-12">
          <div class="card">
            <div class="card-body changebg">
              <div class="card">
                <div class="card-body changebg">
                  <h5 class="font-size-14 mb-4">
                    <i class="mdi mdi-arrow-right text-primary me-1"></i>
                    Take Scale Weights
                  </h5>
                  <div class="input-group">
                    <div class="input-group-text col-sm-3">Deck 1:</div>
                    <div class="input-group-text col-sm-3">Deck 2:</div>
                    <div class="input-group-text col-sm-3">Deck 3:</div>

                    <div class="input-group-text col-sm-3">Deck 4:</div>

                    <div class="input-group-text col-sm-3">
                      <input
                        class="form-control"
                        type="text"
                        disabled="disabled"
                        placeholder="0"
                        v-model="deckA"
                      />
                    </div>
                    <div class="input-group-text col-sm-3">
                      <input
                        class="form-control"
                        type="text"
                        disabled="disabled"
                        placeholder="0"
                        v-model="deckB"
                      />
                    </div>
                    <div class="input-group-text col-sm-3">
                      <input
                        class="form-control"
                        type="text"
                        disabled="disabled"
                        placeholder="0"
                        v-model="deckC"
                      />
                    </div>
                    <div class="input-group-text col-sm-3">
                      <input
                        class="form-control"
                        type="text"
                        disabled="disabled"
                        placeholder="0"
                        v-model="deckD"
                      />
                    </div>

                    <div class="input-group-text col-sm-3">
                      <button
                        v-show="buttona"
                        v-b-tooltip.hover
                        title="view"
                        class="btn btn-primary uil-export"
                        @click="setdeck('deck1')"
                      >
                        Assign Deck 1
                      </button>
                    </div>
                    <div class="input-group-text col-sm-3">
                      <button
                        v-show="buttonb"
                        v-b-tooltip.hover
                        title="view"
                        class="btn btn-primary uil-export"
                        @click="setdeck('deck2')"
                      >
                        Assign Deck 2
                      </button>
                    </div>
                    <div class="input-group-text col-sm-3">
                      <button
                        v-show="buttonc"
                        v-b-tooltip.hover
                        title="view"
                        class="btn btn-primary uil-export"
                        @click="setdeck('deck3')"
                      >
                        Assign Deck 3
                      </button>
                    </div>

                    <div class="input-group-text col-sm-3">
                      <button
                        v-show="buttond"
                        v-b-tooltip.hover
                        title="view"
                        class="btn btn-primary uil-export"
                        @click="setdeck('deck4')"
                      >
                        Assign Deck 4
                      </button>
                    </div>
                  </div>
                </div>
              </div>

              <div class="card">
                <div class="card-body changebg">
                  <h5 class="font-size-14 mb-4">
                    <i class="mdi mdi-arrow-right text-primary me-1"></i>
                    Scale Test Details
                  </h5>
                  <div class="row" id="print">
                    <form @submit.prevent="handleSubmit">
                      <div class="row">
                        <div class="input-group">
                          <div class="input-group-text col-sm-12">
                            Station: {{ stationname }} ({{ station }})
                          </div>
                        </div>
                        <div class="input-group">
                          <div class="input-group-text col-sm-4">
                            Weighbridge:
                          </div>

                          <input
                            class="form-control"
                            type="text"
                            disabled="disabled"
                            placeholder="weighbridge"
                            v-model="Weighbridge"
                          />
                          <b-button v-b-modal.modal-Edit pill variant="primary"
                            >...
                          </b-button>
                        </div>
                        <div class="input-group">
                          <div class="input-group-text col-sm-4">
                            Load Used:
                          </div>

                          <input
                            ref="load"
                            id="load"
                            class="form-control"
                            type="text"
                            placeholder="Name"
                            v-model="Load"
                          />
                        </div>
                      </div>
                    </form>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <!----@click="position(), $bvModal.hide('modal-1') --->
      <div class="row">
        <div class="col-sm-10">
          <button
            v-b-tooltip.hover
            title="view"
            class="btn btn-danger"
            @click="clearvalues()"
          >
            Clear
          </button>
        </div>
        <div class="col-sm-2">
          <button
            v-b-tooltip.hover
            title="view"
            class="btn btn-primary uil-export"
            @click="position()"
          >
            Save Test
          </button>
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
//#print { //height: 11in; //width: 8.5in; //}
<style scoped>
.changebg {
  background-color: rgb(240, 240, 240);
}
</style>