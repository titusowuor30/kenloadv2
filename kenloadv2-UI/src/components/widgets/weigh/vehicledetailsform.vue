<script>
/**
 * Stat component
 */
import Deckweights from "@/components/widgets/weigh/deckweights";
import Axleweights from "@/components/widgets/weigh/axleweights";
import Multiselect from "vue-multiselect";
import Swal from "sweetalert2";
import store from "@/state/store";

import axios from "axios";
import Direction from "@/components/widgets/weigh/vehicle-direction";
//var lswimint=null;
//import { swal } from "vue/types/umd";
export default {
  props: {},
  name: "vehicledetails",
  components: {
    Deckweights,
    Direction,
    Multiselect,
    Axleweights,
  },
  methods: {
    onchangefalse(value) {
      this.isVisible = value; // someValue
    },
    addsp(events) {
      this.goToSrdoc(events[0].reason, events[0].description, events[0].userid);
    },
    updatevDirection(events) {
      this.vDirection = events[0].vDirection;
      this.granted = events[0].granted;
      this.reason = events[0].reason;
      this.modalname = events[0].modalname;
      this.autospecialrelease = events[0].autospecialrelease;
      this.description = events[0].description;
    },
    vehiclesupt() {
      if (this.vehicles.id > 0) {
        this.updatevehicles();
      } else {
        this.postvehicles();
      }
    },
    getcameras() {
      //screen and roles combination
      //alert("mounted");

      axios
        .get(window.$http + `Camera`, { headers: window.$headers })
        .then((response) => {
          //alert(window.$http + `Camera`);
          // JSON responses are automatically parsed.
          this.cameras = response.data;
          //Swal.close();
          //console.log(this.orderData);
          // this.nocameras = response.data.length;
          // for (let i = 0; i < response.data.length; i++) {
          //   //alert(response.data[i].position);
          //   if (
          //     response.data[i].position == "Front" ||
          //     response.data[i].position == "Rear"
          //   ) {
          //     this.anprcamera++;
          //   }
          // }
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Cameras" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
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
    updatehswim() {
      //alert(this.nodata);
      var d = new Date();
      var fromdate = this.mydatenew(d);
      let fhour = d.getHours();

      //alert("" + fromdate);

      d.setHours(d.getHours() - 1);

      var todate = this.mydatenew(d);

      var ampm = "AM";
      let thour = d.getHours();

      var fromhour = this.mydatehour(new Date());
      var tohour = this.mydatehour(d);

      if (Number(fhour) > 12) {
        ampm = "PM";
      }
      var ampm2 = "AM";
      if (Number(thour) > 12) {
        ampm2 = "PM";
      }
      todate = this.getmydate(fromdate) + " " + fromhour + " " + ampm + "";
      fromdate = this.getmydate(todate) + " " + tohour + " " + ampm2 + "";
      //alert(
      //   window.$http +
      //     `virtualtickets/search?veh_reg=` +
      //     this.nodata.veh_reg +
      //     `&fromdate=` +
      //     fromdate +
      //     `&todate=` +
      //     todate
      // );
      // console.log(
      //   window.$http +
      //     `virtualtickets/search?veh_reg=` +
      //     this.nodata.veh_reg +
      //     `&fromdate=` +
      //     fromdate +
      //     `&todate=` +
      //     todate
      // );
      axios
        .get(
          window.$http +
            `virtualtickets/search?veh_reg=` +
            this.nodata.veh_reg +
            `&fromdate=` +
            fromdate +
            `&todate=` +
            todate +
            `&wbrg_ticket_no_update=updated`,
          {
            headers: window.$headers,
          }
        )
        .then((respvirt) => {
          console.log(respvirt.data);
          respvirt.data[0].wbrg_ticket_no = this.WBTNO;
          //respvirt.data[0].axle_conf = this.axlec;
          //console.log("respvirt.data[0].id" + respvirt.data[0].id);
          axios
            .put(
              window.$http + `virtualtickets/` + respvirt.data[0].id,
              respvirt.data[0],
              {
                headers: window.$headers,
              }
            )
            .then((r) => {
              console.log(r.data);
            })
            .catch((e) => {
              Swal.fire({
                position: "center",
                icon: "error",
                title: "Inserting virtualticket" + e,
                showConfirmButton: true,
              }).then(() => {
                Swal.close();
              });
            });
        })
        .catch(() => {});
    },
    genimganpr(index) {
      this.getimg(index);
      this.getoverview(index);
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
    openboom() {
      var theUrl =
        window.$url +
        "/kenload/dashboard/openboom.php?string=http://192.168.4.57/cgi-bin/viewer/video.jpg&filename=" +
        this.WBTNO +
        "&dir=" +
        this.dir;

      //alert(theUrl);
      const logFileText = async (file) => {
        const response = await fetch(file);
        let text = await response.text();
        //alert(text);
        Swal.close(text);

        //alert(this.src2);
      };
      logFileText(theUrl);
    },
    getimg(index) {
      console.log(index);
      var ipaddress = "";
      var port = "80";
      var wb = window.$weighb;
      var imagepath = "";
      var protocol = "http";
      //alert(this.cameras.length);
      //alert(wb);
      try {
        for (let i = 0; i < this.cameras.length; i++) {
          if (
            this.cameras[i].position == "Front" &&
            this.cameras[i].weighbridge == wb
          ) {
            //alert(this.cameras[i].position);
            //alert(this.cameras[i].weighbridge);
            ipaddress = this.cameras[i].ipaddress;
            imagepath = this.cameras[i].file;
            protocol = this.cameras[i].protocol;
            port = this.cameras[i].port;
          }
        }
      } catch (e) {
        console.log(e);
      }
      // var theUrl =
      //   window.$url +
      //   "/kenload/dashboard/getimg.php?string=" +
      //   protocol +
      //   "://" +
      //   ipaddress +
      //   ":" +
      //   port +
      //   imagepath;
      // "&filename=" + "F" + this.WBTNO + ".jpg&dir=" + this.dir;
      var theUrl =
        window.$url +
        "/kenload/dashboard/getimg.php?string=" +
        protocol +
        "://" +
        ipaddress +
        ":" +
        port +
        imagepath +
        "&filename=" +
        "F" +
        this.WBTNO +
        ".jpg&dir=" +
        this.dir;
      if (store.getters["screens/getcameratype"] == "hikvision") {
        theUrl =
          window.$http +
          "UploadFile/CaptureImage?url=" +
          protocol +
          "://" +
          ipaddress +
          ":" +
          port +
          imagepath +
          this.dir +
          "&imageName=F" +
          this.WBTNO +
          ".jpg";
      }
      //alert(theUrl);
      if (store.getters["screens/getBidirection"] == "Left") {
        if (store.getters["screens/getcameratype"] == "hikvision") {
          theUrl =
            window.$http +
            "UploadFile/CaptureImage?url=" +
            protocol +
            "://" +
            ipaddress +
            ":" +
            port +
            imagepath +
            this.dir +
            "&imageName=F" +
            this.WBTNO +
            ".jpg";
        }
      }

      if (store.getters["screens/getBidirection"] == "Right") {
        for (let i = 0; i < this.cameras.length; i++) {
          //alert(this.cameras[i].file);
          if (
            this.cameras[i].position == "Front" &&
            this.cameras[i].weighbridge == "B"
          ) {
            //alert(this.cameras[i].file);
            //alert(this.cameras[i].position);
            //alert(this.cameras[i].weighbridge);
            ipaddress = this.cameras[i].ipaddress;
            imagepath = this.cameras[i].file;
            protocol = this.cameras[i].protocol;
            port = this.cameras[i].port;
          }
        }
        if (store.getters["screens/getcameratype"] == "hikvision") {
          theUrl =
            window.$http +
            "UploadFile/CaptureImage?url=" +
            protocol +
            "://" +
            ipaddress +
            ":" +
            port +
            imagepath +
            this.dir +
            "&imageName=F" +
            this.WBTNO +
            ".jpg";
        } else {
          theUrl =
            window.$url +
            "/kenload/dashboard/getimg.php?string=" +
            protocol +
            "://" +
            ipaddress +
            ":" +
            port +
            imagepath +
            "&filename=" +
            "F" +
            this.WBTNO +
            ".jpg&dir=" +
            this.dir;
        }
      }
      const logFileText = async (file) => {
        const response = await fetch(file);
        let text = await response.text();
        //alert(text);
        this.src2 = text;
        Swal.close(text);
        //alert(this.src2);
      };
      logFileText(theUrl);
    },
    getoverview(index) {
      console.log(index);
      var ipaddress = "";
      var port = "80";
      var wb = window.$weighb;
      var imagepath = "/cgi-bin/viewer/video.jpg";
      var protocol = "http";
      //alert(this.cameras.length);
      //alert(wb);
      try {
        for (let i = 0; i < this.cameras.length; i++) {
          if (store.getters["screens/getBidirection"] == "Left") {
            if (
              this.cameras[i].position == "Side" &&
              this.cameras[i].weighbridge == "B"
            ) {
              //alert(this.cameras[i].position);
              //alert(this.cameras[i].weighbridge);
              ipaddress = this.cameras[i].ipaddress;
              imagepath = this.cameras[i].file;
              protocol = this.cameras[i].protocol;
              port = this.cameras[i].port;
            }
          } else {
            if (
              this.cameras[i].position == "Side" &&
              this.cameras[i].weighbridge == wb
            ) {
              //alert(this.cameras[i].position);
              //alert(this.cameras[i].weighbridge);
              ipaddress = this.cameras[i].ipaddress;
              imagepath = this.cameras[i].file;
              protocol = this.cameras[i].protocol;
              port = this.cameras[i].port;
            }
          }
        }
      } catch (e) {
        console.log(e);
      }
      // var theUrl =
      //   window.$url +
      //   "/kenload/dashboard/getimg.php?string=" +
      //   protocol +
      //   "://" +
      //   ipaddress +
      //   ":" +
      //   port +
      //   imagepath;
      // "&filename=" + "O" + this.WBTNO + ".jpg&dir=" + this.dir;
      var theUrl =
        window.$url +
        "/kenload/dashboard/getimg.php?string=" +
        protocol +
        "://" +
        ipaddress +
        ":" +
        port +
        imagepath +
        "&filename=" +
        "O" +
        this.WBTNO +
        ".jpg&dir=" +
        this.dir;
      //alert(theUrl);
      const logFileText = async (file) => {
        const response = await fetch(file);
        let text = await response.text();
        //alert(text);
        this.src3 = text;
      };
      logFileText(theUrl);
    },
    async goTonoplate() {
      if (this.axlec.trim() == "") {
        Swal.fire("Please select Axle Type!");
        this.$refs.axlec.focus();
        return;
      }
      var rea = "";
      const { value: text } = await Swal.fire({
        title:
          "Are you sure, you want to cancel weighing for " + this.nplate + " ?",
        input: "textarea",
        inputLabel: "Enter reason:",
        inputPlaceholder: "Enter reason here...",
        inputAttributes: {
          "aria-label": "Enter reason here",
        },
        icon: "danger",
        showCancelButton: true,
        confirmButtonText: "Yes, Cancel Weighing!",
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
      });
      try {
        rea = text;
      } catch (e) {
        rea = "";
      }
      //alert(rea.length);

      if (rea.length <= 0) {
        Swal.fire("Please enter reason to cancel weighing!");
        return;
      }

      var data = {
        events: "Weighing Cancelled",
        datetime: this.getcurrentdate(),
        userid: window.$userid,
        desc:
          this.nplate +
          " GVW: " +
          this.totalweight +
          " for ticket " +
          this.WBTNO,
      };
      var wbrg_ticket_type = "L";
      if (this.cat == "multideck") {
        wbrg_ticket_type = "M";
      }
      if (this.cat == "saw") {
        wbrg_ticket_type = "S";
      }
      axios
        .get(window.$http + `weighbridgetransactions/` + this.WBTNO, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.

          var id = response.data.id;
          //alert(id);
          response.data.wbrg_product = this.cargo;
          response.data.wbrg_ticket_type = wbrg_ticket_type;
          response.data.wbrg_ticket_dateout = this.getcurrentdate();

          response.data.wbrg_ticket_status = "X";
          response.data.wbrg_ticket_grossweight = "0.0";

          response.data.wbrg_ticket_axel = this.axlec;

          response.data.wbrg_ticket_shift = this.shift;
          response.data.wbrg_ticket_notes = this.comment;
          response.data.wbrg_ticket_driver = this.driver;
          response.data.wbgr_ticket_trailer_no = this.trailer;
          if (this.validpermit) {
            response.data.permit_no = this.permit;
          }

          response.data.wbrg_ticket_direction = "Y";
          response.data.wbrg_ticket_source = this.origin;
          response.data.wbrg_ticket_destination = this.destination;
          response.data.wbrg_tx_code = this.transporter;

          //alert("orderData" + this.orderData.length);
          var axleoverl = 0;
          for (let i = 0; i < this.orderData.length; i++) {
            //alert(i);
            axleoverl += Number(this.orderData[i].overload);
            if (i == 0) {
              response.data.axle1_weight = this.orderData[i].actual;
              response.data.axle1_limit = this.orderData[i].permissible;
              response.data.axle1_time = this.getcurrentdate();
              response.data.axle1_group_type1 = "1";
              response.data.axle1_group_type2 = "A";
            }
            if (i == 1) {
              response.data.axle2_weight = this.orderData[i].actual;
              response.data.axle2_limit = this.orderData[i].permissible;
              response.data.axle2_time = this.getcurrentdate();
              response.data.axle2_group_type1 = "2";
              response.data.axle2_group_type2 = "B";
            }

            if (i == 2) {
              response.data.axle3_weight = this.orderData[i].actual;
              response.data.axle3_limit = this.orderData[i].permissible;
              response.data.axle3_time = this.getcurrentdate();
              response.data.axle3_group_type1 = "3";
              response.data.axle3_group_type2 = "C";
            }
            if (i == 3) {
              response.data.axle4_weight = this.orderData[i].actual;
              response.data.axle4_limit = this.orderData[i].permissible;
              response.data.axle4_time = this.getcurrentdate();
              response.data.axle4_group_type1 = "4";
              response.data.axle4_group_type2 = "D";
            }
          }
          response.data.wbrg_ticket_axleload = axleoverl;
          response.data.wbrg_ticket_gvwload = this.gvwoverload;

          //response.data.wbrg_ticket_grossweight;

          if (axleoverl > this.gvwoverload) {
            response.data.wbrg_ticket_excessload = "avw";
            response.data.wbrg_ticket_excessweight = axleoverl;
          }

          if (this.gvwoverload > axleoverl) {
            response.data.wbrg_ticket_excessload = "gvw";
            response.data.wbrg_ticket_excessweight = this.gvwoverload;
          }

          if (this.tagged) {
            response.data.wbrg_ticket_tagged = 1;
          }

          response.data.wbrg_ticket_grossweight = this.totalweight;
          this.nodata = response.data;

          var detail = {
            wbrg_ticket_no: this.WBTNO,
            wbrg_ticket_gvwload: this.gvwoverload,
            wbrg_ticket_axleload: axleoverl,
            wbrg_ticket_grossweight: this.totalweight,
            wbrg_ticket_dateout: this.weighdate,
            wbrg_ticket_axel: this.axlec,
            Weighbridgetransactionsid: id,
            timestamp: this.getcurrentdate(),
            nplate: this.nplate,
            weightaken: 1,
          };

          //"Content-Type": "application/x-www-form-urlencoded",
          var optionAxios = {
            headers: {
              "Content-Type": "application/x-www-form-urlencoded",
            },
          };

          axios
            .post(`http://127.0.0.1:3031/weights`, detail, optionAxios)
            .then((res) => {
              //tt = false;
              this.weightres = res;
            })
            .catch((e) => {
              Swal.fire("Send Weights" + e);
              console.log(e);
            });

          axios
            .put(window.$http + `weighbridgetransactions/` + id, this.nodata, {
              headers: window.$headers,
            })
            .then((tes) => {
              console.log(tes);
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
      axios
        .post(window.$http + `events/`, data, { headers: window.$headers })
        .then(() => {
          Swal.fire(
            "Cancelled Weighing!",
            "Cancelled weighing " + this.nplate + " ",
            "success"
          );
          this.$router.push("/weigh/" + this.cat);
        })
        .catch((e) => {
          Swal.fire("" + e);
          //this.errors.push(e);
        });
    },
    mydatehour(d) {
      let hour = d.getHours();
      let min = d.getMinutes();
      let sec = d.getSeconds();
      hour = hour % 12;
      hour = hour ? hour : 12;

      min = this.getv(min);
      sec = this.getv(sec);

      //const msec = d.getMilliseconds();
      const datetime = hour + ":" + min + ":" + sec;

      return datetime;
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
    focusInput() {
      //const input = this.$refs.nplate;
      this.$refs.permit.focus();
      //this.elments[1].focus();
      //alert(this.nplate);
    },
    gonext() {
      //alert("this.authgiven" + this.authgiven);
      //alert("this.authweight" + this.authweight);
      //alert("this.validpermit" + this.validpermit);

      if ((this.authweight || this.validpermit) && this.authgiven == false) {
        this.getpermitsp();
        this.authgiven = true;
      }
      this.isVisible = false;
    },
    goToVehicleDir() {
      if (this.axlec.trim() == "") {
        Swal.fire("Please select Axle Type!");
        this.$refs.axlec.focus();
        return;
      }
      if (this.cat == "multideck") {
        if (this.vehicleposition.trim() != "") {
          Swal.fire(this.vehicleposition + " Error!");
          this.$refs.axlec.focus();
          return;
        }
      }
      //alert(this.vehicles.vehicle_axle);
      if (this.vehicles.id > 0) {
        if (this.axlec != this.vehicles.vehicle_axle) {
          //alert(this.WBTNO);
          this.$root.$emit("bv::show::modal", "modal-AxleConfig");
          return;
        }
      }

      //if (this.cat == "multideck") {
      var wcount = 0;
      var pcount = 0;
      var str = "";
      if (this.cat == "multideck") {
        for (let i = 0; i < this.weights.length; i++) {
          //alert(
          //   Number(this.weights[i]) + " Deck: " + wcount + " Deck" + this.deck1
          // );
          if (Number(this.weights[i]) > 100) {
            wcount++;
            //alert(Number(this.weights[i]) + " Deck: " + wcount);
          }
        }
        for (let i = 0; i < this.permissible.length; i++) {
          if (Number(this.permissible[i]) > 100) {
            pcount++;
            //alert(Number(this.permissible[i]) + " permissible: " + pcount);
          }
        }
        str =
          "<div> Axle Type:" +
          this.axlec +
          "</div>" +
          "<div> Decks Set:" +
          pcount +
          "</div>" +
          "<div> Decks Required:" +
          wcount +
          "</div>";
      }
      if (this.cat == "lswim") {
        pcount = Number(this.axlec.substring(0, 1));
        for (let i = 0; i < this.axleweight.length; i++) {
          if (Number(this.axleweight[i]) > 100) {
            wcount++;
            //alert(Number(this.axleweight[i]) + " Deck: " + wcount);
          }
        }
        //  for (let i = 0; i < this.axleweight.length; i++) {
        //   if (Number(this.axleweight[i]) > 100) {
        //     pcount++;
        //     //alert(Number(this.permissible[i]) + " permissible: " + pcount);
        //   }
        // }
        str =
          "<div> Axle Type:" +
          this.axlec +
          "</div>" +
          "<div> Axle(s) Weighed:" +
          wcount +
          "</div>" +
          "<div> Axle(s) Required:" +
          pcount +
          "</div>";
      }

      if (wcount != pcount) {
        Swal.fire({
          title: "Error: Vehicle not properly positioned!",
          html: "<pre>" + str + "</pre>" + "?",
          icon: "error",
          showCancelButton: false,
          confirmButtonColor: "#000000",
          cancelButtonColor: "#f46a6a",
          confirmButtonText: "OK!",
        });
        this.$refs.axlec.focus();
        return;
      }
      //}
      //alert(this.WBTNO);
      if (this.reweigh > 0) {
        var diff = Math.abs(
          Number(this.reweighticket.wbrg_ticket_grossweight) -
            Number(this.totalweight)
        );

        //alert("this.totalweight" + this.totalweight);
        if (diff > 100) {
          var strs =
            "<div> First GVW:" +
            this.reweighticket.wbrg_ticket_grossweight +
            "</div>" +
            "<div> Current GVW:" +
            this.totalweight +
            "</div>" +
            "<div> Difference GVW:" +
            diff +
            "</div>";

          Swal.fire({
            title: "Weight issue difference of " + diff + " [KG]",
            html: "<pre>" + strs + "</pre>" + "",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "alert alert-danger",
            cancelButtonColor: "#f46a6a",
            confirmButtonText: "Yes, Continue!",
          }).then((result) => {
            if (result.value) {
              var data = {
                events: "Weight on Reweigh Different",
                datetime: this.getcurrentdate(),
                userid: window.$userid,
                desc:
                  this.nplate +
                  " First GVW:" +
                  this.reweighticket.wbrg_ticket_grossweight +
                  " Second GVW: " +
                  this.totalweight +
                  " for ticket " +
                  this.WBTNO,
              };

              axios
                .post(window.$http + `events/`, data, {
                  headers: window.$headers,
                })
                .then(() => {
                  this.gotoweighing();
                })
                .catch((e) => {
                  Swal.fire("" + e);
                  //this.errors.push(e);
                });
            } else {
              return;
            }
          });
        } else {
          //alert("Diff");
          this.gotoweighing();
        }
      } else {
        //alert("Diff");
        this.gotoweighing();
      }
    },
    gotoweighing() {
      //alert();
      var alt = "";
      var cstate = "";
      var styl = "";
      if (this.status == "Overload") {
        alt = "alert ";
        cstate = "O";
        styl = "background-color: red; font-text:yellow;color:yellow;";
      }
      if (this.status == "Error") {
        alt = "alert alert-danger";
        styl = "background-color: red; font-text:yellow;color:yellow;";
        cstate = "E";
      }
      if (this.status == "Warning") {
        alt = "alert alert-info";
        cstate = "W";
      }
      if (this.status == "Legal") {
        alt = "alert alert-success";
        styl = "background-color: green; font-text:yellow;color:yellow;";
        cstate = "L";
      }
      var myclass = "font-size-14 mb-0";
      if (this.gvwoverload > 0) {
        myclass = "bg-soft-danger font-size-14 mb-0";
      }
      var mygvwlabel = "badge bg-pill bg-soft-success font-size-20";
      if (this.gvwlabel == "Error") {
        mygvwlabel = "badge bg-pill bg-soft-danger font-size-20 ";
      }

      if (this.gvwlabel == "Overload") {
        mygvwlabel = "badge bg-pill bg-soft-danger font-size-20 ";
      }

      if (this.gvwlabel == "Legal") {
        mygvwlabel = "badge bg-pill bg-soft-success font-size-20 ";
      }
      if (this.gvwlabel == "Warning") {
        mygvwlabel = "badge bg-pill bg-soft-warning font-size-20 ";
      }
      this.weighttaking = true;
      var bottomtable =
        " <table width='100%'> <thead></thead> <tbody> <tr> <th class='font-size-14 mb-0' style='text-align: center' colspan='2'> Permissible [KG]: " +
        this.gvwperm +
        "</th>" +
        "  <th class='font-size-14 mb-0'  style='text-align: center'  colspan='2'  >  GVW [KG]:" +
        this.totalweight +
        "  </th> <th class='" +
        myclass +
        "'" +
        "style='text-align: center' colspan='2' > GVW Overload [KG]:" +
        this.gvwoverload +
        "</th> <th class='" +
        mygvwlabel +
        "'> " +
        this.gvwlabel +
        " </th></tr></tbody></table>";
      var table =
        " <table width='100%'> <thead></thead> <tbody> <tr> <th class='font-size-14 mb-0'> Group </th> <th class='font-size-14 mb-0'> Permissible: </th>  <th class='font-size-14 mb-0'> Tolerance: </th> <th class='font-size-14 mb-0'> Actual: </th> <th class='font-size-14 mb-0'> Overload: </th> <th class='font-size-14 mb-0'> Result: </th> </tr>";
      var inc = 1;
      for (let i = 0; i < this.orderData.length; i++) {
        table += " <tr> <td>Group " + inc + "</td>";

        table += " <td>  " + this.orderData[i].permissible + " </td>";
        table += " <td>  " + this.orderData[i].tolerance + " 5%" + " </td>";
        table += " <td>  " + this.orderData[i].actual + " </td>";
        myclass = "";
        if (Number(this.orderData[i].overload) > 0) {
          myclass = "bg-soft-danger font-size-14 mb-0";
        }
        table += " <td " + this.orderData[i].overload + " </td>";
        table +=
          " <td class='" + myclass + "'>" + this.orderData[i].result + " </td>";

        table += " </tr>  ";
        inc++;
      }

      table += " </tbody></table>";

      var str =
        "<div> Axle Type:" +
        this.axlec +
        "</div>" +
        "<div style='" +
        styl +
        " font-weight:bold;' class='" +
        alt +
        " role='alert'>" +
        this.status +
        "</div>" +
        "<div>" +
        table +
        "</div>" +
        "<div>" +
        bottomtable +
        "</div>";
      if (this.overspeed) {
        Swal.fire({
          title: "Overspeed for " + this.nplate + "",
          text: "" + this.speed + " km/h",
          icon: "warning",
          showCancelButton: true,
          confirmButtonColor: "#000000",
          cancelButtonColor: "#f46a6a",
          confirmButtonText: "Yes, Restart Weighing",
        }).then((result) => {
          if (result.value) {
            Swal.fire(
              "Restarting !",
              this.nplate + " reset of weight values !.",
              "success"
            );
            this.restartlswimservice();
            return;
          }
        });
        return;
      }

      Swal.fire({
        title:
          "Are you sure, you want to Take Vehicle Weight for " +
          this.nplate +
          "?",
        html: "<pre>" + str + "</pre>" + "",
        icon: "warning",
        width: "800px",
        showCancelButton: true,
        allowOutsideClick: false,
        confirmButtonColor: alt,
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, Take Weight!",
      }).then((result) => {
        if (result.value) {
          this.saveweights(cstate);
        } else {
          this.weighttaking = false;
        }
      });
    },
    saveweights(cstate) {
      var yard = "E";
      if (this.vehicleDirection == 1) {
        yard = "Y";
      }
      this.weighdate = this.getcurrentdate();
      var wbrg_ticket_type = "L";
      if (this.cat == "multideck") {
        wbrg_ticket_type = "M";
      }
      if (!this.staticw) {
        wbrg_ticket_type = "S";
      }
      var id = 0;
      //alert(window.$http + `weighbridgetransactions/` + this.WBTNO);
      axios
        .get(window.$http + `weighbridgetransactions/` + this.WBTNO, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.

          id = response.data.id;
          //alert(id);
          response.data.wbrg_product = this.cargo;
          response.data.wbrg_ticket_type = wbrg_ticket_type;
          response.data.wbrg_ticket_dateout = this.weighdate;

          response.data.wbrg_ticket_status = cstate;
          response.data.wbrg_ticket_grossweight = "0.0";

          response.data.wbrg_ticket_axel = this.axlec;

          response.data.wbrg_ticket_shift = this.shift;
          response.data.wbrg_ticket_notes = this.comment;
          response.data.wbrg_ticket_driver = this.driver;
          response.data.wbgr_ticket_trailer_no = this.trailer;
          if (this.validpermit) {
            response.data.permit_no = this.permit;
          }
          response.data.wbrg_ticket_direction = yard;
          response.data.wbrg_ticket_source = this.origin;
          response.data.wbrg_ticket_destination = this.destination;
          response.data.wbrg_tx_code = this.transporter;

          //alert("orderData" + this.orderData.length);
          var axleoverl = 0;
          for (let i = 0; i < this.orderData.length; i++) {
            //alert(i);
            axleoverl += Number(this.orderData[i].overload);
            if (i == 0) {
              response.data.axle1_weight = this.orderData[i].actual;
              response.data.axle1_limit = this.orderData[i].permissible;
              response.data.axle1_time = this.weighdate;
              response.data.axle1_group_type1 = "1";
              response.data.axle1_group_type2 = "A";
            }
            if (i == 1) {
              response.data.axle2_weight = this.orderData[i].actual;
              response.data.axle2_limit = this.orderData[i].permissible;
              response.data.axle2_time = this.weighdate;
              response.data.axle2_group_type1 = "2";
              response.data.axle2_group_type2 = "B";
            }

            if (i == 2) {
              response.data.axle3_weight = this.orderData[i].actual;
              response.data.axle3_limit = this.orderData[i].permissible;
              response.data.axle3_time = this.weighdate;
              response.data.axle3_group_type1 = "3";
              response.data.axle3_group_type2 = "C";
            }
            if (i == 3) {
              response.data.axle4_weight = this.orderData[i].actual;
              response.data.axle4_limit = this.orderData[i].permissible;
              response.data.axle4_time = this.weighdate;
              response.data.axle4_group_type1 = "4";
              response.data.axle4_group_type2 = "D";
            }
          }
          response.data.wbrg_ticket_axleload = axleoverl;
          response.data.wbrg_ticket_gvwload = this.gvwoverload;

          //response.data.wbrg_ticket_grossweight;

          if (axleoverl > this.gvwoverload) {
            response.data.wbrg_ticket_excessload = "avw";
            response.data.wbrg_ticket_excessweight = axleoverl;
          }

          if (this.gvwoverload > axleoverl) {
            response.data.wbrg_ticket_excessload = "gvw";
            response.data.wbrg_ticket_excessweight = this.gvwoverload;
          }
          if (this.gvwoverload > 0) {
            Swal.fire({
              position: "center",
              icon: "error",
              title: "Has a GVW overload of " + this.gvwoverload + " [KG]",
              showConfirmButton: true,
            }).then(() => {});
          }
          response.data.wbrg_ticket_grossweight = this.totalweight;
          if (this.tagged) {
            response.data.wbrg_ticket_tagged = 1;
          }

          this.nodata = response.data;

          // const formData = new FormData();

          // // Add images to form data
          // formData.append("bannerImg", "bannerImg");
          // formData.append("inAppImg", "appimg");
          // formData.append("inAppImg", "img");

          // // Add the serialized JSON data to the formData (not
          // // sure what your JSON object is called)
          // formData.append("data", JSON.stringify(this.WBTNO));

          var detail = {
            wbrg_ticket_no: this.WBTNO,
            wbrg_ticket_gvwload: this.gvwoverload,
            wbrg_ticket_axleload: axleoverl,
            wbrg_ticket_grossweight: this.totalweight,
            wbrg_ticket_dateout: this.weighdate,
            wbrg_ticket_axel: this.axlec,
            Weighbridgetransactionsid: id,
            timestamp: this.getcurrentdate(),
            nplate: this.nplate,
            weightaken: 1,
          };

          //"Content-Type": "application/x-www-form-urlencoded",
          var optionAxios = {
            headers: {
              "Content-Type": "application/x-www-form-urlencoded",
            },
          };

          axios
            .post(`http://127.0.0.1:3031/weights`, detail, optionAxios)
            .then((res) => {
              //tt = false;
              this.weightres = res;
            })
            .catch((e) => {
              Swal.fire("Send Weights" + e);
              console.log(e);
            });

          // axios
          //   .post(`http://127.0.0.1:3031/weights`, detail)
          //   .then(() => {
          //     //tt = false;
          //   })
          //   .catch((e) => {
          //     Swal.fire("Send Weights" + e);
          //     console.log(e);
          //   });
          axios
            .put(window.$http + `weighbridgetransactions/` + id, this.nodata, {
              headers: window.$headers,
            })
            .then(() => {
              this.genimganpr("");
              if (this.cat == "lswim") {
                this.insertaxleweights(id, this.orderData3);
              } else {
                this.insertdeckweights(id, this.orderData);
              }

              if (this.$route.params.vehicles.id > 0) {
                this.updatevehicles();
              } else {
                this.postvehicles();
              }
              var axleexist = false;
              for (let i = 0; i < this.axles.length; i++) {
                //alert(this.axles[i]);
                if (this.axlec == this.axles[i]) {
                  axleexist = true;
                }
              }
              if (!axleexist) {
                this.postaxles();
              }
              if (this.authweight) {
                var re = this.getpermitsp();
                if (re) {
                  this.authgiven = true;
                }

                //alert("authgiven");
              }
              try {
                this.updatehswim();
              } catch (e) {
                console.log(e);
              }

              // JSON responses are automatically parsed.
              //  console.log(response.data);
              //Swal.close();
              //alert(id);
              //this.autospecialrelease = this.getsp();

              if (this.getsp() && !this.authgiven) {
                //alert("SP");
                this.vehicleDirection = 1;
                //this.vDirection = this.vehicleDirection;
              }
              //alert("this.validpermit" + this.validpermit);
              //alert("this.authgiven" + this.authgiven);

              if (this.validpermit && this.authgiven == false) {
                this.getpermitsp();
                this.vehicleDirection = 1;
              }

              this.vDirection = this.vehicleDirection;

              try {
                if (this.reweigh > 0 && this.prohibitionno == "") {
                  //alert(this.reweighticket.wbrg_ticket_date);

                  Swal.fire({
                    position: "center",
                    icon: "error",
                    title:
                      "Compliance Certificate cannot be raised! Probition order not raised!",
                    showConfirmButton: true,
                  }).then(() => {});
                }
                if (
                  this.prohibitionno != "" &&
                  Number(this.nodata.wbrg_ticket_excessweight) -
                    Number(this.allgvw) <=
                    0
                ) {
                  var mycasid = "CC";
                  var doc = "Compliance Certificate";
                  this.updatecasedocs(mycasid, doc);
                }
              } catch (e) {
                console.log(e);
              }
              this.endthis();
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
    },
    getpermitsp() {
      var res = false;
      ////alert(this.status);
      ////alert(this.orderData[0].overload);
      if (this.status != "Overload" || this.orderData[0].overload > 0) {
        return;
      }
      //alert(
      //   "this.authweight" +
      //     this.authweight +
      //     "this.tareweight" +
      //     this.tareweight +
      //     "this.gvwperm" +
      //     this.gvwperm
      // );
      var diffw =
        Number(this.authweight) +
        Number(this.tareweight) -
        Number(this.gvwperm);
      //alert(diffw);
      var over = false;

      for (let i = 1; i < this.orderData.length; i++) {
        //alert("this.orderData[i].overload" + this.orderData[i].overload);
        if (Number(this.orderData[i].overload) > 3000) {
          over = true;
        }
      }
      //alert(over);
      var gv = 0;
      var axl = Number(this.axlec.substring(0, 1));
      //alert(axl);
      if (Number(axl) <= 2) {
        gv = 1000;
      }
      if (Number(axl) > 2 && Number(axl) < 5) {
        gv = 2000;
      }

      if (this.gvwoverload > gv || over == true) {
        //alert("Invalid Authorized weight!");
        Swal.fire("Invalid Authorized weight! " + this.authweight);
        return;
      }
      ////alert("this.gvwoverload" + this.gvwoverload);
      ////alert("this.gv" + gv);
      ////alert("over" + over);

      if (diffw > 0) {
        Swal.fire("Special Realease Issued! For " + this.registration);
        this.reason = "Vehicle with Permit";
        this.description = "Vehicle has a valid permit " + this.permit;
        this.autospecialrelease = true;
        this.vehicleDirection = 1;
        this.vDirection = this.vehicleDirection;
        //this.userautosp = window.$userid;
        this.goToSrdoc(this.reason, this.description, "KenloadV2_System");
        res = true;
      }
      return res;
    },
    endthis() {
      var spe = "";
      if (this.autospecialrelease) {
        spe =
          " Special Release Issued " + this.reason + "; " + this.description;
      }
      Swal.fire({
        position: "center",
        icon: "success",
        title: "Weight stored! " + spe,
        showConfirmButton: true,
        timer: 3000,
      }).then(() => {
        this.weighttaken = true;
        // this.timer = 1000000;
        this.num = -1;
        this.isVisible = false;
      });
    },
    getfee(column, weight) {
      var fee = 0.0;
      //alert("weight" + weight);

      if (Number(weight) > 99) {
        for (var i = 0; i < this.AVWoverloadCharges.length; i++) {
          var d = 0;
          if (i > 0) {
            d = 1;
          }
          if (Number(this.AVWoverloadCharges[i].overloadkg) == Number(weight)) {
            d = 0;
          }
          //alert(column);
          //alert(weight);
          if (Number(weight) >= Number(this.AVWoverloadCharges[i].overloadkg)) {
            if (column == "steeringaxle") {
              fee = this.AVWoverloadCharges[i - d].steeringaxle;
              i = this.AVWoverloadCharges.length + 1;
            }
            if (column == "singledriveaxle") {
              fee = this.AVWoverloadCharges[i - d].singledriveaxle;
              i = this.AVWoverloadCharges.length + 1;
            }
            if (column == "tandemaxle") {
              fee = this.AVWoverloadCharges[i - d].tandemaxle;
              i = this.AVWoverloadCharges.length + 1;
            }
            if (column == "tridemaxle") {
              fee = this.AVWoverloadCharges[i - d].tridemaxle;
              i = this.AVWoverloadCharges.length + 1;
            }
            //alert("fee" + fee);
          }
        }
      }
      return fee;
    },
    getaxleconf(group) {
      var conf = 0;
      for (let i = 0; i < this.AxleWeightXreffs.length; i++) {
        if (this.AxleWeightXreffs[i].axle_code == this.axlec) {
          //alert(this.AxleWeightXreffs[i].axle_deckgrouping);
          //alert(group);
          if (this.AxleWeightXreffs[i].axle_deckgrouping == group) {
            conf++;
          }
        }
      }
      //alert(conf);
      return conf;
    },
    getaxlegrp() {
      var conf = [];
      for (let i = 0; i < this.AxleWeightXreffs.length; i++) {
        if (this.AxleWeightXreffs[i].axle_code == this.axlec) {
          conf.push(this.AxleWeightXreffs[i].axle_deckgrouping);
        }
      }
      //console.log(conf);
      return conf;
    },
    insertdeckweights(id, orderData) {
      let axleno = 1;
      var grp = ["A", "B", "C", "D"];
      var fee = 0.0;
      var lesspermit = 0;
      if (this.validpermit) {
        if (Number(this.axlec.substring(0, 1)) == 2) {
          lesspermit = 1000;
        }
        if (
          Number(this.axlec.substring(0, 1)) == 3 ||
          Number(this.axlec.substring(0, 1)) == 4
        ) {
          lesspermit = 2000;
        }
      }
      for (var i = 0; i < orderData.length; i++) {
        var actual = Number(orderData[i].actual);
        var perm = Number(orderData[i].permissible);
        var tollerance = (perm * 5) / 100 + perm;
        var overload = 0;
        //alert("tollerance" + tollerance);
        //alert("actual" + actual);
        if (actual - tollerance > 0) {
          overload = actual - (tollerance + lesspermit);
        }
        var conf = 0;
        //alert("overload" + overload);
        if (i == 0) {
          conf = this.getaxleconf("A");

          fee = this.getfee("steeringaxle", overload);
        }
        if (i == 1) {
          conf = this.getaxleconf("B");
          if (conf == 1) {
            fee = this.getfee("singledriveaxle", overload);
          }
          if (conf == 2) {
            fee = this.getfee("tandemaxle", overload);
          }
          if (conf == 3) {
            fee = this.getfee("tridemaxle", overload);
          }
        }
        if (i == 2) {
          conf = this.getaxleconf("C");
          if (conf == 1) {
            fee = this.getfee("singledriveaxle", overload);
          }
          if (conf == 2) {
            fee = this.getfee("tandemaxle", overload);
          }
          if (conf == 3) {
            fee = this.getfee("tridemaxle", overload);
          }
        }
        if (i == 3) {
          conf = this.getaxleconf("D");
          if (conf == 1) {
            fee = this.getfee("singledriveaxle", overload);
          }
          if (conf == 2) {
            fee = this.getfee("tandemaxle", overload);
          }
          if (conf == 3) {
            fee = this.getfee("tridemaxle", overload);
          }
        }
        var dat = {
          wbrg_ticket_no: this.WBTNO,
          axle_actualweight: orderData[i].actual,
          axle_permissibleweight: orderData[i].permissible,
          axle_time: this.weighdate,
          axle_number: axleno,
          axle_group: grp[i],
          axle_grouping: grp[i],
          Weighbridgetransactionsid: id,
          timestamp: this.getcurrentdate(),
          fee: fee,
        };
        axleno++;
        axios
          .post(window.$http + `axleweights/`, dat, {
            headers: window.$headers,
          })
          .then(() => {})
          .catch((e) => {
            Swal.fire("axleweights" + e);
            //this.errors.push(e);
          });
      }
    },
    insertaxleweights(id, orderData) {
      let axleno = 1;
      var grp = this.getaxlegrp();
      //console.log(grp);
      var fee = 0.0;
      var lesspermit = 0;
      if (this.validpermit) {
        if (Number(this.axlec.substring(0, 1)) == 2) {
          lesspermit = 1000;
        }
        if (
          Number(this.axlec.substring(0, 1)) == 3 ||
          Number(this.axlec.substring(0, 1)) == 4
        ) {
          lesspermit = 2000;
        }
      }
      for (var i = 0; i < orderData.length; i++) {
        var actual = Number(orderData[i].actual);
        var perm = Number(orderData[i].permissible);
        var tollerance = (perm * 5) / 100 + perm;
        var overload = 0;
        //alert("tollerance" + tollerance);
        //alert("actual" + actual);
        // if (actual - tollerance > 0) {
        //   overload = actual - tollerance;
        // }
        if (actual - tollerance > 0) {
          overload = actual - (tollerance + lesspermit);
        }
        var conf = 0;
        //alert("overload" + overload);
        if (i == 0) {
          conf = this.getaxleconf("A");

          fee = this.getfee("steeringaxle", overload);
        }
        if (i == 1) {
          conf = this.getaxleconf("B");
          if (conf == 1) {
            fee = this.getfee("singledriveaxle", overload);
          }
          if (conf == 2) {
            fee = this.getfee("tandemaxle", overload);
          }
          if (conf == 3) {
            fee = this.getfee("tridemaxle", overload);
          }
        }
        if (i == 2) {
          conf = this.getaxleconf("C");
          if (conf == 1) {
            fee = this.getfee("singledriveaxle", overload);
          }
          if (conf == 2) {
            fee = this.getfee("tandemaxle", overload);
          }
          if (conf == 3) {
            fee = this.getfee("tridemaxle", overload);
          }
        }
        if (i == 3) {
          conf = this.getaxleconf("D");
          if (conf == 1) {
            fee = this.getfee("singledriveaxle", overload);
          }
          if (conf == 2) {
            fee = this.getfee("tandemaxle", overload);
          }
          if (conf == 3) {
            fee = this.getfee("tridemaxle", overload);
          }
        }
        var dat = {
          wbrg_ticket_no: this.WBTNO,
          axle_actualweight: orderData[i].actual,
          axle_permissibleweight: orderData[i].permissible,
          axle_time: this.weighdate,
          axle_number: axleno,
          axle_group: grp[i],
          axle_grouping: grp[i],
          Weighbridgetransactionsid: id,
          timestamp: this.getcurrentdate(),
          fee: fee,
        };
        axleno++;
        axios
          .post(window.$http + `axleweights/`, dat, {
            headers: window.$headers,
          })
          .then(() => {})
          .catch((e) => {
            Swal.fire("axleweights" + e);
            //this.errors.push(e);
          });
      }
    },
    async getusername(email) {
      var username = "Kenload_System";
      try {
        return await axios
          .get(
            window.$http + `users/search?email=` + email,

            { headers: window.$headers }
          )
          .then((re) => {
            this.usernameautosp = re.data[0].fullnames.toUpperCase();
          })
          .catch((e) => {
            console.log(e);
            //alert(e);
            this.usernameautosp = username;
          });
      } catch (Exception) {
        this.usernameautosp = username;
      }
    },
    async getuser(email) {
      var username = "Kenload_System";
      try {
        return await axios
          .get(
            window.$http + `users/search?email=` + email,

            { headers: window.$headers }
          )
          .then((re) => {
            return re.data[0].fullnames.toUpperCase();
          })
          .catch((e) => {
            console.log(e);
            //alert(e);
            this.usernameautosp = username;
          });
      } catch (Exception) {
        return username;
      }
    },
    getsp() {
      var sp = false;

      if (
        this.nodata.wbrg_ticket_axleload > this.allgaw ||
        this.nodata.wbrg_ticket_gvwload > this.allgvw
      ) {
        sp = false;
      } else {
        var reason = "";
        var description = "";
        if (
          this.nodata.wbrg_ticket_axleload > 0 &&
          this.nodata.wbrg_ticket_excessload == "avw"
        ) {
          sp = true;
          reason = "Minimum Overload of Axle Limit";
          description =
            "Minimum axle overload of " +
            this.nodata.wbrg_ticket_axleload +
            " [KG] : ";
          sp = true;
        }

        if (
          this.nodata.wbrg_ticket_gvwload > 0 &&
          this.nodata.wbrg_ticket_excessload == "gvw"
        ) {
          reason = "Minimum Overload of Gross Limit";
          description =
            "Minimum gross overload of " +
            this.nodata.wbrg_ticket_gvwload +
            " [KG]  ";
          sp = true;
        }
        //alert(sp + reason + description);
        if (sp) {
          this.reason = reason;
          this.description = description;
          this.autospecialrelease = true;
          this.vehicleDirection = 1;

          this.vDirection = this.vehicleDirection;
          this.goToSrdoc(reason, description, this.userautosp);
          //alert(this.vDirection);
        }
      }
      return sp;
    },
    getv(val) {
      if (val < 10) {
        val = "0" + val;
      }
      return val;
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
    getcurrentdateonly(mdate) {
      let d = new Date(mdate);
      let year = d.getFullYear();
      let month = d.getMonth() + 1;
      let date = d.getDate();
      date = this.getv(date);
      month = this.getv(month);

      //const msec = d.getMilliseconds();
      const datetime = date + "/" + month + "/" + year;
      return datetime;
    },
    getsetdate(mdate) {
      let d = new Date(mdate);
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
    getaxle() {
      this.axlegroup = ["", "", "", "", "", "", "", "", "", "", "", "", "", ""];
      this.axleimg = ["", "", "", "", "", "", "", "", "", "", "", "", "", ""];
      let index = 0;
      this.axle_deckgrouping = [];
      this.axle_typeoftyres = [];
      this.permissible = [];
      this.axlepermissible = [];

      let totA = 0;
      let totB = 0;
      let totC = 0;
      let totD = 0;
      //alert(this.AxleWeightXreffs.length);
      for (let i = 0; i < this.AxleWeightXreffs.length; i++) {
        if (this.AxleWeightXreffs[i].axle_code == this.axlec) {
          //alert(this.AxleWeightXreffs[i].axle_deckgrouping);
          if (this.AxleWeightXreffs[i].axle_deckgrouping == "B" && index < 2) {
            index = 2;
          }
          if (this.AxleWeightXreffs[i].axle_deckgrouping == "C" && index < 6) {
            index = 6;
          }
          if (this.AxleWeightXreffs[i].axle_deckgrouping == "D" && index < 10) {
            index = 10;
          }
          this.axle_deckgrouping.push(
            this.AxleWeightXreffs[i].axle_deckgrouping
          );
          if (this.AxleWeightXreffs[i].axle_deckgrouping == "A") {
            totA = totA + Number(this.AxleWeightXreffs[i].axle_legalweight);
          }
          if (this.AxleWeightXreffs[i].axle_deckgrouping == "B") {
            totB = totB + Number(this.AxleWeightXreffs[i].axle_legalweight);
          }
          if (this.AxleWeightXreffs[i].axle_deckgrouping == "C") {
            totC = totC + Number(this.AxleWeightXreffs[i].axle_legalweight);
          }
          if (this.AxleWeightXreffs[i].axle_deckgrouping == "D") {
            totD = totD + Number(this.AxleWeightXreffs[i].axle_legalweight);
          }

          this.axle_typeoftyres.push(this.AxleWeightXreffs[i].axle_typeoftyres);

          this.axlepermissible.push(
            Number(this.AxleWeightXreffs[i].axle_legalweight)
          );

          this.axlegroup[index] = this.AxleWeightXreffs[i].axle_typeoftyres;
          if (this.axlegroup[index] == "S") {
            this.axleimg[index] = require("@/assets/images/weigh/single.png");
          }

          if (this.axlegroup[index] == "D") {
            this.axleimg[index] = require("@/assets/images/weigh/double.png");
          }

          if (this.axlegroup[index] == "W") {
            this.axleimg[index] = require("@/assets/images/weigh/wide.png");
          }
          //
          //this.permissible

          index++;
          // this.changestate(this.axlegroup[g], g);
          //g++;
        }
        //this.countaxles();
      }
      if (totA > 0) {
        this.permissible.push(Number(totA));
      }
      if (totB > 0) {
        this.permissible.push(Number(totB));
      }
      if (totC > 0) {
        this.permissible.push(Number(totC));
      }
      if (totD > 0) {
        this.permissible.push(Number(totD));
      }
    },
    countaxles() {
      var axle = 0;
      this.axlepermissible = [];

      var conf = "";
      for (let i = 0; i < this.axlegroup.length; i++) {
        if (i == 2 || i == 6 || i == 10) {
          conf += "|";
        }

        if (this.axlegroup[i] != "") {
          axle++;
          conf += this.axlegroup[i];
        }
      }
      this.axlec = axle + "*" + conf;
      if (axle == 0) {
        this.axlec = "";
      }
      var i2 = 0;
      //alert(this.permissible[0]);
      this.permissible[0] = 0;
      if (this.axlegroup[0] != "" || this.axlegroup[1] != "") {
        this.permissible[0] = 8000;
        this.axlepermissible[i2] = 8000;
      }
      if (this.axlegroup[0] != "" && this.axlegroup[1] != "") {
        this.permissible[0] = 12000;
        this.axlepermissible[i2] = 6000;
        i2++;
        this.axlepermissible[i2] = 6000;
      }
      var tot = 0;
      var tot2 = 0;
      var i = 1;

      //Group B;
      this.permissible[i] = 0;
      tot = Number(this.getPermissible(2, 3, 4, 5));
      //alert(tot + " - " + i2);
      if (tot > 0) {
        i2++;
        this.axlepermissible[i2] = tot;
        tot2 += tot;
      }

      tot = Number(this.getPermissible(3, 2, 4, 5));
      //alert(tot + " - " + i2);
      if (tot > 0) {
        i2++;
        this.axlepermissible[i2] = tot;
        tot2 += tot;
      }
      tot = Number(this.getPermissible(4, 3, 2, 5));
      //alert(tot + " - " + i2);
      if (tot > 0) {
        i2++;
        this.axlepermissible[i2] = tot;
        tot2 += tot;
      }
      tot = Number(this.getPermissible(5, 3, 4, 2));
      //alert(tot + " - " + i2);
      if (tot > 0) {
        i2++;
        this.axlepermissible[i2] = tot;
        tot2 += tot;
      }

      this.permissible[i] = tot2;

      tot2 = 0;
      tot = 0;
      i = 2;
      //Group C;
      this.permissible[i] = 0;

      tot = Number(this.getPermissible(6, 7, 8, 9));
      //alert(tot + " - " + i2);
      if (tot > 0) {
        i2++;
        this.axlepermissible[i2] = tot;
        tot2 += tot;
      }

      tot = Number(this.getPermissible(7, 6, 8, 9));
      //alert(tot + " - " + i2);
      if (tot > 0) {
        i2++;
        this.axlepermissible[i2] = tot;
        tot2 += tot;
      }
      tot = Number(this.getPermissible(8, 7, 6, 9));
      //alert(tot + " - " + i2);
      if (tot > 0) {
        i2++;
        this.axlepermissible[i2] = tot;
        tot2 += tot;
      }
      tot = Number(this.getPermissible(9, 7, 8, 6));
      //alert(tot + " - " + i2);
      if (tot > 0) {
        i2++;
        this.axlepermissible[i2] = tot;
        tot2 += tot;
      }

      this.permissible[i] = tot2;

      tot2 = 0;
      tot = 0;
      i = 3;
      //Group D;
      this.permissible[i] = 0;
      tot = Number(this.getPermissible(10, 11, 12, 13));
      //alert(tot + " - " + i2);
      if (tot > 0) {
        i2++;
        this.axlepermissible[i2] = tot;
        tot2 += tot;
      }

      tot = Number(this.getPermissible(11, 10, 12, 13));
      //alert(tot + " - " + i2);
      if (tot > 0) {
        i2++;
        this.axlepermissible[i2] = tot;
        tot2 += tot;
      }
      tot = Number(this.getPermissible(12, 11, 10, 13));
      //alert(tot + " - " + i2);
      if (tot > 0) {
        i2++;
        this.axlepermissible[i2] = tot;
        tot2 += tot;
      }
      tot = Number(this.getPermissible(13, 11, 12, 10));
      //alert(tot + " - " + i2);
      if (tot > 0) {
        i2++;
        this.axlepermissible[i2] = tot;
        tot2 += tot;
      }

      this.permissible[i] = tot2;
    },
    getPermissible(N, v1, v2, v3) {
      var tot = 0;
      if (
        this.axlegroup[N] == "S" &&
        (this.axlegroup[v1] != "" ||
          this.axlegroup[v2] != "" ||
          this.axlegroup[v3] != "")
      ) {
        tot = 6750;
      }
      if (
        this.axlegroup[N] == "S" &&
        this.axlegroup[v1] == "" &&
        this.axlegroup[v2] == "" &&
        this.axlegroup[v3] == ""
      ) {
        tot = 7000;
      }

      if (
        this.axlegroup[N] == "W" &&
        (this.axlegroup[v1] != "" ||
          this.axlegroup[v2] != "" ||
          this.axlegroup[v3] != "")
      ) {
        tot = 7500;
      }

      if (
        this.axlegroup[N] == "W" &&
        this.axlegroup[v1] == "" &&
        this.axlegroup[v2] == "" &&
        this.axlegroup[v3] == ""
      ) {
        tot = 8000;
      }

      if (
        this.axlegroup[N] == "D" &&
        this.axlegroup[v1] == "" &&
        this.axlegroup[v2] == "" &&
        this.axlegroup[v3] == ""
      ) {
        tot = 10000;
      }

      if (
        this.axlegroup[N] == "D" &&
        (this.axlegroup[v1] != "" ||
          this.axlegroup[v2] != "" ||
          this.axlegroup[v3] != "")
      ) {
        var weight = 0;
        for (let e = 0; e < 3; e++) {
          if (this.axlegroup[v1] != "") {
            weight++;
          }
          if (this.axlegroup[v2] != "") {
            weight++;
          }
          if (this.axlegroup[v3] != "") {
            weight++;
          }
        }
        if (weight > 3) {
          tot = 8000;
        } else {
          tot = 9000;
        }
      }

      return tot;
    },

    changestate(state, index) {
      if (this.weighttaken) {
        return;
      }
      var group = "";
      if (index >= 10) {
        group = "D";
      }
      if (index <= 1) {
        group = "A";
      }
      if (index >= 2 && index <= 5) {
        group = "B";
      }

      if (index >= 6 && index <= 9) {
        group = "C";
      }
      this.axlegrouping[index] = group;
      if (state == "W") {
        this.axlegrouping[index] = "";
      }
      //alert(group +" - " +index +this.axlegrouping[index] " +this.axlegrouping[index]);

      //this.groupaxles();

      if (state == "") {
        state = "S";
        this.axlegroup[index] = state;
        this.axleimg[index] = require("@/assets/images/weigh/single.png");
        this.countaxles();
        return;
      }
      if (state == "S") {
        state = "D";
        this.axlegroup[index] = state;
        this.axleimg[index] = require("@/assets/images/weigh/double.png");
        this.countaxles();
        return;
      }
      if (state == "D") {
        state = "W";
        this.axlegroup[index] = state;
        this.axleimg[index] = require("@/assets/images/weigh/wide.png");
        this.countaxles();
        return;
      }
      if (state == "W") {
        state = "";
        this.axlegroup[index] = state;
        this.axleimg[index] = "";
        this.countaxles();
        return;
      }
    },
    resetlswim() {
      Swal.fire({
        title:
          "Are you sure, you want to Restart Weighing for " + this.nplate + "?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, Restart Weighing",
      }).then((result) => {
        if (result.value) {
          Swal.fire(
            "Restarting... !",
            this.nplate + " reset of weight values !.",
            "success"
          );
          //clearInterval(lswimint);
          // this.startlswim();
          this.restartlswimservice();

          return;
        }
      });
    },
    restartlswimservice() {
      this.weightindex = 0;
      this.finish = false;
      this.overspeed = false;
      this.resetaxlesval();
    },
    resetaxlesval() {
      this.axleweight = [
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
      ];
      this.axletime = [
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
      ];
    },
    startlswim() {
      if (this.cat != "lswim") {
        return;
      }
      // if (this.axlec.trim() == "") {
      //   Swal.fire("Please select Axle Type!");
      //   this.$refs.axlec.focus();
      //   return;
      // }

      //this.getaxlegrp();

      // if (this.lstatus == "Stop") {
      //   Swal.fire({
      //     title:
      //       "Are you sure, you want to Stop LSWIM for " + this.nplate + "?",
      //     text: "You won't be able to revert this!",
      //     icon: "warning",
      //     showCancelButton: true,
      //     confirmButtonColor: "#000000",
      //     cancelButtonColor: "#f46a6a",
      //     confirmButtonText: "Yes, Stop LSWIM!",
      //   }).then((result) => {
      //     if (result.value) {
      //       Swal.fire(
      //         "Stop LSWIM !",
      //         this.nplate + " LSWIM Stoped !.",
      //         "success"
      //       );
      //       this.lstatus = "Start";
      //       this.lswimtimer = 1000000000;
      //       return;
      //     }
      //   });
      //   return;
      // }
      var cweight = 0;
      try {
        cweight = this.$refs.Axleweights.deck1;
      } catch (e) {
        this.error = e;
      }

      if (this.lstatus == "Start" && this.$refs.Axleweights.deck1 > 50) {
        Swal.fire(
          "You can't weigh load on deck of !" + this.$refs.Axleweights.deck1
        );
        //this.$refs.axlec.focus();
        return;
      }

      this.lswimtimer = 10;
      this.lstatus = "Stop";

      this.lsweight = 0;

      this.resetaxlesval();

      let getLSWIM = setInterval(() => {
        if (this.staticweighlswim) {
          clearInterval(getLSWIM);
        }
        try {
          cweight = this.$refs.Axleweights.deck1;
        } catch (e) {
          this.error = e;
        }
        if (cweight > this.lsweight) {
          this.lsweight = cweight;
        }

        if (cweight <= 50 && this.lsweight > 50) {
          //this.weights[index] = Number(this.lsweight);
          //alert(this.lsweight);
          var seconds = 0;
          this.axleweight[this.weightindex] = this.lsweight;
          this.axletime[this.weightindex] = new Date();

          if (this.weightindex > 0) {
            seconds = parseInt(
              (this.axletime[this.weightindex] -
                this.axletime[this.weightindex - 1]) /
                1000
            );
            var axl = 0;
            var dis = 1;

            try {
              axl = Number(this.axlec.substring(0, 1));
            } catch (e) {
              this.error = e;
            }

            if (axl == 2) {
              dis = 4;
            }
            if (seconds == 0) {
              this.speed = 0;
            } else {
              var fspeed = (dis / seconds) * 3.6; //m/s   s=d/t
              this.speed = new Intl.NumberFormat().format(fspeed);
            }
            if (this.speed > 5) {
              this.overspeed = true;
              if (this.overspeed) {
                Swal.fire({
                  title: "Overspeed for " + this.nplate + " ",
                  text: "" + this.speed + " km/h",
                  icon: "warning",
                  showCancelButton: true,
                  confirmButtonColor: "#000000",
                  cancelButtonColor: "#f46a6a",
                  confirmButtonText: "Yes, Restart Weighing",
                }).then((result) => {
                  if (result.value) {
                    Swal.fire(
                      "Restarting !",
                      this.nplate + " reset of weight values !.",
                      "success"
                    );
                    this.restartlswimservice();
                    //return;
                  }
                });
                //return;
              }
            }
          }
          this.genimganpr(this.weightindex + "");
          this.lsweight = 0;
          this.weightindex++;
        }
        var W1 = 0;
        var W2 = 0;
        var W3 = 0;
        var W4 = 0;
        var grp = 0;
        //alert(this.axlegrouping.length);
        for (let i = 0; i < this.axlegrouping.length; i++) {
          //alert("this.axlegrouping[i]" + i);
          if (this.axlegrouping[i] == "A") {
            W1 += Number(this.axleweight[grp]);
            grp++;
          }
          if (this.axlegrouping[i] == "B") {
            W2 += Number(this.axleweight[grp]);
            grp++;
          }
          if (this.axlegrouping[i] == "C") {
            W3 += Number(this.axleweight[grp]);
            grp++;
          }
          if (this.axlegrouping[i] == "D") {
            W4 += Number(this.axleweight[grp]);
            grp++;
          }
        }
        this.weights[0] = W1;
        this.weights[1] = W2;
        this.weights[2] = W3;
        this.weights[3] = W4;
        //this.getorders(this.weights);
        if (this.weightindex == this.mylength) {
          this.getaxlevals();
          // var cstate = "";
          // if (this.status == "Overload") {
          //   cstate = "O";
          // }
          // if (this.status == "Error") {
          //   cstate = "E";
          // }
          // if (this.status == "Warning") {
          //   cstate = "W";
          // }
          // if (this.status == "Legal") {
          //   cstate = "L";
          // }
          this.weighdate = this.getcurrentdate();
          // this.saveweights(cstate);

          this.weightindex = 0;
          this.lswimtimer = 1000000000;
          //this.weighttaken = true;
          this.finish = true;

          //this.isVisible = false;
          //this.lstatus = "Completed";
          //this.lswimtimer = 1000000000;

          // Swal.fire({
          //   title: "Weight taken",
          //   text: "!",
          //   icon: "success",
          //   confirmButtonColor: "#000000",
          //   confirmButtonText: "OK!",
          // });
        }
      }, this.lswimtimer);
    },
    getorders(weights) {
      try {
        var mylength = 0;
        this.totalweight = 0;
        this.gvwperm = 0;

        var axleoverload = "";
        this.vehicleposition = "";
        this.gvwlabel = "";

        //var this.axlec.substr(0, 1);

        for (let i = 0; i < this.permissible.length; i++) {
          if (this.permissible[i] > 0) {
            mylength++;
            this.orderData.push(Object.values(this.orderData2[i]));
          }
        }

        var id = 0;

        for (let i = 0; i < mylength; i++) {
          id++;
          this.orderData[i].group = id;
          if (this.permissible[i] == null) {
            this.permissible[i] = 0;
          }
          this.orderData[i].permissible = this.permissible[i];
          var tole = (this.permissible[i] * 5) / 100;
          this.orderData[i].tolerance = tole + this.permissible[i];
          var load = weights[i] - this.orderData[i].tolerance;
          var loadbal = weights[i] - this.permissible[i];
          this.status = "Legal";
          if (weights[i] >= 100) {
            this.orderData[i].result = "Legal";
            if (load <= 0) {
              load = 0;
            } else {
              this.orderData[i].result = "Overload";
              axleoverload = "Overload";
              this.status = "Overload";
            }
            if (tole > loadbal && loadbal > 0) {
              this.orderData[i].result = "Warning";
            }
          } else {
            this.orderData[i].result = "Error";
            this.vehicleposition = "Not Positioned Correctly";
          }
          var wcount = 0;
          var pcount = 0;

          if (this.cat == "multideck") {
            for (let i = 0; i < this.weights.length; i++) {
              if (Number(this.weights[i]) > 100) {
                wcount++;
                //alert(Number(this.weights[i]) + " Deck: " + wcount);
              }
            }

            for (let i = 0; i < this.permissible.length; i++) {
              if (Number(this.permissible[i]) > 100) {
                pcount++;
                //alert(Number(this.permissible[i]) + " permissible: " + pcount);
              }
            }

            if (wcount != pcount) {
              this.status = "Error";
            }
          }

          this.orderData[i].actual = weights[i];
          this.orderData[i].overload = load;
          this.orderData[i].pdf = 0.0;
          this.totalweight += Number(weights[i]);
          this.gvwperm += Number(this.permissible[i]);
        }
        //alert(mylength);
        var gvwover = this.totalweight - this.gvwperm;
        this.gvwoverload = gvwover;

        if (this.gvwoverload < 0) {
          this.gvwoverload = 0;
          this.gvwlabel = "Legal";
        }

        this.orderData.splice(mylength, this.orderData.length - mylength);
        for (var l = 0; l < this.orderData.length; l++) {
          if (this.orderData[l].result == "Warning") {
            this.status = "Warning";
          }
        }

        if (this.gvwlabel != "Overload") {
          this.vehicleDirection = 1;
        }

        if (axleoverload == "Overload") {
          this.vehicleDirection = 0;
          this.status = "Overload";
        }
        if (this.tagged) {
          this.vehicleDirection = 0;
        }
        if (this.gvwoverload > 0) {
          this.gvwlabel = "Overload";
          this.status = "Overload";
        }
        if (this.status == "Overload") {
          this.vehicleDirection = 0;
        }
        try {
          if (this.spreleaseno.trim() != "") {
            //alert(this.spreleaseno);
            this.vehicleDirection = 1;
            this.vDirection = this.vehicleDirection;
          }
        } catch (e) {
          this.error = e;
        }

        //alert(this.spreleaseno);
        if (this.cat == "multideck") {
          for (let i = 0; i < this.weights.length; i++) {
            if (Number(this.weights[i]) > 100) {
              wcount++;
              //alert(Number(this.weights[i]) + " Deck: " + wcount);
            }
          }

          for (let i = 0; i < this.permissible.length; i++) {
            if (Number(this.permissible[i]) > 100) {
              pcount++;
              //alert(Number(this.permissible[i]) + " permissible: " + pcount);
            }
          }

          if (wcount != pcount) {
            this.status = "Error";
          }
        }
      } catch (e) {
        //alert(e);
      }
    },
    getdaysdiff(date1, date2) {
      //Swal.fire("Date:" + date1 + "\ndate2" + date2);
      // The number of milliseconds in one day
      const ONE_DAY = 1000 * 60 * 60 * 24;

      // Calculate the difference in milliseconds
      const differenceMs = date2 - date1;

      // Convert back to days and return
      return Math.round(differenceMs / ONE_DAY);
    },
    getmindiff(date1, date2) {
      //Swal.fire("Date:" + date1 + "\ndate2" + date2);
      // The number of milliseconds in one day
      const ONE_DAY = 1000 * 60;

      // Calculate the difference in milliseconds
      const differenceMs = date2 - date1;
      //alert(differenceMs / ONE_DAY);

      var hours = differenceMs / ONE_DAY / 60;
      var rhours = Math.floor(hours);
      var minutes = (hours - rhours) * 60;
      var rminutes = Math.round(minutes);
      return rhours + " hour(s) and " + rminutes + " minute(s).";

      // Convert back to days and return
      //return Math.round(differenceMs / ONE_DAY);
    },
    getpermit() {
      //alert();
      //alert(
      //   "https://permits.kenha.co.ke:7080/ApplicantForms/GeneratePermit/App-0009617"
      // );

      // const logFileText = async (file) => {
      //   const response = await fetch(file);
      //   this.info = await response.text();
      // };
      // var theUrl =
      //   "http://localhost:8080/ApplicantForms/GeneratePermit/App-0009617";

      // logFileText(theUrl);
      // ref = "https://permits.kenha.co.ke:7080";

      if (this.permit.trim() == "") {
        return;
      }

      this.info = "";

      // var href = window.location.href;

      // var arr = href.split("/");
      // var ref = "";
      // var ref2 = arr[2].split(":");
      // ref = arr[0] + "//" + ref2[0] + "";
      //ref = "http://localhost:82";
      //alert(ref);

      //var url = window.$url + "/ApplicantForms/GeneratePermit/" + this.permit.trim();
      var url =
        window.$permiturl +
        "/permits/get-permit?option=1&id=" +
        this.permit.trim() +
        "&uid=kenload&pwd=test";
      // var url = window.$permiturl + this.permit.trim();
      //alert(url);

      //url = "https://kenloadv2.netlify.app";
      //url = "http://localhost:8080";

      // const logFileText = async (file) => {
      //   const response = await fetch(file);
      //   let text = await response.text();
      //   this.info = text;
      // };
      // //var file = "file:///C:/xampp/htdocs/indicators/" + this.weighb + ".txt";
      // logFileText(url);

      //alert(url);

      Swal.fire({
        title: "Please Wait !",
        html: "Scanning permit number " + this.permit.trim(), // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: true,
        willOpen: () => {
          Swal.showLoading();
        },
      });

      let error = "";

      var url2 =
        window.$permiturl +
        "/permits/get-items?id=" +
        this.permit.trim() +
        "&uid=kenload&pwd=test";
      //alert(url2);
      axios
        .get(url2) //, { timeout: 200000 }
        .then((response) => {
          console.log(response);
          this.width_ = response.data;
          var strl = JSON.parse(JSON.stringify(response.data));
          console.log("strl");
          console.log(strl);
          //alert(strl.data[0].Weight.trim());
          //Width:
          try {
            this.width_ = strl.data[0].Width.trim();
          } catch (Exception) {
            this.width_ = "";
          }

          //Height:
          try {
            this.height_ = strl.data[0].Height.trim();
          } catch (Exception) {
            this.height_ = "";
          }

          //Length:
          try {
            this.length_ = strl.data[0].Length.trim();
          } catch (Exception) {
            this.length_ = "";
          }

          //product:
          try {
            this.cargo = strl.data[0].LoadDescription.trim();
            //alert(this.cargo);

            this.vehicles.vehicle_product = strl.data[0].LoadDescription.trim();
          } catch (Exception) {
            this.cargo = "";
          }

          //authweight:

          try {
            this.authweight = Number(strl.data[0].Weight.trim());
          } catch (Exception) {
            this.authweight = 0;
          }
        })
        .catch((err) => {
          //alert(err);
          if (err.response.status === 404) {
            Swal.fire(
              "Permit Scan Error 400!  \nError Connecting to server :" +
                this.url +
                " \n" +
                err
            );
            //throw new Error(`${err.config.url} not found`);
          }
          if (err.response.status === 500) {
            Swal.fire(
              "Permit Scan Timeout Error 500! \n Connecting to server :" +
                this.url +
                "\n" +
                err
            );
            //throw new Error(`${err.config.url} not found`);
          }
          //throw err;
        });

      // url =
      //   window.$url +
      //   "/kenload/dashboard/getpermit.php?string=" +
      //   window.$permiturl +
      //   this.permit.trim();
      //alert(url);
      //this.comment = url;
      //baseURL: url, timeout: 10
      try {
        axios
          .get(url) //, { timeout: 200000 }
          .then((response) => {
            //axios.get({ baseURL: url, timeout: 1000 }).then((response) => {
            this.info = response.data;

            var strl = JSON.parse(JSON.stringify(response.data));

            //Permit
            try {
              this.serialno = strl.data.PermitNo.trim();
            } catch (Exception) {
              this.serialno = "";
            }

            if (
              this.serialno.toUpperCase() != this.permit.trim().toUpperCase()
            ) {
              Swal.fire("Invalid Permit Number " + this.serialno);
              return;
            }
            //Trailer Registration

            try {
              this.trailer =
                strl.data.TrailerRegistrationNumber.trim().toUpperCase();
            } catch (Exception) {
              this.traillerreg = "";
            }

            //Transpoter:
            try {
              this.transporter = strl.data.TransporterName.trim();
            } catch (Exception) {
              this.transporter = "-";
            }
            //TYPE OF VEHICLE:
            try {
              this.make = strl.data.MakeName.trim().toUpperCase();
            } catch (Exception) {
              this.model = "";
            }

            //END OF JOURNEY DATE
            var dtrs = "";
            try {
              dtrs = strl.data.ToDate.split(" ");
            } catch (Exception) {
              dtrs = new Date();
            }

            ddatesp = dtrs[0];
            //alert(this.pEndD);

            this.pEndD = new Date(ddatesp);
            var pEndD = this.pEndD;

            pEndD.setDate(pEndD.getDate() + 1);
            var daydidff = this.getdaysdiff(new Date(), pEndD);

            if (daydidff < 0) {
              Swal.fire("Permit Expired on " + this.pEndD);
              return;
            }

            //vehicle Registration
            var regf = strl.data.VehicleRegistrationNumber.trim();

            const indexk = [];
            indexk[0] = "-K";
            indexk[1] = "- K";
            indexk[2] = " K";

            if (regf.indexOf("-") > 0) {
              regf = regf.substring(0, regf.indexOf("-"));
              //alert("regf" + regf);
            }

            regf = regf
              .toUpperCase()
              .replaceAll("\\s", "")
              .replaceAll("[^a-zA-Z0-9]", "")
              .replaceAll(" ", "");

            this.registration = regf;

            if (this.registration != this.nplate) {
              Swal.fire({
                position: "center",
                icon: "error",
                title:
                  "Permit not Valid. This Permit is for " + this.registration,
                showConfirmButton: true,
              });
              return;
            }
            //Tare Weight

            try {
              this.tareweight = strl.data.TotalTareWeightofvehiclekgs.trim();
            } catch (Exception) {
              this.tareweight = 0.1;
            }

            //Start of Journey
            try {
              dtrs = strl.data.FromDate.split(" ");
            } catch (Exception) {
              dtrs = new Date();
            }

            var ddatesp = dtrs[0];
            //alert(this.pEndD);
            this.dpermitissu = new Date(ddatesp);

            this.pStartD = this.dpermitissu;

            //TOTAL NUMBER OF AXELS:
            var totaltyres = "0";
            try {
              totaltyres = strl.data.Noofwheelsperaxle.trim();
            } catch (Exception) {
              totaltyres = "0";
            }
            //alert(totaltyres);

            var wheelaxle = totaltyres.split(":");
            this.axlecount = wheelaxle.length;

            let axl = 0;
            for (var k = 0; wheelaxle.length > k; k++) {
              axl += Number(wheelaxle[k]);
            }
            this.totaltyres = axl;
            //Phone:
            try {
              this.transptel = strl.data.TelephoneNumber.trim();
            } catch (Exception) {
              this.transptel = "-";
            }

            //Email:
            try {
              this.email = strl.data.EmailAddress.trim();
            } catch (Exception) {
              this.email = "";
            }

            //Address

            this.transpadd = "-";
            var maxweight = 0;
            try {
              maxweight = Number(this.authweight) + Number(this.tareweight);
            } catch (Exception) {
              maxweight = 0;
            }
            this.validpermit = true;
            this.hasavalidpermit = "Valid Permit! " + maxweight + "[KG]";
            //this.permit = this.vehicles.vehicle_permit_no;
            // this.authweight = this.vehicles.vehicle_maxweight;
            //this.validpermit = true;

            //alert(maxweight);
            Swal.close();
            this.driver = "-";
            this.vehicles.vehicle_permit_no = this.permit.trim();
            this.vehicles.vehicle_tx_code = this.transporter;
            this.vehicles.vehicle_make = this.make;
            this.vehicles.vehicle_maxweight = maxweight + "";
            this.vehicles.vehicle_permitexpiration_date = this.getsetdate(
              this.pEndD.setDate(this.pEndD.getDate() - 1)
            );
            try {
              this.vehiclesupt();
            } catch (e) {
              error.message = e;
            }

            //alert("product" + this.product);
            //alert("totaltyres" + this.product);
          })
          .catch((err) => {
            //alert(err);
            // if (err.response.status === 404) {
            //   Swal.fire("Permit Scan Error! Page not found\n" + err);
            //   //throw new Error(`${err.config.url} not found`);
            // }
            // if (err.response.status === 500) {
            //   Swal.fire("Timeout Error\n" + err);
            //   //throw new Error(`${err.config.url} not found`);
            // }
            //throw err;
            error.message = err;
            Swal.close();
          });
      } catch (e) {
        error.message = e;
        Swal.fire("Permit Scan Error! Page not found\n" + e);
        //Swal.close();
      }
    },
    getGetAVWoverloadCharges() {
      axios
        .get(window.$http + `AVWoverloadCharges?limit=10000`, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.AVWoverloadCharges = response.data;

          //Swal.close();
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "AVWoverloadCharges" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
    },
    getwbtid(WBTNO) {
      axios
        .get(window.$http + `weighbridgetransactions` + WBTNO, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          response.data.forEach((e) => {
            return e.id;
          });

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
    },
    getvehiclemake() {
      this.makes = [];
      axios
        .get(window.$http + `VehicleMake`, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          response.data.forEach((e) => {
            this.makes.push(e.vehiclemake.toUpperCase());
          });

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
    },
    getcargo() {
      this.cargos = [];
      axios
        .get(window.$http + `Cargos`, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          // this.cargos = response.data.cargo_name;
          response.data.forEach((e) => {
            this.cargos.push(e.cargo_name.toUpperCase());
          });

          //Swal.close();
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "CARGOS " + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
    },
    gettransporters() {
      this.transporters = [];
      axios
        .get(window.$http + `Transporters/search?limit=100000`, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          // this.cargos = response.data.cargo_name;
          response.data.forEach((e) => {
            this.transporters.push(e.transporter_name.toUpperCase());
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
    getaxles() {
      this.axles = [];
      this.AxleWeightXreffs = [];
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

      // axles
      axios
        .get(window.$http + `AxleWeightXreffs`, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.AxleWeightXreffs = response.data;
          this.getaxle();
          Swal.close();
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Origins" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
    },
    getorigin() {
      this.origins = [];
      //alert(window.$http + `Origins/search?limit=100000`);
      axios
        .get(window.$http + `Origins/search?limit=100000`, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          // this.cargos = response.data.cargo_name;
          response.data.forEach((e) => {
            this.origins.push(e.origin_name.toUpperCase());
          });

          Swal.close();
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Origins" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
    },
    getyardlist() {
      //screen and roles combination
      //alert(window.$http + `yardlist/search?status=I`);

      axios
        .get(window.$http + `yardlist/search?status=I`, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          // this.vehinyard = response.data;
          response.data.forEach((e) => {
            this.vehinyard.push(e.vehreg.toUpperCase());
          });
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
    loadvalues() {
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      //load vehicle make
      this.getvehiclemake();

      // load cargo
      this.getcargo();

      // Transporter
      this.gettransporters();

      // Axle Config
      this.getaxles();

      this.getorigin();
      this.getallowableexcess();
    },
    getallowableexcess() {
      axios
        .get(window.$http + `tollerance`, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          // if (this.reweigh == 0) {
          this.allgvw = response.data[0].gvw;
          this.allsaw = response.data[0].singleaxle;
          this.allgaw = response.data[0].groupaxle;
          this.userautosp = response.data[0].username;
          this.getusername(this.userautosp);
          //}
          Swal.close();
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Allowable " + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
    },
    pulatefields() {
      //alert(this.reweigh);
      if (this.reweigh > 0) {
        this.driver = this.vehicles.vehicle_driver;
        this.cargo = this.vehicles.vehicle_product;
        this.origin = this.vehicles.vehicle_source;
        this.destination = this.vehicles.vehicle_destination;
      }
      this.axlec = this.vehicles.vehicle_axle;

      this.transporter = this.vehicles.vehicle_tx_code;
      this.make = this.vehicles.vehicle_make;
      this.trailer = this.vehicles.vehicle_trailer;
      //alert(this.vehicles.vehicle_permitexpiration_date);
      //var ddatesp = response.data.permitexpiration_date.split("T").replaceAll("-","/");
      //alert(this.reweigh);
      if (this.vehicles.vehicle_permit_no != "") {
        // var ddatesp = this.vehicles.vehicle_permitexpiration_date
        //   .substring(0, 10)
        //   .trim()
        //   .split("-");
        var ddatesp = new Date(this.vehicles.vehicle_permitexpiration_date);
        //alert("ddatesp" + ddatesp);

        this.pEndD = new Date();
        this.pEndD.setDate(this.pEndD.getDate() + 1);
        //this.pEndD = this.getcurrentdateonly(this.pEndD);
        var daydidff = this.getdaysdiff(
          new Date(ddatesp),
          new Date(this.pEndD)
        );
        //alert("pEndD" + this.pEndD);
        //alert("new Date()" + new Date());
        //alert("daydidff" + daydidff);
        //alert(this.pEndD);
        //alert("" + daydidff);

        if (Number(daydidff) > 0) {
          //Swal.fire("Permit Expired on " + this.pEndD);
          //return;
          this.validpermit = false;
        } else {
          if (Number(this.vehicles.vehicle_maxweight) > 60000) {
            this.getpermitno(this.nplate);
            return;
          }
          this.permit = this.vehicles.vehicle_permit_no;
          this.authweight = this.vehicles.vehicle_maxweight;
          Swal.fire("Valid Permit " + this.authweight);
          //alert();

          this.hasavalidpermit = "Valid Permit! " + this.authweight + "[KG]";
          //alert(
          //   "this.vehicles.vehicle_maxweight: " +
          //     this.vehicles.vehicle_maxweight
          // );
          //alert("this.authweight" + this.vehicles.vehicle_maxweight);
          this.validpermit = true;
        }
      }
    },
    getpermitno(reg) {
      //alert(reg);
      var url =
        window.$permiturl +
        "/permits/get-permit?id=" +
        reg +
        "&uid=kenload&pwd=test&option=0";
      try {
        axios
          .get(url) //, { timeout: 200000 }
          .then((response) => {
            //axios.get({ baseURL: url, timeout: 1000 }).then((response) => {
            this.info = response.data;

            var strl = JSON.parse(JSON.stringify(response.data));

            //Permit
            try {
              var totaltyres = "0";
              try {
                totaltyres = strl.data.Noofwheelsperaxle.trim();
              } catch (Exception) {
                totaltyres = "0";
              }
              //alert(totaltyres);

              var wheelaxle = totaltyres.split(":");
              if (wheelaxle.length <= 4) {
                this.permit = strl.data.PermitNo.trim();
                this.getpermit();
              }
            } catch (Exception) {
              this.serialno = "";
            }
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
        //error.message = e;
        Swal.fire("Permit Scan Error! Page not found\n" + e);
        //Swal.close();
      }
    },
    updatevehicles() {
      var yea = this.getcurrentdate();
      yea = "" + yea.substring(0, 4);
      this.vehicles.vehicle_driver = this.driver;
      this.vehicles.vehicle_source = this.origin;
      this.vehicles.vehicle_destination = this.destination;

      this.vehicles.vehicle_axle = this.axlec;

      this.vehicles.vehicle_tx_code = this.transporter;
      this.vehicles.vehicle_make = this.make;
      this.vehicles.vehicle_trailer = this.trailer;
      this.vehicles.vehicle_height = this.height_;
      this.vehicles.vehicle_length = this.length_;
      this.vehicles.vehicle_width = this.width_;
      this.vehicles.vehicle_year = yea;
      this.vehicles.vehicle_model = this.make;
      this.vehicles.vehicle_ownercontact = "0722";
      this.vehicles.vehicle_owneremail = "info@micna.co.ke";
      this.vehicles.vehicle_regdate = this.getcurrentdate();
      this.vehicles.vehicle_desc = this.cargo;
      this.vehicles.vehicle_product = this.cargo;
      //alert(
      //   this.vehicles.vehicle_maxweight + "this.vehicles.vehicle_maxweight"
      // );
      //alert("this.vehicles.id:" + this.vehicles.id);

      //console.log(this.vehicles.data);
      //alert(window.$http + `vehicles/` + this.vehicles.id);
      axios
        .put(window.$http + `vehicles/` + this.vehicles.id, this.vehicles, {
          headers: window.$headers,
        })
        .then(() => {})
        .catch((e) => {
          Swal.fire("Updating Vehicles" + e);
          //alert(e);
          this.errors.push(e);
        });
    },
    postvehicles() {
      //alert(
      //   "{vehicle_axle:" + this.axlec + ",vehicle_reg:" + this.nplate + ",}"
      // );
      const data = {
        vehicle_reg: this.nplate,
        vehicle_axle: this.axlec,
        vehicle_permitexpiration_date: "0001-01-01T00:00:00",
        vehicle_permitissued_date: "0001-01-01T00:00:00",
      };

      axios
        .post(window.$http + `Vehicles/`, data, { headers: window.$headers })
        .then(() => {
          axios
            .get(window.$http + `vehicles/` + this.nplate, {
              headers: window.$headers,
            })
            .then((response) => {
              //var ddatesp = response.data.permitexpiration_date.split("T").replaceAll("-","/");
              this.vehicles = response.data[0];
              console.log(this.vehicles);
            });
        })
        .catch((e) => {
          Swal.fire("" + e);
          //this.errors.push(e);
        });
    },
    postaxles() {
      var axle_number = Number(this.axlepermissible.length);
      var axle_name = this.axlec;

      const data3 = {
        axle_code: axle_name,
        axle_name: axle_name,
        axle_number: axle_number,
      };

      axios
        .post(window.$http + `axleweightconfig/`, data3, {
          headers: window.$headers,
        })
        .then(() => {})
        .catch((e) => {
          Swal.fire("axleweightconfig" + e);
          //this.errors.push(e);
        });

      //var data = {};
      var s = axle_name.split("*");
      var axle = 1;
      var p = 0;
      var grp = ["A", "B", "C", "D"];
      var grpi = 0;
      //alert(s[1]);
      for (var i = 0; i < s[1].length; i++) {
        //grp[i] = s.charAt(i);
        //alert(s[1].charAt(i));
        if (s[1].charAt(i) != "|") {
          //alert(grp[grpi]);
          var dat = {
            axle_code: axle_name,
            axle_code2: axle,
            axle_legalweight: this.axlepermissible[p],
            axle_grouping: grp[grpi],
            axle_deckgrouping: grp[grpi],
            axle_typeoftyres: s[1].charAt(i),
          };
          p++;
          axle++;
          //Swal.close();
          axios
            .post(window.$http + `axleweightxreffs/`, dat, {
              headers: window.$headers,
            })
            .then(() => {})
            .catch((e) => {
              Swal.fire("axleweightxreffs" + e);
              //this.errors.push(e);
            });
        } else {
          grpi++;
        }
      }
    },
    updatef() {
      //alert(this.vehicles.id);

      if (this.vehicles.id > 0) {
        this.pulatefields();
      }
    },
    getreweighticket() {
      //alert(this.yard[0].wbtno);
      axios
        .get(window.$http + `weighbridgetransactions/` + this.yard[0].wbtno, {
          headers: window.$headers,
        })
        .then((rep) => {
          //alert(rep.data.wbrg_ticket_excessweight);
          this.reweighticket = rep.data;
          this.maingross = this.reweighticket.wbrg_ticket_grossweight;
          this.mainload =
            this.reweighticket.wbrg_ticket_excessload.toUpperCase();
          this.mainexcess = this.reweighticket.wbrg_ticket_excessweight;
          this.reweighgvw = this.reweighticket.wbrg_ticket_gvwload;
          this.cargo = this.reweighticket.wbrg_product;
          try {
            this.datediff = this.getmindiff(
              new Date(this.reweighticket.wbrg_ticket_date),
              new Date()
            );
          } catch (Exception) {
            //totaltyres = "0";
          }
        })
        .catch(() => {
          //Swal.fire("reweigh ticket" + e);
          //this.errors.push(e);
        });
    },
    getcasedetails() {
      axios
        .get(
          window.$http + `casedetails/search?caseid=` + this.yard[0].caseid,
          { headers: window.$headers }
        )
        .then((response) => {
          this.casedetails = response.data[0];
          var dd = 0;
          try {
            dd = response.data[0].casedocs.length;
            this.prohibitionno = response.data[0].casedocs[0].casedocid;
          } catch (e) {
            dd = 0;
          }

          if (dd == 0) {
            this.prohibitionorder =
              "Prohibition document not raise. Certificate of Complaince for " +
              this.nplate +
              " will not be issued!";
            Swal.fire({
              position: "center",
              icon: "error",
              title:
                "Prohibition document not raise. Certificate of Complaince for " +
                this.nplate +
                " will not be issued!",
              showConfirmButton: true,
            }).then(() => {
              //this.$root.$emit("bv::hide::modal", "modal-1");
              var data = {
                events: "Prohibition document not raise. ",
                datetime: this.getcurrentdate(),
                userid: window.$userid,
                desc:
                  "Prohibition document not raise. For Registration " +
                  this.nplate +
                  " ticket " +
                  this.WBTNO,
              };

              axios
                .post(window.$http + `events/`, data, {
                  headers: window.$headers,
                })
                .then(() => {})
                .catch((e) => {
                  Swal.fire("" + e);
                  //this.errors.push(e);
                });
            });
          } else {
            axios
              .get(
                window.$http +
                  `casedocs/records?caseid=` +
                  this.yard[0].caseid +
                  `&docname=EAC Act`,
                {
                  headers: window.$headers,
                }
              )
              .then((res) => {
                //alert(res.data.length + "res.data.length");
                if (res.data.length > 0) {
                  this.allgvw = 0;
                  this.allsaw = 0;
                  this.allgaw = 0;
                }
              })
              .catch((e) => {
                Swal.fire({
                  position: "center",
                  icon: "error",
                  title: "" + e,
                  showConfirmButton: true,
                }).then((e) => {
                  Swal.close(e);
                  return false;
                });

                //throw e;
              });
          }
        });
    },
    configchange() {
      var reasonchange = "";
      try {
        reasonchange = this.reasonchange.trim();
      } catch (e) {
        reasonchange = "";
      }

      if (reasonchange.length < 10) {
        if (reasonchange.length > 1) {
          Swal.fire({
            position: "center",
            icon: "error",
            title: " Axle Configuration Change ",
            showConfirmButton: true,
            text: "Please enter reason characters that are more than 10 characters.",
            timer: 15000,
            willOpen: () => {
              Swal.showLoading();
            },
          }).then(() => {
            // this.updatearray();
          });
        } else {
          Swal.fire("Please enter reason.");
        }
        return;
      }
      var rew = "";
      if (this.reweigh > 0) {
        rew = " Re-Weigh No. " + this.reweigh + " ";
      }

      var data = {
        events: "Axle Config Change",
        datetime: this.getcurrentdate(),
        userid: window.$userid,
        desc:
          this.nplate +
          rew +
          " For ticket " +
          this.WBTNO +
          " From" +
          this.vehicles.vehicle_axle +
          " to  " +
          this.axlec +
          " " +
          this.reasonchange,
      };

      axios
        .post(window.$http + `events/`, data, { headers: window.$headers })
        .then(() => {
          this.updatevehicles();

          this.$root.$emit("bv::hide::modal", "modal-AxleConfig");
          this.goToVehicleDir();
        })
        .catch((e) => {
          Swal.fire("" + e);
          //this.errors.push(e);
        });
    },
    getprovehreg() {
      //alert(reliefvehreg);
      //alert(reliefvehreg);
      axios
        .get(
          window.$http +
            `casedetails/search?vehreg=` +
            this.reliefvehreg +
            "&limit=1",
          { headers: window.$headers }
        )
        .then((response) => {
          this.casereliefdetails = response.data;
        })
        .catch((e) => {
          Swal.fire("Prohibition order not raised!" + e);
          //this.errors.push(e);
        });
    },
    updatecasedocs(mycasid, doc) {
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
            `casedocs/max?casedocid=` +
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
            casedocid: Ncasedocsid,
            caseid: this.casedetails.caseid,
            docname: doc,
            docdate: this.getcurrentdate(),
            createdby: window.$userid,
            casedetailsid: this.casedetails.id,
            cancelled: "N",
            printed: "N",
          };
          //alert("Ncasedocsid" + Ncasedocsid + "  casedetailsid" + this.id);
          axios
            .post(window.$http + `casedocs/`, data, {
              headers: window.$headers,
            })
            .then(() => {
              // JSON responses are automatically parsed.
              Swal.close();
              //this.$root.$emit("bv::hide::modal", "modal-Doc");
              Swal.fire({
                position: "center",
                icon: "success",
                title: " Document Created! " + doc,
                showConfirmButton: false,
                timer: 1500,
              }).then(() => {
                // this.updatearray();
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
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: doc + " Case Document" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
    },
    updateweight(weights) {
      this.deck1 = weights[0].deck1;
      this.deck2 = weights[0].deck2;
      this.deck3 = weights[0].deck3;
      this.deck4 = weights[0].deck4;
      //alert(this.deck1);
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
    padLeadingZeros(num, size) {
      var s = num + "";
      while (s.length < size) s = "0" + s;
      return s;
    },
    goToSrdoc(reason, description, userid) {
      var mycasid = "SP";
      //var doc = "Special Release";
      var stationc =
        mycasid + window.$stationcode + window.$weighb + this.ticketdate();
      axios
        .get(window.$http + `specialrelease/max?specialid=` + stationc, {
          headers: window.$headers,
        })
        .then((response) => {
          //alert(response.data);
          let ticketmax = Number(response.data);
          ticketmax++;
          var ticketno = this.padLeadingZeros(ticketmax, 6);
          var Ncasedocsid = stationc + ticketno;
          var data = {
            specialid: Ncasedocsid,
            vehiclereg: this.nplate,
            reason: reason,
            otherreason: description,
            wbrg_ticket_no: this.WBTNO,
            realeasedate: this.getcurrentdate(),
            realeasedby: userid,
            excessload: this.nodata.wbrg_ticket_excessweight,
            station: window.$stationcode,
          };

          axios
            .post(window.$http + `specialrelease/`, data, {
              headers: window.$headers,
            })
            .then(() => {
              // JSON responses are automatically parsed.
              //this.yardlist.spreleaseno = Ncasedocsid;
              this.nodata.wbrg_ticket_state = "S";
              this.nodata.wbrg_ticket_directionarea = description;
              this.description = description;
              this.autospecialrelease = true;
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
    getspecialredetails(sp) {
      axios
        .get(window.$http + `specialrelease/search?specialid=` + sp, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          //this.yardlist.spreleaseno = Ncasedocsid;
          this.reason = response.data[0].reason;
          this.description = response.data[0].otherreason;
          this.reasondescription =
            "Special Release:" + this.reason + "; " + this.description;
          this.autospecialrelease = true;
          this.nodata.wbrg_ticket_state = "S";
          this.nodata.wbrg_ticket_directionarea = this.reasondescription;
          this.description = this.reasondescription;

          axios
            .get(
              window.$http +
                `users/search?email=` +
                response.data[0].realeasedby,

              { headers: window.$headers }
            )
            .then((re) => {
              this.reasondescription +=
                " ; Authorized by: " + re.data[0].fullnames.toUpperCase();
              this.description = this.reasondescription;
            })
            .catch((e) => {
              console.log(e);
              //alert(e);
            });
        })
        .catch(() => {
          // Swal.fire({
          //   position: "center",
          //   icon: "error",
          //   title: "Special Release Document " + e,
          //   showConfirmButton: true,
          // }).then((e) => {
          //   Swal.close(e);
          // });
          //throw e;
        });
    },
    addmake() {
      if (this.vehiclemake.trim() == "") {
        Swal.fire("Please enter  Vehicle Make Name!");
        return;
      }

      const data = {
        vehiclemake: this.vehiclemake.toUpperCase(),
        active: 0,
      };
      axios
        .post(window.$http + `VehicleMake/`, data, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.make = this.vehiclemake.toUpperCase();
          this.getvehiclemake();
          this.$root.$emit("bv::hide::modal", "modal-vmake");
          console.log(response.data);
          Swal.fire({
            position: "center",
            icon: "success",
            title: "Your work has been saved",
            showConfirmButton: false,
            timer: 1500,
          });
        })
        .catch((e) => {
          Swal.fire("" + e);
        });
    },
    addcargo() {
      if (this.cargo_name.trim() == "") {
        Swal.fire("Please enter  Cargos Name!");
        return;
      }

      const data = {
        cargo_name: this.cargo_name.toUpperCase(),
        active: 0,
      };
      axios
        .post(window.$http + `Cargos/`, data, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.cargo = this.cargo_name.toUpperCase();
          this.getcargo();
          this.$root.$emit("bv::hide::modal", "modal-AddCargo");
          console.log(response.data);
          this.addcargo = true;
          Swal.fire({
            position: "center",
            icon: "success",
            title: "Your work has been saved",
            showConfirmButton: false,
            timer: 1500,
          });
        })
        .catch((e) => {
          Swal.fire("" + e);
        });
    },
    addTransporter() {
      if (this.transporter_name.trim() == "") {
        Swal.fire("Please enter  Transporters Name!");
        return;
      }
      if (this.transporter_address.trim() == "") {
        Swal.fire("Please enter  Transporters Address!");
        return;
      }
      if (this.transporter_contact.trim() == "") {
        Swal.fire("Please enter  Transporters Contact!");
        return;
      }

      const data = {
        transporter_name: this.transporter_name,
        transporter_address: this.transporter_address,
        transporter_contact: this.transporter_contact,
        active: 0,
      };
      axios
        .post(window.$http + `Transporters/`, data, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.transporter = this.transporter_name;
          this.gettransporters();
          this.$root.$emit("bv::hide::modal", "modal-AddTransporter");
          console.log(response.data);
          this.addtransporter = true;
          Swal.fire({
            position: "center",
            icon: "success",
            title: "Your work has been saved",
            showConfirmButton: false,
            timer: 1500,
          });
        })
        .catch((e) => {
          Swal.fire("" + e);
        });
    },
    addOrigin() {
      if (this.origin_name.trim() == "") {
        Swal.fire("Please enter  Origin / Destination !");
        return;
      }

      const data = {
        origin_name: this.origin_name.toUpperCase(),
        active: 0,
      };
      axios
        .post(window.$http + `Origins/`, data, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          //alert(this.origin_name);
          this.origin = this.origin_name.toUpperCase();
          this.$root.$emit("bv::hide::modal", "modal-AddOrigin");
          console.log(response.data);
          Swal.fire({
            position: "center",
            icon: "success",
            title: "Your work has been saved",
            showConfirmButton: false,
            timer: 1500,
          });
        })
        .catch((e) => {
          Swal.fire("" + e);
        });
    },
    staticCheck() {
      Swal.fire({
        title:
          "Are you sure, you want to Change to Static Weighing for " +
          this.nplate +
          "?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, Start Static Weighing",
      }).then((result) => {
        if (result.value) {
          Swal.fire(
            "Static Weighing !",
            this.nplate + "  Static Weighing !.",
            "success"
          );
          this.staticweighlswim = true;
          this.staticweighlswimstatus = "Static Weighing";
          this.staticw = false;
          this.resetlswim();
          return;
        }
        this.staticw = true;
      });
    },
    assignweight() {
      if (Number(this.$refs.Axleweights.deck1) <= 200) {
        Swal.fire("You can't weigh value less than 200 KG !");
        return;
      }
      if (window.$weighstate >= 1) {
        Swal.fire("You can't weigh twice, until the vehicle leaves.");
        return;
      }
      this.axleweight[this.weightindex] = this.$refs.Axleweights.deck1;
      this.axletime[this.weightindex] = this.getcurrentdate();

      this.genimganpr(this.weightindex + "");
      ///this.lsweight = 0;
      this.weightindex++;
      if (this.weightindex == this.mylength) {
        this.getaxlevals();
        this.weighdate = this.getcurrentdate();

        this.weightindex = 0;
        this.lswimtimer = 1000000000;
        //this.weighttaken = true;
        this.finish = true;
      }
      var detail = {
        wbrg_ticket_no: this.WBTNO,
        wbrg_ticket_gvwload: this.gvwoverload,
        wbrg_ticket_axleload: this.axleweight[this.weightindex],
        wbrg_ticket_grossweight: this.axleweight[this.weightindex],
        wbrg_ticket_dateout: this.axletime[this.weightindex],
        wbrg_ticket_axel: this.axlec,
        Weighbridgetransactionsid: this.nodata.id,
        timestamp: this.getcurrentdate(),
        nplate: this.nplate,
        weightaken: 1,
      };

      console.log(detail);
      //"Content-Type": "application/x-www-form-urlencoded",
      var optionAxios = {
        headers: {
          "Content-Type": "application/x-www-form-urlencoded",
        },
      };

      axios
        .post(`http://127.0.0.1:3031/weights`, detail, optionAxios)
        .then((res) => {
          //tt = false;
          this.weightres = res;
        })
        .catch((e) => {
          Swal.fire("Send Weights" + e);
          console.log(e);
        });
    },
    getaxlevals() {
      var mylength = this.axlec.substr(0, 1);
      //alert(this.permissible.length);
      if (this.axlec.trim() == "") {
        return;
      }
      for (let i = 0; i < mylength; i++) {
        if (this.permissible[i] > 0) {
          this.orderData3.push(Object.values(this.orderData2[0]));
        }
      }
      var id = 0;
      this.mylength = mylength;
      for (let i = 0; i < mylength; i++) {
        id++;
        this.orderData3[i].group = id;
        if (this.axlepermissible[i] == null) {
          this.axlepermissible[i] = 0;
        }
        this.orderData3[i].permissible = this.axlepermissible[i];
        var tole = this.axlepermissible[i];
        this.orderData3[i].tolerance = this.axlepermissible[i];
        var load = this.axleweight[i] - this.orderData3[i].tolerance;
        var loadbal = this.axleweight[i] - this.axlepermissible[i];
        if (this.axleweight[i] >= 100) {
          this.orderData3[i].result = "Legal";
          if (load <= 0) {
            load = 0;
          } else {
            this.orderData3[i].result = "Overload";
          }
          if (tole > loadbal && loadbal > 0) {
            this.orderData3[i].result = "Warning";
          }
        } else {
          this.orderData3[i].result = "Error";
          this.vehicleposition = "Not Positioned Correctly";
        }

        this.orderData3[i].actual = this.axleweight[i];
        this.orderData3[i].overload = load;
        this.orderData3[i].pdf = 0.0;
        //this.totalweight += Number(this.axleweight[i]);
        //this.gvwperm += Number(this.axlepermissible[i]);
      }
      //this.weights= this.axleweight[i];
      var a = 0;
      var b = 0;
      var c = 0;
      var d = 0;
      var p = 0;
      for (let k = 0; k < this.AxleWeightXreffs.length; k++) {
        //console.log("//");
        if (this.AxleWeightXreffs[k].axle_code == this.axlec) {
          //alert(group);
          // console.log(
          //   this.AxleWeightXreffs[k].axle_deckgrouping + "--" + p++
          // );
          if (
            this.AxleWeightXreffs[k].axle_deckgrouping == "A" &&
            Number(this.axleweight[p]) > 0
          ) {
            a = a + Number(this.axleweight[p]);
            ///alert(a);
          }

          if (
            this.AxleWeightXreffs[k].axle_deckgrouping == "B" &&
            Number(this.axleweight[p]) > 0
          ) {
            b = b + Number(this.axleweight[p]);
          }

          if (
            this.AxleWeightXreffs[k].axle_deckgrouping == "C" &&
            Number(this.axleweight[p]) > 0
          ) {
            c = c + Number(this.axleweight[p]);
          }

          if (
            this.AxleWeightXreffs[k].axle_deckgrouping == "D" &&
            Number(this.axleweight[p]) > 0
          ) {
            d = d + Number(this.axleweight[p]);
          }
          p++;
        }
      }
      //console.log(a + "" + b);
      //alert("weights");
      this.weights = [a, b, c, d];
      this.orderData3.splice(mylength, this.orderData3.length);
      this.getorders(this.weights);
    },
  },
  // updated() {
  //   //this.getaxle();
  // },
  // beforeRouteLeave(next) {
  //   const answer = window.confirm(
  //     "Do you really want to leave? Without weighing!"
  //   );
  //   if (answer) {
  //     next();
  //   } else {
  //     next(false);
  //   }
  // },
  mounted() {
    //this.loadvalues();
    //this.focusInput();
    //alert(this.nplate + " ppp");
    this.origin = window.$origin;
    this.destination = window.$destination;
    this.getcameras();
    this.getGetAVWoverloadCharges();

    this.startlswim();
    if (this.$route.params.vehicles.id > 0) {
      this.updatef();
      // if (this.axlec != "") {

      //}
    }
    //alert(this.permit.length);
    if (this.permit.length < 5) {
      //alert("getting permit no");
      this.getpermitno(this.nplate);
    }

    axios
      .get(window.$http + `permiturl`, {
        headers: window.$headers,
      })
      .then((response) => {
        let orderData = response.data;
        //alert(orderData[0].permiturl);
        window.$permiturl = orderData[0].permiturl;
      })
      .catch(() => {
        //alert(e);
      });
  },
  created() {
    // var href = window.location.href;

    // var arr = href.split("/");
    // var ref2 = arr[2].split(":");

    // var ref = arr[0] + "//" + ref2[0] + "";
    //alert(this.$route.params.nodata.id);
    this.url = window.$url;
    this.loadvalues();
    this.nplate = this.$route.params.nplate;
    //alert(this.$route.params.tag + "this.$route.params.tag");
    this.tagged = this.$route.params.tag;
    //alert(this.tagged);
    this.cat = this.$route.params.cat;
    this.reweigh = this.$route.params.reweighno;
    this.startdatetime = this.$route.params.startdatetime;
    this.yard = this.$route.params.yard;
    this.WBTNO = this.$route.params.WBTNO;
    this.nodata = this.$route.params.nodata;
    this.inyard = this.$route.params.inyard;
    var year = this.startdatetime.substring(0, 4);
    var month = this.startdatetime.substring(5, 7);
    var day = this.startdatetime.substring(8, 10);
    var hour = this.startdatetime.substring(11, 13);
    this.dir = year + month + "/" + day + "/" + hour + "/";
    if (this.cat == "lswim") {
      this.stoped = true;
    }
    if (this.cat == "multideck") {
      this.stoped2 = true;
    }
    //alert(this.$route.params.vehicles.id);
    if (this.$route.params.vehicles.id > 0) {
      //this.updatef();
      this.vehicles = this.$route.params.vehicles;
      //alert(this.vehicles.vehicle_axle);
    }
    this.getyardlist();
    if (this.reweigh > 0) {
      this.getreweighticket();
      this.getcasedetails();
      try {
        this.spreleaseno = this.yard[0].spreleaseno;
        //this.autospecialrelease = true;
        this.getspecialredetails(this.spreleaseno);
      } catch (e) {
        this.error = e;
      }

      // var sp = "";
      // try {
      //   sp = this.yard[0].spreleaseno;
      // } catch (e) {
      //   sp = "";
      // }
      // if (sp.length > 0) {
      //   try {
      //     this.spreleaseno = this.yard[0].spreleaseno;

      //     this.autospecialrelease = true;
      //     this.getspecialredetails(this.spreleaseno);
      //   } catch (e) {
      //     this.error = e;
      //   }
      // }
    }

    var timer = setInterval(() => {
      try {
        this.seeweight = this.$refs.Axleweights.deck1;
      } catch (e) {
        this.error = e;
      }
      if (this.cat == "multideck") {
        if (!this.weighttaken && !this.weighttaking) {
          if (Number(this.deck1) <= 100) {
            if (Number(this.deck2) <= 100) {
              this.weights = [this.deck3, this.deck4, 0, 0];
            } else {
              this.weights = [this.deck2, this.deck3, this.deck4, 0];
            }
          } else {
            this.weights = [this.deck1, this.deck2, this.deck3, this.deck4];
          }
        }
      }
      if (this.cat == "lswim") {
        this.getaxlevals();
      }
      //alert(this.weights);
      this.getorders(this.weights);
      if (this.num <= 0) {
        clearInterval(timer);
      }
    }, this.timer);
  },
  data() {
    return {
      weightindex: 0,
      speed: 0,
      overspeed: false,
      axleweight: [
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
      ],
      axletime: [
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
        "0",
      ],

      reliefvehreg: "",
      casereliefdetails: [],
      hasavalidpermit: "",
      prohibitionorder: "",
      cameras: [],
      destination: "",
      editmode: false,
      weightres: "false",
      reasondescription: "",
      AVWoverloadCharges: [],
      url: "",
      deck1: 0,
      deck2: 0,
      deck3: 0,
      deck4: 0,
      stoped: false,
      stoped2: false,
      prohibitionno: "",
      vehicles: [],
      lsweight: 0,
      mylength: 0,
      group1: 0,
      group2: 0,
      group3: 0,
      group4: 0,
      error: "",
      lstatus: "Start",
      weighttaken: false,
      weighttaking: false,
      weighdate: "",
      timer: 100,
      staticweighlswim: false,
      staticweighlswimstatus: "LSWIM",
      staticw: true,
      reasonchange: "",
      lswimtimer: 1000000,
      vehicleDirection: 1,
      vehicleposition: "",
      tagged: false,
      startdatetime: "",
      yard: [],
      reweighticket: [],
      WBTNO: "",
      nodata: "",
      finish: false,
      src2: require("@/assets/images/truckexample.png"),
      src3: require("@/assets/images/truckexample.png"),
      inyard: false,
      reason: "",
      description: "",
      vDirection: 0,
      modalname: "modal-Edit",
      granted: false,
      dir: "",
      cat: "",
      isVisible: true,
      myimg: "@/assets/images/weigh/single.png",
      AxleWeightXreffs: [],
      axle_deckgrouping: [],
      axle_typeoftyres: [],
      orderData: [],
      orderData2: [
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
          group: 2,
          permissible: 1,
          tolerance: 1,
          actual: 0,
          overload: 0,
          pdf: 0.0,
          result: "",
          deck: "A",
        },
        {
          group: 3,
          permissible: 0,
          tolerance: 0,
          actual: 0,
          overload: 0,
          pdf: 0.0,
          result: "",
          deck: "A",
        },
        {
          group: 4,
          permissible: 0,
          tolerance: 0,
          actual: 0,
          overload: 0,
          pdf: 0.0,
          result: "",
          deck: "A",
        },
        {
          group: 5,
          permissible: 0,
          tolerance: 0,
          actual: 0,
          overload: 0,
          pdf: 0.0,
          result: "",
          deck: "A",
        },
        {
          group: 6,
          permissible: 0,
          tolerance: 0,
          actual: 0,
          overload: 0,
          pdf: 0.0,
          result: "",
          deck: "A",
        },
        {
          group: 7,
          permissible: 0,
          tolerance: 0,
          actual: 0,
          overload: 0,
          pdf: 0.0,
          result: "",
          deck: "A",
        },
        {
          group: 8,
          permissible: 0,
          tolerance: 0,
          actual: 0,
          overload: 0,
          pdf: 0.0,
          result: "",
          deck: "A",
        },
        {
          group: 9,
          permissible: 0,
          tolerance: 0,
          actual: 0,
          overload: 0,
          pdf: 0.0,
          result: "",
          deck: "A",
        },
      ],
      num: 10,
      maingross: 0,
      mainload: "",
      mainexcess: 0,
      reweighgvw: 0,
      spreleaseno: "",
      orderData3: [],
      gvwoverload: 0,
      autospecialrelease: false,
      axlegrouping: ["", "", "", "", "", "", "", "", "", "", "", "", "", ""],
      axlegrouped: null,
      permissible: [0, 0, 0, 0],
      axlepermissible: [8000, 9000, 9000, 8000, 8000, 8000],
      totalRows: 1,
      currentPage: 1,
      perPage: 10,
      pageOptions: [
        1, 2, 5, 10, 25, 50, 100, 200, 300, 500, 1000, 2000, 3000, 4000, 5000,
        6000, 9000, 10000, 20000,
      ],
      filter: null,
      filterOn: [],
      sortBy: "id",
      sortDesc: false,
      fields: [
        {
          key: "group",
          label: "Group",
          sortable: true,
        },
        {
          key: "permissible",
          label: "Permissible [KG]",
          sortable: true,
        },
        {
          key: "tolerance",
          label: "Permissible Tolerances (5%) [KG]",
          sortable: true,
        },
        {
          key: "actual",
          label: "Actual [KG]",
          sortable: true,
        },
        {
          key: "overload",
          label: "Overload [KG]",
          sortable: true,
        },
        {
          key: "pdf",
          label: "Pavement Damage Factor (PDF)",
          sortable: true,
        },
        {
          key: "result",
          label: "Results",
          sortable: true,
        },
      ],
      fields3: [
        {
          key: "group",
          label: "Axle",
          sortable: true,
        },
        {
          key: "permissible",
          label: "Permissible [KG]",
          sortable: true,
        },
        {
          key: "tolerance",
          label: "Permissible Tolerances [KG]",
          sortable: true,
        },
        {
          key: "actual",
          label: "Actual [KG]",
          sortable: true,
        },
        {
          key: "overload",
          label: "Overload [KG]",
          sortable: true,
        },
        {
          key: "pdf",
          label: "Pavement Damage Factor (PDF)",
          sortable: true,
        },
        {
          key: "result",
          label: "Results",
          sortable: true,
        },
      ],
      axles: [
        "2A",
        "2*SS",
        "3A",
        "3*SDD",
        "3*SDS",
        "3*SSD",
        "3*SSS",
        "3*WDD",
        "3*WDW",
        "3*SWW",
      ],
      seeweight: "",
      disabled: false,
      transporter_name: "",
      transporter_address: "",
      transporter_contact: "",
      status: "",
      authweight: 0,
      tareweight: 0,
      authgiven: false,
      modaltitle: "Axle Configuration Change",
      weights: [0, 0, 0, 0],
      axlegroup: ["", "", "", "", "", "", "", "", "", "", "", "", "", ""],
      axleimg: ["", "", "", "", "", "", "", "", "", "", "", "", "", ""],
      axlec: "",
      gvwlabel: "",
      totalweight: 0,
      decktp1: 0,
      gvwperm: 0,
      nplate: "",
      registration: "",
      permit: "-",
      validpermit: false,
      comment: "",
      trailer: " ",
      driver: " ",
      make: " ",
      vehiclemake: " ",
      cargo: "",
      cargo_name: "",
      axle: "",
      transporter: "",
      origin: "",
      origin_name: "",
      reweigh: 0,
      datediff: 0,
      makes: [],
      cargos: [],
      origins: [],
      vehinyard: [],
      allgvw: 0,
      allsaw: 0,
      allgaw: 0,
      userautosp: "",
      usernameautosp: "",
      transporters: [],
      drivers: [],

      series: [
        {
          data: [25, 66, 41, 89, 63, 25, 44, 20, 36, 40, 54],
        },
      ],
      chartOptions: {
        fill: {
          colors: ["#5b73e8"],
        },
        chart: {
          type: "bar",
          sparkline: {
            enabled: true,
          },
        },
        plotOptions: {
          bar: {
            columnWidth: "50%",
          },
        },
        labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11],
        xaxis: {
          crosshairs: {
            width: 1,
          },
        },
        tooltip: {
          fixed: {
            enabled: false,
          },
          x: {
            show: false,
          },
          y: {
            title: {
              formatter: function () {
                return "";
              },
            },
          },
          marker: {
            show: false,
          },
        },
      },
      growthChartOptions: {
        fill: {
          colors: ["#f1b44c"],
        },
        chart: {
          type: "bar",
          sparkline: {
            enabled: true,
          },
        },
        plotOptions: {
          bar: {
            columnWidth: "50%",
          },
        },
        labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11],
        xaxis: {
          crosshairs: {
            width: 1,
          },
        },
        tooltip: {
          fixed: {
            enabled: false,
          },
          x: {
            show: false,
          },
          y: {
            title: {
              formatter: function () {
                return "";
              },
            },
          },
          marker: {
            show: false,
          },
        },
      },
      orderseries: [70],
      orderRadial: {
        fill: {
          colors: ["#34c38f"],
        },
        chart: {
          sparkline: {
            enabled: true,
          },
        },
        dataLabels: {
          enabled: false,
        },
        plotOptions: {
          radialBar: {
            hollow: {
              margin: 0,
              size: "60%",
            },
            track: {
              margin: 0,
            },
            dataLabels: {
              show: false,
            },
          },
        },
      },
      customerseries: [55],
      customerRadial: {
        fill: {
          colors: ["#5b73e8"],
        },
        chart: {
          sparkline: {
            enabled: true,
          },
        },
        dataLabels: {
          enabled: false,
        },
        plotOptions: {
          radialBar: {
            hollow: {
              margin: 0,
              size: "60%",
            },
            track: {
              margin: 0,
            },
            dataLabels: {
              show: false,
            },
          },
        },
      },
    };
  },
};
</script>

<template>
  <div>
    <div v-if="isVisible">
      <div class="row">
        <Deckweights
          id="Deckweights"
          ref="Deckweights"
          v-show="cat == 'multideck'"
          :stoped2="stoped2"
          v-on:updateweight="updateweight($event)"
        />
        <Axleweights
          ref="Axleweights"
          v-show="cat == 'lswim'"
          :stoped="stoped"
        />
        <div class="col-md-3 col-xl-6 mb-0 mt-0">
          <div class="card changebg" style="font-size: 10px">
            <div class="card-body align-items-center">
              <!---- Vehicle Axle Grouping --->

              <div class="row justify-content-center">
                <table class="styled-table mb-2 mt-0">
                  <thead>
                    <tr>
                      <th colspan="14" class="textaxlesize">
                        Vehicle Axle Grouping Configuration
                        {{ axlepermissible }}
                      </th>
                    </tr>

                    <tr>
                      <th colspan="2" class="textaxlesize">GROUP A</th>
                      <th colspan="4" class="textaxlesize">GROUP B</th>
                      <th colspan="4" class="textaxlesize">GROUP C</th>
                      <th colspan="4" class="textaxlesize">GROUP D</th>
                    </tr>
                  </thead>

                  <tbody>
                    <tr>
                      <!--- GRoup A --->
                      <td
                        class="text-center"
                        @click="changestate(axlegroup[0], 0)"
                      >
                        <img :src="axleimg[0]" />
                      </td>
                      <td
                        class="text-center"
                        @click="changestate(axlegroup[1], 1)"
                      >
                        <img :src="axleimg[1]" />
                      </td>
                      <!--- GRoup A  END --->

                      <!--- GRoup B --->
                      <td
                        class="text-center"
                        @click="changestate(axlegroup[2], 2)"
                      >
                        <img :src="axleimg[2]" />
                      </td>
                      <td
                        class="text-center"
                        @click="changestate(axlegroup[3], 3)"
                      >
                        <img :src="axleimg[3]" />
                      </td>
                      <td
                        class="text-center"
                        @click="changestate(axlegroup[4], 4)"
                      >
                        <img :src="axleimg[4]" />
                      </td>
                      <td
                        class="text-center"
                        @click="changestate(axlegroup[5], 5)"
                      >
                        <img :src="axleimg[5]" />
                      </td>
                      <!--- GRoup B  END --->
                      <!--- GRoup C  --->
                      <td
                        class="text-center"
                        @click="changestate(axlegroup[6], 6)"
                      >
                        <img :src="axleimg[6]" />
                      </td>
                      <td
                        class="text-center"
                        @click="changestate(axlegroup[7], 7)"
                      >
                        <img :src="axleimg[7]" />
                      </td>
                      <td
                        class="text-center"
                        @click="changestate(axlegroup[8], 8)"
                      >
                        <img :src="axleimg[8]" />
                      </td>
                      <td
                        class="text-center"
                        @click="changestate(axlegroup[9], 9)"
                      >
                        <img :src="axleimg[9]" />
                      </td>
                      <!--- GRoup C  END --->
                      <!--- GRoup D  --->
                      <td
                        class="text-center"
                        @click="changestate(axlegroup[10], 10)"
                      >
                        <img :src="axleimg[10]" />
                      </td>
                      <td
                        class="text-center"
                        @click="changestate(axlegroup[11], 11)"
                      >
                        <img :src="axleimg[11]" />
                      </td>
                      <td
                        class="text-center"
                        @click="changestate(axlegroup[12], 12)"
                      >
                        <img :src="axleimg[12]" />
                      </td>
                      <td
                        class="text-center"
                        @click="changestate(axlegroup[13], 13)"
                      >
                        <img :src="axleimg[13]" />
                      </td>
                      <!--- GRoup D  END --->
                    </tr>

                    <!----- Status --->
                    <tr>
                      <!--- GRoup A --->
                      <td @click="changestate(axlegroup[0], 0)">
                        <div class="textaxlesize">{{ axlegroup[0] }}</div>
                      </td>
                      <td @click="changestate(axlegroup[1], 1)">
                        <div class="textaxlesize">{{ axlegroup[1] }}</div>
                      </td>
                      <!--- GRoup A  END --->

                      <!--- GRoup B --->
                      <td @click="changestate(axlegroup[2], 2)">
                        <div class="textaxlesize">{{ axlegroup[2] }}</div>
                      </td>
                      <td @click="changestate(axlegroup[3], 3)">
                        <div class="textaxlesize">{{ axlegroup[3] }}</div>
                      </td>
                      <td @click="changestate(axlegroup[4], 4)">
                        <div class="textaxlesize">{{ axlegroup[4] }}</div>
                      </td>
                      <td @click="changestate(axlegroup[5], 5)">
                        <div class="textaxlesize">{{ axlegroup[5] }}</div>
                      </td>
                      <!--- GRoup B  END --->
                      <!--- GRoup C  --->
                      <td @click="changestate(axlegroup[6], 6)">
                        <div class="textaxlesize">{{ axlegroup[6] }}</div>
                      </td>
                      <td @click="changestate(axlegroup[7], 7)">
                        <div class="textaxlesize">{{ axlegroup[7] }}</div>
                      </td>
                      <td @click="changestate(axlegroup[8], 8)">
                        <div class="textaxlesize">{{ axlegroup[8] }}</div>
                      </td>
                      <td @click="changestate(axlegroup[9], 9)">
                        <div class="textaxlesize">{{ axlegroup[9] }}</div>
                      </td>
                      <!--- GRoup C  END --->
                      <!--- GRoup D  --->
                      <td @click="changestate(axlegroup[10], 10)">
                        <div class="textaxlesize">{{ axlegroup[10] }}</div>
                      </td>
                      <td @click="changestate(axlegroup[11], 11)">
                        <div class="textaxlesize">{{ axlegroup[11] }}</div>
                      </td>
                      <td @click="changestate(axlegroup[12], 12)">
                        <div class="textaxlesize">{{ axlegroup[12] }}</div>
                      </td>
                      <td @click="changestate(axlegroup[13], 13)">
                        <div class="textaxlesize">{{ axlegroup[13] }}</div>
                      </td>
                      <!--- GRoup D  END --->
                    </tr>

                    <!---- Status END---->
                  </tbody>
                </table>
              </div>
              <!---- Table -->
              <div class="row">
                <div class="col-md-5 mb-0 mt-0">
                  <h5 class="font-size-14 mb-0">
                    <i class="mdi mdi-arrow-right text-primary me-1"></i>
                    Ticket Details for {{ nplate }} - Axle Grouping {{ axlec }}
                  </h5>
                </div>
                <div class="col-md-3 mb-0 mt-0">
                  <div class="card mb-0 mt-0" v-show="staticweighlswim">
                    <div
                      class="card-body"
                      style="
                        background-color: black;
                        color: yellow;
                        float: right;
                        border: 2px solid grey;
                        border-radius: 5px;
                      "
                    >
                      <div>
                        <h5 class="mb-0 mt-0">
                          <span class="localstyle">
                            <p
                              class="mb-0"
                              style="font-size: 24px; text-align: center"
                            >
                              {{ seeweight }} [KG]
                            </p>
                          </span>
                        </h5>
                      </div>
                    </div>
                  </div>
                </div>
                <!-- kkkkk -->
                <div class="col-md-2 col-xl-2 mb-0 mt-0">
                  <div class="col-sm-12" v-show="cat == 'lswim'">
                    <b-button
                      class=""
                      v-show="!weighttaken && staticweighlswim && !finish"
                      @click="assignweight()"
                      style="
                        background-color: green;
                        color: yellow;
                        float: right;
                        border: 2px solid greenyellow;
                        border-radius: 10px;
                      "
                    >
                      ASSIGN Axle({{ weightindex + 1 }})
                    </b-button>
                  </div>
                </div>
                <!-- kkkkk -->
                <div class="col-md-1 col-xl-2 mb-0 mt-0">
                  <div class="col-sm-12" v-show="cat == 'lswim'">
                    <input
                      type="checkbox"
                      v-show="staticw"
                      @click="staticCheck()"
                      v-model="staticw"
                      style="margin: 20px"
                    />
                    <label style="font-size: larger">
                      {{ staticweighlswimstatus }}</label
                    >
                  </div>
                </div>
                <!-- dfdfd -->
              </div>
              <div
                v-show="cat == 'lswim'"
                class="
                  table table-centered
                  datatable
                  dt-responsive
                  nowrap
                  table-card-list
                  dataTable
                  no-footer
                  dtr-inline
                  styled-table
                  textRight
                  mb-0
                  mt-0
                "
              >
                <!-- Table -->

                <b-table
                  table-class="table table-centered dataTable textRights mb-0 mt-0"
                  thead-tr-class="bg-transparent"
                  :items="orderData3"
                  :fields="fields3"
                  responsive="sm"
                  :per-page="perPage"
                  :current-page="currentPage"
                  :sort-by.sync="sortBy"
                  :sort-desc.sync="sortDesc"
                  :filter="filter"
                  :filter-included-fields="filterOn"
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
                      class="badge bg-pill bg-soft-success font-size-12"
                      :class="{
                        'bg-soft-danger': data.item.result === 'Error',
                        'bg-soft-danger': data.item.result === 'Overload',
                        'bg-soft-success': data.item.result === 'Legal',
                        'bg-soft-secondary': data.item.result === 'Warning',
                      }"
                    >
                      {{ data.item.result }}
                    </div>
                  </template>
                </b-table>
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
                  styled-table
                  textRight
                  mb-0
                  mt-0
                "
              >
                <!-- Table -->

                <b-table
                  table-class="table table-centered dataTable textRights mb-2 mt-0"
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
                  <template v-slot:cell(overload)="data">
                    <div
                      :class="Number(data.item.overload) > 0 ? 'textred' : ''"
                    >
                      {{ data.item.overload }}
                    </div>
                  </template>
                  <template v-slot:cell(result)="data">
                    <div
                      class="badge bg-pill bg-soft-success font-size-12"
                      :class="{
                        'bg-soft-danger': data.item.result === 'Error',
                        'bg-soft-danger': data.item.result === 'Overload',
                        'bg-soft-success': data.item.result === 'Legal',
                        'bg-soft-warning': data.item.result === 'Warning',
                      }"
                    >
                      {{ data.item.result }}
                    </div>
                  </template>
                </b-table>
              </div>

              <div class="table-responsive">
                <div class="card">
                  <div
                    class="card-body"
                    :style="
                      gvwlabel == 'Overload'
                        ? 'background-color: red'
                        : gvwlabel == 'Error'
                        ? 'background-color: pink'
                        : gvwlabel == 'Warning'
                        ? 'background-color: yellow'
                        : gvwlabel == 'Legal'
                        ? 'background-color: white'
                        : gvwlabel == 'Warning'
                        ? 'alert alert-warning'
                        : 'alert alert-primary'
                    "
                    style="font-weight: bold"
                  >
                    <table width="100%" bgcolor="white">
                      <thead></thead>
                      <tbody>
                        <tr>
                          <th
                            class="font-size-14 mb-0"
                            style="text-align: center"
                            colspan="2"
                          >
                            Permissible [KG]: {{ gvwperm }}
                          </th>
                          <th
                            class="font-size-14 mb-0"
                            style="text-align: center"
                            colspan="2"
                          >
                            GVW [KG]:
                            {{ totalweight }}
                          </th>

                          <th
                            :class="
                              gvwoverload > 0
                                ? 'font-size-14 mb-0'
                                : 'font-size-14 mb-0'
                            "
                            style="text-align: center"
                            colspan="2"
                          >
                            GVW Overload [KG]:
                            {{ gvwoverload }}
                          </th>
                          <th
                            class="badge bg-pill bg-soft-success font-size-20"
                            :class="{
                              'bg-soft-danger': gvwlabel === 'Error',
                              'bg-soft-danger': gvwlabel === 'Overload',
                              'bg-soft-success': gvwlabel === 'Legal',
                              'bg-soft-warning': gvwlabel === 'Warning',
                            }"
                          >
                            {{ gvwlabel }}
                          </th>
                        </tr>
                      </tbody>
                    </table>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div class="col-md-12">
            <span
              class="badge bg-pill bg-soft-success font-size-20"
              role="alert"
            >
              Allowable Excess Weight :
            </span>
            <span
              class="badge bg-pill bg-soft-success font-size-20"
              role="alert"
            >
              GAW :{{ allgaw }} [KG]
            </span>
            <span
              class="badge bg-pill bg-soft-success font-size-20"
              role="alert"
            >
              GVW :{{ allgvw }} [KG]
            </span>
          </div>
        </div>

        <div class="col-md-6 col-xl-6">
          <form @submit.prevent="goToVehicleDir()">
            <div class="row">
              <div class="col-sm-2">
                <h5 class="font-size-14 mb-4">
                  <i class="mdi mdi-arrow-right text-primary me-1"></i>
                  Vehicle Details
                </h5>
              </div>
              <!-- <div class="col-md-2" v-show="cat == 'lswim'">
                <b-button
                  pill
                  class=""
                  v-show="!weighttaken"
                  @click="startlswim()"
                  :style="
                    lstatus == 'Stop'
                      ? 'background-color: red; color: yellow; float: right'
                      : 'background-color: green; color: yellow; float: right'
                  "
                >
                  {{ lstatus }}
                </b-button>
              </div> -->
              <p class="col-sm-2 textBig">{{ nplate }}</p>
              <p class="col-sm-2 textBig">{{ WBTNO }}</p>
              <div class="col-sm-4" v-show="cat == 'lswim'">
                <span
                  class=""
                  :style="
                    overspeed
                      ? 'background-color: red;color: yellow;float: right;margin: 5px;padding: 2px;border: 2px solid black;border-radius: 2px;'
                      : 'background-color: green;color: yellow;float: right;margin: 5px;padding: 2px;border: 2px solid black;border-radius: 2px;'
                  "
                >
                  SPEED: {{ speed }} KM/H
                </span>
              </div>
              <div class="col-sm-2" v-show="cat == 'lswim'">
                <b-button
                  class=""
                  v-show="!weighttaken"
                  @click="resetlswim()"
                  style="background-color: red; color: yellow; float: right"
                >
                  RESTART
                </b-button>
              </div>
            </div>
            <div class="row">
              <div class="col-sm-12">
                <div class="input-group">
                  <div class="input-group-text col-sm-3">Permit No:</div>
                  <!-- :disabled="!weighttaken ? disabled : ''" -->
                  <input
                    ref="permit"
                    style="font-size-12px"
                    class="form-control"
                    list="permitOptions"
                    id="exampleDataList"
                    placeholder=""
                    v-model="permit"
                    @blur="getpermit()"
                  />
                  <b-button
                    v-b-modal.modal-Edit
                    variant="primary"
                    @click="getpermit()"
                    >...
                  </b-button>
                </div>
              </div>

              <div class="col-sm-12">
                <div class="input-group">
                  <div class="input-group-text col-sm-3">Trailer No:</div>
                  <input
                    style="font-size-12px"
                    class="form-control"
                    list="trailerOptions"
                    id="exampleDataList"
                    placeholder="Type to search..."
                    v-model="trailer"
                  />
                  <datalist id="trailerOptions">
                    <option value="ZC1028"></option>
                    <option value="ZA21872"></option>
                    <option value="ZB1862"></option>
                  </datalist>
                </div>
              </div>

              <div class="col-sm-12">
                <div class="input-group">
                  <div class="input-group-text col-sm-3">Driver Name:</div>
                  <input
                    style="font-size: 12px"
                    class="form-control"
                    list="driverOptions"
                    id="trailerList"
                    placeholder="Type to search..."
                    v-model="driver"
                  />
                  <datalist
                    id="driverOptions"
                    v-for="drive in drivers"
                    :key="drive"
                  >
                    <option
                      v-for="drive in drivers"
                      :key="drive"
                      :value="drive"
                    ></option>
                  </datalist>
                </div>
              </div>

              <div class="col-sm-12">
                <div class="input-group">
                  <div
                    :class="
                      vehicleDirection == 0
                        ? 'input-group-text textred col-sm-3'
                        : 'input-group-text col-sm-3'
                    "
                  >
                    Vehicle Make:
                  </div>
                  <multiselect
                    style="font-size: 12px"
                    v-model="make"
                    :options="makes"
                    class="form-control"
                  ></multiselect>

                  <div class="input-group-text">
                    <b-button v-b-modal.modal-vmake variant="success"
                      >ADD
                    </b-button>
                  </div>
                  <div class="input-group-text">
                    <b-button
                      v-b-modal.modal-Edit
                      pill
                      variant="primary"
                      @click="getvehiclemake()"
                      ><i class="uil uil-refresh me-"></i>
                    </b-button>
                  </div>
                </div>
              </div>

              <b-modal
                id="modal-vmake"
                title="Add Vehicle Make"
                hide-footer
                size="lg"
                centered
              >
                <div class="row">
                  <div class="col-md-12">
                    <div class="card">
                      <div class="card-body">
                        <form @submit.prevent="handleSubmit">
                          <div class="row">
                            <div class="input-group">
                              <div class="input-group-text col-sm-4">
                                Enter Vehicle Make:
                              </div>

                              <input
                                class="form-control"
                                type="text"
                                placeholder="Name"
                                v-model="vehiclemake"
                              />
                            </div>
                          </div>
                        </form>
                      </div>
                    </div>
                  </div>
                </div>
                <!----@click="position(), $bvModal.hide('modal-1') --->
                <div class="row">
                  <div class="col-sm-9 mb-2"></div>
                  <div class="col-sm-3 mb-2">
                    <b-button variant="dark" @click="addmake()"
                      >Add Make</b-button
                    >
                  </div>
                </div>
              </b-modal>

              <div class="col-sm-12">
                <div class="input-group">
                  <div
                    :class="
                      vehicleDirection == 0
                        ? 'input-group-text textred col-sm-3'
                        : 'input-group-text col-sm-3'
                    "
                  >
                    Cargo:
                  </div>

                  <multiselect
                    style="font-size: 12px"
                    v-model="cargo"
                    :options="cargos"
                    class="form-control"
                  ></multiselect>
                  <div class="input-group-text">
                    <b-button v-b-modal.modal-AddCargo variant="success"
                      >ADD
                    </b-button>
                  </div>
                  <div class="input-group-text">
                    <b-button
                      v-b-modal.modal-Edit
                      variant="primary"
                      pill
                      @click="getcargo()"
                      >...
                    </b-button>
                  </div>
                </div>
              </div>

              <b-modal
                id="modal-AddCargo"
                title="Add Cargo"
                hide-footer
                size="lg"
                centered
              >
                <div class="row">
                  <div class="col-md-12">
                    <div class="card">
                      <div class="card-body">
                        <form @submit.prevent="handleSubmit">
                          <div class="row">
                            <div class="input-group">
                              <div class="input-group-text col-sm-4">
                                Cargo Name:
                              </div>

                              <input
                                class="form-control"
                                type="text"
                                placeholder="Name"
                                v-model="cargo_name"
                              />
                            </div>
                          </div>
                        </form>
                      </div>
                    </div>
                  </div>
                </div>
                <!----@click="position(), $bvModal.hide('modal-1') --->
                <div class="row">
                  <div class="col-sm-9 mb-2"></div>
                  <div class="col-sm-3 mb-2">
                    <b-button
                      v-show="!editmode"
                      variant="dark"
                      @click="addcargo()"
                      >Add Cargo</b-button
                    >
                  </div>
                </div>
              </b-modal>

              <div class="col-sm-12">
                <div class="input-group">
                  <div class="input-group-text textred col-sm-3">
                    Axle Type:
                  </div>
                  <multiselect
                    :disabled="!weighttaken ? disabled : ''"
                    ref="axlec"
                    v-model="axlec"
                    :options="axles"
                    class="form-control"
                    @input="getaxle()"
                  ></multiselect>
                  <b-button
                    v-b-modal.modal-Edit
                    variant="primary"
                    pill
                    @click="getaxles()"
                    >...
                  </b-button>
                </div>
              </div>

              <div class="col-sm-12">
                <div class="input-group">
                  <div
                    :class="
                      vehicleDirection == 0
                        ? 'input-group-text textred col-sm-3'
                        : 'input-group-text col-sm-3'
                    "
                  >
                    Transporter:
                  </div>
                  <multiselect
                    v-model="transporter"
                    :options="transporters"
                    class="form-control"
                  ></multiselect>
                  <div class="input-group-text">
                    <b-button v-b-modal.modal-AddTransporter variant="success"
                      >ADD
                    </b-button>
                  </div>
                  <b-button
                    v-b-modal.modal-Edit
                    pill
                    variant="primary"
                    @click="gettransporters()"
                    >...
                  </b-button>
                </div>
              </div>

              <b-modal
                id="modal-AddTransporter"
                title="Add Transporter"
                hide-footer
                size="lg"
                centered
              >
                <div class="row">
                  <div class="col-md-12">
                    <div class="card">
                      <div class="card-body">
                        <form @submit.prevent="handleSubmit">
                          <div class="row">
                            <div class="input-group">
                              <div class="input-group-text col-sm-4">
                                Transporters Name:
                              </div>

                              <input
                                class="form-control"
                                type="text"
                                placeholder="Name"
                                v-model="transporter_name"
                              />
                            </div>
                            <div class="input-group">
                              <div class="input-group-text col-sm-4">
                                Transporters Address:
                              </div>

                              <input
                                class="form-control"
                                type="text"
                                placeholder="Address"
                                v-model="transporter_address"
                              />
                              <div class="input-group">
                                <div class="input-group-text col-sm-4">
                                  Transporters Contact:
                                </div>

                                <input
                                  class="form-control"
                                  type="text"
                                  placeholder="Phone"
                                  v-model="transporter_contact"
                                />
                              </div>
                            </div>
                          </div>
                        </form>
                      </div>
                    </div>
                  </div>
                </div>
                <!----@click="position(), $bvModal.hide('modal-1') --->
                <div class="row">
                  <div class="col-sm-9 mb-2"></div>
                  <div class="col-sm-3 mb-2">
                    <b-button
                      v-show="!editmode"
                      variant="dark"
                      @click="addTransporter()"
                      >Add Transporter</b-button
                    >
                  </div>
                </div>
              </b-modal>

              <div class="col-sm-12">
                <div class="input-group">
                  <div
                    :class="
                      vehicleDirection == 0
                        ? 'input-group-text textred col-sm-3'
                        : 'input-group-text col-sm-3'
                    "
                  >
                    Origin:
                  </div>
                  <multiselect
                    v-model="origin"
                    :options="origins"
                    class="form-control"
                  ></multiselect>
                  <div class="input-group-text">
                    <b-button v-b-modal.modal-AddOrigin variant="success"
                      >ADD
                    </b-button>
                  </div>
                  <b-button
                    v-b-modal.modal-Edit
                    pill
                    variant="primary"
                    @click="getorigin()"
                    >...
                  </b-button>
                </div>
              </div>

              <div class="col-sm-12">
                <div class="input-group">
                  <div
                    :class="
                      vehicleDirection == 0
                        ? 'input-group-text textred col-sm-3'
                        : 'input-group-text col-sm-3'
                    "
                  >
                    Destination:
                  </div>
                  <multiselect
                    v-model="destination"
                    :options="origins"
                    class="form-control"
                  ></multiselect>

                  <b-button
                    v-b-modal.modal-Edit
                    variant="primary"
                    pill
                    @click="getorigin()"
                    >...
                  </b-button>
                </div>
              </div>

              <div class="col-sm-12">
                <div class="input-group">
                  <div
                    :class="
                      vehicleDirection == 0
                        ? 'input-group-text textred col-sm-3'
                        : 'input-group-text col-sm-3'
                    "
                  >
                    Relief Vehicle Reg:
                  </div>
                  <multiselect
                    @input="getprovehreg()"
                    v-model="reliefvehreg"
                    :options="vehinyard"
                    class="form-control"
                  ></multiselect>

                  <b-button
                    v-b-modal.modal-Edit
                    pill
                    variant="primary"
                    @click="getyardlist()"
                    >...
                  </b-button>
                </div>
              </div>

              <b-modal
                id="modal-AddOrigin"
                title="Add Origin/ Destination"
                hide-footer
                size="lg"
                centered
              >
                <div class="row">
                  <div class="col-md-12">
                    <div class="card">
                      <div class="card-body">
                        <form @submit.prevent="handleSubmit">
                          <div class="row">
                            <div class="input-group">
                              <div class="input-group-text col-sm-4">
                                Enter Origin/ Destination :
                              </div>

                              <input
                                class="form-control"
                                type="text"
                                placeholder="Name"
                                v-model="origin_name"
                              />
                            </div>
                          </div>
                        </form>
                      </div>
                    </div>
                  </div>
                </div>
                <!----@click="position(), $bvModal.hide('modal-1') --->
                <div class="row">
                  <div class="col-sm-9 mb-2"></div>
                  <div class="col-sm-3 mb-2">
                    <b-button
                      v-show="!editmode"
                      variant="dark"
                      @click="addOrigin()"
                      >Add Origin / Destination</b-button
                    >
                  </div>
                </div>
              </b-modal>

              <div class="col-sm-12">
                <div class="input-group">
                  <div class="input-group-text col-sm-3">Comment:</div>
                  <input
                    ref="comment"
                    style="font-size: 12px"
                    class="form-control"
                    list="permitOptions"
                    id="exampleDataList"
                    placeholder=""
                    v-model="comment"
                  />
                </div>
              </div>

              <!---  allgvw: 0,
      allsaw: 0,
      allgaw: 0,--->
            </div>
            <div class="card">
              <div class="card-body" style="font-weight: bold">
                <table v-show="reweigh > 0">
                  <th>
                    <span
                      :class="
                        reweigh > 0 ? 'badge bg-pill bg-dark font-size-20 ' : ''
                      "
                      role="alert"
                      :value="reweigh"
                    >
                      RE-WEIGH: {{ reweigh }}
                    </span>

                    <span
                      :class="
                        reweigh > 0 ? 'badge bg-pill bg-dark font-size-20' : ''
                      "
                      role="alert"
                      :value="reweigh"
                    >
                      GVW: {{ maingross }} [KG]
                    </span>
                    <span
                      :class="
                        reweigh > 0 ? 'badge bg-pill bg-dark font-size-20' : ''
                      "
                      role="alert"
                      :value="reweigh"
                    >
                      Excess
                      {{ mainload }}
                      :{{ mainexcess }}
                      [KG]
                    </span>
                    <span
                      :class="
                        reweighgvw > 0
                          ? 'badge bg-pill bg-danger font-size-20'
                          : ''
                      "
                      role="alert"
                      :value="reweigh"
                    >
                      GVW Excess
                      {{ reweighgvw }}
                      [KG]
                    </span>

                    <span
                      :class="
                        spreleaseno != ''
                          ? 'badge bg-pill bg-soft-success font-size-20'
                          : ''
                      "
                      role="alert"
                    >
                      {{ spreleaseno }}
                      <h6>{{ reasondescription }}</h6>
                    </span>

                    <span
                      :class="
                        prohibitionno != ''
                          ? 'badge bg-pill bg-soft-success font-size-20'
                          : ''
                      "
                      role="alert"
                      :value="prohibitionno"
                    >
                      {{ prohibitionno }}
                    </span>
                    <span
                      :class="
                        prohibitionno == ''
                          ? 'badge bg-pill bg-soft-danger font-size-20'
                          : ''
                      "
                      role="alert"
                      :value="prohibitionno"
                    >
                      {{ prohibitionorder }}
                    </span>
                    <span
                      :class="
                        reweigh > 0
                          ? 'badge bg-pill bg-success font-size-20 '
                          : ''
                      "
                      role="alert"
                      :value="reweigh"
                    >
                      Time Taken: {{ datediff }}
                    </span>
                    <!-- 
                  <div
                    :class="
                      casedetails[0].casedocid == ''
                        ? 'badge bg-pill bg-soft-success font-size-20'
                        : ''
                    "
                    role="alert"
                    :value="spreleaseno"
                  >
                    Not Prohibited
                  </div> -->
                  </th>
                  <th></th>
                </table>
                <div class="row">
                  <span
                    :class="
                      hasavalidpermit != ''
                        ? 'badge bg-pill bg-soft-success font-size-20'
                        : ''
                    "
                    role="alert"
                  >
                    {{ hasavalidpermit }}
                  </span>
                  <div class="col-md-12">
                    <!-- :class="{
                        'alert alert-primary': status === 'Overload',
                        'alert alert-success': status === 'Legal',
                        'alert alert-warning': status === 'Warning',
                        'alert alert-danger': status === 'Error',
                      }" -->
                    <div
                      :class="
                        status == 'Overload'
                          ? 'alert alert-danger'
                          : status == 'Error'
                          ? 'alert alert-danger'
                          : status == 'Warning'
                          ? 'alert alert-info'
                          : status == 'Legal'
                          ? 'alert alert-success'
                          : status == 'Warning'
                          ? 'alert alert-warning'
                          : 'alert alert-primary'
                      "
                      role="alert"
                    >
                      {{ status }}
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="col-md-4">
                    <b-button
                      v-show="weighttaken"
                      @click="gonext()"
                      variant="primary"
                      style="color: yellow"
                      class="mt-2 font-size-15"
                    >
                      Next
                      <i class="uil uil-balance-scale me-2"></i>
                    </b-button>
                  </div>
                  <div class="col-md-4">
                    <b-button
                      v-show="!weighttaken"
                      @click="goTonoplate()"
                      variant="danger"
                      style="color: yellow"
                    >
                      Cancel Weighing
                      <i class="uil uil-exclamation-octagon me-2"></i>
                    </b-button>
                  </div>
                  <div class="col-md-4">
                    <b-button
                      v-show="!weighttaken && cat == 'multideck'"
                      @click="goToVehicleDir()"
                      variant="primary"
                      style="color: yellow"
                    >
                      Take Vehicle Weight
                      <i class="uil uil-balance-scale me-2"></i>
                    </b-button>
                    <b-button
                      v-show="!weighttaken && cat == 'lswim' && finish"
                      @click="goToVehicleDir()"
                      variant="primary"
                      style="color: yellow"
                    >
                      Take Vehicle Weight
                      <i class="uil uil-balance-scale me-2"></i>
                    </b-button>
                  </div>
                </div>
                <!-- row end -->
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>

    <b-modal
      id="modal-AxleConfig"
      :title="modaltitle"
      hide-footer
      size="lg"
      centered
    >
      <div class="row">
        <div class="col-md-12">
          <div class="card">
            <div class="card-body">
              <form @submit.prevent="handleSubmit">
                <div class="row">
                  Enter reason why; <br />
                  You want to change axle configuration from
                  {{ vehicles.vehicle_axle }} to {{ this.axlec }} ?
                  <div class="input-group">
                    <div class="input-group-text col-sm-4">Reason :</div>

                    <input
                      class="form-control"
                      type="text"
                      placeholder="Reason"
                      v-model="reasonchange"
                    />
                  </div>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
      <!----@click="position(), $bvModal.hide('modal-1') --->
      <div class="row">
        <div class="col-sm-9 mb-2"></div>
        <div class="col-sm-3 mb-2">
          <b-button variant="dark" @click="configchange()"
            >Change Configurations</b-button
          >
        </div>
      </div>
    </b-modal>

    <Direction
      :gvwoverload="gvwoverload"
      :totalweight="totalweight"
      :gvwperm="gvwperm"
      :orderData="orderData"
      :orderData3="orderData3"
      :vehicleDirection="vehicleDirection"
      :tagged="tagged"
      :reliefvehreg="reliefvehreg"
      :casereliefdetails="casereliefdetails"
      :nplate="nplate"
      :axlec="axlec"
      :permit="permit"
      :trailer="trailer"
      :driver="driver"
      :make="make"
      :cargo="cargo"
      :transporter="transporter"
      :origin="origin"
      :destination="destination"
      :reweigh="reweigh"
      :comment="comment"
      :weights="weights"
      :cat="cat"
      :startdatetime="startdatetime"
      :WBTNO="WBTNO"
      :status="status"
      :weighdate="weighdate"
      :yard="yard"
      v-if="!isVisible"
      v-on:closeme="onchangefalse($event)"
      v-on:vehileupdate="vehiclesupt()"
      v-on:updatevDirection="updatevDirection($event)"
      v-on:addsp="addsp($event)"
      :nodata="nodata"
      :inyard="inyard"
      :reason="reason"
      :description="description"
      :granted="granted"
      :vDirection="vDirection"
      :modalname="modalname"
      :allgvw="allgvw"
      :allsaw="allsaw"
      :allgaw="allgaw"
      :userautosp="userautosp"
      :src2="src2"
      :src3="src3"
      :autospecialrelease="autospecialrelease"
    />
  </div>
</template>
<style scoped>
.localstyle {
  font-family: "digital-7";
  font-size: 24px;
  color: yellow;
}
.localstyletitle {
  font-size: 18px;
  color: #9cf502;
}

/* table {
  font-family: arial, sans-serif;
  border-collapse: collapse;
  width: 100%;
}

td, th {
  border: 1px solid #dddddd;
  text-align: left;
  padding: 8px;
}

tr:nth-child(even) {
  background-color: #dddddd;
}*/
.styled-table {
  border-collapse: collapse;
  margin: 25px 0;
  font-size: 11px;
  font-family: sans-serif;
  font-weight: bold;
  min-width: 400px;
  box-shadow: 0 0 20px rgba(0, 0, 0, 0.15);
}
.styled-table thead tr {
  background-color: lightgray;
  color: black;
  text-align: left;
}
.styled-table th,
.styled-table td {
  padding: 12px 15px;
  border: 1px solid gray;
}
.styled-table tbody tr {
  border-bottom: 1px solid #dddddd;
}

.styled-table tbody tr:nth-of-type(even) {
  background-color: #f3f3f3;
}

.styled-table tbody tr.active-row {
  font-weight: bold;
  color: yellow;
}
#formrow {
  font-size: 12px;
  padding: 8px 0px 8px 0px;
}
.Overload {
  background-color: red;
  color: yellow;
}
.Legal {
  background-color: green;
  color: yellow;
}
.textRight {
  text-align: right;
  font-size: 14px;
}
.textBig {
  text-align: center;
  font-weight: bold;
  font-size: 20px;
}
.changebg {
  background-color: #f7f6ebfb;
}
.textaxlesize {
  text-align: center;
  font-size: 13px;
}
.textred {
  color: red;
  font-weight: bold;
}
.Start {
  font-size: 20px;
  color: red;
  background-color: red;
  font-weight: bold;
  font-size: 20px;
}
</style>
