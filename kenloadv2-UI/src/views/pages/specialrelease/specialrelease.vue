<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import axios from "axios";
import Swal from "sweetalert2";
import jsPDF from "jspdf";

import Tahoma from "@/assets/fonts/tahoma.ttf";

//import { tableData } from "./dataAdvancedtable";

export default {
  page: {
    title: "Yard List",
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
      title: "Yard List",
      items: [
        {
          text: "USER: " + window.$userid,
        },
        {
          text: "Special Release Vehicles",
          active: true,
        },
      ],
      editmode: false,
      spreleasedata: [],
      yardlist: [],
      excessload: 0,
      description: "",
      reason: "",
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
      imae: require("@/assets/images/redbutton.jpg"),
      imae2: require("@/assets/images/redbutton.jpg"),
      image: [this.imae, this.imae2],
      sptitle: "Enter Reason for Special Release ",
      orderData: [],
      orderDataticket: [],
      orderDatacase: [],
      addSpecialRelease: false,
      viewSpecialRelease: false,
      editSpecialRelease: false,
      deleteSpecialRelease: false,
      totalRows: 1,
      currentPage: 1,
      casedetailsid: "",
      perPage: 50,
      pageOptions: [5, 10, 25, 50, 100, 200],
      filter: null,
      filterOn: [],
      sortBy: "datetime",
      sortDesc: true,
      fields: [
        {
          key: "id",
          label: "#",
          sortable: true,
        },
        {
          key: "vehreg",
          label: "Registration",
          sortable: true,
        },
        {
          key: "station",
          label: "Station",
          sortable: true,
        },
        {
          key: "datetime",
          label: "Date In",
          sortable: true,
        },
        {
          key: "yardtimeout",
          label: "Date Out",
          sortable: true,
        },
        {
          key: "inspected",
          label: "Inspected",
          sortable: true,
        },

        {
          key: "spreleaseno",
          label: "Special Release NO",
          sortable: true,
        },
        {
          key: "release",
          label: "Release",
        },
      ],
      fields2: [
        {
          key: "id",
          label: "#",
          sortable: true,
        },
        {
          key: "wbrg_ticket_no",
          label: "Ticket No.",
          sortable: true,
        },
        {
          key: "wbrg_station",
          label: "Station",
          sortable: true,
        },
        {
          key: "wbrg_ticket_date",
          label: "Date Time",
          sortable: true,
        },
        {
          key: "veh_reg",
          label: "Registration",
          sortable: true,
        },
        "anprpic",
        {
          key: "anpr_veh_reg",
          label: "ANPR",
          sortable: true,
        },
        {
          key: "anprcheck",
          label: "ANPR Check",
          sortable: true,
        },
        {
          key: "timetaken",
          label: "Time Taken (Secs)",
          sortable: true,
        },
        {
          key: "wbrg_ticket_operator",
          label: "User",
          sortable: true,
        },
        {
          key: "wbrg_tx_code",
          label: "Transporter",
          sortable: true,
        },
        {
          key: "wbrg_product",
          label: "cargo",
          sortable: true,
        },
        {
          key: "wbrg_ticket_axel",
          label: "Axle",
          sortable: true,
        },
        {
          key: "axle1_weight",
          label: "Deck A[KG]",
          sortable: true,
        },
        {
          key: "axle2_weight",
          label: "Deck B[KG]",
          sortable: true,
        },
        {
          key: "axle3_weight",
          label: "Deck C[KG]",
          sortable: true,
        },
        {
          key: "axle4_weight",
          label: "Deck D[KG]",
          sortable: true,
        },
        {
          key: "gvw",
          label: "GVW [KG]",
          sortable: true,
        },
        {
          key: "wbrg_ticket_excessweight",
          label: "Exceess [KG]",
          sortable: true,
        },
        {
          key: "wbrg_ticket_status",
          label: "Status",
          sortable: true,
        },
        {
          key: "wbrg_ticket_state",
          label: "State",
          sortable: true,
        },

        "Action",
      ],
      fields3: [
        {
          key: "id",
          sortable: true,
          label: "Id",
        },
        {
          key: "caseid",
          sortable: true,
          label: "Probition No.",
        },
        {
          key: "alongroad",
          label: "alongroad",
          sortable: true,
        },
        {
          key: "caseregno",
          sortable: true,
          label: "Vehicle Number",
        },
        {
          key: "casedatetime",
          sortable: true,
          label: "Date Time",
        },
        {
          key: "caseexcessload",
          sortable: true,
          label: "Excess Load",
        },
        {
          key: "casedocs",
          sortable: true,
          label: "Documents",
        },
        {
          key: "status",
          sortable: true,
          label: "Status",
        },
        {
          key: "createdby",
          sortable: true,
          label: "Prosecutor",
        },
        {
          key: "prohibit",
          label: "Action",
        },
      ],
    };
  },
  watch: {
    viewSpecialRelease(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Opened yard list:${newValue}`,
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
    addSpecialRelease(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Added Special Release:${newValue}`,
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
    editSpecialRelease(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Edited Special Release:${newValue}`,
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
    deleteSpecialRelease(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Deleted special release:${newValue},id:${this.id}`,
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
    this.updatearray();
    this.totalRows = this.items.length;
    this.viewSpecialRelease = true;
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
        .get(window.$http + `yardlist/search?status=I`, {
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
    getimage(wbrg_ticket_date, wbrg_ticket_no, pos) {
      var year = wbrg_ticket_date.substring(0, 4);
      var month = wbrg_ticket_date.substring(5, 7);
      var day = wbrg_ticket_date.substring(8, 10);
      var hour = wbrg_ticket_date.substring(11, 13);
      var dir = year + month + "/" + day + "/" + hour + "/";
      // var theUrl =
      //   "http://"+window.$localurl+"/kenload/dashboard/getbase64.php?filename=F" +
      //   wbrg_ticket_no +
      //   ".jpg&dir=" +
      //   dir +
      //   "&string=http://192.168.4.57/jpg/image.jpg?size=3";
      var theUrl =
        window.$url +
        "/kenload/dashboard/imgs/" +
        dir +
        pos +
        wbrg_ticket_no +
        ".jpg";
      this.img = theUrl;
      //alert(theUrl);
      return theUrl;
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
    updatecasedocs(casedocid, caseid, doc, casedetailsid) {
      var data = {
        casedocid: casedocid,
        caseid: caseid,
        docname: doc,
        docdate: this.getcurrentdate(),
        createdby: window.$userid,
        casedetailsid: casedetailsid,
        cancelled: "N",
        printed: "N",
      };
      //alert("Ncasedocsid" + Ncasedocsid + "  casedetailsid" + this.id);
      axios
        .post(window.$http + `casedocs/`, data, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          Swal.close();
          this.addSpecialRelease = true;
          //this.$root.$emit("bv::hide::modal", "modal-Doc");
          Swal.fire({
            position: "center",
            icon: "success",
            title: response.data + ". Special Release Created! " + doc,
            showConfirmButton: false,
            timer: 1500,
          });
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Case Document " + doc + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
    },
    getspecial(sp) {
      var doc = new jsPDF();
      //This is a key for printing
      //alert(oi.caseid);

      doc.setFillColor(255, 255, 255);

      doc.setTextColor(0, 0, 0);
      doc.setFontSize(10);
      doc.addFont("Tahoma", "Tahoma", "Tahoma");
      doc.setFont("Tahoma");

      doc.setFontSize(13);
      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 10, "KeNHA/MTCE/ALC/F3");

      const kenhaimg = require("@/assets/images/kenyashield.png");
      var imgLogo = new Image();
      imgLogo.src = kenhaimg;

      const gvtimg = require("@/assets/images/kenhaauthority.png");
      var imgLogo2 = new Image();
      imgLogo2.src = gvtimg;

      doc.addImage(kenhaimg, "PNG", 85, 10, 25, 23.5);
      doc.addImage(gvtimg, "PNG", 65, 34.5);
      doc.setFontSize(9);
      doc.setFont("Tahoma", "", "normal");
      doc.text(
        25,
        58,
        "Barabara Plaza, Block A&C, Jomo Kenyatta International Airport (JKIA), Off Airport South Road, along Mazao Road."
      );
      doc.text(
        20,
        62,
        "P.O Box 49712-00100 Nairobi, Tel 020 - 4954000 / 0700 423 606 Email dg@kenha.co.ke Website: www.kenha.co.ke"
      );

      doc.setFontSize(13);
      doc.setFont("Tahoma", "", "bold");
      doc.text(140, 75, "NO. " + sp.specialid);

      doc.text(75, 88, "INTERNAL MEMO");
      doc.text(
        10,
        92,
        "-----------------------------------------------------------------------------------------------------------------------------"
      );

      doc.text(10, 100, "FROM:");
      doc.setFont("Tahoma", "", "normal");
      doc.text(40, 100, "Manager - " + sp.weighbridge);

      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 105, "THROUGH:");
      doc.setFont("Tahoma", "", "normal");
      doc.text(40, 105, "Officer Commanding Station - " + sp.weighbridge);

      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 110, "TO:");
      doc.setFont("Tahoma", "", "normal");
      doc.text(
        40,
        110,
        sp.driver + " / " + sp.transporter + " (Driver / Transporter)"
      );

      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 115, "REF:");
      doc.setFont("Tahoma", "", "normal");
      doc.text(40, 115, sp.specialid);

      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 120, "DATE:");
      doc.setFont("Tahoma", "", "normal");
      doc.text(
        40,
        120,
        "Date: " +
          this.getdate(sp.specialreleasedate) +
          "  Time: " +
          this.gettime(sp.specialreleasedate)
      );

      doc.text(
        10,
        128,
        "-----------------------------------------------------------------------------------------------------------------------------"
      );

      doc.setFont("Tahoma", "", "bold");
      doc.text(20, 140, "RE : SPECIAL PERMISSION TO EXIT THE WEIGHBRIDGE");

      doc.setFontSize(11);
      doc.setFont("Tahoma", "", "normal");
      let theText = doc.splitTextToSize(
        "Vehicle Registration Number " +
          sp.regNo +
          " of Transporter " +
          sp.transporter +
          " address " +
          sp.transporteradd +
          " is hereby granted special permission to leave the weighbridge without correcting the excess load.\nReason:",
        180
      );

      doc.text(10, 150, theText);
      doc.setFont("Tahoma", "", "bold");

      theText = doc.splitTextToSize(
        sp.reason.toUpperCase() +
          "\n-------------------------------------\n" +
          sp.description +
          "\n\nExcess Load " +
          sp.excessload +
          "KG",
        180
      );
      doc.text(10, 170, theText);

      doc.setFont("Tahoma", "", "normal");

      doc.text(10, 200, "Prohibition Order Number: " + sp.prohibitionno);
      doc.text(10, 205, "Weigh Ticket Number: " + sp.ticketno);

      //DATE FUNCTION HERE
      doc.text(
        10,
        210,
        "Weigh Date: " +
          this.getdate(sp.weighdate) +
          " Time:" +
          this.gettime(sp.weighdate) +
          ""
      );

      doc.setFontSize(13);
      doc.setFont("Tahoma", "", "bold");
      doc.text(140, 230, "Signature:..................................");
      doc.text(140, 240, "Name:.........................................");

      doc.text(147, 250, "OFFICER IN CHARGE");
      doc.text(140, 255, sp.weighbridge);
      //alert();
      doc.output("dataurlnewwindow");
    },
    /**
     * Load correction memo
     */
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
    getdate(dy) {
      let d = new Date(dy);
      let year = d.getFullYear();
      let month = this.getmonth(d.getMonth());
      let date = d.getDate();
      date = this.getv(date);
      //month = this.getv(month);

      //const msec = d.getMilliseconds();
      const datetime = date + "/" + month + "/" + year;
      return datetime;
    },
    gettime(dy) {
      let d = new Date(dy);

      let hour = d.getHours();
      let min = d.getMinutes();
      let sec = d.getSeconds();
      hour = this.getv(hour);
      min = this.getv(min);
      sec = this.getv(sec);

      //const msec = d.getMilliseconds();
      const datetime = hour + ":" + min + ":" + sec;
      return datetime;
    },
    ticketdate() {
      let d = new Date();
      let year = d.getFullYear();
      let month = d.getMonth() + 1;
      month = this.getv(month);
      //const msec = d.getMilliseconds();
      const datetime = year + "" + month + "";
      return datetime;
    },
    getcurrentdate() {
      let d = new Date();
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
    getv(val) {
      if (val < 10) {
        val = "0" + val;
      }
      return val;
    },
    padLeadingZeros(num, size) {
      var s = num + "";
      while (s.length < size) s = "0" + s;
      return s;
    },
    updatelist(yardlist) {
      this.editmode = false;
      this.reason = "";
      this.description = "";
      this.sptitle = "Reason for Special Release " + yardlist.vehreg;
      this.yardlist = yardlist;
      this.orderDataticket = [];
      this.orderDatacase = [];

      axios
        .get(
          window.$http +
            `weighbridgetransactions/search?limit=1&wbrg_ticket_no=` +
            this.yardlist.wbtno,
          { headers: window.$headers }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          console.log(response);
          this.orderDataticket = response.data;
          this.excessload = response.data[0].wbrg_ticket_excessweight;
          //alert(this.excessload);
        });

      axios
        .get(
          window.$http + `casedetails/search?caseid=` + this.yardlist.caseid,
          { headers: window.$headers }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          //console.log(response);
          this.orderDatacase = response.data;
          this.casedetailsid = response.data[0].id;
          //alert(response.data[0].casedocs[0]);
          var dd = response.data[0].casedocs.length;

          if (dd == 0) {
            Swal.fire({
              position: "center",
              icon: "error",
              title: "Prohibition document not raise ",
              showConfirmButton: false,
              timer: 1500,
            }).then(() => {
              this.$root.$emit("bv::hide::modal", "modal-1");
            });
          } else {
            this.$root.$emit("bv::show::modal", "modal-1");
          }
        });
    },
    view(yardlist, specialReleaseNo) {
      this.yardlist = yardlist;
      //alert(window.$http + `casedetails/search?caseid=` + this.yardlist.caseid);
      axios
        .get(
          window.$http + `specialrelease/report?specialid=` + specialReleaseNo,
          { headers: window.$headers }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          console.log(response);
          // alert(response.data[0].specialid);
          this.getspecial(response.data[0]);
        });
    },
    edit(yardlist, specialReleaseNo) {
      this.editmode = true;
      this.sptitle = "Reason for Special Release (Edit) for " + yardlist.vehreg;
      this.yardlist = yardlist;
      //alert(window.$http + `casedetails/search?caseid=` + this.yardlist.caseid);
      axios
        .get(
          window.$http + `specialrelease/search?specialid=` + specialReleaseNo,
          { headers: window.$headers }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          console.log(response);
          // alert(response.data[0].specialid);
          this.spreleasedata = response.data[0];
          this.reason = this.spreleasedata.reason;
          this.description = this.spreleasedata.otherreason;
          this.$root.$emit("bv::show::modal", "modal-1");
        });
    },
    async viewticket(nodata) {
      // alert(nodata.id);
      // var dat = {
      //     wbrg_ticket_no: this.WBTNO,
      //     axle_actualweight: orderData[i].actual,
      //     axle_permissibleweight: orderData[i].permissible,
      //     axle_time: this.weighdate,
      //     axle_number: axleno,
      //     axle_group: grp[i],
      //     axle_grouping: grp[i],
      //     Weighbridgetransactionsid: id,
      //     timestamp: this.getcurrentdate(),
      //   };
      var orderData = [];
      var orderData3 = [];
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      // var sp = [];
      // axios
      //   .get(
      //     window.$http + `specialrelease/search?wbtno=` + nodata.wbrg_ticket_no,
      //     {
      //       headers: window.$headers,
      //     }
      //   )
      //   .then((response) => {
      //     sp = response;
      //   })
      //   .catch((e) => {
      //     Swal.fire({
      //       position: "center",
      //       icon: "error",
      //       title: "Special Release Document " + e,
      //       showConfirmButton: true,
      //     }).then((e) => {
      //       Swal.close(e);
      //     });

      //     //throw e;
      //   });
      axios
        .get(
          window.$http + `users/search?email=` + nodata.wbrg_ticket_operator,
          {
            headers: window.$headers,
          }
        )
        .then((resp) => {
          var suser = resp.data;
          //alert(suser[0].fullnames);
          axios
            .get(window.$http + `axleweights/search?wbtno=` + nodata.id, {
              headers: window.$headers,
            })
            .then((res) => {
              //console.log(res);
              orderData = res.data;
              if (
                nodata.wbrg_ticket_type == "L" ||
                nodata.wbrg_ticket_type == "S"
              ) {
                orderData3 = res.data;
              }
              var weigbri = "";

              axios
                .get(
                  window.$http +
                    `ClusterWeighbridges/search?cluster_code=` +
                    nodata.wbrg_station,
                  {
                    headers: window.$headers,
                  }
                )
                .then((response) => {
                  weigbri = response.data[0].cluster_name;

                  var reweigh = [];
                  var status = "";
                  var state = "";
                  reweigh = nodata.wbrg_ticket_no.split("_");

                  if (nodata.wbrg_ticket_status == "P") {
                    status = "Incomplete";
                  }
                  if (nodata.wbrg_ticket_status == "O") {
                    status = "Overloaded";
                  }
                  if (nodata.wbrg_ticket_status == "L") {
                    status = "Legal";
                  }
                  if (nodata.wbrg_ticket_status == "X") {
                    status = "Cancelled";
                  }
                  if (nodata.wbrg_ticket_status == "W") {
                    status = "Warned";
                  }
                  if (nodata.wbrg_ticket_state == "S") {
                    state = "Special Release";
                  }
                  if (nodata.wbrg_ticket_state == "T") {
                    state = "To Redistribute";
                  }
                  if (nodata.wbrg_ticket_state == "R") {
                    state = "Redistribute";
                  }
                  if (nodata.wbrg_ticket_state == "C") {
                    state = "Charged";
                  }
                  //this.tno = this.prefix + "000000" + this.nplate;
                  //let pdfName = 'WeighbridgeReceipt';
                  var doc = new jsPDF();
                  //This is a key for printing
                  doc.addFileToVFS("tahoma.ttf", Tahoma);
                  doc.setFillColor(255, 255, 255);
                  doc.roundedRect(5, 5, 200, 38, 1, 1, "FD");

                  doc.setTextColor(0, 0, 0);
                  doc.setFontSize(12);
                  doc.setFont("Tahoma", "", "bold");
                  doc.text(60, 10, "KENYA NATIONAL HIGHWAYS AUTHORITY");
                  doc.text(70, 15, "Quality Highways, Better Connections");
                  doc.text(80, 25, "Axle Load Control");
                  doc.setFont("Tahoma", "", "normal");
                  doc.setFontSize(10);

                  doc.text(
                    150,
                    30,
                    "Print Date: " + this.getcurrentdate(new Date() + "")
                  );

                  doc.text(15, 40, "Weighbridge Ticket");
                  doc.text(70, 40, "Weighbridge Name:" + weigbri);

                  doc.addFont("Tahoma", "Tahoma", "bold");
                  doc.text(180, 35, state);
                  doc.setFont("Tahoma", "", "normal");
                  doc.setFontSize(12);
                  doc.text(180, 40, status);
                  //alert(this.tagged);

                  if (nodata.wbrg_ticket_tagged == 1) {
                    doc.setFontSize(8);
                    doc.text(180, 45, "Tagged");
                  }
                  //alert();

                  doc.setFontSize(10);
                  doc.text(5, 47, "Ticket No: " + nodata.wbrg_ticket_no);
                  //alert(reweigh[1]);
                  if (Number(reweigh[1]) > 0) {
                    doc.text(85, 47, "Re-Weighed:" + reweigh[1]);
                  }
                  if (
                    nodata.reliefvehreg != "" ||
                    nodata.reliefvehreg != "null"
                  ) {
                    doc.text(
                      70,
                      47,
                      "Relief Load from: " + nodata.reliefvehreg
                    );
                  }
                  doc.text(
                    135,
                    47,
                    "Weighed Date & Time:" +
                      this.getcurrentdate(nodata.wbrg_ticket_date)
                  );
                  doc.setFillColor(255, 255, 255);
                  var si = 70;

                  if (orderData.length == 3) {
                    si = 77;
                  }
                  if (orderData.length == 4) {
                    si = 82;
                  }
                  try {
                    if (orderData3.length >= 2) {
                      si = si + orderData3.length * 6;
                    }
                  } catch (e) {
                    console.log(e);
                  }
                  doc.roundedRect(5, 49, 200, si + 17, 0, 0, "FD");
                  //alert(orderData.length);

                  //doc.addImage(imgLogo.src, "PNG", 10, 10);
                  const logo = require("@/assets/images/client.png");
                  var imgLogo = new Image();
                  imgLogo.src = logo;
                  doc.addImage(imgLogo, "PNG", 10, 7, 40, 30);
                  var operator = suser[0].fullnames.toUpperCase();
                  // try {
                  //   var operator = await axios
                  //     .get(
                  //       window.$http +
                  //         `users/search?email=` +
                  //         nodata.wbrg_ticket_operator,
                  //       {
                  //         headers: window.$headers,
                  //       }
                  //     )
                  //     .then((response) => {
                  //       return response.data;
                  //     })
                  //     .catch(() => {});
                  // } catch (e) {
                  //   console.log(e);
                  // }
                  doc.text(
                    6,
                    53,
                    "Vehicle Registration No.: " + nodata.veh_reg
                  );
                  doc.addFont("bold");
                  doc.text(83.5, 53, "Transporter:" + nodata.wbrg_tx_code);
                  doc.text(160, 58, "Axle Type:" + nodata.wbrg_ticket_axel);
                  doc.text(
                    21.5,
                    58,
                    "Vehicle Make: " + nodata.wbrg_ticket_make
                  );
                  doc.text(
                    80,
                    58,
                    "Source/Origin:" + nodata.wbrg_ticket_source
                  );
                  doc.text(154, 62, "Scale Operator:");
                  doc.text(154, 66, operator);
                  doc.text(32, 62, "Driver: " + nodata.wbrg_ticket_driver);
                  doc.text(
                    84,
                    62,
                    "Destination:" + nodata.wbrg_ticket_destination
                  );
                  doc.text(
                    26.5,
                    66,
                    "Trailer No: " + nodata.wbgr_ticket_trailer_no
                  );
                  doc.text(91.5, 66, "Cargo:" + nodata.wbrg_product);
                  doc.line(5, 68, 205, 68);
                  doc.addFont("bold");
                  let lane1 = 81 - 8;
                  let lane2 = 85 - 8;
                  let lane3 = 87 - 8;
                  let lane4 = 91 - 8;
                  let inc = 6;
                  var str = "";
                  //var downline = 97;
                  let g = 1;
                  let axleoverl = 0;

                  if (
                    nodata.wbrg_ticket_type == "L" ||
                    nodata.wbrg_ticket_type == "S"
                  ) {
                    doc.setFont("Tahoma", "", "bold");
                    doc.text(10, lane1, "Item");
                    doc.text(25, lane1, "No of Axle Wheel");
                    doc.text(25, lane2, "Type and Distance");
                    doc.text(60, lane1, "Permissible");
                    doc.text(60, lane2, "Weight[KG]");
                    doc.text(88, lane1, "Permissible");
                    doc.text(88, lane2, "+Tolerance[KG]");
                    doc.text(120, lane1, "Actual");
                    doc.text(120, lane2, "Weight[KG]");
                    doc.text(140, lane1, "Overload");
                    doc.text(140, lane2, "[KG]");
                    doc.text(163, lane1, "Result");
                    doc.text(183, lane1, "PDF");
                    doc.setLineWidth(0.5);
                    doc.line(5, lane3, 205, lane3);
                    doc.text(85, lane4, "Single Axle Load");
                    doc.setLineWidth(0.5);
                    doc.setFont("Tahoma", "", "bold");

                    for (let i = 0; i < orderData3.length; i++) {
                      doc.line(5, lane3 + inc, 205, lane3 + inc);

                      doc.text(10, lane4 + inc, "Axle");
                      doc.text(25, lane4 + inc, g + "");

                      let leng = 0;
                      str = orderData3[i].permissible + "";
                      //alert(str.length);
                      leng = 8 - str.length;

                      doc.text(
                        58 + leng,
                        lane4 + inc,
                        "" + orderData3[i].permissible
                      );

                      doc.text(
                        84 + leng,
                        lane4 + inc,
                        "" + orderData3[i].permissible + ""
                      );
                      doc.text(120, lane4 + inc, "" + orderData3[i].actual);
                      doc.text(140, lane4 + inc, "" + orderData3[i].overload);
                      doc.text(163, lane4 + inc, "" + orderData3[i].result);
                      doc.text(183, lane4 + inc, "" + orderData3[i].pdf);
                      inc += 6;
                      axleoverl += Number(orderData3[i].overload);
                      g++;
                      doc.setLineWidth(0.1);
                    }
                    doc.setLineWidth(0.5);
                    doc.line(5, lane3 + inc, 205, lane3 + inc);

                    // alert(inc);
                    inc += g + 4;
                    lane1 += inc;
                    lane2 += inc;
                    lane3 += inc;
                    lane4 += inc;
                  }
                  doc.setFont("Tahoma", "", "bold");
                  doc.setFontSize(10);
                  doc.text(10, lane1, "Item");
                  doc.text(25, lane1, "No of Axle Wheel");
                  doc.text(25, lane2, "Type and Distance");
                  doc.text(60, lane1, "Permissible");
                  doc.text(60, lane2, "Weight[KG]");
                  doc.text(88, lane1, "Permissible");
                  doc.text(88, lane2, "+Tolerance[KG]");
                  doc.text(120, lane1, "Actual");
                  doc.text(120, lane2, "Weight[KG]");
                  doc.text(140, lane1, "Overload");
                  doc.text(140, lane2, "[KG]");
                  doc.text(163, lane1, "Result");
                  doc.text(183, lane1, "PDF");
                  doc.setLineWidth(0.5);
                  doc.line(5, lane3, 205, lane3);
                  doc.text(85, lane4, "Axle Group Load");
                  doc.setLineWidth(0.5);
                  doc.setFont("Tahoma", "", "normal");

                  inc = 6;
                  //var downline = 97;
                  g = 1;
                  axleoverl = 0;
                  var gvwperm = 0;
                  for (let i = 0; i < orderData.length; i++) {
                    doc.line(5, lane3 + inc, 205, lane3 + inc);

                    doc.text(10, lane4 + inc, "Group");
                    doc.text(25, lane4 + inc, g + "");

                    let leng = 0;
                    str = orderData[i].permissible + "";
                    //alert(str.length);
                    leng = 8 - str.length;

                    doc.text(
                      62 + leng,
                      lane4 + inc,
                      "" + orderData[i].axle_permissibleweight
                    );
                    var actual = Number(orderData[i].axle_actualweight);
                    var perm = Number(orderData[i].axle_permissibleweight);
                    var tollerance = (perm * 5) / 100 + perm;
                    doc.text(90 + leng, lane4 + inc, "" + tollerance + " 5%");
                    var overload = 0;
                    if (actual - tollerance > 0) {
                      overload = actual - tollerance;
                    }
                    var result = "Legal";
                    if (overload > 0) {
                      result = "Overload";
                    }
                    var pdf = "0";
                    let po = actual / perm;
                    pdf = Math.pow(4.5, po);
                    doc.text(
                      120,
                      lane4 + inc,
                      "" + orderData[i].axle_actualweight
                    );
                    doc.text(140, lane4 + inc, "" + overload);
                    doc.text(163, lane4 + inc, "" + result);
                    doc.text(
                      183,
                      lane4 + inc,
                      "" + new Intl.NumberFormat().format(pdf)
                    );
                    inc += 6;
                    axleoverl += Number(overload);
                    g++;
                    gvwperm += perm;
                    doc.setLineWidth(0.1);
                  }
                  inc -= 4;
                  doc.setLineWidth(1.0);
                  doc.line(5, lane4 + inc, 205, lane4 + inc);
                  doc.text(90, lane4 + inc + 4, "Vehicle Load");
                  doc.setLineWidth(0.5);
                  doc.line(5, lane4 + inc + 6, 205, lane4 + inc + 6);
                  doc.text(15, lane4 + inc + 10, "GVW");

                  doc.text(60, lane4 + inc + 10, gvwperm + "");
                  doc.text(88, lane4 + inc + 10, gvwperm + " 0%");
                  doc.text(
                    120,
                    lane4 + inc + 10,
                    "" + nodata.wbrg_ticket_grossweight
                  );
                  doc.text(
                    140,
                    lane4 + inc + 10,
                    nodata.wbrg_ticket_gvwload + ""
                  );
                  doc.text(
                    163,
                    lane4 + inc + 10,
                    nodata.wbrg_ticket_gvwload > 0 ? "Overload" : "Legal"
                  );
                  var gv = Number(nodata.wbrg_ticket_grossweight);

                  pdf = "0";
                  let po2 = gv / gvwperm;
                  pdf = Math.pow(4.5, po2);
                  doc.text(
                    183,
                    lane4 + inc + 10,
                    " " + new Intl.NumberFormat().format(pdf)
                  );
                  doc.setLineWidth(0.5);
                  doc.line(5, lane4 + inc + 12, 205, lane4 + inc + 12);
                  doc.text(
                    6,
                    lane4 + inc + 17,
                    "Legend:GVW:Gross Vehicle Weight, Wheels:S, Single Conventional Tyre D:Double,W Super Single (wide), Pavement Damage Factor"
                  );
                  doc.text(6, lane4 + inc + 21, "[PDF]");

                  doc.text(
                    5,
                    lane4 + inc + 27,
                    "Note: Axle group weights were checked, but axle weights were not checked. One or more axles in axle group can be overloaded even if the "
                  );

                  doc.text(
                    5,
                    lane4 + inc + 31,
                    "total weight of the axle group is reported as legal. It means the responsibility of the owner to ensure that the vehicle is correctly loaded."
                  );

                  doc.setFillColor(238, 238, 238);

                  doc.addFont("ComicSans", "Comic Sans", "bold");
                  doc.setFont("Tahoma");
                  doc.setFontSize(13);
                  if (axleoverl > nodata.wbrg_ticket_gvwload) {
                    doc.rect(5, lane4 + inc + 35, 200, 7, "F");
                    doc.text(
                      40,
                      lane4 + inc + 40,
                      "Excess Group Axle Weight.Remedial action required: Redistribute or offload " +
                        axleoverl +
                        "[KG]"
                    );
                  }
                  if (nodata.wbrg_ticket_gvwload > axleoverl) {
                    doc.rect(5, lane4 + inc + 35, 200, 7, "F");
                    doc.text(
                      40,
                      lane4 + inc + 40,
                      "Excess Group Vehicle Weight.Remedial action required: offload " +
                        nodata.wbrg_ticket_gvwload +
                        "[KG]"
                    );
                  }
                  var wbrg_ticket_directionarea = "";
                  try {
                    wbrg_ticket_directionarea =
                      nodata.wbrg_ticket_directionarea.trim();
                  } catch (e) {
                    console.log(e);
                    wbrg_ticket_directionarea = "";
                  }
                  doc.setFont("Tahoma", "", "bold");
                  doc.setFontSize(10);
                  doc.text(40, lane4 + inc + 50, wbrg_ticket_directionarea);

                  //alert();
                  // var reason = sp.data[0].reason;
                  // var description = sp.data[0].otherreason;
                  // var reasondescription =
                  //   "Special Release:" + reason + "; " + description;

                  //alert(nodata.id);
                  // const logofront =
                  //   "http://"+window.$localurl+"/kenload/dashboard/imgs/202110/19/16/FATMBA202110000156_2.jpg";
                  // const overview =
                  //   "http://"+window.$localurl+"/kenload/dashboard/imgs/202110/19/16/OATMBA202110000156_2.jpg";
                  // var imgfront = new Image();
                  // var imgoverview = new Image();
                  // imgfront.src = logofront;
                  // imgoverview.src = overview;

                  // doc.addImage(imgfront, "PNGs", 10, lane4 + inc + 60, 80, 60);
                  // doc.addImage(imgoverview, "PNG", 120, lane4 + inc + 60, 80, 60);
                  //alert("");

                  // var imgoverview = "";

                  var year = nodata.wbrg_ticket_date.substring(0, 4);
                  var month = nodata.wbrg_ticket_date.substring(5, 7);
                  var day = nodata.wbrg_ticket_date.substring(8, 10);
                  var hour = nodata.wbrg_ticket_date.substring(11, 13);
                  var dir = year + month + "/" + day + "/" + hour + "/";

                  //alert(theUrl);
                  // this.url +
                  // "/kenload/dashboard/getimg.php?string=http://192.168.5.53/jpg/image.jpg?size=3&filename=" +
                  // "O" +
                  // this.WBTNO +
                  // ".jpg&dir=" +
                  // this.dir;
                  //alert(theUrl);

                  const logFileText = async () => {
                    try {
                      var ur = window.$url;
                      // var href = window.location.href;
                      // var arr = href.split("/");
                      // var arr2 = arr[2].split(":");
                      // var ref = arr[0] + "//" + arr2[0] + ":82";
                      // var ur = window.$url;
                      // ur = ref;
                      // var theUrl =
                      //   ur +
                      //   "/kenload/dashboard/getbase64img.php?string=" +
                      //   window.$url +
                      //   "/kenload/dashboard/imgs/" +
                      //   dir +
                      //   "F" +
                      //   nodata.wbrg_ticket_no +
                      //   ".jpg";
                      // console.log(theUrl);
                      //alert(theUrl);
                      var theUrl =
                        ur +
                        "/kenload/dashboard/getbase64.php?filename=F" +
                        nodata.wbrg_ticket_no +
                        ".jpg&dir=" +
                        dir +
                        "&string=";
                      //alert(theUrl);

                      var theUrl2 =
                        ur +
                        "/kenload/dashboard/getbase64.php?filename=O" +
                        nodata.wbrg_ticket_no +
                        ".jpg&dir=" +
                        dir +
                        "&string=";

                      const response = await fetch(theUrl);
                      var imgfront = await response.text();

                      const response2 = await fetch(theUrl2);
                      var imgfront2 = await response2.text();

                      doc.addImage(
                        imgfront,
                        "PNG",
                        10,
                        lane4 + inc + 60,
                        90,
                        60
                      );

                      doc.addImage(
                        imgfront2,
                        "PNG",
                        110,
                        lane4 + inc + 60,
                        90,
                        60
                      );
                      //alert("");
                    } catch (e) {
                      console.log(e);
                    }

                    // doc.save(
                    //   nodata.wbrg_ticket_no + "-" + nodata.veh_reg + ".pdf"
                    // );

                    // doc.output("dataurlnewwindow");
                    //doc.output("datauristring");
                    //doc.autoPrint();
                    window.open(
                      doc.output("datauristring"),
                      "_blank",
                      "toolbar=no,status=no,menubar=no,scrollbars=no,resizable=yes,modal=yes,top=200,left=350,width=1200,height=800"
                    );

                    Swal.close();
                  };
                  logFileText();
                  // doc.save(nodata.wbrg_ticket_no + "-" + nodata.veh_reg + ".pdf");
                  // doc.output("dataurlnewwindow");
                  // Swal.close();
                })
                .catch(() => {});
            })
            .catch((e) => {
              Swal.fire("Ticket Error -" + e);
              //this.errors.push(e);
            });
        })
        .catch((e) => {
          Swal.fire("Users - Ticket" + e);
          //this.errors.push(e);
        });
    },
    goToSrdoc(edit) {
      //this.$router.push("/srdoc");
      var mycasid = "SP";
      var doc = "Special Release";
      // alert(
      //   window.$http +
      //     `specialrelease/max?specialid=` +
      //     mycasid +
      //     window.$stationcode +
      //     this.ticketdate()
      // );
      if (edit) {
        this.spreleasedata.reason = this.reason;
        this.spreleasedata.otherreason = this.description;

        axios
          .put(
            window.$http + `specialrelease/` + this.spreleasedata.id,
            this.spreleasedata,
            {
              headers: window.$headers,
            }
          )
          .then(() => {
            // JSON responses are automatically parsed.

            Swal.close();
            this.$root.$emit("bv::hide::modal", "modal-1");
            Swal.fire({
              position: "center",
              icon: "success",
              title: "Edit Special Release ! " + this.spreleasedata.specialid,
              showConfirmButton: false,
              timer: 1500,
            }).then(() => {
              this.updatearray();
            });
          })
          .catch((e) => {
            Swal.fire({
              position: "center",
              icon: "error",
              title: "Special Release Document " + e,
              showConfirmButton: true,
            }).then((e) => {
              Swal.close(e);
            });
          });
        return;
      }
      axios
        .get(
          window.$http +
            `specialrelease/max?specialid=` +
            mycasid +
            window.$stationcode +
            this.ticketdate(),
          { headers: window.$headers }
        )
        .then((response) => {
          //alert(response.data);
          let ticketmax = Number(response.data);
          ticketmax++;
          var ticketno = this.padLeadingZeros(ticketmax, 6);
          var Ncasedocsid =
            mycasid + window.$stationcode + this.ticketdate() + ticketno;
          var data = {
            specialid: Ncasedocsid,
            vehiclereg: this.yardlist.vehreg,
            reason: this.reason,
            otherreason: this.description,
            wbrg_ticket_no: this.yardlist.wbtno,
            realeasedate: this.getcurrentdate(),
            realeasedby: window.$userid,
            excessload: this.excessload,
            case_document_id: this.yardlist.caseid,
            station: window.$stationcode,
          };

          axios
            .post(window.$http + `specialrelease/`, data, {
              headers: window.$headers,
            })
            .then(() => {
              // JSON responses are automatically parsed.
              this.yardlist.spreleaseno = Ncasedocsid;
              this.addSpecialRelease = true;
              axios
                .put(
                  window.$http + `yardlist/` + this.yardlist.id,
                  this.yardlist,
                  { headers: window.$headers }
                )
                .then(() => {
                  //saveticket();
                  // this.updatecasedocs(Ncasedocsid);
                  this.editSpecialRelease = true;
                  this.updatecasedocs(
                    Ncasedocsid,
                    this.yardlist.caseid,
                    doc,
                    this.casedetailsid
                  );
                  Swal.close();
                  this.$root.$emit("bv::hide::modal", "modal-1");
                  Swal.fire({
                    position: "center",
                    icon: "success",
                    title: " Special Release ! " + Ncasedocsid,
                    showConfirmButton: false,
                    timer: 1500,
                  }).then(() => {
                    this.updatearray();
                  });
                })
                .catch((e) => {
                  Swal.fire({
                    position: "center",
                    icon: "error",
                    title: "Yard list " + e,
                    showConfirmButton: true,
                  }).then((e) => {
                    Swal.close(e);
                  });

                  //throw e;
                });
            })
            .catch((e) => {
              Swal.fire({
                position: "center",
                icon: "error",
                title: "Special Release Document " + e,
                showConfirmButton: true,
              }).then((e) => {
                Swal.close(e);
              });

              //throw e;
            });
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
    getloadcase(oi) {
      var driver = 0;
      var owner = 0;
      var transporter = 0;
      var loader = 0;
      var parkingfee = 0;

      var row = "";
      for (let m = 0; m < oi.item.caseresults.length; m++) {
        if (oi.item.caseresults[m].deleted == 0) {
          var reason = "";
          try {
            reason = oi.item.caseresults[m].reason.trim();
          } catch (e) {
            reason = "";
          }
          if (oi.item.caseresults[m].drivercharge >= 0) {
            driver += Number(oi.item.caseresults[m].drivercharge);
          }
          if (oi.item.caseresults[m].ownercharge >= 0) {
            owner += Number(oi.item.caseresults[m].ownercharge);
          }
          if (oi.item.caseresults[m].transportercharge >= 0) {
            transporter += Number(oi.item.caseresults[m].transportercharge);
          }
          if (oi.item.caseresults[m].loadercharge >= 0) {
            loader += Number(oi.item.caseresults[m].loadercharge);
          }
          if (oi.item.caseresults[m].parkingfee >= 0) {
            parkingfee += Number(oi.item.caseresults[m].parkingfee);
          }
          var total =
            Number(oi.item.caseresults[m].drivercharge) +
            Number(oi.item.caseresults[m].ownercharge) +
            Number(oi.item.caseresults[m].transportercharge) +
            Number(oi.item.caseresults[m].loadercharge) +
            Number(oi.item.caseresults[m].parkingfee);
          row += "<div class='row'>";
          //alert("Length:" + oi.item.caseresults[m].casereid);
          row += "<div>" + oi.item.caseresults[m].casereid + "</div>";
          row += "<div>" + oi.item.caseresults[m].casebasis + "</div>";
          row += "<div>" + reason + "";
          row += "<div>" + oi.item.caseresults[m].receiptno + "</div>";
          row +=
            "<div>" +
            oi.item.caseresults[m].ownercharge.toLocaleString() +
            "</div>";
          row +=
            "<div>" +
            oi.item.caseresults[m].drivercharge.toLocaleString() +
            "</div>";
          row +=
            "<div>" +
            oi.item.caseresults[m].transportercharge.toLocaleString() +
            "</div>";
          row +=
            "<div>" +
            oi.item.caseresults[m].loadercharge.toLocaleString() +
            "</div>";
          row +=
            "<div>" +
            oi.item.caseresults[m].parkingfee.toLocaleString() +
            "</div>";
          row += "<div>" + total + "</div>";

          row += "</div>";

          //k++;
        }
        var totV = driver + owner + transporter + loader + parkingfee;
        row += "<div>" + totV + "</div>";
      }
      // alert(row);
      return row;
    },
    timetaken(date1, date2) {
      //Swal.fire("Date:" + date1 + "\ndate2" + date2);
      // The number of milliseconds in one day
      const ONE_DAY = 1000;

      // Calculate the difference in milliseconds
      const differenceMs = new Date(date2) - new Date(date1);

      // Convert back to days and return
      return Math.round(differenceMs / ONE_DAY);
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
            <div class="card-body">
              <div class="card changebg">
                <div class="card-body">
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
                          <template v-slot:cell(release)="data">
                            <div class="button">
                              <input
                                v-show="
                                  data.item.spreleaseno != null
                                    ? false
                                    : data.item.caseid == null
                                    ? false
                                    : true
                                "
                                type="button"
                                variant="primary"
                                class="btn btn-primary waves-effect waves-light"
                                :id="`contacusercheck${data.item.id}`"
                                value="Release"
                                @click="updatelist(data.item)"
                              />
                            </div>
                            <div class="button">
                              <input
                                v-show="
                                  data.item.spreleaseno == null ? false : true
                                "
                                type="button"
                                variant="secondary"
                                class="btn btn-primary waves-effect waves-light"
                                :id="`contacusercheck${data.item.id}`"
                                value="View"
                                @click="view(data.item, data.item.spreleaseno)"
                              />
                              <input
                                v-show="
                                  data.item.spreleaseno == null ? false : true
                                "
                                type="button"
                                variant="secondary"
                                class="btn btn-primary waves-effect waves-light"
                                :id="`contacusercheck${data.item.id}`"
                                value="Edit"
                                @click="edit(data.item, data.item.spreleaseno)"
                              />
                            </div>
                          </template>

                          <!-- <template v-slot:cell(specialrelease)="data">
                            <a
                              href="javascript: void(0);"
                              class="text-dark fw-bold"
                              >{{
                                data.item.specialrelease == "" ? "Yes" : "No"
                              }}</a
                            >
                          </template> -->
                          <template v-slot:cell(inspected)="data">
                            <a
                              href="javascript: void(0);"
                              class="text-dark fw-bold"
                              >{{ data.item.inspected == 1 ? "Yes" : "No" }}</a
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
                                'bg-soft-danger':
                                  data.item.action === 'prohibited',
                                'bg-soft-warning':
                                  data.item.action === 'unpaid',
                              }"
                              :click="{
                                'goToProhibition()':
                                  data.item.action === 'prohibited',
                                'goToProhibition()':
                                  data.item.action === 'unpaid',
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
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </form>
    <div class="row"></div>
    <b-modal id="modal-1" :title="sptitle" hide-footer size="xl" centered>
      <div class="card-body">
        <div>
          <!-- Table -->

          <!-- Table -->

          <b-table
            striped
            hover
            :items="this.orderDataticket"
            :fields="fields2"
            current-page="1"
            per-page="10"
          >
            <template v-slot:cell(action)="data">
              <ul class="list-inline mb-0">
                <li class="list-inline-item">
                  <button
                    @click="viewticket(data.item)"
                    style="
                      color: yellow;
                      background-color: black;
                      border-radius: 5px;
                    "
                  >
                    <i class="uil uil-eye me-2"></i>
                    View
                  </button>
                </li>
              </ul>
            </template>

            <template v-slot:cell(wbrg_ticket_date)="data">
              <ul class="list-inline mb-0">
                <li class="list-inline-item">
                  {{ data.item.wbrg_ticket_date.replaceAll("T", " ") }}
                </li>
              </ul>
            </template>
            <!-- src="http://"+window.$localurl+"/kenload/dashboard/imgs/202110/21/18/FATMBA202110000165.jpg" -->
            <template v-slot:cell(anprpic)="data">
              <div class="input-group">
                <div class="col-sm-6">
                  <img
                    class="zoom"
                    :src="
                      getimage(
                        data.item.wbrg_ticket_date,
                        data.item.wbrg_ticket_no,
                        'F'
                      )
                    "
                    height="20"
                    width="20"
                  />
                </div>
                <div class="col-sm-6">
                  <img
                    class="zoom"
                    :src="
                      getimage(
                        data.item.wbrg_ticket_date,
                        data.item.wbrg_ticket_no,
                        'O'
                      )
                    "
                    height="20"
                    width="20"
                  />
                </div>
              </div>
            </template>
            <template v-slot:cell(wbrg_ticket_no)="data">
              <a href="#" @click="viewticket(data.item)">
                <p
                  style="color: #735bec; text-decoration: underline"
                  :style="
                    data.item.wbrg_ticket_status == 'X'
                      ? 'text-decoration: line-through;'
                      : ''
                  "
                >
                  {{ data.item.wbrg_ticket_no }}
                </p>
              </a>
            </template>
            <template v-slot:cell(timetaken)="data">
              <div class="font-size-12">
                {{
                  timetaken(
                    data.item.wbrg_ticket_date.replace("T", " "),
                    data.item.wbrg_ticket_dateout.replace("T", " ")
                  )
                }}
              </div>
            </template>
            <template v-slot:cell(anprcheck)="data">
              <div class="font-size-12">
                {{
                  data.item.anpr_veh_reg == data.item.veh_reg ? "" : "Mismatch"
                }}
              </div>
            </template>
            <template v-slot:cell(gvw)="data">
              <div class="font-size-12">
                {{
                  data.item.axle1_weight +
                  data.item.axle2_weight +
                  data.item.axle3_weight +
                  data.item.axle4_weight
                }}<br />
                ({{
                  data.item.axle1_limit +
                  data.item.axle2_limit +
                  data.item.axle3_limit +
                  data.item.axle4_limit
                }})
              </div>
            </template>
            <template v-slot:cell(wbrg_ticket_status)="data">
              <div class="font-size-2">
                <img
                  v-bind:src="
                    data.item.wbrg_ticket_status == 'O'
                      ? image[0]
                      : data.item.wbrg_ticket_status == 'X'
                      ? image[0]
                      : data.item.wbrg_ticket_status == 'P'
                      ? image[0]
                      : image[1]
                  "
                  alt="?"
                  height="20"
                  width="20"
                />
                {{ data.item.wbrg_ticket_status }}
              </div>
            </template>
            <template v-slot:cell(wbrg_ticket_state)="data">
              <div class="font-size-2">
                {{
                  data.item.wbrg_ticket_state == "T"
                    ? "To Redistribute"
                    : data.item.wbrg_ticket_state == "S"
                    ? "Special Release"
                    : data.item.wbrg_ticket_state == "R"
                    ? "Redistributed"
                    : data.item.wbrg_ticket_state == "C"
                    ? "Charged"
                    : ""
                }}
              </div>
            </template>
          </b-table>
        </div>
        <div>
          <b-table
            table-class="table table-centered datatable table-card-list"
            thead-tr-class="bg-transparent"
            :items="orderDatacase"
            :fields="fields3"
            responsive="sm"
            per-page="10"
            current-page="1"
            :sort-by.sync="sortBy"
            :sort-desc.sync="sortDesc"
            :filter="filter"
            :filter-included-fields="filterOn"
            @filtered="onFiltered"
          >
            <template v-slot:cell(prohibit)="data">
              <div class="button">
                {{ getloadcase(data) }}
              </div>
            </template>

            <template v-slot:cell(id)="data">
              <a href="javascript: void(0);" class="text-dark fw-bold">{{
                data.item.id
              }}</a>
            </template>
            <template v-slot:cell(casedatetime)="data">
              <a href="javascript: void(0);" class="text-dark fw-bold">{{
                data.item.casedatetime.replace("T", " ")
              }}</a>
            </template>
            <template v-slot:cell(caseid)="data">
              <div
                v-show="
                  docs.docname == 'Prohibition Order' && docs.cancelled == 'N'
                "
                v-for="docs in data.item.casedocs"
                :key="docs.id"
              >
                {{ data.item.casedocs[0].casedocid }}
                <a>{{ data.item.caseticket }} </a>
              </div>
            </template>

            <template v-slot:cell(casedocs)="data">
              <div
                class="mb-1"
                :class="
                  docs.cancelled == 'Y'
                    ? 'px-0 alert-danger'
                    : 'px-1 alert show pill alert-success'
                "
                role="alert"
                v-for="docs in data.item.casedocs"
                :key="docs.id"
              >
                <ul class="list-inline mb-0">
                  <li class="list-inline-item">
                    <a
                      v-show="
                        docs.cancelled == 'Y'
                          ? 'px-1 text-danger'
                          : 'px-1 text-primary'
                      "
                      href="javascript:void(0);"
                      :class="
                        docs.cancelled == 'Y'
                          ? 'px-1 text-danger'
                          : 'px-1 text-primary'
                      "
                      :title="docs.docname"
                    >
                      <strong
                        v-b-tooltip.hover
                        @click="getdoc(data, docs.docname)"
                      >
                        {{ docs.docname }}</strong
                      >
                    </a>
                  </li>
                </ul>
                <!-- <button
                            type="button"
                            class="close"
                            data-dismiss="alert"
                            aria-label="Close"
                          >
                            <span aria-hidden="true">&times;</span>
                          </button> -->
              </div>
            </template>

            <template v-slot:cell(action)="data">
              <div class="badge bg-pill bg-soft-success font-size-12">
                {{ data.item.action }}
              </div>
            </template>
          </b-table>
        </div>
        <b-tabs content-class="p-3 text-muted">
          <b-tab active class="border-0">
            <div class="input-group">
              <div class="input-group-text col-sm-4">
                Reason for releasing or closing weigh record:
              </div>
              <select name="reason" id="reason" v-model="reason">
                <option value=">Live stock">Live stock</option>
                <option value="Charged And Paid">Charged And Paid</option>
                <option value="Minimum Overload of Axle Limit">
                  Minimum Overload of Axle Limit
                </option>
                <option value="Minimum Overload of Gross Limit">
                  Minimum Overload of Gross Limit
                </option>
                <option value="Others">Others</option>
                <option value="Passenger bus">Passenger bus</option>
                <option value="Vehicle escaped">Vehicle escaped</option>
                <option value="Vehicle is Not In The Yard">
                  Vehicle is Not In The Yard
                </option>
                <option value="Vehicle With Permit">Vehicle With Permit</option>
              </select>
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-4">Description:</div>
              <input
                v-model="description"
                type="text"
                height="500px"
                class="form-control"
                placeholder="Description"
                id="description"
                name="description"
                required
              />
            </div>
          </b-tab>
        </b-tabs>

        <form>
          <div class="row">
            <div class="col-lg-12">
              <div class="secondary text-end" v-show="!editmode">
                <b-button v-on:click="goToSrdoc(false)">
                  Release Vehicle
                </b-button>
              </div>

              <div class="secondary text-end" v-show="editmode">
                <b-button v-on:click="goToSrdoc(true)">
                  Edit Special Release
                </b-button>
              </div>
            </div>
          </div>
        </form>
      </div>
    </b-modal>
  </Layout>
</template>
<style scoped>
.changebg {
  background-color: rgb(240, 240, 240);
}
</style>