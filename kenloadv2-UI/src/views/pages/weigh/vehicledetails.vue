<script>
import Layout from "../../layouts/main";
//import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

//import Deckweights from "@/components/widgets/weigh/deckweights";
import Vehicledetailsform from "@/components/widgets/weigh/vehicledetailsform";
// /import { tableData } from "../prosecution/dataAdvancedtable";

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
    //Deckweights,
    Vehicledetailsform,
  },
  created() {},
  data() {
    return {
      deck1: 0,
      deck2: 0,
      deck3: 0,
      deck4: 0,
      title: "Weight Capture",
      axleconf: "2A",
      permisibleweight: [8000, 10000],
      axlegroup: ["A", "B"],
      tyretype: ["S", "D"],
      deckweights: [
        this.$refs.child.deck1,
        this.$refs.child.deck2,
        this.$refs.child.deck3,
        this.$refs.child.deck4,
      ],

      tableData: [
        {
          group: 1,
          permissible: 1,
          tolerance: 1,
          actual: this.$refs.child.deck1,
          overload: 0,
          pdf: 0.0,
          result: "Legal",
        },
      ],
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
    <!--- <Deckweights ref="child" /> ! --->
    <Vehicledetailsform
      :deck1="deck1"
      :deck2="deck2"
      :deck3="deck3"
      :deck4="deck4"
    />
  </Layout>
</template>
