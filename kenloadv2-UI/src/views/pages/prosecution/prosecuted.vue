<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";
// import moment from 'moment'

//import { tableData } from "./dataAdvancedtable";
import DatePicker from "vue2-datepicker";
import jsPDF from "jspdf";
import Swal from "sweetalert2";
import axios from "axios";
import Multiselect from "vue-multiselect";

import "jspdf-autotable";

export default {
  page: {
    title: "Prosecute",
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
    //tableData,
  },
  data() {
    return {
      title: "Prosecuted",
      items: [
        {
          text: "USER: COP",
        },
        {
          text: "Prosecuted",
          active: true,
        },
      ],
      problist: [
        "Prohibition Order",
        "Conditional Load Correction Memo",
        "Charge Sheet",
        "Billing Instruction Sheet",
        "Load Correction Memo",
      ],
      receiptno: "",
      wbTicket: [],
      casedischarge: "",
      casebasis: "",
      finetotal: 0,
      parkingfee: 0,
      loaderfine: 0,
      transporterfine: 0,
      driverfine: 0,
      ownerfine: 0,
      finetot: "",
      tots: "",
      casebasisopt: [
        "Case discharge on fact bases",
        "Cash bill",
        "Cash Bond",
        "Court fine",
        "Fine fully paid",
        "Fine partially paid",
        "Partially paid",
      ],
      dec: [],
      billingto: "",
      checkedNames: "",
      selectedIndex: "",
      from: "",
      froms: "",
      destination: "",
      cdate: "",
      origins: [],
      selectedData: {
        driverslicense: "",
        driverid: "",
        dlicense: "",
        dnames: "",
        dsurname: "",
        dgender: "",
        dnationality: "KENYAN",
        paystation: 1,
        permitno: "",
        dage: "40",
        daddress: "",
        oid: "",
        onames: "",
        osurname: "",
        ogender: "",
        onationality: "KENYAN",
        oage: "40",
        oaddress: "",
        vroad: "",
        district: "",
        weighbridge: "",
        weighbridgecode: "",
        county: "",
        court: "",
        police: "",
        tpolice: "",
        vi: " ",
        moveto: "",
        rneeded: "",
        offload: "",
        Tnames: "",
        Taddress: "",
      },
      mytitle: "Case Details for Reg.  Ticket No. ",
      prosecute: false,
      prohibitsToAdd: [
        "Prohibition Order",
        "Conditional Load Correction Memo",
        "Charge Sheet",
        "Billing Instruction Sheet",
        "Load Correction Memo",
      ],
      Others: false,
      Overload: false,
      Parking: false,
      Absconding: false,
      othersdetails: "",
      overloadw: 0,
      amtpayable: 0,
      parkingtodate: "",
      parkingfromdate: "",
      abscondingdate: "",
      LoadCtitle: "",
      BillingStitle: "",
      getdata: "",
      caseid: "",
      tot: 0,
      caseregno: "",
      caseticket: "",
      axle_code: "",
      id: 0,
      editmode: false,
      nplate: "",
      driverid: "",
      dnames: "",
      dsurname: "",
      dgender: "",
      dnationality: "Kenyan",
      paystation: 1,
      permitno: "",
      dage: "40",
      daddress: "",
      oid: "",
      onames: "",
      osurname: "",
      ogender: "",
      onationality: "Kenyan",
      oage: "40",
      oaddress: "",
      vroad: "",
      district: "",
      weighbridge: "",
      county: "",
      court: "",
      police: "",
      tpolice: "",
      tpolices: [],
      vi: "",
      VIs: [],
      moveto: "",
      rneeded: "",
      offload: "",
      Tnames: "",
      Taddress: "",
      index: "",
      autoClose: "",
      shift: null,
      route: null,
      hour: null,
      gvtimglogo: require("@/assets/images/prosecution/Coatofarmskenya.png"),
      kenhaimglogo: require("@/assets/images/prosecution/kenhalogo.png"),
      kenhalonglogo: require("@/assets/images/kenhalonglogo.png"),
      kenhalogo: require("@/assets/images/client.png"),
      policeimglogo: require("@/assets/images/prosecution/kenyapolice.png"),
      eacimglogo: require("@/assets/images/prosecution/eacact.png"),
      shifts: ["Shift A", "Shift B", "Shift C"],
      routes: ["Nairobi-Mombasa", "Mombasa-Nairobi"],
      hours: [
        "00",
        "01",
        "02",
        "03",
        "04",
        "05",
        "06",
        "07",
        "08",
        "09",
        "10",
        "11",
        "12",
        "13",
        "14",
        "15",
        "16",
        "17",
        "18",
        "19",
        "20",
        "21",
        "22",
        "23",
      ],
      ticketsshow: [],
      yardlist: [],
      rep: [],
      tableData: [],
      orderData3: [],
      fields3: [
        {
          key: "id",
          label: "Case ID",
          sortable: true,
        },
        {
          key: "casebasis",
          label: "Case Basis",
          sortable: true,
        },
        {
          key: "reason",
          label: "Reason",
          sortable: true,
        },
        {
          key: "receiptno",
          label: "Receipt / Mpesa No.",
          sortable: true,
        },
        {
          key: "ownercharge",
          label: "Owner Charge",
          sortable: true,
        },
        {
          key: "drivercharge",
          label: "Driver Charge",
          sortable: true,
        },
        {
          key: "transportercharge",
          label: "Transporter Charge",
          sortable: true,
        },
        {
          key: "loadercharge",
          label: "Loader Charge",
          sortable: true,
        },
        {
          key: "paystation",
          label: "station Fee",
          sortable: true,
        },
        {
          key: "parkingfee",
          label: "Parking Fee",
          sortable: true,
        },
        "total",
        "action",
      ],
      orderData: [
        //   {
        //     id: "1",
        //     caseticket: "WBT0001",
        //     caseno: "202107290001",
        //     caseregno: "KBA857V",
        //     casedatetime: "2018/07/29 21:30 Hrs",
        //     caseexcessload: "48000",
        //     documents: "",
        //     status: "prohibited",
        //     createdby: "James",
        //     prohibitionOrder: "PR00004",
        //     prohibitionOrderNo: "PO20200700000001",
        //     oChargeSheetNo: "OCH00004",
        //     dChargeSheetNo: "DCH00004",
        //     specialReleaseNo: "SR00001",
        //     complianceNo: "CCEABL00000001",
        //     parkingFees: "Ksh 1000.00",
        //     subtotal: "Ksh 2000.00",
        //     total: "Ksh 3000.00",
        //     prohibits: [],
        //     prohibitsToAdd: [
        //       "Prohibition Order",
        //       "Conditional Load Correction Memo",
        //       "Charge Sheet",
        //       "Billing Instruction",
        //       "Load Correction Memo",
        //     ],
        //     data: {
        //       driverid: "29986734",
        //       dlicense: "KENT45556",
        //       dnames: "Ahmed Hajo",
        //       dsurname: "Hajo",
        //       dgender: "Male",
        //       dnationality: "Kenya",
        //       dage: "39",
        //       daddress: "P.O.Box 58669 - 00200 NAIROBI - KENYA",
        //       oid: "27835462",
        //       onames: "Ali Yusuf",
        //       osurname: "Yusuf",
        //       ogender: "Male",
        //       onationality: "Kenya",
        //       oage: "45",
        //       oaddress: "P.O.Box 58670 - 00200 NAIROBI - KENYA",
        //       vroad: "Mombasa Road",
        //       district: "Athi River",
        //       weighbridge: "Athi River Mombasa Bound",
        //       weighbridgecode: "ATH",
        //       county: "Machakos",
        //       court: "Mavoko Courts",
        //       police: "Jane Mutua",
        //       tpolice: "Lucy Kibaki",
        //       vi: "Eunice Musyoka",
        //       moveto: "Yard",
        //       rneeded: "Brake Light",
        //       offload: "KCV756D",
        //       Tnames: "Yusuf Transporters LTD",
        //       Taddress: "P.O.Box 58668 - 00200 NAIROBI - KENYA",
        //     },
        // },
        //   {
        //     id: "2",
        //     caseticket: "WBT0002",
        //     caseno: "202107290001",
        //     caseregno: "KBA857V",
        //     casedatetime: "2018/07/29 22:05 Hrs",
        //     caseexcessload: "48000",
        //     documents: "",
        //     status: "prohibited",
        //     createdby: "James",
        //     prohibitionOrder: "PR00004",
        //     prohibitionOrderNo: "PO20200700000001",
        //     oChargeSheetNo: "OCH00004",
        //     dChargeSheetNo: "DCH00004",
        //     specialReleaseNo: "SR00001",
        //     complianceNo: "CCEABL00000001",
        //     parkingFees: "Ksh 1000.00",
        //     subtotal: "Ksh 2000.00",
        //     total: "Ksh 3000.00",
        //     prohibits: [],
        //     data: {},
        //   },
      ],
      totalRows: 1,
      viewProsecuted: false,
      addProsecuted: false,
      editProsecuted: false,
      printReport: false,
      clickedPrintpdf: "",
      filteredValues: "",
      printedpdf: true,
      generatedExel: "",
      currentPage: 1,
      perPage: 25,
      pageOptions: [
        1, 2, 5, 10, 25, 50, 100, 200, 300, 400, 500, 1000, 2000, 3000, 5000,
        10000,
      ],
      filter: null,
      filterOn: [],
      sortBy: "casedatetime",
      sortDesc: true,
      fields: [
        {
          key: "id",
          sortable: true,
          label: "Id",
        },
        {
          key: "caseid",
          sortable: true,
          label: "Probition No.",
        },
        {
          key: "alongroad",
          label: "alongroad",
          sortable: true,
        },
        {
          key: "caseregno",
          sortable: true,
          label: "Vehicle Number",
        },
        {
          key: "casedatetime",
          sortable: true,
          label: "Date Time",
        },
        {
          key: "caseexcessload",
          sortable: true,
          label: "Excess Load",
        },
        {
          key: "casedocs",
          sortable: true,
          label: "Documents",
        },
        {
          key: "status",
          sortable: true,
          label: "Status",
        },

        {
          key: "prohibit",
          label: "Action",
        },
        {
          key: "paystation",
          label: "station Fee",
          sortable: true,
        },
      ],

      totalRows2: 1,
      currentPage2: 1,
      perPage2: 2,
      pageOptions2: [1, 2, 5, 10, 25, 50, 100, 200],
      filter2: null,
      filterOn2: [],
      sortBy2: "id",
      sortDesc2: false,
      fields2: [
        {
          key: "id",
          label: "#",
          sortable: true,
        },
        {
          key: "wbrg_ticket_no",
          label: "Ticket No.",
          sortable: true,
        },

        {
          key: "gvw",
          label: "GVW [KG]",
          sortable: true,
        },
        {
          key: "alongroad",
          label: "alongroad",
          sortable: true,
        },
        {
          key: "wbrg_ticket_axleload",
          label: "Excess Axle",
          sortable: true,
        },
        {
          key: "wbrg_ticket_gvwload",
          label: "Excess GVW",
          sortable: true,
        },
        {
          key: "wbrg_ticket_status",
          label: "Status",
          sortable: true,
        },

        "Action",
      ],
    };
  },
  watch: {
    viewProsecuted(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Opened Prosecuted list:${newValue}`,
        computer: window.localStorage.clientip,
      };
      axios
        .post(window.$http + "AuditLogs", data, { headers: window.$headers })
        .then((response) => {
          console.log(response.data);
        })
        .catch((e) => {
          console.log(e);
        });
    },
    printReport() {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Prosecuted reports:${this.clickedPrintpdf},${this.generatedExel},${this.filteredValues}`,
        computer: window.localStorage.clientip,
      };
      axios
        .post(window.$http + "AuditLogs", data, { headers: window.$headers })
        .then((response) => {
          console.log(response.data);
        })
        .catch((e) => {
          console.log(e);
        });
    },
    addProsecuted(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Added Documents:${newValue}`,
        computer: window.localStorage.clientip,
      };
      axios
        .post(window.$http + "AuditLogs", data, { headers: window.$headers })
        .then((response) => {
          console.log(response.data);
        })
        .catch((e) => {
          console.log(e);
        });
    },
    editProsecuted(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Edited Case details:${newValue}`,
        computer: window.localStorage.clientip,
      };
      axios
        .post(window.$http + "AuditLogs", data, { headers: window.$headers })
        .then((response) => {
          console.log(response.data);
        })
        .catch((e) => {
          console.log(e);
        });
    },
  },
  computed: {
    /**
     * Total no. of records
     */
    rows() {
      return this.orderData.length;
    },
  },
  mounted() {
    // Set the initial number of items
    //alert(window.$station);
    //alert(window.$tokenString);
    this.from = window.$stationcode;
    this.froms = window.$station;
    this.cdate = new Date();
    this.calc();
    this.updatearray();
    this.fetchusers();
    this.viewProsecuted = true;
    this.getorigin();

    this.defaultvals();
    //this.totalRows = this.items.length;
  },
  methods: {
    calc() {
      this.finetotal =
        Number(this.parkingfee) +
        Number(this.loaderfine) +
        Number(this.transporterfine) +
        Number(this.driverfine) +
        Number(this.ownerfine);
      this.finetot = this.finetotal.toLocaleString() + "";
    },
    getorigin() {
      this.origins = [];
      axios
        .get(window.$http + `Origins`, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          // this.cargos = response.data.cargo_name;
          response.data.forEach((e) => {
            this.origins.push(e.origin_name.toUpperCase());
          });

          Swal.close();
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Origins" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
    },
    getcaseresults() {
      axios
        .get(
          window.$http +
            `caseresults/search?caseid=` +
            this.caseid +
            "&deleted=5",
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          // this.cargos = response.data.cargo_name;

          this.orderData3 = response.data;
          for (let i = 0; i < this.orderData3.length; i++) {
            this.tot +=
              Number(response.data[i].drivercharge) +
              Number(response.data[i].ownercharge) +
              Number(response.data[i].transportercharge) +
              Number(response.data[i].loadercharge) +
              Number(response.data[i].parkingfee);
          }

          this.tots = this.tot.toLocaleString() + "";
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Origins" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
    },
    printpdf(p_pdf) {
      this.printReport = true;
      p_pdf = "clicked print pdf button";
      this.clickedPrintpdf = p_pdf;
    },
    getrpt(p_excel) {
      this.printReport = true;
      p_excel = "Generated excel file";
      this.generatedExel = p_excel;
    },
    getcase(doc) {
      //alert(window.$http + `casedetails/search?caseid=` + this.caseid);
      axios
        .get(window.$http + `casedetails/search?caseid=` + this.caseid, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          // this.cargos = response.data.cargo_name;
          // this.dec = response;
          var getres = "";
          try {
            var string = JSON.stringify(response);
            getres = string.replace("data", "item");
          } catch (e) {
            Swal.fire({
              position: "center",
              icon: "error",
              title: "getcase replace " + e,
              showConfirmButton: true,
            });
          }
          var dec = [];
          try {
            dec = JSON.parse(getres);
          } catch (e) {
            //dec = response;
            Swal.fire({
              position: "center",
              icon: "error",
              title: "getcase parse " + e,
              showConfirmButton: true,
            });
          }
          // this.dec = dec;
          this.getdoc(dec, doc);
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "getcase " + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
    },
    addbilling() {
      // Others: false,
      // Overload: false,
      // Parking: false,
      // Absconding: false,
      // othersdetails: "",
      // overloadw: 0,
      // parkingtodate: "",
      // parkingfromdate: "",
      // abscondingdate: "",

      //alert(this.Absconding);
      var overload = 0;

      if (this.Overload) {
        overload = 1;
      }
      var billingto = "";
      try {
        billingto = this.billingto;
      } catch (e) {
        //alert(billingto);
        billingto = "";
      }

      if (billingto.length <= 0) {
        Swal.fire("Enter To:").then(() => {
          //this.handleBackToDetails();
        });
        return;
      }
      var abscondingdate = "0001-01-01T00:00:01";
      try {
        abscondingdate = this.abscondingdate;
      } catch (e) {
        abscondingdate = "0001-01-01T00:00:01";
      }
      var abscond = 0;
      if (this.Absconding) {
        if (abscondingdate.length <= 0) {
          Swal.fire("Enter Date of Absconding").then(() => {
            //this.handleBackToDetails();
          });
          return;
        }
        abscond = 1;
      }
      var parkingfromdate = "0001-01-01T00:00:01";
      var park = 0;
      try {
        parkingfromdate = this.parkingfromdate;
      } catch (e) {
        parkingfromdate = "";
      }

      var parkingtodate = "0001-01-01T00:00:01";
      try {
        parkingtodate = this.parkingtodate;
      } catch (e) {
        parkingtodate = "0001-01-01T00:00:01";
      }

      if (this.Parking) {
        if (parkingfromdate.length <= 0) {
          Swal.fire("Enter Date of Absconding").then(() => {
            //this.handleBackToDetails();
          });
          return;
        }
        if (parkingtodate.length <= 0) {
          Swal.fire("Enter Date of Absconding").then(() => {
            //this.handleBackToDetails();
          });
          return;
        }
        park = 1;
      }

      var othersdetails = "";
      try {
        othersdetails = this.othersdetails;
      } catch (e) {
        othersdetails = "";
      }
      var other = 0;
      if (this.Others) {
        if (othersdetails.length <= 0) {
          Swal.fire("Enter Details for other selection").then(() => {
            //this.handleBackToDetails();
          });
          return;
        }
        other = 1;
      }
      if (this.amtpayable <= 0) {
        Swal.fire("Approximate Amount Payable in Kshs.").then(() => {
          //this.handleBackToDetails();
        });
        return;
      }
      if (!this.Absconding && !this.Parking && !this.Overload && !this.Others) {
        Swal.fire("Select one Billing").then(() => {
          //this.handleBackToDetails();
        });
        return;
      }
      var Stri = "<div> Billing </div>";
      if (this.Absconding) {
        Stri +=
          "<div> Absconding: Yes; Date of Absconding;" +
          abscondingdate +
          "</div>";
      }
      if (this.Parking) {
        Stri +=
          "<div> Parking: Yes; From Date:" +
          parkingfromdate +
          "To Date:" +
          parkingtodate +
          "</div>";
      }
      if (this.Overload) {
        Stri += "<div> Overload: Yes; " + this.overloadw + "[KG]" + "</div>";
      }
      if (this.Others) {
        Stri += "<div> Others: Yes; Details:" + othersdetails + "" + "</div>";
      }
      var str =
        "<div> From:" +
        window.$station +
        "</div>" +
        "<div> To:" +
        billingto +
        "</div>" +
        "<div> Date :" +
        this.getcurrentdate() +
        "</div>" +
        Stri +
        "<div> Approximate Amount Payable Kshs: :" +
        this.amtpayable +
        "</div>";
      Swal.fire({
        title: "Are you sure, you want to add Billing Instruction Sheet ?",
        html: "<pre>" + str + "</pre>" + "?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, Add Billing Instruction Sheet!",
      }).then((result) => {
        if (result.value) {
          //alert("index:" + index + "ID:" + id);

          var mycasereid = "BS";

          axios
            .get(
              window.$http +
                `billinginstruction/max?billingid=` +
                mycasereid +
                window.$stationcode +
                this.ticketdate(),
              {
                headers: window.$headers,
              }
            )
            .then((response) => {
              //alert(response.data);
              let ticketmax = Number(response.data);
              ticketmax++;
              var ticketno = this.padLeadingZeros(ticketmax, 6);
              var Ncasedocsid =
                mycasereid + window.$stationcode + this.ticketdate() + ticketno;

              var data = {
                billingid: Ncasedocsid,
                caseid: this.caseid,
                billingfrom: window.$stationcode,
                billingto: billingto,
                datetime: this.getcurrentdate(),
                absconding: abscond,
                parking: park,
                overload: overload,
                others: other,
                timestamp: this.getcurrentdate(),
                createdby: window.$userid,
                deleted: "0",
                deleteddatetime: this.getcurrentdate(),
                payableamount: this.amtpayable,
                casedetailsid: this.id,
              };
              if (this.Absconding) {
                data.push({ abscondingdatetime: abscondingdate });
              }
              if (this.Parking) {
                data.push({
                  parkingfromdatetime: parkingfromdate,
                  parkingtodatetime: parkingtodate,
                });
              }

              axios
                .post(window.$http + `billinginstruction/`, data, {
                  headers: window.$headers,
                })
                .then(() => {
                  // JSON responses are automatically parsed.
                  //Swal.close();

                  this.$root.$emit("bv::hide::modal", "modal-Billing");
                  this.updatecasedocs("Billing Instruction Sheet");
                  Swal.fire({
                    position: "center",
                    icon: "success",
                    title: " Case results added ! ",
                    showConfirmButton: false,
                    timer: 1500,
                  }).then(() => {
                    this.updatearray();
                  });
                });
            });
        }
      });
    },
    addresults() {
      var ownerfine = 0;
      try {
        ownerfine = this.ownerfine;
      } catch (e) {
        ownerfine = 0;
      }

      if (ownerfine < 0) {
        Swal.fire("Enter Owner Fine!").then(() => {
          //this.handleBackToDetails();
        });
        return;
      }
      var driverfine = 0;
      try {
        driverfine = this.driverfine;
      } catch (e) {
        driverfine = 0;
      }
      if (driverfine < 0) {
        Swal.fire("Enter Driver Fine!").then(() => {
          //this.handleBackToDetails();
        });
        return;
      }
      var transporterfine = 0;
      try {
        transporterfine = this.transporterfine;
      } catch (e) {
        transporterfine = 0;
        return;
      }
      if (transporterfine < 0) {
        Swal.fire("Enter Transporter Fine!").then(() => {
          //this.handleBackToDetails();
        });
        return;
      }

      var loaderfine = 0;
      try {
        loaderfine = this.loaderfine;
      } catch (e) {
        loaderfine = 0;
      }
      if (loaderfine < 0) {
        Swal.fire("Enter Loader Fine!").then(() => {
          //this.handleBackToDetails();
        });
        return;
      }

      var parkingfee = 0;
      try {
        parkingfee = this.parkingfee;
      } catch (e) {
        parkingfee = 0;
      }
      if (parkingfee < 0) {
        Swal.fire("Enter Parking Fee!").then(() => {
          //this.handleBackToDetails();
        });
        return;
      }

      var receiptno = "";
      try {
        receiptno = this.receiptno;
      } catch (e) {
        receiptno = "";
      }
      if (receiptno.length <= 0) {
        Swal.fire("Enter receiptno/ Mpesa Code Fee!").then(() => {
          //this.handleBackToDetails();
        });
        return;
      }

      var casebasis = "";
      try {
        casebasis = this.casebasis;
      } catch (e) {
        casebasis = "";
      }
      if (casebasis.length <= 0) {
        Swal.fire("Enter Case Basis!").then(() => {
          //this.handleBackToDetails();
        });
        return;
      }

      var Reason = "";
      try {
        Reason = this.casedischarge;
      } catch (e) {
        Reason = "";
      }

      var mycasereid = "CR";

      axios
        .get(
          window.$http +
            `caseresults/max?casereid=` +
            mycasereid +
            window.$stationcode +
            this.ticketdate(),
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          //alert(response.data);
          let ticketmax = Number(response.data);
          ticketmax++;
          var ticketno = this.padLeadingZeros(ticketmax, 6);
          var Ncasedocsid =
            mycasereid + window.$stationcode + this.ticketdate() + ticketno;

          var data = {
            casereid: Ncasedocsid,
            caseid: this.caseid,
            drivercharge: driverfine,
            ownercharge: ownerfine,
            transportercharge: transporterfine,
            loadercharge: loaderfine,
            parkingfee: parkingfee,
            receiptno: receiptno,
            casebasis: casebasis,
            reason: Reason,
            datetime: this.getcurrentdate(),
            timestamp: this.getcurrentdate(),
            createdby: window.$userid,
            casereticketno: this.caseticketno,
            casereregno: this.caseregno,
            deleted: "0",
            deleteddatetime: this.getcurrentdate(),
            casedetailsid: this.id,
          };
          axios
            .post(window.$http + `caseresults/`, data, {
              headers: window.$headers,
            })
            .then(() => {
              // JSON responses are automatically parsed.
              //Swal.close();

              this.getcaseresults();
              Swal.fire({
                position: "center",
                icon: "success",
                title: " Case results added ! ",
                showConfirmButton: false,
                timer: 1500,
              }).then(() => {
                this.updatearray();
              });
            });
        });
    },
    getwbt() {
      axios
        .get(
          window.$http +
            `weighbridgetransactions/search?limit=1&wbrg_ticket_no=` +
            this.caseticket,
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          this.wbTicket = response.data;
        });
    },
    finishloadc() {
      if (this.orderData3.length == 0) {
        Swal.fire("Please enter add a record!").then(() => {
          //this.handleBackToDetails();
        });
        return;
      }
      Swal.fire({
        title:
          "Are you sure, you want to finish prosecution charges Ticket no.  " +
          this.caseticket +
          " for Registration " +
          this.caseregno +
          "?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, Finish !",
      }).then((result) => {
        if (result.value) {
          //alert("index:" + index + "ID:" + id);
          this.$root.$emit("bv::hide::modal", "modal-LoadC");
          this.updatecasedocs("Load Correction Memo");
        }
      });
    },
    showdetails() {
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      this.ticketsshow = [];
      this.getdata = "";

      this.nplate = this.nplate
        .toUpperCase()
        .replaceAll("\\s", "")
        .replaceAll("[^a-zA-Z0-9]", "")
        .replaceAll(" ", "")
        .trim();

      axios
        .get(
          window.$http +
            `yardlist/search?status=I&caseid=PS&vehreg=` +
            this.nplate,
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          this.yardlist = response.data;
          //alert(dat.length);
          if (!this.yardlist.length > 0) {
            this.getdata = "No Data";
            Swal.close();
            return;
          }

          axios
            .get(
              window.$http +
                `weighbridgetransactions/search?limit=1&status=O&allwbt=all&wbrg_ticket_no=` +
                this.yardlist[0].wbtno,
              {
                headers: window.$headers,
              }
            )
            .then((response) => {
              // JSON responses are automatically parsed.
              this.ticketsshow = response.data;
              this.editmode = true;
              //var id = response.data.id;
              //console.log(this.orderData);

              Swal.close();
            });
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
    },

    updatearray() {
      //screen and roles combination
      //alert(window.$http);
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      axios
        .get(window.$http + `casedetails/search?limit=` + this.perPage, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;

          Swal.close();
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });

      // axles
      axios
        .get(window.$http + `AxleWeightXreffs`, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.AxleWeightXreffs = response.data;
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Origins" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
    },
    deleteresult(id, casereid, data) {
      Swal.fire({
        title: "Are you sure, you want to delete  " + casereid,
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, Delete (" + casereid + ")!",
      }).then((result) => {
        if (result.value) {
          //alert("index:" + index + "ID:" + id);
          data.deleted = 1;
          axios
            .put(window.$http + `caseresults/` + id, data, {
              headers: window.$headers,
            })
            .then(() => {
              //saveticket();
              //Swal.close();
              this.getcaseresults();
              Swal.fire({
                position: "center",
                icon: "success",
                title: "Deleted  " + casereid,
                showConfirmButton: false,
                timer: 1500,
              }).then(() => {});
            })
            .catch((e) => {
              Swal.fire("Error!\n " + e);
              //Swal.close();
              //throw e;
            });
        }
      });
    },
    // puteac(caseid,docname,caseregno) {
    //   axios
    //     .put(window.$http + `eacact/` + caseid, docs, {
    //       headers: window.$headers,
    //     })
    //     .then(() => {
    //       //saveticket();
    //       //Swal.close();
    //       Swal.fire({
    //         position: "center",
    //         icon: "success",
    //         title: "Deleted  " + docname + " for Registration " + caseregno,
    //         showConfirmButton: false,
    //         timer: 1500,
    //       }).then(() => {
    //         this.updatearray();
    //       });
    //     })
    //     .catch((e) => {
    //       Swal.fire("Error!\n " + e);
    //       //Swal.close();
    //       //throw e;
    //     });
    // },
    remove(id, docname, caseregno, docs) {
      Swal.fire({
        title:
          "Are you sure, you want to delete  " +
          docname +
          " for Registration " +
          caseregno +
          "?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, Delete (" + docname + ")!",
      }).then((result) => {
        if (result.value) {
          //alert("index:" + index + "ID:" + id);
          docs.cancelled = "Y";
          docs.cancelledby = JSON.parse(localStorage.user).email;
          docs.editeddate = this.getcurrentdate();
          axios
            .put(window.$http + `casedocs/` + id, docs, {
              headers: window.$headers,
            })
            .then(() => {
              //saveticket();
              //Swal.close();
              // if (docname == "EAC Act" || docname == "Traffic Act") {
              //   this.puteac(caseid);
              //   return;
              // }
              Swal.fire({
                position: "center",
                icon: "success",
                title: "Deleted  " + docname + " for Registration " + caseregno,
                showConfirmButton: false,
                timer: 1500,
              }).then(() => {
                this.updatearray();
              });
            })
            .catch((e) => {
              Swal.fire("Error!\n " + e);
              //Swal.close();
              //throw e;
            });
        }
      });
    },
    defaultvals() {
      //screen and roles combination
      //alert(window.$http);
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      axios
        .get(window.$http + `ProsecutionSettings`, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          var orderData = response.data;
          console.log(orderData);
          this.vroad = orderData[0].alongroad;
          this.district = orderData[0].district;
          this.weighbridge = orderData[0].weighbridge;
          this.county = orderData[0].county;
          this.court = orderData[0].court;
          this.police = orderData[0].police;
          this.tpolice = orderData[0].complainant;
          this.vi = orderData[0].inspector;
          this.moveto = orderData[0].movevehicleto;
          Swal.close();
        })
        .catch((e) => {
          Swal.fire("Error!\n " + e);
          Swal.close();
          //throw e;
        });
    },
    editcase(index, oi) {
      this.rep = oi;
      //alert(this.rep.offloadreg);
      this.index = index;
      this.title =
        "Case Details for Reg. " +
        oi.caseregno +
        " Ticket No. " +
        oi.caseticket;
      //alert();
      // this.caseticket = oi.caseticket;
      // this.caseregno = oi.caseregno;
      // this.casedatetime = oi.casedatetime;
      // this.gvw = oi.gvw;
      // this.excess = oi.excess;
      // this.prosecute = oi.prosecute;
      //
      this.selectedData.driverid = oi.driverid;
      this.selectedData.dnames = oi.drivername;
      this.selectedData.dsurname = oi.driversurname;
      this.selectedData.dgender = oi.drivergender;
      this.selectedData.dnationality = oi.drivernationality;
      this.selectedData.paystation = oi.paystation;
      this.selectedData.permitno = oi.permitno;
      this.selectedData.dage = oi.driverage;
      this.selectedData.daddress = oi.driveraddress;
      this.selectedData.driverslicense = oi.driverslicense;
      //OWNER DETAILS

      this.selectedData.oid = oi.ownerid;
      this.selectedData.onames = oi.ownername;
      this.selectedData.osurname = oi.ownersurname;
      this.selectedData.ogender = oi.ownergender;
      this.selectedData.onationality = oi.ownernationality;
      this.selectedData.oage = oi.ownerage;
      this.selectedData.oaddress = oi.owneraddress;

      // LOCATION DETAILS
      this.selectedData.vroad = oi.alongroad;
      this.selectedData.district = oi.district;
      this.selectedData.weighbridge = oi.weighbrige;
      this.selectedData.county = oi.county;
      // COURT DETAIL
      this.selectedData.court = oi.court;
      this.selectedData.police = oi.trafficofficer;
      this.selectedData.tpolice = oi.comppolice;

      // PROHIBITION DETAILS

      this.selectedData.vi = oi.vehicleins;
      this.selectedData.moveto = oi.movevehicleto;
      this.selectedData.rneeded = oi.repairsneeded;
      this.selectedData.offload = oi.offloadreg;
      // TRANSPORTER DETAILS

      this.selectedData.Tnames = oi.transportername;
      this.selectedData.Taddress = oi.transporteradd;

      //alert(oi.driverid);
    },

    // Prohibition Order
    getprohib(oi) {
      //alert(oi.item.caseticket);
      var doc = new jsPDF();
      //This is a key for printing
      doc.setFillColor(255, 255, 255);

      doc.setTextColor(0, 0, 0);
      doc.setFontSize(10);
      doc.addFont("Tahoma", "Tahoma", "Tahoma");
      doc.setFont("Tahoma");

      doc.setFontSize(13);
      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 10, "KeNHA/MTCE/ALC/F3");

      var gvtimg = new Image();
      gvtimg.src = this.gvtimglogo;
      var kenhaimg = new Image();
      kenhaimg.src = this.kenhaimglogo;

      doc.addImage(gvtimg, "PNG", 85, 10, 25, 23.5);
      doc.addImage(kenhaimg, "PNG", 65, 34.5);

      doc.setFontSize(9);
      doc.setFont("Tahoma", "", "normal");
      doc.text(
        35,
        58,
        "Barabara Plaza, Block A&C, Jomo Kenyatta International Airport (JKIA), Off Airport South Road, along Mazao Road."
      );
      doc.text(
        30,
        62,
        "P.O Box 49712-00100 Nairobi, Tel 020 - 4954000 / 0700 423 606 Email dg@kenha.co.ke Website: www.kenha.co.ke"
      );

      doc.setFontSize(13);
      doc.setFont("Tahoma", "", "bold");
      doc.text(140, 75, "NO. " + oi.item.casedocs[0].casedocid);
      doc.setFont("Tahoma", "", "normal");
      doc.text(75, 88, "THE TRAFFIC ACT");
      doc.text(85, 92, "(cap. 403)");
      doc.setFont("Tahoma", "", "bold");
      doc.text(30, 100, "ORDER TO REMOVE VEHICLE FROM ROAD OR PUBLIC PLACE, ");
      doc.text(40, 105, "TO OFFLOAD EXCESS WEIGHT, OR TO EFFECT REPAIRS");

      doc.setFontSize(11);
      doc.setFont("Tahoma", "", "normal");
      let theText = doc.splitTextToSize(
        "To the owner or driver of Vehicle Registration No. " +
          oi.item.caseregno +
          " make:" +
          oi.item.vehiclemake,
        180
      );
      //doc.text(10, 120, theText);
      doc.setFontSize(11);
      doc.setFont("Tahoma", "", "normal");
      doc.text(10, 120, theText);
      //alert(oi.item.alongroad);
      theText = doc.splitTextToSize(
        "I " +
          oi.item.trafficofficer +
          ", a Police Officer/ Licensing Officer/ Inspector acting under Section 106 and 107 of the Traffic Act, have inspected and found the above vehicle is being used along road " +
          oi.item.alongroad +
          " at " +
          oi.item.weighbrige +
          " in contravention of Section 55,56 and 58 of the Traffic Rules in respect of loading and construction of the vehicle and I ORDER that the vehicle shall not be used further until the excess load of " +
          oi.item.caseexcessload +
          " KG is properly distributed or offloaded as per weigh ticket number(s) " +
          oi.item.caseticket,
        180
      );
      doc.text(15, 130, "1.");
      doc.text(20, 130, theText);

      theText = doc.splitTextToSize(
        "For this purpose,I further order that the vehicle shall be moved to " +
          oi.item.movevehicleto +
          " and there detained until this is compiled with in accordance with the traffic act",
        180
      );

      doc.text(20, 160, theText);

      theText = doc.splitTextToSize(
        "This order is without prejudice to any proceedings that may be brought for the contravention with the Traffic Act or the Traffic Rules",
        180
      );
      doc.text(15, 180, "2.");
      doc.text(20, 180, theText);

      theText = doc.splitTextToSize(
        "It is an offense to disobey this order",
        180
      );
      doc.text(15, 200, "3.");
      doc.text(20, 200, theText);

      theText = doc.splitTextToSize(
        "Attention is drawn to subsection (3) of Section 106 of the Traffic Act that any loss or damage incurred as a result of this order shall not be the responsibility of the police officer,licensing officer or inspector",
        180
      );
      doc.text(15, 220, "4.");
      doc.text(20, 220, theText);

      // let newdate = new Date(oi.casedatetime);
      // moment(newdate).format('MMMM Do YYYY, h:mm:ss a');

      // console.log(newdate);

      //ADD DATE AND TIME FUNCTIONS BELOW
      doc.text(
        110,
        260,
        "Signed:......................................................"
      );
      doc.text(
        120,
        270,
        "Date:" +
          this.getdate(oi.item.casedatetime) +
          "  Time" +
          this.gettime(oi.item.casedatetime)
      );
      doc.save(
        oi.item.casedocs[0].casedocid + "-" + oi.item.caseregno + ".pdf"
      );
      doc.output("dataurlnewwindow");
    },
    getmonth(d) {
      const monthNames = [
        "Jan",
        "Feb",
        "Mar",
        "Apr",
        "May",
        "Jun",
        "Jul",
        "Aug",
        "Sep",
        "Oct",
        "Nov",
        "Dec",
      ];
      return monthNames[d];
    },
    getdate(dy) {
      let d = new Date(dy);
      let year = d.getFullYear();
      let month = this.getmonth(d.getMonth());
      let date = d.getDate();
      date = this.getv(date);
      //month = this.getv(month);

      //const msec = d.getMilliseconds();
      const datetime = date + "/" + month + "/" + year;
      return datetime;
    },
    gettime(dy) {
      let d = new Date(dy);

      let hour = d.getHours();
      let min = d.getMinutes();
      let sec = d.getSeconds();
      hour = this.getv(hour);
      min = this.getv(min);
      sec = this.getv(sec);

      //const msec = d.getMilliseconds();
      const datetime = hour + ":" + min + ":" + sec;
      return datetime;
    },
    getv(val) {
      if (val < 10) {
        val = "0" + val;
      }
      return val;
    },
    // Condtional Load Correction Memo
    getcond(oi) {
      var doc = new jsPDF();
      //This is a key for printing
      doc.setFillColor(255, 255, 255);

      doc.setTextColor(0, 0, 0);
      doc.setFontSize(10);
      doc.addFont("Tahoma", "Tahoma", "Tahoma");
      doc.setFont("Tahoma");

      doc.setFontSize(13);
      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 10, "KeNHA/MTCE/ALC/F3");
      var gvtimg = new Image();
      gvtimg.src = this.gvtimglogo;
      var kenhaimg = new Image();
      kenhaimg.src = this.kenhaimglogo;

      doc.addImage(gvtimg, "PNG", 85, 10, 25, 23.5);
      doc.addImage(kenhaimg, "PNG", 65, 34.5);

      doc.setFontSize(9);
      doc.setFont("Tahoma", "", "normal");
      doc.text(
        35,
        58,
        "Barabara Plaza, Block A&C, Jomo Kenyatta International Airport (JKIA), Off Airport South Road, along Mazao Road."
      );
      doc.text(
        38,
        62,
        "P.O Box 49712-00100 Nairobi, Tel 020 - 4954000 / 0700 423 606 Email dg@kenha.co.ke Website: www.kenha.co.ke"
      );

      doc.setFontSize(13);
      doc.setFont("Tahoma", "", "bold");

      doc.text(75, 88, "INTERNAL MEMO");

      doc.line(5, 92, 205, 92);
      doc.text(10, 100, "FROM:");
      doc.setFont("Tahoma", "", "normal");
      doc.text(40, 100, oi.item.weighbrige);

      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 105, "THROUGH:");
      doc.setFont("Tahoma", "", "normal");
      doc.text(40, 105, "Officer Commanding Station - " + oi.item.weighbrige);

      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 110, "TO:");
      doc.setFont("Tahoma", "", "normal");
      doc.text(
        40,
        110,
        oi.item.drivername +
          " / " +
          oi.item.transportername +
          "(Driver / Transporter)"
      );

      var certno = "";
      for (let i = 0; i < oi.item.casedocs.length; i++) {
        if (oi.item.casedocs[i].docname == "Conditional Load Correction Memo") {
          certno = oi.item.casedocs[i].casedocid;
        }
      }
      //alert(oi.item.drivername);
      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 115, "REF:");
      doc.setFont("Tahoma", "", "normal");
      doc.text(40, 115, "" + certno);

      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 120, "DATE:");
      doc.setFont("Tahoma", "", "normal");
      doc.text(
        40,
        120,
        this.getdate(oi.item.casedatetime) +
          "  " +
          this.gettime(oi.item.casedatetime)
      );

      //doc.roundedRect(5, 5, 5, 17, 0, 0, "FD");
      doc.line(5, 128, 205, 128);

      doc.setFont("Tahoma", "", "bold");
      doc.text(20, 140, "RE : ORDER TO DISTRIBUTE EXCESS WEIGHT ON VEHICLE");

      doc.setFontSize(11);
      doc.setFont("Tahoma", "", "normal");
      let theText = doc.splitTextToSize(
        "Vehicle Registration Number " +
          oi.item.caseregno +
          " of Transporter " +
          oi.item.transportername +
          "  address " +
          oi.item.transporteradd +
          " is hereby ordered to distribute " +
          oi.item.caseexcessload +
          " KG within the " +
          oi.item.weighbrige +
          " (impound yard)",
        180
      );
      doc.text(10, 150, theText);

      doc.text(
        10,
        170,
        "Prohibition Order Number: " + oi.item.casedocs[0].casedocid
      );

      doc.text(10, 175, "Weigh Ticket Number: " + oi.item.caseticket);

      //DATE FUNCTION HERE
      doc.text(
        10,
        180,
        "Weigh Date: " +
          this.getdate(oi.item.weighdate) +
          "  Time" +
          this.gettime(oi.item.weighdate)
      );

      doc.text(
        120,
        220,
        "Signature:......................................................."
      );
      doc.text(
        120,
        240,
        "     Name:........................................................"
      );
      doc.setFontSize(13);
      doc.setFont("Tahoma", "", "bold");
      doc.text(147, 250, "OFFICER IN CHARGE");
      doc.text(140, 260, oi.item.weighbrige);
      //alert(certno);
      doc.save(certno + "-" + oi.item.caseregno + ".pdf");
      doc.output("dataurlnewwindow");
    },

    // Compliance Certificate
    getcomp(oi) {
      var traname = "";
      try {
        traname = oi.item.transportername.trim();
      } catch (e) {
        traname = "";
      }
      //alert(traname);
      if (traname.length <= 0) {
        Swal.fire("Enter Transporter name on the Case Details ");
      }
      var doc = new jsPDF();
      //This is a key for printing
      doc.setFillColor(255, 255, 255);

      doc.setTextColor(0, 0, 0);
      doc.setFontSize(10);
      doc.addFont("Tahoma", "Tahoma", "Tahoma");
      doc.setFont("Tahoma");

      doc.setFontSize(13);
      doc.setFont("Tahoma", "", "bold");
      var gvtimg = new Image();
      gvtimg.src = this.gvtimglogo;
      var kenhaimg = new Image();
      kenhaimg.src = this.kenhaimglogo;

      doc.addImage(gvtimg, "PNG", 85, 10, 25, 23.5);
      doc.addImage(kenhaimg, "PNG", 65, 34.5);

      doc.setFontSize(9);
      doc.setFont("Tahoma", "", "normal");
      doc.text(
        35,
        58,
        "Barabara Plaza, Block A&C, Jomo Kenyatta International Airport (JKIA), Off Airport South Road, along Mazao Road."
      );
      doc.text(
        30,
        62,
        "P.O Box 49712-00100 Nairobi, Tel 020 - 4954000 / 0700 423 606 Email dg@kenha.co.ke Website: www.kenha.co.ke"
      );

      doc.setFontSize(13);
      doc.text(75, 75, "THE TRAFFIC ACT");
      doc.text(85, 79, "(cap. 403)");
      var certno = "";
      for (let i = 0; i < oi.item.casedocs.length; i++) {
        if (oi.item.casedocs[i].docname == "Compliance Certificate") {
          certno = oi.item.casedocs[i].casedocid;
        }
      }
      doc.setFont("Tahoma", "", "bold");
      doc.text(150, 88, "NO. " + certno);

      doc.setFontSize(15);
      doc.text(67, 100, "CERTIFICATE OF COMPLIANCE");

      doc.setFontSize(11);
      doc.setFont("Tahoma", "", "normal");
      var vehreg = "",
        transportername = "";
      for (let m = -3; m < oi.item.caseregno.length; m++) {
        vehreg += "_";
      }
      for (let q = -10; q < oi.item.transportername.length; q++) {
        transportername += "_";
      }
      let theText = doc.splitTextToSize(
        "Vehicle Registration Number " +
          vehreg +
          " of transporter " +
          transportername +
          " of address  " +
          oi.item.transporteradd +
          " has complied with Prohibition Order Number " +
          oi.item.casedocs[0].casedocid +
          " by distributing and/or offloading",
        180
      );

      doc.setFont("Tahoma", "", "bold");
      doc.text(55, 120, "  " + oi.item.caseregno);
      doc.text(
        65 +
          oi.item.transportername.length +
          " of transporter ".length +
          oi.item.caseregno.length +
          5,
        120,
        "  " + oi.item.transportername
      );
      doc.setFont("Tahoma", "", "normal");
      doc.text(10, 120, theText);

      doc.setFont("Tahoma", "", "normal");

      doc.text(10, 156, "Ticket No.: ");
      doc.text(10, 160, "Station:");
      doc.text(100, 160, "Date and Time:");

      doc.setFont("Tahoma", "", "bold");
      // DATE FUNCTION HERE
      doc.text(25, 156, oi.item.caseticket);
      doc.text(25, 160, oi.item.weighbrige);
      doc.text(130, 160, oi.item.casedatetime);

      theText = doc.splitTextToSize(
        "....................................................................................                         Signature: ............................................",
        180
      );
      doc.text(10, 190, theText);

      doc.text(25, 195, "Machine Attendant:");

      doc.text(
        100,
        220,
        "Name: ............................................................."
      );
      doc.text(100, 228, "Officer in Charge -");
      doc.setFont("Tahoma", "", "bold");
      doc.text(130, 228, oi.item.weighbrige);
      doc.save(certno + "-" + oi.item.caseregno + ".pdf");
      doc.output("dataurlnewwindow");
    },

    // Driver Charge Sheet
    getdcharg(oi, person) {
      var doc = new jsPDF();
      //This is a key for printing
      doc.setFillColor(255, 255, 255);

      doc.setTextColor(0, 0, 0);
      doc.setFontSize(10);
      doc.addFont("Tahoma", "Tahoma", "Tahoma");
      doc.setFont("Tahoma");

      doc.setFontSize(12);
      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 35, "POLICE 5");
      // doc.text(75, 88, "THE TRAFFIC ACT");
      // doc.text(85, 92, "(cap. 403)");

      doc.text(80, 50, "THE KENYA POLICE");
      doc.text(85, 53, "-----------------------");
      doc.setFontSize(11);
      doc.text(85, 57, "CHARGE SHEET");
      //
      var policeimg = new Image();
      policeimg.src = this.policeimglogo;

      doc.addImage(policeimg, "PNG", 75, 10, 55, 36.6);

      doc.setFont("Tahoma", "", "normal");
      doc.setFontSize(9);

      doc.text(20, 71, "O.B. NO:...................................");
      doc.text(20, 79, "CHARGE SHEET No:");
      doc.setFont("Tahoma", "", "bold");
      var certno = "";
      for (let i = 0; i < oi.item.casedocs.length; i++) {
        if (oi.item.casedocs[i].docname == "Charge Sheet") {
          certno = oi.item.casedocs[i].casedocid;
        }
      }
      doc.text(52, 79, person + certno);

      doc.setFont("Tahoma", "", "normal");
      doc.text(135, 63, "POLICE CASE NO:..................................");
      doc.text(135, 71, "DATE TO COURT:...................................");
      doc.text(135, 79, "COURT FILE NO:...................................");
      var comment = "";
      //alert(oi.item.WBT_chargedon);
      var gvlegal =
        Number(this.wbTicket[0].axle1_limit) +
        Number(this.wbTicket[0].axle2_limit) +
        Number(this.wbTicket[0].axle3_limit) +
        Number(this.wbTicket[0].axle4_limit) +
        Number(this.wbTicket[0].axle5_limit) +
        Number(this.wbTicket[0].axle6_limit) +
        Number(this.wbTicket[0].axle7_limit) +
        Number(this.wbTicket[0].axle8_limit);
      var group = 0;
      var legal = 0;
      var weig = 0;

      if (this.wbTicket[0].axle1_weight > weig) {
        group = this.wbTicket[0].axle1_weight;
        legal = this.wbTicket[0].axle1_limit;
      }
      if (this.wbTicket[0].axle2_weight > weig) {
        group = this.wbTicket[0].axle2_weight;
        legal = this.wbTicket[0].axle2_limit;
      }
      if (this.wbTicket[0].axle3_weight > weig) {
        group = this.wbTicket[0].axle3_weight;
        legal = this.wbTicket[0].axle3_limit;
      }
      if (this.wbTicket[0].axle4_weight > weig) {
        group = this.wbTicket[0].axle4_weight;
        legal = this.wbTicket[0].axle4_limit;
      }

      if (this.wbTicket[0].wbrg_ticket_excessload == "gvw") {
        comment =
          "on Gross Vehicle Weight(GVW) by carrying " +
          Number(this.wbTicket[0].wbrg_ticket_grossweight)
            .toLocaleString()
            .replace(",", " ") +
          " kg instead of legal limit of " +
          gvlegal.toLocaleString().replace(",", " ") +
          " kg an overload of Gross Vehicle Weight(GVW) " +
          Number(oi.item.caseexcessload).toLocaleString().replace(",", " ") +
          " kg";
      } else {
        var legmit = (Number(legal) * 5) / 100 + Number(legal);
        comment =
          "on Axle Group Weight by carrying " +
          group.toLocaleString().replace(",", " ") +
          " kg instead of legal limit of " +
          legmit.toLocaleString().replace(",", " ") +
          " kg an overload of Axle Group Weight " +
          oi.item.caseexcessload.toLocaleString().replace(",", " ") +
          " kg";
      }
      var name = oi.item.drivername + " ";
      var surname = oi.item.driversurname;
      var id = oi.item.driverid;
      var gender = oi.item.drivergender;
      var nationality = oi.item.drivernationality;
      var address = oi.item.driveraddress;
      var age = oi.item.driverage;
      var drivown = "DRIVING";
      var don = "Driver";

      if (person == "O") {
        name = oi.item.ownername;
        surname = oi.item.ownersurname;
        id = oi.item.ownerid;
        gender = oi.item.ownergender;
        nationality = oi.item.ownernationality;
        address = oi.item.owneraddress;
        age = oi.item.ownerage;
        drivown = "OWNING";
        don = "Owner";
      }
      var head = [];
      for (let i = 0; i < 40; i++) {
        head.push({
          content: "",
          colSpan: 1,
          styles: {
            halign: "left",
            fillColor: [255, 255, 255],
            textColor: [0, 0, 0],
            cellWidth: 5,
          },
        });
      }

      //alert(head.length);
      doc.autoTable({
        head: [head],
        body: [
          [
            {
              content: "Full Name",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Surname or Father's",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Identity or Passport",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Gender\n(sex) M/F",
              colSpan: 3,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Nationality",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Apparent\nAge",
              colSpan: 4,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Address",
              colSpan: 15,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],

          [
            {
              content: name,
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: surname,
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: id,
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: gender,
              colSpan: 3,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: nationality,
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: age,
              colSpan: 4,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: address,
              colSpan: 11,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
          [
            {
              content: "CHARGE",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content:
                drivown +
                " A MOTOR VEHICLE ON THE ROAD WITH A GREATER LOAD WITH LOAD GREATER THAN THE LOAD SPECIFIED IN CONTRARY TO SECTION 56(1) AS READ WITH SECTION 58(1)AND RULE 41(2) OF THE TRAFFIC RULE 1953(REVISED 2018) OF THE TRAFFIC ACT CAP 403 OF LAWS OF KENYA",
              colSpan: 36,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
          [
            {
              content: "PARTICULARS OF OFFENCE (See second Schedule of C.P.C)",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            //"<b>"+{Command.drivername}+ " " + {Command.driversurname}+ " </b>on <b>" + CSTR( {Command.weighdate}, 'dddd, MMMM d, yyyy')+ "</b> at about <b>" + CSTR({Command.weighdate},'HH:mm')+ "</b> along <b>" + {Command.alongroad}+ "</b> at <b>" + {Command.weighbrige} + "</b> being the Driver of motor vehicle Reg. No. <b>" + {Command.caseregno} + "</b> makes class <b>" + {Command.ticketaxletype} + "</b> USED a motor vehicle on the road with greater load than the specified load <b> " + iif({Command.WBT_chargedon}='GVW',"on Gross Vehicle Weight(GVW) by carrying "+CSTR({Command.sAxleaCTW})+" kg instead of legal limit of "+CSTR({Command.sAxlealEW})+" kg","on Axle Group Weight by carrying "+ CSTR(iif({Command.AxleaCTW}-(({Command.AxlealEW}*5/100)+{Command.AxlealEW})={Command.axleload},{Command.AxleaCTW},0)) +" kg instead of legal limit of "+ CSTR(iif({Command.AxleaCTW}-(({Command.AxlealEW}*5/100)+{Command.AxlealEW})={Command.axleload},{Command.AxlealEW}+({Command.AxlealEW}*5/100),0)) +" kg") +", an overload of " + iif({Command.WBT_chargedon}='GVW',"Gross Vehicle Weight(GVW) ","Axle Group Weight ") + CSTR( {Command.caseexcessload}, 0, '') +" kg above the legal limit plus five percent grace </b> as per attached Kenya National Highways Authority Axle Load Weghing Ticket Number <b>"+{Command.caseticket}+"</b>"
            {
              content:
                name +
                " " +
                surname +
                " on " +
                this.getdate(oi.item.weighdate) +
                " at about " +
                this.gettime(oi.item.weighdate) +
                " along " +
                oi.item.alongroad +
                "  at " +
                oi.item.weighbrige +
                " being the " +
                don +
                " of motor vehicle Reg. No. " +
                oi.item.caseregno +
                " Ticket No. " +
                oi.item.caseticket +
                "makes " +
                oi.item.vehiclemake +
                " class (" +
                oi.item.ticketaxletype +
                ") USED a motor vehicle on the road with greater load than the specified load  " +
                comment +
                " as per attached Kenya National Highways Authority Axle Load Weighing Ticket Number " +
                oi.item.caseticket,
              colSpan: 36,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
          [
            {
              content: "Is Accused Arrested",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Date of Arrest",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Without or With Warrant",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Date Apprehension Report to Court",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Bond or Bail and Amount",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Is Application made for summons to issue",
              colSpan: 36,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
          [
            {
              content: "\n\n\n",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "\n\n\n",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "\n\n\n",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "\n\n\n",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "\n\n\n",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "\n\n\n",
              colSpan: 36,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
          [
            {
              content: "Remanded or",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "\n\n\n\n\n",
              colSpan: 36,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
          [
            {
              content: "Complainant and Address",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "REP:THROUGH " + oi.item.weighbrige + " POLICE STATION",
              colSpan: 36,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
          [
            {
              content: "",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content:
                "\n1............................................................................. 6.............................................................................\n\n2............................................................................. 7.............................................................................\n\n3............................................................................. 8.............................................................................\n\n4............................................................................. 9.............................................................................\n\n5............................................................................. 10.............................................................................",
              colSpan: 36,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
          [
            {
              content: "Sentence Court and Date",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content:
                "\n\n.....................................................................................................................................................................................................\n\n.....................................................................................................................................................................................................\n\n " +
                oi.item.court +
                " on ............................. If fine paid .....................................",
              colSpan: 36,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
        ],
        startY: 80,
        margin: { horizontal: 5 },
        styles: {
          columnWidth: "wrap",
          fontSize: 7,
          overflow: "linebreak",
          cellWidth: "wrap",
        },
        columnStyles: {
          text: { columnWidth: "auto" },
          nil: { halign: "right" },
          tgl: { halign: "right" },
        },
        headerStyles: {
          halign: "left",
          fillColor: [255, 255, 255],
          textColor: [0, 0, 0],
        },
        bodyStyles: { lineColor: [0, 0, 0] },
        theme: "grid",
      });
      //console.log(head);
      doc.setDrawColor(0, 0, 0);
      doc.line(10, 250, 200, 250);
      doc.text(
        80,
        260,
        ".................................................... Officer in Charge .............................................. Police Station"
      );
      doc.save(person + certno + "-" + oi.item.caseregno + ".pdf");
      doc.output("dataurlnewwindow");
    },

    // Owner Charge Sheet
    getocharg(oi) {
      var doc = new jsPDF();
      //This is a key for printing
      doc.setFillColor(255, 255, 255);

      doc.setTextColor(0, 0, 0);
      doc.setFontSize(10);
      doc.addFont("Tahoma", "Tahoma", "Tahoma");
      doc.setFont("Tahoma");

      doc.setFontSize(12);
      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 35, "POLICE 5");
      // doc.text(75, 88, "THE TRAFFIC ACT");
      // doc.text(85, 92, "(cap. 403)");

      doc.text(80, 50, "THE KENYA POLICE");
      doc.setFontSize(11);
      doc.text(85, 55, "CHARGE SHEET");

      var policeimg = new Image();
      policeimg.src = this.policeimglogo;

      doc.addImage(policeimg, "PNG", 75, 10, 55, 36.6);

      doc.setFont("Tahoma", "", "normal");
      doc.setFontSize(9);

      doc.text(20, 71, "O.B. NO:...................................");
      doc.text(20, 79, "CHARGE SHEET No:");
      doc.setFont("Tahoma", "", "bold");
      doc.text(52, 79, oi.oChargeSheetNo);

      doc.setFont("Tahoma", "", "normal");
      doc.text(135, 63, "POLICE CASE NO:..................................");
      doc.text(135, 71, "DATE TO COURT:...................................");
      doc.text(135, 79, "COURT FILE NO:...................................");

      doc.autoTable({
        head: [
          [
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
            {
              content: "",
              colSpan: 1,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                cellWidth: 5,
              },
            },
          ],
        ],
        body: [
          [
            {
              content: "Full Name",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Surname or Father's",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Identity or Passport",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Gender\n(sex) M/F",
              colSpan: 3,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Nationality",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Apparent\nAge",
              colSpan: 4,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Address",
              colSpan: 15,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
          [
            {
              content: oi.data.onames,
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: oi.data.osurname,
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: oi.data.oid,
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: oi.data.ogender,
              colSpan: 3,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: oi.data.onationality,
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: oi.data.oage,
              colSpan: 4,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: oi.data.oaddress,
              colSpan: 11,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
          [
            {
              content: "CHARGE",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content:
                "USING A MOTOR VEHICLE ON A ROAD WITH A LOAD GREATER THAN THE LOAD SPECIFIED BY SEC 58(1) AND RULE 41(2) OF THE LEGAL NOTICE NUMBER 93/2013 DATED 2013/06/01 OF THE TRAFFIC ACT 403 LAWS OF KENYA",
              colSpan: 36,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
          [
            {
              content: "PARTICULARS OF OFFENCE (See second Schedule of C.P.C)",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content:
                oi.data.onames +
                " being the Driver of motor vehicle Reg. No. " +
                oi.caseregno +
                " Ticket No. " +
                oi.caseticket +
                "carrying " +
                oi.caseexcessload +
                ") kg instead of legal limit of 18000 kg, an overload of Gross Vehicle Weight(GVW) 1000 kg on 15/May/2021 at about 08:00:00 along Athiriver Mombasa Bound class 6C makes class FAW USED a motor vehicle on the road with greater load than the specified on Gross Vehicle Weight(GVW) by  as per attached Kenya National Highways Authority Axle Weighing",
              colSpan: 36,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
          [
            {
              content: "Is Accused Arrested",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Date of Arrest",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Without or With Warrant",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Date Apprehension Report to Court",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Bond or Bail and Amount",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Is Application made for summons to issue",
              colSpan: 36,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
          [
            {
              content: "\n\n\n",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "\n\n\n",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "\n\n\n",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "\n\n\n",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "\n\n\n",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "\n\n\n",
              colSpan: 36,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
          [
            {
              content: "Remanded or",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "\n\n\n\n\n",
              colSpan: 36,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
          [
            {
              content: "Complainant and Address",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "REP:THROUGH " + oi.data.weighbridge + " POLICE STATION",
              colSpan: 36,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
          [
            {
              content: "",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content:
                "\n1............................................................................. 6.............................................................................\n\n2............................................................................. 7.............................................................................\n\n3............................................................................. 8.............................................................................\n\n4............................................................................. 9.............................................................................\n\n5............................................................................. 10.............................................................................",
              colSpan: 36,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
          [
            {
              content: "Sentence Court and Date",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content:
                "\n\n.....................................................................................................................................................................................................\n\n.....................................................................................................................................................................................................\n\nCOURT NAME on ............................. If fine paid .....................................",
              colSpan: 36,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
        ],
        startY: 80,
        margin: { horizontal: 5 },
        styles: {
          columnWidth: "wrap",
          fontSize: 7,
          overflow: "linebreak",
          cellWidth: "wrap",
        },
        columnStyles: {
          text: { columnWidth: "auto" },
          nil: { halign: "right" },
          tgl: { halign: "right" },
        },
        headerStyles: {
          halign: "left",
          fillColor: [255, 255, 255],
          textColor: [0, 0, 0],
        },
        bodyStyles: { lineColor: [0, 0, 0] },
        theme: "grid",
      });

      doc.setDrawColor(0, 0, 0);
      doc.line(10, 250, 200, 250);
      doc.text(
        80,
        260,
        ".................................................... Officer in Charge .............................................. Police Station"
      );
      doc.save(
        oi.item.casedocs[0].casedocid + "-" + oi.item.caseregno + ".pdf"
      );
      doc.output("dataurlnewwindow");
    },
    getact(act, oi) {
      //var nodata = [];
      //alert(oi.item.caseticket);
      try {
        axios
          .get(
            window.$http +
              `weighbridgetransactions/search?limit=1&wbrg_ticket_no=` +
              oi.item.caseticket,
            {
              headers: window.$headers,
            }
          )
          .then((response) => {
            this.getact2(act, oi, response.data[0]);
            // return response.data[0].cluster_name;
          })
          .catch(() => {});
      } catch (Exception) {
        console.log();
      }
    },
    getact2(act, oi, nodata) {
      //alert(nodata.wbrg_ticket_type);
      axios
        .get(window.$http + `eacact/search?caseid` + oi.item.caseid, {
          headers: window.$headers,
        })
        .then((resp) => {
          var res = resp.data[0];
          axios
            .get(
              window.$http +
                `axleweights/search?wbrg_ticket_no=` +
                oi.item.caseticket,
              {
                headers: window.$headers,
              }
            )
            .then((response) => {
              // JSON responses are automatically parsed.
              // this.cargos = response.data.cargo_name;
              var orderData = [];
              if (
                nodata.wbrg_ticket_type == "L" ||
                nodata.wbrg_ticket_type == "S"
              ) {
                var a = 0;
                var b = 0;
                var c = 0;
                var d = 0;

                var pa = 0;
                var pb = 0;
                var pc = 0;
                var pd = 0;
                var myfeea = 0;
                var myfeeb = 0;
                var myfeec = 0;
                var myfeed = 0;
                //console.log(orderData3);
                var orderd = response.data;

                for (let k = 0; k < orderd.length; k++) {
                  //alert(orderd.length + " - " + orderd[k].axle_grouping);
                  //console.log(orderd.length + " - " + orderd[k].axle_grouping);
                  if (orderd[k].axle_grouping == "A") {
                    a = a + Number(orderd[k].axle_actualweight);
                    pa = pa + Number(orderd[k].axle_permissibleweight);
                    myfeea = myfeea + Number(orderd[k].fee);
                    orderData[0] = orderd[k];
                    orderData[0].axle_actualweight = a + "";
                    orderData[0].axle_permissibleweight = pa + "";
                    orderData[0].fee = myfeea + "";
                    //alert(orderd[k].axle_grouping);
                  }

                  if (orderd[k].axle_grouping == "B") {
                    b = b + Number(orderd[k].axle_actualweight);
                    pb = pb + Number(orderd[k].axle_permissibleweight);
                    myfeeb = myfeeb + Number(orderd[k].fee);

                    orderData[1] = orderd[k];
                    orderData[1].axle_actualweight = b + "";
                    //alert(orderData[1].axle_actualweight);
                    orderData[1].axle_permissibleweight = pb + "";
                    orderData[1].fee = myfeeb + "";
                  }

                  if (orderd[k].axle_grouping == "C") {
                    c = c + Number(orderd[k].axle_actualweight);
                    pc = pc + Number(orderd[k].axle_permissibleweight);
                    myfeec = myfeec + Number(orderd[k].fee);

                    orderData[2] = orderd[k];
                    orderData[2].axle_actualweight = c + "";
                    orderData[2].axle_permissibleweight = pc + "";
                    orderData[2].fee = myfeec + "";
                    //alert(orderd[k].axle_grouping);
                  }

                  if (orderd[k].axle_grouping == "D") {
                    orderData[1].fee = myfeeb + "";
                    myfeed = myfeed + Number(orderd[k].fee);
                    d = d + Number(orderd[k].axle_actualweight);
                    pd = pd + Number(orderd[k].axle_permissibleweight);
                    orderData[3] = orderd[k];
                    orderData[3].axle_actualweight = d + "";
                    orderData[3].axle_permissibleweight = pd + "";
                    orderData[3].fee = myfeed + "";
                    //alert(orderd[k].axle_grouping);
                  }
                }
              } else {
                orderData = response.data;
              }

              var axles = [];
              var gvwactual = 0;
              var gvwperm = 0;
              //var fee = 0;
              var g = 0;
              var totalfee = 0.0;
              var takehigest = 0;
              var highestaxlefee = 0;

              for (let i = 0; i < orderData.length; i++) {
                var actual = Number(orderData[i].axle_actualweight);
                var perm = Number(orderData[i].axle_permissibleweight);
                var tollerancediff = (perm * 5) / 100;
                var tollerance = (perm * 5) / 100 + perm;
                var overload = 0;
                var adjusted = actual - tollerancediff;
                if (actual - tollerance > 0) {
                  overload = actual - tollerance;
                }
                //alert(overload);
                if (overload > takehigest) {
                  takehigest = Number(overload);
                  highestaxlefee = Number(orderData[i].fee);
                }
                g++;
                var group = "Axle - Group " + g;
                var row = [];

                row.push(g);
                row.push(group);
                row.push(actual.toLocaleString());
                row.push(perm.toLocaleString());
                row.push(tollerancediff.toLocaleString());
                row.push(adjusted.toLocaleString());
                row.push(overload.toLocaleString());
                row.push(new Intl.NumberFormat().format(orderData[i].fee));
                totalfee += Number(orderData[i].fee);
                axles.push(Object.values(row));
                // doc.text(120, lane4 + inc, "" + orderData[i].axle_actualweight);
                // doc.text(140, lane4 + inc, "" + overload);
                // doc.text(163, lane4 + inc, "" + result);

                // axleoverl += Number(overload);

                gvwperm += perm;
                gvwactual += actual;
              }
              var gvwoverload = 0;
              if (gvwactual - gvwperm > 0) {
                gvwoverload = gvwactual - gvwperm;
              }
              var overrallfee = res.gvwfee;
              // if (Number(gvwoverload) >= Number(takehigest)) {
              //   overrallfee = res.gvwfee;
              // }
              // if (Number(takehigest) > Number(gvwoverload)) {
              //   overrallfee = highestaxlefee;
              // }
              //alert(highestaxlefee);
              //alert(res.gvwfee);

              if (Number(res.gvwfee) >= Number(highestaxlefee)) {
                overrallfee = res.gvwfee;
              }
              if (Number(highestaxlefee) > Number(res.gvwfee)) {
                overrallfee = highestaxlefee;
              }

              var overaltamt =
                Number(overrallfee) *
                Number(res.dollarrate) *
                Number(oi.item.paystation);
              //steeringaxle = axleoverl;
              //alert(axles);
              this.geteacact(
                oi,
                axles,
                act,
                gvwactual,
                gvwperm,
                gvwoverload,
                res,
                totalfee,
                highestaxlefee,
                overrallfee,
                overaltamt,
                nodata
              );
            })
            .catch((e) => {
              Swal.fire({
                position: "center",
                icon: "error",
                title: "Getting Axle Weights " + e,
                showConfirmButton: true,
              }).then((e) => {
                Swal.close(e);
              });

              //throw e;
            });
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Get EAC Act " + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
    },
    // EAC Act
    async geteacact(
      oi,
      axles,
      act,
      gvw,
      gvwperm,
      gvwoverload,
      res,
      totalfee,
      highestaxlefee,
      overrallfee,
      overaltamt,
      nodata
    ) {
      try {
        var suser = await axios
          .get(
            window.$http + `users/search?email=` + nodata.wbrg_ticket_operator,
            {
              headers: window.$headers,
            }
          )
          .then((resp) => {
            return resp.data;
            //alert(suser[0].fullnames);
          })
          .catch((e) => {
            Swal.fire("Users - Ticket" + e);
            //this.errors.push(e);
          });
      } catch (Exception) {
        console.log();
      }
      var operator = "";
      try {
        operator = suser[0].fullnames.toUpperCase();
      } catch (Exception) {
        console.log();
      }
      var certno = "";
      for (let i = 0; i < oi.item.casedocs.length; i++) {
        if (oi.item.casedocs[i].docname == "EAC Act") {
          certno = oi.item.casedocs[i].casedocid;
        }
      }
      var body = [];
      var doc = new jsPDF();
      var head = [];
      for (let i = 0; i < 40; i++) {
        head.push({
          content: "",
          colSpan: 1,
          styles: {
            halign: "left",
            fillColor: [255, 255, 255],
            textColor: [0, 0, 0],
            cellWidth: "auto",
            lineWidth: 0,
          },
        });
      }

      var eacimg = new Image();
      if (act == "EAC") {
        eacimg.src = this.eacimglogo;
      }
      if (act == "Traffic") {
        eacimg.src = this.policeimglogo;
      }

      var kenhaimg = new Image();
      kenhaimg.src = this.kenhalogo;

      var gas = new Array(1);
      gas[0] = "Stamp";
      var ff = "";
      for (var g = 0; g < gas.length; g++) {
        var row = [];

        row.push({
          content: "",
          colSpan: 30,
          styles: {
            halign: "center",
            fillColor: [255, 255, 255],
            cellPadding: 12,
            textColor: [0, 0, 0],
            fontStyle: "bold",
            fontSize: 12,
            lineWidth: 0.1,
            valign: "bottom",
          },
        });
        ff = "";

        row.push({
          content: gas[g],
          colSpan: 10,
          rowSpan: 3,
          styles: {
            halign: "center",
            cellPadding: 0,
            fillColor: [255, 255, 255],
            textColor: [0, 0, 0],
            fontStyle: "bold",
            fontSize: 12,
            lineWidth: 0.1,
            valign: "middle",
          },
        });
        body.push(Object.values(row));
      }
      row = [];

      for (g = 0; g < 1; g++) {
        ff = "THE EAC (WEIGHBRIDGE) CERTIFICATE ";

        row.push({
          content: ff,
          colSpan: 30,
          styles: {
            halign: "center",
            fillColor: [255, 255, 255],
            textColor: [0, 0, 0],
            fontStyle: "bold",
            fontSize: 9,
            lineWidth: 0,
          },
        });
        ff = "";
        row.push({
          content: "",
          colSpan: 10,
          styles: {
            halign: "center",
            fillColor: null,
            textColor: [0, 0, 0],
            fontStyle: "bold",
            fontSize: 9,
            lineWidth: 0,
          },
        });
        body.push(Object.values(row));
      }

      row = [];
      row.push({
        content: "Date:",
        colSpan: 5,
        styles: {
          halign: "right",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 9,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: oi.item.casedatetime.substring(0, 10),
        colSpan: 5,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "normal",
          fontSize: 6,
          lineWidth: 0,
        },
      });

      row.push({
        content: "Region/Location:" + oi.item.district,
        colSpan: 10,
        styles: {
          halign: "center",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: "Time:" + oi.item.casedatetime.substring(11, 20),
        colSpan: 10,
        styles: {
          halign: "center",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: "",
        colSpan: 10,
        styles: {
          halign: "center",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
          lineWidth: 0.1,
        },
      });
      body.push(Object.values(row));
      row = [];
      row.push({
        content: "Country: Kenya",
        colSpan: 10,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
          lineWidth: 0,
        },
      });
      row.push({
        content: "Ticket No." + oi.item.caseticket,
        colSpan: 10,
        styles: {
          halign: "center",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
          lineWidth: 0,
        },
      });
      row.push({
        content: " EAC Act No.:" + certno,
        colSpan: 20,
        styles: {
          halign: "center",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
          lineWidth: 0,
        },
      });
      body.push(Object.values(row));

      row = [];
      row.push({
        content: "Weighbridge Station: " + oi.item.weighbrige,
        colSpan: 20,
        styles: {
          halign: "center",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: "Weigbridge Code:" + oi.item.station,
        colSpan: 20,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
          lineWidth: 0.1,
        },
      });
      body.push(Object.values(row));

      row = [];
      row.push({
        content: "PART A: TRANSPORTER INFORMATION ",
        colSpan: 40,
        styles: {
          halign: "center",
          fillColor: [204, 204, 204],
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 12,
          lineWidth: 0.1,
        },
      });

      body.push(Object.values(row));

      row = [];
      row.push({
        content: "Owner of The Vehicle ",
        colSpan: 10,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: oi.item.ownername + " " + oi.item.ownersurname,
        colSpan: 10,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: "Address:" + oi.item.owneraddress,
        colSpan: 20,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
          lineWidth: 0.1,
        },
      });
      body.push(Object.values(row));

      row = [];
      row.push({
        content: "Type of cargo ",
        colSpan: 10,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: oi.item.caseticket,
        colSpan: 10,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
          lineWidth: 0.1,
        },
      });
      row.push({
        content:
          "Origin:" +
          nodata.wbrg_ticket_source +
          "Destination (Country and City):" +
          nodata.wbrg_ticket_destination,
        colSpan: 20,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
          lineWidth: 0.1,
        },
      });
      body.push(Object.values(row));

      row = [];
      row.push({
        content: "Special Load permit No. (If Applicable) ",
        colSpan: 10,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: "" + oi.item.permitno,
        colSpan: 10,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
          lineWidth: 0.1,
        },
      });
      row.push({
        content:
          "Transporter Name:" +
          oi.item.transportername +
          " Address:" +
          oi.item.transporteradd,
        colSpan: 20,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
          lineWidth: 0.1,
        },
      });
      body.push(Object.values(row));

      row = [];
      row.push({
        content: "Axle Configuration+ ",
        colSpan: 10,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: oi.item.ticketaxletype,
        colSpan: 15,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: "",
        colSpan: 15,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
          lineWidth: 0.1,
        },
      });
      body.push(Object.values(row));

      row = [];
      row.push({
        content: "Vehicle Reg No. " + oi.item.caseregno,
        colSpan: 10,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: "Vehicle;",
        colSpan: 8,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: "Semi-trailer;",
        colSpan: 7,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: "Trailer;",
        colSpan: 15,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: "Trailer",
        colSpan: 15,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
          lineWidth: 0.1,
        },
      });
      body.push(Object.values(row));

      row = [];
      row.push({
        content: "PART B: AXLE WEIGHTS ",
        colSpan: 40,
        styles: {
          halign: "center",
          fillColor: [204, 204, 204],
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 12,
          lineWidth: 0.1,
        },
      });
      body.push(Object.values(row));

      row = [];
      row.push({
        content: "Axle No.: ",
        colSpan: 5,
        styles: {
          halign: "center",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 7,
          lineWidth: 0.1,
        },
      });

      row.push({
        content: "Axle-Group: ",
        colSpan: 5,
        styles: {
          halign: "center",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 7,
          lineWidth: 0.1,
        },
      });

      row.push({
        content: "Load measured scale reading (kg) (a)",
        colSpan: 5,
        styles: {
          halign: "center",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 7,
          lineWidth: 0.1,
        },
      });

      row.push({
        content: "Allowed Load (kg) (b)",
        colSpan: 5,
        styles: {
          halign: "center",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 7,
          lineWidth: 0.1,
        },
      });

      row.push({
        content: "Descretion allowance (kg) <5% of (b) (c)",
        colSpan: 5,
        styles: {
          halign: "center",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 7,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: "Adjusted Load (kg)  (a-c) (d)",
        colSpan: 5,
        styles: {
          halign: "center",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 7,
          lineWidth: 0.1,
        },
      });

      row.push({
        content: "Overload Load (kg)  (d-b) (e)",
        colSpan: 5,
        styles: {
          halign: "center",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 7,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: "Overload fee (From e)  USD $",
        colSpan: 5,
        styles: {
          halign: "center",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 7,
          lineWidth: 0.1,
        },
      });
      body.push(Object.values(row));
      for (var b = 0; b < axles.length; b++) {
        row = [];

        for (let elements of axles[b]) {
          console.log(elements);
          row.push({
            content: elements,
            colSpan: 5,
            styles: {
              halign: "center",
              fillColor: null,
              textColor: [0, 0, 0],
              fontStyle: "bold",
              fontSize: 7,
              lineWidth: 0.1,
            },
          });
        }
        // for (g = 0; g < axles[0].length; g++) {
        //   row.push({
        //     content: axles[b][g],
        //     colSpan: 5,
        //     styles: {
        //       halign: "center",
        //       fillColor: null,
        //       textColor: [0, 0, 0],
        //       fontStyle: "bold",
        //       fontSize: 7,
        //       lineWidth: 0.1,
        //     },
        //   });
        // }

        body.push(Object.values(row));
      }

      var vlues = 4 - axles.length;

      for (b = 0; b < vlues; b++) {
        row = [];

        for (g = 0; g < 8; g++) {
          row.push({
            content: "",
            colSpan: 5,
            styles: {
              halign: "center",
              fillColor: null,
              textColor: [0, 0, 0],
              fontStyle: "bold",
              fontSize: 7,
              lineWidth: 0.1,
            },
          });
        }

        body.push(Object.values(row));
      }

      row = [];
      row.push({
        content: "",
        colSpan: 40,
        styles: {
          halign: "center",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 12,
          lineWidth: 0.1,
        },
      });
      body.push(Object.values(row));
      var r = 0;
      for (b = 0; b < 3; b++) {
        row = [];
        if (b > 0) {
          r = 1;
        }

        for (g = 0; g < 8; g++) {
          var rowSpan = 0;
          var dval = "";

          if ((b == 0 && g == 0) || (b == 0 && g == 7)) {
            rowSpan = 3;
            dval = "Dimension";
          }
          var he = "";
          if (b == 0) {
            he = "Legal";
          }

          if (b == 1) {
            he = "Measured";
          }
          if (b == 2) {
            he = "Extra";
          }

          if (r == 1) {
            dval = he + " Height";
          }
          if (r == 3) {
            dval = he + " Width";
          }
          if (r == 5) {
            dval = he + " Length";
          }
          row.push({
            content: dval,
            colSpan: 5,
            rowSpan: rowSpan,
            styles: {
              halign: "center",
              fillColor: null,
              textColor: [0, 0, 0],
              fontStyle: "bold",
              fontSize: 7,
              lineWidth: 0.1,
              valign: "middle",
            },
          });
          r++;
        }
        body.push(Object.values(row));
      }

      row = [];
      row.push({
        content: "PART C: FEES AND PENALTIES ",
        colSpan: 40,
        styles: {
          halign: "center",
          fillColor: [204, 204, 204],
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 12,
          lineWidth: 0.1,
        },
      });
      body.push(Object.values(row));

      row = [];
      row.push({
        content: "CURRENCY USD $",
        colSpan: 10,
        styles: {
          halign: "left",
          fillColor: [204, 204, 204],
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 7,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: "FEE $",
        colSpan: 6,
        styles: {
          halign: "left",
          fillColor: [204, 204, 204],
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 7,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: "Actual GVW [KG]",
        colSpan: 6,
        styles: {
          halign: "left",
          fillColor: [204, 204, 204],
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 7,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: "Allowed GVW [KG]",
        colSpan: 6,
        styles: {
          halign: "left",
          fillColor: [204, 204, 204],
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 7,
          lineWidth: 0.1,
        },
      });

      row.push({
        content: "Overload GVW [KG]",
        colSpan: 6,
        styles: {
          halign: "left",
          fillColor: [204, 204, 204],
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 7,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: "",
        colSpan: 6,
        styles: {
          halign: "left",
          fillColor: [204, 204, 204],
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 7,
          lineWidth: 0.1,
        },
      });
      body.push(Object.values(row));
      //var totfee = res.gvwfee + totalfee;
      var period = "2021-2030";
      var lesspermit = 0;
      if (nodata.permit_no.length > 5) {
        if (Number(nodata.wbrg_ticket_axel.substring(0, 1)) == 2) {
          lesspermit = 1000;
        }
        if (
          Number(nodata.wbrg_ticket_axel.substring(0, 1)) == 3 ||
          Number(nodata.wbrg_ticket_axel.substring(0, 1)) == 4
        ) {
          lesspermit = 2000;
        }
      }
      var eacgvw = Number(nodata.wbrg_ticket_gvwload) - lesspermit;

      const gvwfee = await axios
        .get(
          window.$http +
            `EACAct/EACActGVWCharges?period=` +
            period +
            `&GVWOverloadkg=` +
            eacgvw,
          {
            headers: window.$headers,
          }
        )
        .then((res) => {
          return res.data;
        })
        .catch((e) => {
          Swal.fire("Ticket Error -" + e);
          //this.errors.push(e);
        });
      var items = [
        [
          "Sum of Axle-fees [$" +
            new Intl.NumberFormat().format(Number(totalfee, 2)) +
            "]",
          new Intl.NumberFormat().format(Number(highestaxlefee, 2)) + "",
          "",
          "",
          "No. of Pay Station(s)",
          oi.item.paystation,
        ],
        [
          "Gross Vehicle Weight (GVW)",
          new Intl.NumberFormat().format(Number(gvwfee, 2)) + "",
          new Intl.NumberFormat().format(Number(gvw, 0)) + " [KG]",
          new Intl.NumberFormat().format(Number(gvwperm, 0)) + " [KG]",
          new Intl.NumberFormat().format(Number(gvwoverload, 0)) + " [KG]",
          "",
        ],
        [
          "Total Amount to be paid",
          new Intl.NumberFormat().format(Number(overrallfee, 2)),
          "",
          "",
          "Amount in USD($)",
          new Intl.NumberFormat().format(Number(overrallfee, 2)),
        ],

        [
          "Demerits Points",
          res.demeritspoints,
          "",
          "",
          "Dollar rate ",
          res.dollarrate,
        ],

        [
          "Cumulative Demerits Points",
          res.cdemeritspoints + "",
          "",
          "",
          "Amount in Kshs.",
          new Intl.NumberFormat().format(overaltamt) + "/=",
        ],
      ];
      //["Total Amount to be paid", totfee, "", "", "Amount in USD($)"],
      //"" + totfee + "/=",
      // "" + (totfee * res.dollarrate).toLocaleString() + "/=",
      // "" + res.dollarrate + "",
      var less = 0;
      var alignm = "center";
      for (b = 0; b < 5; b++) {
        row = [];
        less = 0;

        for (g = 0; g < 8; g++) {
          alignm = "center";
          if (g == 1 && b <= 2) {
            alignm = "right";
          }
          row.push({
            content: items[b][g],
            colSpan: 10 - less,
            styles: {
              halign: alignm,
              fillColor: null,
              textColor: [0, 0, 0],
              fontStyle: "bold",
              fontSize: 7,
              lineWidth: 0.1,
            },
          });
          less = 4;
        }

        body.push(Object.values(row));
      }

      row = [];
      row.push({
        content: "",
        colSpan: 40,
        styles: {
          halign: "center",
          fillColor: [204, 204, 204],
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 12,
          lineWidth: 0.1,
        },
      });
      body.push(Object.values(row));
      row = [];
      row.push({
        content: "Action Taken:{" + res.penalties + "}",
        colSpan: 40,
        styles: {
          halign: "Left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 8,
          lineWidth: 0.1,
        },
      });
      body.push(Object.values(row));

      row = [];
      row.push({
        content: "Name of Weighbridge Operator:" + operator,
        colSpan: 30,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 7,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: "Signature:",
        colSpan: 10,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 7,
          lineWidth: 0.1,
        },
      });
      body.push(Object.values(row));

      row = [];
      row.push({
        content: "Name of Driver:" + oi.item.drivername,
        colSpan: 15,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 9,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: "Driver's License No.:" + oi.item.driverslicense,
        colSpan: 15,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 9,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: "Signature:",
        colSpan: 10,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 9,
          lineWidth: 0.1,
        },
      });
      body.push(Object.values(row));

      row = [];
      row.push({
        content:
          "If not satisfied with the findings and you may appeal (Tick the box)",
        colSpan: 15,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 7,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: "Yes",
        colSpan: 15,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 12,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: "No",
        colSpan: 10,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 12,
          lineWidth: 0.1,
        },
      });
      body.push(Object.values(row));

      row = [];

      row.push({
        content: "Remarks:",
        colSpan: 38,
        rowSpan: 2,
        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 9,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: "",
        colSpan: 2,

        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 9,
          lineWidth: 0,
        },
      });
      body.push(Object.values(row));
      row = [];

      row.push({
        content: "",
        colSpan: 38,

        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 9,
          lineWidth: 0.1,
        },
      });
      row.push({
        content: "",
        colSpan: 2,

        styles: {
          halign: "left",
          fillColor: null,
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 9,
          lineWidth: 0,
        },
      });
      body.push(Object.values(row));

      row = [];

      row.push({
        content: "" + new Date().toString().substring(0, 25),
        colSpan: 40,
        styles: {
          halign: "right",
          fillColor: [204, 204, 204],
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 9,
          lineWidth: 0.1,
        },
      });
      body.push(Object.values(row));

      doc.roundedRect(165, 249.5, 7, 7, 1, 1);

      doc.roundedRect(89, 249.5, 7, 7, 1, 1);

      doc.autoTable({
        head: [head],
        body: body,
        // didDrawCell: function (data) {
        //   if (data.column.index === 5 && data.cell.section === "body") {
        //     var td = data.cell.raw;
        //     // Assuming the td cells have an img element with a data url set (<td><img src="data:image/jpeg;base64,/9j/4AAQ..."></td>)
        //     //var kenhaimg = new Image();
        //     var kenhaimg = td.getElementsByTagName("img")[0];
        //     kenhaimg.src = this.kenhaimglogo;
        //     var dim = data.cell.height - data.cell.padding("vertical");
        //     var textPos = data.cell.textPos;
        //     doc.addImage(kenhaimg, textPos.x, textPos.y, dim, dim);
        //   }
        // },
        startY: 0,
        margin: { horizontal: 1 },
        styles: {
          columnWidth: "wrap",
          fontSize: 7,
          overflow: "linebreak",
          cellWidth: "wrap",
        },
        columnStyles: {
          text: { columnWidth: "auto" },
          nil: { halign: "right" },
          tgl: { halign: "right" },
        },
        headerStyles: {
          halign: "left",
          fillColor: [255, 255, 255],
          textColor: [0, 0, 0],
        },
        bodyStyles: { lineColor: [0, 0, 0] },
        theme: "grid",
      });

      doc.addImage(eacimg, "PNG", 10, 8, 25, 23.5);
      doc.addImage(kenhaimg, "PNG", 110, 8, 40, 23.5);

      doc.output("dataurlnewwindow");

      doc.save(certno + "-" + oi.item.caseregno + ".pdf");
      //doc.output("dataurlnewwindow");
    },
    geteac(oi) {
      console.log(oi);
      var doc = new jsPDF();
      //This is a key for printing
      doc.setFillColor(255, 255, 255);

      doc.setTextColor(0, 0, 0);
      doc.setFontSize(10);
      doc.addFont("Tahoma", "Tahoma", "Tahoma");

      doc.setFontSize(13);
      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 10, "KeNHA/MTCE/ALC/F3");
      var eacimg = new Image();
      eacimg.src = this.eacimglogo;

      var kenhaimg = new Image();
      kenhaimg.src = this.kenhaimglogo;

      doc.addImage(eacimg, "PNG", 10, 15, 25, 23.5);
      doc.addImage(kenhaimg, "PNG", 110, 15);

      doc.setFontSize(11);
      doc.setFont("Tahoma", "", "normal");
      doc.text(120, 45, "Stamp");

      doc.setFontSize(13);
      doc.setFont("Tahoma", "", "bold");
      doc.text(20, 55, "THE EAC (" + oi.item.weighbrige + " ) CERTIFICATE");

      doc.setFontSize(11);
      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 65, "Date:");
      doc.text(70, 65, "Region/Location:");
      doc.text(150, 65, "Time:");

      doc.setFont("Tahoma", "", "normal");
      // DATE FUNCTION HERE
      doc.text(25, 65, "15/May/2021");
      doc.text(110, 65, oi.item.district);
      // TIME FUNCTION HERE
      doc.text(165, 65, "08:00:00");

      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 75, "Country: Kenya");
      doc.text(10, 83, "Weighbridge Station:");
      doc.text(120, 83, "Weighbridge Code:");

      doc.setFont("Tahoma", "", "normal");
      // DATE FUNCTION HERE
      doc.text(40, 75, oi.item.casedocs[0].casedocid);
      doc.text(50, 83, oi.item.weighbrige);
      doc.text(155, 83, oi.item.station);

      doc.setFillColor(211, 211, 211);
      doc.rect(10, 88, 190, 15, "F");

      doc.setFillColor(211, 211, 211);
      doc.rect(10, 143, 190, 15, "F");

      doc.setFillColor(211, 211, 211);
      doc.rect(10, 215, 190, 22, "F");

      doc.setFillColor(211, 211, 211);
      doc.rect(10, 276, 190, 10, "F");

      doc.setFontSize(13);
      doc.setFont("Tahoma", "", "bold");
      doc.text(65, 98, "PART A: TRANSPORT INFORMATION");
      doc.text(70, 153, "PART B: AXLE WEIGHTS");
      doc.text(63, 225, "PART C: FEES AND PENALTIES");

      doc.setFontSize(10);
      doc.text(10, 108, "Owner Of The Vehicle:");
      doc.text(10, 116, "Type Of Cargo:");
      doc.text(10, 124, "Special Load Permit No. (If Applicable):");
      doc.text(10, 132, "Axle Configuration:");
      doc.text(10, 140, "Vehicle Reg No.:");
      doc.text(50, 140, "Vehicle:");
      doc.text(90, 140, "Semi-trailer:");
      doc.text(140, 140, "Trailer:");
      doc.text(110, 108, "Address:");
      doc.text(110, 116, "Origin:");
      doc.text(110, 124, "Destination (Country and City):");
      doc.text(110, 132, "Transporter No:");

      doc.autoTable({
        head: [
          [
            "Axle\nNo.",
            "Axle\nGroup",
            "Load\nmeasured\nscale reading (kg)\n(a)",
            "Allowed\naxle\n(kg)\n(b)",
            "Discretion\nallowance\n(kg) <5% of\n(b)(c)",
            "Adjuste\nload\n(kg)\n(a-c)(d)",
            "Overload\n(kg)\n(d-b)(e)",
            "Overload\nfee\n(From e) USD $",
          ],
        ],
        body: [
          ["1", "Axle -\nGroup 1", "", "", "", "", "", ""],
          ["2", "Axle -\nGroup 2", "", "", "", "", "", ""],
          ["3", "Axle -\nGroup 3", "", "", "", "", "", ""],
        ],
        startY: 160,
        margin: { horizontal: 10 },
        styles: { columnWidth: "wrap" },
        columnStyles: {
          text: { columnWidth: "auto" },
          nil: { halign: "right" },
          tgl: { halign: "right" },
        },
        headerStyles: { halign: "center" },
        theme: "plain",
      });

      doc.autoTable({
        head: [
          [
            "CURRENCY USD $",
            "FEES $",
            "Actual GVW [KG]",
            "Allowed GVW [KG]",
            "Overload GVW [KG]",
          ],
        ],
        body: [],
        startY: 230,
        margin: { horizontal: 10 },
        styles: { columnWidth: "wrap" },
        columnStyles: {
          text: { columnWidth: "auto" },
          nil: { halign: "right" },
          tgl: { halign: "right" },
        },
        headerStyles: { halign: "center" },
        theme: "plain",
      });

      doc.autoTable({
        head: [["", "", "", "", ""]],
        body: [
          ["Sum of Axle fees", "", "", "", ""],
          ["Gross Vehicle Weight GVW", "", "", "", ""],
          ["Total Amount to be paid", "", "", "", ""],
          ["Demerit Points", "", "", "", ""],
          ["Cumulative Demerit Point", "", "", "", ""],
        ],
        startY: 232,
        margin: { horizontal: 10 },
        styles: { columnWidth: "wrap" },
        columnStyles: {
          text: { columnWidth: "auto" },
          nil: { halign: "right" },
          tgl: { halign: "right" },
        },
        headerStyles: { halign: "center" },
        theme: "plain",
      });

      doc.addPage(), doc.setFontSize(11);
      doc.setFont("Tahoma", "", "normal");
      doc.text(10, 10, "Action Taken:{}");

      doc.text(10, 30, "Administrator");
      doc.text(10, 45, oi.item.drivername);
      doc.text(10, 50, "If not satisfied with the findings and you may appeal");
      doc.text(10, 55, "(Tick the box)");
      doc.text(108, 45, oi.item.driverslicense);

      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 25, "Name of Weighbridge Operator:");
      doc.text(10, 40, "Name of the Driver:");
      doc.text(10, 70, "Remarks:");
      doc.text(105, 40, "Driver's License No:");
      doc.text(108, 55, "Yes");
      doc.text(160, 25, "Signature:");
      doc.text(160, 40, "Signature:");
      doc.text(163, 55, "No");

      doc.setFillColor(211, 211, 211);
      doc.rect(10, 80, 190, 15, "F");

      doc.text(163, 85, oi.item.casedatetime);
      doc.save(
        oi.item.casedocs[0].casedocid + "-" + oi.item.caseregno + ".pdf"
      );
      doc.output("dataurlnewwindow");
    },

    // Load Correction Memo
    getload(oi) {
      var doc = new jsPDF();
      //This is a key for printing
      doc.setFillColor(255, 255, 255);

      doc.setTextColor(0, 0, 0);
      doc.setFontSize(10);
      doc.addFont("Tahoma", "Tahoma", "Tahoma");
      doc.setFont("Tahoma");

      doc.setFontSize(13);
      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 10, "KeNHA/MTCE/ALC/F3");
      var gvtimg = new Image();
      gvtimg.src = this.gvtimglogo;
      var kenhaimg = new Image();
      kenhaimg.src = this.kenhaimglogo;

      doc.addImage(gvtimg, "PNG", 85, 10, 25, 23.5);
      doc.addImage(kenhaimg, "PNG", 65, 34.5);

      doc.setFontSize(9);
      doc.setFont("Tahoma", "", "normal");
      doc.text(
        35,
        58,
        "Barabara Plaza, Block A&C, Jomo Kenyatta International Airport (JKIA), Off Airport South Road, along Mazao Road."
      );
      doc.text(
        30,
        62,
        "P.O Box 49712-00100 Nairobi, Tel 020 - 4954000 / 0700 423 606 Email dg@kenha.co.ke Website: www.kenha.co.ke"
      );

      doc.setFontSize(10);
      doc.setFont("Tahoma", "", "bold");
      var certno = "";
      for (let i = 0; i < oi.item.casedocs.length; i++) {
        if (oi.item.casedocs[i].docname == "Load Correction Memo") {
          certno = oi.item.casedocs[i].casedocid;
        }
      }
      doc.text(140, 68, "NO. " + certno);
      doc.setFontSize(13);
      doc.text(75, 72, "INTERNAL MEMO");
      doc.text(
        10,
        75,
        "-----------------------------------------------------------------------------------------------------------------------------"
      );

      doc.text(10, 80, "FROM:");
      doc.setFont("Tahoma", "", "normal");
      doc.text(40, 80, oi.item.weighbrige);

      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 85, "THROUGH:");
      doc.setFont("Tahoma", "", "normal");
      doc.text(40, 85, "Officer Commanding Station - " + oi.item.weighbrige);

      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 90, "TO:");
      doc.setFont("Tahoma", "", "normal");
      doc.text(
        40,
        90,
        oi.item.drivername +
          " / " +
          oi.item.transportername +
          "(Driver / Transporter)"
      );

      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 95, "REF:");
      doc.setFont("Tahoma", "", "normal");
      doc.text(40, 95, "NO. " + oi.item.casedocs[0].casedocid);

      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 100, "DATE:");
      doc.setFont("Tahoma", "", "normal");
      doc.text(40, 100, oi.item.casedatetime);

      doc.text(
        10,
        105,
        "-----------------------------------------------------------------------------------------------------------------------------"
      );

      doc.setFont("Tahoma", "", "bold");
      doc.text(
        20,
        115,
        "RE : ORDER TO OFFLOAD / DISTRIBUTE EXCESS WEIGHT FROM VEHICLE"
      );

      doc.setFontSize(11);
      doc.setFont("Tahoma", "", "normal");

      var lastrow = [];
      for (let b = 0; b < 9; b++) {
        lastrow.push({
          content: "",
          colSpan: 4,
          styles: {
            halign: "right",
            fillColor: [255, 255, 255],
            textColor: [0, 0, 0],
            fontStyle: "bold",
            fontSize: 6,
            lineWidth: 0,
          },
        });
      }

      var owner = 0;
      var driver = 0;
      var transporter = 0;
      var loader = 0;
      var parkingfee = 0;
      var rowno = [];
      var row = [];
      //var k = 0;

      row.push({
        content: "Case id",
        colSpan: 4,
        styles: {
          halign: "center",
          fillColor: [82, 148, 82],
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 9,
        },
      });
      row.push({
        content: "Case Basis",
        colSpan: 4,
        styles: {
          halign: "center",
          fillColor: [82, 148, 82],
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 9,
        },
      });
      row.push({
        content: "Reason",
        colSpan: 7,
        styles: {
          halign: "center",
          fillColor: [82, 148, 82],
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 9,
        },
      });
      row.push({
        content: "Receipt /Mpesa No.",
        colSpan: 4,
        styles: {
          halign: "center",
          fillColor: [82, 148, 82],
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 7,
        },
      });
      row.push({
        content: "Owner Charge",
        colSpan: 3,
        styles: {
          halign: "center",
          fillColor: [82, 148, 82],
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
        },
      });
      row.push({
        content: "Driver Charge",
        colSpan: 3,
        styles: {
          halign: "center",
          fillColor: [82, 148, 82],
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
        },
      });
      row.push({
        content: "Transporter Charge",
        colSpan: 4,
        styles: {
          halign: "center",
          fillColor: [82, 148, 82],
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
        },
      });
      row.push({
        content: "Loader Charge",
        colSpan: 3,
        styles: {
          halign: "center",
          fillColor: [82, 148, 82],
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
        },
      });
      row.push({
        content: "Parking Fee",
        colSpan: 3,
        styles: {
          halign: "center",
          fillColor: [82, 148, 82],
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
        },
      });
      row.push({
        content: "Total (Ksh(s).)",
        colSpan: 5,
        styles: {
          halign: "center",
          fillColor: [82, 148, 82],
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 6,
        },
      });

      rowno.push(Object.values(row));
      //alert("Length:" + oi.item.caseresults.length);
      for (let m = 0; m < oi.item.caseresults.length; m++) {
        if (oi.item.caseresults[m].deleted == 0) {
          var reason = "";
          try {
            reason = oi.item.caseresults[m].reason.trim();
          } catch (e) {
            reason = "";
          }

          row = [];
          //alert("Length:" + oi.item.caseresults[m].casereid);
          row.push({
            content: oi.item.caseresults[m].casereid,
            colSpan: 4,
            styles: {
              halign: "right",
              fillColor: [255, 255, 255],
              textColor: [0, 0, 0],
              fontStyle: "bold",
              fontSize: 6,
            },
          });
          row.push({
            content: oi.item.caseresults[m].casebasis,
            colSpan: 4,
            styles: {
              halign: "left",
              fillColor: [255, 255, 255],
              textColor: [0, 0, 0],
              fontStyle: "bold",
              fontSize: 6,
            },
          });

          row.push({
            content: reason,
            colSpan: 7,
            styles: {
              halign: "left",
              fillColor: [255, 255, 255],
              textColor: [0, 0, 0],
              fontStyle: "bold",
              fontSize: 6,
            },
          });
          row.push({
            content: oi.item.caseresults[m].receiptno,
            colSpan: 4,
            styles: {
              halign: "left",
              fillColor: [255, 255, 255],
              textColor: [0, 0, 0],
              fontStyle: "bold",
              fontSize: 6,
            },
          });
          row.push({
            content: oi.item.caseresults[m].ownercharge.toLocaleString(),
            colSpan: 3,
            styles: {
              halign: "right",
              fillColor: [255, 255, 255],
              textColor: [0, 0, 0],
              fontStyle: "bold",
              fontSize: 8,
            },
          });

          row.push({
            content: oi.item.caseresults[m].drivercharge.toLocaleString(),
            colSpan: 3,
            styles: {
              halign: "right",
              fillColor: [255, 255, 255],
              textColor: [0, 0, 0],
              fontStyle: "bold",
              fontSize: 8,
            },
          });
          row.push({
            content: oi.item.caseresults[m].transportercharge.toLocaleString(),
            colSpan: 4,
            styles: {
              halign: "right",
              fillColor: [255, 255, 255],
              textColor: [0, 0, 0],
              fontStyle: "bold",
              fontSize: 8,
            },
          });

          row.push({
            content: oi.item.caseresults[m].loadercharge.toLocaleString(),
            colSpan: 3,
            styles: {
              halign: "right",
              fillColor: [255, 255, 255],
              textColor: [0, 0, 0],
              fontStyle: "bold",
              fontSize: 8,
            },
          });
          row.push({
            content: oi.item.caseresults[m].parkingfee.toLocaleString(),
            colSpan: 3,
            styles: {
              halign: "right",
              fillColor: [255, 255, 255],
              textColor: [0, 0, 0],
              fontStyle: "bold",
              fontSize: 8,
            },
          });

          if (oi.item.caseresults[m].drivercharge >= 0) {
            driver += Number(oi.item.caseresults[m].drivercharge);
          }
          if (oi.item.caseresults[m].ownercharge >= 0) {
            owner += Number(oi.item.caseresults[m].ownercharge);
          }
          if (oi.item.caseresults[m].transportercharge >= 0) {
            transporter += Number(oi.item.caseresults[m].transportercharge);
          }
          if (oi.item.caseresults[m].loadercharge >= 0) {
            loader += Number(oi.item.caseresults[m].loadercharge);
          }
          if (oi.item.caseresults[m].parkingfee >= 0) {
            parkingfee += Number(oi.item.caseresults[m].parkingfee);
          }
          var total =
            Number(oi.item.caseresults[m].drivercharge) +
            Number(oi.item.caseresults[m].ownercharge) +
            Number(oi.item.caseresults[m].transportercharge) +
            Number(oi.item.caseresults[m].loadercharge) +
            Number(oi.item.caseresults[m].parkingfee);
          row.push({
            content: total.toLocaleString(),
            colSpan: 5,
            styles: {
              halign: "right",
              fillColor: [255, 255, 255],
              textColor: [0, 0, 0],
              fontStyle: "bold",
              fontSize: 9,
            },
          });

          //row.push([]);
          //console.log(row.item.content);
          // rowno[m] = Array.from(new Set(row));
          rowno.push(Object.values(row));
          console.log(row);

          //k++;
        }
      }
      console.log(rowno);
      var totV = driver + owner + transporter + loader + parkingfee;

      lastrow.push({
        content: totV.toLocaleString() + "/=",
        colSpan: 4,
        styles: {
          halign: "right",
          fillColor: [255, 255, 255],
          textColor: [0, 0, 0],
          fontStyle: "bold",
          fontSize: 9,
        },
      });
      rowno.push(Object.values(lastrow));
      var gas = new Array(4);
      gas[0] = "Signature:..................................";
      gas[1] = "Name:.........................................";
      gas[2] = "OFFICER IN CHARGE";
      gas[3] = oi.item.weighbrige;

      for (var g = 0; g < gas.length; g++) {
        lastrow = [];
        lastrow.push({
          content: "",
          colSpan: 20,
          styles: {
            halign: "right",
            fillColor: [255, 255, 255],
            textColor: [0, 0, 0],
            fontStyle: "bold",
            fontSize: 12,
            lineWidth: 0,
          },
        });

        lastrow.push({
          content: gas[g],
          colSpan: 20,
          styles: {
            halign: "center",
            fillColor: [255, 255, 255],
            textColor: [0, 0, 0],
            fontStyle: "bold",
            fontSize: 12,
            lineWidth: 0,
          },
        });
        rowno.push(Object.values(lastrow));
      }
      let theText = doc.splitTextToSize(
        "Vehicle Registration Number " +
          oi.item.caseregno +
          " of Transporter address " +
          oi.item.transportername +
          " is hereby ordered to distribute or to offload " +
          oi.item.caseexcessload +
          " KG (excess weight) to relief truck Registration Number " +
          oi.item.offloadreg +
          " within the " +
          oi.item.weighbrige +
          " station (impound yard) having paid overload fee of Kshs. (Owner:" +
          owner.toLocaleString() +
          " ,Driver:" +
          driver.toLocaleString() +
          " , Transporter: " +
          transporter.toLocaleString() +
          ", Loader:" +
          loader.toLocaleString() +
          " , Parking Fee:" +
          parkingfee.toLocaleString() +
          " ).",
        180
      );
      doc.text(10, 120, theText);
      doc.text(
        10,
        140,
        "Prohibition Order Number: " + oi.item.casedocs[0].casedocid
      );
      doc.text(10, 145, "Weigh Ticket Number: " + oi.item.caseticket);

      //DATE FUNCTION HERE
      doc.text(
        10,
        150,
        "Weigh Date: " +
          this.getdate(oi.item.weighdate) +
          "  Time" +
          this.gettime(oi.item.weighdate)
      );

      var head = [];
      for (let i = 0; i < 40; i++) {
        head.push({
          content: "",
          colSpan: 1,
          styles: {
            halign: "left",
            fillColor: [255, 255, 255],
            textColor: [0, 0, 0],
            cellWidth: "auto",
            lineWidth: 0,
          },
        });
      }

      //alert(head.length);
      doc.autoTable({
        head: [head],
        body: rowno,
        startY: 150,
        margin: { horizontal: 5 },
        styles: {
          columnWidth: "wrap",
          fontSize: 7,
          overflow: "linebreak",
          cellWidth: "wrap",
        },
        columnStyles: {
          text: { columnWidth: "auto" },
          nil: { halign: "right" },
          tgl: { halign: "right" },
        },
        headerStyles: {
          halign: "left",
          fillColor: [255, 255, 255],
          textColor: [0, 0, 0],
        },
        bodyStyles: { lineColor: [0, 0, 0] },
        theme: "grid",
      });
      doc.save(certno + "-" + oi.item.caseregno + ".pdf");
      doc.output("dataurlnewwindow");
    },

    // Load Correction Memo
    getbill(oi) {
      var doc = new jsPDF();

      //var gvtimg = new Image();
      //gvtimg.src = this.gvtimglogo;
      var kenhaimg = new Image();
      kenhaimg.src = this.kenhalonglogo;

      //doc.addImage(gvtimg, "PNG", 85, 10, 25, 23.5);
      doc.addImage(kenhaimg, "PNG", 15, 34.5);

      var head = [];
      for (let i = 0; i < 40; i++) {
        head.push({
          content: "",
          colSpan: 1,
          styles: {
            halign: "left",
            fillColor: [255, 255, 255],
            textColor: [0, 0, 0],
            cellWidth: "auto",
            lineWidth: 0,
          },
        });
      }

      //alert(head.length);
      doc.autoTable({
        head: [head],
        body: [
          [
            {
              content:
                "From: " +
                oi.item.billinginstruction[0].billingfrom +
                " Weighbrige To: " +
                oi.item.billinginstruction[0].billingto +
                " Region",
              colSpan: 40,
              styles: {
                halign: "center",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                fontStyle: "normal",
                fontSize: 9,
                lineWidth: 0,
              },
            },
          ],
          [
            {
              content: "TRANSPORTER'S NAME",
              colSpan: 25,
              styles: {
                halign: "center",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                fontStyle: "bold",
                fontSize: 15,
              },
            },
            {
              content: "VEHICLE REGISTRATION NUMBER",
              colSpan: 15,
              styles: {
                halign: "center",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                fontStyle: "bold",
                fontSize: 15,
              },
            },
          ],

          [
            {
              content: oi.item.transportername,
              colSpan: 25,
              styles: {
                halign: "center",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                fontStyle: "bold",
              },
            },
            {
              content: oi.item.caseregno,
              colSpan: 15,
              styles: {
                halign: "center",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                fontStyle: "bold",
              },
            },
          ],
          head,
          [
            {
              content: "BILLING ITEM",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "PARTICULARS",
              colSpan: 19,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Approximate amount payable (Kshs)",
              colSpan: 15,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
          [
            {
              content: "Absconding Weighbridge",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Date of Absconding Weighbridge",
              colSpan: 19,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content:
                oi.item.billinginstruction[0].payableamount.toLocaleString() +
                "/=",
              colSpan: 15,
              styles: {
                halign: "right",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                fontStyle: "bold",
                fontSize: 15,
              },
            },
          ],
          [
            {
              content: "Parking",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Date From: To:",
              colSpan: 19,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "",
              colSpan: 15,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
          [
            {
              content: "Overload",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "",
              colSpan: 19,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "",
              colSpan: 15,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
          [
            {
              content: "Others",
              colSpan: 6,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: oi.item.billinginstruction[0].othersdetails,
              colSpan: 19,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "",
              colSpan: 15,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
          head,
          [
            {
              content: "PREPARED ",
              colSpan: 25,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "VERIFIED",
              colSpan: 15,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
          [
            {
              content:
                "Signature:................................................................. ",
              colSpan: 25,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content:
                "Signature:.................................................................",
              colSpan: 15,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
          [
            {
              content:
                "Issuing Office                          Date:...........",
              colSpan: 25,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "Station Manager  Date:............................",
              colSpan: 15,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
          [
            {
              content: "Received",
              colSpan: 40,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                fontStyle: "bold",
                lineWidth: 0,
              },
            },
          ],
          [
            {
              content: "Cheque No.",
              colSpan: 10,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                fontStyle: "bold",
              },
            },
            {
              content: "Bank",
              colSpan: 10,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                fontStyle: "bold",
              },
            },
            {
              content: "Branch",
              colSpan: 10,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                fontStyle: "bold",
              },
            },
            {
              content: "Amount",
              colSpan: 10,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
                fontStyle: "bold",
              },
            },
          ],
          [
            {
              content: "",
              colSpan: 10,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "",
              colSpan: 10,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "",
              colSpan: 10,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
            {
              content: "",
              colSpan: 10,
              styles: {
                halign: "left",
                fillColor: [255, 255, 255],
                textColor: [0, 0, 0],
              },
            },
          ],
        ],
        startY: 80,
        margin: { horizontal: 5 },
        styles: {
          columnWidth: "wrap",
          fontSize: 7,
          overflow: "linebreak",
          cellWidth: "wrap",
        },
        columnStyles: {
          text: { columnWidth: "auto" },
          nil: { halign: "right" },
          tgl: { halign: "right" },
        },
        headerStyles: {
          halign: "left",
          fillColor: [255, 255, 255],
          textColor: [0, 0, 0],
        },
        bodyStyles: { lineColor: [0, 0, 0] },
        theme: "grid",
      });
      //This is a key for printing

      doc.setFontSize(13);
      doc.setFont("Tahoma", "", "bold");
      var certno = "";
      var docdate = "";
      for (let i = 0; i < oi.item.casedocs.length; i++) {
        if (oi.item.casedocs[i].docname == "Billing Instruction Sheet") {
          certno = oi.item.casedocs[i].casedocid;
          docdate = oi.item.casedocs[i].docdate;
        }
      }

      doc.text(20, 75, "BILING INSTRUCTION SHEET");
      doc.text(120, 75, "NO. " + certno);

      doc.setFontSize(11);
      doc.setFont("Tahoma", "", "normal");
      doc.text(120, 80, "Date: " + docdate);
      doc.save(certno + "-" + oi.item.caseregno + ".pdf");
      doc.output("dataurlnewwindow");
    },

    // Special Release
    getspecial(sp) {
      var doc = new jsPDF();
      //This is a key for printing
      //alert(oi.caseid);

      doc.setFillColor(255, 255, 255);

      doc.setTextColor(0, 0, 0);
      doc.setFontSize(10);
      doc.addFont("Tahoma", "Tahoma", "Tahoma");
      doc.setFont("Tahoma");

      doc.setFontSize(13);
      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 10, "KeNHA/MTCE/ALC/F3");

      const kenhaimg = require("@/assets/images/kenyashield.png");
      var imgLogo = new Image();
      imgLogo.src = kenhaimg;

      const gvtimg = require("@/assets/images/kenhaauthority.png");
      var imgLogo2 = new Image();
      imgLogo2.src = gvtimg;

      doc.addImage(kenhaimg, "PNG", 85, 10, 25, 23.5);
      doc.addImage(gvtimg, "PNG", 65, 34.5);
      doc.setFontSize(9);
      doc.setFont("Tahoma", "", "normal");
      doc.text(
        25,
        58,
        "Barabara Plaza, Block A&C, Jomo Kenyatta International Airport (JKIA), Off Airport South Road, along Mazao Road."
      );
      doc.text(
        20,
        62,
        "P.O Box 49712-00100 Nairobi, Tel 020 - 4954000 / 0700 423 606 Email dg@kenha.co.ke Website: www.kenha.co.ke"
      );

      doc.setFontSize(13);
      doc.setFont("Tahoma", "", "bold");
      doc.text(140, 75, "NO. " + sp.specialid);

      doc.text(75, 88, "INTERNAL MEMO");
      doc.text(
        10,
        92,
        "-----------------------------------------------------------------------------------------------------------------------------"
      );

      doc.text(10, 100, "FROM:");
      doc.setFont("Tahoma", "", "normal");
      doc.text(40, 100, "Manager - " + sp.weighbridge);

      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 105, "THROUGH:");
      doc.setFont("Tahoma", "", "normal");
      doc.text(40, 105, "Officer Commanding Station - " + sp.weighbridge);

      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 110, "TO:");
      doc.setFont("Tahoma", "", "normal");
      doc.text(
        40,
        110,
        sp.driver + " / " + sp.transporter + " (Driver / Transporter)"
      );

      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 115, "REF:");
      doc.setFont("Tahoma", "", "normal");
      doc.text(40, 115, sp.specialid);

      doc.setFont("Tahoma", "", "bold");
      doc.text(10, 120, "DATE:");
      doc.setFont("Tahoma", "", "normal");
      doc.text(
        40,
        120,
        "Date: " +
          this.getdate(sp.specialreleasedate) +
          "  Time: " +
          this.gettime(sp.specialreleasedate)
      );

      doc.text(
        10,
        128,
        "-----------------------------------------------------------------------------------------------------------------------------"
      );

      doc.setFont("Tahoma", "", "bold");
      doc.text(20, 140, "RE : SPECIAL PERMISSION TO EXIT THE WEIGHBRIDGE");

      doc.setFontSize(11);
      doc.setFont("Tahoma", "", "normal");
      let theText = doc.splitTextToSize(
        "Vehicle Registration Number " +
          sp.regNo +
          " of Transporter " +
          sp.transporter +
          " address " +
          sp.transporteradd +
          " is hereby granted special permission to leave the weighbridge without correcting the excess load.\nReason:",
        180
      );

      doc.text(10, 150, theText);
      doc.setFont("Tahoma", "", "bold");

      theText = doc.splitTextToSize(
        sp.reason.toUpperCase() +
          "\n-------------------------------------\n" +
          sp.description +
          "\n\nExcess Load " +
          sp.excessload +
          "KG",
        180
      );
      doc.text(10, 170, theText);

      doc.setFont("Tahoma", "", "normal");

      doc.text(10, 200, "Prohibition Order Number: " + sp.prohibitionno);
      doc.text(10, 205, "Weigh Ticket Number: " + sp.ticketno);

      //DATE FUNCTION HERE
      doc.text(
        10,
        210,
        "Weigh Date: " +
          this.getdate(sp.weighdate) +
          " Time:" +
          this.gettime(sp.weighdate) +
          ""
      );

      doc.setFontSize(13);
      doc.setFont("Tahoma", "", "bold");
      doc.text(140, 230, "Signature:..................................");
      doc.text(140, 240, "Name:.........................................");

      doc.text(147, 250, "OFFICER IN CHARGE");
      doc.text(140, 255, sp.weighbridge);
      //alert();
      doc.output("dataurlnewwindow");
    },

    getdoc(data, doc) {
      axios
        .get(
          window.$http +
            `weighbridgetransactions/search?limit=1&wbrg_ticket_no=` +
            this.caseticket,
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          this.wbTicket = response.data;

          //alert();
          //this.dec = this.wbTicket;
          //alert(doc);
          //alert(oi);
          Swal.fire({
            title: "Please Wait !",
            html: "Loading data...", // add html attribute if you want or remove
            allowOutsideClick: false,
            showConfirmButton: false,
            willOpen: () => {
              Swal.showLoading();
            },
          });
          this.dec = data;
          switch (doc) {
            case "Prohibition Order":
              this.getprohib(data);
              break;
            case "Conditional Load Correction Memo":
              this.getcond(data);
              break;
            case "Compliance Certificate":
              this.getcomp(data);
              break;
            case "Charge Sheet":
              this.getdcharg(data, "D");
              this.getdcharg(data, "O");
              break;
            case "Traffic Act":
              this.getact("Traffic", data);
              break;
            case "EAC Act":
              this.getact("EAC", data);
              break;
            case "Load Correction Memo":
              this.getload(data);
              break;
            case "Billing Instruction Sheet":
              this.getbill(data);
              break;
            case "Special Release":
              var certno = "";
              for (let i = 0; i < data.item.casedocs.length; i++) {
                if (data.item.casedocs[i].docname == "Special Release") {
                  certno = data.item.casedocs[i].casedocid;
                }
              }
              axios
                .get(
                  window.$http + `specialrelease/report?specialid=` + certno,
                  { headers: window.$headers }
                )
                .then((response) => {
                  // JSON responses are automatically parsed.
                  console.log(response);
                  //alert(response.data[0].specialid);
                  this.getspecial(response.data[0]);
                });
              //this.getspecial(certno);
              break;
          }
          Swal.close();
        });
    },
    /**
     * Search the table data with search input
     */
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
      this.printReport = true;
      this.filteredValues = "Filtered search '" + this.filter + "'";
    },
    handleSubmit() {
      console.log("Error on submit");
    },
    goToProhibition() {
      this.$router.push("/prohibition");
    },
    /**
     * Load correction memo
     */
    goToLcm() {
      this.$router.push("/lcm");
    },
    /**
     * Conditional Load correction memo
     */
    goToClcm() {
      this.$router.push("/clcm");
    },
    /**
     * Charge Sheet Owner
     */
    goToChargesheet() {
      //var newtab = window.open("", "anotherWindow", "width=1200,height=800");
      this.$router.push("/ownerchargesheet");
    },
    /**
     * Charge Sheet Driver
     */
    goToChargesheetdriver() {
      //var newtab2 = window.open("", "anotherWindow", "width=1200,height=800");
      this.$router.push("/chargesheet");
    },
    /**
     * EAC WB Certificate
     */
    goToEaccert() {
      this.$router.push("/eaccert");
    },
    prosecut() {
      if (this.ticketsshow.length <= 0 || this.yardlist.length <= 0) {
        Swal.fire("No ticket to prosecute!");
        return;
      }

      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      var mycaseid = "PS";
      axios
        .get(
          window.$http +
            `casedetails/max?caseid=` +
            mycaseid +
            window.$stationcode +
            this.ticketdate(),
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          //alert(response.data);
          let ticketmax = Number(response.data);
          ticketmax++;
          var ticketno = this.padLeadingZeros(ticketmax, 6);
          var Ncasedocsid =
            mycaseid + window.$stationcode + this.ticketdate() + ticketno;
          var data = {
            caseid: Ncasedocsid,
            station: window.$stationcode,
            casedatetime: this.getcurrentdate(),
            createdby: window.$userid,
            caseticket: this.ticketsshow[0].wbrg_ticket_no,
            caseregno: this.ticketsshow[0].veh_reg,
            WBT_operator: this.ticketsshow[0].wbrg_ticket_operator,
            ticketaxletype: this.ticketsshow[0].wbrg_ticket_axel,
            weighdate: this.ticketsshow[0].wbrg_ticket_date,
            vehiclemake: this.ticketsshow[0].wbrg_ticket_make,
            tickettransporter: this.ticketsshow[0].wbrg_tx_code,
            transportername: this.ticketsshow[0].wbrg_tx_code,
            drivername: this.ticketsshow[0].wbrg_ticket_driver,
            caseexcessload: this.ticketsshow[0].wbrg_ticket_excessweight,
            WBT_chargedon: this.ticketsshow[0].wbrg_ticket_excessload,
            drivergender: "Male",
            ownergender: "Male",
            alongroad: this.vroad,
            district: this.district,
            weighbrige: this.weighbridge,
            county: this.county,
            court: this.court,
            comppolice: this.police,
            trafficofficer: this.tpolice,
            vehicleins: this.vi,
            movevehicleto: this.moveto,
            paystation: 1,
            permitno: this.ticketsshow[0].permit_no,
          };
          //   this.vroad = orderData[0].alongroad;
          // this.district = orderData[0].district;
          // this.weighbridge = orderData[0].weighbridge;
          // this.county = orderData[0].county;
          // this.court = orderData[0].court;
          // this.police = orderData[0].complainant;
          // this.tpolice = orderData[0].police;
          // this.vi = orderData[0].inspector;
          // this.moveto = orderData[0].movevehicle;
          //alert("Ncasedocsid" + Ncasedocsid + "  casedetailsid" + this.id);
          axios
            .post(window.$http + `casedetails/`, data, {
              headers: window.$headers,
            })
            .then((rep) => {
              // JSON responses are automatically parsed.
              this.addProsecuted = true;
              this.$root.$emit("bv::hide::modal", "modal-Prosecute");
              //var result = [];

              for (var i in rep) {
                //alert(i);
                //result.push([i, rep[i]]);
                //alert([i, rep[i]]);
                if (i == "data") {
                  this.rep = rep[i];
                }
              }

              //console.log(rep);
              //this.rep = result;
              //alert(this.rep.id);
              try {
                // JSON responses are automatically parsed.
                //alert(response.data.id);
                this.editcase(this.rep.id, this.rep);

                this.yardlist[0].caseid = Ncasedocsid;

                //alert(window.$http + `yardlist/` + this.yardlist[0].id);

                axios
                  .put(
                    window.$http + `yardlist/` + this.yardlist[0].id,
                    this.yardlist[0],
                    {
                      headers: window.$headers,
                    }
                  )
                  .then(() => {
                    //saveticket();
                    this.editProsecuted = true;
                    Swal.close();
                    Swal.fire({
                      position: "center",
                      icon: "success",
                      title:
                        this.rep.id +
                        ". Case Details Generated! " +
                        Ncasedocsid,
                      showConfirmButton: false,
                      timer: 1500,
                    }).then(() => {
                      this.updatearray();
                      this.$root.$emit("bv::show::modal", "modal-Case");
                    });
                  });
              } catch (e) {
                Swal.fire({
                  position: "center",
                  icon: "error",
                  title: "Yard List - update " + e,
                  showConfirmButton: true,
                }).then((e) => {
                  Swal.close(e);
                });
              }

              //Swal.close();
            });
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
    },
    search() {
      var filter = "";
      try {
        filter = this.filter;
        this.printReport = true;
        this.filteredValues = "Filtered search '" + filter + "'";
      } catch (e) {
        filter = "";
      }
      if (filter <= 0) {
        Swal.fire("Please enter registration number!").then(() => {
          //this.handleBackToDetails();
        });

        return;
      }
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      axios
        .get(
          window.$http +
            `casedetails/search?limit=` +
            this.perPage +
            `&vehreg=` +
            this.filter,
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          Swal.close();
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
    },
    prosecuteVehicle() {
      var driverid = "";
      try {
        driverid = this.selectedData.driverid.trim();
      } catch (e) {
        //alert(e);
        driverid = "";
        this.selectedData.driverid = "";
      }
      //alert(driverid.trim().length);
      if (driverid.length <= 0) {
        Swal.fire("Please enter driver's ID/Passport No!");
        return;
      }

      var driverslicense = "";
      try {
        driverslicense = this.selectedData.driverslicense.trim();
      } catch (e) {
        driverslicense = "";
        this.selectedData.driverslicense = "";
      }
      if (driverslicense.length <= 0) {
        Swal.fire("Please enter driver License No!");
        return;
      }

      var dnames = "";
      try {
        dnames = this.selectedData.dnames.trim();
      } catch (e) {
        dnames = "";
        this.selectedData.dnames = "";
      }
      if (dnames.length <= 0) {
        Swal.fire("Please enter driver's name!");
        return;
      }

      var dsurname = "";
      try {
        dsurname = this.selectedData.dsurname.trim();
      } catch (e) {
        dsurname = "";
        this.selectedData.dsurname = "";
      }
      if (dsurname.length <= 0) {
        Swal.fire("Please enter driver's surname!");
        return;
      }

      var dgender = "";
      try {
        dgender = this.selectedData.dgender.trim();
      } catch (e) {
        dgender = "";
        this.selectedData.dgender = "";
      }
      if (dgender.length <= 0) {
        Swal.fire("Please enter driver's gender!");
        return;
      }

      var dnationality = "";
      try {
        dnationality = this.selectedData.dnationality.trim();
      } catch (e) {
        dnationality = "";
        this.selectedData.dnationality = "";
      }
      if (dnationality.length <= 0) {
        Swal.fire("Please enter driver's nationality!");
        return;
      }
      if (this.selectedData.paystation <= 0) {
        Swal.fire("Please enter Number of Pay Station(s)!");
        return;
      }

      if (Number(this.selectedData.dage) < 0) {
        Swal.fire("Please enter driver's age!");
        return;
      }

      var daddress = "";
      try {
        daddress = this.selectedData.daddress.trim();
      } catch (e) {
        daddress = "";
        this.selectedData.daddress = "";
      }
      if (daddress.length <= 0) {
        Swal.fire("Please enter driver's address!");
        return;
      }
      //OWNER DETAILS
      var oid = "";
      try {
        oid = this.selectedData.oid.trim();
      } catch (e) {
        oid = "";
        this.selectedData.oid = "";
      }
      if (oid.length <= 0) {
        Swal.fire("Please enter owner's ID/Passport No!");
        return;
      }

      var onames = "";
      try {
        onames = this.selectedData.onames.trim();
      } catch (e) {
        onames = "";
        this.selectedData.onames = "";
      }
      if (onames.length <= 0) {
        Swal.fire("Please enter owner's name!");
        return;
      }

      var osurname = "";
      try {
        osurname = this.selectedData.osurname.trim();
      } catch (e) {
        osurname = "";
        this.selectedData.osurname = "";
      }
      if (osurname.length <= 0) {
        Swal.fire("Please enter owner's surname!");
        return;
      }

      var ogender = "";
      try {
        ogender = this.selectedData.ogender.trim();
      } catch (e) {
        ogender = "";
        this.selectedData.ogender = "";
      }
      if (ogender.length <= 0) {
        Swal.fire("Please enter owner's gender!");
        return;
      }

      var onationality = "";
      try {
        onationality = this.selectedData.onationality.trim();
      } catch (e) {
        onationality = "";
        this.selectedData.onationality = "";
      }
      if (onationality.length <= 0) {
        Swal.fire("Please enter owner's nationality!");
        return;
      }
      if (this.selectedData.oage <= 0) {
        Swal.fire("Please enter owner's age!");
        return;
      }

      var oaddress = "";
      try {
        oaddress = this.selectedData.oaddress.trim();
      } catch (e) {
        oaddress = "";
        this.selectedData.onationality = "";
      }
      if (oaddress.length <= 0) {
        Swal.fire("Please enter owner's address!");
        return;
      }

      // LOCATION DETAILS
      if (this.selectedData.vroad == "") {
        Swal.fire("Please enter road!");
        return;
      }
      if (this.selectedData.district == "") {
        Swal.fire("Please enter district!");
        return;
      }
      if (this.selectedData.weighbridge == "") {
        Swal.fire("Please enter weighbridge!");
        return;
      }
      if (this.selectedData.county == "") {
        Swal.fire("Please enter county!");
        return;
      }

      // COURT DETAIL
      if (this.selectedData.court == "") {
        Swal.fire("Please enter court!");
        return;
      }
      if (this.selectedData.police == "") {
        Swal.fire("Please enter complainant police!");
        return;
      }
      if (this.selectedData.tpolice == "") {
        Swal.fire("Please enter traffic police!");
        return;
      }

      // PROHIBITION DETAILS
      if (this.selectedData.vi == "") {
        Swal.fire("Please enter vehicle inspector!");
        return;
      }
      if (this.selectedData.moveto == "") {
        Swal.fire("Please enter where vehicle is moved to!");
        return;
      }
      if (this.selectedData.rneeded == "") {
        Swal.fire("Please enter the repairs needed!");
        return;
      }
      if (this.selectedData.offload == "") {
        Swal.fire(
          "Please enter caseregno number where excess is offloaded to!"
        );
        return;
      }

      // TRANSPORTER DETAILS
      if (this.selectedData.Tnames == "") {
        Swal.fire("Please enter transporter names!");
        return;
      }
      if (this.selectedData.Taddress == "") {
        Swal.fire("Please enter transporter address!");
        return;
      }

      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });

      this.rep.editedby = window.$userid;
      this.rep.driverid = this.selectedData.driverid.trim();
      this.rep.drivername = this.selectedData.dnames;
      this.rep.driversurname = this.selectedData.dsurname;
      this.rep.drivergender = this.selectedData.dgender;
      this.rep.drivernationality = this.selectedData.dnationality;
      this.rep.paystation = this.selectedData.paystation;
      this.rep.driverage = this.selectedData.dage;
      this.rep.driveraddress = this.selectedData.daddress;
      this.rep.driverslicense = this.selectedData.driverslicense;

      this.rep.transportername = this.selectedData.Tnames;
      this.rep.transporteradd = this.selectedData.Taddress;

      this.rep.repairsneeded = this.selectedData.rneeded;
      this.rep.offloadreg = this.selectedData.offload;

      this.rep.ownerid = this.selectedData.oid;
      this.rep.ownername = this.selectedData.onames;
      this.rep.ownergender = this.selectedData.ogender;
      this.rep.ownernationality = this.selectedData.onationality;
      this.rep.ownerage = this.selectedData.oage;
      this.rep.ownersurname = this.selectedData.osurname;
      this.rep.owneraddress = this.selectedData.oaddress;
      this.rep.drivergender = this.selectedData.dgender;
      this.rep.ownergender = this.selectedData.ogender;
      this.rep.alongroad = this.selectedData.vroad;
      this.rep.district = this.selectedData.district;
      this.rep.weighbrige = this.selectedData.weighbridge;
      this.rep.county = this.selectedData.county;
      this.rep.court = this.selectedData.court;
      this.rep.comppolice = this.selectedData.police;
      this.rep.trafficofficer = this.selectedData.tpolice;
      this.rep.vehicleins = this.selectedData.vi;
      this.rep.movevehicleto = this.selectedData.moveto;
      //alert(this.rep.vehicleins + "this.rep.vehicleins ");
      try {
        axios
          .put(window.$http + `casedetails/` + this.rep.id, this.rep, {
            headers: window.$headers,
          })
          .then(() => {
            // JSON responses are automatically parsed.
            this.$root.$emit("bv::hide::modal", "modal-Case");
            //var result = [];

            //alert(window.$http + `yardlist/` + this.yardlist[0].id);

            Swal.close();
          })
          .catch((e) => {
            Swal.fire({
              position: "center",
              icon: "error",
              title: "" + e,
              showConfirmButton: true,
            }).then((e) => {
              Swal.close(e);
            });

            //throw e;
          });
      } catch (e) {
        Swal.fire({
          position: "center",
          icon: "error",
          title: "Prosecution Details - update " + e,
          showConfirmButton: true,
        }).then((e) => {
          Swal.close(e);
        });
      }
    },
    /**
     * Add Documents Functions
     */
    getdoclist(pta) {
      this.id = pta.item.id;
      this.caseid = pta.item.caseid;
      this.caseticket = pta.item.caseticket;
      this.caseregno = pta.item.caseregno;
      this.axle_code = pta.item.ticketaxletype;
      this.from = window.$stationcode;
      this.froms = window.$station;
      this.overloadw = pta.item.caseexcessload.toLocaleString();
      this.BillingStitle =
        "Billing Instruction Sheet For WBTicket:" +
        this.caseticket +
        " Registration:" +
        this.caseregno +
        " Transporter:" +
        pta.item.transportername;
      this.LoadCtitle =
        "Load Correction Memo For WBTicket:" +
        this.caseticket +
        " For Registration:" +
        this.caseregno +
        " Transporter:" +
        pta.item.transportername;
      //alert(pta.item.casedocs[0].docname);
      // var prob = pta.item.casedocs;
      //alert(prob.docname);
      // for (let i = 0; i < prob.length; i++) {
      //   if (pta.item.casedocs.length == 0) {
      //     this.problist = new Array("Prohibition Order");
      //   }
      // }
      //alert;
      //alert(pta.item.casedocs.length);
      // var problist = [
      //   "Prohibition Order",
      //   "Conditional Load Correction Memo",
      //   "Charge Sheet",
      //   "Billing Instruction",
      //   "Load Correction Memo",
      // ];
      this.problist = new Array("");
      //alert(pta.item.casedocs[0].cancelled);
      var prob = false;
      var cond = false;
      for (let i = 0; i < pta.item.casedocs.length; i++) {
        if (
          pta.item.casedocs[i].docname == "Prohibition Order" &&
          pta.item.casedocs[i].cancelled == "N"
        ) {
          prob = true;
        }
        if (
          pta.item.casedocs[i].docname == "Conditional Load Correction Memo" &&
          pta.item.casedocs[i].cancelled == "N"
        ) {
          cond = true;
        }
        if (
          pta.item.casedocs[i].docname == "Charge Sheet" &&
          pta.item.casedocs[i].cancelled == "N"
        ) {
          cond = true;
        }
      }
      if (pta.item.casedocs.length == 0 || prob == false) {
        this.problist = new Array("Prohibition Order");
        return;
      }
      if (pta.item.casedocs.length == 1 || cond == false) {
        this.problist = new Array(
          "Conditional Load Correction Memo",
          "Charge Sheet"
        );
        return;
      }
      //var prob = [];
      var ChargeSheet = false;
      var CLCM = false;
      //var Traffic = false;
      var EAC = false;
      //var ConditionalLoadCorrectionMemo = false;
      var LoadCorrectionMemo = false;
      var BillingInstructionSheet = false;
      //alert("case doc len " + pta.item.casedocs.length);
      if (pta.item.casedocs.length >= 2) {
        for (let i = 0; i < pta.item.casedocs.length; i++) {
          // prob.push(pta.item.casedocs.docname);
          if (
            pta.item.casedocs[i].docname ==
              "Conditional Load Correction Memo" &&
            pta.item.casedocs[i].cancelled == "N"
          ) {
            CLCM = true;
          }
          if (
            pta.item.casedocs[i].docname == "Charge Sheet" &&
            pta.item.casedocs[i].cancelled == "N"
          ) {
            ChargeSheet = true;
          }
          // if (
          //   pta.item.casedocs[i].docname ==
          //     "Conditional Load Correction Memo" &&
          //   pta.item.casedocs[i].cancelled == "N"
          // ) {
          //   ConditionalLoadCorrectionMemo = true;
          // }
          if (
            (pta.item.casedocs[i].docname == "EAC Act" ||
              pta.item.casedocs[i].docname == "Traffic Act") &&
            pta.item.casedocs[i].cancelled == "N"
          ) {
            EAC = true;
          }

          if (
            pta.item.casedocs[i].docname == "Billing Instruction Sheet" &&
            pta.item.casedocs[i].cancelled == "N"
          ) {
            BillingInstructionSheet = true;
          }
          if (
            pta.item.casedocs[i].docname == "Load Correction Memo" &&
            pta.item.casedocs[i].cancelled == "N"
          ) {
            LoadCorrectionMemo = true;
          }
        }
      }
      //alert("ChargeSheet " + ChargeSheet);
      //alert("ConditionalLoadCorrectionMemo " + ConditionalLoadCorrectionMemo);
      if (!CLCM) {
        if (!ChargeSheet) {
          this.problist = new Array("Conditional Load Correction Memo");
          return;
        }
      }
      if (!ChargeSheet) {
        this.problist = new Array("Charge Sheet");
        return;
      }

      if (ChargeSheet) {
        if (!EAC) {
          this.problist = new Array("EAC Act", "Traffic Act");
          return;
        }

        if (!BillingInstructionSheet) {
          this.problist = new Array("Billing Instruction Sheet");
          return;
        }
        if (!LoadCorrectionMemo) {
          this.problist = new Array("Load Correction Memo");
          return;
        }
      }
    },
    ticketdate() {
      let d = new Date();
      let year = d.getFullYear();
      let month = d.getMonth() + 1;
      month = this.getv(month);
      //const msec = d.getMilliseconds();
      const datetime = year + "" + month + "";
      return datetime;
    },
    getcurrentdate() {
      let d = new Date();
      let year = d.getFullYear();
      let month = d.getMonth() + 1;
      let date = d.getDate();
      date = this.getv(date);
      month = this.getv(month);

      let hour = d.getHours();
      let min = d.getMinutes();
      let sec = d.getSeconds();
      hour = this.getv(hour);
      min = this.getv(min);
      sec = this.getv(sec);

      //const msec = d.getMilliseconds();
      const datetime =
        year + "-" + month + "-" + date + "T" + hour + ":" + min + ":" + sec;
      return datetime;
    },
    padLeadingZeros(num, size) {
      var s = num + "";
      while (s.length < size) s = "0" + s;
      return s;
    },
    getaxleconf(group) {
      var conf = 0;
      for (let i = 0; i < this.AxleWeightXreffs.length; i++) {
        if (this.AxleWeightXreffs[i].axle_code == this.axle_code) {
          //alert(this.AxleWeightXreffs[i].axle_deckgrouping);
          //alert(group);
          if (this.AxleWeightXreffs[i].axle_deckgrouping == group) {
            conf++;
          }
        }
      }
      //alert(conf);
      return conf;
    },
    geteactdetails(ticketno, caseid, casedocid, act) {
      //var nodata = [];
      //alert(oi.item.caseticket);
      //alert(ticketno);

      try {
        axios
          .get(
            window.$http +
              `weighbridgetransactions/search?limit=1&wbrg_ticket_no=` +
              ticketno,
            {
              headers: window.$headers,
            }
          )
          .then((response) => {
            this.geteactdetails2(
              ticketno,
              caseid,
              casedocid,
              act,
              response.data[0]
            );
            // return response.data[0].cluster_name;
          })
          .catch(() => {});
      } catch (Exception) {
        console.log();
      }
    },
    geteactdetails2(ticketno, caseid, casedocid, act, nodata) {
      //var gvwfee = new Double[2]();
      //alert(window.$http + `axleweights/search?wbrg_ticket_no=` + ticketno);
      axios
        .get(window.$http + `axleweights/search?wbrg_ticket_no=` + ticketno, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          // this.cargos = response.data.cargo_name;
          var orderData = [];
          if (
            nodata.wbrg_ticket_type == "L" ||
            nodata.wbrg_ticket_type == "S"
          ) {
            var a = 0;
            var b = 0;
            var c = 0;
            var d = 0;

            var pa = 0;
            var pb = 0;
            var pc = 0;
            var pd = 0;
            //console.log(orderData3);
            var orderd = response.data;

            for (let k = 0; k < orderd.length; k++) {
              //alert(orderd.length + " - " + orderd[k].axle_grouping);
              //console.log(orderd.length + " - " + orderd[k].axle_grouping);
              if (orderd[k].axle_grouping == "A") {
                a = a + Number(orderd[k].axle_actualweight);
                pa = pa + Number(orderd[k].axle_permissibleweight);

                orderData[0] = orderd[k];
                orderData[0].axle_actualweight = a + "";
                orderData[0].axle_permissibleweight = pa + "";
                //alert(orderd[k].axle_grouping);
              }

              if (orderd[k].axle_grouping == "B") {
                b = b + Number(orderd[k].axle_actualweight);
                pb = pb + Number(orderd[k].axle_permissibleweight);

                orderData[1] = orderd[k];
                orderData[1].axle_actualweight = b + "";
                //alert(orderData[1].axle_actualweight);
                orderData[1].axle_permissibleweight = pb + "";
              }

              if (orderd[k].axle_grouping == "C") {
                c = c + Number(orderd[k].axle_actualweight);
                pc = pc + Number(orderd[k].axle_permissibleweight);

                orderData[2] = orderd[k];
                orderData[2].axle_actualweight = c + "";
                orderData[2].axle_permissibleweight = pc + "";
                //alert(orderd[k].axle_grouping);
              }

              if (orderd[k].axle_grouping == "D") {
                d = d + Number(orderd[k].axle_actualweight);
                pd = pd + Number(orderd[k].axle_permissibleweight);
                orderData[3] = orderd[k];
                orderData[3].axle_actualweight = d + "";
                orderData[3].axle_permissibleweight = pd + "";
                //alert(orderd[k].axle_grouping);
              }
            }
          } else {
            orderData = response.data;
          }

          //var axleoverl = 0;
          var gvwactual = 0;
          var gvwperm = 0;
          var steeringaxle = 0;
          var singledriveaxle = 0;
          var tandemaxle = 0;
          var tridemaxle = 0;

          for (let i = 0; i < orderData.length; i++) {
            var actual = Number(orderData[i].axle_actualweight);
            var perm = Number(orderData[i].axle_permissibleweight);
            var tollerance = (perm * 5) / 100 + perm;
            var overload = 0;
            if (actual - tollerance > 0) {
              overload = actual - tollerance;
            }
            var conf = 0;
            if (i == 0) {
              conf = this.getaxleconf("A");

              steeringaxle += overload;
            }
            if (i == 1) {
              //alert(overload);
              conf = this.getaxleconf("B");
              //alert(overload);
              //alert(conf + "conf");
              if (conf == 1) {
                singledriveaxle += overload;
              }
              if (conf == 2) {
                tandemaxle += overload;
              }
              if (conf == 3) {
                tridemaxle += overload;
              }
            }
            if (i == 2) {
              conf = this.getaxleconf("C");
              if (conf == 1) {
                singledriveaxle += overload;
              }
              if (conf == 2) {
                tandemaxle += overload;
              }
              if (conf == 3) {
                tridemaxle += overload;
              }
            }
            if (i == 3) {
              conf = this.getaxleconf("D");
              if (conf == 1) {
                singledriveaxle += overload;
              }
              if (conf == 2) {
                tandemaxle += overload;
              }
              if (conf == 3) {
                tridemaxle += overload;
              }
            }
            // doc.text(120, lane4 + inc, "" + orderData[i].axle_actualweight);
            // doc.text(140, lane4 + inc, "" + overload);
            // doc.text(163, lane4 + inc, "" + result);

            //axleoverl += Number(overload);

            gvwperm += perm;
            gvwactual += actual;
          }
          var lesspermit = 0;
          try {
            //alert(nodata.permit_no.trim().toUpperCase().substring(0, 4));
            if (
              nodata.permit_no.trim().toUpperCase().substring(0, 4) == "APP-"
            ) {
              if (Number(nodata.wbrg_ticket_axel.substring(0, 1)) == 2) {
                lesspermit = 1000;
              }
              if (
                Number(nodata.wbrg_ticket_axel.substring(0, 1)) == 3 ||
                Number(nodata.wbrg_ticket_axel.substring(0, 1)) == 4
              ) {
                lesspermit = 2000;
              }
            }
          } catch (e) {
            console.log(e);
          }
          var gvwoverload = gvwactual - (gvwperm + lesspermit);
          var period = "2021-2030";
          //alert(
          //   window.$http +
          //     `eacact/charges?period=` +
          //     period +
          //     `&GVWOverloadkg=` +
          //     gvwoverload +
          //     "&steeringaxle=" +
          //     steeringaxle +
          //     "&singledriveaxle=" +
          //     singledriveaxle +
          //     "&tandemaxle=" +
          //     tandemaxle +
          //     "&tridemaxle=" +
          //     tridemaxle +
          //     "&caseid=" +
          //     caseid +
          //     "&casedocid=" +
          //     casedocid +
          //     "&act=" +
          //     act +
          //     "&vehreg=" +
          //     this.caseregno +
          //     ""
          // );

          axios
            .get(
              window.$http +
                `eacact/charges?period=` +
                period +
                `&GVWOverloadkg=` +
                gvwoverload +
                "&steeringaxle=" +
                steeringaxle +
                "&singledriveaxle=" +
                singledriveaxle +
                "&tandemaxle=" +
                tandemaxle +
                "&tridemaxle=" +
                tridemaxle +
                "&caseid=" +
                caseid +
                "&casedocid=" +
                casedocid +
                "&act=" +
                act +
                "&vehreg=" +
                this.caseregno +
                "",
              {
                headers: window.$headers,
              }
            )
            .then((response) => {
              // JSON responses are automatically parsed.
              //Swal.close();
              //this.dec = response.data[0];
              //console.log(response.data[0]);
              //alert(Number(response.data[0].gvwfees));
              //alert(window.$http + `eacact/`);
              var axlefee = 0;
              var demeritspoints = 0;
              var cdemeritspoints = 0;

              var steeringaxlefee = Number(response.data[0].steeringaxlefee);
              var singledriveaxlefee = Number(
                response.data[0].singledriveaxlefee
              );

              var tandemaxlefee = Number(response.data[0].tandemaxlefee);
              var tridemaxlefee = Number(response.data[0].tridemaxlefee);
              axlefee =
                steeringaxlefee +
                singledriveaxlefee +
                tandemaxlefee +
                tridemaxlefee;

              var vehicle_demeritpoints = Number(
                response.data[0].vehicle_demeritpoints
              );
              var steeringaxlepoint = Number(
                response.data[0].steeringaxlepoint
              );
              var singledriveaxlepoint = Number(
                response.data[0].singledriveaxlepoint
              );
              var tandemaxlepoint = Number(response.data[0].tandemaxlepoint);
              var tridemaxlepoint = Number(response.data[0].tridemaxlepoint);
              var gvwpoints = Number(response.data[0].gvwpoints);

              var arr = [
                Number(gvwoverload),
                Number(steeringaxle),
                Number(singledriveaxle),
                Number(tandemaxle),
                Number(tridemaxle),
              ];

              var max_of_array = Math.max.apply(Math, arr);

              if ((tandemaxle = Number(max_of_array))) {
                demeritspoints = tandemaxlepoint;
              }

              if ((tridemaxle = Number(max_of_array))) {
                demeritspoints = tridemaxlepoint;
              }
              if ((singledriveaxle = Number(max_of_array))) {
                demeritspoints = singledriveaxlepoint;
              }
              if ((steeringaxle = Number(max_of_array))) {
                demeritspoints = steeringaxlepoint;
              }
              if ((gvwoverload = Number(max_of_array))) {
                demeritspoints = gvwpoints;
              }

              // demeritspoints =
              //   gvwpoints +
              //   steeringaxlepoint +
              //   singledriveaxlepoint +
              //   tandemaxlepoint +
              //   tridemaxlepoint;
              cdemeritspoints = vehicle_demeritpoints + demeritspoints;
              var data = {
                caseid: caseid,
                datetime: this.getcurrentdate(),
                casedocid: casedocid,
                gvwfee: response.data[0].gvwfees,
                axlefee: axlefee,
                demeritspoints: demeritspoints,
                cdemeritspoints: cdemeritspoints,
                penalties: response.data[0].penalty,
                dollarrate: response.data[0].dollarrates,
                act: act,
              };
              //alert(response.data[0].gvwfees + `response.data[0].gvwfees`);
              console.log(data);
              //alert(window.$http + `eacact/`);
              axios
                .post(window.$http + `eacact/`, data, {
                  headers: window.$headers,
                })
                .then(() => {
                  Swal.fire({
                    position: "center",
                    icon: "success",
                    title: " Document Created! ",
                    showConfirmButton: false,
                    timer: 1500,
                  }).then(() => {
                    //this.updatearray();
                    //this.getdoc(data, docs.docname, docs.id);
                  });
                })
                .catch((e) => {
                  Swal.fire("" + e);
                  //this.errors.push(e);
                });
            })
            .catch((e) => {
              Swal.fire({
                position: "center",
                icon: "error",
                title: "" + e,
                showConfirmButton: true,
              }).then((e) => {
                Swal.close(e);
              });

              //throw e;
            });

          Swal.close();
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Origins" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
      //alert(caseid);
    },
    async updatecasedocs(doc) {
      var mycasid = "";

      if (doc == "Prohibition Order") {
        mycasid = "PO";
      }
      if (doc == "Conditional Load Correction Memo") {
        mycasid = "CL";
      }
      if (doc == "Charge Sheet") {
        mycasid = "CS";
      }
      if (doc == "Billing Instruction Sheet") {
        mycasid = "BI";
      }
      if (doc == "Load Correction Memo") {
        mycasid = "LC";
      }

      if (doc == "EAC Act") {
        mycasid = "EA";
      }
      if (doc == "Traffic Act") {
        mycasid = "TA";
      }
      //screen and roles combination
      //alert(window.$http);
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });

      if (doc == "EAC Act") {
        //alert(
        //   window.$http +
        //     `casedocs/records?caseid=` +
        //     this.caseid +
        //     `&docname=EAC Act`
        // );
        var caseexits = await axios
          .get(
            window.$http +
              `casedocs/records?caseid=` +
              this.caseid +
              `&docname=EAC Act`,
            {
              headers: window.$headers,
            }
          )
          .then((res) => {
            //alert(res.data.length + "res.data.length");
            if (res.data.length > 0) {
              return true;
            } else {
              return false;
            }
          })
          .catch((e) => {
            Swal.fire({
              position: "center",
              icon: "error",
              title: "" + e,
              showConfirmButton: true,
            }).then((e) => {
              Swal.close(e);
              return false;
            });

            //throw e;
          });

        var ticketno = this.caseticket;
        if (ticketno.includes("_")) {
          var tick = ticketno.split("_");
          ticketno = tick[0];
          //alert(ticketno);
        }
        //alert(
        //   window.$http +
        //     `weighbridgetransactions/search?limit=1&status=O&allwbt=all&wbrg_ticket_no=` +
        //     ticketno
        // );
        await axios
          .get(
            window.$http +
              `weighbridgetransactions/search?limit=1&status=O&allwbt=all&wbrg_ticket_no=` +
              ticketno,
            {
              headers: window.$headers,
            }
          )
          .then((response) => {
            // JSON responses are automatically parsed.
            this.ticketsshow = response.data;
            //alert(this.ticketsshow[0].wbrg_ticket_no + " Data");
            // this.editmode = true;
            //var id = response.data.id;
            //console.log(this.orderData);
          });

        //alert(this.ticketsshow[0].wbrg_ticket_no);
        // alert("caseexits" + caseexits);
        if (!caseexits) {
          this.updateticket();
        }
      }

      axios
        .get(
          window.$http +
            `casedocs/max?casedocid=` +
            mycasid +
            window.$stationcode +
            this.ticketdate(),
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          //alert(response.data);
          let ticketmax = Number(response.data);
          ticketmax++;
          var ticketno = this.padLeadingZeros(ticketmax, 6);
          var Ncasedocsid =
            mycasid + window.$stationcode + this.ticketdate() + ticketno;
          var data = {
            casedocid: Ncasedocsid,
            caseid: this.caseid,
            docname: doc,
            docdate: this.getcurrentdate(),
            createdby: window.$userid,
            casedetailsid: this.id,
            cancelled: "N",
            printed: "N",
          };
          //alert("Ncasedocsid" + Ncasedocsid + "  casedetailsid" + this.id);

          axios
            .post(window.$http + `casedocs/`, data, {
              headers: window.$headers,
            })
            .then(() => {
              // JSON responses are automatically parsed.
              if (doc == "EAC Act") {
                this.geteactdetails(
                  this.caseticket,
                  this.caseid,
                  Ncasedocsid,
                  "E"
                );
              }
              if (doc == "Traffic Act") {
                this.geteactdetails(
                  this.caseticket,
                  this.caseid,
                  Ncasedocsid,
                  "T"
                );
              }
              Swal.close();
              this.$root.$emit("bv::hide::modal", "modal-Doc");
              //this.getcase(doc);
              Swal.fire({
                position: "center",
                icon: "success",
                title: " Document Created! " + doc,
                showConfirmButton: false,
                timer: 1500,
              }).then(() => {
                this.updatearray();
                //this.getdoc(data, docs.docname, docs.id);
              });
            })
            .catch((e) => {
              Swal.fire({
                position: "center",
                icon: "error",
                title: "Case Document " + doc + e,
                showConfirmButton: true,
              }).then((e) => {
                Swal.close(e);
              });

              //throw e;
            });
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
    },
    async updateticket() {
      var dataget = await axios
        .get(window.$http + `casedetails/search?caseid=` + this.caseid, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          console.log(response.data[0]);
          return response.data[0];
        });
      //alert(window.$http + `casedetails/records?caseid=` + this.caseid);

      dataget.caseticket = this.ticketsshow[0].wbrg_ticket_no;
      dataget.caseregno = this.ticketsshow[0].veh_reg;
      dataget.WBT_operator = this.ticketsshow[0].wbrg_ticket_operator;
      dataget.ticketaxletype = this.ticketsshow[0].wbrg_ticket_axel;
      dataget.weighdate = this.ticketsshow[0].wbrg_ticket_date;
      dataget.vehiclemake = this.ticketsshow[0].wbrg_ticket_make;
      dataget.tickettransporter = this.ticketsshow[0].wbrg_tx_code;
      dataget.transportername = this.ticketsshow[0].wbrg_tx_code;
      dataget.drivername = this.ticketsshow[0].wbrg_ticket_driver;
      dataget.caseexcessload = this.ticketsshow[0].wbrg_ticket_excessweight;
      dataget.WBT_chargedon = this.ticketsshow[0].wbrg_ticket_excessload;

      //alert(dataget.caseticket + " -- ");
      console.log(dataget);
      axios
        .put(window.$http + `casedetails/` + this.id, dataget, {
          headers: window.$headers,
        })
        .then(() => {
          //saveticket();
          //Swal.close();
          Swal.fire({
            position: "center",
            icon: "success",
            title: "updated  casedetails",
            showConfirmButton: false,
            timer: 1500,
          }).then(() => {});
        })
        .catch((e) => {
          Swal.fire("Error!\n " + e);
          //Swal.close();
          //throw e;
        });
    },
    adddoc(val) {
      // let inx = this.problist.indexOf(val);
      Swal.fire({
        title: "Are you sure, you want to add document " + val + "?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, Add (" + val + ")!",
      }).then((result) => {
        if (result.value) {
          //insert record casedocs
          if (val == "Billing Instruction Sheet") {
            this.$root.$emit("bv::show::modal", "modal-Billing");
            return;
          }
          if (val == "Load Correction Memo") {
            //this.LoadCtitle = val;
            this.getcaseresults();
            this.$root.$emit("bv::show::modal", "modal-LoadC");
            return;
          }
          if (val == "Charge Sheet") {
            //this.updatecasedocs("EAC Act");
            //return;
          }

          this.updatecasedocs(val);
        }
      });
    },
    clearvalues() {
      this.editmode = false;
      this.nplate = "";
      this.ticketsshow = [];
    },
    fetchusers() {
      // Swal.fire({
      //   title: "Please Wait !",
      //   html: "Loading data...", // add html attribute if you want or remove
      //   allowOutsideClick: false,
      //   showConfirmButton: false,
      //   willOpen: () => {
      //     Swal.showLoading();
      //   },
      // });
      var UserRole = "Ins";
      // this.VIs = [];
      axios
        .get(window.$http + `Users/search?UserRole=` + UserRole, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          console.log(response);
          response.data.forEach((e) => {
            this.VIs.push(e.fullnames.toUpperCase());
          });
          //Swal.close();
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });
        });
      UserRole = "Police";
      axios
        .get(window.$http + `Users/search?UserRole=` + UserRole, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          //this.tpolices = response.data;
          response.data.forEach((e) => {
            this.tpolices.push(e.fullnames.toUpperCase());
          });
          //Swal.close();
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });
        });
    },
    resetSelectData() {
      this.selectedData = {
        driverid: "",
        driverslicense: "",
        dnames: "",
        dsurname: "",
        dgender: "",
        dnationality: "Kenyan",
        paystation: 1,
        dage: "40",
        daddress: "",
        oid: "",
        onames: "",
        osurname: "",
        ogender: "",
        onationality: "Kenyan",
        oage: "40",
        oaddress: "",
        vroad: "",
        district: "",
        weighbridge: "",
        weighbridgecode: "",
        county: "",
        court: "",
        police: "",
        tpolice: "",
        vi: " ",
        moveto: "",
        rneeded: "",
        offload: "",
        Tnames: "",
        Taddress: "",
      };
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <!-- {{ dec }} -->
    <form @submit.prevent="handleSubmit">
      <div class="row">
        <div class="card changebg">
          <div class="card-body">
            <div class="row justify-content-between">
              <div class="col-sm-6">
                <button
                  class="btn btn-primary waves-effect waves-light uil-export"
                  @click="getrpt(p_excel)"
                >
                  Export to CSV
                </button>
              </div>

              <div class="col-sm-2">
                <button
                  @click="printpdf('p', p_pdf)"
                  v-b-modal.modal-Print
                  class="btn btn-primary waves-effect waves-light mdi-file-pdf"
                >
                  Print PDF
                </button>
              </div>

              <div class="col-sm-1">
                <button
                  @click="clearvalues()"
                  v-b-modal.modal-Prosecute
                  class="btn btn-primary waves-effect waves-light"
                >
                  Prosecute
                </button>
              </div>

              <!-- ///// -->
            </div>
          </div>
        </div>
        <div class="col-sm-12">
          <div class="card">
            <div class="card-body changebg">
              <b-card-title>
                <h5 class="card-title"></h5>
              </b-card-title>

              <div class="row">
                <div class="col-12">
                  <div>
                    <div class="float-end">
                      <form class="d-inline-flex mb-1"></form>
                    </div>
                  </div>
                  <div
                    class="
                      table table-centered
                      datatable
                      dt-responsive
                      nowrap
                      table-card-list
                      dataTable
                      no-footer
                      dtr-inline
                    "
                  >
                    <div class="row">
                      <div class="col-sm-12 col-md-6">
                        <div
                          id="tickets-table_length"
                          class="dataTables_length"
                        >
                          <label
                            class="d-inline-flex align-items-center fw-normal"
                          >
                            Show&nbsp;
                            <b-form-select
                              @input="updatearray()"
                              v-model="perPage"
                              size="sm"
                              :options="pageOptions"
                            ></b-form-select
                            >&nbsp;entries
                          </label>
                        </div>
                      </div>

                      <!-- Search -->
                      <div class="col-sm-12 col-md-6">
                        <div
                          id="tickets-table_filter"
                          class="dataTables_filter text-md-end"
                        >
                          <label
                            class="d-inline-flex align-items-center fw-normal"
                          >
                            Search:
                            <b-form-input
                              v-model="filter"
                              type="search"
                              placeholder="Search..."
                              class="form-control form-control-sm ms-2"
                            ></b-form-input>
                            <b-button
                              variant="dark"
                              class="uil-search"
                              @click="search()"
                              >Search</b-button
                            >
                          </label>
                        </div>
                      </div>
                      <!-- End search -->
                    </div>

                    <!-- Table -->

                    <b-table
                      table-class="table table-centered datatable table-card-list"
                      thead-tr-class="bg-transparent"
                      :items="orderData"
                      :fields="fields"
                      responsive="sm"
                      :per-page="perPage"
                      :current-page="currentPage"
                      :sort-by.sync="sortBy"
                      :sort-desc.sync="sortDesc"
                      :filter="filter"
                      :filter-included-fields="filterOn"
                      @filtered="onFiltered"
                    >
                      <template v-slot:cell(prohibit)="data">
                        <div class="button mb-1">
                          <input
                            type="button"
                            variant="primary"
                            class="btn btn-primary waves-effect waves-light"
                            :id="`contacusercheck${data.item.prohibit}`"
                            value="Edit Case Details"
                            v-b-modal.modal-Case
                            @click="editcase(data.item.id, data.item)"
                          />
                        </div>

                        <div class="button">
                          <input
                            type="button"
                            variant="primary"
                            class="btn btn-primary waves-effect waves-light"
                            :id="`contacusercheck${data.item.prohibit}`"
                            value="Add Documents"
                            v-b-modal.modal-Doc
                            @click="getdoclist(data)"
                          />
                        </div>
                      </template>

                      <template v-slot:cell(id)="data">
                        <a
                          href="javascript: void(0);"
                          class="text-dark fw-bold"
                          >{{ data.item.id }}</a
                        >
                      </template>
                      <template v-slot:cell(casedatetime)="data">
                        <a
                          href="javascript: void(0);"
                          class="text-dark fw-bold"
                          >{{ data.item.casedatetime.replace("T", " ") }}</a
                        >
                      </template>
                      <template v-slot:cell(caseid)="data">
                        <div
                          v-show="
                            docs.docname == 'Prohibition Order' &&
                            docs.cancelled == 'N'
                          "
                          v-for="docs in data.item.casedocs"
                          :key="docs.id"
                        >
                          {{ data.item.casedocs[0].casedocid }}
                          <a>{{ data.item.caseticket }} </a>
                        </div>
                      </template>

                      <template v-slot:cell(casedocs)="data">
                        <div
                          class=""
                          role="alert"
                          v-for="docs in data.item.casedocs"
                          :key="docs.id"
                        >
                          <ul class="list-inline mb-0">
                            <li class="list-inline-item">
                              <div
                                class="badge bg-pill font-size-12"
                                :class="
                                  docs.cancelledby
                                    ? 'bg-soft-danger'
                                    : 'bg-soft-success'
                                "
                                role="alert"
                              >
                                <a
                                  v-show="
                                    docs.cancelled == 'Y'
                                      ? 'px-1 text-danger'
                                      : 'px-1 text-primary'
                                  "
                                  href="javascript:void(0);"
                                  :class="
                                    docs.cancelled == 'Y'
                                      ? 'px-1 text-danger'
                                      : 'px-1 text-primary'
                                  "
                                  :title="docs.docname"
                                >
                                  <strong
                                    v-b-tooltip.hover
                                    @click="getdoc(data, docs.docname)"
                                  >
                                    {{ docs.docname }} ({{ docs.createdby }})
                                  </strong>
                                </a>
                              </div>
                              {{ docs.cancelledby }}

                              <button
                                v-show="docs.cancelled == 'Y' ? false : true"
                                type="button"
                                class="close"
                                data-dismiss="alert"
                                aria-label="Close"
                                @click="
                                  remove(
                                    docs.id,
                                    docs.docname,
                                    data.item.caseregno,
                                    docs
                                  )
                                "
                              >
                                <i
                                  v-show="
                                    docs.docname != 'Compliance Certificate' &&
                                    docs.docname != 'Special Release' &&
                                    docs.cancelled == 'N'
                                  "
                                  class="
                                    uil uil-trash-alt
                                    font-size-18
                                    text-right
                                  "
                                  data-dismiss="alert"
                                  aria-label="Close"
                                ></i>
                              </button>
                            </li>
                          </ul>
                          <!-- <button
                            type="button"
                            class="close"
                            data-dismiss="alert"
                            aria-label="Close"
                          >
                            <span aria-hidden="true">&times;</span>
                          </button> -->
                        </div>
                      </template>

                      <template v-slot:cell(action)="data">
                        <div class="badge bg-pill bg-soft-success font-size-12">
                          {{ data.item.action }}
                        </div>
                      </template>
                    </b-table>
                  </div>
                  <div class="row">
                    <div class="col">
                      <div
                        class="
                          dataTables_paginate
                          paging_simple_numbers
                          float-end
                        "
                      >
                        <ul class="pagination pagination-rounded">
                          <!-- pagination -->
                          <b-pagination
                            v-model="currentPage"
                            :total-rows="rows"
                            :per-page="perPage"
                          ></b-pagination>
                        </ul>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </form>
    <!-- MODAL START-->
    <b-modal id="modal-Doc" title="Documents" hide-footer size="sm" centered>
      <div class="card-body">
        <div class="mb-1" v-for="prob in problist" :key="prob">
          <button type="submit" class="btn btn-primary" @click="adddoc(prob)">
            {{ prob }}
          </button>
        </div>
      </div>
    </b-modal>
    <!-- MODAL START-->

    <b-modal id="modal-Case" :title="title" hide-footer size="xl" centered>
      <div class="card-body">
        <b-tabs
          pills
          vertical
          nav-class="p-0"
          nav-wrapper-class="col-sm-3"
          content-class="pt-0 px-2 text-muted"
        >
          <b-tab active class="border-0">
            <template v-slot:title>
              <span class="d-inline-block d-sm-none">
                <i class="fas fa-home"></i>
              </span>
              <span class="d-none d-sm-inline-block">Driver Details</span>
            </template>

            <div class="input-group">
              <div class="input-group-text col-sm-3">
                Driver ID / Passport No.
              </div>
              <input
                type="text"
                class="form-control"
                placeholder="ID"
                v-model="selectedData.driverid"
                name="driverid"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Driving License No.:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Driving License Number"
                v-model="selectedData.driverslicense"
                name="dnames"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Driver Full Names.</div>
              <input
                type="text"
                class="form-control"
                placeholder="Driver Full Names"
                v-model="selectedData.dnames"
                name="dnames"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">
                Driver Surname/Father's name:
              </div>
              <input
                type="text"
                class="form-control"
                placeholder="Driver Surname"
                v-model="selectedData.dsurname"
                name="dsurname"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Driver Gender:</div>
              <select
                class="form-control"
                name="dgender"
                v-model="selectedData.dgender"
              >
                <option value="">-Choose One-</option>
                <option value="Male">Male</option>
                <option value="Female">Female</option>
              </select>
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Nationality:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Driver Nationality"
                v-model="selectedData.dnationality"
                name="dnationality"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Age:</div>
              <input
                type="number"
                class="form-control"
                placeholder="Driver Age"
                v-model="selectedData.dage"
                name="dage"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Driver Address:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Driver Address"
                v-model="selectedData.daddress"
                name="daddress"
                required
              />
            </div>
          </b-tab>

          <b-tab>
            <template v-slot:title>
              <span class="d-inline-block d-sm-none">
                <i class="far fa-user"></i>
              </span>
              <span class="d-none d-sm-inline-block">Owners Details</span>
            </template>

            <div class="input-group">
              <div class="input-group-text col-sm-3">
                Owner ID / Passport No.
              </div>
              <input
                type="text"
                class="form-control"
                placeholder="Owners id"
                v-model="selectedData.oid"
                name="oid"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Owner Full Names.</div>
              <input
                type="text"
                class="form-control"
                placeholder="Owners Name"
                v-model="selectedData.onames"
                name="onames"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">
                Owner Surname/Fathers name.
              </div>
              <input
                type="text"
                class="form-control"
                placeholder="Owners Surname"
                v-model="selectedData.osurname"
                name="osurname"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Owner Gender:</div>
              <select
                class="form-control"
                name="ogender"
                v-model="selectedData.ogender"
              >
                <option value="">-Choose One-</option>
                <option value="Male">Male</option>
                <option value="Female">Female</option>
              </select>
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Owner Nationality:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Owners Nationality"
                v-model="selectedData.onationality"
                name="onationality"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Owner Age:</div>
              <input
                type="Number"
                class="form-control"
                placeholder="Owners Age"
                v-model="selectedData.oage"
                name="oage"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Owner Address:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Owner Address"
                v-model="selectedData.oaddress"
                name="oaddress"
                required
              />
            </div>
          </b-tab>

          <b-tab>
            <template v-slot:title>
              <span class="d-inline-block d-sm-none">
                <i class="far fa-envelope"></i>
              </span>
              <span class="d-none d-sm-inline-block">Location Details</span>
            </template>

            <div class="input-group">
              <div class="input-group-text col-sm-3">
                Vehicle Used Along Road:
              </div>
              <input
                type="text"
                class="form-control"
                placeholder="Vehicle Used Along Road"
                v-model="selectedData.vroad"
                name="vroad"
                required
              />
            </div>

            <div class="input-group">
              <div class="input-group-text col-sm-3">District</div>
              <input
                type="text"
                class="form-control"
                placeholder="District"
                v-model="selectedData.district"
                name="district"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Weighbridge:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Weighbridge"
                v-model="selectedData.weighbridge"
                name="weighbridge"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">County:</div>
              <input
                type="text"
                class="form-control"
                placeholder="county"
                v-model="selectedData.county"
                name="county"
                required
              />
            </div>
          </b-tab>

          <b-tab>
            <template v-slot:title>
              <span class="d-inline-block d-sm-none">
                <i class="fas fa-cog"></i>
              </span>
              <span class="d-none d-sm-inline-block">Court Details</span>
            </template>

            <div class="input-group">
              <div class="input-group-text col-sm-3">Court:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Court"
                v-model="selectedData.court"
                name="court"
                required
              />
            </div>

            <div class="input-group">
              <div class="input-group-text col-sm-3">Complainant (Police):</div>
              <input
                type="text"
                class="form-control"
                placeholder="Complainant (Police)"
                v-model="selectedData.police"
                name="police"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">
                Police / Traffic Officer:
              </div>
              <!-- <input
                type="text"
                class="form-control"
                placeholder="Police / Traffic Officer"
                v-model="selectedData.tpolice"
                name="tpolice"
                required
              /> -->
              <multiselect
                v-model="selectedData.tpolice"
                :options="tpolices"
                class="form-control"
              ></multiselect>
            </div>
          </b-tab>

          <b-tab>
            <template v-slot:title>
              <span class="d-inline-block d-sm-none">
                <i class="fas fa-cog"></i>
              </span>
              <span class="d-none d-sm-inline-block">Prohibition Details</span>
            </template>

            <div class="input-group">
              <div class="input-group-text col-sm-3">Vehicle Inspector:</div>
              <!-- <input
                type="text"
                class="form-control"
                placeholder="Vehicle Inspector"
                v-model="selectedData.vi"
                name="vi"
              /> -->
              <multiselect
                v-model="selectedData.vi"
                :options="VIs"
                class="form-control"
              ></multiselect>
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Move Vehicle to:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Move Vehicle to"
                v-model="selectedData.moveto"
                name="moveto"
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Repairs Needed:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Repair's Needed"
                v-model="selectedData.rneeded"
                name="rneeded"
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">
                Offload Excess to Reg. No.:
              </div>
              <input
                type="text"
                class="form-control"
                placeholder="Offload Excess to Reg. No."
                v-model="selectedData.offload"
                name="offload"
              />
            </div>

            <div class="input-group">
              <div class="input-group-text col-sm-3">
                No. of Pay Station(s):
              </div>
              <input
                type="number"
                class="form-control"
                v-model="selectedData.paystation"
                name="offload"
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">
                Special Load permit No.:
              </div>
              <input
                type="text"
                class="form-control"
                v-model="selectedData.permitno"
                name="offload"
              />
            </div>
          </b-tab>

          <b-tab>
            <template v-slot:title>
              <span class="d-inline-block d-sm-none">
                <i class="fas fa-cog"></i>
              </span>
              <span class="d-none d-sm-inline-block"
                >Transporter's Details</span
              >
            </template>

            <div class="input-group">
              <div class="input-group-text col-sm-3">Transporter Names:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Transporter Names"
                v-model="selectedData.Tnames"
                name="Tnames"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Transporter Address:</div>
              <input
                type="text"
                class="form-control"
                placeholder="Transporter Address"
                v-model="selectedData.Taddress"
                name="Taddress"
                required
              />
            </div>
          </b-tab>

          <!-- <b-tab>
            <template v-slot:title>
              <span class="d-inline-block d-sm-none">
                <i class="fas fa-cog"></i>
              </span>
              <span class="d-none d-sm-inline-block">Dimensions Details</span>
            </template>

            <div class="input-group">
              <div class="input-group-text col-sm-3">Measured Height:</div>
              <input
                disabled
                type="number"
                class="form-control"
                placeholder="Transporter Names"
                v-model="selectedData.Tnames"
                name="Tnames"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Measured Width:</div>
              <input
                disabled
                type="number"
                class="form-control"
                placeholder="Transporter Address"
                v-model="selectedData.Taddress"
                name="Taddress"
                required
              />
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-3">Measured Length:</div>
              <input
                disabled
                type="number"
                class="form-control"
                placeholder="Transporter Address"
                v-model="selectedData.Taddress"
                name="Taddress"
                required
              />
            </div>
          </b-tab> -->
        </b-tabs>

        <form>
          <div class="row">
            <div class="col-lg-12">
              <div class="text-end">
                <b-button variant="dark" @click="prosecuteVehicle()"
                  >Save Case Details</b-button
                >
                <!--- <button type="submit" class="btn btn-primary" @click="prosecuteVehicle(data.index);">Submit</button>-->&nbsp;&nbsp;&nbsp;
              </div>
            </div>
          </div>
        </form>
        <div class="row"></div>
      </div>
    </b-modal>
    <b-modal
      id="modal-Prosecute"
      ref="dialog"
      title="Prosecute Vehicle"
      hide-footer
      size="xl"
      centered
    >
      <div class="row">
        <div class="col-md-12">
          <div class="card">
            <div class="card-body">
              <form @submit.prevent="handleSubmit">
                <div class="row">
                  <div class="input-group">
                    <div class="input-group-text col-sm-4">
                      Vehicle Registration:
                    </div>

                    <input
                      class="form-control"
                      type="text"
                      placeholder="Name"
                      v-model="nplate"
                    />
                    <button
                      type="submit"
                      class="btn btn-primary"
                      @click="showdetails()"
                    >
                      Show
                    </button>
                  </div>
                </div>
              </form>
              <div class="input-group-text col-sm-12" v-if="getdata">
                {{ getdata }}
              </div>
            </div>
          </div>
        </div>
      </div>

      <b-table
        table-class="table table-centered
                      datatable
                      dt-responsive
                      nowrap
                      table-card-list
                      dataTable
                      no-footer
                      dtr-inline"
        thead-tr-class="bg-transparent"
        :items="ticketsshow"
        :field="fields2"
        responsive="sm"
        :per-page="perPage2"
        :current-page="currentPage2"
        :sort-by.sync="sortBy2"
        :sort-desc.sync="sortDesc2"
        :filter="filter2"
        :filter-included-fields="filterOn2"
      >
      </b-table>
      <!----@click="position(), $bvModal.hide('modal-1') --->
      <div class="row">
        <div class="col-sm-9 mb-2"></div>
        <div class="col-sm-3 mb-2">
          <b-button v-show="editmode" variant="dark" @click="prosecut()"
            >Prosecute
          </b-button>
        </div>
      </div>
    </b-modal>

    <b-modal
      id="modal-Billing"
      ref="dialog"
      :title="BillingStitle"
      hide-footer
      size="lg"
      centered
    >
      <div class="col-sm-12">
        <div class="card">
          <div class="card-body changebg">
            <div class="card">
              <div class="card-body">
                <!-- Beggining of form -->
                <div class="col-sm-12 mb-1">
                  <div class="input-group">
                    <div class="input-group-text col-sm-5">
                      From: <strong>{{ froms }} </strong> To:
                    </div>
                    <multiselect
                      v-model="billingto"
                      :options="origins"
                      aria-disabled="true"
                      class="form-control"
                    ></multiselect>
                  </div>
                </div>

                <div class="col-sm-12 mb-1">
                  <div class="input-group-text col-sm-12 mb-1">
                    <div class="col-sm-1">Date :</div>
                    <div class="col-sm-2"></div>
                    <div class="col-sm-9">
                      <date-picker
                        disabled
                        v-model="cdate"
                        :first-day-of-week="1"
                        lang="en"
                      ></date-picker>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="card">
              <div class="card-body">
                Billing
                <div class="col-sm-12">
                  <div class="input-group">
                    <div class="input-group-text col-sm-12">
                      <input
                        type="checkbox"
                        id="Absconding"
                        v-model="Absconding"
                        value="Absconding"
                      />
                      Absconding WB:
                      <div
                        class="input-group-text col-sm-10"
                        v-show="Absconding"
                      >
                        Date of Absconding;
                        <date-picker
                          v-show="Absconding"
                          :value="new Date()"
                          v-model="abscondingdate"
                          :first-day-of-week="1"
                          lang="en"
                        ></date-picker>
                      </div>
                    </div>
                    <div class="input-group-text col-sm-12">
                      <input
                        type="checkbox"
                        id="Parking"
                        value="Parking"
                        v-model="Parking"
                      />Parking:
                      <div class="input-group-text col-sm-10" v-show="Parking">
                        Date From:
                        <date-picker
                          v-show="Parking"
                          :value="new Date()"
                          v-model="parkingfromdate"
                          :first-day-of-week="1"
                          lang="en"
                        ></date-picker>
                        To:
                        <date-picker
                          v-show="Parking"
                          :value="new Date()"
                          v-model="parkingtodate"
                          :first-day-of-week="1"
                          lang="en"
                        ></date-picker>
                      </div>
                    </div>

                    <div class="input-group-text col-sm-12">
                      <input
                        type="checkbox"
                        id="Overload"
                        value="Overload"
                        v-model="Overload"
                      />Overload: <strong>{{ overloadw }} [KG]</strong>
                    </div>
                    <div class="input-group-text col-sm-12">
                      <input
                        type="checkbox"
                        id="Others"
                        value="Others"
                        v-model="Others"
                      />Others:
                      <input
                        v-show="Others"
                        class="form-control"
                        type="text"
                        placeholder="details"
                        v-model="othersdetails"
                      />
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <div class="card">
              <div class="card-body">
                <div class="col-sm-12">
                  <div class="input-group">
                    <div class="input-group-text col-sm-8">
                      Approximate Amount Payable Kshs:
                    </div>
                    <input
                      class="form-control textright"
                      type="number"
                      v-model="amtpayable"
                    />
                  </div>
                </div>
              </div>
            </div>

            <div class="row">
              <div class="col-sm-8">&nbsp;</div>
              <div class="col-sm-4">
                <b-button variant="dark" @click="addbilling()"
                  >Add Billing Instruction >></b-button
                >
              </div>
            </div>
          </div>
        </div>
      </div>
    </b-modal>

    <b-modal
      id="modal-LoadC"
      ref="dialog"
      :title="LoadCtitle"
      hide-footer
      size="xl"
      centered
    >
      <div class="row col-xl-12">
        <div class="col-xl-6">
          <div class="card">
            <div class="card-body changebg">
              <!-- Beggining of form -->

              <div class="card">
                <div class="card-body">
                  <!-- Beggining of form -->

                  <div class="col-sm-12 mb-1">
                    <div class="input-group">
                      <button
                        type="button"
                        class="
                          btn btn-primary
                          bg-pill
                          col-sm-12
                          text-right
                          font-size-20
                        "
                      >
                        Total Ksh(s):
                        <span class="badge badge-light font-size-20"
                          >{{ finetot }}/=</span
                        >
                      </button>
                    </div>
                  </div>

                  <div class="col-sm-12 mb-1">
                    <div class="input-group">
                      <div class="input-group-text col-sm-8">
                        Enter Owner fine:
                      </div>
                      <input
                        @input="calc()"
                        class="form-control text-right"
                        type="number"
                        placeholder="0"
                        v-model="ownerfine"
                      />
                    </div>
                  </div>

                  <div class="col-sm-12 mb-1">
                    <div class="input-group">
                      <div class="input-group-text col-sm-8">
                        Enter Driver fine:
                      </div>
                      <input
                        @input="calc()"
                        class="form-control"
                        type="number"
                        placeholder="0"
                        v-model="driverfine"
                      />
                    </div>
                  </div>

                  <div class="col-sm-12 mb-1">
                    <div class="input-group">
                      <div class="input-group-text col-sm-8">
                        Enter Transporter fine:
                      </div>
                      <input
                        @input="calc()"
                        class="form-control"
                        type="number"
                        placeholder="0"
                        v-model="transporterfine"
                      />
                    </div>
                  </div>

                  <div class="col-sm-12 mb-1">
                    <div class="input-group">
                      <div class="input-group-text col-sm-8">
                        Enter Loader fine:
                      </div>
                      <input
                        @input="calc()"
                        class="form-control"
                        type="number"
                        placeholder="0"
                        v-model="loaderfine"
                      />
                    </div>
                  </div>

                  <div class="col-sm-12 mb-1">
                    <div class="input-group">
                      <div class="input-group-text col-sm-8">
                        Enter Parking fee :
                      </div>
                      <input
                        @input="calc()"
                        class="form-control"
                        type="number"
                        placeholder="0"
                        v-model="parkingfee"
                      />
                    </div>
                  </div>

                  <div class="col-sm-12 mb-1">
                    <div class="input-group">
                      <div class="input-group-text col-sm-6">
                        Enter Receipt/Mpesa number:
                      </div>
                      <input
                        class="form-control"
                        type="text"
                        placeholder=""
                        v-model="receiptno"
                      />
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>

        <div class="col-xl-6">
          <div class="card">
            <div class="card-body changebg">
              <div class="card">
                <div class="card-body">
                  <div class="col-sm-12 mb-1">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">Case basis:</div>
                      <multiselect
                        v-model="casebasis"
                        :options="casebasisopt"
                        aria-disabled="true"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-4">
                        Reason for <br />Case Discharge <br />or date set
                        <br />for postponed <br />case:
                      </div>
                      <textarea
                        class="form-control"
                        type="textarea"
                        placeholder=""
                        v-model="casedischarge"
                        rows="5"
                      ></textarea>
                    </div>
                  </div>

                  <!-- End of form -->
                </div>
              </div>

              <div class="row">
                <div class="col-sm-8">&nbsp;</div>
                <div class="col-sm-4">
                  <b-button variant="dark" @click="addresults()"
                    >Add >></b-button
                  >
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- Data -->

        <div class="col-xl-12">
          <div class="card">
            <div class="card-body changebg">
              <!-- Table -->

              <b-table
                table-class="table table-centered
                      datatable
                      dt-responsive
                      nowrap
                      table-card-list
                      dataTable
                      no-footer
                      dtr-inline textRights"
                :items="orderData3"
                :fields="fields3"
                responsive="sm"
                :per-page="perPage"
                :current-page="currentPage"
                :sort-by.sync="sortBy"
                :sort-desc.sync="sortDesc"
                :filter="filter"
                :filter-included-fields="filterOn"
              >
                <template v-slot:cell(screens)="data">
                  <a
                    href="javascript: void(0);"
                    class="text-dark fw-bold"
                    v-for="screen in data.item.screens"
                    :key="screen"
                    >{{ screen }} <br
                  /></a>
                </template>

                <template v-slot:cell(total)="data">
                  {{
                    Number(data.item.drivercharge) +
                    Number(data.item.ownercharge) +
                    Number(data.item.transportercharge) +
                    Number(data.item.loadercharge) +
                    Number(data.item.parkingfee)
                  }}
                </template>
                <template v-slot:cell(action)="data">
                  <ul class="list-inline mb-0">
                    <li class="list-inline-item">
                      <a
                        href="javascript:void(0);"
                        class="px-2 text-danger"
                        v-b-tooltip.hover
                        title="Delete"
                        @click="
                          deleteresult(
                            data.item.id,
                            data.item.casereid,
                            data.item
                          )
                        "
                      >
                        <i class="uil uil-trash-alt font-size-18"></i>
                      </a>
                    </li>
                  </ul>
                </template>
                <!----
                          <template v-slot:cell(total)="data">
                            <a href="#" class="text-body">{{
                              data.item.name
                            }}</a>
                          </template>
                            --->
                <template v-slot:cell(result)="data">
                  <div
                    class="badge bg-pill bg-soft-success font-size-12"
                    :class="{
                      'bg-soft-danger': data.item.result === 'Error',
                      'bg-soft-danger': data.item.result === 'Overload',
                      'bg-soft-success': data.item.result === 'Legal',
                      'bg-soft-secondary': data.item.result === 'Warning',
                    }"
                  >
                    {{ data.item.result }}
                  </div>
                </template>
              </b-table>
              <div class="row">
                <div class="col-sm-10 mb-1"></div>
                <div class="col-sm-2 mb-1">
                  <div class="input-group text-right">
                    <button
                      type="button"
                      class="
                        btn btn-success
                        bg-pill
                        col-sm-12
                        text-right
                        font-size-20
                      "
                      @click="finishloadc()"
                    >
                      Finish
                    </button>
                  </div>
                </div>
              </div>
              <div class="col-sm-12 mb-1">
                <div class="input-group text-right">
                  <button
                    type="button"
                    class="
                      btn btn-primary
                      bg-pill
                      col-sm-12
                      text-right
                      font-size-20
                    "
                  >
                    Total Ksh(s):
                    <span class="badge badge-light font-size-20"
                      >{{ tots }}/=</span
                    >
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </b-modal>
    <!-- Modal End -->
  </Layout>
</template>
<style scoped>
.changebg {
  background-color: rgb(240, 240, 240);
}
.textright {
  text-align: right;
}
</style>