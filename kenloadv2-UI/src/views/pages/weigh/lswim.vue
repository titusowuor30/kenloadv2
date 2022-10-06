<script>
import Layout from "../../layouts/main";
//import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import Axleweights from "@/components/widgets/weigh/axleweights";
import Vehicledetailsformaxle from "@/components/widgets/weigh/vehicledetailsformaxle";

export default {
  page: {
    title: "Weigh",
    meta: [
      {
        name: "description",
        content: appConfig.description,
      },
    ],
  },
  components: {
    Layout,
    Axleweights,
    Vehicledetailsformaxle,

  
  },
  data() {
    return {
      title: "Weight Capture",
      items: [
        {
          text: "USER: Cow",
        },
        {
          text: "Weigh",
          active: true,
        },
      ],
    };
  },
};
</script>

<template>
  <Layout>
    <Axleweights />
    <Vehicledetailsformaxle/>

  </Layout>
</template>
