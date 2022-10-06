<script>
//import countTo from "vue-count-to";

import axios from "axios";
/**
 * Stat component
 */
export default {
  components: {
    //countTo,
  },
  data() {
    return {
      stoped: true,
      Warned: 0,
      Weighed: 0,
      Legal: 0,
      Overloaded: 0,
      tickets: 2.65,
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
              formatter: function() {
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
            columnWidth: "20%",
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
              formatter: function() {
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
  created() {
    setInterval(() => {
      if (this.stoped) {
        this.updatearrays();
      }
    }, 5000);
    this.updatearrays();
  },
  destroyed() {
    this.stoped = false;
  },
  mounted() {
    this.Overloaded = 0;
    this.Legal = 0;
    this.updatearrays();
  },
  methods: {
    updatearrays() {
      axios
        .get(window.$http + `weighbridgetransactions/max?`, {
          headers: window.$headers,
        })
        .then((response) => {
          //alert(response.data);
          this.Weighed = Number(response.data);

          let d = new Date();
          let year = d.getFullYear();
          let month = this.getmonth(d.getMonth());
          let date = d.getDate();
          date = this.getv(date);
          //month = this.getv(month);

          //const msec = d.getMilliseconds();
          // const datetime =
          //   date + "/" + month + "/" + year + " " + hour + ":" + min + ":" + sec;

          var fromdate =
            this.getmydate(date + "/" + month + "/" + year) + " 12:00:00 AM";
          var todate =
            this.getmydate(date + "/" + month + "/" + year) + " 11:59:59 PM";

          axios
            .get(
              window.$http +
                `weighbridgetransactions/max?fromdate=` +
                fromdate +
                "&todate=" +
                todate,
              {
                headers: window.$headers,
              }
            )
            .then((response) => {
              //alert(response.data);
              var yest = Number(response.data);
              var nu = (Number(yest) * 100) / Number(this.Weighed);
              this.tickets = nu.toFixed(2);
            });
        })
        .then(() => {
          axios
            .get(window.$http + `weighbridgetransactions/max?status=L`, {
              headers: window.$headers,
            })
            .then((response) => {
              //alert(response.data);
              this.Legal = Number(response.data);
            })
            .then(() => {
              axios
                .get(window.$http + `weighbridgetransactions/max?status=O`, {
                  headers: window.$headers,
                })
                .then((response) => {
                  //alert(response.data);
                  this.Overloaded = Number(response.data);
                });
            });
        });
      axios
        .get(window.$http + `weighbridgetransactions/max?status=W`, {
          headers: window.$headers,
        })
        .then((response) => {
          //alert(response.data);
          this.Warned = Number(response.data);
        });
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
  },
};
</script>

<template>
  <div class="row">
    <div class="col-md-6 col-xl-3">
      <div class="card">
        <div class="card-body">
          <div class="float-end mt-2">
            <apexchart
              class="apex-charts"
              dir="ltr"
              width="70"
              height="40"
              :options="chartOptions"
              :series="series"
            ></apexchart>
          </div>
          <div>
            <h4 class="mb-1 mt-1">
              <span data-plugin="counterup">
                <p class="mb-0" style="color: black">
                  {{ new Intl.NumberFormat().format(Weighed) }}
                </p>
                <!-- <countTo
                  :startVal="1000"
                  :endVal="Weighed"
                  :duration="2000"
                ></countTo> -->
              </span>
            </h4>
            <p class="text-muted mb-0">Number of Tickets</p>
          </div>
          <p class="text-muted mt-3 mb-0">
            <span class="text-success me-1">
              <i class="mdi mdi-arrow-up-bold me-1"></i>{{ tickets }}%
            </span>
            since yesterday
          </p>
        </div>
      </div>
    </div>
    <div class="col-md-6 col-xl-3">
      <div class="card">
        <div class="card-body">
          <div class="float-end mt-2">
            <apexchart
              class="apex-charts"
              type="radialBar"
              dir="ltr"
              width="45"
              height="45"
              :options="orderRadial"
              :series="orderseries"
            ></apexchart>
          </div>
          <div>
            <h4 class="mb-1 mt-1">
              <span data-plugin="counterup">
                <p class="mb-0" style="color: black">
                  {{ new Intl.NumberFormat().format(Legal) }}
                </p>
                <!-- <countTo
                  :startVal="100"
                  :endVal="Legal"
                  :duration="2000"
                ></countTo> -->
              </span>
            </h4>
            <p class="text-muted mb-0">Legal Vehicles</p>
          </div>
          <p class="text-muted mt-3 mb-0">
            <span class="text-danger me-1">
              <i class="mdi mdi-arrow-down-bold me-1"></i>0.82%
            </span>
            since last week
          </p>
        </div>
      </div>
    </div>
    <!-- end col-->

    <div class="col-md-6 col-xl-3">
      <div class="card">
        <div class="card-body">
          <div class="float-end mt-2">
            <apexchart
              class="apex-charts"
              type="radialBar"
              dir="ltr"
              width="45"
              height="45"
              :options="customerRadial"
              :series="customerseries"
            ></apexchart>
          </div>
          <div>
            <h4 class="mb-1 mt-1">
              <span data-plugin="counterup">
                <p class="mb-0" style="color: black">
                  {{ new Intl.NumberFormat().format(Overloaded) }}
                </p>
                <!-- <countTo
                  :startVal="1000"
                  :endVal="Overloaded"
                  :duration="2000"
                ></countTo> -->
              </span>
            </h4>
            <p class="text-muted mb-0">Overloaded Vehicles</p>
          </div>
          <p class="text-muted mt-3 mb-0">
            <span class="text-danger me-1">
              <i class="mdi mdi-arrow-down-bold me-1"></i>6.24%
            </span>
            since last week
          </p>
        </div>
      </div>
    </div>

    <!-- ////////////// -->

    <div class="col-md-6 col-xl-3">
      <div class="card">
        <div class="card-body">
          <div class="float-end mt-2">
            <apexchart
              class="apex-charts"
              type="radialBar"
              dir="ltr"
              width="45"
              height="45"
              :options="customerRadial"
              :series="customerseries"
            ></apexchart>
          </div>
          <div>
            <h4 class="mb-1 mt-1">
              <span data-plugin="counterup">
                <p class="mb-0" style="color: black">
                  {{ new Intl.NumberFormat().format(Warned) }}
                </p>
                <!-- <countTo
                  :startVal="1000"
                  :endVal="Warned"
                  :duration="2000"
                ></countTo> -->
              </span>
            </h4>
            <p class="text-muted mb-0">Warned Vehicles</p>
          </div>
          <p class="text-muted mt-3 mb-0">
            <span class="text-danger me-1">
              <i class="mdi mdi-arrow-down-bold me-1"></i>6.24%
            </span>
            since last week
          </p>
        </div>
      </div>
    </div>

    <!-- <div class="col-md-6 col-xl-3">
      <div class="card">
        <div class="card-body">
          <div class="float-end mt-2">
            <apexchart
              class="apex-charts"
              dir="ltr"
              width="70"
              height="40"
              :options="growthChartOptions"
              :series="series"
            ></apexchart>
          </div>
          <div>
            <h4 class="mb-1 mt-1">
              +
              <span data-plugin="counterup">12.58</span>%
            </h4>
            <p class="text-muted mb-0">Warned Vehicles</p>
          </div>
          <p class="text-muted mt-3 mb-0">
            <span class="text-success me-1">
              <i class="mdi mdi-arrow-up-bold me-1"></i>10.51%
            </span>
            since last week
          </p>
        </div>
      </div>
    </div> -->
    <!-- end col-->
  </div>
  <!-- end row-->
</template>
