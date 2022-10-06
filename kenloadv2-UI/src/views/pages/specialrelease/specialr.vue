<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import axios from "axios";
import Swal from "sweetalert2";
import jsPDF from "jspdf";

//import { tableData } from "./dataAdvancedtable";

export default {
  page: {
    title: "Special Released",
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
    //tableData,
  },
  data() {
    return {
      title: "Special Released",
      items: [
        {
          text: "USER:",
        },
        {
          text: "Special Release",
          active: true,
        },
      ],
      yardlist: [],
      excessload: 0,
      description: "",
      reason: "",
      autoClose: "",
      shift: null,
      route: null,
      hour: null,
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
      orderData: [],
      totalRows: 1,
      viewSprdoc: false,
      currentPage: 1,
      casedetailsid: "",
      perPage: 50,
      pageOptions: [
        1, 2, 5, 10, 25, 50, 100, 200, 300, 500, 1000, 2000, 3000, 4000, 5000,
        6000, 9000, 10000, 20000,
      ],
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
          key: "specialid",
          label: "Serial",
          sortable: true,
        },
        {
          key: "weighbridge",
          label: "Station",
          sortable: true,
        },
        {
          key: "regNo",
          label: "Registration  ",
          sortable: true,
        },
        {
          key: "ticketno",
          label: "wbrg_ticket_no",
          sortable: true,
        },
        {
          key: "cargo",
          label: "Cargo",
          sortable: true,
        },
        {
          key: "transporter",
          label: "Transporter",
          sortable: true,
        },
        {
          key: "reason",
          label: "Reason",
          sortable: true,
        },
        {
          key: "description",
          label: "Description",
          sortable: true,
        },
        {
          key: "excessload",
          label: "Excess Load [KG]",
          sortable: true,
        },
        {
          key: "weighdate",
          label: "Weigh Date",
          sortable: true,
        },
        {
          key: "realeasedate",
          label: "Realease Date",
          sortable: true,
        },
        {
          key: "weighedby",
          label: "Weighed By",
          sortable: true,
        },
        {
          key: "createdby",
          label: "Realeased By",
          sortable: true,
        },
        {
          key: "release",
          label: "View  ",
        },
      ],
    };
  },
  watch: {
    viewSprdoc(newValue) {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Opened Special release:${newValue}`,
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
    this.items.text = "User:" + window.$userid;
    this.updatearray();
    this.totalRows = this.items.length;
    this.viewSprdoc = true;
  },
  methods: {
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
        .get(window.$http + `SpecialRelease/reports?limit=` + this.perPage, {
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
    updatecasedocs(casedocid, caseid, doc, casedetailsid) {
      var data = {
        casedocid: casedocid,
        caseid: caseid,
        docname: doc,
        docdate: this.getcurrentdate(),
        createdby: window.$userid,
        casedetailsid: casedetailsid,
        cancelled: "N",
        printed: "N",
      };
      //alert("Ncasedocsid" + Ncasedocsid + "  casedetailsid" + this.id);
      axios
        .post(window.$http + `casedocs/`, data, { headers: window.$headers })
        .then((response) => {
          // JSON responses are automatically parsed.
          Swal.close();
          //this.$root.$emit("bv::hide::modal", "modal-Doc");
          Swal.fire({
            position: "center",
            icon: "success",
            title: response.data + ". Special Release Created! " + doc,
            showConfirmButton: false,
            timer: 1500,
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
    },
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
    /**
     * Load correction memo
     */
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
    getv(val) {
      if (val < 10) {
        val = "0" + val;
      }
      return val;
    },
    padLeadingZeros(num, size) {
      var s = num + "";
      while (s.length < size) s = "0" + s;
      return s;
    },
    updatelist(yardlist) {
      this.yardlist = yardlist;

      axios
        .get(
          window.$http +
            `weighbridgetransactions/search?limit=1&wbrg_ticket_no=` +
            this.yardlist.wbtno,
          { headers: window.$headers }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          console.log(response);
          this.excessload = response.data[0].wbrg_ticket_excessweight;
          //alert(this.excessload);
        });

      axios
        .get(
          window.$http + `casedetails/search?caseid=` + this.yardlist.caseid,
          { headers: window.$headers }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          //console.log(response);
          this.casedetailsid = response.data[0].id;
          //alert(response.data[0].casedocs[0]);
          var dd = response.data[0].casedocs.length;

          if (dd == 0) {
            Swal.fire({
              position: "center",
              icon: "error",
              title: "Prohibition document not raise ",
              showConfirmButton: false,
              timer: 1500,
            }).then(() => {
              this.$root.$emit("bv::hide::modal", "modal-1");
            });
          } else {
            this.$root.$emit("bv::show::modal", "modal-1");
          }
        });
    },
    view(specialReleaseNo) {
      //this.yardlist = yardlist;
      //alert(window.$http + `casedetails/search?caseid=` + this.yardlist.caseid);
      axios
        .get(
          window.$http + `specialrelease/report?specialid=` + specialReleaseNo,
          { headers: window.$headers }
        )
        .then((response) => {
          // JSON responses are automatically parsed.
          console.log(response);
          // alert(response.data[0].specialid);
          this.getspecial(response.data[0]);
        });
    },
    goToSrdoc() {
      //this.$router.push("/srdoc");
      var mycasid = "SP";
      var doc = "Special Release";
      // alert(
      //   window.$http +
      //     `specialrelease/max?specialid=` +
      //     mycasid +
      //     window.$stationcode +
      //     this.ticketdate()
      // );
      axios
        .get(
          window.$http +
            `specialrelease/max?specialid=` +
            mycasid +
            window.$stationcode +
            this.ticketdate(),
          { headers: window.$headers }
        )
        .then((response) => {
          //alert(response.data);
          let ticketmax = Number(response.data);
          ticketmax++;
          var ticketno = this.padLeadingZeros(ticketmax, 6);
          var Ncasedocsid =
            mycasid + window.$stationcode + this.ticketdate() + ticketno;
          var data = {
            specialid: Ncasedocsid,
            vehiclereg: this.yardlist.vehreg,
            reason: this.reason,
            otherreason: this.description,
            wbrg_ticket_no: this.yardlist.wbtno,
            realeasedate: this.getcurrentdate(),
            realeasedby: window.$userid,
            excessload: this.excessload,
            case_document_id: this.yardlist.caseid,
          };

          axios
            .post(window.$http + `specialrelease/`, data, {
              headers: window.$headers,
            })
            .then(() => {
              // JSON responses are automatically parsed.
              this.yardlist.spreleaseno = Ncasedocsid;
              axios
                .put(
                  window.$http + `yardlist/` + this.yardlist.id,
                  this.yardlist,
                  { headers: window.$headers }
                )
                .then(() => {
                  //saveticket();
                  this.updatecasedocs(Ncasedocsid);
                  this.updatecasedocs(
                    Ncasedocsid,
                    this.yardlist.caseid,
                    doc,
                    this.casedetailsid
                  );
                  Swal.close();
                  this.$root.$emit("bv::hide::modal", "modal-1");
                  Swal.fire({
                    position: "center",
                    icon: "success",
                    title: " Special Release ! " + Ncasedocsid,
                    showConfirmButton: false,
                    timer: 1500,
                  }).then(() => {
                    this.updatearray();
                  });
                })
                .catch((e) => {
                  Swal.fire({
                    position: "center",
                    icon: "error",
                    title: "Yard list " + e,
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
                title: "Special Release Document " + e,
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
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />

    <form @submit.prevent="handleSubmit">
      <div class="row">
        <div class="col-sm-12">
          <div class="card">
            <div class="card-body">
              <div class="card changebg">
                <div class="card-body">
                  <b-card-title>
                    <h5 class="card-title"></h5>
                  </b-card-title>

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
                                  @change="updatearray()"
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
                          <template v-slot:cell(release)="data">
                            <div class="button">
                              <input
                                type="button"
                                variant="secondary"
                                class="btn btn-primary waves-effect waves-light"
                                :id="`contacusercheck${data.item.id}`"
                                value="View"
                                @click="view(data.item.specialid)"
                              />
                            </div>
                          </template>

                          <template v-slot:cell(specialrelease)="data">
                            <a
                              href="javascript: void(0);"
                              class="text-dark fw-bold"
                              >{{
                                data.item.specialrelease == "" ? "Yes" : "No"
                              }}</a
                            >
                          </template>
                          <template v-slot:cell(inspected)="data">
                            <a
                              href="javascript: void(0);"
                              class="text-dark fw-bold"
                              >{{ data.item.inspected == 1 ? "Yes" : "No" }}</a
                            >
                          </template>

                          <template v-slot:cell(registration)="data">
                            <a
                              href="#"
                              :click="{
                                'goToProhibition()':
                                  data.item.registration === 'KBA857V',
                                'goToProhibition()':
                                  data.item.registration === 'unpaid',
                              }"
                              class=""
                            >
                              {{ data.item.registration }}
                            </a>
                          </template>
                          <template v-slot:cell(action)="data">
                            <div
                              class="badge bg-pill bg-soft-success font-size-12"
                              :class="{
                                'bg-soft-danger':
                                  data.item.action === 'prohibited',
                                'bg-soft-warning':
                                  data.item.action === 'unpaid',
                              }"
                              :click="{
                                'goToProhibition()':
                                  data.item.action === 'prohibited',
                                'goToProhibition()':
                                  data.item.action === 'unpaid',
                              }"
                            >
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
        </div>
      </div>
    </form>
    <div class="row"></div>
    <b-modal
      id="modal-1"
      title="Special Release Reason"
      hide-footer
      size="xl"
      centered
    >
      <div class="card-body">
        <b-tabs content-class="p-3 text-muted">
          <b-tab active class="border-0">
            <div class="input-group">
              <div class="input-group-text col-sm-4">
                Reason for releasing or closing weigh record:
              </div>
              <select name="reason" id="reason" v-model="reason">
                <option value="livestock">Live stock</option>
                <option value="aoverload">
                  Minimum Overload of Axle Limit
                </option>
                <option value="goverload">
                  Minimum Overload of Gross Limit
                </option>
                <option value="others">Others</option>
                <option value="bus">Passenger bus</option>
                <option value="escaped">Vehicle escaped</option>
                <option value="nyard">Vehicle is Not In The Yard</option>
                <option value="wpermit">Vehicle With Permit</option>
              </select>
            </div>
            <div class="input-group">
              <div class="input-group-text col-sm-4">Description:</div>
              <input
                v-model="description"
                type="text"
                height="500px"
                class="form-control"
                placeholder="Description"
                id="description"
                name="description"
                required
              />
            </div>
          </b-tab>
        </b-tabs>

        <form>
          <div class="row">
            <div class="col-lg-12">
              <div class="text-end">
                <b-button v-on:click="goToSrdoc()"> Release Vehicle</b-button>
              </div>
            </div>
          </div>
        </form>
      </div>
    </b-modal>
  </Layout>
</template>
<style scoped>
.changebg {
  background-color: rgb(240, 240, 240);
}
</style>