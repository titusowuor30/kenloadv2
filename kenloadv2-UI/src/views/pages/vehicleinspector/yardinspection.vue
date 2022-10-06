<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import Multiselect from "vue-multiselect";
import axios from "axios";
import reportdet from "@/components/report/header";
import Swal from "sweetalert2";

export default {
  page: {
    title: "Heavy Vehicles Inspection",
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
    reportdet,
    Multiselect,
  },
  data() {
    return {
      title: "Heavy Vehicles Inspection",
      items: [
        {
          text: "USER: VI",
        },
        {
          text: "Heavy Vehicles Inspection",
          active: true,
        },
      ],
      yardid: 0,
      ticketno: "",
      registration: "",
      tareweight: 0,
      permitno: "",
      permitid: "",
      regs: [],
      yardlist: [],
      serialno: "",
      traillerreg: "",
      model: "",
      axlecount: 0,
      transptel: "",
      email: "",
      transp: "",
      transpadd: "",
      loaddisc: "",
      SteeringOL: "",
      SteeringBP: "",
      rodend: "",
      balljoints: "",
      wheelallignment: "",
      insecureload: "",
      lightstatus: "",
      sterringdef: "",
      fhBreak: "",
      aoDefects: "",
      abnormalLPermit: "",
      totaltyres: 0,
      perconf: 0,
      nonperconf: 0,
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

      shifts: ["Shift A", "Shift B", "Shift C"],
      routes: ["Nairobi-Mombasa", "Mombasa-Nairobi"],
      transporters: [],
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
      modaltitle: "Add Heavy Inspection",
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
      info: "",
      totalRows: 1,
      viewYardInspection: false,
      addYardInspection: false,
      editYardInspection: false,
      deleteYardInspection: false,
      printReport: false,
      clickedPrintpdf: "",
      filteredValues: "",
      printedpdf: false,
      generatedExel: "",
      currentPage: 1,
      perPage: 10,
      pageOptions: [1, 10, 25, 50, 100],
      filter: null,
      filterOn: [],
      sortBy: "id",
      sortDesc: false,
      fields: [
        {
          key: "id",
          label: "#",
          sortable: true,
        },
        {
          key: "permitno",
          label: "Inspection No",
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
          key: "status",
          label: "Status",
          sortable: true,
        },
        {
          key: "authweight",
          label: "Weight of Load ",
          sortable: true,
        },
        "Dimensions",
        "action",
      ],
    };
  },
  watch: {
    viewYardInspection(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Opened Heavy Vehicles Inspection:${newValue}`,
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
        application: window.navigator.appCodeName,
        details: `Heavy Vehicles Inspection reports:${this.clickedPrintpdf},${this.generatedExel},${this.filteredValues}`,
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
    addYardInspection(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Added YardI nspection:${newValue}`,
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
    editYardInspection(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Edited Heavy Vehicles Inspection:${newValue}`,
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
    deleteYardInspection(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Deleted Heavy Vehicles Inspection record:${newValue},id:${this.id}`,
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

    this.totalRows = this.items.length;
    this.updatearray();
    this.gettransporters();
    this.viewYardInspection = true;
  },
  methods: {
    gettransporters() {
      this.transporters = [];
      axios
        .get(window.$http + `Transporters`, { headers: window.$headers })
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
      axios
        .get(window.$http + `yardinspection/search?deletestatus=0`, {
          headers: window.$headers,
        })
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
      this.regs = [];
      axios
        .get(window.$http + `yardlist/search?status=I&inspected=0&limit=1000`, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.yardlist = response.data;
          response.data.forEach((e) => {
            this.regs.push(e.vehreg.toUpperCase());
          });
          //console.log(this.orderData);

          //Swal.close();
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Heavy Vehicles List " + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
    },
    getdetails() {
      var wbtno = "";
      for (var i = 0; i < this.yardlist.length; i++) {
        if (this.registration == this.yardlist[i].vehreg) {
          wbtno = this.yardlist[i].wbtno;
        }
      }
      axios
        .get(
          window.$http +
            `weighbridgetransactions/search?wbrg_ticket_no=` +
            wbtno,
          { headers: window.$headers }
        )
        .then((response) => {
          this.origin = response.data[0].wbrg_ticket_source;
          this.destination = response.data[0].wbrg_ticket_destination;
          this.loaddisc = response.data[0].wbrg_product;
          this.transp = response.data[0].wbrg_tx_code;
          this.model = response.data[0].wbrg_ticket_make;
          this.axleconf = response.data[0].wbrg_ticket_axel;
          this.traillerreg = response.data[0].wbgr_ticket_trailer_no;
          this.firstAkit = "YES";
          this.fireext = "YES";
          this.drunkDriver = "NO";
          this.dlicensestate = "VALID";
          this.insuaranceStic = "VALID";
          this.inspstick = "VALID";
          this.insecureload = "NO";
          this.lightstatus = "OK";
          this.sterringdef = "None";
          this.SteeringOL = "OK";
          this.SteeringBP = "OK";
          this.rodend = "OK";
          this.balljoints = "OK";
          this.wheelallignment = "OK";
          this.fhBreak = "NO";
          this.aoDefects = "None";
          this.perconf = 87;
          this.nonperconf = 13;
          this.remarks = "None";
        });
    },
    printpdf(pl, p_pdf) {
      this.printReport = true;
      p_pdf = "Clicked print pdf button";
      this.clickedPrintpdf = p_pdf;
      this.pl = pl;
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
        const [key, value] = val;
        console.log(key, value);
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
      this.filteredValues = "Filtered search '" + this.filter + "'";
    },
    handleSubmit() {
      console.log("Error on submit");
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

      SteeringOL,
      SteeringBP,
      rodend,
      balljoints,
      wheelallignment,

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
      this.modaltitle = "Edit Heavy Vehicles Inspection";
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

      this.SteeringOL = SteeringOL;
      this.SteeringBP = SteeringBP;
      this.rodend = rodend;
      this.balljoints = balljoints;
      this.wheelallignment = wheelallignment;

      this.remarks = remarks;
      this.weighofload = weighofload;
      this.authweight = authweight;
      this.dateofTravel = new Date(dateofTravel);
      this.status = status;
      this.amountPaid = amountPaid;
      this.permitdatetime = permitdatetime;
      //this.focusInput();

      //this.$refs.search.focus();
      //$("#search").focus();
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
      if (this.status.trim() == "-Choose One-") {
        Swal.fire("Please select Status !");
        return val;
      }

      val = true;
      return val;
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
      let month = d.getMonth();
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

          var dpermitissu2 = this.getcurrentdate(new Date(this.dpermitissu));
          // alert(dpermitissu2);
          var pStartD2 = this.getcurrentdate(new Date(this.pStartD));
          var pEndD2 = this.getcurrentdate(new Date(this.pEndD));
          var permitdatetime2 = this.getcurrentdate(
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
            .put(window.$http + `yardinspection/` + this.id, data, {
              headers: window.$headers,
            })
            .then(() => {
              // JSON responses are automatically parsed.
              this.editYardInspection = true;

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
    getrpt(g_excel) {
      //alert(new Date());
      this.printReport = true;
      g_excel = "Generated excel document";
      this.generatedExel = g_excel;
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
          // alert(escaped);
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
    padLeadingZeros(num, size) {
      var s = num + "";
      while (s.length < size) s = "0" + s;
      return s;
    },
    position() {
      let val = this.validate();

      if (val == false) {
        return;
      }
      if (Number(this.totaltyres.trim()) <= 0) {
        Swal.fire({
          position: "center",
          icon: "error",
          title: "Enter Total Tyres:",
          showConfirmButton: true,
        }).then(() => {
          Swal.close();
        });
        return;
      }

      var state = "C";
      if (this.status == "Legal") {
        state = "L";
      }
      for (var i = 0; i < this.yardlist.length; i++) {
        if (this.yardlist[i].vehreg == this.registration) {
          this.ticketno = this.yardlist[i].wbtno;
          this.yardid = this.yardlist[i].id;
        }
      }
      var permitdatetime2 = this.getcurrentdate(new Date());
      var str =
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
        "</div>" +
        "<div> Status:" +
        this.status +
        "</div>";

      Swal.fire({
        title:
          "Do you want add vehicle " +
          this.registration +
          " to yard inspection   \n" +
          "",
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
                `yardinspection/max?inspectionid=YI` +
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
              var inspectionid =
                "YI" + window.$stationcode + this.ticketdate() + ticketno;
              const data = {
                inspectionid: inspectionid,
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
                SteeringOL: this.SteeringOL,
                SteeringBP: this.SteeringBP,
                rodend: this.rodend,
                balljoints: this.balljoints,
                wheelallignment: this.wheelallignment,
                lightstatus: this.lightstatus,
                sterringdef: this.sterringdef,
                fhBreak: this.fhBreak,
                aoDefects: this.aoDefects,
                height_: this.height_,
                length_: this.length_,
                width_: this.width_,
                totaltyres: this.totaltyres,
                perconf: this.perconf,
                nonperconf: this.nonperconf,
                remarks: this.remarks,
                status: state,
                datetime: permitdatetime2,
                station: window.$stationcode,
                ticketno: this.ticketno,
              };

              axios
                .post(window.$http + `yardinspection/`, data, {
                  headers: window.$headers,
                })
                .then((response) => {
                  // JSON responses are automatically parsed.
                  console.log(response.data);
                  this.addYardInspection = true;
                  Swal.close();
                  Swal.fire({
                    position: "center",
                    icon: "success",
                    title: "Your work has been saved",
                    showConfirmButton: false,
                    timer: 1500,
                  }).then(() => {
                    const yarddata = {
                      id: this.yardid,
                      inspected: 1,
                    };
                    //alert(window.$http + `yardlist/` + this.yard[0].id);
                    axios
                      .put(
                        window.$http + `yardlist/inspected?id=` + this.yardid,
                        yarddata,
                        {
                          headers: window.$headers,
                        }
                      )
                      .then(() => {
                        this.editYardInspection = true;
                        //saveticket();
                        Swal.fire({
                          position: "center",
                          icon: "success",
                          title: "Updated yard.",
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
                          title: "Update Heavy Vehicles List" + e,
                          showConfirmButton: true,
                        }).then(() => {
                          Swal.close();
                        });
                      });
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
            })
            .catch((e) => {
              Swal.fire({
                position: "center",
                icon: "error",
                title: "Heavy Vehicles Inspection Max" + e,
                showConfirmButton: true,
              }).then(() => {
                Swal.close();
              });
            });
        }
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
            .put(window.$http + `yardinspection/` + this.id, data, {
              headers: window.$headers,
            })
            .then((response) => {
              // JSON responses are automatically parsed.
              console.log(response.data);
              this.deleteYardInspection = true;
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
      this.permitdatetime = "";
      this.modaltitle = "Add Heavy Vehicles Inspection";
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />

    <div class="card">
      <div class="card-body">
        <div class="row justify-content-between">
          <div class="col-sm-6">
            <button
              class="btn btn-primary waves-effect waves-light uil-export"
              @click="getrpt(g_excel)"
            >
              Export to CSV
            </button>
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
              Add Heavy Vehicles Inspection
            </button>
          </div>
        </div>
      </div>
    </div>
    <div class="card">
      <div class="card-body">
        <form @submit.prevent="handleSubmit">
          <div class="row">
            <div class="col-sm-12">
              <div class="card">
                <div class="card-body changebg">
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
                                class="
                                  d-inline-flex
                                  align-items-center
                                  fw-normal
                                "
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
                                class="
                                  d-inline-flex
                                  align-items-center
                                  fw-normal
                                "
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
                          <!----
                          <template v-slot:cell(check)="data">
                            <div class="">
                              <input
                                type="button"
                                class="custom-control-input"
                                :id="`contacusercheck${data.item.id}`"
                                value="Submit"
                              />
                              <label
                                class="custom-control-label"
                                :for="`contacusercheck${data.item.id}`"
                              ></label>
                            </div>
                          </template>
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
                            <div
                              class="badge bg-pill bg-soft-success font-size-12"
                            >
                              L:{{ data.item.length_ }},W:{{
                                data.item.width_
                              }},H:{{ data.item.height_ }}
                            </div>
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
                                      data.item.SteeringOL,
                                      data.item.SteeringBP,
                                      data.item.rodend,
                                      data.item.balljoints,
                                      data.item.wheelallignment,
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
                      <div class="input-group-text col-sm-5">
                        Registration No:
                      </div>

                      <input
                        @blur="getdetails()"
                        class="form-control"
                        list="regsOptions"
                        id="regsList"
                        placeholder="Type to search..."
                        v-model="registration"
                      />
                      <datalist
                        id="regsOptions"
                        v-for="reg in regs"
                        :key="reg.id"
                      >
                        <option
                          v-for="reg in regs"
                          :key="reg.id"
                          :value="reg"
                        ></option>
                      </datalist>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Trailer <br />
                        Registration No:
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
                        :options="towns"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">Destination:</div>
                      <multiselect
                        v-model="destination"
                        :options="towns"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Axle Configuration:
                      </div>
                      <multiselect
                        v-model="axleconf"
                        :options="axles"
                        class="form-control"
                      ></multiselect>
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
                        Inspection Sticker <br />
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
                        Insurance Sticker <br />
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
                        Description Of Load:
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
                      <div class="input-group-text col-sm-5">
                        Steering Oil Level:
                      </div>

                      <multiselect
                        v-model="SteeringOL"
                        :options="stats"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Steering Box <br />& Pipes:
                      </div>

                      <multiselect
                        v-model="SteeringBP"
                        :options="stats"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">The Rod End:</div>

                      <multiselect
                        v-model="rodend"
                        :options="stats"
                        class="form-control"
                      ></multiselect>
                    </div>
                  </div>

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">Ball Joints:</div>

                      <multiselect
                        v-model="balljoints"
                        :options="stats"
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
                        Wheel Allignment:
                      </div>

                      <multiselect
                        v-model="wheelallignment"
                        :options="stats"
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

                  <div class="col-sm-12">
                    <div class="input-group">
                      <div class="input-group-text col-sm-5">
                        Foot and
                        <br />Hand Brake Defects:
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
                      <div class="input-group-text col-sm-5">Total Tyres:</div>
                      <input
                        class="form-control"
                        type="number"
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
                        type="number"
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
                        type="number"
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
        <div class="col-sm-9 mb-2"></div>
        <div class="col-sm-3 mb-2">
          <b-button v-show="!editmode" variant="dark" @click="position()"
            >Add Heavy Vehicle ({{ this.registration }})</b-button
          >
          <b-button v-show="editmode" variant="dark" @click="editrec()"
            >Edit Heavy Vehicle ({{ this.registration }})
          </b-button>
        </div>
      </div>
    </b-modal>
    <b-modal id="modal-Print" title="Print PDF" hide-footer size="bg" centered>
      <reportdet
        :title="title"
        :orderData="orderData"
        :pl="pl"
        :printpdf="true"
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
  background-color: #f7f6ebfb;
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