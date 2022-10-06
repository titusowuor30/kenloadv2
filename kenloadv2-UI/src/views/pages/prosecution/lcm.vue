<template>
  <Layout>
    <PageHeader :title="title" :items="items" />

    <h6 style="margin-left: 10px mr-3"><strong>KeNHA/MTCE/ALC/F3</strong></h6>
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
    <h3 align="left"><b>FROM: </b>Manager - {{ weighbridge }} </h3>
    <h3 align="left"><b>THROUGH: </b>Officer Commanding Station - {{ weighbridge }} </h3>
    <h3 align="left"><b>TO: </b>Driver details </h3>
    <h3 align="left"><b>REF: </b> {{ prohino }}</h3>
    <h3 align="left"><b>DATE: </b> {{date}} {{time}} </h3>
    <br>
    <p>----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------</p>
    <div style="margin: 10px 50px 10px">
      <p>
        <h3 align="left"><b><u>RE : ORDER TO OFFLOAD / DISTRIBUTE EXCESS WEIGHT FROM VEHICLE</u> </b></h3>
      <p>
        <br />
        Vehicle Registration Number {{registration}} of Transporter {{transporter}} address {{address}} is hereby ordered to distribute or to offload {{excessload}} KG (excess weight)
         to relief truck Registration Number {{registration_relief}} within the {{weighbridge}} station (impound yard) having paid overload fee of Kshs. {{overload_fee}} (Driver: {{driver_fee}}
          , Transporter: {{transporter_fee}}, Loader: {{loader_fee}}, Parking Fee: {{parking_fee}}). 
        <br />
      </p>
      <table width="70%">
        <tr>
          <td>
            <strong>Receipt No</strong>
          </td>
          <td>
            <strong>Driver</strong>
          </td>
          <td>
            <strong>Transporter</strong>
          </td>
          <td>
            <strong>Loader</strong>
          </td>
          <td>
            <strong>Parking Fee</strong>
          </td>
          <td>
            <strong>Sub-Total</strong>
          </td>
        </tr>
        <tr>
          <td>
            {{prohino}}
          </td>
          <td>
            {{driver_fee}}
          </td>
          <td>
            {{transporter_fee}}
          </td>
          <td>
            {{loader_fee}}
          </td>
          <td>
            {{parking_fee}}
          </td>
          <td>
            {{overload_fee}}
          </td>
        </tr>
        <tr>
          <td>
            
          </td>
          <td>
            
          </td>
          <td>
            
          </td>
          <td>
            
          </td>
          <td>
            <strong>Total</strong>
          </td>
          <td>
            <strong> {{overload_fee}} </strong>
          </td>
        </tr>
      </table>
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
      title: "Load Correction Memo",
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