<template>
  <Layout>
    <PageHeader :title="title" :items="items" />

    <h6 style="margin-left: 10px"><strong>KeNHA/MTCE/ALC/F3</strong></h6>
    
    <div align="center">
      <table class="table" cellspacing="2" width="100%">
        <tr>
          <td colspan="5">
          <img
        src="@/assets/images/prosecution/Coatofarmseac.png"
        style="width: 107px; height: 67px"
      />
      <img
        src="@/assets/images/logo-m.png"
        style="width: 107px; height: 67px"
        align="right"
      />
      <p><h3 align="center"><strong>THE EAC ( {{weighbridge}} ) CERTIFICATE</strong></h3>
      <p>
        <table cellspacing="2" width="100%">
          <tr>
            <td>
              <strong>Date: </strong> {{date}}
            </td>
            <td>
              <strong>Region/Location: </strong> {{region}}
            </td>
            <td>
              <strong>Time: </strong> {{time}}
            </td>
          </tr>
        </table>
      </p>
          </td>
          <td colspan="3">
          <p align="center">Stamp</p>
          </td>
        </tr>
        <tr>
          <td colspan="8">
          <strong>Country: Kenya</strong> {{prohino}}  {{prohino}} {{prohino}}
          </td>
        </tr>
        <tr>
          <td colspan="5">
            <strong>Weighbridge Station: </strong> {{weighbridge}}
          </td>
          <td colspan="3">
            <strong>Weighbridge Code: </strong> {{weighbridgecode}}
          </td>
        </tr>
        <tr bgcolor="lightgrey">
          <td colspan="8">
          <h4 align="center"><strong>PART A: TRANSPORT INFORMATION</strong></h4>
          </td>
        </tr>
        <tr>
          <td colspan="2">
            <strong>Owner Of The Vehicle</strong>
          </td>
          <td colspan="3">
            {{owner}}
          </td>
          <td colspan="3">
            <strong>Address:</strong> {{address}}
          </td>
        </tr>
        <tr>
          <td colspan="2">
            <strong>Type Of Cargo</strong>
          </td>
          <td colspan="3">
            {{cargo}}
          </td>
          <td colspan="3">
            <strong>Origin:</strong> {{origin}}<br>
            <strong>Destination (Country and City):</strong> {{destination}}
          </td>
        </tr>
        <tr>
          <td colspan="2">
            <strong>Special Load Permit No. (If Applicable)</strong>
          </td>
          <td colspan="3">
            {{ticketnumber}}
          </td>
          <td colspan="3">
            <strong>Transporter No:</strong> {{transporterno}}
          </td>
        </tr>
        <tr>
          <td colspan="2">
            <strong>Axle Configuration+</strong>
          </td>
          <td colspan="3">
            {{axle}}
          </td>
          <td colspan="3">
          </td>
        </tr>
        <tr>
          <td colspan="2">
            <strong>Vehicle Reg No.</strong> {{vehreg}}
          </td>
          <td colspan="2">
            <strong>Vehicle;</strong>
          </td>
          <td colspan="2">
            <strong>Semi-trailer;</strong>
          </td>
          <td colspan="2">
            <strong>Trailer</strong>
          </td>
        </tr>
        <tr bgcolor="lightgrey">
          <td colspan="8">
          <h4 align="center"><strong>PART B: AXLE WEIGHTS</strong></h4>
          </td>
        </tr>
        <!-- axle weight -->
        <tr>
          <td>
            <strong>Axle No.</strong>
          </td>
          <td>
            <strong>Axle Group</strong>
          </td>
          <td>
            <strong>Load measured scale reading (kg) <br>(a)</strong>
          </td>
          <td>
            <strong>Allowed axle (kg) <br>(b)</strong>
          </td>
          <td>
            <strong>Discretion allowance (kg) &lt;5% of (b) <br>(c)</strong>
          </td>
          <td>
            <strong>Adjusted load (kg) (a-c) <br>(d)</strong>
          </td>
          <td>
            <strong>Overload (kg) (d-b) <br>(e)</strong>
          </td>
          <td>
            <strong>Overload fee (From e) USD $</strong>
          </td>
        </tr>
        <tr>
          <td>
            <strong>1</strong>
          </td>
          <td>
            <strong>Axle - Group 1</strong>
          </td>
          <td>
            <strong></strong>
          </td>
          <td>
            <strong></strong>
          </td>
          <td>
            <strong></strong>
          </td>
          <td>
            <strong></strong>
          </td>
          <td>
            <strong></strong>
          </td>
          <td>
            <strong></strong>
          </td>
        </tr>
        <tr>
          <td>
            <strong>2</strong>
          </td>
          <td>
            <strong>Axle - Group 2</strong>
          </td>
          <td>
            <strong></strong>
          </td>
          <td>
            <strong></strong>
          </td>
          <td>
            <strong></strong>
          </td>
          <td>
            <strong></strong>
          </td>
          <td>
            <strong></strong>
          </td>
          <td>
            <strong></strong>
          </td>
        </tr>
        <tr>
          <td>
            <strong>3</strong>
          </td>
          <td>
            <strong>Axle - Group 3</strong>
          </td>
          <td>
            <strong></strong>
          </td>
          <td>
            <strong></strong>
          </td>
          <td>
            <strong></strong>
          </td>
          <td>
            <strong></strong>
          </td>
          <td>
            <strong></strong>
          </td>
          <td>
            <strong></strong>
          </td>
        </tr>
        <tr><td colspan="8" height="50px"></td></tr>
        <tr>
          <td colspan="1"><strong>Dimension: </strong></td>
          <td colspan="6">
            <table cellspacing="2" width="100%">
              <tr>
                <td>
                  Legal Height
                </td>
                <td>
                  {{legalheight}}
                </td>
                <td>
                  Measured Height
                </td>
                <td>
                  {{measuredheight}}
                </td>
                <td>
                  Extra Height
                </td>
                <td>
                  {{extraheight}}
                </td>
              </tr>
              <tr>
                <td>
                  Legal Width
                </td>
                <td>
                  {{legalwidth}}
                </td>
                <td>
                  Measured Width
                </td>
                <td>
                  {{measuredwidth}}
                </td>
                <td>
                  Extra Width
                </td>
                <td>
                  {{extrawidth}}
                </td>
              </tr>
              <tr>
                <td>
                  Legal Length
                </td>
                <td>
                  {{legallength}}
                </td>
                <td>
                  Measured Length
                </td>
                <td>
                  {{measuredlength}}
                </td>
                <td>
                  Extra Length
                </td>
                <td>
                  {{extralength}}
                </td>
              </tr>
            </table>
          </td>
          <td colspan="1"></td>
        </tr>
        <tr bgcolor="lightgrey">
          <td colspan="8">
          <h4 align="center"><strong>PART C: FEES AND PENALTIES</strong></h4>
          </td>
        </tr>
        <tr bgcolor="lightgrey">
          <td colspan="2">
            <strong>CURRENCY USD $ </strong>
          </td>
          <td>
            <strong>FEES $ </strong>
          </td>
          <td colspan="2">
            <strong>Actual GVW [KG] </strong>
          </td>
          <td>
            <strong>Allowed GVW [KG] </strong>
          </td>
          <td>
            <strong>Overload GVW [KG] </strong>
          </td>
          <td></td>
        </tr>
        <tr>
          <td colspan="2">
            Sum of Axle fees
          </td>
          <td>
            <strong></strong>
          </td>
          <td colspan="2">
            <strong></strong>
          </td>
          <td>
            <strong></strong>
          </td>
          <td>
            <strong></strong>
          </td>
          <td></td>
        </tr>
        <tr>
          <td colspan="2">
            Gross Vehicle Weight GVW
          </td>
          <td>
            <strong></strong>
          </td>
          <td colspan="2">
            <strong></strong>
          </td>
          <td>
            <strong></strong>
          </td>
          <td>
            <strong></strong>
          </td>
          <td></td>
        </tr>
        <tr>
          <td colspan="2">
            Total Amount to be paid
          </td>
          <td>
            <strong></strong>
          </td>
          <td colspan="2">
            <strong></strong>
          </td>
          <td>
            <strong></strong>
          </td>
          <td>
            <strong></strong>
          </td>
          <td></td>
        </tr>
        <tr>
          <td colspan="2">
            Demerit Points
          </td>
          <td>
            <strong></strong>
          </td>
          <td colspan="2">
            <strong></strong>
          </td>
          <td>
            <strong></strong>
          </td>
          <td>
            <strong></strong>
          </td>
          <td></td>
        </tr>
        <tr>
          <td colspan="2">
            Cumulative Demerit Point
          </td>
          <td>
            <strong></strong>
          </td>
          <td colspan="2">
            <strong></strong>
          </td>
          <td>
            <strong></strong>
          </td>
          <td>
            <strong></strong>
          </td>
          <td></td>
        </tr>
        <tr bgcolor="lightgrey">
          <td colspan="8" height="40px">
          </td>
        </tr>
        <tr>
          <td colspan="8" height="100px">
          Action Taken:{}
          </td>
        </tr>
        <tr>
          <td colspan="6" height="50px">
          <strong>Name of Weighbridge Operator:</strong> <br> Administrator
          </td>
          <td colspan="2" height="50px">
          <strong> Signature: </strong>
          </td>
        </tr>
        <tr>
          <td colspan="4" height="50px">
          <strong>Name of the Driver:</strong> <br> {{driver}}
          </td>
          <td colspan="2" height="50px">
          <strong> Driver's License No: </strong><br> {{driverlicense}}
          </td>
          <td colspan="2" height="50px">
          <strong> Signature: </strong>
          </td>
        </tr>
        <tr>
          <td colspan="4" height="50px">
          If not satisfied with the findings and you may appeal<br>(Tick the box)
          </td>
          <td colspan="2" height="50px">
          <strong> Yes </strong>
          </td>
          <td colspan="2" height="50px">
          <strong> No </strong>
          </td>
        </tr>
        <tr>
          <td colspan="8" height="100px">
          <strong><p>Remarks:</p></strong>
          </td>
        </tr>
        <tr bgcolor="lightgrey">
          <td colspan="8">
          <p align="right"><strong> {{date}} {{time}} </strong></p>
          </td>
        </tr>
      </table>
    </div>
  </Layout>
