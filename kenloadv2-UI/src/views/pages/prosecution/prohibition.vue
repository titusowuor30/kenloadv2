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
        <br /><strong>THE TRAFFIC ACT</strong> <br /><strong>(cap. 403)</strong
        ><br />
      </p>
    </div>
    <h3 align="center">
      <b
        >ORDER TO REMOVE VEHICLE FROM ROAD OR PUBLIC PLACE,TO OFFLOAD EXCESS
        WEIGHT,OR TO EFFECT REPAIRS</b
      >
    </h3>
    <div style="margin: 10px 50px 10px">
      <p>
        <br />
        {{ id }} To the owner or driver of Vehicle Registration No.
        <b>{{ vehreg }}</b> make:<br />
      </p>
      <p>
        <br />1. I <b>{{ police }}</b
        >, a Police Officer/Licensing Officer/Inspector acting under Section 106
        and 107 of the Traffic Act, have inspected and found the above vehicle
        is being used along road <b>{{ road }}</b> at<b>{{ weighbridge }} </b>
        in contravention of Section 55,56 and 58 of the Traffic Rules in respect
        of loading and construction of the vehicle and I ORDER that the vehicle
        shall not be further until the excess load of <b>{{ excessload }}</b> is
        properly distributed or offloaded as per weigh ticket number(s)<b>{{
          ticketnumber
        }}</b
        ><br />
      </p>
      <p>
        <br />For this purpose,I further order that the vehicle shall be moved
        to <b>{{ weighbridge }}</b> and there detained until this is compiled
        with in accordance with the traffic act<br />
      </p>
      <p>
        <br />2. This order is without prejudice to any proceedings that may be
        brought for the contravention with the Traffic Act or the Traffic
        Rules<br />
      </p>
      <p><br />3. It is an offense to disobey this order<br /></p>
      <p>
        <br />4. Attention is drawn to subsection (3) of Section 106 of the
        Traffic Act that any loss or damage incurred as a result of this order
        shall not be the responsibility of the police officer,licensing officer
        or inspector<br />
      </p>
    </div>
    <p style="margin-right: 50px" align="right">
      <br />Date:<b>{{ date }} </b> Time:<b>{{ time }}</b>
    </p>
    <p style="margin-left: 50px"></p>
    <p>
      <br />Prosecution Clerk:<b>{{ prosecuteruser }}</b
      >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Prosecution
      Clerk sign:......................................
    </p>
  </Layout>
</template>

<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import html2canvas from 'html2canvas'
import jspdf from 'jspdf'

export default {
  name: "Prohibition",
  page: {
    title: "Prohibition",
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
      title: "Prohibition",
      items: [
        {
          text: "USER: COP",
        },
        {
          text: "Prohibition Order",
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
  mounted() {
    // to PDF function
    this.pageToPDF();
  },
  methods: {
    /**
     * PDF Function
     */
    pageToPDF: function() {
      const d = new Date();
      const year = d.getFullYear();
      const month = d.getMonth();
      const date = d.getDate();

      const hour = d.getHours();
      const min = d.getMinutes();
      const sec = d.getSeconds();
      const msec = d.getMilliseconds();
      const filename =
        year +
        "-" +
        month +
        "-" +
        date +
        "-" +
        hour +
        "-" +
        min +
        "-" +
        sec +
        "-" +
        msec;

      window.html2canvas = html2canvas;
      
      // var doc = new jspdf("l", "pt", "a4"); // landscape
      var doc = new jspdf("p", "pt", "a4"); // potrait
      doc.setProperties({
        // title: 'title', // Change here
        title: filename, // Change here
        subject: 'subject', // Change here
        author: 'author', // Change here
        creator: 'creator' // Change here
      });

      var newdiv = document.querySelector("#app").cloneNode(true);
      newdiv.style.width = "80vh"; // for landscape
      newdiv.style.width = "560px"; //for potrait
      
      newdiv.style.marginTop = "15px";
      newdiv.style.marginLeft = "15px";
      newdiv.style.marginBottom = "15px";
      newdiv.style.marginRight = "15px";
      newdiv.style.fontSize = "10px";
      // newdiv.style.background = "red";

      newdiv.querySelector("#page-topbar").remove();
      newdiv.querySelector("#preloader").remove();
      newdiv.querySelector(".main-content").style.marginTop = "0px";
      newdiv.querySelector(".page-content").style.marginTop = "-50px";

    doc.html(
      newdiv,
      {
        callback: function(pdf) {
          // pdf.output('save', filename+'.pdf');
          window.open(pdf.output('bloburl'), 'Frame', '_blank');
        }
      }
    );

      /*
      doc.html(document.querySelector("#app"), {
        callback: function(pdf){
          // pdf.save(filename+".pdf");
          window.open(pdf.output('bloburl'), 'Frame', '_blank');
        },
      });
      */
    },
  }
};
</script>

<style>
</style>