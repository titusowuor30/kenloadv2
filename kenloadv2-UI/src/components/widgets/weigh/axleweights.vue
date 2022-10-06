<script>
//import countTo from "vue-count-to";
/**
 * Stat component
 */
export default {
  components: {
    //countTo,
  },
  props: {
    stoped: Boolean,
  },
  data() {
    return {
      weighb: "ATMBA",
      timerrun: 10,
      // stoped: true,
      weighbridgename: "",
      host: "",
      deck1: 0,
      lsweight: 0,
      weights: "",
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
  beforeDestroy() {
    this.stoped = false;
  },
  destroyed() {
    //this.timerrun = 1000000000;
  },
  mounted() {
    //this.deck1 = Number(6800);
    //this.deck2 = Number(11000);
    //this.weights = res;
  },
  methods: {
    changetimmer(timer) {
      // alert(timer);
      this.timerrun = Number(timer);
    },
    getweights() {
      this.changetimmer(10);

      const url = "http://127.0.0.1:3031";

      var theUrl = url + "/weights";
      this.host = url;

      setInterval(() => {
        const logFileText = async (file) => {
          const response = await fetch(file, { mode: "cors" });
          let text = (await response.text()) + "";
          this.weighbridgename = text.indexOf("*");
          //console.log(text);

          if (text.indexOf("*") <= 0) {
            this.weighbridgename = "Error!";
            this.deck1 = 0;
            this.deck2 = 0;
            this.deck3 = 0;
            this.deck4 = 0;
            return;
          }
          var arryW = text.split("*");
          this.weighbridgename = arryW[0];
          window.$weighstate = Number(arryW[1]);
          this.weighstatus = arryW[3];
          this.anprcheck2 = arryW[4];
          // if (arryW[4] == 1) {
          //   Swal.fire({
          //     title: "Please Wait !",
          //     html: "Getting image...", // add html attribute if you want or remove
          //     allowOutsideClick: false,
          //     showConfirmButton: true,
          //     willOpen: () => {
          //       Swal.showLoading();
          //     },
          //   });
          //   //this.genimganpr();
          //   this.$emit("genimganprs", [
          //     {
          //       scanned: true,
          //     },
          //   ]);

          //   var detail = {
          //     nplate: this.nplate,
          //     scanned: 1,
          //   };

          //   //"Content-Type": "application/x-www-form-urlencoded",
          //   var optionAxios = {
          //     headers: {
          //       "Content-Type": "application/x-www-form-urlencoded",
          //     },
          //   };

          //   axios
          //     .post(`http://127.0.0.1:3031/scan`, detail, optionAxios)
          //     .then(() => {
          //       //tt = false;
          //       /// this.anprcheck = 0;
          //     })
          //     .catch((e) => {
          //       Swal.fire("Send Weights" + e);
          //       console.log(e);
          //     });
          // }
          var wb = window.$stationcode + window.$weighb;
          if (wb == this.weighbridgename) {
            var deckweights = arryW[2].split("kg,");
            if (!isNaN(deckweights[0])) {
              this.deck1 = Number(deckweights[0].trim());
              if (this.deck1 > this.lsweight) {
                this.lsweight = this.deck1;
              }
            } else {
              this.deck1 = 0;
            }
            this.weights = arryW[1];
          }
        };

        if (this.deck1 > 0) {
          this.veicleref = "Vehicle on Scale";
        }

        if (this.stoped) {
          logFileText(theUrl);
        } else {
          this.timerrun = 100000000;
        }

        //this.weighbridgename = file.text();
        //this.weighbridgename = rawFile.open("GET", file, false);
      }, this.timerrun);
    },

    getweights2() {
      //this.changetimmer(100);
      var hos = window.location.host;
      if (hos.includes(":")) {
        //var hostip = hos.split(":");
        this.host = "94.237.94.104"; //hostip[0];
      } else {
        this.host = hos;
      }

      var href = window.location.href;

      var arr = href.split("/");
      var url = arr[0] + "//" + arr[2];
      //url = "http://192.168.4.111:3031";
      var theUrl = url + "/weights";
      this.host = url;
      setInterval(() => {
        const logFileText = async (file) => {
          const response = await fetch(file);
          let text = (await response.text()) + ",";
          //this.weighbridgename = file;
          //console.log(text);
          var arryW = text.split("*");
          this.weighbridgename = arryW[0];
          var wb = window.$stationcode + window.$weighb;
          if (wb == this.weighbridgename) {
            var deckweights = arryW[1].split("kg,");
            if (!isNaN(deckweights[0])) {
              this.deck1 = Number(deckweights[0]);
              if (this.deck1 > this.lsweight) {
                this.lsweight = this.deck1;
              }
            } else {
              this.deck1 = 0;
            }
            this.weights = arryW[1];
          }
        };
        //var theUrl = "http://" + this.host + ":3031/weights";
        //var file = "file:///C:/xampp/htdocs/indicators/" + this.weighb + ".txt";
        if (this.stoped) {
          logFileText(theUrl);
        } else {
          this.timerrun = 100000000;
        }

        //this.weighbridgename = file.text();
        //this.weighbridgename = rawFile.open("GET", file, false);
      }, this.timerrun);
    },
  },
  created() {
    //this.weights = "http://localhost:3000/test";
    //"Response" + your_url;
    //this.timerrun = 1000000000;
    this.getweights();
  },
};
</script>

<template>
  <div class="row">
    <div class="col-md-6 col-xl-6">
      <div class="card">
        <div class="card-body" id="localcarddesign">
          <div>
            <p class="mb-0" style="color: yellow">
              Axle :{{ weighbridgename }}
            </p>

            <hr style="color: white" />
            <p class="mb-0 mt-0">
              <span class="localstyletitle"> Actual (Kg) </span>
            </p>
          </div>
        </div>
      </div>
    </div>
    <div class="col-md-6 col-xl-6">
      <div class="card">
        <div class="card-body" id="localcarddesign">
          <div>
            <p class="mb-0" style="color: yellow">Axle Weight: [KG]</p>
            <hr style="color: white" />
            <h5 class="mb-1 mt-1">
              <span class="localstyle">
                <p class="mb-0" style="color: yellow">{{ deck1 }}</p>
                <!-- <countTo
                  :startVal="1000"
                  :endVal="deck1"
                  :duration="1200"
                ></countTo> -->
              </span>
            </h5>
          </div>
        </div>
      </div>
    </div>

    <!-- end col-->
  </div>
  <!-- end row-->
</template>
<style scoped>
.localstyle {
  font-family: "digital-7";
  font-size: 42px;
  color: #9cf502;
}
.localstyletitle {
  font-size: 17px;
  color: #9cf502;
}

#localcarddesign {
  background-color: black;
  padding: 5px;
  border-radius: 2px;
  border: 2px solid rgb(104, 104, 25);
}
</style>
