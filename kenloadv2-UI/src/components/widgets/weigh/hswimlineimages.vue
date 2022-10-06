
<script>
import anprassist2 from "@/components/widgets/weigh/anprassist";
import addTags2 from "@/components/widgets/weigh/tagcreate";
import jsPDF from "jspdf";

import Swal from "sweetalert2";
import Tahoma from "@/assets/fonts/tahoma.ttf";

import axios from "axios";

export default {
  props: {
    src: String,
    hswimData: {
      type: Array,
      default: () => {
        return [];
      },
    },
  },
  components: {
    anprassist2,
    addTags2,
  },
  data() {
    return {
      // src3: require("@/assets/images/redbutton.jpg"),
      // src2: require("@/assets/images/redbutton.jpg"),
      src2: "",
      src3: "",
      nplate: "",
      id: "",
      navbuttonVisible: "",
      overweighting: "",
      wim: "",
      orderData: [],
      imae: require("@/assets/images/redbutton.jpg"),
      imae2: require("@/assets/images/redbutton.jpg"),
      tagimg: require("@/assets/images/tagged.png"),
      tagimages: ["", this.tagimg],
      image: [this.imae, this.imae, this.tagimg],
      //src: "@/assets/images/truckexample.png",
    };
  },
  // computed: {
  //   calledIn () {
  //     return this.hswimData.overload ? "Called In" : " -"
  //   }
  // }
  mounted() {
    // Set the initial number of items
    this.imae = require("@/assets/images/button.png");
    this.imae2 = require("@/assets/images/redbutton.jpg");
    this.image = [this.imae, this.imae2, this.tagimg];
    this.tagimages = ["", this.tagimg];
    //alert(this.nplate);
  },

  methods: {
    handleBackToDetails() {},
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
    async anpr(anpr, id, overweighting, wim, index, datetime) {
      //alert(index);
      // this.src2 = this.getimage(datetime, "WIM1ANPR");
      //this.src3 = this.getimage(datetime, "WIM1SV");

      this.nplate = anpr;
      this.id = id;
      this.navbuttonVisible = false;
      this.overweighting = overweighting;
      this.wim = wim;
      this.orderData[0] = this.hswimData[index];

      var imgfront = await this.getvbase64(
        this.getimage(
          this.hswimData[index].date_time_local,
          this.hswimData[index].cid,
          "F",
          this.hswimData[index].wim
        )
      );
      //alert(imgfront);

      var imgfront2 = await this.getvbase64(
        this.getimage(
          this.hswimData[index].date_time_local,
          this.hswimData[index].cid,
          "O",
          this.hswimData[index].wim
        )
      );
      this.src2 = JSON.stringify(imgfront)
        .replace('"', "")
        .substring(22, imgfront.length - 2)
        .trim();
      this.src3 = JSON.stringify(imgfront2)
        .replace('"', "")
        .substring(22, imgfront2.length - 2)
        .trim();

      //alert(this.src2);
      this.$emit("updateplate", [
        {
          nplate2: this.nplate,
        },
      ]);
      console.log(datetime);
      //alert(this.hswimData[index].lp);
    },
    updatenplate(events) {
      this.nplate = events[0].nplate;
    },
    mytag(reg) {
      var st = this.gettag(reg);
      //alert(st.sto);
      return st;
    },
    async gettag(reg) {
      var get = `tag/search?vehreg=` + reg;

      //alert(window.$http + get);
      var st = await axios
        .get(window.$http + get, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          ////alert(
          //   reg +
          //     " " +
          //     response.data[0].type +
          //     response.data[0].reason +
          //     response.data[0].station
          // );
          return response.data[0].type;
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

      return st;
    },
    getimage2(wbrg_ticket_date, wbrg_ticket_no, pos) {
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
    getimage(date, no, position, wim) {
      var server = window.$url; // arr[0] + "//" + arr2[0] + ":82";
      var year = date.substring(0, 4);
      var month = date.substring(5, 7);
      var day = date.substring(8, 10);
      var hour = date.substring(11, 13);

      var dir = year + month + "/" + day + "/" + hour + "/";

      var theUrl = "";
      if (window.$hswim == "Camea") {
        if (position == "F") {
          position = "LF";
        }
        theUrl = server + "/" + dir + position + no + ".jpg";
      }
      var min = date.substring(14, 16);
      var sec = date.substring(17, 19);
      var number = "";

      if (window.$hswim == "Bosvark") {
        server = window.$url; //arr[0] + "//" + arr2[0] + ":82";
        year = date.substring(0, 4);
        month = date.substring(5, 7);
        day = date.substring(8, 10);
        hour = date.substring(11, 13);
        min = date.substring(14, 16);
        sec = date.substring(17, 19);
        dir = year + month + "/" + day + "/" + hour + "/";
        number = "";
        if (position == "F") {
          number = "WIM1ANPR";
        }
        if (position == "O") {
          number = "WIM1SV";
        }
        theUrl =
          server +
          "/kenload/dashboard/imgs/" +
          dir +
          number +
          year.substring(2, 4) +
          "-" +
          month +
          "-" +
          day +
          "_" +
          hour +
          "-" +
          min +
          "-" +
          sec +
          "-00" +
          ".jpg";
      }
      if (wim == "WB-ML-W1" || wim == "WB-EL-W1") {
        server = window.$url; //arr[0] + "//" + arr2[0] + ":82";
        year = date.substring(0, 4);
        month = date.substring(5, 7);
        day = date.substring(8, 10);
        hour = date.substring(11, 13);
        min = date.substring(14, 16);
        sec = date.substring(17, 19);
        dir = year + month + "/" + day + "/" + hour + "/";
        number = "";

        if (position == "O") {
          number = "WIM1SV";
        } else {
          number = "WIM1ANPR";
        }
        theUrl =
          server +
          "/" +
          dir +
          number +
          year.substring(2, 4) +
          "-" +
          month +
          "-" +
          day +
          "_" +
          hour +
          "-" +
          min +
          "-" +
          sec +
          "-00" +
          ".jpg";
      }

      //this.truckimae2;
      //alert("List " + theUrl);
      return theUrl;
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
    async viewticket(nodata) {
      // var orderData = [];
      var orderData3 = [
        {
          group: 1,
          permissible: 1,
          tolerance: 1,
          actual: 0,
          overload: 0,
          pdf: 0.0,
          result: "",
          deck: "A",
        },
        {
          group: 1,
          permissible: 1,
          tolerance: 1,
          actual: 0,
          overload: 0,
          pdf: 0.0,
          result: "",
          deck: "A",
        },
        {
          group: 1,
          permissible: 1,
          tolerance: 1,
          actual: 0,
          overload: 0,
          pdf: 0.0,
          result: "",
          deck: "A",
        },
        {
          group: 1,
          permissible: 1,
          tolerance: 1,
          actual: 0,
          overload: 0,
          pdf: 0.0,
          result: "",
          deck: "A",
        },
        {
          group: 1,
          permissible: 1,
          tolerance: 1,
          actual: 0,
          overload: 0,
          pdf: 0.0,
          result: "",
          deck: "A",
        },
        {
          group: 1,
          permissible: 1,
          tolerance: 1,
          actual: 0,
          overload: 0,
          pdf: 0.0,
          result: "",
          deck: "A",
        },
        {
          group: 1,
          permissible: 1,
          tolerance: 1,
          actual: 0,
          overload: 0,
          pdf: 0.0,
          result: "",
          deck: "A",
        },
        {
          group: 1,
          permissible: 1,
          tolerance: 1,
          actual: 0,
          overload: 0,
          pdf: 0.0,
          result: "",
          deck: "A",
        },
        {
          group: 1,
          permissible: 1,
          tolerance: 1,
          actual: 0,
          overload: 0,
          pdf: 0.0,
          result: "",
          deck: "A",
        },
      ];
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });

      var calledin = "Legal";

      if (nodata.calledin == "1") {
        calledin = "Called in";
      }

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

      doc.text(150, 30, "Print Date: " + this.getcurrentdate(new Date() + ""));

      doc.text(15, 40, "HSWIM Ticket");

      doc.text(70, 40, "HSWIM Name:" + nodata.wim);

      doc.addFont("Tahoma", "Tahoma", "bold");
      doc.text(180, 35, calledin);

      doc.setFont("Tahoma", "", "normal");
      doc.setFontSize(12);
      //doc.text(180, 40, status);

      doc.setFontSize(10);
      doc.text(5, 47, "Ticket No: " + nodata.id);
      //alert(reweigh[1]);
      try {
        if (nodata.wbrg_ticket_no.length > 5) {
          doc.text(75, 47, "WBT No:" + nodata.wbrg_ticket_no);
        }
      } catch (Exception) {
        console.log("");
      }

      doc.text(
        135,
        47,
        "Weighed Date & Time:" + this.getcurrentdate(nodata.date_time_local)
      );
      doc.setFillColor(255, 255, 255);
      var axles = Number(nodata.axles_count); //axle_conf.substring(0, 1);
      var si = 70;
      //alert("nodata.wbrg_ticket_no.length");
      doc.roundedRect(5, 49, 200, si + 17 + axles * 4, 0, 0, "FD");
      //alert(orderData.length);

      //doc.addImage(imgLogo.src, "PNG", 10, 10);
      var logo = require("@/assets/images/client.png");
      var imgLogo = new Image();
      imgLogo.src = logo;
      doc.addImage(imgLogo, "PNG", 10, 7, 40, 30);

      logo = require("@/assets/images/button.png");

      if (nodata.calledin == "1") {
        logo = require("@/assets/images/redbutton.jpg");
      }
      imgLogo = new Image();
      imgLogo.src = logo;
      doc.addImage(imgLogo, "PNG", 150, 31, 12, 12);

      if (nodata.tagged == "1") {
        logo = require("@/assets/images/tagged.png");
        imgLogo = new Image();
        imgLogo.src = logo;
        doc.addImage(imgLogo, "PNG", 163, 32, 11, 11);
        doc.text(180, 40, "Tagged");
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
      doc.text(6, 53, "Vehicle Registration No.: " + nodata.lp);
      doc.addFont("bold");
      doc.text(83.5, 53, "Transporter:");
      doc.text(160, 58, "Axle Type:" + nodata.axle_conf);
      doc.text(21.5, 58, "Vehicle Make: ");
      doc.text(80, 58, "Source/Origin:");
      doc.text(154, 62, "Scale Operator:");
      doc.text(154, 66, "");
      doc.text(32, 62, "Driver: ");
      doc.text(84, 62, "Destination:");
      doc.text(26.5, 66, "Trailer No: ");
      doc.text(91.5, 66, "Cargo:");
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
      var pdf = "0";

      var gvwperm = 0;
      var gvwoverload = 0;
      var totalgvw = 0;

      //alert(axles);
      var load = 0;
      var result = "Legal";
      var perm = 0;
      var actual = 0;
      //alert(nodata.Axl_Legal_Weight_1);

      for (let b = 0; b < axles; b++) {
        if (b == 0) {
          try {
            actual = Number(nodata.axle_1_load);
            perm = Number(nodata.axl_Legal_Weight_1);
            orderData3[b].permissible = perm;
            orderData3[b].actual = actual;

            if (actual - perm > 0) {
              load = actual - perm;
              result = "Overload";
            }
            pdf = "0";
            let por = actual / perm;
            pdf = Math.pow(4.5, por);
          } catch (Exception) {
            console.log("");
          }
          orderData3[b].overload = load;
          orderData3[b].result = result;
          orderData3[b].pdf = new Intl.NumberFormat().format(Number(pdf, 2));
          gvwperm = gvwperm + perm;
          totalgvw = totalgvw + actual;
        }
        load = 0;
        result = "Legal";
        if (b == 1) {
          try {
            actual = Number(nodata.axle_2_load);
            perm = Number(nodata.axl_Legal_Weight_2);
            orderData3[b].permissible = perm;
            orderData3[b].actual = actual;

            if (actual - perm > 0) {
              load = actual - perm;
              result = "Overload";
            }
            pdf = "0";
            let por = actual / perm;
            pdf = Math.pow(4.5, por);
          } catch (Exception) {
            console.log("");
          }
          orderData3[b].overload = load;
          orderData3[b].result = result;
          orderData3[b].pdf = new Intl.NumberFormat().format(Number(pdf, 2));
          gvwperm = gvwperm + perm;
          totalgvw = totalgvw + actual;
        }
        load = 0;
        result = "Legal";
        if (b == 2) {
          try {
            actual = Number(nodata.axle_3_load);
            perm = Number(nodata.axl_Legal_Weight_3);
            orderData3[b].permissible = perm;
            orderData3[b].actual = actual;

            if (actual - perm > 0) {
              load = actual - perm;
              result = "Overload";
            }
            pdf = "0";
            let por = actual / perm;
            pdf = Math.pow(4.5, por);
          } catch (Exception) {
            console.log("");
          }
          orderData3[b].overload = load;
          orderData3[b].result = result;
          orderData3[b].pdf = new Intl.NumberFormat().format(Number(pdf, 2));
          gvwperm = gvwperm + perm;
          totalgvw = totalgvw + actual;
        }
        load = 0;
        result = "Legal";
        if (b == 3) {
          actual = Number(nodata.axle_4_load);
          perm = Number(nodata.axl_Legal_Weight_4);
          orderData3[b].permissible = perm;
          orderData3[b].actual = actual;

          if (actual - perm > 0) {
            load = actual - perm;
            result = "Overload";
          }
          pdf = "0";
          let por = actual / perm;
          pdf = Math.pow(4.5, por);

          orderData3[b].overload = load;
          orderData3[b].result = result;
          orderData3[b].pdf = new Intl.NumberFormat().format(Number(pdf, 2));
          gvwperm = gvwperm + perm;
          totalgvw = totalgvw + actual;
        }
        load = 0;
        result = "Legal";
        if (b == 4) {
          actual = Number(nodata.axle_5_load);
          perm = Number(nodata.axl_Legal_Weight_5);
          orderData3[b].permissible = perm;
          orderData3[b].actual = actual;

          if (actual - perm > 0) {
            load = actual - perm;
            result = "Overload";
          }
          pdf = "0";
          let por = actual / perm;
          pdf = Math.pow(4.5, por);

          orderData3[b].overload = load;
          orderData3[b].result = result;
          orderData3[b].pdf = new Intl.NumberFormat().format(Number(pdf, 2));
          gvwperm = gvwperm + perm;
          totalgvw = totalgvw + actual;
        }
        load = 0;
        result = "Legal";
        if (b == 5) {
          actual = Number(nodata.axle_6_load);
          perm = Number(nodata.axl_Legal_Weight_6);
          orderData3[b].permissible = perm;
          orderData3[b].actual = actual;

          if (actual - perm > 0) {
            load = actual - perm;
            result = "Overload";
          }
          pdf = "0";
          let por = actual / perm;
          pdf = Math.pow(4.5, por);

          orderData3[b].overload = load;
          orderData3[b].result = result;
          orderData3[b].pdf = new Intl.NumberFormat().format(Number(pdf, 2));
          gvwperm = gvwperm + perm;
          totalgvw = totalgvw + actual;
        }
        load = 0;
        result = "Legal";
        if (b == 6) {
          actual = Number(nodata.axle_7_load);
          perm = Number(nodata.axl_Legal_Weight_7);
          orderData3[b].permissible = perm;
          orderData3[b].actual = actual;

          if (actual - perm > 0) {
            load = actual - perm;
            result = "Overload";
          }
          pdf = "0";
          let por = actual / perm;
          pdf = Math.pow(4.5, por);

          orderData3[b].overload = load;
          orderData3[b].result = result;
          orderData3[b].pdf = new Intl.NumberFormat().format(Number(pdf, 2));
          gvwperm = gvwperm + perm;
          totalgvw = totalgvw + actual;
        }
        load = 0;
        result = "Legal";
        if (b == 7) {
          actual = Number(nodata.axle_8_load);
          perm = Number(nodata.axl_Legal_Weight_8);
          orderData3[b].permissible = perm;
          orderData3[b].actual = actual;

          if (actual - perm > 0) {
            load = actual - perm;
            result = "Overload";
          }
          pdf = "0";
          let por = actual / perm;
          pdf = Math.pow(4.5, por);

          orderData3[b].overload = load;
          orderData3[b].result = result;
          orderData3[b].pdf = new Intl.NumberFormat().format(Number(pdf, 2));
          gvwperm = gvwperm + perm;
          totalgvw = totalgvw + actual;
        }
        //alert(result + load + actual + "perm:" + perm);
      }

      for (let i = 0; i < axles; i++) {
        doc.line(5, lane3 + inc, 205, lane3 + inc);

        doc.text(10, lane4 + inc, "Axle");
        doc.text(25, lane4 + inc, g + "");

        let leng = 0;
        str = orderData3[i].permissible + "";
        //alert(str.length);
        leng = 8 - str.length;

        doc.text(58 + leng, lane4 + inc, "" + orderData3[i].permissible);

        doc.text(84 + leng, lane4 + inc, "" + orderData3[i].permissible + "");
        doc.text(120, lane4 + inc, "" + orderData3[i].actual);
        doc.text(140, lane4 + inc, "" + orderData3[i].overload);
        doc.text(163, lane4 + inc, "" + orderData3[i].result);
        doc.text(183, lane4 + inc, "" + orderData3[i].pdf);
        inc += 6;
        axleoverl += Number(orderData3[i].overload);
        g++;
        doc.setLineWidth(0.1);
      }

      //gvwperm = gvwperm;
      //totalgvw -gvwperm

      if (Number(totalgvw) - Number(gvwperm) > 0) {
        gvwoverload = Number(totalgvw) - Number(gvwperm);
      }

      doc.setLineWidth(0.5);
      doc.line(5, lane3 + inc, 205, lane3 + inc);

      ////alert(inc);
      //inc += g + 4;

      inc -= 4;
      doc.setLineWidth(1.0);
      doc.line(5, lane4 + inc, 205, lane4 + inc);
      doc.text(90, lane4 + inc + 4, "Vehicle Load");
      doc.setLineWidth(0.5);
      doc.line(5, lane4 + inc + 6, 205, lane4 + inc + 6);
      doc.text(15, lane4 + inc + 10, "GVW");

      doc.text(60, lane4 + inc + 10, gvwperm + "");
      doc.text(88, lane4 + inc + 10, gvwperm + " 0%");
      doc.text(120, lane4 + inc + 10, "" + totalgvw);
      doc.text(140, lane4 + inc + 10, gvwoverload + "");
      doc.text(
        163,
        lane4 + inc + 10,
        Number(gvwoverload) > 0 ? "Overload" : "Legal"
      );
      var gv = Number(gvwoverload);

      //var pdf = "0";
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
        "Legend:GVW:Gross Vehicle Weight, Wheels:S, Single Conventional Tyre D:Double,W Super Single (wide), "
      );
      doc.text(6, lane4 + inc + 21, " Pavement Damage Factor [PDF]");

      doc.text(
        5,
        lane4 + inc + 27,
        "Note: Axle group weights were checked, but axle weights were not checked. One or more axles in axle group can"
      );

      doc.text(
        5,
        lane4 + inc + 31,
        " be overloaded even if the total weight of the axle group is reported as legal. It means the responsibility  "
      );
      doc.text(
        5,
        lane4 + inc + 35,
        "of the owner to ensure that the vehicle is correctly loaded."
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

      var imgF = this.getimage(
        nodata.date_time_local,
        nodata.cid,
        "F",
        nodata.wim
      );

      var imgO = this.getimage(
        nodata.date_time_local,
        nodata.cid,
        "O",
        nodata.wim
      );
      //alert(imgF);
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
      try {
        if (nodata.wbrg_ticket_no.length > 5) {
          doc.addPage();
          axios
            .get(
              window.$http + `weighbridgetransactions/` + nodata.wbrg_ticket_no,
              {
                headers: window.$headers,
              }
            )
            .then((respnodata) => {
              this.wbtviewticket(respnodata.data, doc);
            })
            .catch((e) => {
              Swal.fire("Ticket Error" + e);
              //this.errors.push(e);
            });
          return;
        }
      } catch (Exception) {
        console.log("");
      }

      doc.output("dataurlnewwindow");
      doc.save(nodata.id + "-" + nodata.lp + ".pdf");

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
      //        //alert("HSWIM Ticket Overload");
      //         doc.addImage(imgfront, "JPEG", 10, lane4 + inc + 60, 90, 60);
      //         doc.addImage(imgfront2, "JPEG", 110, lane4 + inc + 60, 90, 60);
      //         doc.save(nodata.wbrg_ticket_no + "-" + nodata.veh_reg + ".pdf");
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
    },
    async wbtviewticket(nodata, doc) {
      //var doc = new jsPDF();
      var imgF2 = this.getimage2(
        nodata.wbrg_ticket_date,
        nodata.wbrg_ticket_no,
        "F"
      );

      var imgO2 = this.getimage2(
        nodata.wbrg_ticket_date,
        nodata.wbrg_ticket_no,
        "O"
      );

      //alert(imgF);
      var imgfront3 = "";

      var imgfront4 = "";

      try {
        imgfront3 = await this.getvbase64(imgF2);
      } catch (Exception) {
        imgfront3 = "";
      }

      try {
        imgfront4 = await this.getvbase64(imgO2);
      } catch (Exception) {
        imgfront4 = "";
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
      const gvwfee = await axios
        .get(
          window.$http +
            `EACAct/EACActGVWCharges?period=` +
            period +
            `&GVWOverloadkg=` +
            nodata.wbrg_ticket_gvwload,
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
          ////alert(orderd.length + " - " + orderd[k].axle_grouping);
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
        doc.text(180, 45, "Tagged");
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
      const logo = require("@/assets/images/client.png");
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

        ////alert(inc);
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
      doc.text(183, lane1, "Fee ($)");
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
        if (Number(orderData[i].fee) > 0)
          doc.text(
            183,
            lane4 + inc,
            "$" + new Intl.NumberFormat().format(Number(orderData[i].fee, 2))
          );
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
        if (imgfront3.length > 100) {
          imgfront3 = JSON.stringify(imgfront3);
          imgfront3 = imgfront3
            .replace('"', "")
            .substring(0, imgfront3.length - 2)
            .trim();
          doc.addImage(
            imgfront3,
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
        imgfront3 = "";
      }

      try {
        if (imgfront4.length > 100) {
          imgfront4 = JSON.stringify(imgfront4);
          imgfront4 = imgfront4
            .replace('"', "")
            .substring(0, imgfront4.length - 2)
            .trim();
          doc.addImage(imgfront4, "PNG", 110, +lane4 + inc + 60, 90, 60);
        }
      } catch (Exception) {
        imgfront4 = "";
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
  },
};
</script>

<template>
  <div class="row">
    <!-- :class="data.calledin == 1 ? 'bg_green' : 'bg_red'" 
    ///
    -->
    <div
      class="d-flex bodgrey"
      v-for="(data, index) in hswimData"
      :key="data.cid"
      :class="data.calledin == 0 ? 'bodgreen' : 'bg_red'"
    >
      <div
        class="d-flex card col-xl-1"
        style="height: 100%"
        :class="data.calledin == 0 ? 'bodgreen' : 'bodred'"
      >
        <div>
          <img
            class="img-fluid rounded"
            v-bind:src="image[data.calledin]"
            alt="error"
            style="width: 75%"
          />
        </div>
        <div>
          <img
            class="img-fluid rounded"
            v-bind:src="tagimages[data.tagged]"
            :alt="tagimages[data.tagged]"
            style="width: 75%"
          />
        </div>
      </div>

      <div
        class="d-flex card col-sm-3"
        :class="data.calledin == 1 ? 'bodgreen ' : 'bodred'"
      >
        <div style="height: 100%">
          <img
            :src="getimage(data.date_time_local, data.cid, 'F', data.wim)"
            :alt="getimage(data.date_time_local, data.cid, 'F', data.wim)"
            class="rounded zoom"
            height="350"
            style="width: 99%; height: 100%"
          />
        </div>
      </div>
      <div
        class="d-flex card col-sm-3"
        :class="data.calledin == 0 ? 'bodgreen' : 'bodred'"
      >
        <div style="height: 100%">
          <img
            :src="getimage(data.date_time_local, data.cid, 'O', data.wim)"
            alt="image not found"
            class="rounded zoom"
            height="350"
            style="width: 99%; height: 100%"
          />
        </div>
      </div>
      <div
        style="height: 100%"
        class="d-flex card col-xl-3"
        :class="data.calledin == 0 ? 'bodgreen' : 'bodred'"
      >
        <div class="card-header" style="width: 100%">
          <div
            class="d-flex justify-content-between divider"
            style="width: 100%"
          >
            <h5>{{ data.cid }}</h5>
            <b-button
              @click="
                anpr(
                  data.lp,
                  data.id,
                  data.overweighting,
                  data.wim,
                  index,
                  data.date_time_local
                )
              "
              variant="secondary w-sm"
              v-b-modal.modal-ANPR2
              v-show="data.calledin == 0 ? true : true"
            >
              ANPR Assist
            </b-button>
            <h5>Axles: {{ data.axles_count }}</h5>
          </div>
          <div class="justify-content-between mt-2">
            <h5
              style="
                color: yellow;
                text-align: center;
                background-color: black;
                padding: 18px;
                boder-radius: 5px;
              "
            >
              {{ data.lp }}
              <!-- {{ data.anprassist.value() == "" ? data.lp : data.anprassist }} -->
            </h5>
          </div>

          <div>
            <div class="textLarge">Tag: {{ data.tagged }}</div>
            <div class="textLarge">Speed: {{ data.velocity }} Km/h</div>
            <div class="textLarge">Gap: {{ data.gap }} Sec(s)</div>
            <div class="textLarge">Length: {{ data.length }}</div>
            <div class="textLarge">WIM: {{ data.wim }}</div>
            <div class="textLarge">
              Date: {{ data.date_time_local.substring(0, 10) }}
            </div>
            <div class="textLarge">
              Time: {{ data.date_time_local.substring(11, 20) }}
            </div>
          </div>
        </div>

        <div class="card-footer">
          <div>
            <b-button
              @click="viewticket(data)"
              variant="primary w-sm"
              style="background-color: black; color: yellow; float: right"
            >
              <i class="uil uil-search ms-2"></i>
              Ticket
            </b-button>
            <b-button
              v-show="data.calledin == 0 ? false : true"
              @click="handleBackToDetails"
              variant="secondary w-sm"
              v-b-modal.modal-Tag
            >
              Tag
            </b-button>
            <b-button
              :style="
                data.calledin == 0
                  ? 'background-color:  green;'
                  : 'background-color:  red;'
              "
              @click="handleBackToDetails"
            >
              {{ data.total_weight }} [KG]
            </b-button>
          </div>
        </div>
      </div>
      <div
        style="height: 100%"
        class="d-flex card col-xl-2"
        :class="data.calledin == 0 ? 'bodgreen' : 'bodred'"
      >
        <div class="card-header">
          <div class="d-flex justify-content-between divider">
            <div>
              <div class="textLarge">
                <table class="striped hover">
                  <th>Axle</th>
                  <th>Actual</th>
                  <th>Permissible</th>
                  <th>Excess</th>
                  <tr>
                    <td style="text-align: right">Axle1:</td>
                    <td style="text-align: right">{{ data.axle_1_load }}</td>
                    <td style="text-align: right">
                      {{ data.axl_Legal_Weight_1 }}
                    </td>
                    <td style="text-align: right">
                      {{
                        data.axle_1_load - data.axl_Legal_Weight_1 > 0
                          ? data.axle_1_load - data.axl_Legal_Weight_1
                          : 0
                      }}
                    </td>
                  </tr>

                  <tr>
                    <td style="text-align: right">Axle2:</td>
                    <td style="text-align: right">{{ data.axle_2_load }}</td>
                    <td style="text-align: right">
                      {{ data.axl_Legal_Weight_2 }}
                    </td>
                    <td
                      style="text-align: right"
                      :class="
                        data.axle_2_load - data.axl_Legal_Weight_2 > 0
                          ? 'bg_red'
                          : ''
                      "
                    >
                      {{
                        data.axle_2_load - data.axl_Legal_Weight_2 > 0
                          ? data.axle_2_load - data.axl_Legal_Weight_2
                          : 0
                      }}
                    </td>
                  </tr>

                  <tr>
                    <td style="text-align: right">Axle3:</td>
                    <td style="text-align: right">{{ data.axle_3_load }}</td>
                    <td style="text-align: right">
                      {{ data.axl_Legal_Weight_3 }}
                    </td>
                    <td style="text-align: right">
                      {{
                        data.axle_3_load - data.axl_Legal_Weight_3 > 0
                          ? data.axle_3_load - data.axl_Legal_Weight_3
                          : 0
                      }}
                    </td>
                  </tr>

                  <tr>
                    <td style="text-align: right">Axle4:</td>
                    <td style="text-align: right">{{ data.axle_4_load }}</td>
                    <td style="text-align: right">
                      {{ data.axl_Legal_Weight_4 }}
                    </td>
                    <td style="text-align: right">
                      {{
                        data.axle_4_load - data.axl_Legal_Weight_4 > 0
                          ? data.axle_4_load - data.axl_Legal_Weight_4
                          : 0
                      }}
                    </td>
                  </tr>

                  <tr>
                    <td style="text-align: right">Axle5:</td>
                    <td style="text-align: right">{{ data.axle_5_load }}</td>
                    <td style="text-align: right">
                      {{ data.axl_Legal_Weight_5 }}
                    </td>
                    <td style="text-align: right">
                      {{
                        data.axle_5_load - data.axl_Legal_Weight_5 > 0
                          ? data.axle_5_load - data.axl_Legal_Weight_5
                          : 0
                      }}
                    </td>
                  </tr>

                  <tr>
                    <td style="text-align: right">Axle6:</td>
                    <td style="text-align: right">{{ data.axle_6_load }}</td>
                    <td style="text-align: right">
                      {{ data.axl_Legal_Weight_6 }}
                    </td>
                    <td style="text-align: right">
                      {{
                        data.axle_6_load - data.axl_Legal_Weight_6 > 0
                          ? data.axle_6_load - data.axl_Legal_Weight_6
                          : 0
                      }}
                    </td>
                  </tr>

                  <tr>
                    <td style="text-align: right">Axle7:</td>
                    <td style="text-align: right">{{ data.axle_7_load }}</td>
                    <td style="text-align: right">
                      {{ data.axl_Legal_Weight_7 }}
                    </td>
                    <td style="text-align: right">
                      {{
                        data.axle_7_load - data.axl_Legal_Weight_7 > 0
                          ? data.axle_7_load - data.axl_Legal_Weight_7
                          : 0
                      }}
                    </td>
                  </tr>
                </table>
              </div>
            </div>
          </div>
        </div>
        <div v-show="data.calledin == 0 ? false : true" class="textLarge">
          <!-- Called In - GVW Overload <b> {{ data.gvw }} </b> or Axle Overload  <b> {{ data.avw }} </b> -->

          {{ data.calledinreason }}
        </div>
      </div>
    </div>

    <b-modal
      id="modal-ANPR2"
      title="ANPR Assist"
      hide-footer
      size="xl"
      centered
    >
      <anprassist2
        :nplate="nplate"
        :navbuttonVisible="navbuttonVisible"
        :id="id"
        :anpr="nplate"
        :overweighting="overweighting"
        :wim="wim"
        :orderData="orderData"
        :src2="src2"
        :src3="src3"
        v-on:updatenplate="updatenplate($event)"
      >
      </anprassist2>
    </b-modal>
    <b-modal
      id="modal-Tag"
      title="Add Tag"
      hide-footer
      size="xl"
      class=".modal-xl"
      centered
    >
      <addTags2 :nplate="nplate" />
    </b-modal>
  </div>
</template>

<style scoped>
.divider {
  border-bottom: 2px solid #000;
}
.avatar {
  vertical-align: middle;
  width: 50px;
  height: 50px;
  border-radius: 50%;
}
.bg_red {
  /* background: #f7a0a0; */
}
.bg_green {
  background: #73ad21;
}
.bodgreen {
  /* border-radius: 25px;
  border: 2px solid green;
  padding: 7px; */
}
.bodgrey {
  /* border-radius: 25px;
  border: 2px solid green;
  padding: 7px; */
  border: 2px solid rgb(207, 205, 205);
  border-radius: 5px;
  margin: 2px;
  padding: 5px;
}
.bodred {
  /* border-style: solid;
  border-color: red; */
  /* border-radius: 25px;
  border: 1px solid red;
  padding: 7px; */
}
.textSmall {
  text-align: center;
  font-weight: bold;
  font-size: 9px;
}
.zoom {
  transition: transform 0.2s; /* Animation */
  width: 400px;
  height: 350px;
  margin: 0 auto;
}

.zoom:hover {
  transform: scale(2); /* (150% zoom)*/
}
</style>
