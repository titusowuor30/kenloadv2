<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";

import WeighVehicleDirection from "../../../components/widgets/weigh/vehicle-direction";
export default {
  page: {
    title: "Weigh Vehicle Direction",
  },
  components: {
    Layout,
    PageHeader,
    WeighVehicleDirection,
  },
  methods: {
    goTonoplate() {
      this.$router.push("/weigh");
    },
  },
  created() {
    this.nplate = this.$route.params.nplate;
    //alert(this.nplate);
    this.permit = this.$route.params.permit;
    this.trailer = this.$route.params.trailer;
    this.driver = this.$route.params.driver;
    this.make = this.$route.params.make;
    this.cargo = this.$route.params.cargo;
    this.axle = this.$route.params.axle;
    this.transporter = this.$route.params.transporter;
    this.origin = this.$route.params.origin;
    this.destination = this.$route.params.destination;
    this.reweigh = this.$route.params.reweigh;
    this.weights = this.$route.params.weights;

    //alert(this.nplate);
  },
  data() {
    return {
      nplate: "",
      permit: "",
      trailer: "",
      driver: "",
      make: "",
      cargo: "",
      axle: "",
      transporter: "",
      origin: "",
      destination: "",
      reweigh: "",
      weights: [
        {
          deck1: "",
          deck2: "",
          deck3: "",
          deck4: "",
        },
      ],
      title: "Vehicle Direction",
    };
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" />
    <WeighVehicleDirection
      v-bind:nplate="nplate"
      v-bind:axle="axle"
      v-bind:permit="permit"
      v-bind:trailer="trailer"
      v-bind:driver="driver"
      v-bind:make="make"
      v-bind:cargo="cargo"
      v-bind:transporter="transporter"
      v-bind:origin="origin"
      v-bind:destination="destination"
      v-bind:reweigh="reweigh"
      v-bind:weights="weights"
    />
  </Layout>
</template>