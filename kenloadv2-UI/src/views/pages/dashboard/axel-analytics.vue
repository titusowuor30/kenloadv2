<script>
import countTo from "vue-count-to";
import axios from "axios";
/**
 * Sales-analytics component
 */
export default {
  components: {
    countTo,
  },
  mounted() {
    axios
      .get(
        window.$http +
          `WeighbridgeTransactions/max?fromdate=01/Oct/2021 12:00:00 AM&todate=31/Oct/2021 11:59:59 PM
   `,
        { headers: window.$headers }
      )
      .then((response) => {
        //alert(response.data);
        var d = Number(response.data);
        this.series[0].data = { d };
      });
  },
  data() {
    return {
      series: [
        {
          name: "Weighed",
          type: "column",
          data: [23, 11, 22],
        },
        {
          name: "Legal",
          type: "area",
          data: [44, 55, 41],
        },
        {
          name: "Overloaded",
          type: "line",
          data: [30, 25, 36],
        },
      ],
      chartOptions: {
        chart: {
          stacked: false,
          toolbar: {
            show: false,
          },
        },
        stroke: {
          width: [0, 2, 4],
          curve: "smooth",
        },
        plotOptions: {
          bar: {
            columnWidth: "30%",
          },
        },
        colors: ["#000", "#dfe2e6", "#f1b44c"],
        fill: {
          opacity: [0.85, 0.25, 1],
          gradient: {
            inverseColors: false,
            shade: "light",
            type: "vertical",
            opacityFrom: 0.85,
            opacityTo: 0.55,
            stops: [0, 100, 100, 100],
          },
        },
        labels: ["08/01/2021", "09/01/2021", "10/01/2021"],
        markers: {
          size: 0,
        },

        xaxis: {
          type: "datetime",
        },
        yaxis: {
          title: {
            text: "Points",
          },
        },
        tooltip: {
          shared: true,
          intersect: false,
          y: {
            formatter: function (y) {
              if (typeof y !== "undefined") {
                return y.toFixed(0) + " points";
              }
              return y;
            },
          },
        },
        grid: {
          borderColor: "#f1f1f1",
        },
      },
    };
  },
};
</script>

<template>
  <div class="col-xl-8">
    <div class="card">
      <div class="card-body">
        <div class="float-end">
          <b-dropdown
            variant="white"
            toggle-class="text-reset p-0"
            menu-class="dropdown-menu-end"
          >
            <template v-slot:button-content>
              <span class="fw-semibold">Sort By:</span>
              <span class="text-muted">
                Yearly
                <i class="mdi mdi-chevron-down ms-1"></i>
              </span>
            </template>
            <a class="dropdown-item" href="#">Monthly</a>
            <a class="dropdown-item" href="#">Yearly</a>
            <a class="dropdown-item" href="#">Weekly</a>
          </b-dropdown>
        </div>
        <h4 class="card-title mb-4">Axle-Load Analytics</h4>

        <div class="mt-1">
          <ul class="list-inline main-chart mb-0">
            <li class="list-inline-item chart-border-left me-0 border-0">
              <h3 class="text-primary">
                <span data-plugin="counterup">
                  <countTo
                    :startVal="1"
                    :endVal="2371"
                    :duration="2000"
                  ></countTo>
                </span>
                <span class="text-muted d-inline-block font-size-15 ms-3"
                  >Total Vehicles</span
                >
              </h3>
            </li>
            <li class="list-inline-item chart-border-left me-0">
              <h3>
                <span data-plugin="counterup">
                  <countTo
                    :startVal="1"
                    :endVal="258"
                    :duration="2000"
                  ></countTo>
                </span>
                <span class="text-muted d-inline-block font-size-15 ms-3"
                  >Weighed Vehicles</span
                >
              </h3>
            </li>
            <li class="list-inline-item chart-border-left me-0">
              <h3>
                <span data-plugin="counterup">3.6</span>%
                <span class="text-muted d-inline-block font-size-15 ms-3"
                  >Conversation ratio</span
                >
              </h3>
            </li>
          </ul>
        </div>

        <div class="mt-3">
          <apexchart
            type="line"
            class="apex-charts"
            dir="ltr"
            height="339"
            :options="chartOptions"
            :series="series"
          ></apexchart>
        </div>
      </div>
    </div>
  </div>
</template>
