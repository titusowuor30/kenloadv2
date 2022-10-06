<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import Swal from "sweetalert2";
//import html2canvas from "html2canvas";
//import rptheader from "@/components/report/header.js";
import reportdet from "@/components/report/header";

import axios from "axios";

export default {
  page: {
    title: "Axle Configuration",
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
  },
  data() {
    return {
      title: "Axle Configuration",
      addaxle_config: false,
      editaxle_config: false,
      deleteaxle_config: false,
      viewAxle_config: false,
      clickedPrintpdf: "",
      printedpdf: true,
      generatedExel: "",
      printReport: false,
      filteredValues: "",
      items: [
        {
          text: "USER: Admin",
        },
        {
          text: "Axle Configuration List",
          active: true,
        },
      ],
      orderDatanew: null,
      pl: "",
      orderData: [],
      axleData: [],
      id: "",
      errors: "",
      headers: null,
      uniqueCars: null,
      showme: true,
      totalRows: 1,
      currentPage: 1,
      perPage: 500,
      edit_axle: false,
      pageOptions: [
        1,
        2,
        5,
        10,
        25,
        50,
        100,
        200,
        300,
        500,
        1000,
        2000,
        3000,
        4000,
        5000,
        6000,
        9000,
        10000,
        20000,
      ],
      roadauthoritys: ["KeNHA", "KURA"],
      filter: null,
      filterOn: [],
      sortBy: "id",
      sortDesc: false,
      axle_code: "",
      Permissible: "",
      Permissibleaxle: [],
      Permissiblegvw: "",
      Permissiblegroupdeck: "",
      axleids: [],
      roadauthority: "",
      prefix: "",
      myindex: "",
      editmode: false,
      modaltitle: "Add",
      axle_val: "",
      fields: [
        {
          key: "id",
          label: "#",
          sortable: true,
        },
        {
          key: "axle_code",
          label: "Axle Configuration Name",
          sortable: true,
        },

        {
          key: "Permissible",
          label: "Permissible Grouping Deck [A,B,C,D]",
          sortable: true,
        },
        {
          key: "Permissibleaxle",
          label: "Permissible Axle [KG]",
          sortable: true,
          editable: true,
        },
        {
          key: "Permissiblegvw",
          label: "Permissible GVW [KG]",
          sortable: true,
        },
        "action",
      ],
    };
  },

  watch: {
    viewAxle_config(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Opned axle configuration:${newValue}`,
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
        details: `Axle Config reports:${this.clickedPrintpdf},${this.generatedExel},${this.filteredValues}`,
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
    addaxle_config(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Add axle_config:${newValue},value:${this.axle_code}`,
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
    editaxle_config() {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Edited axle configurations,axle code:${this.axle_code},axle weight id:${this.id},new value:${this.axle_val}`,
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
    deleteaxle_config(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.appCodeName,
        details: `Delete axle_config:${newValue},axle_config id:${this.id}`,
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
  created() {
    if (window.location.href != "") {
      this.viewAxle_config = true;
    }
  },
  computed: {
    /**
     * Total no. of records
     */
    rows() {
      return this.orderData.length;
    },
    editableFields() {
      return this.fields.filter((field) => {
        return field.editable === true;
      });
    },
  },
  mounted() {
    // Set the initial number of items
    this.updatearray();
    this.updateAxleData();
    this.totalRows = this.items.length;
    this.calc_gvw();
    this.calc_permgroupaxle();
  },
  methods: {
    calc_permgroupaxle() {
      var axle = [];
      var groupaxle = [];
      let totA = 0;
      let totB = 0;
      let totC = 0;
      let totD = 0;
      var axleids = JSON.parse("[" + this.axleids + "]");
      var weights = JSON.parse("[" + this.Permissibleaxle + "]");
      axleids.forEach((id, i) => {
        var axle_obj = this.AxleWeightXreffs.filter((item) => item.id === id);
        if (axle_obj) {
          axle[i] = axle_obj[0].axle_legalweight;
          if (axle_obj[0].axle_deckgrouping == "A") {
            totA = totA + Number(weights[i]);
          }
          if (axle_obj[0].axle_deckgrouping == "B") {
            totB = totB + Number(weights[i]);
          }
          if (axle_obj[0].axle_deckgrouping == "C") {
            totC = totC + Number(weights[i]);
          }
          if (axle_obj[0].axle_deckgrouping == "D") {
            totD = totD + Number(weights[i]);
          }
        }
      });
      groupaxle[0] = totA;
      groupaxle[1] = totB;
      groupaxle[2] = totC;
      groupaxle[3] = totD;
      this.Permissiblegroupdeck = groupaxle;
      console.log(groupaxle);
    },
    calc_gvw() {
      var total = 0;
      var weight;
      var Permissibleaxle = JSON.parse("[" + this.Permissibleaxle + "]");
      Permissibleaxle.forEach((val, i) => {
        //console.log(document.getElementById("axle_val_" + id));
        //var weight = Number(document.getElementById("axle_val_" + id).value);
        weight = Number(this.Permissibleaxle[i]);
        total += weight;
      });
      console.log(this.Permissibleaxle);
      console.log(total);
      this.Permissiblegvw = total;
    },
    updateAxleData() {
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
            `AxleWeightConfig/report/axleconfig?limit=` +
            this.perPage,
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          this.axleData = response.data;
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
        .get(window.$http + `AxleWeightXreffs`, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.AxleWeightXreffs = response.data;

          axios
            .get(window.$http + `AxleWeightConfig/`, {
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
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "axle config" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });
    },
    /**
     * Export to CSV
     */
    getrpt(p_excel) {
      this.printReport = true;
      p_excel = "Generated an excel document";
      this.generatedExel = p_excel;
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
      const data = this.axleData.map((row) => ({
        ID: row.id,
        "Axle Name": row.axle_name,
        "Permissible Grouping Deck": row.axle_deckgrouping,
        "Permissible Axle": row.axle_group,
        "Permissible GVW": row.axle_legalweight,
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

    /**
     * Search the table data with search input
     */
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
      this.printReport = true;
      this.filteredValues = this.filter;
    },
    handleSubmit() {
      console.log("Error on submit");
    },
    printpdf(pl, p_pdf) {
      this.printReport = true;
      p_pdf = "Clicked print pdf button";
      this.clickedPrintpdf = p_pdf;
      this.pl = pl;
      const data = this.axleData.map((row) => ({
        ID: row.id,
        "Axle Configuration Name": row.axle_name,
        "Permissible Grouping Deck [A,B,C,D]": row.axle_deckgrouping,
        "Permissible Axle [KG]": row.axle_legalweight,
        "Permissible GVW [KG]": row.axle_typeoftyres,
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
    editaxle() {
      //alert(this.axle_val)
      //axle_weights = JSON.parse("[" + this.Permissibleaxle + "]");
      var axleids = JSON.parse("[" + this.axleids + "]");
      var Permissibleaxle = JSON.parse("[" + this.Permissibleaxle + "]");
      Swal.fire({
        position: "center",
        icon: "warning",
        title:
          "Are you sure you want to edit Axle Configuration " + this.axle_code,
        html: "You won't be able to revert this!",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, edit it!",
        closeOnConfirm: true,
      }).then((res) => {
        if (res.value) {
          this.$bvModal.hide("modal-Edit");
          axleids.forEach((id, i) => {
            var axle_obj = this.AxleWeightXreffs.filter(
              (item) => item.id === id
            );
            console.log(Permissibleaxle);
            console.log(axle_obj);
            this.axle_val = Number(Permissibleaxle[i]);
            var data = {
              axle_code: this.axle_code,
              id: id,
              axle_code2: axle_obj[0].axle_code2,
              axle_legalweight: this.axle_val,
              axle_group: axle_obj[0].axle_group,
              axle_grouping: axle_obj[0].axle_grouping,
              axle_deckgrouping: axle_obj[0].axle_deckgrouping,
              axle_typeoftyres: axle_obj[0].axle_typeoftyres,
            };
            axios
              .put(window.$http + "AxleWeightXreffs/" + id, data, {
                headers: window.$headers,
              })
              .then((result) => {
                this.editaxle_config = true;
                console.log(result.data);
                axios
                  .get(window.$http + `AxleWeightXreffs`, {
                    headers: window.$headers,
                  })
                  .then((response) => {
                    this.AxleWeightXreffs = response.data;
                    axios
                      .get(window.$http + `AxleWeightConfig/`, {
                        headers: window.$headers,
                      })
                      .then((response) => {
                        this.orderData = response.data;
                      });
                  });
              })
              .catch((e) => {
                Swal.fire({
                  position: "center",
                  icon: "danger",
                  title: "An Error Occurred!",
                  html: "Error:" + e,
                  showConfirmButton: true,
                });
              });
          });
          Swal.fire({
            position: "center",
            icon: "success",
            title: "Success!!",
            html: "Axle Weights saved!",
            showConfirmButton: true,
          });
        }
      });
    },
    editpermaxle() {
      //alert(this.axle_val)
      var total = 0;
      //axle_weights = JSON.parse("[" + this.Permissibleaxle + "]");
      var Permissibleaxle = JSON.parse("[" + this.Permissibleaxle + "]");
      Permissibleaxle.forEach((val) => {
        //var weight = Number(document.getElementById("axle_val_" + id).value);
        total += Number(val);
      });
      if (total > 56000) {
        Swal.fire({
          position: "center",
          icon: "warning",
          title:
            "Axle Weight of " +
            new Intl.NumberFormat().format(total) +
            "KG exceeds maximum total of 56,000KG",
          html: "Please Reconfigure to comply with maximum axle total weight!",
          showCancelButton: true,
          cancelButtonColor: "#000000",
          confirmButtonColor: "#f46a6a",
          cancelButtonText: "Reconfigure",
          confirmButtonText: "Proceed anyway",
          closeOnConfirm: true,
        }).then((result) => {
          if (result.value) {
            this.editaxle();
          } else {
            return;
          }
        });
        return;
      }
      this.editaxle();
    },
    getpermaxle(axlec) {
      var axle = [];
      let totA = 0;
      let totB = 0;
      let totC = 0;
      let totD = 0;
      let a = 0;
      for (let i = 0; i < this.AxleWeightXreffs.length; i++) {
        if (this.AxleWeightXreffs[i].axle_code == axlec) {
          axle[a] = this.AxleWeightXreffs[i].axle_legalweight;
          a++;
          if (this.AxleWeightXreffs[i].axle_deckgrouping == "A") {
            totA = totA + Number(this.AxleWeightXreffs[i].axle_legalweight);
          }
          if (this.AxleWeightXreffs[i].axle_deckgrouping == "B") {
            totB = totB + Number(this.AxleWeightXreffs[i].axle_legalweight);
          }
          if (this.AxleWeightXreffs[i].axle_deckgrouping == "C") {
            totC = totC + Number(this.AxleWeightXreffs[i].axle_legalweight);
          }
          if (this.AxleWeightXreffs[i].axle_deckgrouping == "D") {
            totD = totD + Number(this.AxleWeightXreffs[i].axle_legalweight);
          }
        }
      }

      return axle;
    },
    getpermaxleids(axlec) {
      var axleid = [];
      let a = 0;
      for (let i = 0; i < this.AxleWeightXreffs.length; i++) {
        if (this.AxleWeightXreffs[i].axle_code == axlec) {
          axleid[a] = this.AxleWeightXreffs[i].id;
          a++;
        }
      }
      return axleid;
    },
    getpermgvw(axlec) {
      let gvw = 0;

      for (let i = 0; i < this.AxleWeightXreffs.length; i++) {
        if (this.AxleWeightXreffs[i].axle_code == axlec) {
          gvw += this.AxleWeightXreffs[i].axle_legalweight;
        }
      }
      return gvw;
    },
    getpermgroupaxle(axlec) {
      var axle = [];
      var groupaxle = [];
      let totA = 0;
      let totB = 0;
      let totC = 0;
      let totD = 0;
      for (let i = 0; i < this.AxleWeightXreffs.length; i++) {
        if (this.AxleWeightXreffs[i].axle_code == axlec) {
          axle[i] = this.AxleWeightXreffs[i].axle_legalweight;
          if (this.AxleWeightXreffs[i].axle_deckgrouping == "A") {
            totA = totA + Number(this.AxleWeightXreffs[i].axle_legalweight);
          }
          if (this.AxleWeightXreffs[i].axle_deckgrouping == "B") {
            totB = totB + Number(this.AxleWeightXreffs[i].axle_legalweight);
          }
          if (this.AxleWeightXreffs[i].axle_deckgrouping == "C") {
            totC = totC + Number(this.AxleWeightXreffs[i].axle_legalweight);
          }
          if (this.AxleWeightXreffs[i].axle_deckgrouping == "D") {
            totD = totD + Number(this.AxleWeightXreffs[i].axle_legalweight);
          }
        }
      }
      groupaxle[0] = totA;
      groupaxle[1] = totB;
      groupaxle[2] = totC;
      groupaxle[3] = totD;
      return groupaxle;
    },
    getpergvw() {},
    edit(
      index,
      myid,
      axle_code,
      Permissible,
      Permissiblegroupdeck,
      Permissibleaxle,
      Permissiblegvw,
      axleids
    ) {
      //alert(rolename);
      this.editmode = true;
      this.modaltitle = "Edit Axle Configurations";
      this.id = myid;
      this.axle_code = axle_code;
      this.Permissible = Permissible;
      this.Permissiblegroupdeck = Permissiblegroupdeck;
      this.Permissibleaxle = Permissibleaxle;
      this.Permissiblegvw = Permissiblegvw;
      this.axleids = axleids;
      this.myindex = index;
      //this.focusInput();

      //this.$refs.search.focus();
      //$("#search").focus();
    },
    editrec() {
      if (this.axle_code.trim() == "") {
        Swal.fire("Please enter  Axle Configuration Name!");
        return;
      }

      //alert(this.myindex);

      const data = {
        id: this.id,
        axle_code: this.axle_code,
        Permissible: this.Permissible,
        Permissibleaxle: this.Permissibleaxle,
        Permissiblegvw: this.Permissiblegvw,
        active: 0,
      };
      //alert(this.screenname);
      axios
        .put(window.$http + `AxleWeightConfig/` + this.id, data, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          this.orderData[this.myindex].id = this.id;
          this.orderData[this.myindex].axle_code = this.axle_code;
          this.orderData[this.myindex].Permissible = this.Permissible;
          this.orderData[this.myindex].Permissibleaxle = this.Permissibleaxle;
          this.orderData[this.myindex].Permissiblegvw = this.Permissiblegvw;
          this.orderData[this.myindex].active = this.active;
          console.log(response.data);
          this.editaxle_config = true;
          if (response.data.trim() != "") {
            Swal.fire({
              position: "center",
              icon: "success",
              title: "Your work has been saved" + response.data,
              showConfirmButton: false,
              timer: 1500,
            });
          }
        })
        .catch((e) => {
          Swal.fire("" + e);
          //this.errors.push(e);
        });

      Swal.fire({
        position: "center",
        icon: "success",
        title: "Your work has been saved",
        showConfirmButton: false,
        timer: 1500,
      });
    },
    position() {
      if (this.axle_code.trim() == "") {
        Swal.fire("Please enter  AxleWeightConfig Name!");
        return;
      }

      var orderid = this.orderData.length + 1;
      this.orderData.push({
        id: orderid,
        axle_code: this.axle_code,
        Permissible: this.Permissible,
        Permissibleaxle: this.Permissibleaxle,
        Permissiblegvw: this.Permissiblegvw,
      });
      const data = {
        axle_code: this.axle_code,
        Permissible: this.Permissible,
        Permissibleaxle: this.Permissibleaxle,
        Permissiblegvw: this.Permissiblegvw,
        active: 0,
      };
      axios
        .post(window.$http + `AxleWeightConfig/`, data, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          console.log(response.data);
          this.addaxle_config = true;
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
    deleterec(
      index,
      id,
      axle_code,
      Permissible,
      Permissibleaxle,
      Permissiblegvw
    ) {
      //alert(rolename);
      this.id = id;
      this.axle_code = axle_code;
      this.Permissible = Permissible;
      this.Permissibleaxle = Permissibleaxle;
      this.Permissiblegvw = Permissiblegvw;
      Swal.fire({
        title: "Are you sure, you want to delete " + this.axle_code + "?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, delete it!",
      }).then((result) => {
        if (result.value) {
          axios
            .delete(window.$http + `AxleWeightConfig/` + this.id, {
              headers: window.$headers,
            })
            .then((response) => {
              // JSON responses are automatically parsed.
              console.log(response.data);
              this.deleteaxle_config = true;
              this.orderData.splice(index, 1);
              Swal.fire(
                this.axle_code + " Deleted!",
                "Your record has been deleted.",
                "success"
              );
            })
            .catch((e) => {
              Swal.fire("" + e);
            });
        }
      });
    },
    clearvalues() {
      this.name = "";
      this.editmode = false;
      this.modaltitle = "Add AxleWeightConfig";
    },
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <div ref="content">
      <div class="card">
        <div class="card-body">
          <form @submit.prevent="handleSubmit">
            <div class="card">
              <div class="card-body">
                <div class="row justify-content-between">
                  <div class="col-sm-6">
                    <button
                      class="
                        btn btn-primary
                        waves-effect waves-light
                        uil-export
                      "
                      @click="getrpt(p_excel)"
                    >
                      Export to CSV
                    </button>
                  </div>

                  <div class="col-sm-4">
                    <button
                      @click="printpdf('p', p_pdf)"
                      v-b-modal.modal-Print
                      class="
                        btn btn-primary
                        waves-effect waves-light
                        mdi-file-pdf
                      "
                    >
                      Print PDF
                    </button>
                  </div>
                  <div class="col-sm-2">
                    <button
                      v-b-modal.modal-Edit
                      class="
                        btn btn-primary
                        waves-effect waves-light
                        uil-focus-add
                      "
                      @click="clearvalues()"
                    >
                      Add {{ title }}
                    </button>
                  </div>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col-sm-12">
                <div class="card">
                  <div class="card-body changebg">
                    <div class="row" id="print">
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
                            <template v-slot:cell(permissible)="data">
                              {{ getpermgroupaxle(data.item.axle_code) }}
                            </template>
                            <template v-slot:cell(Permissiblegvw)="data">
                              {{ getpermgvw(data.item.axle_code) }}
                            </template>
                            <template v-slot:cell(Permissibleaxle)="data">
                              {{ getpermaxle(data.item.axle_code) }}
                            </template>
                            <template v-slot:cell(action)="data">
                              <ul class="list-inline mb-0">
                                <li class="list-inline-item">
                                  <a
                                    href="javascript:void(0);"
                                    class="px-2 text-primary"
                                    v-b-tooltip.hover
                                    title="Edit"
                                    v-b-modal.modal-Edit
                                    @click="
                                      edit(
                                        data.index,
                                        data.item.id,
                                        data.item.axle_code,
                                        data.item.Permissible,
                                        getpermgroupaxle(data.item.axle_code),
                                        getpermaxle(data.item.axle_code),
                                        getpermgvw(data.item.axle_code),
                                        getpermaxleids(data.item.axle_code)
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
                                        data.index,
                                        data.item.id,
                                        data.item.axle_code,
                                        data.item.Permissible,
                                        data.item.Permissibleaxle,
                                        data.item.Permissiblegvw
                                      )
                                    "
                                  >
                                    <i
                                      class="uil uil-trash-alt font-size-18"
                                    ></i>
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
    </div>
    <b-modal id="modal-Edit" :title="modaltitle" hide-footer size="lg" centered>
      <div class="row">
        <div class="col-md-12">
          <div class="card">
            <div class="card-body">
              <form @submit.prevent="handleSubmit">
                <div class="row">
                  <div class="input-group">
                    <div v-show="editmode" class="input-group-text col-sm-4">
                      IDs:<span v-bind="axle">{{ axleids }}</span>
                    </div>

                    <input
                      v-show="editmode"
                      class="form-control"
                      type="text"
                      disabled="disabled"
                      placeholder="id"
                      v-model="id"
                    />
                  </div>
                  <div class="input-group">
                    <div class="input-group-text col-sm-4">
                      Axle Configuration Name:
                    </div>
                    <input
                      class="form-control"
                      type="text"
                      placeholder="Axle Configuration Name"
                      v-model="axle_code"
                      :disabled="editmode"
                    />
                  </div>
                  <div class="input-group">
                    <div class="input-group-text col-sm-4">
                      Permissible Grouping Deck [A,B,C,D]:
                    </div>
                    <input
                      class="form-control"
                      type="text"
                      placeholder="[A,B,C,D]"
                      v-model="Permissiblegroupdeck"
                      :disabled="editmode"
                    />
                  </div>
                  <div class="input-group">
                    <div class="input-group-text col-sm-4">
                      Permissible Axle [KG]:
                    </div>
                    <div class="d-flex">
                      <span class="font-size-8 p-1 d-block"
                        ><input
                          v-for="(val, index) in Permissibleaxle"
                          :key="index"
                          style="width:50px"
                          type="text"
                          v-model="Permissibleaxle[index]"
                          id="axle_val"
                          @keyup="[calc_gvw(), calc_permgroupaxle()]"
                      /></span>
                    </div>
                    <!-- <b-button
                      class="badge badge-pill bg-success round m-auto p-1 m-1"
                      @click="calc_gvw()"
                      >verify</b-button
                    > -->
                  </div>
                  <div class="input-group">
                    <div class="input-group-text col-sm-4">
                      Permissible GVW [KG]:
                    </div>
                    <input
                      class="form-control"
                      type="text"
                      placeholder="NPermissible GVW [KG]ame"
                      v-model="Permissiblegvw"
                      :disabled="editmode"
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
        <div class="col-sm-9 mb-2">
          <h4>
            GVW:<span
              v-bind="Permissiblegvw"
              class="my-4 text-success font-weight-bold"
              :class="{ 'text-danger': Number(Permissiblegvw) > 56000 }"
            >
              {{ Permissiblegvw }}&nbsp;<i
                class="fa fa-check text-success"
                :class="{ 'd-none': Number(Permissiblegvw) > 56000 }"
              ></i></span
            >&nbsp;<span
              class="text-danger  fa fa-exclamation-circle"
              :class="{ 'd-none': Number(Permissiblegvw) < 56000 }"
              >Total Weight Exceeds 56T</span
            >
          </h4>
          <br/>
          <h4>
            Deck Grouping:<span
              v-bind="Permissiblegroupdeck"
              class="my-4 text-success font-weight-bold"
              :class="{ 'text-danger': Number(Permissiblegvw) > 56000 }"
            >
              {{ Permissiblegroupdeck }}&nbsp;<i
                class="fa fa-check text-success"
                :class="{ 'd-none': Number(Permissiblegvw) > 56000 }"
              ></i></span
            >
          </h4>
        </div>
        <div class="col-sm-3 mb-2">
          <b-button v-show="!editmode" variant="dark" @click="position()"
            >Add {{ title }}</b-button
          >
          <b-button v-show="editmode" variant="dark" @click="editpermaxle()"
            >Edit {{ title }}</b-button
          >
        </div>
      </div>
    </b-modal>
    <b-modal id="modal-Print" title="Print PDF" hide-footer size="bg" centered>
      <reportdet
        :title="title"
        :orderData="orderData"
        :pl="pl"
        :headers="headers"
        :uniqueCars="uniqueCars"
        :shome="showme"
        :printedpdf="printedpdf"
        v-show="showme"
      ></reportdet>
    </b-modal>
  </Layout>
</template>
//#print { //height: 11in; //width: 8.5in; //}
<style scoped>
.changebg {
  background-color: #f7f6ebfb;
}
</style>