<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import DatePicker from "vue2-datepicker";
import Multiselect from "vue-multiselect";
import axios from "axios";
import reportdet from "@/components/report/header";
import Swal from "sweetalert2";

export default {
  page: {
    title: "Wide Load Inspection",
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
    reportdet,
    Multiselect,
  },
  data() {
    return {
      title: "Wide Load",
      items: [
        {
          text: "USER: VI",
        },
        {
          text: "Wide Load Inspection",
          active: true,
        },
      ],
      registration: "",
      tareweight: 0,
      permitno: "",
      permitid: "",

      serialno: "",
      traillerreg: "",
      model: "",
      axlecount: 0,
      transptel: "",
      email: "",
      transp: "",
      transpadd: "",
      loaddisc: "",
      insecureload: "",
      lightstatus: "",
      sterringdef: "",
      fhBreak: "",
      aoDefects: "",
      abnormalLPermit: "",
      totaltyres: "",
      perconf: "",
      nonperconf: "",
      remarks: "",
      weighofload: "",
      height_: 0,
      width_: 0,
      length_: 0,
      authweight: 0,
      pEndD: "",
      pStartD: "",
      dpermitissu: "",
      startdateofjourney: "",
      drunkDriver: "",
      dateofTravel: "",
      editmode: false,
      showme: true,
      amountPaid: 0,
      pl: "",
      imae: require("@/assets/images/redbutton.jpg"),
      imae2: require("@/assets/images/button.png"),
      image: ["@/assets/images/redbutton.jpg", "@/assets/images/button.png"],
      headers: null,
      uniqueCars: null,
      autoClose: "",
      shift: null,
      route: null,
      hour: null,
      dlicensestate: null,
      inspstick: null,
      insuaranceStic: null,
      status: "",
      Defects: ["None", "With Defects"],
      fromdate: "",

      shifts: ["Shift A", "Shift B", "Shift C"],
      routes: ["Nairobi-Mombasa", "Mombasa-Nairobi"],
      transporters: ["Micna Solutions", "Masterspace"],
      yesno: ["YES", "NO"],
      valid: ["VALID", "INVALID"],
      axles: [
        "2A",
        "3A",
        "4A",
        "5A",
        "5B",
        "5C",
        "5D",
        "6A",
        "6B",
        "6C",
        "6G",
        "6I",
        "7A",
        "7B",
        "7C ",
      ],
      stats: ["OK", "NOT OK"],
      towns: [
        "Nairobi",
        "Mombasa",
        "Kisumu",
        "Nakuru",
        "Ruiru",
        "Eldoret",
        "Kikuyu",
        "Thika",
        "Meru",
        "Kangundo-Tala",
        "Malindi",
        "Naivasha",
        "Kitui",
        "Machakos",
        "Mlolongo",
        "Athi River (Mavoko)",
        "Karuri",
        "Nyeri",
        "Kilifi",
        "Garissa",
        "Vihiga",
        "Wote",
        "Mumias",
        "Bomet",
        "Molo",
        "Ngong",
        "Kitale",
        "Litein",
        "Limuru",
        "Kericho",
        "Kimilili",
        "Awasi",
        "Kakamega",
        "Kapsabet",
        "Mariakani",
        "Kiambu",
        "Mandera",
        "Nyamira",
        "Mwingi",
        "Kisii",
        "Wajir",
        "Rongo",
        "Bungoma",
        "Ahero",
        "Nandi Hills",
        "Makuyu",
        "Kapenguria",
        "Taveta",
        "Narok",
        "Ol Kalou",
        "Kakuma",
        "Webuye",
        "Malaba",
        "Mbita Point",
        "Ukunda",
        "Wundanyi",
        "Busia",
        "Runyenjes",
        "Migori",
        "Malava",
        "Suneka",
        "Embu",
        "Ogembo",
        "Homa Bay",
        "Lodwar",
        "Kitengela",
        "Ukwala",
        "Keroka",
        "Matuu",
        "Oyugis",
        "Nyahururu",
        "Kipkelion",
        "Luanda",
        "Nanyuki",
        "Maua",
        "Mtwapa",
        "Isiolo",
        "Eldama Ravine",
        "Voi",
        "Siaya",
        "Nyansiongo",
        "Londiani",
        "Iten/Tambach",
        "Chuka",
        "Malakisi",
        "Juja",
        "Ongata Rongai",
        "Bondo",
        "Moyale",
        "Maralal",
        "Gilgil",
        "Nambale",
        "Tabaka",
        "Muhoroni",
        "Kerugoya/Kutus",
        "Ugunja",
        "Yala",
        "Rumuruti",
        "Burnt Forest",
        "Maragua",
        "Kendu Bay",
        "Ruai",
        "Kamulu",
      ],
      Models: ["Mercedes Benz", "FAW", "Toyota"],
      fireext: null,
      firstAkit: null,
      axleconf: null,
      origin: null,
      destination: null,

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
      modaltitle: "Add Wide Load Permit",
      orderData: [
        // {
        //   id: "1",
        //   permitid: "99",
        //   permitno: "app-0094194",
        //   registration: "KDA157R",
        //   traillerreg: "ZC2416",
        //   transp: "AWALE ENTERPRISES",
        //   transptel: "0724649538",
        //   transpadd: "931225-80102",
        //   model: "SCANIA",
        //   origin: "juba",
        //   destination: "mombasa",
        //   axleconf: "6C",
        //   firstAkit: "YES",
        //   fireext: "YES",
        //   drunkDriver: "NO",
        //   dlicensestate: "VALID",
        //   inspstick: "VALID",
        //   insuaranceStic: "VALID",
        //   loaddisc: "2 APC UNMISS VEHICLES ",
        //   dpermitissu: "2021-08-06 00:00:00",
        //   pStartD: "2021-08-06 00:00:00",
        //   pEndD: "2021-11-06 00:00:00",
        //   insecureload: "NO",
        //   lightstatus: "OK",
        //   sterringdef: "None",
        //   fhBreak: "None",
        //   aoDefects: "None",
        //   height_: 5.3,
        //   length_: 17,
        //   width_: 3.5,
        //   abnormalLPermit: "VALID",
        //   totaltyres: 22,
        //   perconf: 87,
        //   nonperconf: 13,
        //   remarks: "None",
        //   weighofload: 28000,
        //   authweight: 50000,
        //   dateofTravel: "2021-08-08 00:00:00",
        //   amountPaid: 0,
        //   status: "Legal",
        //   permitdatetime: "2021-08-08 00:00:00",
        // },
      ],
      origin_name: "",
      transporter_name: "",
      transporter_address: "",
      transporter_contact: "",
      info: "",
      totalRows: 1,
      currentPage: 1,
      viewWideLoad: false,
      printReport: false,
      clickedPrintpdf: "",
      filteredValues: "",
      printedpdf: true,
      generatedExel: "",
      addWideLoad: false,
      editWideLoad: false,
      deleteWideLoad: false,
      perPage: 10,
      pageOptions: [1, 10, 25, 50, 100],
      filter: null,
      filterOn: [],
      sortBy: "id",
      sortDesc: true,
      fields: [
        {
          key: "id",
          label: "#",
          sortable: true,
        },
        {
          key: "index",
          label: "No.",
          sortable: true,
        },
        {
          key: "permitno",
          label: "Permit No",
          sortable: true,
        },
        {
          key: "registration",
          label: "Plate No.",
          sortable: true,
        },
        {
          key: "traillerreg",
          label: "Trailer No",
          sortable: true,
        },
        {
          key: "transp",
          label: "Transporter",
          sortable: true,
        },
        {
          key: "transptel",
          label: "Transporter Tel",
          sortable: true,
        },
        {
          key: "model",
          label: "Model",
          sortable: true,
        },
        {
          key: "origin",
          label: "Origin",
          sortable: true,
        },
        {
          key: "destination",
          label: "Destination",
          sortable: true,
        },
        {
          key: "axleconf",
          label: "Axle",
          sortable: true,
        },
        {
          key: "permitdatetime",
          label: "Date Time",
          sortable: true,
        },
        {
          key: "dateofTravel",
          label: "Date of Travel",
          sortable: true,
        },
        {
          key: "pStartD",
          label: "Period Start Date",
          sortable: true,
        },
        {
          key: "pEndD",
          label: "Period End  Date",
          sortable: true,
        },
        {
          key: "status",
          label: "Status",
          sortable: true,
        },
        {
          key: "weighofload",
          label: "Weight of Load ",
          sortable: true,
        },
        "Dimensions",
        "action",
      ],
    };
  },
  watch: {
    viewWideLoad(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Opened wideload list:${newValue}`,
        computer: window.localStorage.clientip,
      };
      axios.post(window.$http + "AuditLogs", data, {
        headers: window.$headers,
      });
    },
    printReport() {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Wideload reports:${this.clickedPrintpdf},${this.generatedExel},${this.filteredValues}`,
        computer: window.localStorage.clientip,
      };
      axios.post(window.$http + "AuditLogs", data, {
        headers: window.$headers,
      });
    },

    addWideLoad(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Added WideLoad permit:${newValue}`,
        computer: window.localStorage.clientip,
      };
      axios.post(window.$http + "AuditLogs", data, {
        headers: window.$headers,
      });
    },
    editWideLoad(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Edited WideLoad permit:${newValue}`,
        computer: window.localStorage.clientip,
      };
      axios.post(window.$http + "AuditLogs", data, {
        headers: window.$headers,
      });
    },
    deleteWideLoad(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Deleted WideLoad record:${newValue},id:${this.id}`,
        computer: window.localStorage.clientip,
      };
      axios.post(window.$http + "AuditLogs", data, {
        headers: window.$headers,
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
    this.imae = require("@/assets/images/redbutton.jpg");
    this.imae2 = require("@/assets/images/button.png");
    this.image = [this.imae, this.imae2];
    this.totalRows = this.items.length;
    this.viewWideLoad = true;
    this.fromdate = new Date();
    this.updatearray();
    this.gettransporters();
    this.getorigin();
    this.getvehiclemake();
    this.getaxles();
    axios
      .get(window.$http + `permiturl`, {
        headers: window.$headers,
      })
      .then((response) => {
        let orderData = response.data;
        //alert(orderData[0].permiturl);
        window.$permiturl = orderData[0].permiturl;
      })
      .catch(() => {
        //alert(e);
      });
  },
  methods: {
    getimg(str) {
      if (str == "L") {
        return this.imae2;
      }
      return this.imae;
    },
    addOrigin() {
      if (this.origin_name.trim() == "") {
        Swal.fire("Please enter  Origin / Destination !");
        return;
      }

      const data = {
        origin_name: this.origin_name.toUpperCase(),
        active: 0,
      };
      axios
        .post(window.$http + `Origins/`, data, { headers: window.$headers })
        .then(() => {
          // JSON responses are automatically parsed.
          //alert(this.origin_name);
          //this.origin = this.origin_name.toUpperCase();
          this.origin = this.origin_name.toUpperCase();

          Swal.fire({
            position: "center",
            icon: "success",
            title: "Your work has been saved",
            showConfirmButton: false,
            timer: 1500,
          }).then(() => {
            this.$root.$emit("bv::hide::modal", "modal-AddOrigin");
            this.getorigin();
          });
        })
        .catch((e) => {
          Swal.fire("" + e);
        });
    },
    updatearray() {
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      var fromdate = "";
      try {
        fromdate = this.fromdate;
      } catch (e) {
        fromdate = "";
      }

      if (fromdate <= 0) {
        return;
      }

      fromdate = this.getmydate(this.fromdate) + " 00:00:00 AM";
      var tdate = this.getmydate(this.fromdate) + " 23:59:59 PM";
      //alert(
      //   window.$http +
      //     `wideload/search?limit=1000&deletetatus=0&fromdate=` +
      //     fromdate +
      //     "&todate=" +
      //     tdate
      // );
      axios
        .get(
          window.$http +
            `wideload/search?limit=1000&deletetatus=0&fromdate=` +
            fromdate +
            "&todate=" +
            tdate,
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData = response.data;
          //console.log(this.orderData);

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
    getaxles() {
      this.axles = [];
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
    printpdf(pl, p_pdf) {
      this.printReport = true;
      this.pl = pl;
      p_pdf = "Clicked print pdf button";
      this.clickedPrintpdf = p_pdf;
      //alert(this.showme);
      const data = this.orderData.map((row) => ({
        id: row.id,
        permitid: row.permitid,
        permitno: row.permitno,
        registration: row.registration,
        traillerreg: row.traillerreg,
        transp: row.transp,
        transptel: row.transptel,
        transpadd: row.transpadd,
        model: row.model,
        origin: row.origin,
        destination: row.destination,
        axleconf: row.axleconf,
        firstAkit: row.firstAkit,
        fireext: row.fireext,
        drunkDriver: row.drunkDriver,
        dlicensestate: row.dlicensestate,
        inspstick: row.inspstick,
        insuaranceStic: row.insuaranceStic,
        loaddisc: row.loaddisc,

        dpermitissu: row.dpermitissu,
        pStartD: row.pStartD,
        pEndD: row.pEndD2,
        insecureload: row.insecureload,
        lightstatus: row.lightstatus,
        sterringdef: row.sterringdef,
        fhBreak: row.fhBreak,
        aoDefects: row.aoDefects,
        height_: row.height_,
        length_: row.length_,
        width_: row.width_,

        abnormalLPermit: row.abnormalLPermit,
        totaltyres: row.totaltyres,
        perconf: row.perconf,
        nonperconf: row.nonperconf,

        remarks: row.remarks,
        weighofload: row.weighofload,
        authweight: row.authweight,
        dateofTravel: row.dateofTravel,
        status: row.state,
        permitdatetime: row.permitdatetime,
      }));

      //get headers
      const headers = Object.keys(data[0]);
      const cars = [];
      Object.entries(data).forEach((val) => {
        const [key] = val;
        //console.log(key, value);
        cars.push(Object.values(data[key]));
      });

      const uniqueCars = Array.from(new Set(cars));
      this.headers = headers;
      this.uniqueCars = uniqueCars;
      //alert(headers);
    },
    /**
     * Search the table data with search input
     */
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
      this.printReport = true;
      this.filteredValues = "Searched for '" + this.filter + "'";
    },
    handleSubmit() {
      //console.log("Error on submit");
    },
    getconf() {
      if (this.nonperconf > 100) {
        this.$refs.tyrenoncon.focus();
        Swal.fire(this.nonperconf + "! \nPlease enter number less than 100!");
        this.nonperconf = 0;
        return;
      }
      this.perconf = Math.abs(this.nonperconf - 100);
    },
    getconf2() {
      if (this.perconf > 100) {
        this.$refs.tyrecon.focus();
        Swal.fire(this.perconf + "! \nPlease enter number less than 100!");
        this.perconf = 0;
        return;
      }
      this.nonperconf = Math.abs(this.perconf - 100);
    },
    getdaysdiff(date1, date2) {
      //Swal.fire("Date:" + date1 + "\ndate2" + date2);
      // The number of milliseconds in one day
      const ONE_DAY = 1000 * 60 * 60 * 24;

      // Calculate the difference in milliseconds
      const differenceMs = date2 - date1;

      // Convert back to days and return
      return Math.round(differenceMs / ONE_DAY);
    },
    getpermit() {
      //alert(
      //   "https://permits.kenha.co.ke:7080/ApplicantForms/GeneratePermit/App-0009617"
      // );

      // const logFileText = async (file) => {
      //   const response = await fetch(file);
      //   this.info = await response.text();
      // };
      // var theUrl =
      //   "http://localhost:8080/ApplicantForms/GeneratePermit/App-0009617";

      // logFileText(theUrl);
      // ref = "https://permits.kenha.co.ke:7080";
      // this.info = "";

      // var href = window.location.href;

      // var arr = href.split("/");
      // var ref = "";
      // ref = arr[0] + "//" + arr[2];
      // var ref2 = arr[2].split(":");
      // ref = arr[0] + "//" + ref2[0] + "";
      // var url = ref + "/ApplicantForms/GeneratePermit/" + this.permitno.trim();

      // var url = window.$permiturl + this.permitno.trim();
      //alert(url);

      //url = "https://kenloadv2.netlify.app";
      // = "http://localhost";

      // const logFileText = async (file) => {
      //   const response = await fetch(file);
      //   let text = await response.text();
      //   this.info = text;
      // };
      // //var file = "file:///C:/xampp/htdocs/indicators/" + this.weighb + ".txt";
      // logFileText(url);
      // window.$permiturl =
      //   "https://permits.kenha.co.ke:7080/ApplicantForms/GeneratePermit/";

      // url =
      //   window.$url +
      //   "/kenload/dashboard/getpermit.php?string=" +
      //   window.$permiturl +
      //   this.permitno.trim();

      //alert(url);

      this.firstAkit = "YES";
      this.fireext = "YES";
      this.drunkDriver = "NO";
      this.dlicensestate = "VALID";
      this.insuaranceStic = "VALID";
      this.inspstick = "VALID";
      this.insecureload = "NO";
      this.lightstatus = "OK";
      this.sterringdef = "None";
      this.fhBreak = "None";
      this.aoDefects = "None";
      this.abnormalLPermit = "VALID";
      this.perconf = 87;
      this.nonperconf = 13;
      this.dateofTravel = new Date();
      this.remarks = "None";
      this.amountPaid = 0;
      this.startdateofjourney = new Date();
      this.dpermitissu = new Date();
      this.pEndD = new Date();
      // if (this.amountPaid == 0) {
      //   return;
      // }

      let error = "";
      //baseURL: url, timeout: 10
      //alert(url);

      var url2 =
        window.$permiturl +
        "/permits/get-items?id=" +
        this.permitno.trim() +
        "&uid=kenload&pwd=test";
      //alert(url2);
      axios
        .get(url2) //, { timeout: 200000 }
        .then((response) => {
          //console.log(response);
          this.width_ = response.data;
          var strl = JSON.parse(JSON.stringify(response.data));
          //console.log("strl");
          //console.log(strl);
          //alert(strl.data[0].Weight.trim());
          //Width:
          try {
            this.width_ = strl.data[0].Width.trim();
          } catch (Exception) {
            this.width_ = "";
          }

          //Height:
          try {
            this.height_ = strl.data[0].Height.trim();
          } catch (Exception) {
            this.height_ = "";
          }

          //Length:
          try {
            this.length_ = strl.data[0].Length.trim();
          } catch (Exception) {
            this.length_ = "";
          }

          //product:
          try {
            this.loaddisc = strl.data[0].LoadDescription.trim();
          } catch (Exception) {
            this.loaddisc = "";
          }

          //authweight:

          try {
            // this.authweight = Number(strl.data[0].Weight.trim());
            this.weighofload = Number(strl.data[0].Weight.trim());
          } catch (Exception) {
            this.weighofload = 0;
          }
          try {
            this.authweight += Number(this.weighofload);
            //this.weighofload = strl.data.TotalTareWeightofvehiclekgs.trim();
          } catch (Exception) {
            this.authweight = 0;
          }
        })
        .catch((err) => {
          //alert(err);
          if (err.response.status === 404) {
            Swal.fire(
              "Permit Scan Error 400!  \nError Connecting to server :" +
                this.url +
                " \n" +
                err
            );
            //throw new Error(`${err.config.url} not found`);
          }
          if (err.response.status === 500) {
            Swal.fire(
              "Timeout Error 500! \n Connecting to server :" +
                this.url +
                "\n" +
                err
            );
            //throw new Error(`${err.config.url} not found`);
          }
          //throw err;
        });

      var url =
        window.$permiturl +
        "/permits/get-permit?option=1&id=" +
        this.permitno.trim() +
        "&uid=kenload&pwd=test";
      try {
        axios
          .get(url) //, { timeout: 200000 }
          .then((response) => {
            //axios.get({ baseURL: url, timeout: 1000 }).then((response) => {
            this.info = response.data;

            // //alert(this.dpermitissu);

            // var target = "VEHICLE REGI";
            // //Permit
            // target = "Permit No:";
            // ddp = response.data.substring(
            //   response.data.indexOf(target) + target.length,
            //   response.data.indexOf(target) + target.length + 50
            // );
            // target = "</address>";
            //console.log(response.status);

            var strl = JSON.parse(JSON.stringify(response.data));
            this.serialno = strl.data.PermitNo.trim();
            // //console.log(strl);
            //alert(strl.status);
            //alert(strl.data.PermitNo);

            if (this.serialno.toUpperCase() != this.permitno.toUpperCase()) {
              Swal.fire("Invalid Permit Number " + this.serialno);
              return;
            }
            //END OF JOURNEY DATE
            var dtrs = strl.data.ToDate.split(" ");
            var ddatesp = dtrs[0];
            //alert(ddatesp);

            this.pEndD = new Date(ddatesp);

            //alert(this.pEndD);
            var pEndD = this.pEndD;
            pEndD.setDate(this.pEndD.getDate() + 1);
            var daydidff = this.getdaysdiff(new Date(), pEndD);
            //alert(daydidff);

            if (daydidff < 0) {
              Swal.fire("Permit Expired on " + this.pEndD);
              return;
            }
            //vehicle Registration

            var regf = strl.data.VehicleRegistrationNumber.trim();
            //alert(regf);

            const indexk = [];
            indexk[0] = "-K";
            indexk[1] = "- K";
            indexk[2] = " K";
            //alert(indexk.length);
            for (var i = 0; indexk.length > i; i++) {
              if (regf.includes(indexk[i])) {
                regf = regf.substring(regf.indexOf(indexk[i]) + 1, regf.length);
              }
            }
            if (regf.indexOf("-") > 0) {
              regf = regf.substring(0, regf.indexOf("-"));
              //alert("regf" + regf);
            }

            regf = regf
              .toUpperCase()
              .replaceAll("\\s", "")
              .replaceAll("[^a-zA-Z0-9]", "")
              .replaceAll(" ", "");

            this.registration = regf;

            //Tare Weight
            try {
              this.tareweight = strl.data.TotalTareWeightofvehiclekgs.trim();
            } catch (Exception) {
              this.tareweight = 0.1;
            }
            //alert(this.tareweight);
            //Trailer Registration

            try {
              this.traillerreg =
                strl.data.TrailerRegistrationNumber.trim().toUpperCase();
            } catch (Exception) {
              this.traillerreg = "";
            }

            //alert(this.traillerreg);

            //Start of Journey

            try {
              dtrs = strl.data.FromDate.split(" ");
            } catch (Exception) {
              dtrs = new Date();
            }

            ddatesp = dtrs[0];
            //alert(this.pEndD);
            this.dpermitissu = new Date(ddatesp);

            this.pStartD = this.dpermitissu;
            //alert(this.pStartD);
            //TYPE OF VEHICLE:
            try {
              this.model = strl.data.MakeName.trim().toUpperCase();
            } catch (Exception) {
              this.model = "";
            }

            //TOTAL NUMBER OF AXELS:

            var totaltyres = "0";
            try {
              totaltyres = strl.data.Noofwheelsperaxle.trim();
            } catch (Exception) {
              totaltyres = "0";
            }
            //alert(totaltyres);

            var wheelaxle = totaltyres.split(":");
            this.axlecount = wheelaxle.length;
            //this.axleconf = wheelaxle.length;
            //alert(this.axlecount);
            let axl = 0;
            for (var k = 0; wheelaxle.length > k; k++) {
              axl += Number(wheelaxle[k]);
            }
            this.totaltyres = axl;
            //Phone:
            try {
              this.transptel = strl.data.TelephoneNumber.trim();
            } catch (Exception) {
              this.transptel = "";
            }

            //Email:
            try {
              this.email = strl.data.EmailAddress.trim();
            } catch (Exception) {
              this.email = "";
            }

            //Transpoter:
            try {
              this.transp = strl.data.Name.trim();
            } catch (Exception) {
              this.transp = "";
            }

            //Address
            try {
              this.transpadd = strl.data.PostalAddress.trim();
            } catch (Exception) {
              this.transpadd = "";
            }

            //Weight:

            //authweight;
            try {
              this.authweight = Number(
                strl.data.TotalTareWeightofvehiclekgs.trim()
              );

              //this.weighofload = strl.data.TotalTareWeightofvehiclekgs.trim();
            } catch (Exception) {
              this.authweight = 0;
            }
          })
          .catch((err) => {
            //alert(err);
            if (err.response.status === 404) {
              Swal.fire(
                "Permit Scan Error 400!  \nError Connecting to server :" +
                  this.url +
                  " \n" +
                  err
              );
              //throw new Error(`${err.config.url} not found`);
            }
            if (err.response.status === 500) {
              Swal.fire(
                "Timeout Error 500! \n Connecting to server :" +
                  this.url +
                  "\n" +
                  err
              );
              //throw new Error(`${err.config.url} not found`);
            }
            //throw err;
          });
      } catch (e) {
        Swal.fire("Permit Scan Error! Page not found\n" + e);
      }

      error.message;
    },

    edit(
      index,
      myid,
      permitid,
      permitno,
      registration,
      traillerreg,
      transp,
      transptel,
      transpadd,
      model,
      origin,
      destination,
      axleconf,
      firstAkit,
      fireext,
      drunkDriver,
      dlicensestate,
      inspstick,
      insuaranceStic,
      loaddisc,

      dpermitissu,
      pStartD,
      pEndD,
      insecureload,
      lightstatus,
      sterringdef,
      fhBreak,
      aoDefects,

      height_,
      length_,
      width_,

      abnormalLPermit,
      totaltyres,
      perconf,
      nonperconf,

      remarks,
      weighofload,
      authweight,
      dateofTravel,
      amountPaid,
      status,
      permitdatetime
    ) {
      //alert(rolename);
      this.editmode = true;
      this.modaltitle = "Edit Wide Load Permit(s)";
      this.id = myid;
      this.permitid = permitid;
      this.myindex = index;
      this.permitno = permitno;
      this.registration = registration;
      this.traillerreg = traillerreg;
      this.transp = transp;
      this.transptel = transptel;
      this.transpadd = transpadd;
      this.model = model;
      this.origin = origin;
      this.destination = destination;
      this.axleconf = axleconf;
      this.firstAkit = firstAkit;
      this.fireext = fireext;
      this.drunkDriver = drunkDriver;
      this.dlicensestate = dlicensestate;
      this.inspstick = inspstick;
      this.insuaranceStic = insuaranceStic;
      this.loaddisc = loaddisc;

      this.dpermitissu = new Date(dpermitissu);
      this.pStartD = new Date(pStartD);
      this.pEndD = new Date(pEndD);
      this.insecureload = insecureload;
      this.lightstatus = lightstatus;
      this.sterringdef = sterringdef;
      this.fhBreak = fhBreak;
      this.aoDefects = aoDefects;
      this.height_ = height_;
      this.length_ = length_;
      this.width_ = width_;

      this.abnormalLPermit = abnormalLPermit;
      this.totaltyres = totaltyres;
      this.perconf = perconf;
      this.nonperconf = nonperconf;

      this.remarks = remarks;
      this.weighofload = weighofload;
      this.authweight = authweight;
      this.dateofTravel = new Date(dateofTravel);
      this.status = "Charged";
      if (status == "L") {
        this.status = "Legal";
      }
      this.amountPaid = amountPaid;
      this.permitdatetime = permitdatetime;
      //this.focusInput();

      //this.$refs.search.focus();
      //$("#search").focus();
    },
    getv(val) {
      if (val < 10) {
        val = "0" + val;
      }
      return val;
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
    getconvertdate(mydate) {
      let d = new Date(mydate);
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
    validate() {
      let val = false;
      this.traillerreg = this.traillerreg
        .toUpperCase()
        .replaceAll("\\s", "")
        .replaceAll("[^a-zA-Z0-9]", "")
        .replaceAll(" ", "")
        .trim();
      this.registration = this.registration
        .toUpperCase()
        .replaceAll("\\s", "")
        .replaceAll("[^a-zA-Z0-9]", "")
        .replaceAll(" ", "")
        .trim();

      if (this.registration.trim() == "") {
        Swal.fire("Please enter registration!");
        return val;
      }
      if (this.traillerreg.trim() == "") {
        Swal.fire("Please enter trailer Number!");
        return val;
      }
      if (this.axleconf.trim() == "") {
        Swal.fire("Please enter Axle Configuration!");
        return val;
      }
      if (Number(this.totaltyres) <= 0) {
        Swal.fire("Please enter Total Tyres !");
        return val;
      }
      if (
        Number(this.height_) <= 0 ||
        Number(this.width_) <= 0 ||
        Number(this.length_) <= 0
      ) {
        Swal.fire("Please enter heigh or width or length !");
        return val;
      }
      if (this.status.trim() == "-Choose One-") {
        Swal.fire("Please select Status !");
        return val;
      }

      val = true;
      return val;
    },
    editrec() {
      //alert();
      let val = this.validate();
      //alert();
      if (val == false) {
        return;
      }
      var state = "C";
      if (this.status == "Legal") {
        state = "L";
      }
      //2021-08-16T09:27:13
      //alert(this.id);

      Swal.fire({
        title: "Are you sure, you want to edit ?",
        text: "" + this.permitno,
        icon: "info",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, Edit it!",
      }).then((result) => {
        if (result.value) {
          Swal.fire({
            title: "Kenload V2. Please Wait!",
            html: "Editing data...", // add html attribute if you want or remove
            allowOutsideClick: false,
            showConfirmButton: false,
            willOpen: () => {
              Swal.showLoading();
            },
          });

          var dpermitissu2 = this.getconvertdate(new Date(this.dpermitissu));
          //alert(dpermitissu2);
          var pStartD2 = this.getconvertdate(new Date(this.pStartD));
          var pEndD2 = this.getconvertdate(new Date(this.pEndD));
          var permitdatetime2 = this.getconvertdate(
            new Date(this.permitdatetime)
          );

          const data = {
            id: this.id,
            permitid: this.permitid,
            permitno: this.permitno,
            registration: this.registration,
            traillerreg: this.traillerreg,
            transp: this.transp,
            transptel: this.transptel,
            transpadd: this.transpadd,
            model: this.model,
            origin: this.origin,
            destination: this.destination,
            axleconf: this.axleconf,
            firstAkit: this.firstAkit,
            fireext: this.fireext,
            drunkDriver: this.drunkDriver,
            dlicensestate: this.dlicensestate,
            inspstick: this.inspstick,
            insuaranceStic: this.insuaranceStic,
            loaddisc: this.loaddisc,

            dpermitissu: dpermitissu2,
            pStartD: pStartD2,
            pEndD: pEndD2,
            insecureload: this.insecureload,
            lightstatus: this.lightstatus,
            sterringdef: this.sterringdef,
            fhBreak: this.fhBreak,
            aoDefects: this.aoDefects,
            height_: this.height_,
            length_: this.length_,
            width_: this.width_,

            abnormalLPermit: this.abnormalLPermit,
            totaltyres: this.totaltyres,
            perconf: this.perconf,
            nonperconf: this.nonperconf,

            remarks: this.remarks,
            weighofload: this.weighofload,
            authweight: this.authweight,
            dateofTravel: this.dateofTravel,
            status: state,
            permitdatetime: permitdatetime2,
          };
          //console.log(data);
          //alert(this.screenname);

          axios
            .put(window.$http + `wideload/` + this.id, data, {
              headers: window.$headers,
            })
            .then(() => {
              // JSON responses are automatically parsed.
              this.orderData[this.myindex].id = this.id;
              this.orderData[this.myindex].permitno = this.permitno;
              this.orderData[this.myindex].registration = this.registration;
              this.orderData[this.myindex].traillerreg = this.traillerreg;
              this.orderData[this.myindex].transporter = this.transporter;
              this.orderData[this.myindex].transptel = this.transptel;
              this.orderData[this.myindex].model = this.model;
              this.orderData[this.myindex].origin = this.origin;
              this.orderData[this.myindex].destination = this.destination;
              this.orderData[this.myindex].axleconf = this.axleconf;
              this.orderData[this.myindex].permitdatetime = this.permitdatetime;

              this.orderData[this.myindex].status = this.status;
              this.orderData[this.myindex].authweight = this.authweight;
              this.orderData[this.myindex].length_ = this.length_;
              this.orderData[this.myindex].width_ = this.width_;
              this.orderData[this.myindex].height_ = this.height_;
              this.editWideLoad = true;

              Swal.close();
              Swal.fire(
                " Edited!",
                "Your record has been edited.",
                "success"
              ).then(() => {
                //this.clearvalues();
                this.updatearray();
              });
            })
            .catch((e) => {
              Swal.fire({
                position: "center",
                icon: "error",
                title: "" + e,
                showConfirmButton: true,
              }).then(() => {
                Swal.close();
              });
            });
        }
      });
    },
    getrpt(generated_Exel) {
      this.printReport = true;
      generated_Exel = "Generated Wideload Excel Document";
      this.generatedExel = generated_Exel;
      //alert(new Date());
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
      //alert(filename);
      const data = this.orderData.map((row) => ({
        id: row.id,
        permitid: row.permitid,
        permitno: row.permitno,
        registration: row.registration,
        traillerreg: row.traillerreg,
        transp: row.transp,
        transptel: row.transptel,
        transpadd: row.transpadd,
        model: row.model,
        origin: row.origin,
        destination: row.destination,
        axleconf: row.axleconf,
        firstAkit: row.firstAkit,
        fireext: row.fireext,
        drunkDriver: row.drunkDriver,
        dlicensestate: row.dlicensestate,
        inspstick: row.inspstick,
        insuaranceStic: row.insuaranceStic,
        loaddisc: row.loaddisc,

        dpermitissu: row.dpermitissu,
        pStartD: row.pStartD,
        pEndD: row.pEndD2,
        insecureload: row.insecureload,
        lightstatus: row.lightstatus,
        sterringdef: row.sterringdef,
        fhBreak: row.fhBreak,
        aoDefects: row.aoDefects,
        height_: row.height_,
        length_: row.length_,
        width_: row.width_,

        abnormalLPermit: row.abnormalLPermit,
        totaltyres: row.totaltyres,
        perconf: row.perconf,
        nonperconf: row.nonperconf,

        remarks: row.remarks,
        weighofload: row.weighofload,
        authweight: row.authweight,
        dateofTravel: row.dateofTravel,
        status: row.state,
        permitdatetime: row.permitdatetime,
      }));
      //alert("");
      const csvRows = [];
      //get headers
      const headers = Object.keys(data[0]);

      csvRows.push(headers.join(","));
      //alert(csvRows);
      //loop over the headers
      for (const row of data) {
        const values = headers.map((header) => {
          const escaped = ("" + row[header]).replace(/"/g, '\\"');
          //alert(escaped);
          return '"' + escaped + '"'; //'" + escaped + "';
        });
        csvRows.push(values.join(","));
      }
      //alert(csvData);
      const csvData = csvRows.join("\n");
      //alert(csvData);

      const blob = new Blob([csvData], { type: "textcsv" });
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement("a");
      a.setAttribute("hidden", "");
      a.setAttribute("href", url);
      a.setAttribute("download", this.title + filename + ".csv");
      this.generatedExel = "Generated Excel document";
      document.body.appendChild(a);
      a.click();
      document.body.removeChild(a);
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
    gettransporters() {
      this.transporters = [];
      axios
        .get(window.$http + `Transporters/search?limit=100000`, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          // this.cargos = response.data.cargo_name;
          response.data.forEach((e) => {
            this.transporters.push(e.transporter_name.toUpperCase());
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
    },
    addTransporter() {
      if (this.transporter_name.trim() == "") {
        Swal.fire("Please enter  Transporters Name!");
        return;
      }
      if (this.transporter_address.trim() == "") {
        Swal.fire("Please enter  Transporters Address!");
        return;
      }
      if (this.transporter_contact.trim() == "") {
        Swal.fire("Please enter  Transporters Contact!");
        return;
      }

      const data = {
        transporter_name: this.transporter_name,
        transporter_address: this.transporter_address,
        transporter_contact: this.transporter_contact,
        active: 0,
      };
      axios
        .post(window.$http + `Transporters/`, data, {
          headers: window.$headers,
        })
        .then(() => {
          // JSON responses are automatically parsed.
          this.transp = this.transporter_name;
          this.gettransporters();
          this.$root.$emit("bv::hide::modal", "modal-AddTransporter");
          //console.log(response.data);
          this.addtransporter = true;
          Swal.fire({
            position: "center",
            icon: "success",
            title: "Your work has been saved",
            showConfirmButton: false,
            timer: 1500,
          });
        })
        .catch((e) => {
          Swal.fire("" + e);
        });
    },
    savepermit() {
      var state = "C";
      if (this.status == "Legal") {
        state = "L";
      }

      var dpermitissu2 = this.getconvertdate(new Date(this.dpermitissu));
      //alert(dpermitissu2);
      var pStartD2 = this.getconvertdate(this.pStartD);
      var pEndD2 = this.getconvertdate(new Date(this.pEndD));
      var permitdatetime2 = this.getcurrentdate();

      var str =
        "<div> Permit No:" +
        this.permitno +
        "</div>" +
        "<div> Registration Number:" +
        this.registration +
        "</div>" +
        "<div> Axle Configuration:" +
        this.axleconf +
        "</div>" +
        "<div> Source:" +
        this.origin +
        "</div>" +
        "<div> Destination:" +
        this.destination +
        "</div>";

      Swal.fire({
        title: "Do you want to save  \n" + "",
        html: "<pre>" + str + "</pre>" + "?",

        icon: "info",
        showCancelButton: true,
        confirmButtonColor: "#006400",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, Save it!",
      }).then((result) => {
        if (result.value) {
          Swal.fire({
            title: "Kenload V2. Please Wait!",
            html: "Saving data...", // add html attribute if you want or remove
            allowOutsideClick: false,
            showConfirmButton: false,
            willOpen: () => {
              Swal.showLoading();
            },
          });

          axios
            .get(
              window.$http +
                `wideload/max?permitid=WL` +
                window.$stationcode +
                this.ticketdate(),
              {
                headers: window.$headers,
              }
            )
            .then((response) => {
              let ticketmax = Number(response.data);
              ticketmax++;
              var ticketno = this.padLeadingZeros(ticketmax, 6);
              this.permitid =
                "WL" + window.$stationcode + this.ticketdate() + ticketno;
              const data = {
                permitid: this.permitid,
                permitno: this.permitno,
                registration: this.registration,
                traillerreg: this.traillerreg,
                transp: this.transp,
                transptel: this.transptel,
                transpadd: this.transpadd,
                model: this.model,
                origin: this.origin,
                destination: this.destination,
                axleconf: this.axleconf,
                firstAkit: this.firstAkit,
                fireext: this.fireext,
                drunkDriver: this.drunkDriver,
                dlicensestate: this.dlicensestate,
                inspstick: this.inspstick,
                insuaranceStic: this.insuaranceStic,
                loaddisc: this.loaddisc,

                dpermitissu: dpermitissu2,
                pStartD: pStartD2,
                pEndD: pEndD2,
                insecureload: this.insecureload,
                lightstatus: this.lightstatus,
                sterringdef: this.sterringdef,
                fhBreak: this.fhBreak,
                aoDefects: this.aoDefects,
                height_: this.height_,
                length_: this.length_,
                width_: this.width_,

                abnormalLPermit: this.abnormalLPermit,
                totaltyres: this.totaltyres,
                perconf: this.perconf,
                nonperconf: this.nonperconf,

                remarks: this.remarks,
                weighofload: this.weighofload,
                authweight: this.authweight,
                dateofTravel: this.dateofTravel,
                status: state,
                permitdatetime: permitdatetime2,
                UserName: JSON.parse(localStorage.user).email,
              };

              axios
                .post(window.$http + `wideload/`, data, {
                  headers: window.$headers,
                })
                .then(() => {
                  // JSON responses are automatically parsed.
                  //console.log(response.data);
                  this.addWideLoad = true;
                  Swal.close();
                  Swal.fire({
                    position: "center",
                    icon: "success",
                    title: "Your work has been saved",
                    showConfirmButton: false,
                    timer: 1500,
                  }).then(() => {
                    this.clearvalues();
                    this.updatearray();
                  });
                })
                .catch((e) => {
                  Swal.fire({
                    position: "center",
                    icon: "error",
                    title: "Wide Load Insert" + e,
                    showConfirmButton: true,
                  }).then(() => {
                    Swal.close();
                  });
                });
            })
            .catch((e) => {
              Swal.fire({
                position: "center",
                icon: "error",
                title: "Wide Load Max" + e,
                showConfirmButton: true,
              }).then(() => {
                Swal.close();
              });
            });
        }
      });
    },
    getmonth(d) {
      // const monthNames = [
      //   "January",
      //   "February",
      //   "March",
      //   "April",
      //   "May",
      //   "June",
      //   "July",
      //   "August",
      //   "September",
      //   "October",
      //   "November",
      //   "December",
      // ];
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
    padLeadingZeros(num, size) {
      var s = num + "";
      while (s.length < size) s = "0" + s;
      return s;
    },
    getmydate(mydate) {
      let d = new Date(mydate);
      let year = d.getFullYear();
      let month = this.getmonth(d.getMonth());
      let date = d.getDate();
      date = this.getv(date);
      //month = this.getv(month);

      //const msec = d.getMilliseconds();
      const datetime = date + "/" + month + "/" + year;
      return datetime;
    },
    position() {
      let val = this.validate();

      if (val == false) {
        return;
      }

      var fromdate = this.getmydate(new Date()) + " 12:00:00 AM";
      //alert(
      //   window.$http +
      //     `wideload/search?fromdate=` +
      //     fromdate +
      //     "&todate=" +
      //     todate +
      //     "&permitno=" +
      //     this.permitno
      // );

      axios
        .get(
          window.$http +
            `wideload/search?fromdate=` +
            fromdate +
            "&todate=" +
            fromdate +
            "&permitno=" +
            this.permitno,
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          // this.orderData = response.data;
          //console.log(response);
          //alert(response.data[0].permitno);
          var res = "";
          try {
            res = response.data[0].permitno;
          } catch (e) {
            //console.log(e);
          }
          if (this.permitno == res) {
            Swal.fire({
              title:
                "Permit Number " +
                this.permitno +
                " already exist for date " +
                this.getmydate(new Date()) +
                ". Do you want to add this?",
              text: "You won't be able to revert this!",
              icon: "warning",
              showCancelButton: true,
              confirmButtonColor: "#000000",
              cancelButtonColor: "#f46a6a",
              confirmButtonText: "Yes, Continue!",
            }).then((result) => {
              if (result.value) {
                //this.$delete(this.orderData, this.id - 1);
                this.savepermit();
              }
            });
          } else {
            this.savepermit();
          }

          // Swal.close();
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
    getorigin() {
      this.origins = [];
      for (let a = 0; a < this.towns.length; a++) {
        this.origins.push(this.towns[a].toUpperCase());
      }
      this.origins.push(this.towns);
      //alert(window.$http + `Origins/search?limit=100000`);
      axios
        .get(window.$http + `Origins/search?limit=100000`, {
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
    getvehiclemake() {
      this.Models = [];

      axios
        .get(window.$http + `VehicleMake`, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          response.data.forEach((e) => {
            this.Models.push(e.vehiclemake.toUpperCase());
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

          //throw e;
        });
    },
    deleterec(data, id, permitno) {
      //alert(rolename);
      this.id = id;
      this.permitno = permitno;
      Swal.fire({
        title: "Are you sure, you want to delete " + this.permitno + "?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, delete it!",
      }).then((result) => {
        if (result.value) {
          data.deletestatus = 1;
          axios
            .put(window.$http + `wideload/` + this.id, data, {
              headers: window.$headers,
            })
            .then(() => {
              // JSON responses are automatically parsed.
              //console.log(response.data);
              this.deleteWideLoad = true;
              Swal.fire(
                this.permitno + " Deleted!",
                "Your record has been deleted.",
                "success"
              ).then(() => {
                //this.clearvalues();
                this.updatearray();
              });
            })
            .catch((e) => {
              Swal.fire({
                position: "center",
                icon: "error",
                title: "" + e,
                showConfirmButton: true,
              }).then(() => {
                Swal.close();
              });
            });
        }
      });
    },
    clearvalues() {
      this.editmode = false;
      this.permitid = "";
      this.permitno = "";
      this.registration = "";
      this.traillerreg = "";
      this.transp = "";
      this.transptel = "";
      this.transpadd = "";
      this.model = "";
      this.origin = "";
      this.destination = "";
      this.axleconf = "";
      this.firstAkit = "";
      this.fireext = "";
      this.drunkDriver = "";
      this.dlicensestate = "";
      this.inspstick = "";
      this.insuaranceStic = "";
      this.loaddisc = "";

      this.dpermitissu = "";
      this.pStartD = "";
      this.pEndD = "";
      this.insecureload = "";
      this.lightstatus = "";
      this.sterringdef = "";
      this.fhBreak = "";
      this.aoDefects = "";
      this.height_ = 0;
      this.length_ = 0;
      this.width_ = 0;

      this.abnormalLPermit = "";
      this.totaltyres = "";
      this.perconf = "";
      this.nonperconf = "";

      this.remarks = "";
      this.weighofload = 0;
      this.authweight = 0;
      this.dateofTravel = "";
      this.status = "-Choose One-";
      this.permitdatetime = this.getcurrentdate();
      this.modaltitle = "Add Wide Load Permit(s)";
    },
    // try {
    //   fromdate = this.fromdate;
    // } catch (e) {
    //   fromdate = "";
    // }
    // try {
    //   todate = this.todate;
    // } catch (e) {
    //   todate = "";
    // }
    // if (fromdate <= 0) {
    //   return;
    // }
    // if (todate <= 0) {
    //   return;
    // }
    // fromdate = this.getmydate(this.fromdate) + " 12:00:00 AM";
    // todate = this.getmydate(this.todate) + " 11:59:59 PM";
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <!-- {{ width_ }} -->

    <div class="card changebg">
      <div class="card-body">
        <div class="row justify-content-between">
          <div class="col-sm-6">
            <button
              class="btn btn-primary waves-effect waves-light uil-export"
              @click="getrpt(generated_Exel)"
            >
              Export to CSV
            </button>
          </div>
          <div class="col-sm-1">
            <span>Date:</span>
            <date-picker
              @change="updatearray()"
              value="2021-05-17"
              v-model="fromdate"
              :first-day-of-week="1"
              lang="en"
            ></date-picker>
          </div>
          <div class="col-sm-2">
            <button
              @click="printpdf('l', p_pdf)"
              v-b-modal.modal-Print
              class="btn btn-primary waves-effect waves-light mdi-file-pdf"
            >
              Print PDF
            </button>
          </div>

          <div class="col-sm-2">
            <button
              v-b-tooltip.hover
              v-b-modal.modal-Edit
              @click="clearvalues()"
              class="btn btn-primary waves-effect waves-light mdi-file-pdf"
            >
              Add Wide Load Permit
            </button>
          </div>
        </div>
      </div>
    </div>
    <div class="card changebg">
      <div class="card-body">
        <form @submit.prevent="handleSubmit">
          <div class="row">
            <div class="col-sm-12">
              <div class="row">
                <div class="col-12">
                  <div>
                    <div class="float-end">
                      <form class="d-inline-flex mb-3"></form>
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
                      <template v-slot:cell(status)="data">
                        <div class="">
                          <img
                            class="img-fluid rounded"
                            v-bind:src="getimg(data.item.status)"
                            alt="error"
                            width="60"
                            height="20"
                          />
                        </div>
                      </template>
                      <!----
                          <template v-slot:cell(id)="data">
                            <a
                              href="javascript: void(0);"
                              class="text-dark fw-bold"
                              >{{ data.item.id }}</a
                            >
                          </template>

                          <template v-slot:cell(name)="data">
                            <a href="#" class="text-body">{{
                              data.item.name
                            }}</a>
                          </template>
                          
                          --->
                      <template v-slot:cell(Dimensions)="data">
                        <div class="badge bg-pill bg-soft-success font-size-12">
                          L:{{ data.item.length_ }},W:{{
                            data.item.width_
                          }},H:{{ data.item.height_ }}
                        </div>
                      </template>
                      <template v-slot:cell(index)="data">
                        <div class="">{{ data.index + 1 }}</div>
                      </template>
                      <template v-slot:cell(action)="data">
                        <ul class="list-inline mb-0">
                          <li class="list-inline-item">
                            <a
                              href="javascript:void(0);"
                              class="px-2 text-primary"
                              title="Edit"
                              v-b-modal.modal-Edit
                              @click="
                                edit(
                                  data.index,
                                  data.item.id,
                                  data.item.permitid,
                                  data.item.permitno,
                                  data.item.registration,
                                  data.item.traillerreg,
                                  data.item.transp,
                                  data.item.transptel,
                                  data.item.transpadd,
                                  data.item.model,
                                  data.item.origin,
                                  data.item.destination,
                                  data.item.axleconf,
                                  data.item.firstAkit,
                                  data.item.fireext,
                                  data.item.drunkDriver,
                                  data.item.dlicensestate,
                                  data.item.inspstick,
                                  data.item.insuaranceStic,
                                  data.item.loaddisc,
                                  data.item.dpermitissu,
                                  data.item.pStartD,
                                  data.item.pEndD,
                                  data.item.insecureload,
                                  data.item.lightstatus,
                                  data.item.sterringdef,
                                  data.item.fhBreak,
                                  data.item.aoDefects,
                                  data.item.height_,
                                  data.item.length_,
                                  data.item.width_,
                                  data.item.abnormalLPermit,
                                  data.item.totaltyres,
                                  data.item.perconf,
                                  data.item.nonperconf,
                                  data.item.remarks,
                                  data.item.weighofload,
                                  data.item.authweight,
                                  data.item.dateofTravel,
                                  data.item.amountPaid,
                                  data.item.status,
                                  data.item.permitdatetime
                                )
                              "
                            >
                              <i class="uil uil-pen font-size-18"></i>
                            </a>
                          </li>
                          <li class="list-inline-item">
                            <a
                              href="javascript:void(0);"
                              class="px-2 text-danger"
                              v-b-tooltip.hover
                              title="Delete"
                              @click="
                                deleterec(
                                  data.item,
                                  data.item.id,
                                  data.item.permitno,
                                  data.item.route,
                                  data.item.hour
                                )
                              "
                            >
                              <i class="uil uil-trash-alt font-size-18"></i>
                            </a>
                          </li>
                        </ul>
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
        </form>
      </div>
    </div>
    <b-modal
      id="modal-Edit"
      :title="modaltitle"
      hide-footer
      size="xl"
      centered
      modal-dialog
    >
      <div class="row">
        <form @submit.prevent="handleSubmit">
          <div class="row">
            <div class="col-sm-4">
              <div class="card">
                <div class="card-body changebg">
                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">Permit No:</div>
                      <input
                        @change="getpermit()"
                        class="form-control"
                        type="text"
                        placeholder="App-0000000"
                        v-model="permitno"
                      />
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Registration No:
                      </div>
                      <input
                        class="form-control"
                        type="text"
                        placeholder=""
                        v-model="registration"
                      />
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Trailer <br />Registration No:
                      </div>
                      <input
                        class="form-control"
                        type="text"
                        placeholder=""
                        v-model="traillerreg"
                      />
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">Transporter:</div>
                      <multiselect
                        v-model="transp"
                        :options="transporters"
                        class="form-control"
                      ></multiselect>
                      <!-- <select
                        class="form-control"
                        name="dgender"
                        v-model="transp"
                      >
                        <option value="">-Choose One-</option>
                        <option
                          v-for="transport in transporters"
                          :key="transport"
                        >
                          {{ transport }}
                        </option>
                      </select> -->
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Transporter Tel.:
                      </div>
                      <input
                        class="form-control"
                        type="text"
                        placeholder=""
                        v-model="transptel"
                      />
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Transporter<br />
                        Address:
                      </div>
                      <input
                        class="form-control"
                        type="text"
                        placeholder=""
                        v-model="transpadd"
                      />
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">Model:</div>
                      <multiselect
                        v-model="model"
                        :options="Models"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">Origin:</div>
                      <multiselect
                        v-model="origin"
                        :options="origins"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">Destination:</div>
                      <multiselect
                        v-model="destination"
                        :options="origins"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Axle Configuration:
                      </div>

                      <input
                        class="form-control"
                        list="regsOptions"
                        id="regsList"
                        placeholder="Type to search..."
                        v-model="axleconf"
                      />
                      <datalist
                        id="regsOptions"
                        v-for="reg in axles"
                        :key="reg.id"
                      >
                        <option
                          v-for="reg in axles"
                          :key="reg.id"
                          :value="reg"
                        ></option>
                      </datalist>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        First Aid Kit:
                      </div>
                      <multiselect
                        v-model="firstAkit"
                        :options="yesno"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Fire Extinguisher:
                      </div>
                      <multiselect
                        v-model="fireext"
                        :options="yesno"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <div class="col-sm-4">
              <div class="card">
                <div class="card-body changebg">
                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">Drunk Driver:</div>
                      <multiselect
                        v-model="drunkDriver"
                        :options="yesno"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Drivers License <br />Status:
                      </div>
                      <multiselect
                        v-model="dlicensestate"
                        :options="valid"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Inspection Sticker<br />
                        State:
                      </div>

                      <multiselect
                        v-model="inspstick"
                        :options="valid"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Insurance Sticker<br />
                        State:
                      </div>
                      <multiselect
                        v-model="insuaranceStic"
                        :options="valid"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Description Of <br />Load:
                      </div>
                      <input
                        class="form-control"
                        type="text"
                        placeholder=""
                        v-model="loaddisc"
                      />
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-12">
                        <div class="col-sm-5">
                          Date Permit <br />
                          Issued:
                        </div>
                        <div class="col-sm-7">
                          <date-picker
                            value="2021/05/17"
                            v-model="dpermitissu"
                            lang="en"
                          ></date-picker>
                        </div>
                      </div>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-12">
                        <div class="col-sm-5">Period Start Date:</div>
                        <div class="col-sm-7">
                          <date-picker
                            v-model="pStartD"
                            :first-day-of-week="1"
                            lang="en"
                          ></date-picker>
                        </div>
                      </div>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-12">
                        <div class="col-sm-5">Period End Date:</div>
                        <div class="col-sm-7">
                          <date-picker
                            v-model="pEndD"
                            :first-day-of-week="1"
                            lang="en"
                          ></date-picker>
                        </div>
                      </div>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Insecure Load:
                      </div>
                      <multiselect
                        v-model="insecureload"
                        :options="yesno"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Lights Status:
                      </div>
                      <multiselect
                        v-model="lightstatus"
                        :options="stats"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Steering Defects:
                      </div>
                      <multiselect
                        v-model="sterringdef"
                        :options="Defects"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>

                  <!-- End of row -->
                </div>
              </div>
            </div>

            <div class="col-sm-4">
              <div class="card">
                <div class="card-body changebg">
                  <!-- Begining of row -->

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Foot and Hand <br />
                        Brake Defects:
                      </div>
                      <multiselect
                        v-model="fhBreak"
                        :options="Defects"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Any Other Defect(s):
                      </div>
                      <multiselect
                        v-model="aoDefects"
                        :options="Defects"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">Height (m):</div>
                      <input
                        class="form-control"
                        type="text"
                        placeholder="App-0000000"
                        v-model="height_"
                      />
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">Length (m):</div>
                      <input
                        class="form-control"
                        type="text"
                        placeholder=""
                        v-model="length_"
                      />
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">Width (m):</div>
                      <input
                        class="form-control"
                        type="text"
                        placeholder=""
                        v-model="width_"
                      />
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Abnormal Load <br />Permit:
                      </div>
                      <multiselect
                        v-model="abnormalLPermit"
                        :options="valid"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">Total Tyres:</div>
                      <input
                        class="form-control"
                        type="text"
                        placeholder=""
                        v-model="totaltyres"
                      />
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Tyre(s) % Conf:
                      </div>
                      <input
                        ref="tyrecon"
                        @change="getconf2()"
                        class="form-control"
                        type="text"
                        placeholder=""
                        v-model="perconf"
                      />
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Tyre(s) % Non-Conf:
                      </div>
                      <input
                        ref="tyrenoncon"
                        @change="getconf()"
                        class="form-control"
                        type="text"
                        placeholder=""
                        v-model="nonperconf"
                      />
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">Remarks:</div>
                      <input
                        class="form-control"
                        type="text"
                        placeholder=""
                        v-model="remarks"
                      />
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Weight of Load (KG):
                      </div>
                      <input
                        class="form-control"
                        type="text"
                        placeholder="0"
                        v-model="weighofload"
                      />
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Authorised Weight:
                      </div>
                      <input
                        class="form-control"
                        type="text"
                        placeholder=""
                        v-model="authweight"
                      />
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-12">
                        <div class="col-sm-5">Date of Travel:</div>
                        <div class="col-sm-7">
                          <date-picker
                            value="2021-05-17"
                            v-model="dateofTravel"
                            :first-day-of-week="1"
                            lang="en"
                          ></date-picker>
                        </div>
                      </div>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Amount Charged:
                      </div>
                      <input
                        class="form-control"
                        type="text"
                        placeholder=""
                        v-model="amountPaid"
                      />
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">Status:</div>

                      <select
                        class="form-control"
                        name="dgender"
                        v-model="status"
                      >
                        <option value="-Choose One-">-Choose One-</option>
                        <option value="Legal">Legal</option>
                        <option value="Charged">Charged</option>
                      </select>
                    </div>
                  </div>

                  <!-- End -->
                </div>
              </div>
            </div>
          </div>
        </form>
      </div>

      <!----@click="position(), $bvModal.hide('modal-1') --->
      <div class="row">
        <div class="col-sm-3 mb-2">
          <b-button v-b-modal.modal-AddOrigin variant="success"
            >ADD Origin / Destination
          </b-button>
        </div>
        <div class="col-sm-6 mb-2">
          <b-button v-b-modal.modal-AddTransporter variant="success"
            >ADD Transporter
          </b-button>
        </div>
        <div class="col-sm-3 mb-2">
          <b-button v-show="!editmode" variant="dark" @click="position()"
            >Add {{ title }}</b-button
          >
          <b-button v-show="editmode" variant="dark" @click="editrec()"
            >Edit {{ title }}</b-button
          >
        </div>
      </div>
    </b-modal>

    <b-modal
      id="modal-AddTransporter"
      title="Add Transporter"
      hide-footer
      size="lg"
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
                      Transporters Name:
                    </div>

                    <input
                      class="form-control"
                      type="text"
                      placeholder="Name"
                      v-model="transporter_name"
                    />
                  </div>
                  <div class="input-group">
                    <div class="input-group-text col-sm-4">
                      Transporters Address:
                    </div>

                    <input
                      class="form-control"
                      type="text"
                      placeholder="Address"
                      v-model="transporter_address"
                    />
                    <div class="input-group">
                      <div class="input-group-text col-sm-4">
                        Transporters Contact:
                      </div>

                      <input
                        class="form-control"
                        type="text"
                        placeholder="Phone"
                        v-model="transporter_contact"
                      />
                    </div>
                  </div>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
      <!----@click="position(), $bvModal.hide('modal-1') --->
      <div class="row">
        <div class="col-sm-9 mb-2"></div>
        <div class="col-sm-3 mb-2">
          <b-button variant="dark" @click="addTransporter()"
            >Add Transporter</b-button
          >
        </div>
      </div>
    </b-modal>

    <b-modal
      id="modal-AddOrigin"
      title="Add Origin/ Destination"
      hide-footer
      size="lg"
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
                      Enter Origin/ Destination :
                    </div>

                    <input
                      class="form-control"
                      type="text"
                      placeholder="Name"
                      v-model="origin_name"
                    />
                  </div>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
      <!----@click="position(), $bvModal.hide('modal-1') --->
      <div class="row">
        <div class="col-sm-9 mb-2"></div>
        <div class="col-sm-3 mb-2">
          <b-button variant="dark" @click="addOrigin()"
            >Add Origin / Destination</b-button
          >
        </div>
      </div>
    </b-modal>

    <b-modal id="modal-Print" title="Print PDF" hide-footer size="bg" centered>
      <reportdet
        :title="title"
        :printedpdf="true"
        :orderData="orderData"
        :pl="pl"
        :headers="headers"
        :uniqueCars="uniqueCars"
        v-show="showme"
      ></reportdet>
    </b-modal>

    <!-- hallo {{ info }} -->
  </Layout>
</template>
<style scoped>
.changebg {
  background-color: rgb(240, 240, 240);
}
.modal-wrapper {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100%;
}
.modal-container {
  display: inline-block;
  width: auto;
}
</style>