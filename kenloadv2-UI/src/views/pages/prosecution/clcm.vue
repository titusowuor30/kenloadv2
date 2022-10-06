<template>
  <Layout>
    <PageHeader :title="title" :items="items" />

    <h6 style="margin-left: 10px"><strong>KeNHA/MTCE/ALC/F3</strong></h6>
    <div align="center">
      <img
        src="@/assets/images/prosecution/Coatofarmskenya.png"
        style="width: 90px; height: 85px; margin-top: -25px"
      />
    </div>
    <div align="center">
      <img src="@/assets/images/prosecution/kenhalogo.png" />
      <p>
        <font size="1">
          Blueshield Towers, Hospital Road, Upper Hill P.O. Box 49712 - 00100
          Nairobi <br />
          Tel: 020-801342 Email:dg@kenha.co.ke / info@kenha.co.ke
          Website:www.kenha.co.ke
        </font>
      </p>
    </div>
    <div>
      <p align="right" style="margin-right: 10px">
        <b>NO.{{ prohino }}</b>
      </p>
    </div>
    <div>
      <p align="center" style="font-size: 23px">
        <br /><strong>INTERNAL MEMO</strong> <br />
      </p>
      <p>----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------</p>
    </div>
    <h3 align="left"><b>FROM: </b>{{ weighbridge }} </h3>
    <h3 align="left"><b>THROUGH: </b>Officer Commanding Station - {{ weighbridge }} </h3>
    <h3 align="left"><b>TO: </b>Driver details </h3>
    <h3 align="left"><b>REF: </b> {{ prohino }}</h3>
    <h3 align="left"><b>DATE: </b> {{date}} {{time}} </h3>
    <br>
    <p>----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------</p>
    <div style="margin: 10px 50px 10px">
      <p>
        <h3 align="left"><b><u>RE : ORDER TO DISTRIBUTE EXCESS WEIGHT ON VEHICLE</u> </b></h3>
      <p>
        <br />
        Vehicle Registration Number of Transporter {{Transporter}} address {{address}} is hereby ordered to distribute {{excessload}}
         within the {{weighbridge}} station (impound yard)
        <br />
      </p>
      <p style="margin-right: 50px" align="left">
      <br><br>Prohibition Order Number: <b> {{prohino}} </b>
      <br>Weigh Ticket Number: <b> {{prohino}} </b>
      <br />Weigh Date: <b>{{ date }} </b> Time:<b>{{ time }}</b>
      <br>
    </p>
    <p style="margin-left: 50px"></p>
    <h4 align="right">
      Signature: ..........................................
      <br><br>
      Name: ...............................................
      <br>
      <b>OFFICER IN CHARGE
        <br>
        {{weighbridge}}
        </b>
    </h4>
    </div>
    
  </Layout>
</template>

<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

export default {
  name: "lcm",
  page: {
    title: "lcm",
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
  },
  data() {
    return {
      title: "Conditional Load Correction Memo",
      items: [
        {
          text: "USER: COP",
        },
        {
          text: "Load Correction Memo",
          active: true,
        },
      ],
      prohino: "PR00004",
      id: 1,
      vehreg: "KBA857V",
      police: "Michael Police",
      road: "Mlolongo Highway",
      weighbridge: "Athiriver Mombasa Bound",
      excessload: "1000",
      ticketnumber: "WBT0007",
      date: "15/May/2021",
      time: "08:00:00",
      /** prosecuteruser="Prosecutername", */
    };
  },
};
</script>

<style>
</style>