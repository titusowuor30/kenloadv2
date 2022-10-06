<script>
import Layout from "../../../views/layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";
import anprassist from "@/components/widgets/weigh/anprassist";

export default {
  page: {
    title: "ANPR Assist",
    meta: [
      {
        name: "description",
        content: appConfig.description,
      },
    ],
  },
  components: {
    Layout,
    PageHeader,
    anprassist,
  },
  data() {
    return {
      nplate: "",
      navbuttonVisible: true,
      name: "weigh",
      gvw: 0,
      gaw: 0,
      saw: 0,
      title: "ANPR Assist",
      items: [
        {
          text: "USER: Admin",
        },
        {
          text: "ANPR Assist",
          active: true,
        },
      ],
      orderDatanew: null,
      pl: "",
      orderData: [
        {
          id: "1",
          datetime: "2021-02-09 18:18:46.0",
          username: "Michael@micna.co.ke",
          saw: "2000",
          gaw: "2000",
          gvw: "2000",
        },
        {
          id: "2",
          datetime: "2021-02-09 18:18:46.0",
          username: "Michael@micna.co.ke",
          saw: "2000",
          gaw: "2000",
          gvw: "2000",
        },
        {
          id: "3",
          datetime: "2021-02-09 18:18:46.0",
          username: "Michael@micna.co.ke",
          saw: "2000",
          gaw: "2000",
          gvw: "2000",
        },
        {
          id: "4",
          datetime: "2021-02-09 18:18:46.0",
          username: "Michael@micna.co.ke",
          saw: "2000",
          gaw: "2000",
          gvw: "2000",
        },
      ],
      Weighbridge: "ATHIRIVER MOMBASA BOUND STATION",
      username: "michael@micna.co.ke",
      id: "",
      headers: null,
      uniqueCars: null,
      showme: true,
      totalRows: 1,
      currentPage: 1,
      perPage: 10,
      pageOptions: [
        1, 2, 5, 10, 25, 50, 100, 200, 300, 500, 1000, 2000, 3000, 4000, 5000,
        6000, 9000, 10000, 20000,
      ],
      roadauthoritys: ["KeNHA", "KURA"],
      filter: null,
      filterOn: [],
      sortBy: "id",
      sortDesc: true,
      roadauthority: "",
      pix: "",
      myindex: "",
      editmode: false,
      modaltitle: "Add",
      fields: [
        {
          key: "id",
          label: "#",
          sortable: true,
        },
        {
          key: "datetime",
          label: "Date Time",
          sortable: true,
        },
        {
          key: "username",
          label: "Username",
          sortable: true,
        },
        {
          key: "saw",
          label: "Single Axle",
          sortable: true,
        },
        {
          key: "gaw",
          label: "Group Axle",
          sortable: true,
        },
        {
          key: "gvw",
          label: "Gross Vehicle Weight",
          sortable: true,
        },
      ],
    };
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />

    <div>
      <anprassist
        :nplate="nplate"
        :navbuttonVisible="navbuttonVisible"
        :id="id"
        :anpr="nplate"
        :overweighting="overweighting"
        :wim="wim"
        :orderData="orderData"
      >
      </anprassist>
    </div>

    <!-- end row-->
  </Layout>
</template>