</template>

<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

export default {
  name: "eaccert",
  page: {
    title: "eaccert",
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
      title: "EAC WB Certificate",
      items: [
        {
          text: "USER: COP",
        },
        {
          text: "EAC WB Certificate",
          active: true,
        },
      ],
      prohino: "PR00004",
      id: 1,
      vehreg: "KBA857V",
      police: "Michael Police",
      road: "Mlolongo Highway",
      weighbridge: "Athiriver Mombasa Bound",
      weighbridgecode: "ATH",
      excessload: "1000",
      ticketnumber: "WBT0007",
      date: "15/May/2021",
      time: "08:00:00",
      cargo: "PETROL",
      address: "BOX 3456 - Mombasa",
      origin: "Mombasa",
      destination: "Uganda, Busia",
      owner: "Peter Mwabe",
      region: "AthiRiver",
      transporterno: "0745667883",
      axle: "5C",
      legalheight: "200",
      measuredheight: "230",
      extraheight: "30",
      legalwidth: "200",
      measuredwidth: "230",
      extrawidth: "30",
      legallength: "200",
      measuredlength: "230",
      extralength: "30",
      driver: "Kevin Chama",
      driverlicense: "KENT45556",
      /** prosecuteruser="Prosecutername", */
    };
  },
};
</script>

<style>
</style>