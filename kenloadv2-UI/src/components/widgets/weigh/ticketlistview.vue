<script>
//import Layout from "../../layouts/main";
//import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";
import jsPDF from "jspdf";
import Tahoma from "@/assets/fonts/tahoma.ttf";

//import DatePicker from "vue2-datepicker";

import axios from "axios";
import Swal from "sweetalert2";

export default {
  page: {
    title: "Tickets",
    meta: [
      {
        name: "description",
        content: appConfig.description,
      },
    ],
  },
  props: {
    orderData: Array,
    filter: String,
  },
  components: {
    //Layout,
    //PageHeader,
    //DatePicker,
  },
  data() {
    return {
      title: "HSWIM",
      viewHSWIM: false,
      items: [
        {
          text: "HSWIM",
        },
        {
          text: "HSWIM",
          active: true,
        },
      ],

      imae: require("@/assets/images/redbutton.jpg"),
      tagimg: "",
      imae2: require("@/assets/images/redbutton.jpg"),
      image: [this.imae, this.imae2],
      truckimae: require("@/assets/images/redbutton.jpg"),
      truckimae2: require("@/assets/images/redbutton.jpg"),
      truckimage: [this.truckimae, this.truckimae2],
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

      totalRows: 1,
      currentPage: 1,
      perPage: 10,
      pageOptions: [1, 10, 25, 50, 100],

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
          key: "wbrg_ticket_type",
          label: "Ticket No.",
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
    viewHSWIM() {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Opened HSWIM List`,
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
    this.viewHSWIM = true;
    this.tagimg = require("@/assets/images/tagged.png");
    // Set the initial number of items
    this.imae2 = require("@/assets/images/button.png");
    this.imae = require("@/assets/images/redbutton.jpg");
    this.image = [this.imae, this.imae2];
    this.truckimae = require("@/assets/images/truckpass.jpg");
    this.truckimae2 = require("@/assets/images/truckcalledin.jpg");
    this.truckimage = [this.truckimae, this.truckimae2];
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
        .get(window.$http + `virtualtickets`, { headers: window.$headers })
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
    ///////////////////////////////////////////////////////////

    async getvbase64(imgF) {
      try {
        return await axios
          .get(
            window.$http + `UploadFile/ConvertBase64?url=` + imgF,

            { headers: window.$headers }
          )
          .then((re) => {
            const imgfront = re.data;
            //alert(imgfront);
            return imgfront;
          })
          .catch((e) => {
            console.log(e);
            //alert(e);
            return "image/jpeg;base64,9j";
          });
      } catch (Exception) {
        return "image/jpeg;base64,9j";
      }
    },
    async viewticket(nodata) {
      var doc = new jsPDF();
      var imgF = this.getimage(
        nodata.wbrg_ticket_date,
        nodata.wbrg_ticket_no,
        "F"
      );

      var imgO = this.getimage(
        nodata.wbrg_ticket_date,
        nodata.wbrg_ticket_no,
        "O"
      );

      //alert(imgF);
      var imgfront = "";

      var imgfront2 = "";

      try {
        imgfront = await this.getvbase64(imgF);
      } catch (Exception) {
        imgfront = "";
      }

      try {
        imgfront2 = await this.getvbase64(imgO);
      } catch (Exception) {
        imgfront2 = "";
      }

      // var orderData2 = [
      //   {
      //     group: 1,
      //     permissible: 1,
      //     tolerance: 1,
      //     actual: 0,
      //     overload: 0,
      //     pdf: 0.0,
      //     result: "",
      //     deck: "A",
      //   },
      //   {
      //     group: 2,
      //     permissible: 1,
      //     tolerance: 1,
      //     actual: 0,
      //     overload: 0,
      //     pdf: 0.0,
      //     result: "",
      //     deck: "A",
      //   },
      //   {
      //     group: 3,
      //     permissible: 0,
      //     tolerance: 0,
      //     actual: 0,
      //     overload: 0,
      //     pdf: 0.0,
      //     result: "",
      //     deck: "A",
      //   },
      //   {
      //     group: 4,
      //     permissible: 0,
      //     tolerance: 0,
      //     actual: 0,
      //     overload: 0,
      //     pdf: 0.0,
      //     result: "",
      //     deck: "A",
      //   },
      // ];
      //alert(imgfront);
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
      try {
        var suser = await axios
          .get(
            window.$http + `users/search?email=` + nodata.wbrg_ticket_operator,
            {
              headers: window.$headers,
            }
          )
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
      var weigbri = "";
      try {
        weigbri = await axios
          .get(
            window.$http +
              `ClusterWeighbridges/search?cluster_code=` +
              nodata.wbrg_station,
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

      const weights = await axios
        .get(window.$http + `axleweights/search?wbtno=` + nodata.id, {
          headers: window.$headers,
        })
        .then((res) => {
          return res.data;
        })
        .catch((e) => {
          Swal.fire("Ticket Error -" + e);
          //this.errors.push(e);
        });
      const weis = await axios
        .get(window.$http + `axleweights/search?wbtno=` + nodata.id, {
          headers: window.$headers,
        })
        .then((res) => {
          return res.data;
        })
        .catch((e) => {
          Swal.fire("Ticket Error -" + e);
          //this.errors.push(e);
        });
      var period = "2021-2030";
      var lesspermit = 0;
      if (nodata.permit_no.length > 5) {
        if (Number(nodata.wbrg_ticket_axel.substring(0, 1)) == 2) {
          lesspermit = 1000;
        }
        if (
          Number(nodata.wbrg_ticket_axel.substring(0, 1)) == 3 ||
          Number(nodata.wbrg_ticket_axel.substring(0, 1)) == 4
        ) {
          lesspermit = 2000;
        }
      }
      var eacgvw = Number(nodata.wbrg_ticket_gvwload) - lesspermit;

      const gvwfee = await axios
        .get(
          window.$http +
            `EACAct/EACActGVWCharges?period=` +
            period +
            `&GVWOverloadkg=` +
            eacgvw,
          {
            headers: window.$headers,
          }
        )
        .then((res) => {
          return res.data;
        })
        .catch((e) => {
          Swal.fire("Ticket Error -" + e);
          //this.errors.push(e);
        });
      //alert(nodata.wbrg_ticket_type);
      if (nodata.wbrg_ticket_type == "L" || nodata.wbrg_ticket_type == "S") {
        orderData3 = weights;
        console.log(weights);

        var a = 0;
        var b = 0;
        var c = 0;
        var d = 0;

        var pa = 0;
        var pb = 0;
        var pc = 0;
        var pd = 0;
        //console.log(orderData3);
        var orderd = orderData3;

        for (let k = 0; k < orderd.length; k++) {
          // alert(orderd.length + " - " + orderd[k].axle_grouping);
          //console.log(orderd.length + " - " + orderd[k].axle_grouping);
          if (orderd[k].axle_grouping == "A") {
            a = a + Number(orderd[k].axle_actualweight);
            pa = pa + Number(orderd[k].axle_permissibleweight);

            orderData[0] = orderd[k];
            orderData[0].axle_actualweight = a + "";
            orderData[0].axle_permissibleweight = pa + "";
            //alert(orderd[k].axle_grouping);
          }

          if (orderd[k].axle_grouping == "B") {
            b = b + Number(orderd[k].axle_actualweight);
            pb = pb + Number(orderd[k].axle_permissibleweight);

            orderData[1] = orderd[k];
            orderData[1].axle_actualweight = b + "";
            //alert(orderData[1].axle_actualweight);
            orderData[1].axle_permissibleweight = pb + "";
          }

          if (orderd[k].axle_grouping == "C") {
            c = c + Number(orderd[k].axle_actualweight);
            pc = pc + Number(orderd[k].axle_permissibleweight);

            orderData[2] = orderd[k];
            orderData[2].axle_actualweight = c + "";
            orderData[2].axle_permissibleweight = pc + "";
            //alert(orderd[k].axle_grouping);
          }

          if (orderd[k].axle_grouping == "D") {
            d = d + Number(orderd[k].axle_actualweight);
            pd = pd + Number(orderd[k].axle_permissibleweight);
            orderData[3] = orderd[k];
            orderData[3].axle_actualweight = d + "";
            orderData[3].axle_permissibleweight = pd + "";
            //alert(orderd[k].axle_grouping);
          }
        }
        //orderData.push(Object.values(orderData2));
        orderData3 = [];
        orderData3 = weis;
      } else {
        orderData = weights;
      }

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

      doc.text(150, 30, "Print Date: " + this.getcurrentdate(new Date() + ""));

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
        doc.text(110, 47, "Re-Weighed:" + reweigh[1]);
      }
      if (nodata.reliefvehreg != "" || nodata.reliefvehreg != "null") {
        doc.text(65, 47, "Relief Load from: " + nodata.reliefvehreg);
      }
      doc.text(
        135,
        47,
        "Weighed Date & Time:" + this.getcurrentdate(nodata.wbrg_ticket_date)
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

      var operator = "";
      try {
        operator = suser[0].fullnames.toUpperCase();
      } catch (Exception) {
        console.log();
      }

      //alert(nodata.wbrg_ticket_operator);
      if (operator == "") {
        operator = nodata.wbrg_ticket_operator.toUpperCase();
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
      doc.text(26.5, 66, "Trailer No: " + nodata.wbgr_ticket_trailer_no);
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
      let takehigest = 0;

      if (nodata.wbrg_ticket_type == "L" || nodata.wbrg_ticket_type == "S") {
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
        doc.setFont("Tahoma", "", "");

        for (let i = 0; i < orderData3.length; i++) {
          var actual = Number(orderData3[i].axle_actualweight);
          var perm = Number(orderData3[i].axle_permissibleweight);
          var tollerance = perm;

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
          doc.line(5, lane3 + inc, 205, lane3 + inc);

          doc.text(10, lane4 + inc, "Axle");
          doc.text(25, lane4 + inc, g + "");

          let leng = 0;
          str = orderData3[i].axle_permissibleweight + "";
          //alert(str.length);
          leng = 8 - str.length;

          doc.text(
            58 + leng,
            lane4 + inc,
            "" + orderData3[i].axle_permissibleweight
          );

          doc.text(
            84 + leng,
            lane4 + inc,
            "" + orderData3[i].axle_permissibleweight + ""
          );
          doc.text(120, lane4 + inc, "" + orderData3[i].axle_actualweight);
          doc.text(140, lane4 + inc, "" + overload);
          doc.text(163, lane4 + inc, "" + result);
          doc.text(
            183,
            lane4 + inc,
            "" + new Intl.NumberFormat().format(Number(pdf, 2))
          );

          inc += 6;
          axleoverl += Number(overload);
          if (overload > takehigest) {
            takehigest = Number(overload);
          }
          g++;
          doc.setLineWidth(0.1);
        }
        doc.setLineWidth(0.5);
        doc.line(5, lane3 + inc, 205, lane3 + inc);

        // alert(inc);
        inc += g + 8;
        lane1 += inc;
        lane2 += inc;
        lane3 += inc;
        lane4 += inc;
      }
      if (nodata.wbrg_ticket_type == "L" || nodata.wbrg_ticket_type == "S") {
        inc += 10;
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
      doc.text(117, lane1, "Actual");
      doc.text(117, lane2, "Weight[KG]");
      doc.text(137, lane1, "Overload");
      doc.text(137, lane2, "[KG]");
      doc.text(155, lane1, "Result");
      doc.text(171, lane1, "PDF");
      var nofee = true;
      if (nodata.wbrg_station == "SWMM" || nodata.wbrg_station == "MMSW") {
        nofee = false;
      }
      if (nofee) {
        doc.text(183, lane1, "Fee ($)");
      }

      doc.setLineWidth(0.5);
      doc.line(5, lane3, 205, lane3);
      doc.text(85, lane4, "Axle Group Load");
      doc.setLineWidth(0.5);
      doc.setFont("Tahoma", "", "normal");

      inc = 6;
      //var downline = 97;
      g = 1;
      axleoverl = 0;
      takehigest = 0;
      //var takehigest = 0;
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
        actual = Number(orderData[i].axle_actualweight);
        perm = Number(orderData[i].axle_permissibleweight);
        tollerance = (perm * 5) / 100 + perm;
        doc.text(90 + leng, lane4 + inc, "" + tollerance + " 5%");
        overload = 0;
        if (actual - tollerance > 0) {
          overload = actual - tollerance;
        }
        result = "Legal";
        if (overload > 0) {
          result = "Overload";
        }
        pdf = "0";
        let por = actual / perm;
        pdf = Math.pow(4.5, por);
        doc.text(117, lane4 + inc, "" + orderData[i].axle_actualweight);
        doc.text(137, lane4 + inc, "" + overload);
        doc.text(155, lane4 + inc, "" + result);
        doc.text(
          171,
          lane4 + inc,
          "" + new Intl.NumberFormat().format(Number(pdf, 2))
        );
        if (nofee) {
          if (Number(orderData[i].fee) > 0)
            doc.text(
              183,
              lane4 + inc,
              "$" + new Intl.NumberFormat().format(Number(orderData[i].fee, 2))
            );
          //doc.text(183, lane1, "Fee ($)");
        }
        inc += 6;
        axleoverl += Number(overload);
        if (overload > takehigest) {
          takehigest = Number(overload);
        }
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
      doc.text(117, lane4 + inc + 10, "" + nodata.wbrg_ticket_grossweight);
      doc.text(137, lane4 + inc + 10, nodata.wbrg_ticket_gvwload + "");
      doc.text(
        155,
        lane4 + inc + 10,
        nodata.wbrg_ticket_gvwload > 0 ? "Overload" : "Legal"
      );
      var gv = Number(nodata.wbrg_ticket_grossweight);

      pdf = "0";
      let po2 = gv / gvwperm;
      pdf = Math.pow(4.5, po2);
      doc.text(
        171,
        lane4 + inc + 10,
        " " + new Intl.NumberFormat().format(pdf)
      );
      if (Number(gvwfee) > 0)
        doc.text(
          183,
          lane4 + inc + 10,
          "$" + new Intl.NumberFormat().format(Number(gvwfee, 2))
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
            takehigest +
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

      // var imgF = this.getimage(
      //   nodata.wbrg_ticket_date,
      //   nodata.wbrg_ticket_no,
      //   "F"
      // );

      // var imgO = this.getimage(
      //   nodata.wbrg_ticket_date,
      //   nodata.wbrg_ticket_no,
      //   "O"
      // );
      // axios
      //   .get(
      //     window.$http + `UploadFile/ConvertBase64?url=` + imgF,

      //     { headers: window.$headers }
      //   )
      //   .then((re) => {
      //     const imgfront = re.data;
      //     axios
      //       .get(
      //         window.$http + `UploadFile/ConvertBase64?url=` + imgO,

      //         { headers: window.$headers }
      //       )
      //       .then((re) => {
      //         const imgfront2 = re.data;
      //         //console.log(imgfront);

      //         doc.addImage(
      //           imgfront,
      //           "JPEG",
      //           10,
      //           lane4 + inc + 60,
      //           90,
      //           60
      //         );
      //         doc.addImage(
      //           imgfront2,
      //           "JPEG",
      //           110,
      //           lane4 + inc + 60,
      //           90,
      //           60
      //         );
      //         doc.save(
      //           nodata.wbrg_ticket_no +
      //             "-" +
      //             nodata.veh_reg +
      //             ".pdf"
      //         );
      //         //doc.output("datauri");
      //         doc.output("dataurlnewwindow");
      //         Swal.close();
      //       })
      //       .catch((e) => {
      //         console.log(e);
      //       });
      //   })
      //   .catch((e) => {
      //     console.log(e);
      //   });
      //alert(imgfront);

      try {
        if (imgfront.length > 100) {
          imgfront = JSON.stringify(imgfront);
          imgfront = imgfront
            .replace('"', "")
            .substring(0, imgfront.length - 2)
            .trim();
          doc.addImage(
            imgfront,
            "PNG",
            10,
            lane4 + inc + 60,
            90,
            60,
            "front",
            "SLOW",
            360
          );
        }
      } catch (Exception) {
        imgfront = "";
      }

      try {
        if (imgfront2.length > 100) {
          imgfront2 = JSON.stringify(imgfront2);
          imgfront2 = imgfront2
            .replace('"', "")
            .substring(0, imgfront2.length - 2)
            .trim();
          doc.addImage(imgfront2, "PNG", 110, +lane4 + inc + 60, 90, 60);
        }
      } catch (Exception) {
        imgfront = "";
      }

      doc.output("dataurlnewwindow");
      doc.save(nodata.wbrg_ticket_no + "-" + nodata.veh_reg + ".pdf");

      //doc.output("datauri");

      Swal.close();

      // var year = nodata.wbrg_ticket_date.substring(0, 4);
      // var month = nodata.wbrg_ticket_date.substring(5, 7);
      // var day = nodata.wbrg_ticket_date.substring(8, 10);
      // var hour = nodata.wbrg_ticket_date.substring(11, 13);
      // var dir = year + month + "/" + day + "/" + hour + "/";

      // //alert(theUrl);
      // // this.url +
      // // "/kenload/dashboard/getimg.php?string=http://192.168.5.53/jpg/image.jpg?size=3&filename=" +
      // // "O" +
      // // this.WBTNO +
      // // ".jpg&dir=" +
      // // this.dir;
      // //alert(theUrl);

      // const logFileText = async () => {
      //   try {
      //     var ur = window.$url;
      //     // var href = window.location.href;
      //     // var arr = href.split("/");
      //     // var arr2 = arr[2].split(":");
      //     // var ref = arr[0] + "//" + arr2[0] + ":82";
      //     // var ur = window.$url;
      //     // ur = ref;
      //     // var theUrl =
      //     //   ur +
      //     //   "/kenload/dashboard/getbase64img.php?string=" +
      //     //   window.$url +
      //     //   "/kenload/dashboard/imgs/" +
      //     //   dir +
      //     //   "F" +
      //     //   nodata.wbrg_ticket_no +
      //     //   ".jpg";
      //     // console.log(theUrl);
      //     //alert(theUrl);
      //     var theUrl =
      //       ur +
      //       "/kenload/dashboard/getbase64.php?filename=F" +
      //       nodata.wbrg_ticket_no +
      //       ".jpg&dir=" +
      //       dir +
      //       "&string=";
      //     //alert(theUrl);

      //     var theUrl2 =
      //       ur +
      //       "/kenload/dashboard/getbase64.php?filename=O" +
      //       nodata.wbrg_ticket_no +
      //       ".jpg&dir=" +
      //       dir +
      //       "&string=";

      //     const response = await fetch(theUrl);
      //     var imgfront = await response.text();

      //     const response2 = await fetch(theUrl2);
      //     var imgfront2 = await response2.text();

      //     doc.addImage(
      //       imgfront,
      //       "PNG",
      //       10,
      //       lane4 + inc + 60,
      //       90,
      //       60
      //     );

      //     doc.addImage(
      //       imgfront2,
      //       "PNG",
      //       110,
      //       lane4 + inc + 60,
      //       90,
      //       60
      //     );
      //     //alert("");
      //   } catch (e) {
      //     console.log(e);
      //   }

      //   doc.save(
      //     nodata.wbrg_ticket_no + "-" + nodata.veh_reg + ".pdf"
      //   );
      //   //doc.autoPrint();
      //   doc.output("dataurlnewwindow");
      //   //doc.output("datauristring");
      //   // window.open(
      //   //   doc.output("datauristring"),
      //   //   "_blank",
      //   //   "toolbar=no,status=no,menubar=no,scrollbars=no,resizable=yes,modal=yes,top=200,left=350,width=600,height=400"
      //   // );

      //   // doc.autoPrint();
      //   // window.open(
      //   //   doc.output("datauristring"),
      //   //   "_blank",
      //   //   "toolbar=no,status=no,menubar=no,scrollbars=no,resizable=yes,modal=yes,top=200,left=350,width=600,height=400"
      //   // );

      //   Swal.close();
      // };
      // logFileText();
      // doc.save(nodata.wbrg_ticket_no + "-" + nodata.veh_reg + ".pdf");
      // doc.output("dataurlnewwindow");
      // Swal.close();
    },

    ///////////////////////////////////////////////////////

    timetaken(date1, date2) {
      //Swal.fire("Date:" + date1 + "\ndate2" + date2);
      // The number of milliseconds in one day
      const ONE_DAY = 1000;

      // Calculate the difference in milliseconds
      const differenceMs = new Date(date2) - new Date(date1);

      // Convert back to days and return
      return Math.round(differenceMs / ONE_DAY);
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
  },
};
</script>

<template>
  <div class="card" style="font-size: 11px; font-weight: bold">
    <div class="col-sm-12">
      <div class="row" style="font-size: 11px; font-weight: bold">
        <div class="col-12">
          <div>
            <!-- Table -->

            <!-- Table -->

            <b-table
              striped
              hover
              :items="this.orderData"
              :fields="fields"
              :current-page="currentPage"
              :per-page="perPage"
              :filter="filter"
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
              <template v-slot:cell(id)="data">
                <ul class="list-inline mb-0">
                  <li class="list-inline-item">
                    {{ data.item.id }}
                    <img
                      v-show="data.item.wbrg_ticket_tagged == 1 ? true : false"
                      :src="tagimg"
                      width="40"
                    />
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
                    data.item.anpr_veh_reg == data.item.veh_reg
                      ? ""
                      : "Mismatch"
                  }}
                </div>
              </template>
              <!-- Type/Side -->

              <template v-slot:cell(wbrg_ticket_type)="data">
                <div class="font-size-12">
                  {{
                    data.item.wbrg_ticket_type == "M"
                      ? "Multi-Deck"
                      : data.item.wbrg_ticket_type == "S"
                      ? "SAW"
                      : data.item.wbrg_ticket_type == "L"
                      ? "LSWIM"
                      : "Unknown"
                  }}
                  ({{ data.item.wbrg_ticket_weighbridge }})
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
          <div class="row">
            <div class="col">
              <div class="dataTables_paginate paging_simple_numbers float-end">
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