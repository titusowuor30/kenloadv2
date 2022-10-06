<script>
import Layout from "../../layouts/main";
//import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";
import Deckweights from "@/components/widgets/weigh/axleweights";
import Noplatecapture from "@/components/widgets/weigh/vehiclenoplatelswim";
import Deckimages from "@/components/widgets/weigh/deckimages";


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
    Noplatecapture,
    Deckweights,
    Deckimages,
  
  
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
    <Deckweights />
    <Deckimages/>
    <Noplatecapture/>
  </Layout>
</template>
