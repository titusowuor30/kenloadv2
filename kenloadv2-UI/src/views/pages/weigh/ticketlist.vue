<script>
import jsPDF from "jspdf";
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";
import Swal from "sweetalert2";
//import Tahoma from "@/assets/fonts/tahoma.ttf";
import Multiselect from "vue-multiselect";
import axios from "axios";
import reportdet from "@/components/report/header";
import ticketlistview from "@/components/widgets/weigh/ticketlistview";
import ticketlineview from "@/components/widgets/weigh/ticketlineview";
import tickettilesview from "@/components/widgets/weigh/tickettilesview";
import Tahoma from "@/assets/fonts/tahoma.ttf";

import DatePicker from "vue2-datepicker";
//import { BIconTruckFlatbed } from "bootstrap-vue";

export default {
  page: {
    title: "TICKETS",
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
    ticketlistview,
    ticketlineview,
    tickettilesview,
    reportdet,
  },
  created() {
    //this.timer = 2000;
  },
  destroyed() {
    this.stoped = false;
  },
  beforeDestroy() {
    this.stoped = false;
    //alert(this.timer);
  },
  Unmounted() {
    //alert(this.timerrun);
    this.timer = 10000000;
  },
  data() {
    return {
      src: null,
      list: true,
      headers: null,
      uniqueCars: null,
      cluster: [],
      wb: [],
      wbs: [],
      pl: "L",
      showme: true,
      stoped: true,
      timer: 5000,
      text: "",
      viewTickets: false,
      posts: [],
      errors: [],
      ticketdata: null,
      image: [],
      title: "Tickets",
      items: [
        {
          text: "Recent Tickets",
        },
        {
          text: "Recent Tickets",
          active: true,
        },
      ],
      img:
        "http://" +
        window.$localurl +
        "/kenload/dashboard/imgs/202110/21/18/FATMBA202110000165.jpg",
      fromdate: "",
      todate: "",
      status: "All",
      state: [],
      states: ["Charged", "To Redistribute", "Special Release", "Redistribute"],

      shift: null,
      route: null,
      hour: null,
      shifts: ["Shift A", "Shift B", "Shift C"],
      routes: ["Nairobi-Mombasa", "Mombasa-Nairobi"],
      thours: [
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
      tohour: "23:59",
      fromhour: "00:00",
      view: "List",
      views: ["List", "Images", "Line Image"],
      imag: false,
      line: false,
      orderData: [],
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
      filter: "",
      filterOn: [],
      sortBy: "id",
      sortDesc: false,
      fields: [
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
    };
  },
  watch: {
    viewTickets(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Opened tickets' list:${newValue}`,
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
    //this.url = window.$url;
    var imae = require("@/assets/images/redbutton.jpg");
    var imae2 = require("@/assets/images/button.png");
    this.image = [imae, imae2];
    this.totalRows = this.items.length;

    this.fromdate = new Date();
    this.todate = new Date();
    this.fromdate.setDate(this.fromdate.getDate() - 2);
    this.todate.setDate(this.todate.getDate() + 1);
    this.getrec();
    setInterval(() => {
      //
      if (this.stoped) {
        this.getrec();
        //alert(this.timer);
      } else {
        this.timer = 1000000;
      }
    }, this.timer);
    this.getview();

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
  },
  methods: {
    timetaken(date1, date2) {
      //Swal.fire("Date:" + date1 + "\ndate2" + date2);
      // The number of milliseconds in one day
      const ONE_DAY = 1000;

      // Calculate the difference in milliseconds
      const differenceMs = new Date(date2) - new Date(date1);

      // Convert back to days and return
      return Math.round(differenceMs / ONE_DAY);
    },
    gettickets() {
      // this.makes = [];
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
        .get(window.$http + `weighbridgetransactions/search?limit=10`, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.

          this.ticketdata = response.data;
          this.orderData = this.ticketdata;

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

    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
    },
    handleSubmit() {
      console.log("Error on submit");
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
    getcurrentdate(mydate) {
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
    viewticketrpt(nodata) {
      console.log(this.$refs);
      // this.$emit("viewTickets", nodata);
      this.$refs.form.viewticket(nodata);
    },

    async viewticket(nodata) {
      //alert(nodata.id);
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
          this.getmyticket(resp, nodata);
        })
        .catch((e) => {
          Swal.fire("users" + e);
          //this.errors.push(e);
        });
    },

    getmyticket(resp, nodata) {
      var orderData = [];
      var orderData3 = [];
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
              //doc.addFileToVFS("tahoma.ttf", Tahoma);
              doc.setFillColor(255, 255, 255);
              doc.roundedRect(5, 5, 200, 38, 1, 1, "FD");

              doc.setTextColor(0, 0, 0);
              doc.setFontSize(12);
              //doc.setFont("Tahoma", "", "bold");
              doc.text(60, 10, "KENYA NATIONAL HIGHWAYS AUTHORITY");
              doc.text(70, 15, "Quality Highways, Better Connections");
              doc.text(80, 25, "Axle Load Control");
              //    doc.setFont("Tahoma", "", "normal");
              doc.setFontSize(10);

              doc.text(
                150,
                30,
                "Print Date: " + this.getcurrentdate(new Date() + "")
              );

              doc.text(15, 40, "Weighbridge Ticket");
              doc.text(70, 40, "Weighbridge Name:" + weigbri);

              //doc.addFont("Tahoma", "Tahoma", "bold");
              doc.text(180, 35, state);
              //   doc.setFont("Tahoma", "", "normal");
              doc.setFontSize(12);
              doc.text(180, 40, status);
              //alert(this.tagged);

              //alert();

              doc.setFontSize(10);
              doc.text(5, 47, "Ticket No: " + nodata.wbrg_ticket_no);
              //alert(reweigh[1]);
              if (Number(reweigh[1]) > 0) {
                doc.text(85, 47, "Re-Weighed:" + reweigh[1]);
              }
              if (nodata.reliefvehreg != "" || nodata.reliefvehreg != "null") {
                doc.text(70, 47, "Relief Load from: " + nodata.reliefvehreg);
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
              var logo = require("@/assets/images/client.png");
              var imgLogo = new Image();
              imgLogo.src = logo;
              doc.addImage(imgLogo, "PNG", 10, 7, 40, 30);

              var operator = suser[0].fullnames.toUpperCase();
              //alert(nodata.wbrg_ticket_operator);
              if (suser[0].fullnames == "") {
                operator = nodata.wbrg_ticket_operator;
              }
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
              doc.text(6, 53, "Vehicle Registration No.: " + nodata.veh_reg);
              doc.addFont("bold");
              doc.text(83.5, 53, "Transporter:" + nodata.wbrg_tx_code);
              doc.text(160, 58, "Axle Type:" + nodata.wbrg_ticket_axel);
              doc.text(21.5, 58, "Vehicle Make: " + nodata.wbrg_ticket_make);
              doc.text(80, 58, "Source/Origin:" + nodata.wbrg_ticket_source);
              doc.text(154, 62, "Scale Operator:");
              doc.text(154, 66, operator);
              doc.text(32, 62, "Driver: " + nodata.wbrg_ticket_driver);
              doc.text(84, 62, "Destination:" + nodata.wbrg_ticket_destination);
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
                //  doc.setFont("Tahoma", "", "bold");
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
                //  doc.setFont("Tahoma", "", "bold");

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

                //alert(inc);
                inc += g + 4;
                lane1 += inc;
                lane2 += inc;
                lane3 += inc;
                lane4 += inc;
              }
              //doc.setFont("Tahoma", "", "bold");
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
              //  doc.setFont("Tahoma", "", "normal");

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
                doc.text(120, lane4 + inc, "" + orderData[i].axle_actualweight);
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
              doc.text(140, lane4 + inc + 10, nodata.wbrg_ticket_gvwload + "");
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
              // doc.setFont("Tahoma");
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
                wbrg_ticket_directionarea = nodata.wbrg_ticket_directionarea.trim();
              } catch (e) {
                console.log(e);
                wbrg_ticket_directionarea = "";
              }
              //doc.setFont("Tahoma", "", "bold");
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

                  doc.addImage(imgfront, "PNG", 10, lane4 + inc + 60, 90, 60);

                  doc.addImage(imgfront2, "PNG", 110, lane4 + inc + 60, 90, 60);
                  //alert("");
                } catch (e) {
                  console.log(e);
                }

                doc.save(nodata.wbrg_ticket_no + "-" + nodata.veh_reg + ".pdf");
                //doc.autoPrint();
                doc.output("dataurlnewwindow");
                //doc.output("datauristring");
                // window.open(
                //   doc.output("datauristring"),
                //   "_blank",
                //   "toolbar=no,status=no,menubar=no,scrollbars=no,resizable=yes,modal=yes,top=200,left=350,width=600,height=400"
                // );

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
          Swal.fire("axleweights" + e);
          //this.errors.push(e);
        });
    },

    async getspecialredetails(wbtno) {
      axios
        .get(window.$http + `specialrelease/search?wbtno=` + wbtno, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          //this.yardlist.spreleaseno = Ncasedocsid;
          // this.reason = response.data[0].reason;
          // this.description = response.data[0].otherreason;
          // this.reasondescription =
          //   "Special Release:" + this.reason + "; " + this.description;
          return response;
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
      return theUrl;
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
    getrec() {
      var fromdate = "";
      var todate = "";
      var status = "";
      var state = "";
      var station = [];
      try {
        station = this.wb;
      } catch (e) {
        station = [];
        //this.station = [];
      }

      try {
        status = this.status;
      } catch (e) {
        status = "";
      }
      if (status != "All") {
        status = "&Status=" + status;
      } else {
        status = "";
      }
      try {
        state = this.state;
      } catch (e) {
        state = "";
      }
      var filter = "";
      if (state.length > 0) {
        filter += "&state=";
        for (var r = 0; r < state.length; r++) {
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
          if (state[r] == "Legal") {
            st = "L";
          }
          if (state[r] == "Warned") {
            st = "W";
          }

          filter += "" + st + ",";
          //rpt += "  " + state[r] + " or ";
        }

        filter = filter.substring(0, filter.length - 1);
      }

      // try {
      //   fromdate = this.fromdate;
      // } catch (e) {
      //   fromdate = "";
      // }
      // try {
      //   todate = this.todate;
      // } catch (e) {
      //   todate = "";
      // }
      // if (fromdate <= 0) {
      //   return;
      // }
      // if (todate <= 0) {
      //   return;
      // }
      // fromdate = this.getmydate(this.fromdate) + " 12:00:00 AM";
      // todate = this.getmydate(this.todate) + " 11:59:59 PM";

      var ampm = "AM";
      var ampm2 = "AM";

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
      //alert(fromdate);
      if (fromdate.length > 0) {
        filter += "fromdate=" + fromdate + "&todate=" + todate;
      }

      if (station.length > 0) {
        filter += "&station=";

        for (r = 0; r < station.length; r++) {
          filter += "" + this.getStationcode(station[r]) + ",";
        }

        filter += filter.substring(0, filter.length - 1);
      }

      // Swal.fire({
      //   title: "Please Wait !",
      //   html: "Loading data...", // add html attribute if you want or remove
      //   allowOutsideClick: false,
      //   showConfirmButton: false,
      //   willOpen: () => {
      //     Swal.showLoading();
      //   },
      // });
      // var filter = this.filter;
      //alert(this.filter.substring(4, 5));
      if (this.filter.trim() != "") {
        filter += "&veh_reg=" + this.filter;
      }

      //alert(filter);

      if (
        this.filter.substring(4, 5) == "A" ||
        this.filter.substring(4, 5) == "B"
      ) {
        filter += "&wbrg_ticket_no=" + this.filter;
      }
      //alert(
      //   window.$http +
      //     `weighbridgetransactions/search?limit=` +
      //     this.perPage +
      //     "&" +
      //     filter +
      //     status
      // );
      axios
        .get(
          window.$http +
            `weighbridgetransactions/search?limit=` +
            this.perPage +
            "&" +
            filter +
            status,
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          // JSON responses are automatically parsed.

          this.ticketdata = response.data;
          this.orderData = this.ticketdata;

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
    getview() {
      //alert(this.view);
      this.list = false;
      this.imag = false;
      this.line = false;

      if (this.view == "List") {
        this.list = true;
      }
      if (this.view == "Images") {
        this.imag = true;
      }
      if (this.view == "Line Image") {
        this.line = true;
      }
      //this.getrec();
    },
  },
  async previewticket(id) {
    //alert(nodata.id);
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
    //alert(window.$http + `weighbridgetransactions/` + id);
    axios
      .get(window.$http + `weighbridgetransactions/` + id, {
        headers: window.$headers,
      })
      .then((respnodata) => {
        var nodata = respnodata.data;
        //console.log(nodata);
        //alert(nodata.id);
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
                    if (nodata.wbrg_ticket_status == "X") {
                      state = "Cancelled";
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
                      doc.text(180, 35, "Tagged");
                      logo = require("@/assets/images/tagged.png");
                      imgLogo = new Image();
                      imgLogo.src = logo;
                      doc.addImage(imgLogo, "PNG", 163, 32, 11, 11);
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
                    var logo = require("@/assets/images/client.png");
                    var imgLogo = new Image();
                    imgLogo.src = logo;
                    doc.addImage(imgLogo, "PNG", 10, 7, 40, 30);
                    var operator = suser[0].fullnames.toUpperCase();
                    alert(nodata.wbrg_ticket_operator);
                    if (suser[0].fullnames == "") {
                      operator = nodata.wbrg_ticket_operator;
                    }

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

                      //alert(inc);
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
                      wbrg_ticket_directionarea = nodata.wbrg_ticket_directionarea.trim();
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
                    // "/kenload/dashboard/getimg.php?string=http://192.168.5.53/cgi-bin/viewer/video.jpg&filename=" +
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

                      doc.save(
                        nodata.wbrg_ticket_no + "-" + nodata.veh_reg + ".pdf"
                      );
                      //doc.autoPrint();
                      doc.output("dataurlnewwindow");
                      //doc.output("datauristring");
                      // window.open(
                      //   doc.output("datauristring"),
                      //   "_blank",
                      //   "toolbar=no,status=no,menubar=no,scrollbars=no,resizable=yes,modal=yes,top=200,left=350,width=600,height=400"
                      // );

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
                Swal.fire("axleweights" + e);
                //this.errors.push(e);
              });
          })
          .catch((e) => {
            Swal.fire("users" + e);
            //this.errors.push(e);
          });
      })
      .catch((e) => {
        Swal.fire("Ticket Error" + e);
        //this.errors.push(e);
      });
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />

    <div class="card" style="font-size: 11px; font-weight: bold">
      <div class="col-sm-12 mb-0">
        <div class="row">
          <div class="col-sm-1">
            <span>Date From:</span>
            <date-picker
              @change="getrec()"
              value="2021-05-17"
              v-model="fromdate"
              :first-day-of-week="1"
              lang="en"
            ></date-picker>
          </div>
          <div class="col-sm-1">
            <span>Time From:</span>

            <multiselect
              v-model="fromhour"
              :options="thours"
              placeholder="00:00"
              :allow-empty="false"
            ></multiselect>
          </div>
          <div class="col-sm-1">
            <span>To:</span>
            <date-picker
              @change="getrec()"
              value="2021-05-17"
              v-model="todate"
              :first-day-of-week="1"
              lang="en"
            ></date-picker>
          </div>
          <div class="col-sm-1">
            <span>Time To:</span>

            <multiselect
              v-model="tohour"
              :options="thours"
              placeholder="00:00"
              :allow-empty="false"
            ></multiselect>
          </div>
          <div class="col-sm-1">
            <span> <i class="uil uil-eye"></i>View</span>

            <div class="col-md-10">
              <div class="col-md-10">
                <b-form-select
                  @change="getview()"
                  v-model="view"
                  :options="views"
                ></b-form-select>
              </div>
            </div>
          </div>
          <div class="col-sm-1">
            <span> <i class="uil uil-status"></i>Status</span>

            <div class="col-md-10">
              <select class="form-select" @change="getrec()" v-model="status">
                <option value="All">All</option>
                <option value="X">Cancelled</option>
                <option value="L">Legal</option>
                <option value="O">Overloaded</option>
                <option value="W">Warned</option>
              </select>
            </div>
          </div>
          <div class="col-sm-1">
            <span> <i class="uil uil-status"></i>State</span>

            <div class="col-md-10">
              <multiselect
                @input="getrec()"
                v-model="state"
                :options="states"
                placeholder="All"
                :multiple="true"
              ></multiselect>
            </div>
          </div>
          <div class="col-sm-2 col-md-2">
            <span>
              <i class="uil uil-eye"></i>
              <label class="d-inline-flex align-items-center fw-normal">
                Show&nbsp; &nbsp;entries
              </label>
            </span>
            <div id="tickets-table_length" class="dataTables_length col-sm-10">
              <b-form-select
                v-model="perPage"
                size="sm"
                :options="pageOptions"
              ></b-form-select>
            </div>
          </div>

          <!-- Station-->

          <div class="col-sm-2">
            <span>
              <i class="uil uil-eye"></i>
              <label class="d-inline-flex align-items-center fw-normal">
                Station
              </label>
            </span>
            <div id="tickets-table_length" class="dataTables_length col-sm-10">
              <multiselect
                @input="getrec()"
                v-model="wb"
                :options="wbs"
                placeholder="All"
                :multiple="true"
              ></multiselect>
            </div>
          </div>

          <!-- Search -->

          <div class="col-sm-1">
            <span> <i class="uil uil-search"></i>Search</span>
            <div id="tickets-table_filter" class="col-sm-12">
              <b-form-input
                v-model="filter"
                type="search"
                placeholder="Search..."
                class="form-control form-control-sm ms-2"
              ></b-form-input>
            </div>
          </div>
          <div class="col-sm-1">
            <b-button variant="dark" class="uil-search" @click="getrec()"
              >Search</b-button
            >
          </div>
        </div>
      </div>
      <ticketlistview v-show="list" :orderData="orderData" />
      <tickettilesview v-show="imag" :orderData="orderData" :src="src" />
      <ticketlineview v-show="line" :orderData="orderData" :src="src" />
    </div>

    <b-modal id="modal-Print" title="Print PDF" hide-footer size="bg" centered>
      <reportdet
        ref="form"
        :title="title"
        :printedpdf="true"
        :orderData="orderData"
        :pl="pl"
        :headers="headers"
        :uniqueCars="uniqueCars"
        v-show="showme"
      ></reportdet>
    </b-modal>
  </Layout>
</template>
<style scoped>
/* ::v-deep/ .table > tbody > tr > td { */
.table > tbody > tr > td {
  padding: 0px;
  margin: 0px;
  height: 2px;
}
.zoom {
  transition: transform 0.2s; /* Animation */
  width: 40px;
  height: 40px;
  margin: 0 auto;
}

.zoom:hover {
  transform: scale(15); /* (150% zoom)*/
}
</style>