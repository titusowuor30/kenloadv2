<script>
import Swal from "sweetalert2";
import axios from "axios";
//import html2canvas from "html2canvas";
//import rptheader from "@/components/report/header.js";
import reportdet from "@/components/report/header";
import Multiselect from "vue-multiselect";
import DatePicker from "vue2-datepicker";

export default {
  components: {
    reportdet,
    Multiselect,
    DatePicker,
  },
  props: {
    nplate: String,
    cat: String,
    mode: Boolean,
    startdatetime: String,
    WBTNO: String,
    nodata: String,
    inyard: Boolean,
    vehicles: Array,
    reweighno: Number,
    yard: Array,
  },
  data() {
    return {
      category: [],
      categories: [],
      user: "",
      users: [],
      statuses: ["Closed", "Open"],
      status: [],
      fromdate: "",
      todate: "",
      tohour: "23:59",
      fromhour: "00:00",
      thours: [
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
      title: "Tags",
      mymode: false,
      link: "vehicledetails",
      items: [
        {
          text: "USER: Admin",
        },
        {
          text: "Tag List for " + this.title,
          active: true,
        },
      ],
      search: false,
      orderDatanew: null,
      wb: [],
      wbs: [],
      cluster: [],
      pl: "",
      orderData: [],
      id: "",
      headers: null,
      uniqueCars: null,
      showme: true,
      totalRows: 1,
      currentPage: 1,
      perPage: 100,
      pageOptions: [
        1, 2, 5, 10, 25, 50, 100, 200, 300, 500, 1000, 2000, 3000, 4000, 5000,
        6000, 9000, 10000, 20000,
      ],
      roadauthoritys: ["KeNHA", "KURA"],
      filter: null,
      filterOn: [],
      sortBy: "id",
      sortDesc: true,
      name: "",
      roadauthority: "",
      prefix: "",
      myindex: "",
      editmode: false,
      modaltitle: "Access Control (TAG's)",
      fields: [
        {
          key: "id",
          label: "#",
          sortable: true,
        },
        {
          key: "vehreg",
          label: "Registration",
          sortable: true,
        },
        {
          key: "station",
          label: "Station",
          sortable: true,
        },
        {
          key: "datetime",
          label: "Date",
          sortable: true,
        },
        {
          key: "type",
          label: "Category",
          sortable: true,
        },
        {
          key: "reason",
          label: "Reason",
          sortable: true,
        },
        {
          key: "userid",
          label: "Tagged By",
          sortable: true,
        },
        {
          key: "status",
          label: "Status",
          sortable: true,
        },
        {
          key: "removedby",
          label: "Closed By",
          sortable: true,
        },
        {
          key: "removeddatetime",
          label: "Date Close",
          sortable: true,
        },
        {
          key: "removedreason",
          label: "Reason Close",
          sortable: true,
        },

        "action",
      ],
    };
  },
  computed: {
    /**
     * Total no. of records
     */
    rows() {
      return this.orderData.length;
    },
  },
  created() {
    // Set the initial number of items
    //this.focusInput();
    //this.totalRows = this.items.length;
    //alert(this.nplate);
    this.fromdate = new Date();
    this.todate = new Date();
    this.fromdate.setDate(this.fromdate.getDate() - 3000);
    this.todate.setDate(this.todate.getDate() + 1);
    this.updatearray();
    this.filter = this.nplate;
    this.mymode = this.mode;
    this.categories = ["Manual", "Automatic"];
    axios
      .get(window.$http + `ClusterWeighbridges`, {
        headers: window.$headers,
      })
      .then((response) => {
        this.cluster = response.data;

        response.data.forEach((e) => {
          this.wbs.push(e.cluster_name);
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

    axios
      .get(window.$http + `users`, { headers: window.$headers })
      .then((response) => {
        // JSON responses are automatically parsed.
        // this.cargos = response.data.cargo_name;
        response.data.forEach((e) => {
          this.users.push(e.email);
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
  methods: {
    getrec() {
      this.search = true;
      this.updatearray();
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

      var get = `tag/search?offset=` + 0 + `&limit=` + this.perPage;

      try {
        this.nplate.trim();
      } catch (e) {
        //alert("e " + e);
        this.nplate = "";
      }

      var station = [];
      try {
        station = this.wb;
      } catch (e) {
        station = [];
        //this.station = [];
      }

      var ampm = "AM";
      var ampm2 = "AM";
      var datefil = "";

      if (Number(this.fromhour.substring(0, 2)) > 12) {
        ampm = "PM";
      }

      if (Number(this.tohour.substring(0, 2)) > 12) {
        ampm2 = "PM";
      }
      var fromdate =
        this.getmydate(this.fromdate) +
        " " +
        this.fromhour +
        ":00 " +
        ampm +
        "";
      var todate =
        this.getmydate(this.todate) + " " + this.tohour + ":59 " + ampm2 + "";
      //alert(fromdate);
      if (fromdate.length > 0) {
        datefil = "&fromdate=" + fromdate + "&todate=" + todate;
      }

      var stationfilter = "";
      if (station.length > 0) {
        for (var r = 0; r < station.length; r++) {
          stationfilter += "" + this.getStationcode(station[r]) + ",";
        }

        stationfilter =
          "&station=" + stationfilter.substring(0, stationfilter.length - 1);
      }

      if (this.user != "") {
        stationfilter += "&user=" + this.user;
      }

      var status = [];
      try {
        status = this.status;
      } catch (e) {
        status = [];
        //this.station = [];
      }
      var statusfilter = "";

      if (status.length > 0) {
        for (r = 0; r < status.length; r++) {
          if (status[r] == "Closed") {
            statusfilter = "1";
          }
          if (status[r] == "Open") {
            statusfilter = "0";
          }
        }
        if (status.length < 2) {
          stationfilter += "&status=" + statusfilter;
        }
      }

      var category = [];
      try {
        category = this.category;
      } catch (e) {
        category = [];
        //this.station = [];
      }
      var categoryfilter = "";

      if (category.length > 0) {
        for (r = 0; r < category.length; r++) {
          if (category[r] == "Manual") {
            categoryfilter += "M,";
          }
          if (category[r] == "Automatic") {
            categoryfilter += "A,";
          }
        }

        stationfilter +=
          "&type=" + categoryfilter.substring(0, categoryfilter.length - 1);
      }

      if (this.mode) {
        get =
          "tag/search?vehreg=" +
          this.nplate +
          `&offset=` +
          0 +
          `&status=0&limit=` +
          this.perPage;
      }
      if (this.search) {
        get =
          "tag/search?vehreg=" +
          this.filter +
          `&limit=` +
          this.perPage +
          stationfilter +
          datefil;
      }
      //alert(window.$http + get);
      axios
        .get(window.$http + get, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.

          this.orderData = response.data;

          if (this.mode) {
            this.orderData = response.data;
            console.log(this.orderData);
          }

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
    getStationcode(name) {
      var code = "";
      //alert(this.cluster.length);
      var stat = this.cluster;
      for (var i = 0; i < stat.length; i++) {
        if (name == stat[i].cluster_name) {
          code = stat[i].cluster_code;
        }
      }
      return code;
    },
    /**
     * Export to CSV
     */
    getrpt() {
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
        ID: row.id,
        Cargo_Name: row.name,
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
    },
    handleSubmit() {
      console.log("Error on submit");
    },
    printpdf(pl) {
      this.pl = pl;
      const data = this.orderData.map((row) => ({
        ID: row.id,
        Cargo_Name: row.name,
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
    edit(index, myid, name) {
      //alert(rolename);
      this.editmode = true;
      this.modaltitle = "Edit Cargo";
      this.id = myid;
      this.name = name;
      this.myindex = index;
      this.focusInput();

      //this.$refs.search.focus();
      //$("#search").focus();
    },
    editrec() {
      if (this.name.trim() == "") {
        Swal.fire("Please enter  Cargo Name!");
        return;
      }

      //alert(this.myindex);
      this.orderData[this.myindex].id = this.id;
      this.orderData[this.myindex].name = this.name;
      this.orderData[this.myindex].prefix = this.prefix;
      Swal.fire({
        position: "center",
        icon: "success",
        title: "Your work has been saved",
        showConfirmButton: false,
        timer: 1500,
      });
    },
    position() {
      if (this.name.trim() == "") {
        Swal.fire("Please enter  Cargo Name!");
        return;
      }

      var orderid = this.orderData.length + 1;
      this.orderData.push({
        id: orderid,
        name: this.name,
      });
      this.name = "";
      Swal.fire({
        position: "center",
        icon: "success",
        title: "Your work has been saved",
        showConfirmButton: false,
        timer: 1500,
      });
    },
    deleterec(index, id, name) {
      //alert(rolename);
      this.id = id;
      this.name = name;
      Swal.fire({
        title: "Are you sure, you want to delete " + this.name + "?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, delete it!",
      }).then((result) => {
        if (result.value) {
          this.orderData.splice(index, 1);
          //this.$delete(this.orderData, this.id - 1);
          Swal.fire("Deleted!", this.name + " has been deleted.", "success");
        }
      });
    },
    clearvalues() {
      this.name = "";
      this.editmode = false;
      this.modaltitle = "Add Cargo";
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
    async removetag(id, vehreg, reason, station, datat) {
      if (station != window.$stationcode) {
        Swal.fire(
          "Tag does not belong to this station. It belongs to " + station
        );
        return;
      }

      var rea = "";
      const { value: text } = await Swal.fire({
        title:
          "Do you want to Close Tag for " + vehreg + " \n " + reason + " ?",
        input: "textarea",
        inputLabel: "Enter reason:",
        inputPlaceholder: "Enter reason here...",
        inputAttributes: {
          "aria-label": "Enter reason here",
        },
        showCancelButton: true,
      });
      try {
        rea = text;
      } catch (e) {
        rea = "";
      }
      //alert(rea.length);

      if (rea.length <= 0) {
        Swal.fire("Please enter reason");
        return;
      }

      //alert(this.mdate + " " + this.hour);
      var datet = this.getcurrentdate();
      datat.removeddatetime = datet;
      datat.removedby = window.$userid;
      datat.status = 1;
      datat.removedreason = rea;
      datat.exported = "M";
      // const data = {
      //   id: id,
      //   removeddatetime: datet,
      //   removedby: this.username,
      //   status: 1,
      //   removedreason: rea,
      // };
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
        .put(window.$http + `tag/` + id, datat, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          console.log(response.data);

          var data = {
            events: "Tag Closed for " + vehreg,
            datetime: this.getcurrentdate(),
            userid: window.$userid,
            desc:
              this.nplate +
              " GVW: " +
              this.totalweight +
              " for ticket " +
              this.WBTNO,
          };

          axios
            .post(window.$http + `events/`, data, { headers: window.$headers })
            .then(() => {
              Swal.fire(
                "Cancelled Weighing!",
                "Cancelled weighing " + this.nplate + " ",
                "success"
              );
              //this.$router.push("/weigh/" + this.cat);
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
          }).then(() => {
            Swal.close();
          });
        });
    },
    Weighing() {
      //alert("");
      Swal.fire({
        title: "Are you sure, you want to cancel weighing " + this.nplate + "?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#000000",
        cancelButtonColor: "#f46a6a",
        confirmButtonText: "Yes, Cancel it!",
      }).then((result) => {
        if (result.value) {
          Swal.fire(
            "Cancelled Weighing!",
            this.nplate + " cancelled weighing.",
            "success"
          );
          this.$router.push({
            name: "/weigh/" + this.cat,
            params: { nplate: this.nplate, cat: this.cat },
          });
        }
      });
    },
    proceed() {
      this.$router.push({
        name: "/weigh/" + this.cat,
        params: { nplate: this.nplate, cat: this.cat },
      });

      this.$router.push({
        name: "vehicledetails",
        params: {
          nplate: this.nplate,
          cat: this.cat,
          startdatetime: this.startdatetime,
          WBTNO: this.WBTNO,
          nodata: this.nodata,
          inyard: this.inyard,
          vehicles: this.vehicles,
          reweighno: this.reweighno,
          yard: this.yard,
          tag: true,
        },
      });
    },
  },
};
</script>

<template>
  <div>
    <div class="d-flex flex-row-reverse">
      <div class="d-flex" v-show="mode">
        <b-button v-show="mode" @click="proceed()" variant="dark">
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Next >>&nbsp;&nbsp;&nbsp;
        </b-button>
      </div>
    </div>
    <div class="card">
      <div class="card-body">
        <form @submit.prevent="handleSubmit">
          <div class="row">
            <div class="card-body changebg">
              <div class="row" id="print">
                <div class="col-12">
                  <div></div>

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
                      <div class="col-sm-1">
                        <div v-show="!mode">
                          <label
                            v-show="!mode"
                            class="d-inline-flex align-items-center fw-normal"
                          >
                            Show&nbsp;
                          </label>
                        </div>
                        <div v-show="!mode">
                          <b-form-select
                            v-show="!mode"
                            @change="updatearray()"
                            v-model="perPage"
                            size="sm"
                            :options="pageOptions"
                          ></b-form-select
                          >&nbsp;entries
                        </div>
                      </div>

                      <div class="col-sm-1">
                        <span v-show="!mode">Date From:</span>
                        <date-picker
                          v-show="!mode"
                          @change="getrec()"
                          value="2021-05-17"
                          v-model="fromdate"
                          :first-day-of-week="1"
                          lang="en"
                        ></date-picker>
                      </div>
                      <div class="col-sm-1">
                        <span v-show="!mode">Time From:</span>

                        <multiselect
                          v-show="!mode"
                          v-model="fromhour"
                          :options="thours"
                          placeholder="00:00"
                          :allow-empty="false"
                        ></multiselect>
                      </div>
                      <div class="col-sm-1">
                        <span v-show="!mode">To:</span>
                        <date-picker
                          v-show="!mode"
                          @change="getrec()"
                          value="2021-05-17"
                          v-model="todate"
                          :first-day-of-week="1"
                          lang="en"
                        ></date-picker>
                      </div>
                      <div class="col-sm-1">
                        <span v-show="!mode">Time To:</span>

                        <multiselect
                          v-show="!mode"
                          v-model="tohour"
                          :options="thours"
                          placeholder="00:00"
                          :allow-empty="false"
                        ></multiselect>
                      </div>

                      <!-- Station-->

                      <div class="col-sm-2">
                        <div class="row col-md-12">
                          <span v-show="!mode">
                            <i class="uil uil-eye"></i>
                            <label
                              v-show="!mode"
                              class="d-inline-flex align-items-center fw-normal"
                            >
                              Station
                            </label>
                          </span>
                          <multiselect
                            v-show="!mode"
                            @input="getrec()"
                            v-model="wb"
                            :options="wbs"
                            placeholder="All"
                            :multiple="true"
                          ></multiselect>
                        </div>
                      </div>

                      <!-- USer-->

                      <div class="col-sm-2">
                        <div class="row col-md-12">
                          <span v-show="!mode">
                            <i class="uil uil-eye"></i>
                            <label
                              class="d-inline-flex align-items-center fw-normal"
                            >
                              Tagged By
                            </label>
                          </span>

                          <b-form-input
                            v-show="!mode"
                            v-model="user"
                            type="text"
                            class="form-control form-control-sm ms-2"
                          ></b-form-input>
                        </div>
                      </div>

                      <!-- Status-->

                      <div class="col-sm-1">
                        <div class="row col-md-12">
                          <span v-show="!mode">
                            <label
                              class="d-inline-flex align-items-center fw-normal"
                            >
                              Status
                            </label>
                          </span>
                          <multiselect
                            v-show="!mode"
                            @input="getrec()"
                            v-model="status"
                            :options="statuses"
                            placeholder="All"
                            :multiple="true"
                          ></multiselect>
                        </div>
                      </div>

                      <!-- Category-->

                      <div class="col-sm-1">
                        <div class="row col-md-12">
                          <span v-show="!mode">
                            <label
                              class="d-inline-flex align-items-center fw-normal"
                            >
                              Category
                            </label>
                          </span>
                          <multiselect
                            v-show="!mode"
                            @input="getrec()"
                            v-model="category"
                            :options="categories"
                            placeholder="All"
                            :multiple="true"
                          ></multiselect>
                        </div>
                      </div>

                      <!-- Search -->
                      <div class="col-sm-2">
                        <div
                          id="tickets-table_filter"
                          class="dataTables_filter text-md-end"
                          v-show="!mode"
                        >
                          <label
                            class="d-inline-flex align-items-center fw-normal"
                          >
                            Search:
                          </label>
                          <b-form-input
                            v-show="!mode"
                            v-model="filter"
                            type="search"
                            placeholder="Search..."
                            class="form-control form-control-sm ms-2"
                          ></b-form-input>

                          <b-button
                            v-show="!mode"
                            variant="dark"
                            class="uil-search"
                            @click="getrec()"
                            >Search</b-button
                          >
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
                      <template v-slot:cell(screens)="data">
                        <a
                          href="javascript: void(0);"
                          class="text-dark fw-bold"
                          v-for="screen in data.item.screens"
                          :key="screen"
                          >{{ screen }} <br
                        /></a>
                      </template>
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
                          <template v-slot:cell(status)="data">
                            <div
                              class="badge bg-pill bg-soft-success font-size-12"
                              :class="{
                                'bg-soft-danger':
                                  data.item.status === 'Chargeback',
                                'bg-soft-warning':
                                  data.item.status === 'unpaid',
                              }"
                            >
                              {{ data.item.status }}
                            </div>
                          </template>
                          --->
                      <template v-slot:cell(status)="data">
                        <div
                          class="badge bg-pill bg-soft-success font-size-12"
                          :class="{
                            'bg-soft-danger': data.item.status == '0',
                            'bg-soft-success': data.item.status == '1',
                          }"
                        >
                          {{ data.item.status == 0 ? "Open" : "Closed" }}
                        </div>
                      </template>

                      <template v-slot:cell(type)="data">
                        <div
                          class="badge bg-pill bg-soft-success font-size-12"
                          :class="{
                            'bg-soft-danger':
                              data.item.type === 'M' && data.item.status == '0',
                            'bg-soft-info': data.item.type === 'A',
                          }"
                        >
                          {{
                            data.item.type === "M"
                              ? "Manual"
                              : data.item.type === "A"
                              ? "Automatic"
                              : ""
                          }}
                        </div>
                      </template>

                      <template v-slot:cell(action)="data">
                        <ul class="list-inline mb-0">
                          <li class="list-inline-item">
                            <button
                              v-show="data.item.status == 0 ? !mode : false"
                              @click="
                                removetag(
                                  data.item.id,
                                  data.item.vehreg,
                                  data.item.reason,
                                  data.item.station,
                                  data.item
                                )
                              "
                              style="
                                color: yellow;
                                background-color: black;
                                border-radius: 5px;
                              "
                            >
                              <i class="uil uil-eye me-2"></i>
                              Close Tag
                            </button>
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
                      <b-modal
                        id="modal-Print"
                        title="Print PDF"
                        hide-footer
                        size="bg"
                        centered
                      >
                        <reportdet
                          :title="title"
                          :orderData="orderData"
                          :pl="pl"
                          :headers="headers"
                          :uniqueCars="uniqueCars"
                          :shome="showme"
                          v-show="showme"
                        ></reportdet>
                      </b-modal>
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
</template>
//#print {
  //height: 11in;
  //width: 8.5in;
//}
<style scoped>
.changebg {
  background-color: #f7f6ebfb;
}
</style>