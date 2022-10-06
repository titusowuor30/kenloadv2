<template>
  <Layout>
    <PageHeader :title="title" :items="items" />

    <div align="center">
      <img
        src="@/assets/images/prosecution/kenyapolice.png"
        style="width: 207px; height: 137px"
      />
    </div>
    <div align="left">
      <h6><strong>POLICE 5</strong></h6>
    </div>
    <div align="center">
      <h3><strong>THE KENYA POLICE</strong></h3>
    </div>
    <div align="center">
      <h5><strong>CHARGE SHEET</strong></h5>
    </div>
    <p align="right" style="margin-right: 50px; margin-top: -50px">
      POLICE CASE NO:..................................<br /><br />
      DATE TO COURT:...................................<br /><br />COURT FILE
      NO:...................................
    </p>
    <p align="left" style="margin-left: 50px; margin-top: -80px">
      O.B. NO:<br /><br />CHARGE SHEET No:<b>{{ chargesheet }} </b>
    </p>

    <table class="table" cellspacing="2" width="100%">
      <tr>
        <td>
          Full <br />
          Name
        </td>
        <td>Surname or Father's</td>
        <td>Identity or Passport</td>

        <td>Gender (sex) M/F</td>

        <td>Nationality</td>

        <td>Apparent <br />Age</td>

        <td>Address</td>
      </tr>

      <tr>
        <td>
          <b>{{ dname }}</b>
        </td>
        <td>
          <b>{{ dsurname }}</b>
        </td>

        <td>
          <b>{{ driverid }}</b>
        </td>

        <td>
          <b>{{ dgender }}</b>
        </td>

        <td>
          <b>{{ dnationality }}</b>
        </td>

        <td>
          <b>{{ dage }}</b>
        </td>

        <td>
          <b>{{ daddress }}</b>
        </td>
      </tr>

      <tr>
        <td>CHARGE</td>

        <td colspan="6">
          USING A MOTOR VEHICLE ON A ROAD WITH A LOAD GREATER THAN THE LOAD
          SPECIFIED BY SEC 58(1) AND RULE 41(2) OF THE LEGAL NOTICE NUMBER
          93/2013 DATED 2013/06/01 OF THE TRAFFIC ACT 403 LAWS OF KENYA
        </td>
      </tr>

      <tr>
        <td>PARTICULARS <br />OF OFFENCE (See second Schedule of C.P.C)</td>

        <td colspan="6">
          <b>{{ dname }}</b> being the Driver <b>{{ oname }}</b> being the Owner
          of motor vehicle Reg. No.
          <strong>
            {{ vehreg }}
            <br />Ticket No. <b> {{ cid }} </b> carrying
            <b>{{ totalweight }}) kg</b> instead of legal limit of
            <b> {{ permissible }} kg</b>, an overload of Gross Vehicle
            Weight(GVW) <b> {{ excessload }} </b>kg on
            <strong> {{ date }} </strong> at about
            <strong> {{ time }}</strong> along
            <strong> {{ weighbridge }} </strong> class
            <strong> {{ configuration }} </strong></strong
          >
          makes class <strong>{{ vmake }}</strong> USED a motor vehicle on the
          road with greater load than the specified on Gross Vehicle Weight(GVW)
          by {{ charg }} <br />as per attached Kenya National Highways Authority
          Axle Weighing
        </td>
      </tr>
      <tr>
        <td>Is Accused Arrested</td>
        <td>Date of Arrest</td>
        <td>Without or With Warrant</td>
        <td>Date Apprehension Report to Court</td>
        <td>Bond or Bail and Amount</td>
        <td colspan="2">Is Application made for summons to issue</td>
      </tr>
      <tr>
        <td>
          <br />
          <br />
          <br /><br />
        </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td colspan="2">&nbsp;</td>
      </tr>
      <tr>
        <td>Remanded or</td>
        <td colspan="6">
          <br />
          <br />
          <br />
        </td>
      </tr>
      <tr>
        <td>Complainant and Address</td>
        <td colspan="6">
          <b>REP:THROUGH {{ weighbridge }} POLICE STATION</b>
        </td>
      </tr>
      <tr>
        <td></td>
        <td colspan="6">
          <br />
          1.............................................................................&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;6.............................................................................<br /><br />
          2.............................................................................&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;7.............................................................................<br /><br />
          3.............................................................................&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;8.............................................................................<br /><br />
          4.............................................................................&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;9.............................................................................<br /><br />
          5.............................................................................&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;10.............................................................................
        </td>
      </tr>
      <tr>
        <td>Sentence Court and Date</td>
        <td colspan="6">
          <br />
          .....................................................................................................................................................................................................<br /><br />
          .....................................................................................................................................................................................................<br /><br />
          COURT NAME on ............................. If fine paid
          .....................................
        </td>
      </tr>
    </table>
    <div style="border-top: black solid 2px"></div>

    <br />

    <div align="right" style="margin-right: 50px">
      .................................................... Officer in Charge
      .............................................. Police Station
    </div>
  </Layout>
</template>

<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

export default {
  name: "OwnerChargeSheet",
  page: {
    title: "Owner Charge Sheet",
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
      title: "Owner Charge Sheet",
      items: [
        {
          text: "USER: COP",
        },
        {
          text: "Owner Charge Sheet",
          active: true,
        },
      ],
      chargesheet: "OCH00004",
      id: 1,
      vehreg: "KBA857V",
      road: "Mlolongo Highway",
      weighbridge: "Athiriver Mombasa Bound",
      dname: "Driver Michael",
      dsurname: "Driver Tamo",
      driverid: "22335657",
      dgender: "Male",
      dnationality: "Kenyan",
      dage: "40",
      daddress: "P,O Box 58668 - 00200 NAIROBI - KENYA",
      oname: "",
      excessload: "1000",
      ticketnumber: "WBT0007",
      date: "15/May/2021",
      time: "08:00:00",
      configuration: "6C",
      permissible: "18000",
      totalweight: "19000",
      vmake: "FAW",
      /** prosecuteruser="Prosecutername", */
    };
  },
};
</script>

<style>
table td {
  border: black solid 2px !important;
}
</style>