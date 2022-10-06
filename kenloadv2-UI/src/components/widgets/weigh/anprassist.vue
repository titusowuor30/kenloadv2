<script>
import Swal from "sweetalert2";
import axios from "axios";

import Deckimages from "@/components/widgets/weigh/deckimages";
/**
 * Stat component
 */
export default {
  props: {
    nplate: String,
    navbuttonVisible: Boolean,
    id: Number,
    anpr: String,
    overweighting: String,
    wim: String,
    orderData: Array,
    src2: String,
    src3: String,
  },
  components: {
    Deckimages,
  },
  data() {
    return {
      reason: "",
    };
  },
  updated() {
    this.$emit("updatenplate", [
      {
        nplate: this.nplate,
      },
    ]);
  },
  methods: {
    goToVehicleReg() {
      //this.$nplate = this.nplate;
      //alert(this.name + "-" + this.nplate);

      if (this.nplate.trim() == "") {
        Swal.fire("Please enter Vehicle Registration Number!");
        return;
      }

      if (this.nplate.length < 4) {
        Swal.fire("Please enter correct Vehicle Registration Number !");
        return;
      }
    },
    updateplate(events) {
      this.nplate2 = events[0].nplate;
    },
    handleSubmit() {
      console.log("Error on submit");
    },
    focusInput() {
      //const input = this.$refs.nplate;
      this.$refs.nplate.focus();
      //this.elments[1].focus();
    },
    save() {
      if (this.nplate.trim() == "") {
        Swal.fire("Please enter Vehicle Registration Number!");
        return;
      }

      // if (this.nplate.length < 4) {
      //   Swal.fire("Please enter correct Vehicle Registration Number !");
      //   return;
      // }
      //alert(this.orderData.length);

      //alert(this.orderData[0].anprassist);
      //var str = "<div> Registration:" + this.nplate + "</div><br>";
      // Swal.fire({
      //   title: "Do you want to save ANPR Assist Registration " + "?",
      //   html: "<pre>" + str + "</pre>",
      //   allowOutsideClick: false,
      //   allowEscapeKey: false,
      //   icon: "info",
      //   showCancelButton: true,
      //   confirmButtonColor: "#006400",
      //   cancelButtonColor: "#f46a6a",
      //   confirmButtonText: "Yes, Save it!",
      // }).then((result) => {
      //   if (result.value) {
      //window.location.reload();
      //alert(this.mdate);
      //var datet = this.mydate(new Date(this.mdate));

      this.orderData[0].anprassist = this.nplate;
      this.orderData[0].lp = this.nplate;
      //alert(this.orderData[0].lp);
      Swal.fire({
        title: "Kenload V2. Please Wait!",
        html: "Saving data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      axios
        .put(
          window.$http + `virtualtickets/` + this.orderData[0].id,
          this.orderData[0],
          { headers: window.$headers }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          if (this.orderData[0].wbrg_ticket_no != "") {
            this.updatehswim();
          }
          console.log(response.data);
          Swal.fire({
            position: "center",
            icon: "success",
            title: "Your work has been saved",
            showConfirmButton: false,
            timer: 1500,
          }).then(() => {
            this.clearvalues();
            this.updatearray();
          });
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "" + e,
            showConfirmButton: true,
          }).then(() => {
            Swal.close();
          });
        });
      // }
      // });
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
    getv(val) {
      if (val < 10) {
        val = "0" + val;
      }
      return val;
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
    updatehswim() {
      //alert(this.nodata);
      var d = new Date(this.orderData[0].date_time_local);
      var td = new Date(this.orderData[0].date_time_local);
      //alert(d);
      var fromdate = this.mydatenew(d);
      let fhour = d.getHours();

      //alert("" + fromdate);

      d.setHours(d.getHours() + 1);

      var todate = this.mydatenew(d);

      var ampm = "AM";
      let thour = d.getHours();

      var fromhour = this.mydatehour(td);
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
      console.log(
        window.$http +
          `WeighbridgeTransactions/search?veh_reg=` +
          this.orderData[0].lp +
          `&fromdate=` +
          fromdate +
          `&todate=` +
          todate
      );
      axios
        .get(
          window.$http +
            `WeighbridgeTransactions/search?veh_reg=` +
            this.orderData[0].lp +
            `&fromdate=` +
            fromdate +
            `&todate=` +
            todate,
          {
            headers: window.$headers,
          }
        )
        .then((respvirt) => {
          console.log(respvirt.data);
          this.orderData[0].wbrg_ticket_no = respvirt.data[0].wbrg_ticket_no;
          //respvirt.data[0].axle_conf = this.axlec;
          //console.log("respvirt.data[0].id" + respvirt.data[0].id);
          axios
            .put(
              window.$http + `virtualtickets/` + this.orderData[0].id,
              this.orderData[0],
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
  },
  mounted() {
    this.focusInput();
  },
};
</script>

<template>
  <div>
    <Deckimages :src2="src2" :src3="src3" />

    <div class="row">
      <div class="col-md-12 col-xl-12">
        <form
          class="row row-cols-lg-auto gx-3 gy-2 align-items-center"
          @submit.prevent="goToVehicleReg"
        >
          <div class="col-12" style="width: 50%">
            <div class="card">
              <div class="card-body">
                <div class="input-group">
                  <div class="input-group-text">Vehicle Registration No:</div>
                  <input
                    ref="nplate"
                    type="text"
                    class="form-control"
                    id="specificSizeInputGroupUsername"
                    placeholder="No Plate"
                    v-model="nplate"
                  />
                  <b-button
                    variant="dark"
                    style="background-color: black; color: yellow"
                    @click="save()"
                  >
                    Save
                    <i class="uil uil-save ms-2"></i>
                  </b-button>
                </div>

                <div class="input-group" v-show="navbuttonVisible">
                  <div class="input-group-text">
                    Reson why image not captured:
                  </div>
                  <input
                    v-show="navbuttonVisible"
                    ref="nplate"
                    type="text"
                    class="form-control"
                    id="specificSizeInputGroupUsername"
                    placeholder="No Plate"
                    v-model="reason"
                  />
                </div>
              </div>
            </div>
            <div class="input-group">
              <div class="col-sm-3">
                <b-button
                  v-show="navbuttonVisible"
                  class="uil-backward"
                  variant="dark"
                  style="background-color: black; color: yellow"
                >
                  First
                </b-button>
              </div>
              <div class="col-sm-3">
                <b-button
                  v-show="navbuttonVisible"
                  variant="dark"
                  style="background-color: black; color: yellow"
                  ><i class="uil uil-left-arrow-from-left ms-2"></i>
                  Previous
                </b-button>
              </div>
              <div class="col-sm-3">
                <b-button
                  v-show="navbuttonVisible"
                  variant="dark"
                  style="background-color: black; color: yellow"
                >
                  Next
                  <i class="uil uil-arrow-to-right ms-2"></i>
                </b-button>
              </div>
              <div class="col-sm-3">
                <b-button
                  v-show="navbuttonVisible"
                  variant="dark"
                  style="background-color: black; color: yellow"
                >
                  Last
                  <i class="uil uil-forward ms-2"></i>
                </b-button>
              </div>
            </div>
          </div>
        </form>
      </div>

      <!-- End Form Layout -->
    </div>
  </div>
</template>
