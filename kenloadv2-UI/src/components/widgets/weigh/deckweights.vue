<script>
//import countTo from "vue-count-to";
import Swal from "sweetalert2";
import axios from "axios";

import store from "@/state/store";

/**
 * Stat component
 */
export default {
  components: {
    //countTo,
  },
  props: {
    stoped2: Boolean,
    anprcheck: Number,
    scanned: Boolean,
  },
  data() {
    return {
      //weighb: "ATMBBOUNDA",
      nprcheck2: 0,
      weighstatus: "test",
      weighbridgename: "",
      timerrun: 1000,
      veicleref: "",
      host: "",

      deck1: 0,
      deck2: 0,
      deck3: 0,
      deck4: 0,
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
  updated() {
    this.$emit("updatedata", [
      {
        anprcheck: this.anprcheck2,
      },
    ]);
  },
  mounted() {},
  methods: {
    changetimmer(timer) {
      // alert(timer);
      this.timerrun = Number(timer);
    },
    updateweight() {
      this.$emit("updateweight", [
        {
          deck1: this.deck1,
          deck2: this.deck2,
          deck3: this.deck3,
          deck4: this.deck4,
        },
      ]);
    },
    getweights() {
      this.changetimmer(1000);
      //this.deck1 = Number(6800);
      //this.deck2 = Number(11000);
      //this.weights = res;
      var hos = window.location.host;
      if (hos.includes(":")) {
        //var hostip = hos.split(":");
        this.host = "94.237.94.104"; //hostip[0];
      } else {
        this.host = hos;
      }

      // var href = window.location.href;

      // var arr = href.split("/");
      // var url = arr[0] + "//" + arr[2];

      const url = "http://127.0.0.1:3031";

      //  var arr = href.split("/");
      // var hostip = arr[2].split(":");
      // var url = arr[0] + "//" + hostip[0] + ":3031/weights";

      var theUrl = url + "/weights";
      this.host = url;

      // var theUrl =
      //   "http://" +
      //   window.$localurl +
      //   "/kenload/dashboard/getweight.php?string=ATMBA";
      // const logWeights = async (file) => {
      //   //alert(theUrl);
      //   const response = await fetch(file);
      //   let text = await response.text();
      //   //alert(text);
      //   this.src2 = text;
      // };
      // logWeights(theUrl);
      //url = "94.237.94.104";
      //function isNumeric(value) {
      //return /^-?\d+$/.test(value);
      //}

      // fetch("c:/dcon/weights.txt")
      //   .then((response) => response.text())
      //   .then((data) => {
      //     // Do something with your data
      //     console.log(data);
      //   });
      //var theUrl = "http://94.237.94.104:3031/weights";
      //      var xmlHttp = new XMLHttpRequest();
      //      xmlHttp.open("GET", theUrl, false); // false for synchronous request
      //      xmlHttp.send(null);
      //      var weight = xmlHttp.responseText + ",";
      //      var arryW = weight.split("*");
      //      this.weighbridgename = arryW[0];
      //      if (this.weighb == this.weighbridgename) {
      //        var deckweights = arryW[1].split("kg,");
      //        //var deck1 = Number(deckweights[0]);
      //        //alert(isNumeric(deckweights[0]));
      //        if (!isNaN(deckweights[0])) {
      //          this.deck1 = deckweights[0];
      //        } else {
      //          this.deck1 = 0;
      //        }
      //        if (!isNaN(deckweights[1])) {
      //          this.deck2 = deckweights[1];
      //        } else {
      //          this.deck2 = 0;
      //        }
      //        if (!isNaN(deckweights[2])) {
      //          this.deck3 = deckweights[2];
      //        } else {
      //          this.deck3 = 0;
      //        }
      //        if (!isNaN(deckweights[3])) {
      //          this.deck4 = deckweights[3];
      //        } else {
      //          this.deck4 = 0;
      //        }
      //      }
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
          if (arryW[4] == 1) {
            Swal.fire({
              title: "Please Wait !",
              html: "Getting image...", // add html attribute if you want or remove
              allowOutsideClick: false,
              showConfirmButton: true,
              willOpen: () => {
                Swal.showLoading();
              },
            });
            //this.genimganpr();
            this.$emit("genimganprs", [
              {
                scanned: true,
              },
            ]);

            var detail = {
              nplate: this.nplate,
              scanned: 1,
            };

            //"Content-Type": "application/x-www-form-urlencoded",
            var optionAxios = {
              headers: {
                "Content-Type": "application/x-www-form-urlencoded",
              },
            };

            axios
              .post(`http://127.0.0.1:3031/scan`, detail, optionAxios)
              .then(() => {
                //tt = false;
                /// this.anprcheck = 0;
              })
              .catch((e) => {
                Swal.fire("Send Weights" + e);
                console.log(e);
              });
          }
          var wb = window.$stationcode + window.$weighb;
          if (wb == this.weighbridgename) {
            var deckweights = arryW[2].split("kg,");
            if (store.getters["screens/getBidirection"] == "Left") {
              if (!isNaN(deckweights[3])) {
                this.deck1 = Number(deckweights[3].trim());
              } else {
                this.deck1 = 0;
              }
              if (!isNaN(deckweights[2])) {
                this.deck2 = Number(deckweights[2].trim());
              } else {
                this.deck2 = 0;
              }
              if (!isNaN(deckweights[1])) {
                this.deck3 = Number(deckweights[1].trim());
              } else {
                this.deck3 = 0;
              }
              if (!isNaN(deckweights[0])) {
                this.deck4 = Number(deckweights[0].trim());
              } else {
                this.deck4 = 0;
              }
            } else {
              if (!isNaN(deckweights[0])) {
                this.deck1 = Number(deckweights[0].trim());
              } else {
                this.deck1 = 0;
              }
              if (!isNaN(deckweights[1])) {
                this.deck2 = Number(deckweights[1].trim());
              } else {
                this.deck2 = 0;
              }
              if (!isNaN(deckweights[2])) {
                this.deck3 = Number(deckweights[2].trim());
              } else {
                this.deck3 = 0;
              }
              if (!isNaN(deckweights[3])) {
                this.deck4 = Number(deckweights[3].trim());
              } else {
                this.deck4 = 0;
              }
            }
          }
        };
        //var file = "file:///C:/xampp/htdocs/indicators/" + window.$weighb + ".txt";
        if (this.deck2 > 0) {
          this.veicleref = "Vehicle on Deck";
        }
        this.updateweight();

        if (this.stoped2) {
          logFileText(theUrl);
        } else {
          this.timerrun = 100000000;
        }

        //this.weighbridgename = file.text();
        //this.weighbridgename = rawFile.open("GET", file, false);
      }, this.timerrun);
    },
  },
  beforeDestroy() {
    this.stoped2 = false;
  },
  created() {
    //this.weights = "http://localhost:3000/test";
    //"Response" + your_url;
    //alert(this.timerrun);
    this.getweights();
  },
  Unmounted() {
    //alert(this.timerrun);
    this.timerrun = 10000000;
  },
};
</script>

