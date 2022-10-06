<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import jsPDF from "jspdf";

import DatePicker from "vue2-datepicker";
import Multiselect from "vue-multiselect";

import axios from "axios";
import Swal from "sweetalert2";

export default {
  page: {
    title: "Reporting",
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
    DatePicker,
    Multiselect,
  },
  data() {
    return {
      title: "Reporting",
      items: [
        {
          text: "USER: Management",
        },
        {
          text: "Reporting",
          active: true,
        },
      ],
      hswim: false,
      showdata: false,
      showtickets: false,
      showhour: false,
      showdate: false,
      autoClose: "",
      autoClose2: "",
      mail_checkbox: "",
      day_checkbox: "",
      week_checkbox: "",
      month_checkbox: "",
      year_checkbox: "",
      specify_checkbox: "",
      time_checkbox: "",
      reg_checkbox: "",
      ticket_checkbox: "",
      status_checkbox: "",
      wb_checkbox: "",
      user_checkbox: "",
      transporter_checkbox: "",
      shift_checkbox: "",
      axle_checkbox: "",
      cargo_checkbox: "",
      report: null,
      picked: "",
      reports: [
        "Auto Generate Reports",
        "Axle Configurations",
        "Census Report",
        "Daily Hour Data",
        "Daily Hour Statistics",
        "Daily & Hourly Register",
        "Daily Summary",
        "Impounded And Prohibited",
        "Manual Weighing",
        "Overloaded",
        "Prosecution Process List",
        "Prosecution Court Fine List",
        "Re Weighed Statement",
        "Manual Weighing",
        "Overloaded Above Allowance",
        "Shifts",
        "Static Configuration",
        "Time Turn Report",
        "Transporter Statement",
        "Vehicle",
        "Weighbridge Picture Tickets",
        "Weighbridge Register",
        "Weighbridge Picture",
        "Weighbridge Register Compressed",
        "Weighbridge Tickets",
        "Wide Load",
        "WIM Ticket",
        "HSWIM Configuration",
        "HSWIM List",
        "HSWIM Register",
        "Static vs HSWIM",
        "HSWIM Hourly",
        "HWSWIM(BOSVARK)",
        "HSWIM vs Scale Evaluation",
        "Tagged",
        "Weighbridge Status Report",
        "Special Release",
        "Excess GVW & Axle",
        "Excess Per Axel over 5000KG",
        "Vehicle Weighed per Axle",
        "Overloaded and Chargeed Per Configuration",
        "Daily Charged",
        "WIM Tickets",
        "Payment List",
        "Habitual Offenders",
      ],
      reg: null,
      regs: [
        "KBA234A",
        "KAV257K",
        "KCA345H",
        "KCA198G",
        "KCV456",
        "KAB347H",
        "KCR234T",
        "KBT456Y",
        "KAT459F",
        "KAP987R",
        "KWT345",
        "KBT567F",
        "KBP786D",
        "KAP478B",
        "KCF290Y",
      ],
      fromdate: new Date(),
      todate: new Date(),
      ticket: null,
      tickets: [
        "SR00001",
        "SR00002",
        "SR00003",
        "SR00004",
        "SR00005",
        "SR00006",
        "SR00007",
        "SR00008",
        "SR00009",
        "SR000010",
        "SR000011",
        "SR000012",
        "SR000013",
        "SR000014",
        "SR000015",
      ],
      status: null,
      statuses: [
        "ALL",
        "Prohibited",
        "Closed",
        "Charged",
        "Released",
        "Special release",
      ],
      wb: null,
      wbs: [
        "Athi River Mombasa Bound",
        "Athi River Nairobi Bound",
        "Isinya",
        "Juja Nairobi Bound",
        "Juja Thika Bound",
        "Rongo",
        "Webuye",
        "Busia",
        "Mtwapa",
        "Mariakani Mombasa Bound",
        "Mariakani Nairobi Bound",
        "Maai Mahiu",
        "Mobile Weighbridge",
        "Dongo Kundu",
      ],
      user: null,
      users: ["COW", "COP", "DM", "CM", "VI", "CC"],
      transporter: null,
      transporters: [],
      shift: null,
      shifts: ["Shift A", "Shift B", "Shift C"],
      axle: null,
      axles: [],
      cargo: null,
      cargos: [
        "Petrol",
        "Cement",
        "Livestock",
        "Sand",
        "Ballast",
        "Maize",
        "Grains",
        "Rice",
        "Others",
      ],
      lanes: ["ALL", "WIM", "LANE1", "LANE2", "LANE3"],
      lane: "",
      fromhour: null,
      hours: [
        "00:00",
        "01:00",
        "02:00",
        "03:00",
        "04:00",
        "05:00",
        "06:00",
        "07:00",
        "08:00",
        "09:00",
        "10:00",
        "11:00",
        "12:00",
        "13:00",
        "14:00",
        "15:00",
        "16:00",
        "17:00",
        "18:00",
        "19:00",
        "20:00",
        "21:00",
        "22:00",
        "23:00",
      ],

      tohour: null,
    };
  },
  mounted() {
    // Set the initial number of items
    this.updatefields();
    this.totalRows = this.items.length;
  },
  methods: {
    updatefields() {
      axios
        .get(window.$http + `Transporters`, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          // this.cargos = response.data.cargo_name;
          response.data.forEach((e) => {
            this.transporters.push(e.transporter_name);
          });

          Swal.close();
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Transporters" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });

      // Axle Config

      axios
        .get(window.$http + `AxleWeightConfig`, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          // this.cargos = response.data.cargo_name;
          response.data.forEach((e) => {
            this.axles.push(e.axle_code);
          });

          Swal.close();
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Axle" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
    },
    censusreport() {
      //alert();
      //let pdfName = 'WeighbridgeReceipt';
      var doc = new jsPDF();
      const cdatetime = new Date(this.mydate(new Date(), 0, 0, 0));
      //This is a key for printing
      doc.setFillColor(255, 255, 255);
      doc.roundedRect(5, 5, 200, 40, 1, 1, "FD");
      var img =
        "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAJIAAABZCAYAAADVVH35AAAACXBIWXMAABYlAAAWJQFJUiTwAABFw0lEQVR4Xu19B2Bc5ZX1nT6j3puLbMkFd5tierGpC5gFEiAJvSQbSoAkpO2fXVJJAimUkB42sLsBAqRRQkIzmG56teUmS7J6LyNp2vvPud97oxlZkuUCBjYvTEaeefPK993v3nPPLc8l/we34eGoK5GI+WPxeCgaiebFYrGceDxWHI8nCvCeZ1mWD59luFzuCIbHwiuBzyyv192Jzwbcbnev2+0Z9Ho9A35/oNvr8YY9XveQx+ONBAL++P/BIRXXR/mmu7t7iwYGeqvCg8PV/X29ywcGBuaEw4Ol4XA4c3h4KBCNxfzxeNyXSCR8ECD8nfB63B5JJOIuy9KxSdjjQ2ESCFDMFqoEvk3g33Gv1xv1eDxxv88f9fl90YxgxkBmVkYTBKwlNzfv+VBGaENWZmhLZmZWfTAYdI73kRv2j4QgDYTD3oH+gYru7q6l7e0dq3p6+ub09vZWDA0N5keikVyBMnF73OJyecWNO8bEUyjE4/XgM37uGnmHAnLhuwSm3C1xSJJbKFJufA7h0g3qKSkIVsLCvxNiJeICQcQL7/hxAn9b+BwKLe71+fozQqGujIyMtrz8/HU52TnPFRcXPJGZmV2fm5sz8FGQqg+lIPV0d+f09vXNbW1tPamzs/uInp7uGUNDQ3mYyHxOis/nVWHweX0qMJhrCA8EAhJhJhfTawuD866fqRXDkFAdQbi4iw4QP0qZ7dGDxn11J/Of/u2CxFKY9Kg4Zywag4AZYUtA2Dxeb5/f5+3Nzs6pLSzMX1tQUPA4hOzF4qKilg+jYH1oBKmluaW6o7PjcAjPaV1d3YuGI8NTMFF+rwoLBcUIjZlUs1FIKACpwuJO+X5HE+b8VsVkF36nImX/buSqqP2g1ai1IFjAYvo3z+X1eJqzs7O2FBYVrS4tLrlveuX0l3d0jR+U7z+wgtTX1+fv7e1b1NDQcG5HR+eK/v6+6ZiUPDdWOoUH2EQFx2gNaA/b9HCSjODYt+Yi2DHaxRavEe1ia57JTAaPOZ4w6flSBI3nczTZiFCPCLdqO5pQvbaRa6VZpGDZwjUcDIYa8/Jy3i4rLb+vuKTkgYKC/PbJXOve2OcDJ0htbS1VdXVN53d3t68aHBycjzkKOBjG4zEmxAiQPQEqOSNyk6qN+Lt4DLgY7243dzJ4x2zASMnj4XvFSoqE8DdBEfalRrMlkJ8laCN5MgqCrUVorvgRcZGRVnMxRvDwe/tjt57L2WxB0v1p/hyDaLuIOCavhQIVx4tm2u/3N+Tk5K4tKyv9dVVV1d/2hrBMdM4PhCC1trZPa25pPr2hYduFsWh0OiY4n1qHk2kukBOCVY7JsOKcINv5wWCnCY6BxOLB77zASZx44JDkROlkYvJoSrj6o9EoXjEZGo4IvDidtEhkSDFMNEpzMzLJxDk0nz6fCy+/+ANBXJ5HAKCBxTyYaGhJfM4rJj6isEejBN5xFToCcl4PMdLIZoSOApdUmarZgOuwH3GdpQvFmL54DMeLRXn5DUVFhS9Onz7tR9OmTX3+gyBUe1WQ6urr921sbLy6o7PneKz3Ep8vACEwq3mszZgvY2I4mga7AFRjEv0+nwT8fvFAcKhdhoajEhkelv6BXmlpaZG21jZpbW2R9rZ2aWpuEphL6evvwz5GiCKRiB6T7+p54TV6o4C64fHR64OG0PdgKKTnDuG9uLhYCguLpLy8QjDReC+VvLxCfJYPgQsBYEPQcH88NpwDnBdmLA5GgYIyynRSENMQ/qiLAXWBRZWIQIDfqphS8bMpU6f8d052NnmvvbLtFUGq2bDxY8A+Vw0Nhvf1+YKZ/qAPK5DjSZVO1yf9shQ0q9Uwq9kfCAiIPwkGs6ANRCLQLF2dHdLY1CKbN22Q2tpa2bBhozRDYHAeAX+0VwaXJyWWq6iokClTpuA1VebuM1emTauUmTMqBZgHApapC2IQAg1TnjRn/C1NmqItezhU2FL+TaGMwXRHo8NcBJsrysvvhYa6vrCwsOP9vuH3VZBqajacVN/QcC3M1wE0BT5/QFci1b2RH5oqahyDgWgK6NmQ+wnBhGRnZevKHhoaFnhw8sYbb0rN+nXy5ptvyoaNG6SjvUMAyt/vMUw7n6ENxtaozo7UZkVFxTJ16lRZsHCBLF64SGbPnSNVVdVqKqkhe/v6ZSg8KNQ8PCa1Hzfn2KnnAeOOMbQwLv1ckPXQjPdVV1V/PS8/931bQe+LIDXUbztk4+aNNyA0cUhWZoYSgQpW1T03ANp4W2aowTLrdwEIW3FJqWRi1YJ0lE0QlhdefFGeePxxqampkaamxu1MkDPoRkA/WNEKahiCdrNIRrASrzknJ0fmz58v++67rxxx5FGyZPESmMVcGYap7ezqlf6+Lv2d201vlc4BAX6KDNOh4FBCa9NsxuKJ2uKi4ruWLl38tfdjZb2ngtTU1LRP7ZbaHw8ODZ8Qygi6fFDzMQBGZ0sVHoJZaiDErCQXg5qfX6iC8NJLa+Wxxx7H6zF5++23xh6T1AO9H6O2p84xwXVPnTpNDj74IDn55JNlv/32k/KycuC9AWlvb1NBMbwTHYmRzfBTFswpwzwJXXxYpK9Pqaj4wdy5c+7cU5c91nHeE0FCLCu0afPmb3Z2dl4IAFykTDPMVeoqNKvLdnGhgegJEUcUFRXJli2b5Y9//JM89NDfVJCIHXRzBv7DKjiTmEmHq3JMGALBAu9MzjjjDDnppJPkgP33h5Yalq1b62HG+5XFp9kzjD01u5lSjjXH1wH2MKePgjb4bGlp6aZJXMZO77LHBam+vu403OT1uMFZoVCGYW1xk6MZZ3OzcfV2iopK9O9HHnlU/ud//kce+cff0+JZSTzwERag7VSLvdBSFx/3Wbx4sZxzznlyyr+ukrLSEmlr65De3p4kjnIE0OG81PvDRk4KC7ITJvR3S5Ys+eJOS8oOfrDHBCk8EA5u3rL5Z7jYcxH5RpDUQ87DViQjfA/BNFdJKBTAQJTr6rrjjv+We++9T15c+2I6n7ID0LqnB+ODfLzRIH7a9OnyibPOkjPPOEvmzZ+rAoWYY5pW4v2k0gpch5FIFBSG+/HysrIrysvL391T97xHBKmlpfUAuNo3QbUerGQcvS+bSHN8VwoPTxaE15WTky3dXV3y+9/fKb/57W+lcdu2kfvB7xQz/lOIxpxjAnY6KqnbJZdcIueee67Mmzdfzd3AQL8JHXEsU0M3KUAffFldYWHBdTNnzvzlnhCm3RYkcDZX9vb0ftnn90/Rm6QZS8a5jIdCL4xmrhC8SXt7u/z+zjvltt/8Vmq31u6Je/jnMTACpFM+8cmz5XOXXy7Vs2aBPugD2B5QL9GZD6a/qNdo9BaphQjY8z9MnTLl01jcQ7szkLssSIODQwGQfb8BGXY2GFtcnR0/ohDZQUyGBUj/FxYUKjH36KOPyr//+9dAFm7QayZINBhqYt5ld25wb/92MrzS7l5j6jnorPzbv31WPve5KxR/Nre0KZRIZkY4MUrOjb3wgZ9eKCsrPwMMfP2uXssuCVJfX38ZQPXduLgjSK5puEJJRKNyXS6PxrEyszJl6pQKee211+VzV35Onl6zZvzrHANIj/Zgxvrx+zJRY5iTnR1wYkbG8HZ3Mx5aOg811jGrZ82Wr331K3LRhRdJV0+Phob4W2PpnGk3aTYJkJmIMW7Kzc29Asz4w7tyjTstSN3d3Ysbm5ruDPr983lCky1owLSjYRiymAbWNjMzJDfeeJN8//vf03iXs3F1TJs2TYWN7m00GkE4o3nc62eIAWmr6nmQjKPQTgfYXA9We+vWrYoDdlWrBRBuYehCg7YpE+24zrxuTY4bhUt4sYyt0WRH4DDAwcDqb9b4Hq/NCQpTKyCHaoTCGOcuyWjzeOSIGP4h5dGGyR+9OfeamZmZ3D91XAcGwjboNp9edtll8r3vfU8Jz5oNmzVtRYPBo4yAG5ZjaHioA8J0Jcbj97siTJP+DYTogHfXravdWldv1W3dihffa62tdQ0WXH6rtnartXnzFsypZcF8WUceeZSTV2FhMkxeBQzfwYccYkEorOHh4eS+VdXVI/va+3HfK674nAVPQ/dL3erq6ix4HfobHhsDnDw+P9vRC2pd9zn99NMtTLoFATCvSERfsVjUQmaAddFFF213LJ4L8Szr+Rde0Evi76CNrZNPPsUKBYNWR0eHfg6h0PcvfPEaPYZe4+hrsz+74YYbdF9nTO6//wEzZin7O79HaMVat26d2R/ncK6Z/968eTPGpSI5Lvw9hMi69957dX84Rtin1qrDnKW/6q2GhkarZuPGXgTTPz9pobB3TPXLJ/wtvIGl7R3t92VnZlUybYIpFdRAHhCJXrW1cQRRfTJz5gx58MGH5KijjpInn1ydRpA56g+DpdqFZpEbY23UTtxM0pfZzj77bLnllpuVdDO8iNl+d/sdcsABByBE0qT/TuKsncBajudDbchUEjgL5oVIPl/UlGSIf/CDHwiEI21sMB+aJTAAD4kbf2cBI6pWs7Me+Tm1HbfwJILGwwjaOsfiO69rLC3I7845+xyZO3eu7s8AtnPN/De8MPnUpz6l36mtwNwgf13OPPNM+fa3vy0lJcXg7QpUgzPSYF4mu5QhluyMrOxEzPp+S2vrZ9Nuegf/SOfYx9l5MBye09be8YdQIDRNCS6YFsgRbKudNor8oKzsLAXVN998i1x11ZV6JJMvvT2Qpto3qaZkt+E92CkcevP2/v/5n/8p1157rR5HJ1rzikSuv+EG+cqXv2zoBRtXEX984qxPIKRwsIJ6HmO8bEZ+X7OhRu783/+VBtAOzBkiGepci/NbvvM6CV5v/ulP5dNwsVNNKL/nYuCmOUa4HC4G/k3Trp/jHpl6Ek0JC403HxRMbk4eknPs1P15ztKyMuBNM762T6NpMeTkKMRc3F/72lflF7/4haEBGIOz8RnH9JlnnpG//OWvUlpSIu0dHRpm4XEoULwJWvBQRsCP9JrrYVq7i4uL7toZgRp3X3hnM7Ztq3+zsbHJamltsZqbW6Ae21RFtra24d/NVnd3j6rNz3zmM+mqe5Qqd1TzkqVLLWAAmBQtF7NeeeVlqwCmgmPD17e+9S39nOZvyDZ/IDytSz796XRTae9/66236v47sz399NN6rGOPPU6vBTFA/fnq1U9at912m/6NRDcoLnONF15oTJxjErOysqyH//aw2Y8vmMcVK1ZamEgLAmo+x2fczjzzrB2a2muuucb+jTnfPffck/wNx80xczfc8EP9nuaM2zvvvmtVAxY8++xz+m+aY25f//rXxzTJvIfDDj/cqgU04b6N25qt5qZWq6UZr1a8MK/8N+e2vr6+HWGukycjSBNqJGCTUE9P121er38hV7J6Z94RkoteGkpqYNJCcvVVV8uvfvWr9HNO0tRkZeXIsqXLAMib5OKLL5Grr746qZmYrBYeDENdn42V9Ofk8R21v++++8lnP3upDR4twY3LtsZGCULlU8tQi3C100zNnbuP/p6k6T7ICypFiIEcl0mU42p2A+SG4TpfKUuWLEUkfplqCGZm0sStXv0E4oBbjObAWGhS2iS2448/XoAvx9yTWrutrU0j/0bL0PM1KSPOBrnQ8WAg94ILztOPnbSSn/z4x7Jp0ya57bbfapCX98ntYmhQEr6bN4+E1ngMbvSejzv2WHn2ueekrLxEx2wsDQ5HoRAw5IeInb4JZ2DrJG517F1wgh8gkxAap9vq6emx37v1nS+cAEAzap11lllxY4LJMcCi0UhhrFgMm73idRnZGzVVBNqA25o1z1gLFixIHt9Zmc65jjv++LSV+M1vfXvc1Y8MyeQ5wKZbyAmyVq5cqaDYWeGvvfaahQG0luIa4Y2Z1W9fy2OPPmEB1+nxMbDW/fffr9+Pp5F4bzARFsy9VdfQkHqL4/7taOlUjcTz8XXzzTfr75DMpu9PPfWUXge/47i88uqraWPxy1/+asyxcLQqcJaFdBz9Deba6urqSs6tM8d8h5Z6GgKVNZEgjQu2kbKwChd3MbGJAjG8TGGhKS6khqJr+zUQjHfffbe9mrbHQxOd3MCc7RkIfkYAz+2L13wR6SNvJ48/GoA6v3aOA5ZWtdc555wLgHmWfPKTn0IqxiqBaZIsJMYZ5gR4hvnPKRqF98WN2osgGQIF2uJ6/cxJqV559FHyhS9co58ZjLfjwlmmATMF5qgjjxB4Q/pbh2aghhiNIcfDdlhM8kkbRBu5Erj13xcs5uT13AyqhZujrS688AKlTcxNjIyz42isX79eHRpmkObn5+vcUqOpE+UUkeIddMSh/f0DOx9KAWgM9vT0PoXyZsUPfE/923Frv/nNbxqJH8utHcMFT8dIYdyPWZjEQsRefDkaynnnijjv/PPHdZtPOeVf7WMYPPLt73zXuurqz+9w9RMLwOOxjjnmGNVIECz9zSuvvGIVFBi8xutFOkvyGp3rPfW00/T7Bx58yGgkfDEWRnLu4TP/9lndn5SBowH4G37PF+/fObbzPlojPfHEaj0XgLy+v/POuxaKK62ysjILPJtVWVlp5eXlWWuefsbsZ2Oo//qv36VhLZ0v++XMx6GHHqrH5bnBQyXn25l3ZKRa8PzCeF1upHL7bUyMBIB9HmI3h3F3k4bAczuZfchcxIr981/ul2984xvmiJPEQqNP73gdzHa8+OJPK0Y69dRT5frrr9cVwUg1CDK5/Xe/A0M+Ra677rrkiuNK5op22ZrE0TSomJSbbvyJ5n+f/rGPqYvuXJ5ZqawC8UhjoyEPWa2i34+hTHn8yy+/TFatOlk1MK+HMa0bcH1//tOfBOz+eOOa9jlSi/Xf4Jc0p4gvVq6QBOT1kEQ85ZRT5PTTTxvT4wQUAJ1yZDIQiwmWNWueltdef01jZsRVvAfiO+Z0qRcGCoPb6R87XX72s1tl7dq1212rjh80FT25L33pS3LjT36SzG0amXfjScOkh6BUroSJ+wfm38S4JtqwOjOhxp4joueLZKDzws2rtJNwg5nYoSdiT892K2C010ZcUlJamtzvpJNOtlAgqeeCdkxqFwR7Lahg3Y/eEd9PP/1jZj8bx3z3uut26rr+5V9OTPPaXgXOgMtvzuHz6full15qNA+xm309t/z0p9Zdd91ltBXAHl+jvTZHI8GBSLvm0ePCf1951VVp9+FoJLD41p///Je0sQBPp8e76qqrx8SY3DkVet599x8mNSa//a3xVukpp867Q3hCwRBH3TGW/GynkeAGrwqFggc4CVGO1iAn4rNxy2WXXrpHk+y52lnSw43nffDBB+T440+AF/gLWbBgoeIWrrBPfuITshBYgVoLDK7uj84f+u5EuPOACRiiYAjBeGSORjW3z/vh+cj5MHjM1aaf2xDCKZTkZwk78f7nP/+5HHjggXL++ecrd8btCkTZneM5edjOADsYxFnxjkbkGI7GQMZjTEh+XoF9H+nTdAq0IdNtnWOQl/0lxoXbTTfdiBBRndx660+TIR7kaosPmrd6VrVN1Aqw4hly402HyHPPPjuWDCQ/u/TSz0Irnopc8XxNiWZRAYlWHV+8cYrA+B8FU1cZDAbG9+IA3Lx4GXJk1Eabzu3e++5Lcikc/515OTYZLrtqARsiWQDTFhK1ksdytE1JSQkwxQY9b+oKgwtuwaXW/S+++GKjFWxPBgy8Yi0QqBYItTFfnZ1dFrUgV/sJJ/yLFcZKczYEmK2SkhHt6NwfcUgzcNV4G+gDeIBHo4eSxwL9YK7Z3vmSSwz/NVGI5Nprv5H2mzvvvEt/8wR4Lb0/m3Mj/4WFoN85Hiw1KMcKNXWKw6i1Eceyx8Vgv7/e/9eJ58rGuYjNpZ1v9P1SO8GDv2VCiYTt3R8mone8wYpEYhYoeHMTtmnZFUE66OBD1HRRmGg+N27caM2YMSN5oxwgR5hQfmTdcccdqmrhXVgUFMfcffkrX1WAuXq1AaJjMAnjTnx9fYNOyKpVqxRg8rgEzPBkFLymTrwjACeeeJKFhHqA82Hs36e/4YvXFca9nHjiiRbwA8i8VgsNvJLm+bLLLh9fkOzFeN1139VrRWaFvt9yyy2W40jo8XFebnQOeG3oZjIh3XLOOeeoieLveI3czj//gvGFyRYkYEDr4Yf/bo+nsxTSh7Gvr3cDxiAnVZjSfO9weOhWpMBeNlraHPN21113w53+xITCONGXqZHrww8/XFUvgTvJuhdeeEFNWOrmRN1p1o455lh8BRIQKtcQoVkCrwXUw13q1p9wwglShCpXzssYuDntuAxbdCEt9Y7bb9cQCK+F8SjSGYy+P7n6SdADBiA7m3Pthx52GEJBBRqdT818YcyLpoPk3nHHHafZAAS/dBZefvnlHWYpMBmN+dh9vX24tww13RyfWbNna6yONAJDRY8hp2sytAPjmEetWKEJIwy3QFMJAr2oBXxjh/MHr1T+9Mc/jr0fBncI2Q5wIE7Ozs5+cLudMDDTYP+2jF7VNstP989atHDhTpmy0dpqogj9WGo/NTSwM5rvvdiXJN7oaPxY53HIvu3ufVQkP03jjfHdbt3DJOmYic7x/PPPp6ihEc1EeSCMgEN2a6oQJcE2zMXH4ArPSP0SR0omQoFhFsR1dijNE+3A4+lm52WbPyeo9bf3dwBq8vf2MUyHEEMKageRnchLckCu/lazF0b0WNp57BsigNYS6jHOwc/0eKQkbEDtHMPZf3SetTMORNHJY6cMnhKVu0irjPc7A5jZEggZGwDSvDa2OmQtoUl2Y02cD7lhLXLPPfeqg8EN4Wv8P0RFM175Ow61dQgyFrJBVmppc1KQcIBVIxl05o6MIBmG+b777lVGdqKJn7SUcfCcCdrBYI01qfbFpbHCOzvwqcedjKngOcfbj8caTxDHvX6Obwoz7rTJGT2GHG8KOk0VYQAT1JhMB7OiRRS5ObnwsvIkBHMIT0qrkjOzsjT1JYTEQvS01JpB/hZ9FjRpzot3HtPnC+l1R6JOxxZ2cnFJV3cH5jkhw0OAHkH2H2CXF1N8adLKWELuq4LJJNeoLXZUkADIZiKdY+noaIVDAZBIe/Y54zpONDCTFqQP4442IKJWSl3xOzsexGHETyoIuTla64+eklqeXVpaBkyVIwX5eVKEbMnikjKELvKAAbMgIPydIwjww5XGMF1RmNgZjRmNEo2is8owW+pENeBMEjWCXglR/B1LDCEcPIDPSa5C+/ha0VG1EwKoFbmaPRnwh6UoH+XhiU6k9+RAkJbidpk6xD5NIwF7yAs6ActpaYKEvU6DF2aIjDE2Ms+1dtT7wygDO33NtulNExKHbxonvkZtTiGhA8DkMWqK0vIyTYetQJsblP5AUEqlHGnDFIrsbGiS/FzNnGAukOmdhMZamJ3hIXQmAehnlxEmATKe1t7aDmGIojIEQhFB/yZ2dot4BIk24LugMeJIvoNwQY+IJxARf6ID7zGcq1eCXvBw7PQMK+TNxL/9CcmGpsnM6BE3fm9Jn23YtP2Y8oUxF64hPFeycpbqcS0XpFWtk4lJ2nDgIDg/AQj0sGPaTkZf4O3Gm2PGBfjOu+vUm/kobA4emlCT2KaXKpwrnq1nslDIwKQyFBYiKcwIBHsgMd+bnt/UqVMkG6YGPSBhVgxJSnPA5lj0mtBhFyTuALxD9mQa1vDKunVh/TwBLTIYYcPSKDQBzKQPQXJh1iUS/XGMRNwvIR96IWByg+4h8QaGxY1XSagbnw9KRk49BKNLPP4eCXiR1+5hSgz2sQYkIsgpj5muL5pkGkNWqoXz+BGigfgEoJ3EnYfvoa08SMxLBHG+AIQWZfIRZoAOQ4CQ6UliMsXHt8vEy8D3Z3MnL1ZAPoQEyTDbR+FtIlvq6gyJuUfw0fskjWMCdE7KGBqFTDfdY0bKp6DqBZwWWszMlNmz52iRAsg+FSZWxRCDaCc3/BeBO94/0KdFiXTR21sHJTz0rvShcwiTJtFcTXOWGPdT7OwawmrHpHqghSgiWOWWK1c7vmViPmPRLOwXkqgXrDJMSdDVK/5gm4T8A5LnXyehnI2Smb1JMiBEvjhWOP6LI1eKghIZiko4GpTBGKpVoshyiCFPLFAiWQFUjwRbJAuaJIHqHg+wU2QYRgx5ZT3RHGi/QgkPZ0jPcAEELRutdDJwvaBBBkKSV5AtZVPxG9UxJk6ZuiEdORP3VYLP2r34YzZWQaGzQlM5GC2tw28btzW+T9O/86cZT7hTNQ4apSsuQamNCsWcObOR21wlMypnyjQ0aKioKNdGDU4qCVNMmDPdizIeZGZKB6qC6+u3aboFTQ16d2u7Y9MOkOVBbJAKo+DyQ/ujcVaiEOajH5ojgs8AaDEHnAv2MEokYDYAbqNsqgEN43Zj5SN4G/R0w9S9IgUZrZKX1Q6BaZOMUKf4rGGGmSVidUJIp8hAb6U09UHLQZNEEzk4d7/kZoZVMAsCqMoJhZF82C4RCK1EMxC0niJ9PTOlfgBYargCxyhQ7DMcwTFj0FZWANcewgUOQoPZOMhC+i20yDAWQmSY5fX02IzFMk0IbZGiKU3gANiQ9uOOY6Ul7dpY9Ulj5V3v/JTv2i9MEZ9xhccSmlSBYROuApCSNDfVqlFma9xt3rx5ilWocRBHxASFNJ8ajd21dAftltHlbas28CIeYe6zKUYwETztR0Q32c7LCgHXeLNMnhaxjZVA8rwHWMPVB/DaK3Gs7FgsEwKTa7paDvokhhUdBNj1+bpghqBlslokJ7NP8jPegTlsk0BON7RVN34TlL7hXOkdzJfWvlkyFPVDSyAnyJcnHpifvGCflGZ2SIC/CTVLHFosFi6FYGVKz5AXfZSqZaC7Svq6Z8MooaginiFDMFVMknChtxKyySDAuCqYP3fQkgwIqke6IMtEOSru0EzAXZrHDs8PPRoca5QUIHsq0aqIeEeDntBIVg3Ip4GmpuZsPO4grYiPg8eVvLV2i/50Zz2UXRGd0cKS5iLbgJcBWWqWGTNmSlV1lcxHzTuFZtq06RAc1sDlq9CxZHkY4LSnv1d6unuUXWYKBhl0J1FfKQ8m6rHtMitikAxPriWA+2bSvPI8FGL2tkRDVLOZDismMQ49IT3AOQlMRLwYX0Go4DXFfVHx+lvhbsOsBBphlgakMGeT5OVsgbZCxY23A8IGltjKguabKt1bF0t/bzVANAQ2s14yXRnQTt1SmlsnmYXQTFmdEFKkikDLdEHDNDcul/6umTKAf/fHynBP+VgA2YLTosECPDMP+ncLXH5vWDKtHlwXTBY0n4dYyQ0vjSksAOguHBRISeMBcbr2FDXt2MW2gjF54IH7dRGlzj3HyyTnWUPHasTB1lDnnXdeB+JZBRzU0V3OHApgslzLpIQnJbYwEe4icC0sLEZTzzJZtGgR8poXyGyYpdnVs6QIgpSbk6U3yaoNmiE8PkJ7CxCz9OEVBZVvZM9wMXSXiYdMXhKzPu3INoeEFRQoqeIAQzRIzbH6EwLJ5pZclawUwfFIyEFokKGjbjTWNd7R4QPn8Xv6JDejXnIL3oG2aZHCrFbx5XRJIKMJ3/fgOitksK9MOnpLpau3CmYnExObCSwzgOYa9XD1N4EbagdYB2CGmx6BduruqJbu8BTp7K+Wwe5S4K/ZEFycF+eMw5T6TfwW5gwvaBfB41KUNKTQuyBVCdwrNIvqVgoKhEjdebxYYaILgpwS7o/uP7JklDJwlMp5552Lc8KrSyFindAVw1To1ZlRUVE2qF4bVvRrILpWMp1zaHAomeqgXgsILKZbcIL22DYOi1xdVSVzkJRfXTVLli1bCiwzV5Pi6THRe2KcDUFN9I9sl41IeO/pYm+gfuCWiK6oEfMDFxYMrWqVFDaai8F4bWTE2XF/hJHmOFtuU1LNVcqNZskDUJvABFgQphhL0eFyuyM0EYCfQQiKr1OKC1qkLLMGAlQDUF6LuBjIXnhFEskDrqpCMcIJ0taXLz0DU+Cy+wHY+6QMoHl65duSn70VZhLeESY42lskPb1l0lw/Xzp6KqRzoBqYZqpEAcDdFjr3AoB7PXDlga0kACDv7ley0ENyn3KO+4viGl0QQAW3MGWCfS26/vicm5WAN+iAZmI8TQy0oKnRLpCJhCA3mYKCxqbyxutvyCzEAOlJOgqFx0DAGDHFdsKIJgoRP1NBQmvd54EfVpIDIRfibCZ3x6PYYmcEaSItQ7PBQCZNk9MzEZW3MqViCgBwpVL03KgZOxAArYObzLr1HgRVKTDacwnX5YOAk7GlwPDdMMCmw782SbcfbJSqSUeHRXRgNSKNwYn5VMW7XGFMEAaOmt8NsMq6L5ggT2xIMJ0Syq+VnIz1MjWvBUKwQYLZW2DC0EQWmgtPtAA4r5a6tiOlrXMfaJBiNS8eYKKinGaZN221FJRtgCeG8nR4RtFhjGsfmmVtPBCNVOcBSOM1CC8OwkJeyIN79MM0+V2dEAbTkY3XGwV6pwDRDHEsQCOZDULhopvuNR3ucBcK1Bl606auXEQUI94vXLEIKIf+/iEtbGUj1MrpUyEgXfLss89oz6r162sEpU7KmKeZNhyVAWk0PO1di56eSUECIH1GG49rlWl6VSs/wwNXUPKSlK+RP+wYE3GMo+6Sk2PvRTXLsh+W9yxYOF/2Rz9E/g3hVdKOG1VpR0c7NN8m7YVNrgVpGYpjyG8ZrOZDV1sWMhjNY85jiiy5mWR856QOTDaUhWF0zLbdv+3VGQf/YvEpWpiIGIBnHIJlwbPxugalLP8tAOK38d6IyHwTgC88HJgxtw9NY/2D0g2A29ICLdKxj/SHSyTgjkp2CFpn+vPwxNaB1NsM/EVexifhSCFCEEvQyvkg6ehbIOGuYpgoLt4c4Cpojmy0ookXwQQB57jRLwECZQEs46oMMUh5SfWI0ukdqiXsQUxjlJLBc07KtHnnIu2Gk8Ewyn77L9M0ZvbnvOmmmwU53tpWmoLDngts4cyiTyNI5sQUQi54eLoAX2ZTjTRjRtVLZWUlfO4Y5CY9aZKCRFaWpoUgNa3RU0qMiRPJffKRXbcMnVkXLlwozDVetmwZ3Oxpmvaht4XJ70B3sfq6Bnml7VXNtCSmMXEcU6ni96NCReNGHGAnqOuMnml4Pnob4Y30F/o/FRxjs+wxgLDov7miTXWsKRGBawx3OTEMnBCdAVKvEyGL16So9CUpDUF4YHr8njYIDV6szPUGpCtaKp31K6V+0xEyYBXgepukOP9VDO7TUpbVAy0FysTbjQnwSl/XXGCiudLatlA11dBgiUTZnB6CGIIC9gBzxeH1eWGqLOAw8TYjIpGAJwQcpovVCJB5dslYfnXqaDiPrxgJhjtBZUOMQrPCcz30UKbD5KPd4iNy8UUXIW/76eRBnLFkHR2tFFNm+LgNChMpDFopsvHovPtkmiBVVk5rPefccxvQMGsmPSJiEWcd04thDIjCBM8uLdBIM4hGDjIHdeiHwjwddNDBSuax/t8piemHx9TZ1SPvvP0OXO0erIReu9SZXTFMDwFiIJ1yHSMDAFXobBVjsPlIhNq5eOeGnX+bIDNXDwSFIJPYBivZgqkgZhYL7jiFH+YCle9wncHi0mvB/fpwHZlZbVJacaOUF6+VbE8HsA6OQYYXsaiElQNwPEdaO2bJtpb9pK9zLkxPqxSVvSnz82olF1jH528Au4w8JWiEnsEZ0lF/uLT1LJCm3jkg+BCBgnYOEOhn90gGATGEDNAYdroX0oTFFCuEncoBBgL2AS6KEzB7wSPhOwuA2R6g7RbRWB84cVO2GKIY8ikHHMEDDlwulSBZH/nHI/Kd735XzZhZcey0R/XGx4NhPKBQqKloCcysOP9nnkSVjZjg7Dlz/54mSPwHWvA+/vijj18cRGR5tJufBeGia01BWoTkq3n7zJMDli+H4BwoyFFSzONsKGNC8/SN2jy9G4LDKg6teFUuxuRmB/Acj7GYZ8dMORkHqUHkEaGZeEVSkFx0wXnrEMQ4+BILXAoxkxvS5Ma1xCJZEsYk+bytUgBOprR4PdT4k3gcRDswLAg9hBbiiVzwQdBS8M6amk6SrfWLYZJmAcxmSBkY5mXLbpdiYCUfWGOoKYxuAGYrT/raD4LZovAsxL0XaKjDDe8q6KcnRrGB56dakf/hM7LbuF6hOVPNyXOyjxH2VS1A/WqabUxmM4vJPICH78PDYWiUITxxYA464h4gLwLTnH/uOdBAI/nbDqZN5QtL0UugDEQtzRob66cuXmqoyumVW2fNql69nSAdecQRtz/37HMoiCTxBjpdL4g2le71MAZuP/n6f3xdjjziKE1fGBGcHnTfr0FDgi5ps3tA64PtbGwTCIC8I/trmxon5WJ7Tiq93+FkBi19HwMKrDjCDDAPMaxkC+6vC+yvPwacAaIwEnVLP6YzP3ObVOauk6Ipr0hebg3MS6NyP9Zglrh8YHM9FdITL5CWrculue5A6UdWaVZmJ2iHJ6Wi9DXJCq3DqeBpRUGXWIXSlyiXtmYID0xddye0DzgbL+JgQR+Fhl4whJtxM1XzNKspqE3/zWtXt0tvSR163ckhYR1tPfGoOEJkBAkebn9YihEPPP7445CF2qPFC6B59CDJHKpxOuYtXYqG8VAQ/eDiSIPovOF3pE9ItRxx9DFPAD8lMUYSEKHj/JprvvTlTQgiVtO8Ma3TaDpwDDBv/bF+YJYcFSIGHLehLqynu0sxDrkH4ht9PgjyXky+yggnpY/qTHH5R5uknRea7X/hYEGCUjMJEOAo4kfwjrotRL0hKMUAvUXTnpNpxc/hXgh+Ye8HMRYD5cA94E+yQV62Ho5720+2NO8r7mhICktflX2X/EpKc2rwvC+4zoOY1IF88HvQapIpWzetkoaW5Yhzgcx1QWAzoPmAjbwMdmpqhxNaIFxwEHDSAU8RqRFNm/zL/mN0es9Y4+UIERdtGDgoghY788HoL1u2BLlk98kVV1yRbGbmonkdVSmc6mkTsizEYy2oRALAcka6bUEiYQugvd9++6Y140pD1ocfethPH3visZ+EEJgM2jjFFl9NnHrqqdVQkwPAOr0q8YxhMWEqEEh/TgZ/4yTBmWsYv83MZIVIj2GvVGegHV+MLq8ZCKxoaKIEtBAeoo04VTPCDl1Snv+ulFe/DIKwWTLidK17JDaQY9zhzCg0iCV1DQfLtneXS1vDHMkABzSz/AWZXvUP5AxtFP9wvlj9CIQGOyif0jlYJfXrj5bG2mMkTDyB+3f54PW6DA3gQniEvBMv2GS9Ou8jDsNk73uy+zkJcD3weLOyM+WkE09QRXAiijH/9tBDSS2kmmUMZyV1oa9ceYxi3y4oCrUs+oQBM4fMbV+4eOHbaLDxSOq1pQkSvrxr7ctrvwX1lc240ojTDLyJpKrwUBieRzs4nzJNf6DEaqyJjDBRSQp8SdU6u6uBeJNuYIY4o9dxgGR6Mrg4nF3NGAyp4hDiizii2EMDeZKT9zoohmelMn8LgCHCQZ5GsYYRJY+gyZQXjHJWN5LYi6V523zZWneytDUeJ27/Njxc5i6ZUr4J8ax3YGkGJNpdDm+tTlwFg4hlzZCtG0+WxvolCFXgc3hv/hAFHNoHRtOK5yop6GY6Bs0Uo7VqrhxjtiOPa7Jik76fjg8wKIsoKAArVx6lXUjYA4FFFeNlQox1NjypUo444nD9KggsmzqpXHidXZ2yfPnytHxtXSijD4a6tR/haUNfKELTrGSLBNvNVzCMGayumm3zRvZjsXbt/if/K40PkWUmDcegI0g5plkgRBFzBwFVmNwFHAIQneXdKlOqV0tVxXOS4YWHGAeZF88DIz0VRCD4HG8UWnWh1HfMlpraYxGumCVZ4IpmTlsjM+b8XXKDGyUBk6epGPhfMAQi1J0l9Q2Hy2s1H8dxyqGxyCMxXom8HoX1NscCD9DSbn02Q67siiNIk7/dye6Z1PSYRfYtZxe35csPkFdffQ2VMYdptsJE5HDqeRwe8IvXXAOztb9ySdqEy96JghKm8nC5tnz5S1+qGn2N21XaHnboId/dvGnzRS6PO89vqzTnR7wokoetba1KoatHYWOf3dU64w8e3RtoIOTXJOgWe+Aqax6fV4aghBIAvC6ww5mBdVKOhylWVj4GDuhViQ+FJDJQpnk3XqRtZEBAYhkx2da0TLasP1taOg+EGAxJZcE6mTPrISmv/CtSK/AbpGn4QQS6kQvkQX5aV7gSE/NpaYPr77MykAtEIeqBp4Xnq6h35RB19EyIgyhW/I6fv3dC5IwXsWgHtATLmShEb7/1lpx22qkqRAxlTPYJUaRayPsdc/SxwIgN+kDDVC1D68OH5Cw/cPnPx5qrMXXtI48++sVNGzf+kGmgqbDQHABOKxjnfJKUqEYwatV0fX1vhImmk6LDsCI0AOIVbvBBLrj0YQRCQ5jwqWWvS+WMx8B8I87FyHsC3hJwShSZhp5AG/CLCyp5nrz19oWIzy0BBo4C120GUfq8VFeukSxfPegiaDk87V3hMVlr8Cd1W1dIzebjpDO2FPwQvDB/LQQ6V+kEl4YsxlTqOkZjKPvJKppJ7eeMO4oVpQJ8z5EwR3h0PYjGQ5B5+W46cTzOETlfTvk6szx/euvP1LHqxrNNnOiB3iFjvCxf93hfuuD88w6YtCBxR5QMvwjZQA+AVGhL9GhKf2jlGBQly+nEsPaMII0ucCTnQmHlI0K5SoB2o73QCAMQoE1SOfNxKSh6XhJD7NWIZ8GBeQUqVpfbHXRJP0zUxroVsq0ZaRcDS8QLISsqfEbmVj0qZYU1GCAA6OFiWGz8xg0clTEsg/Gpsn7dmeCOVuAYUU3FUBhIPgcCRE08ookmNe97dCfHpLH5KZfYxz9+mh7/4x8/Q6t9KBxj9aAc7yIYvbjxxpv1IYQN9SBVNTtCgzH6E4pAb19vz4oVK4+ZNavqpbGOM27rP0Tfv7p27Sv/gAfnITYyFHn6IRy63uGGdl+QNKQIiwDcwZQHu10LeTqC13gCCVwQotKC12TqtLdk+rSHwf9kSqIPrjeY7ESiAhmJqJ7wYoCRy9PccBAaj54n7Yh/MazhBeCeWbJGFs79CxYBhHEI8Ssstyg0lsfqRqJYWPojc/BEyk9JY8d8cWdDM8FN82mzsm6kwAbBG5FgZFB0+xz3PSot4xxshHA0+VAHH3yI7slOwhQibpM1Z84prkFLG2qyjZs2aynTiAfHRcmmq+ibPn3ar8cTItVaE938U0+tuQU5PlfQzU91D52bcQTIsKi7P4wm0EgWnAnx+aZewcdHIOTDzUaNlrdG9oEpmlb5gngjwCpxEI8gBJkLnYDWcIMhDrh7pC+6QF599zSEM/bXrEAfkrw8yB6cv+QWhAeeFk84iKxBAGp0xiFRyPwif1aDtPceIGtf/rj0Dc2TTGQPupiCQUdDb44v80Dm99psTTSSvB5qDOZbIbQlBx90kAa5TzjhOHnuuck9cDvVi/vKV76KZ5ici6Q/OCL6ZICRiTRBcWYNeB48+eSTJmxKOuGywnO9Po98pFcMJ0T6fgRcKwqwU09N+oapGOW2PWs9SSHTACr2Rbafh8LkRzMsrPzhSFAqQm/J8kW/l6kzHoA56oIARVGnVYprQO4PqQDkIXtQk7Wt5xDUt5+BhLBDEL6gPQJlEOmVxUt+IlV4OkI8HAO2QoYDBM+DMIkbg+XPapTmppXy4mtnSGxoqQQzwVpTiIiHNFCqaW8KpPemEKkIK2tNaiEh1cg758awx8svv2JEfBIr2lEAV3/+C3LGmZ+UbQ1bTb6Rs2BIr2hFrcUc9U2LFi+8akczOK5p4w9RrBdDx9UTn37m+Rdhdqe7mTSFP0audUR6CcDJhtsSvKPzjvG9QzgyZICoN+JgiViFuPtdCAQ/JPvP/jN4m3qJDuVhjqdAUzVILFAPZrpcXIzKezOkqfVoeePtU6U/USm+EJnlTsnyb5D9Fv1FikufEKsrD+cFrYGouzeOACkzqQHQu/qXYDL+HRkbSBvJ2qYBUheTkFBAyESwPaJud2FERv/EsQToCqOFkyWlSO3F9sADDyQbcEy0iJ1oA3Ete2FeiKh/Q8M2bTjvELqG9DUB3MHBSDf6U12BuNoOnzq5Q0OPpPmWRQvnn4OCvX47yU6xknk5pcqIpcPLYQKUrhiTzzuJodOoko297Kg9fxWo04qHwe5MmTn3ftl/n3sAm5pA+5dgYjOQt9MKD6sUIYwiCBzcdBT8tXYslTfePUXDFEHkPCfCRUi2F1m+5L+lOO9ZiXYhGR4VHoyye6GJPMiQ9Hv7UGGxXNa+dIFEQkhfQW61C3lIPmZPahYBwwMOmTiJ23mfdmF2pwmUm4VcU7Nux2fGvDjY6frrfyjnX3CB1NVinNSK2JbGmBktchgYGEqUFJX8YP78eZN6yM0OBYlXiCjvmjmzZ108OBCOswaLuSgmKGuyEp10Vi9SD7JQisynaFMzjcZVPFYakOO/AZJdSCiz4jmKjSxvF3jibBB/Ibjmd8qieTfD1AHoAiNZSOXg5FpeuqcIc8AN96Aao7f/YHn1jU8C98yAIBTBeyvDNa6XJYvuRjpojcT78pDtiIg++0W68HgIC15YaJsMuvLRh/EM6R9ahrzqTTg/cq9BXiYYeefg0s1PMtM7nqv3eg/HbNEjIzXjbDkodtBtIrOGRc9K39uQuLZq1SmyDSVmbnBsnDtaNQqPSetxa7p1QUHujw46+MDvT/aeJiVIPNjChQv+MGv2rIu6QMMz6KexHUhv8kUvxigYVJwiXRT0OosDHTfUYVhTmVZlhYm7gISYWsoJZHFeNFwGbuSPst8S4KHuPNCGSLGAJnKjXMflQgYhgrGs1ggEG8As8TFeJ+K9FLkzXSgKbZcYuKTqac/I1PLHUHYMtoCpuLg0H9JlGayMISMxLvlSA/e+HemtAaTCuhEuSaCkJ4GaMBc8Mw35QHvtLe9srAl0uCMz+SNTd/hhh5rdR7nVqXhpxYoV8sCDf5PDDj1cNmzaaATHKbHiPEIhUIiGwV4Hg/5fH3H4YXhOx+S3SQuSCtOCBXegY9vVKO2JmTxpuuk2SGODAU20N+RkDjQTexFyBlm2nJriYITJvkhwMszJIc5xxxAHG6iUzBgCg/P+imWyCZOLyLybyVW81JF4nhuxd6bhbt5yqLQNLhB3BgK0qAGLIdaWm/m8TK1+FCRjPrAQ0jdQ5qwXYgAAcN4wshbmSOPGI5ACa0hI+EK4djLTTJifiGyc/OC+F3tybMkTMevC2dhvk8mHqVrJWbDUMnjClPzmN7dp1ulWPLWTMbTkvNnAmlzS0DCEKBT6ObrCfWZnr32nBIkH33fZ0psWL55/DKo5ulhhojVgeOeL2XSBIN/5MEALJS0Zqk75wJsIuAh0fh0JqZglhOmjiw2GHLXqVgyEY6JOFi39keT6t0oc2YVx1IV5XE6lr42pGIQIDSHneaFsefcimCngmVgxPsU5QAXMBseUg5BJDBkAbmQcugGYncxJbYbgCUptE5LzYzM1OzGOtA/mSJvGm6ms9B7gNHZ2RiaxPxMEWXpFbc8FCuiBB9l8TX+pWY6qnCwGV+Xpp5+Ra7/xDTymolVaWlGYicwOZ74okMyEDMDJQLLaIBpbXL9yxYrtOvZN4pJ2jVWrmln15Lx5+xw3NBiuNTy006hqhB6ggJmovVsy0KenuKhYsyP5kDwKFA2zi2QXOB4LhX2a5I66rKz8jVIydS1q2YsgXKyxJ0GGx76zVIgAH2Sl6iZwQBuQtRhBD4MMYB6ShH3ghHJzUOGBpDULaSIxBGjpvidtLgTF5xuS4f5S6W7ZH6x1L0IhdbB9zE4kifXehzYmMykT7WPCGm4ty8JDaGw6wNIH/CEOptaAxRrf/s53FA/loaP/unXrQaEwLESTPXKPFCQeb3BwuKGkuORyxFm/sqvXt9MayTlR1czKl5btu2x/mJcXtCiAk0s7S+Cmi8K486aLGaLouAmWTaOrfrIZO9LoYY5Qq4XaMZSLacXqjOkvAq/kQIPlQhAQvoCp8RCIC4A4whisBvWg/KcPdfB1zceiKwdypMkj4VwsR64oelP82XTh+S8ILQRVv4WQxb3AVxDIAXh0w9BgHpxPQAO4IKi8lh0n1u/qMO+53zkxTQrFunfeVdjAjZqFXdYuv/wK+cMf7pHzzztfG1u04qmWXkyI9iaw54eOkBZI6LPlomuqq2cevXTZkv/anavcZUHiSdGUs2PJ4sUH5+Vm/4IsqFM6pCXQkCgfLt6P1F2aPFMhgqIbVMeyPUwRgr4e4il27IALbyEdNgASsCS7DtPeht/iXVupIOebtGQiAEFgzK1fnxW3rXkhyoXK4XmAL9KCRz+OF5OCbFIDyF1C/w0KKBNP1L2lmLB1HVjsSDgP/2a9FwP2DEyzgJDOQjJxZnfG9D39rYN9iHOYhbG+Zr0uYD7KlYLDhyKz/KupCXFD3A/LzJw+5nx3/qYChvDdvmjBgmNnzphRs7sXPSEhOZmDIy2XuvLSrXV1d9fX1f0OF1rJi+WWXpxIvoigGRoIN56bm43qkQwwp1Fk4iFtAx028gvXox5sI1JAMsE452FiEQdDvZkLYDlB88aoPcxfNFIqvZ3zIZjMhzZl1jHkKgWtiGTmbAUFwNAHTBu6kbFE2Y087gTO7YZHiEpHAHimmgI3MSkPgskgr4XGDxbTVCZz03thn9QsC0YQ4hAStgBsbgZxi/twyqqTZegpWInClxoWAbbqQDn899FF94d76lZ2W5CcC6mcPn01unosa2iovw0dPU41j/M0KbjczECoEk56cFTHfn8RaqfyUEnbJzmhGAwYvCgWIEo7Jh82nGVF4HaY6+NG5N4DD25wMAflzLM1jGFsPivwSUUAb/kAnJFFqc2kCOAhgBBH7MPke+KguPjRqSzuAb4A34SILD5DkQBzaJPk4/srTskENXucRoc5+D0Xpd24QTmkHCxEahuCa2arOmTwaMFIjYuy4BSW4Znq6qrz8DAc7SKyp7Y9Jki8IND2XXg7bcuW2k+1tDZ/Hetgngn4kitz8rZTKyJYft0CgSmR/Nw+7PevUts8SwpynkdzqbWoAXsN2gvlQShcjLnYsweCAr4nFoWrCnbaCyFgDEzhox4flbkkEZFikiD+YfsWkJtog65CYsGtTyT6JZjTqd3NLNSesbGVhUQ1Cp+ZwMkw8ntm+B1y1mmPo6ZWexOYa6DwOJ4ZO6Ow72R+Xg5cdNwj88LgtTmFqeOFRnhP3A+vWjQt/RkenPwjhL72uA3fo4LkDC8KJFlh8Ht0qri0q6vz/4FzmuI8a9Z4cnZyKlrK0Y6z3ov9mqwEnx9yuDR149m0qCnLCNajl9E94s98HYn1PUjCB+GITMdIdD4mHgCfpc7QQpRU1u2HkZTlisAzg3scZ6cQBGaptJjLxLRYD0uCkI6bE2qR0vwaaRxYCE4JTapYCcQmESwd2jMyMu5RUic8PQ2WVawmOZBeLQWEWp19Jlntmo0iUhYmmg5wzvOAzfPzuDmktqOB+DnTTFBZG0FVyO2zqmd/Acciw/qebO/1uLHWLa+1pfW7eMDyuf5gMNt51JV6DdrcEpdgFw/QRNFzSqC1nQyUYg8GJdEbyNsgWcENqMN/UXKy/o5sxynyzEuXgBLIhUuLKD67bgirZodk6aLfSUUeEt2AGxKBXvEOodAR9IKFhHymgbjZ0wiVt32xfHnphWtRRjQfAd7N+ByPLrVTZScz0qMFgr9xPhudJ53K6jt4xYlTMnWZxROmiIIxS7/Wk7H7f4a2PGYrY3RBYX9JOz9rXK1pe/Z4LAoEKGwBOjxcUlr2JaRFmycnvofbey5IzrWjOUQxMNQ16Ip2OQYrk4OjhoSplgRP9JicZcXKU3ZAUwFjcSVjd/jLykcNfis+QyVvL9rE9CAlBAV8eO4H9mGbOjwVqPJxPB/3dhR1gDOJT8Fv+iXGkmiAbVahaJYjvLQQzr9125HyyttnoLEV00jp0bHlC8+542FxTFAyrUO1gkkz4YRrCCklBZlCQoxDqxWHV6m3C6eDXm0AzUvpeDAa4Ic3FuBjiLDI2PeJAVpNNbbTeMzCs/sWkFezr9YNL5ml9hE0NvW6vX9Hif1/oKJk+4e0vUfCtOMR28MnxhO6p3R2dvwY7X1Pw0AjaQC52Eo02tiEOEErReCpsSIDJoddVj1s9QJ+ic0WPPDkQi6QiMA2fGYIU06h8dD2DgDftUYWz74P+GobgHQ3xAa1ZjBpNKA0Xx407mQbGDamEmQ8bqw9FLncX4ZWAn0RYBeOEQdholtPix1Sm2gWg7kHTqjRUOZxpQQkmFxdLwGEKVhEmoGmGohpSQiC44UW0pwu8q924aITlTckKTcK0Mif/JuyRWElBsITwdFvwvUkenffNH3atL/s4Wnb4eHed0Fyrqizo6Oiq6f3i/A4zkWjg2KWitPQMeWaI0YGKK7dxhg85VQQ57DLGCYG+Ifaw402MMzq48aO9Ka1bz/8QkS2kUng8YHcBOvNnkd+9xb93IM8J3Y14yT7Pe04Rh5yuo9EHtPp4LNytULWNF4Ye3OExXTqp6AbnaAmG1pIW74AyzADIllAiu4qbCvILiDoBKvmmzlBpv8kQDO0jtmMQzJ6UtQM8iz2d+aMHB9tvTwA7PQEuLyb4YmlFS3ucPb34A57TZCcewBV4MMThS7HI6GuRB7TTJKZjEo7/pPBD6Z+zsWusSybhmufsFhjRV7KiS0Z2tFCPhIFQRtHKD0A4UtAqNhAgvVvCaSg4HOPG49JgLZC3xBoiT5papuHljPVmGh0HtGMUFtGHCWg5zdKgYLiBltMp8GHkmbml3v5YGHtL0XawwgL/xvdMc5hptm6jzuYKP7Y0zDiutNUGvExGovX51kHovdueHK3IiSCzL69u+11QUq9fXSFO6i/r/8ytCY+HgJVoqucHpn9FEjqKWflpg9+ittuuAbDZCcnyHiJOhEkRRk+gfaKg+hklawHeUhegHFQfKrZVOvYUCTNrVbZGIknOk07R7veY+Vhpd6nwVXmihwCMZ1LMnAx9TjRmOaP9yBisDorM/vXWTnZD2dmZGzfKGovydMHSpBSx6Cjo+tIBIUvQSzoaAxgGXu8cHDTvZ5dHTVOIlvKmPbGJkVFD24TpyZdxRCBzoSbzxzvyhEAp0GY44U5VzSaVNzRlaZ6ejwPG1qpiXe7G/F6DoTj7xFFeAgtoD+Qj2D4wApS0vQNDvqAA8oAplchJ/xEhFQWAl9UYNZ9hsAzJkarye27cXKdUvSU0Uf297ovYncmvGa3jtEObjSFuzckjklMNY0qqrapHH2NvC4jhOahhhCoHgjO5kDAdx9aU68OhoJr0Rc8/YmIO5LKvfD97o3aXrhgNI0KIBWlCgJ1HDyVM0DQLcJE5JBO4OPPHaBszIeW0iQxlgGzBkg7WEX3w7M+SFoaz8uuGHb86tHvY9yzsaZmKI3wGnOcXghhrsf5XPWNhWI6kSYIzusw4y+h5eHzEJ5X0MHOdBP9EG0fOkEaPbZ4Hm0IZF0RWNxqaK7ZMIXzIQtzMLHTMVHTMUk56b+xJUMtGWvobNzFzEk7r4r8jgFJtppLfVdRSYkw0BxCQBTqqyokOmOjMge+WP3oD4VOFtIEQ7UNGGwdhLkWyX+vo6FnDYSnFzGzPR6yeL9l8EMvSBMNGPo4ZUCwijHBy0AGHobnly3D36hlQk0SanF1xrUzOzNajECZt+2za9QEqoABN5FYNNLEZ1QRs1CDgPWUFhCDfMxmLV4NEOJaCHQ9tE0nPCu0Pvvobh9pQRpr2sCse2Fi/BAoYq8AvOlcCAcEiy9XYSwKCsLlohZDfozmvVC1hKGvKCh4AnliAK5/B/iidiggCIe7G0qpC7itD4Iz/FHQLh9dcf/nnX3gR+D/A/mQapoON3vLAAAAAElFTkSuQmCC";
      doc.setTextColor(0, 0, 0);
      doc.setFontSize(10);
      //doc.addFont("Tahoma", "Tahoma", "Tahoma");
      doc.setFont("Tahoma");
      doc.text(60, 10, "KENYA NATIONAL HIGHWAYS AUTHORITY");
      doc.text(70, 15, "Quality Highways, Better Connections");
      doc.text(80, 25, "Axle Load Control");
      doc.text(150, 30, "Print Date: " + cdatetime);

      doc.text(10, 40, "Scale Test (Multideck)");
      doc.text(70, 40, "Weighbridge Name:ATHIRIVER MOMBASA BOUND A");
      doc.setFontSize(14);
      doc.addFont("Tahoma", "Tahoma", "bold");
      doc.setFont("Tahoma", "Tahoma", "bold");
      doc.text(180, 40, "");
      doc.setFontSize(12);

      doc.setFillColor(255, 255, 255);

      doc.roundedRect(5, 47, 43, 5, 1, 1, "FD");
      doc.roundedRect(50, 47, 100, 5, 1, 1, "FD");

      doc.roundedRect(5, 55, 43, 5, 1, 1, "FD");
      doc.roundedRect(50, 55, 100, 5, 1, 1, "FD");

      doc.roundedRect(5, 60, 43, 5, 1, 1, "FD");
      doc.roundedRect(50, 60, 100, 5, 1, 1, "FD");

      doc.roundedRect(5, 65, 43, 5, 1, 1, "FD");
      doc.roundedRect(50, 65, 100, 5, 1, 1, "FD");

      doc.roundedRect(5, 70, 43, 5, 1, 1, "FD");
      doc.roundedRect(50, 70, 100, 5, 1, 1, "FD");

      doc.roundedRect(5, 80, 40, 15, 1, 1, "FD");
      doc.roundedRect(50, 80, 40, 15, 1, 1, "FD");
      doc.roundedRect(95, 80, 40, 15, 1, 1, "FD");
      doc.roundedRect(140, 80, 40, 15, 1, 1, "FD");

      doc.roundedRect(5, 93, 40, 5, 1, 1, "FD");
      doc.roundedRect(50, 93, 40, 5, 1, 1, "FD");
      doc.roundedRect(95, 93, 40, 5, 1, 1, "FD");
      doc.roundedRect(140, 93, 40, 5, 1, 1, "FD");

      doc.addImage(img, "PNG", 10, 10);

      doc.output("dataurlnewwindow");
    },
    dailyHourData() {
      //alert();
      //let pdfName = 'WeighbridgeReceipt';
      var doc = new jsPDF();
      const cdatetime = new Date().toString().split(" ").splice(0, 5).join(" ");
      //This is a key for printing
      doc.setFillColor(255, 255, 255);
      doc.roundedRect(5, 5, 200, 40, 1, 1, "FD");
      var img =
        "data:image/png;base64,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";
      doc.setTextColor(0, 0, 0);
      doc.setFontSize(10);
      //doc.addFont("Tahoma", "Tahoma", "Tahoma");
      doc.setFont("Tahoma");
      doc.text(60, 10, "KENYA NATIONAL HIGHWAYS AUTHORITY");
      doc.text(70, 15, "Quality Highways, Better Connections");
      doc.text(80, 25, "Axle Load Control");
      doc.text(140, 30, "Print Date: " + cdatetime);

      doc.text(10, 40, "Daily Hourly Report");
      doc.text(70, 40, "Weighbridge Name:ATHIRIVER MOMBASA BOUND A");
      doc.setFontSize(14);
      doc.addFont("Tahoma", "Tahoma", "bold");
      doc.setFont("Tahoma", "Tahoma", "bold");
      doc.text(180, 40, "");
      doc.setFontSize(12);

      doc.setFillColor(255, 255, 255);

      doc.addImage(img, "PNG", 10, 10);


       var col1 = ["Details", "Values"];
       var rows1 = [];
      var itemNew = [

        { index:'1',id: 'Case Number', name : '101111111' },
        { index:'2',id: 'Patient Name', name : 'UAT DR' },
        { index:'3',id: 'Hospital Name', name: 'Dr Abcd' }

      ]


         itemNew.forEach(element => {      
             
              var temp1 = [element.id,element.name];
          
              rows1.push(temp1);

          });        


        doc.autoTable(col1, rows1, { startY: 50 });

      doc.output("dataurlnewwindow");
    },
    mydate(d, day, mon, yea) {
      let year = d.getFullYear() - yea;
      let month = d.getMonth() + 1 - mon;
      //alert("month" + month);
      let date = d.getDate() - day;
      date = this.getv(date);
      month = this.getv(month);

      //const msec = d.getMilliseconds();
      const datetime = year + "-" + month + "-" + date;
      return datetime;
    },
    getv(val) {
      if (val < 10) {
        val = "0" + val;
      }
      return val;
    },
    genrpt() {
      //alert(this.report);
      if (this.report == "Census Report") {
        this.censusreport();
      }
    },
    getdatefilter() {
      //alert(this.mydate(new Date(), 0, 0, 0));
      this.todate = new Date(this.mydate(new Date(), 0, 0, 0));
      if (this.picked == "Day") {
        this.fromdate = new Date(this.mydate(new Date(), 1, 0, 0));
      }
      if (this.picked == "week") {
        this.fromdate = new Date(this.mydate(new Date(), 7, 0, 0));
      }
      if (this.picked == "month") {
        this.fromdate = new Date(this.mydate(new Date(), 0, 1, 0));
      }
      if (this.picked == "year") {
        this.fromdate = new Date(this.mydate(new Date(), 0, 0, 1));
      }
    },
    getrptstate() {
      //alert(this.report);
      this.hswim = false;
      this.showdata = false;
      this.showtickets = false;
      this.showhour = false;
      this.showdate = false;

      if (this.report == "Census Report" || this.report == "Wide Load") {
        this.showdate = true;
        this.showdata = true;
        this.showhour = true;
      }

      if (
        this.report == "Auto Generate Reports" ||
        this.report == "Daily Hour Data" ||
        this.report == "Daily Hour Statistics" ||
        this.report == "Daily & Hourly Register" ||
        this.report == "Daily Summary"
      ) {
        this.showdata = true;
        this.showhour = true;
        this.showdate = true;
      }
      if (this.report == "Auto Generate Reports") {
        this.showdata = true;
        this.showhour = true;
        this.showdate = true;
      }
      if (this.report == "Manual Weighing") {
        this.showhour = true;
        this.showdate = true;
      }
    },
    handleSubmit() {
      console.log("Error on submit");
    },
    /**
     * Generate Wb register report
     */
    goToWbRegisterReport() {
      this.$router.push("/wbregisterreport");
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />

    <div class="row">
      <div class="card">
        <div class="card-body">
          <form @submit.prevent="handleSubmit">
            <div class="row">
              <div class="card col-md-2"></div>
              <div class="card col-md-8 changebg">
                <div class="card-body">
                  <div class="card">
                    <div class="card-body">
                      <div class="col-sm-12">
                        <div class="input-group">
                          <div class="input-group-text col-sm-2">
                            Report For:
                          </div>
                          <div class="col-sm-10">
                            <multiselect
                              @select="getrptstate()"
                              v-model="report"
                              :options="reports"
                              placeholder="Select Report"
                            ></multiselect>
                          </div>
                        </div>
                      </div>

                      <div class="col-sm-12">
                        <div class="input-group">
                          <div class="input-group-text col-sm-2">
                            <input
                              type="checkbox"
                              id="mail_checkbox"
                              v-model="mail_checkbox"
                            />
                            <label for="checkbox"></label>
                            Send Mail:
                          </div>
                          <input
                            class="form-control"
                            type="text"
                            placeholder="Type in Your Email Address"
                            v-model="Length"
                          />
                        </div>
                      </div>
                    </div>
                  </div>

                  <!--Date specification -->
                  <div class="card" v-show="showdate">
                    <div class="card-body">
                      <div class="row col-sm-12">
                        <div class="input-group">
                          <div class="col-sm-2">
                            <input
                              type="radio"
                              id="Day"
                              value="Day"
                              v-model="picked"
                              @change="getdatefilter()"
                            />
                            <label class="form-check-label">Day</label>
                          </div>

                          <div class="col-sm-2">
                            <input
                              type="radio"
                              id="week"
                              value="week"
                              v-model="picked"
                              @change="getdatefilter()"
                            />
                            <label>Week</label>
                          </div>

                          <div class="col-sm-2">
                            <input
                              type="radio"
                              id="month"
                              value="month"
                              v-model="picked"
                              @change="getdatefilter()"
                            />
                            <label for="checkbox">Month</label>
                          </div>

                          <div class="col-sm-2">
                            <input
                              type="radio"
                              id="year"
                              value="year"
                              v-model="picked"
                              @change="getdatefilter()"
                            />
                            <label>Year</label>
                          </div>

                          <div class="col-sm-2">
                            <input
                              type="radio"
                              id="specify"
                              value="specify"
                              v-model="picked"
                              @change="getdatefilter()"
                            />
                            <label for="checkbox">Specify</label>
                            {{ picked }}
                          </div>
                        </div>
                      </div>
                      <!-- Date Selection -->
                      <div class="row">
                        <div class="col-sm-6">
                          <div class="input-group">
                            <div class="input-group-text col-sm-12">
                              <div class="col-sm-3">From Date:</div>
                              <div class="col-sm-9">
                                <date-picker
                                  value="2021/05/17"
                                  v-model="fromdate"
                                  :first-day-of-week="1"
                                  lang="en"
                                  placeholder="Pick a Date"
                                ></date-picker>
                              </div>
                            </div>
                          </div>
                        </div>

                        <div class="col-sm-6">
                          <div class="input-group">
                            <div class="input-group-text col-sm-12">
                              <div class="col-sm-3">To Date:</div>
                              <div class="col-sm-9">
                                <date-picker
                                  value="2021/05/17"
                                  v-model="todate"
                                  :first-day-of-week="1"
                                  lang="en"
                                  placeholder="Pick a Date"
                                ></date-picker>
                              </div>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!--new Time specification-->
                  <div class="card" v-show="showhour">
                    <div class="card-body">
                      <div class="row">
                        <div class="col-sm-6">
                          <div class="input-group">
                            <div class="input-group-text col-sm-12">
                              <div class="col-sm-3">From Hour:</div>
                              <div class="col-sm-9">
                                <multiselect
                                  v-model="fromhour"
                                  :options="hours"
                                  placeholder="Select Report"
                                  :allow-empty="false"
                                ></multiselect>
                              </div>
                            </div>
                          </div>
                        </div>

                        <div class="col-sm-6">
                          <div class="input-group-text col-sm-12">
                            <div class="col-sm-3">To Hour:</div>
                            <div class="col-sm-9">
                              <multiselect
                                v-model="tohour"
                                :options="hours"
                                placeholder="Select Report"
                                :allow-empty="false"
                              ></multiselect>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>

                  <div class="card" v-show="showtickets">
                    <div class="card-body">
                      <!-- Ticket Number  -->
                      <div class="col-sm-6">
                        <div class="input-group">
                          <div class="input-group-text col-sm-5">
                            Ticket No:
                          </div>

                          <input
                            style="font-size: 12px"
                            class="form-control"
                            list="TicketList"
                            id="TicketList"
                            placeholder="Enter Ticket Number"
                            v-model="ticket"
                          />
                          <datalist
                            id="TicketList"
                            v-for="ticket in tickets"
                            :key="ticket"
                          >
                            <option
                              v-for="ticket in tickets"
                              :key="ticket"
                              :value="ticket"
                            ></option>
                          </datalist>
                        </div>
                      </div>
                    </div>
                  </div>

                  <!--Registration specification -->

                  <div class="card" v-show="showdata">
                    <div class="card-body">
                      <div class="row">
                        <div class="col-sm-6">
                          <div class="input-group">
                            <div class="input-group-text col-sm-3">Cargo:</div>
                            <div class="col-sm-9">
                              <multiselect
                                v-model="cargo"
                                :options="cargos"
                                placeholder="Select Status"
                                :multiple="true"
                              ></multiselect>
                            </div>
                          </div>
                        </div>

                        <!-- registration -->
                        <div class="col-sm-6">
                          <div class="input-group">
                            <div class="input-group-text col-sm-4">
                              Registration:
                            </div>
                            <div class="col-sm-8">
                              <multiselect
                                v-model="reg"
                                :options="regs"
                                placeholder="Select"
                                :multiple="true"
                              ></multiselect>
                            </div>
                          </div>
                        </div>
                      </div>

                      <!-- Row 2 -->

                      <div class="row">
                        <div class="col-sm-6">
                          <div class="input-group">
                            <div class="input-group-text col-sm-3">Status:</div>
                            <div class="col-sm-9">
                              <multiselect
                                v-model="status"
                                :options="statuses"
                                placeholder="Select Status"
                                :multiple="true"
                              ></multiselect>
                            </div>
                          </div>
                        </div>

                        <!-- registration -->
                        <div class="col-sm-6">
                          <div class="input-group">
                            <div class="input-group-text col-sm-4">
                              Weighbridge:
                            </div>
                            <div class="col-sm-8">
                              <multiselect
                                v-model="wb"
                                :options="wbs"
                                placeholder="Select Status"
                                :multiple="true"
                              ></multiselect>
                            </div>
                          </div>
                        </div>
                      </div>

                      <div class="row">
                        <div class="col-sm-6">
                          <div class="input-group">
                            <div class="input-group-text col-sm-3">Users:</div>
                            <div class="col-sm-9">
                              <multiselect
                                v-model="user"
                                :options="users"
                                placeholder="ALL"
                                :multiple="true"
                              ></multiselect>
                            </div>
                          </div>
                        </div>

                        <div class="col-sm-6">
                          <div class="input-group">
                            <div class="input-group-text col-sm-4">
                              Transporter:
                            </div>
                            <div class="col-sm-8">
                              <multiselect
                                v-model="transporter"
                                :options="transporters"
                                placeholder="All"
                                :multiple="true"
                              ></multiselect>
                            </div>
                          </div>
                        </div>
                      </div>

                      <div class="row">
                        <div class="col-sm-6">
                          <div class="input-group">
                            <div class="input-group-text col-sm-3">Shift:</div>
                            <div class="col-sm-9">
                              <multiselect
                                v-model="shift"
                                :options="shifts"
                                placeholder="ALL"
                                :multiple="true"
                              ></multiselect>
                            </div>
                          </div>
                        </div>

                        <div class="col-sm-6">
                          <div class="input-group">
                            <div class="input-group-text col-sm-4">
                              Axle Type:
                            </div>
                            <div class="col-sm-8">
                              <multiselect
                                v-model="axle"
                                :options="axles"
                                placeholder="Select Status"
                                :multiple="true"
                              ></multiselect>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!-- ////////////0000 -->
                  <div class="card" v-show="hswim">
                    <div class="card-body">
                      <div class="col-sm-6">
                        <div class="input-group">
                          <div class="input-group-text col-sm-4">
                            HSWIM Lane:
                          </div>
                          <div class="col-sm-7">
                            <multiselect
                              v-model="lane"
                              :options="lanes"
                              class="form-control"
                              placeholder="ALL"
                              :multiple="true"
                            ></multiselect>
                          </div>
                        </div>
                      </div>

                      <div class="row col-sm-12">
                        <div class="input-group">
                          <div class="col-sm-2">
                            <input
                              type="checkbox"
                              class="form-check-input"
                              v-model="Calledin"
                            />
                            <label class="form-check-label">Called In</label>
                          </div>

                          <div class="col-sm-2">
                            <input
                              type="checkbox"
                              class="form-check-input"
                              v-model="Overloaded"
                            />
                            <label>Overloaded</label>
                          </div>

                          <div class="col-sm-2">
                            <input
                              type="checkbox"
                              class="form-check-input"
                              v-model="month_checkbox"
                            />
                            <label for="checkbox">Overspeeding</label>
                          </div>

                          <div class="col-sm-2">
                            <input
                              type="checkbox"
                              class="form-check-input"
                              id="year_checkbox"
                              v-model="Trucks"
                            />
                            <label for="checkbox">Trucks Only</label>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>

                  <!-- ///buttons -->
                  <div class="row">
                    <div class="col-sm-10 text-right">{{ report }}</div>
                    <div class="col-sm-2">
                      <b-button
                        pill
                        variant="outline-primary"
                        v-on:click="genrpt()"
                        style="margin-right: 10px"
                      >
                        Generate</b-button
                      >
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
  </Layout>
</template>
<style scoped>
.changebg {
  background-color: rgb(240, 240, 240);
}
</style>