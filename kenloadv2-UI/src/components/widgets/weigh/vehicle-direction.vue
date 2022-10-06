<script>
import jsPDF from "jspdf";
//import accesscontrol from "../weigh/accesscontrol";
import Accesscontrol from "./accesscontrol.vue";
import Swal from "sweetalert2";
import Deckimages from "@/components/widgets/weigh/deckimages";
import Tahoma from "@/assets/fonts/tahoma.ttf";
import reportdet from "@/components/report/header";
import store from "@/state/store";

import axios from "axios";

export default {
  props: {
    gvwoverload: Number,
    totalweight: Number,
    gvwperm: Number,
    orderData: Array,
    orderData3: Array,
    vehicleDirection: Number,
    isVisible: Boolean,
    nplate: String,
    axlec: String,
    permit: String,
    trailer: String,
    driver: String,
    make: String,
    cargo: String,
    transporter: String,
    origin: String,
    destination: String,
    reliefvehreg: String,
    casereliefdetails: Array,
    reweigh: Number,
    comment: String,
    weights: Array,
    tagged: Boolean,
    cat: String,
    status: String,
    weighdate: String,
    WBTNO: String,
    startdatetime: String,
    nodata: Array,
    inyard: Boolean,
    yard: Array,
    reason: String,
    description: String,
    granted: Boolean,
    vDirection: Number,
    modalname: String,
    allgvw: Number,
    allsaw: Number,
    allgaw: Number,
    autospecialrelease: Boolean,
    src2: String,
    src3: String,
  },
  components: {
    Deckimages,
    Accesscontrol,
    reportdet,
  },
  data() {
    return {
      imae: "",
      modaltitle: "",
      showAlert: true,
      editmode: false,
      finishWeighing: false,
      specialRelease: "",

      vDirection2: 0,
      granted2: false,
      reason2: "",
      autospecialrelease2: false,
      usersp: "",
    };
  },
  updated() {
    //alert("updated");
    // this.vDirection2 = this.vDirection;
    //alert(this.tagged);
    // if (this.tagged) {
    //   this.vDirection2 = 0;
    // }
    this.$emit("updatevDirection", [
      {
        vDirection: this.vDirection2,
        granted: this.granted2,
        reason: this.reason2,
        modalname: this.modalname,
        autospecialrelease: this.autospecialrelease2,
        description: this.description,
      },
    ]);
  },
  watch: {
    finishWeighing() {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Finished weighing process for ${this.nplate},Receipt generated!,${this.specialRelease}`,
        computer: window.localStorage.clientip,
      };
      axios
        .post(window.$http + "AuditLogs", data, { headers: window.$headers })
        .then(() => {
          //console.log(response.data);
        })
        .catch((e) => {
          console.log(e);
        });
    },
  },
  created() {
    // this.vDirection = this.vehicleDirection;
  },
  mounted() {
    //alert(this.vDirection);
    this.imae = require("@/assets/images/tagged.png");
    this.vDirection2 = this.vDirection;
    //alert(this.tagged);
    if (this.tagged) {
      this.vDirection2 = 0;
    }

    this.granted2 = this.granted;
    this.reason2 = this.reason;
    this.autospecialrelease2 = this.autospecialrelease;
  },
  methods: {
    changegranted(state, usersp) {
      this.finishWeighing = true;
      this.usersp = usersp;
      //alert(state);
      this.granted2 = state;
      this.reason2 = "Others";
      this.$root.$emit("bv::hide::modal", "modal-Edit");
      this.modalname = "";
      this.specialRelease = "Special release(vehicle:" + this.nplate + ")";
      this.autospecialrelease2 = true;
    },
    handleBackToDetails() {
      //alert("");
      this.$emit("closeme", "false");
      //this.$router.replace("/vehicledetails");
    },
    handleDirectionStatus(e) {
      var selected = e.target.value;
      console.log(selected);
      this.showAlert = true;
    },
    generateReceipt() {
      this.finishWeighing = true;
      ////alert(this.make.length);
      var trailer = "".trim();
      try {
        trailer = this.trailer.trim();
      } catch (e) {
        trailer = "".trim();
      }
      this.trailer = trailer
        .toUpperCase()
        .replaceAll("\\s", "")
        .replaceAll("[^a-zA-Z0-9]", "")
        .replaceAll(" ", "")
        .trim();
      var make = "";
      try {
        make = this.make.trim();
      } catch (e) {
        make = "";
      }

      if (make.length <= 0 && this.vDirection2 == 0) {
        Swal.fire("Please select vehicle make!").then(() => {
          this.handleBackToDetails();
        });

        return;
      }
      var cargo = "";
      try {
        cargo = this.cargo.trim();
        //alert(cargo);
      } catch (e) {
        cargo = "";
      }
      if (cargo.length <= 0 && this.vDirection2 == 0) {
        Swal.fire("Please select cargo!").then(() => {
          this.handleBackToDetails();
        });

        return;
      }

      var transporter = "";
      try {
        transporter = this.transporter.trim();
      } catch (e) {
        transporter = "";
      }
      if (transporter <= 0 && this.vDirection2 == 0) {
        Swal.fire("Please select Transporter!").then(() => {
          this.handleBackToDetails();
        });

        return;
      }

      var origin = "";
      try {
        origin = this.origin.trim();
      } catch (e) {
        origin = "";
      }
      if (origin <= 0 && this.vDirection2 == 0) {
        Swal.fire("Please select Origin!").then(() => {
          this.handleBackToDetails();
        });
        return;
      }

      var destination = "";
      try {
        destination = this.destination.trim();
      } catch (e) {
        destination = "";
      }
      if (destination <= 0 && this.vDirection2 == 0) {
        Swal.fire("Please select Destination!").then(() => {
          this.handleBackToDetails();
        });
        return;
      }
      var yard = "E";
      if (this.vDirection2 == 0) {
        yard = "Y";
      }
      var description = "";
      try {
        description = this.description;
      } catch (e) {
        description = "";
      }
      this.description = description;
      if (yard == "E" && this.vehicleDirection == 0) {
        if (description.length < 10) {
          Swal.fire("Enter a valid reason!");
          return;
        }
      }
      ////alert(
      //   "vehreg:" +
      //     this.nplate +
      //     " - " +
      //     "status:I" +
      //     " - " +
      //     "userid: " +
      //     window.$userid +
      //     " - " +
      //     "datetime:" +
      //     this.weighdate +
      //     " - " +
      //     "wbtno:" +
      //     this.WBTNO +
      //     " - " +
      //     "wbrg_tickt_no:" +
      //     this.WBTNO +
      //     " - " +
      //     "station: " +
      //     window.$stationcode
      // );
      //alert(window.$http + `yardlist/`);

      //alert(yard);
      //alert(this.inyard);
      // if (yard == "Y") {
      //   return;
      // }

      if (yard == "Y" && !this.inyard) {
        const yarddata = {
          vehreg: this.nplate,
          status: "I",
          userid: window.$userid,
          datetime: this.weighdate,
          wbtno: this.WBTNO,
          station: window.$stationcode,
          weighbridgeTransactionsid: this.nodata.id,
        };
        //alert(yarddata);
        //console.log(yarddata);
        axios
          .post(window.$http + `yardlist/`, yarddata, {
            headers: window.$headers,
          })
          .then((rep) => {
            //saveticket();
            for (var i in rep) {
              //alert(i);
              //result.push([i, rep[i]]);
              //alert([i, rep[i]]);
              if (i == "data") {
                this.yard = rep[i];
              }
            }
            this.nodata.yardlistid = this.yard.id;
            Swal.fire({
              position: "center",
              icon: "success",
              title: "Vehicle added in yard ",
              showConfirmButton: false,
              timer: 1500,
            }).then(() => {
              this.inyard = true;
              this.generateReceipt();
              // this.weighttaken = true;
              // this.timer = 1000000000;
              // this.isVisible = false;
            });
          })
          .catch((e) => {
            Swal.fire({
              position: "center",
              icon: "error",
              title: "Inserting Vehicle in Yard" + e,
              showConfirmButton: true,
            }).then(() => {
              Swal.close();
            });
          });
      } else {
        if (yard == "E" && this.inyard) {
          const yarddata = {
            id: this.yard[0].id,
            //wbrg_tickt_no: this.nodata.id,
            status: "O",
            yardtimeout: this.weighdate,
            userid: this.yard[0].userid,
            datetime: this.yard[0].datetime,
            wbtno: this.yard[0].wbtno,
            station: this.yard[0].station,
            vehreg: this.yard[0].vehreg,
            weighbridgeTransactionsid: this.nodata.id,
          };
          this.yard[0].status = "O";
          this.yard[0].yardtimeout = this.weighdate;
          //alert(window.$http + `yardlist/` + this.yard[0].id);
          axios
            .put(window.$http + `yardlist/` + this.yard[0].id, yarddata, {
              headers: window.$headers,
            })
            .then(() => {
              //saveticket();
              Swal.fire({
                position: "center",
                icon: "success",
                title: "Vehicle removed from yard ",
                showConfirmButton: false,
                timer: 1500,
              }).then(() => {
                this.inyard = true;
                this.nodata.yardlistid = this.yard[0].id;
                this.generateReceipt2();
                // this.weighttaken = true;
                // this.timer = 1000000000;
                // this.isVisible = false;
              });
            })
            .catch((e) => {
              Swal.fire({
                position: "center",
                icon: "error",
                title: "Inserting Remove Vehicle from Yard" + e,
                showConfirmButton: true,
              }).then(() => {
                Swal.close();
              });
            });
        } else {
          this.generateReceipt2();
        }
      }
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
    getdbdate() {
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
    getv(val) {
      if (val < 10) {
        val = "0" + val;
      }
      return val;
    },
    generateReceipt2() {
      var yard = "E";
      if (this.vDirection2 == 0) {
        yard = "Y";
      }
      // else {
      //   saveticket();
      // }
      var nodata = this.nodata;
      nodata.wbrg_ticket_notes = this.comment;
      nodata.wbrg_ticket_driver = this.driver;
      nodata.wbgr_ticket_trailer_no = this.trailer;
      nodata.wbrg_ticket_directionarea = this.description;
      nodata.permit_no = this.permit;
      nodata.wbrg_ticket_direction = yard;
      nodata.wbrg_ticket_source = this.origin;
      nodata.wbrg_ticket_destination = this.destination;
      nodata.wbrg_tx_code = this.transporter;
      nodata.wbrg_product = this.cargo;
      nodata.reliefvehreg = this.reliefvehreg;

      var make = "";
      try {
        make = this.make.trim();
        nodata.wbrg_ticket_make = make;
      } catch (e) {
        make = "";
      }

      if (this.nodata.wbrg_ticket_excessload == "avw") {
        nodata.wbrg_ticket_state = "T";
      }
      if (this.nodata.wbrg_ticket_gvwload > this.allgvw) {
        nodata.wbrg_ticket_state = "C";
      }
      if (this.autospecialrelease) {
        nodata.wbrg_ticket_state = "S";
      }
      if (this.granted2) {
        this.$emit("addsp", [
          {
            reason: this.reason,
            description: this.description,
            userid: this.usersp,
          },
        ]);
      }

      if (this.nodata.wbrg_ticket_no.includes("_")) {
        // var getreweigh = this.nodata.wbrg_ticket_no.split("_");

        axios
          .get(
            window.$http +
              `weighbridgetransactions/` +
              this.yard[0].weighbridgeTransactionsid,
            {
              headers: window.$headers,
            }
          )
          .then((response) => {
            //alert(response.data);
            ////alert(response.data.wbrg_ticket_state);

            if (response.data.wbrg_ticket_state == "T") {
              response.data.wbrg_ticket_state = "R";
              axios
                .put(
                  window.$http + `weighbridgetransactions/` + response.data.id,
                  response.data,
                  { headers: window.$headers }
                )
                .then(() => {
                  //alert(response.data);
                });
            }
          });
      }
      if (this.tagged) {
        nodata.wbrg_ticket_tagged = 1;
      }
      //alert(this.tagged);
      //console.log(nodata);
      axios
        .put(window.$http + `weighbridgetransactions/` + nodata.id, nodata, {
          headers: window.$headers,
        })
        .then(() => {
          var direction = "Legal";
          if (yard == "Y") {
            direction = "Yard";
          }
          if (store.getters["screens/getBidirection"] == "Right") {
            if (direction == "Yard") {
              this.exitBoomdirection("left", "Yard");
            } else {
              this.exitBoomdirection("left", "Legal");
            }
          } else if (store.getters["screens/getBidirection"] == "Left") {
            if (direction == "Yard") {
              this.exitBoomdirection("Right", "Yard");
            } else {
              this.exitBoomdirection("Right", "Legal");
            }
          } else {
            this.exitBoom(direction);
          }

          this.$emit("vehileupdate");
          Swal.fire({
            position: "center",
            icon: "success",
            title: "Weight taken ",
            showConfirmButton: false,
            timer: 3000,
          }).then(() => {
            // this.weighttaken = true;
            // this.timer = 1000000000;
            // this.isVisible = false;
          });
          //this.viewticket(nodata, this.orderData, this.orderData3);
          this.previewticket(nodata.id);
          this.gotorouter();
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Inserting weighbridgetransactions" + e,
            showConfirmButton: true,
          }).then(() => {
            Swal.close();
          });
        });
      //alert(this.casereliefdetails.length);
      //alert(this.reliefvehreg);
      //alert("this.casereliefdetails.id" + this.casereliefdetails[0].id);
      if (this.casereliefdetails.length > 0 && this.reliefvehreg != "") {
        this.casereliefdetails[0].offloadreg = this.nplate;
        try {
          axios
            .put(
              window.$http + `casedetails/` + this.casereliefdetails[0].id,
              this.casereliefdetails[0],
              {
                headers: window.$headers,
              }
            )
            .then(() => {
              // JSON responses are automatically parsed.
              // this.$root.$emit("bv::hide::modal", "modal-Case");
              //var result = [];
              //alert(window.$http + `yardlist/` + this.yardlist[0].id);
              //Swal.close();
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
        } catch (e) {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Prosecution Details - update " + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });
        }
      }
    },
    exitBoomdirection(direction, action) {
      ////alert(this.yardlist[0].vehreg);
      ////alert(window.$http + `yardlist/search?vehreg=` + this.nplate);
      //var yardtick = "ATMBA202109000032";
      var data = {};
      var msg = "Exit Boom Open";
      if (action == "Yard") {
        msg = "Vehicle Sent To Yard";
      }
      // console.log(
      //   window.$http + `IOSettings/` + direction + `?Action=` + action
      // );
      axios
        .post(
          window.$http + `IOSettings/` + direction + `?Action=` + action,
          data,
          {
            headers: window.$headers,
          }
        )
        .then(() => {
          Swal.fire({
            position: "center",
            icon: "success",
            title: msg,
            showConfirmButton: true,
            timer: 3000,
          }).then(() => {
            Swal.close();
          });
          //console.log(response.data);
          //alert(response.data[0].vehreg);
        })
        .catch((e) => {
          console.log(e);
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Could Not Connect to Exit!",
            showConfirmButton: true,
          }).then(() => {
            Swal.close();
          });
        });
    },
    exitBoom(direction) {
      ////alert(this.yardlist[0].vehreg);
      ////alert(window.$http + `yardlist/search?vehreg=` + this.nplate);
      //var yardtick = "ATMBA202109000032";
      var data = {};
      var msg = "Exit Boom Open";
      if (direction == "Yard") {
        msg = "Vehicle Sent To Yard";
      }
      axios
        .post(window.$http + `IOSettings/deckExit?Action=` + direction, data, {
          headers: window.$headers,
        })
        .then(() => {
          Swal.fire({
            position: "center",
            icon: "success",
            title: msg,
            showConfirmButton: true,
            timer: 3000,
          }).then(() => {
            Swal.close();
          });
          //console.log(response.data);
          //alert(response.data[0].vehreg);
        })
        .catch((e) => {
          console.log(e);
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Could Not Connect to Exit!",
            showConfirmButton: true,
          }).then(() => {
            Swal.close();
          });
        });
    },
    printpdf(pl, p_pdf) {
      this.printReport = true;
      this.pl = pl;
      p_pdf = "Weighbridge ticket";
      this.clickedPrintpdf = p_pdf;
      //alert(this.showme);
      const data = this.orderData.map((row) => ({
        id: row.id,
        permitid: row.permitid,
        permitno: row.permitno,
        registration: row.registration,
        traillerreg: row.traillerreg,
        transp: row.transp,
        transptel: row.transptel,
        transpadd: row.transpadd,
        model: row.model,
        origin: row.origin,
        destination: row.destination,
        axleconf: row.axleconf,
        firstAkit: row.firstAkit,
        fireext: row.fireext,
        drunkDriver: row.drunkDriver,
        dlicensestate: row.dlicensestate,
        inspstick: row.inspstick,
        insuaranceStic: row.insuaranceStic,
        loaddisc: row.loaddisc,

        dpermitissu: row.dpermitissu,
        pStartD: row.pStartD,
        pEndD: row.pEndD2,
        insecureload: row.insecureload,
        lightstatus: row.lightstatus,
        sterringdef: row.sterringdef,
        fhBreak: row.fhBreak,
        aoDefects: row.aoDefects,
        height_: row.height_,
        length_: row.length_,
        width_: row.width_,

        abnormalLPermit: row.abnormalLPermit,
        totaltyres: row.totaltyres,
        perconf: row.perconf,
        nonperconf: row.nonperconf,

        remarks: row.remarks,
        weighofload: row.weighofload,
        authweight: row.authweight,
        dateofTravel: row.dateofTravel,
        status: row.state,
        permitdatetime: row.permitdatetime,
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
    async previewticket(id) {
      ////alert(nodata.id);
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
      var nodata = await axios
        .get(window.$http + `weighbridgetransactions/` + id, {
          headers: window.$headers,
        })
        .then((respnodata) => {
          return respnodata.data;
        })
        .catch((e) => {
          Swal.fire("Ticket Error" + e);
          //this.errors.push(e);
        });
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
      // var orderData = [];
      // var orderData3 = [];
      // Swal.fire({
      //   title: "Please Wait !",
      //   html: "Loading data...", // add html attribute if you want or remove
      //   allowOutsideClick: false,
      //   showConfirmButton: false,
      //   willOpen: () => {
      //     Swal.showLoading();
      //   },
      // });
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
        // console.log(weights);

        var a = 0;
        var b = 0;
        var c = 0;
        var d = 0;

        var pa = 0;
        var pb = 0;
        var pc = 0;
        var pd = 0;
        // console.log(orderData3);
        var orderd = orderData3;

        for (let k = 0; k < orderd.length; k++) {
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
      //alert(nodata.wbrg_ticket_tagged);

      doc.setFontSize(10);
      doc.text(5, 47, "Ticket No: " + nodata.wbrg_ticket_no);
      //alert(reweigh[1]);
      if (Number(reweigh[1]) > 0) {
        doc.text(95, 47, "Re-Weighed:" + reweigh[1]);
      }
      if (nodata.reliefvehreg != "" || nodata.reliefvehreg != "null") {
        doc.text(60, 47, "Relief Load from: " + nodata.reliefvehreg);
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
          doc.text(183, lane1, "Fee ($)");
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
    viewticket(nodata, orderData, orderData3) {
      var weigbri = "";

      axios
        .get(
          window.$http +
            `ClusterWeighbridges/search?cluster_code=` +
            nodata.wbrg_station,
          { headers: window.$headers }
        )
        .then((response) => {
          axios
            .get(
              window.$http +
                `users/search?email=` +
                nodata.wbrg_ticket_operator,
              {
                headers: window.$headers,
              }
            )
            .then((resp) => {
              var suser = resp.data;
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
              //this.tno = this.prefix + "000000" + this.nplate;
              //let pdfName = 'WeighbridgeReceipt';
              var doc = new jsPDF();
              //This is a key for printing
              doc.addFileToVFS("tahoma.ttf", Tahoma);
              doc.setFillColor(255, 255, 255);
              doc.roundedRect(5, 5, 200, 38, 1, 1, "FD");

              doc.setTextColor(0, 0, 0);
              doc.setFontSize(10);
              doc.addFont("Tahoma");
              doc.setFont("Tahoma");
              doc.text(60, 10, "KENYA NATIONAL HIGHWAYS AUTHORITY");
              doc.text(70, 15, "Quality Highways, Better Connections");
              doc.text(80, 25, "Axle Load Control");
              doc.text(
                150,
                30,
                "Print Date: " + this.getcurrentdate(new Date() + "")
              );

              doc.text(15, 40, "Weighbridge Ticket");
              doc.text(70, 40, "Weighbridge Name:" + weigbri);

              doc.text(180, 35, state);
              doc.setFont("Tahoma", "", "normal");
              doc.setFontSize(14);
              doc.text(180, 40, status);
              //alert(this.tagged);

              if (nodata.wbrg_ticket_tagged == 1) {
                doc.setFontSize(8);
                doc.text(180, 45, "Tagged");
              }
              alert(nodata.wbrg_ticket_tagged);

              //alert();
              doc.setFontSize(10);
              doc.text(5, 47, "Ticket No: " + nodata.wbrg_ticket_no);
              //alert(reweigh[1]);
              if (nodata.reliefvehreg != "" || nodata.reliefvehreg != "null") {
                doc.text(70, 47, "Relief Load from: " + nodata.reliefvehreg);
              }
              if (Number(reweigh[1]) > 0) {
                doc.text(85, 47, "Re-Weighed:" + reweigh[1]);
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
              if (orderData3.length >= 2) {
                si = si + orderData3.length * 6;
              }
              doc.roundedRect(5, 49, 200, si + 17, 0, 0, "FD");
              //alert(orderData.length);

              //doc.addImage(imgLogo.src, "PNG", 10, 10);
              var logo = require("@/assets/images/client.png");
              var imgLogo = new Image();
              imgLogo.src = logo;
              doc.addImage(imgLogo, "PNG", 10, 7, 40, 30);
              doc.text(6, 53, "Vehicle Registration No.: " + nodata.veh_reg);
              doc.addFont("bold");
              doc.text(83.5, 53, "Transporter:" + nodata.wbrg_tx_code);
              doc.text(160, 58, "Axle Type:" + nodata.wbrg_ticket_axel);
              doc.text(21.5, 58, "Vehicle Make: " + nodata.wbrg_ticket_make);
              doc.text(80, 58, "Source/Origin:" + nodata.wbrg_ticket_source);
              doc.text(154, 62, "Scale Operator:");
              doc.text(154, 66, suser[0].fullnames);
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

                ////alert(inc);
                inc += g + 4;
                lane1 += inc;
                lane2 += inc;
                lane3 += inc;
                lane4 += inc;
              }

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

                doc.text(58 + leng, lane4 + inc, "" + orderData[i].permissible);

                doc.text(
                  84 + leng,
                  lane4 + inc,
                  "" + orderData[i].tolerance + " 5%"
                );
                doc.text(120, lane4 + inc, "" + orderData[i].actual);
                doc.text(140, lane4 + inc, "" + orderData[i].overload);
                doc.text(163, lane4 + inc, "" + orderData[i].result);
                doc.text(183, lane4 + inc, "" + orderData[i].pdf);
                inc += 6;
                axleoverl += Number(orderData[i].overload);
                g++;
                gvwperm += Number(orderData[i].permissible);
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
              doc.text(183, lane4 + inc + 10, "2.2.i ");
              doc.setLineWidth(0.5);
              doc.line(5, lane4 + inc + 12, 205, lane4 + inc + 12);
              doc.text(
                6,
                lane4 + inc + 17,
                "Legend:GVW:Gross Vehicle Weight, Wheels:S, Single Conventional Tyre D:Double,W Super Single (wide)"
              );

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
              doc.setFont("Tahoma", "", "bold");
              doc.setFontSize(10);
              doc.text(40, lane4 + inc + 50, nodata.wbrg_ticket_directionarea);
              var year = nodata.wbrg_ticket_date.substring(0, 4);
              var month = nodata.wbrg_ticket_date.substring(5, 7);
              var day = nodata.wbrg_ticket_date.substring(8, 10);
              var hour = nodata.wbrg_ticket_date.substring(11, 13);
              var dir = year + month + "/" + day + "/" + hour + "/";

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
        .catch(() => {});
    },
    sendtoexit() {
      if (this.vDirection2 == 0) {
        var data = {
          events: "Sending Vehicle to Yard",
          datetime: this.getdbdate(),
          userid: window.$userid,
          desc:
            this.nplate +
            " Sent to Exit " +
            this.totalweight +
            " for ticket " +
            this.WBTNO,
        };
        axios
          .post(window.$http + `events/`, data, {
            headers: window.$headers,
          })
          .then(() => {
            //this.$router.push("/weigh/" + this.cat);
          })
          .catch(() => {
            //Swal.fire("" + e);
            //this.errors.push(e);
          });
      }
    },
    sendtoyard() {
      if (this.vDirection2 == 1) {
        Swal.fire({
          position: "center",
          icon: "error",
          title: "Sending Vehicle in Yard",
          text: "Do you want to send the vehicle to the Yard ?",
          showDenyButton: true,
          confirmButtonText: "Yes",
          denyButtonText: "No",
        }).then((result) => {
          if (result.isConfirmed) {
            var data = {
              events: "Sending Vehicle to Yard",
              datetime: this.getdbdate(),
              userid: window.$userid,
              desc:
                this.nplate +
                " Sent to Yard " +
                this.totalweight +
                " for ticket " +
                this.WBTNO,
            };
            axios
              .post(window.$http + `events/`, data, {
                headers: window.$headers,
              })
              .then(() => {
                //this.$router.push("/weigh/" + this.cat);
              })
              .catch((e) => {
                Swal.fire("" + e);
                //this.errors.push(e);
              });

            Swal.fire("Sending to yard!", "", "success");
          } else if (result.isDenied) {
            this.vDirection2 = 0;
            Swal.fire("Changes are not saved", "", "info");
          }
        });
      }
    },
    gotorouter() {
      this.$router.push("/weigh/" + this.cat);
    },
  },
};
</script>

<template>
  <div class="col-12">
    <!--image section-->
    <Deckimages :src2="src2" :src3="src3" />

    <!--end image section-->

    <!--vehicle plate number-->
    <div class="row justify-content-md-center">
      <div class="col-md-6">
        <div class="input-group-text" style="font-size: 20px">
          Send Vehicle Registration No.&#160;
          <p class="list-inline mb-0" style="font-weight: bold">
            {{ nplate }}
          </p>
          &#160;To
        </div>

        <!--radio-yard-exit-->
        <div class="row justify-content-md-center">
          <div class="d-flex justify-content-between col-12">
            <div>
              <div class="form-check py-2">
                <input
                  class="form-check-input mb-2"
                  type="radio"
                  v-model="vDirection2"
                  @change="handleDirectionStatus($event)"
                  @click="sendtoyard()"
                  value="0"
                  id="yard"
                />
                <label class="form-check-label px-2" for="yard">
                  <h5>Yard</h5>
                </label>
              </div>
              <div class="form-check py-2" v-show="tagged">
                <img :src="imae" width="100" />

                <label class="form-check-label ps-2" for="exitStation">
                  <h5>Tagged</h5>
                </label>
              </div>
              <div class="form-check py-2">
                <input
                  v-b-modal.modal-Edit
                  class="form-check-input"
                  type="radio"
                  v-model="vDirection2"
                  @change="handleDirectionStatus($event)"
                  @click="sendtoexit()"
                  value="1"
                  id="exitStation"
                />
                <label class="form-check-label ps-2" for="exitStation">
                  <h5>Exit</h5>
                </label>
              </div>
            </div>

            <div class="col-md-3" v-show="showAlert">
              <b-alert
                v-if="vDirection2 == 1"
                variant="success"
                show
                class="text-center"
              >
                <i
                  class="
                    uil uil-check-circle
                    d-block
                    display-6
                    mt-1
                    mb-2
                    text-success
                  "
                ></i>
                <h6 class="text-success">
                  {{ vDirection2 == 1 ? "Exit" : "Yard" }}
                </h6>
              </b-alert>

              <b-alert
                v-if="vDirection2 == 0"
                variant="danger"
                show
                class="text-center"
              >
                <i
                  class="
                    uil uil-check-circle
                    d-block
                    display-6
                    mt-1
                    mb-2
                    text-danger
                  "
                ></i>
                <h6 class="text-danger">
                  {{ vDirection2 == 1 ? "Exit" : "Yard" }}
                </h6>
              </b-alert>
            </div>
          </div>
        </div>

        <!--end radio-yard-exit-->

        <div
          v-show="granted2 || autospecialrelease"
          class="justify-content-md-center"
        >
          <b-alert
            v-if="granted2 || autospecialrelease"
            variant="success"
            show
            class=""
          >
            <h6>Special Release: {{ reason2 }} ; {{ description }}</h6>
          </b-alert>
        </div>

        <div class="justify-content-md-center">
          <div class="input-group">
            <div v-show="granted2" class="input-group-text col-sm-2">
              Reason:
            </div>

            <input
              v-show="granted2"
              class="form-control"
              type="text"
              placeholder="Enter reason for release"
              v-model="description"
            />
          </div>
        </div>
      </div>
    </div>

    <div class="row mt-2 justify-content-md-center">
      <div class="col-6 d-flex justify-content-between">
        <b-button
          @click="handleBackToDetails"
          variant="primary w-lg"
          style="background-color: black; color: yellow; float: right"
        >
          <i class="uil uil-angle-double-left ms-2"></i>
          Back
        </b-button>

        <b-button
          @click="generateReceipt()"
          variant="primary w-lg"
          style="background-color: black; color: yellow; float: right"
        >
          Finish ({{ vDirection2 == 1 ? "Exit" : "Yard" }})
          <i class="uil uil-angle-double-right ms-2"></i>
        </b-button>
      </div>
    </div>

    <b-modal
      v-show="!granted2 || !autospecialrelease"
      :id="modalname"
      :title="modaltitle"
      hide-footer
      size="lg"
      centered
    >
      <Accesscontrol
        v-bind:granted2="granted2"
        v-bind:usersp="usersp"
        v-show="!granted2"
        v-on:changegranted="changegranted"
      >
      </Accesscontrol>
    </b-modal>
    <b-modal id="modal-Print" title="Print PDF" hide-footer size="bg" centered>
      <reportdet
        :title="title"
        :printedpdf="true"
        :orderData="orderData"
        :pl="pl"
        :headers="headers"
        :uniqueCars="uniqueCars"
        v-show="showme"
      ></reportdet>
    </b-modal>
  </div>
</template>

<style scoped>
input[type="radio"] {
  width: 25px;
  height: 25px;
}
</style>