<template>
  <div class="row">
    <link
      rel="preload"
      as="font"
      href="/src/assets/fonts/modern_lcd-7.ttf"
      type="font/ttf"
      crossorigin="anonymous"
    />
    <div class="col-md-3 col-xl-1">
      <div class="card">
        <div class="card-body" id="localcarddesign">
          <div @click="changetimmer(1000)">
            <p class="mb-0" style="color: yellow">
              Platform :{{ weighbridgename }}
            </p>

            <hr style="color: white" />
            <p class="mb-0 mt-0">
              <span class="localstyletitle"> Actual (Kg) </span>
            </p>
          </div>
        </div>
      </div>
    </div>
    <div class="col-md-3 col-xl-1">
      <div class="card">
        <div class="card-body" id="localcarddesign">
          <div>
            <p class="mb-0 font-face" style="color: yellow">Status</p>
            <hr style="color: white" />
            <p class="mb-0 mt-0">
              <span class="localstyletitle">
                {{ weighstatus }}
              </span>
            </p>
          </div>
        </div>
      </div>
    </div>
    <div class="col-md-6 col-xl-2">
      <div class="card">
        <div class="card-body" id="localcarddesign">
          <div>
            <p class="mb-0" style="color: yellow">Deck 1</p>
            <hr style="color: white" />
            <h5 class="mb-0 mt-0">
              <span class="localstyle">
                <p class="mb-0" style="color: yellow">{{ deck1 }}</p>
                <!-- <countTo
                  :startVal="1000"
                  :endVal="deck1"
                  :duration="2000"
                ></countTo> -->
              </span>
            </h5>
          </div>
        </div>
      </div>
    </div>
    <div class="col-md-3 col-xl-2">
      <div class="card">
        <div class="card-body" id="localcarddesign">
          <div>
            <p class="mb-0" style="color: yellow">Deck 2</p>

            <hr style="color: white" />
            <h5 class="mb-0 mt-0">
              <span class="localstyle">
                <p class="mb-0" style="color: yellow">{{ deck2 }}</p>
                <!-- <countTo
                  :startVal="100"
                  :endVal="deck2"
                  :duration="2000"
                ></countTo> -->
              </span>
            </h5>
          </div>
        </div>
      </div>
    </div>
    <!-- end col-->

    <div class="col-md-6 col-xl-2">
      <div class="card">
        <div class="card-body" id="localcarddesign">
          <div>
            <p class="mb-0" style="color: yellow">Deck 3</p>
            <hr style="color: white" />
            <h5 class="mb-0 mt-0">
              <span class="localstyle">
                <p class="mb-0" style="color: yellow">{{ deck3 }}</p>
                <!-- <countTo
                  :startVal="1000"
                  :endVal="deck3"
                  :duration="2000"
                ></countTo> -->
              </span>
            </h5>
          </div>
        </div>
      </div>
    </div>
    <div class="col-md-6 col-xl-2">
      <div class="card">
        <div class="card-body" id="localcarddesign">
          <div>
            <p class="mb-0 font-face" style="color: yellow">Deck 4</p>
            <hr style="color: white" />
            <h5 class="mb-0 mt-0">
              <span class="localstyle">
                <p class="mb-0" style="color: yellow">{{ deck4 }}</p>
                <!-- <countTo
                  :startVal="1000"
                  :endVal="deck4"
                  :duration="2000"
                ></countTo> -->
              </span>
            </h5>
          </div>
        </div>
      </div>
    </div>

    <!-- end col-->

    <div class="col-md-3 col-xl-2">
      <div class="card">
        <div class="card-body" id="localcarddesign">
          <div>
            <p class="mb-0" style="color: yellow">Total (GVW) {{ weights }}</p>
            <hr style="color: white" />
            <h5 class="mb-0 mt-0" style="color: #9cf502">
              <span class="localstyle">
                <p class="mb-0" style="color: yellow">
                  {{
                    Number(deck1) +
                    Number(deck2) +
                    Number(deck3) +
                    Number(deck4)
                  }}
                </p>
                <!-- <countTo
                  :startVal="1000"
                  :endVal="
                    Number(deck1) +
                    Number(deck2) +
                    Number(deck3) +
                    Number(deck4)
                  "
                  :duration="500"
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
