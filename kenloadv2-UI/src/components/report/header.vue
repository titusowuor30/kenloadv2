<template>
  <div>
    <div class="row">
      <div class="col-md-12">
        <div class="card">
          <div class="card-body">
            <form @submit.prevent="handleSubmit">
              <div class="row">
                <div class="input-group-text col-sm-12">
                  Do you want to generate PDF document for {{ title }}
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
        <b-button
          variant="dark"
          @click="generatePDF('Excel'), $bvModal.hide('modal-1')"
          >Print Excel
        </b-button>
      </div>
      <div class="col-sm-3 mb-2">
        <b-button
          variant="dark"
          @click="generatePDF('pdf'), $bvModal.hide('modal-1')"
          >Print PDF
        </b-button>
      </div>
    </div>
  </div>
</template>

<script>
import jsPDF from "jspdf";
import autoTable from "jspdf-autotable";
import axios from "axios";

import Tahoma from "@/assets/fonts/tahoma.ttf";
import Swal from "sweetalert2";

export default {
  name: "report",
  props: {
    title: String,
    reportfor: String,
    rpt: String,
    orderData: Array,
    pl: String,
    headers: Object,
    uniqueCars: Object,
    showme: Boolean,
    printedpdf: Boolean,
  },
  methods: {
    makepdf() {
      //    alert("");
      //  window.html2canvas = html2canvas;
      var doc = new jsPDF("l", "pt", "a4");
      doc.html(document.querySelector("#print"), {
        callback: function (pdf) {
          pdf.save("make.pdf");
        },
      });
    },
    gen() {
      autoTable();
    },
    generatePDF(event) {
      //alert();
      const d = new Date();
      const year = d.getFullYear();
      const month = d.getMonth();
      const date = d.getDate();

      const hour = d.getHours();
      const min = d.getMinutes();
      const sec = d.getSeconds();
      const msec = d.getMilliseconds();
      //alert(this.printedpdf);
      // if (this.printedpdf == true) {
      var curentuser = window.$userid;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details:
          `Generated ` +
          event +
          ` reports for ${this.title} module(s) -> ${this.reportfor}`,
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
      // }

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

      var doc = new jsPDF(this.pl);

      doc.setFillColor(255, 255, 255);
      doc.roundedRect(5, 5, 200, 30, 1, 1, "FD");
      doc.setTextColor(0, 0, 0);

      doc.setFontSize(10);
      doc.addFont("Tahoma", "Tahoma", "Tahoma");
      doc.setFont("Tahoma");
      doc.text(60, 10, "KENYA NATIONAL HIGHWAYS AUTHORITY");
      doc.text(60, 15, "Quality Highways, Better Connections");
      doc.text(60, 20, "P.O. BOX 30417 - 00100 NAIROBI, KENYA");
      doc.text(60, 25, "Phone:0700 423606  | Email: dg@kenha.co.ke");
      doc.text(150, 30, "Print Date: " + this.getcurrentdate(new Date()));
      //doc.text(10, 40, this.title);
      doc.text(60, 33, "Weighbridge Name:" + window.$station);
      doc.setFontSize(14);
      doc.addFont("Tahoma", "Tahoma", "bold");

      const logo = require("@/assets/images/client.png");
      var imgLogo = new Image();
      imgLogo.src = logo;
      doc.addImage(imgLogo, "PNG", 10, 7, 40, 30);

      //doc.addImage(img, "PNG", 10, 10);
      doc.text(10, 40, this.title);

      doc.addFont("Tahoma", "Tahoma", "bold");
      doc.setFontSize(9);
      try {
        doc.text(10, 44, this.rpt);
      } catch (e) {
        console.log(e);
      }

      // Simple data example
      var head = [this.headers];
      var body = this.uniqueCars;

      if (event == "Excel") {
        //alert(this.orderData.length);
        if (this.orderData.length == 0) {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "No Data ",
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });
        }
        const csvRows = [];
        //get headers
        // var exceldata = [];
        // try {
        //   if (this.exceldata.length > 0) {
        //     this.orderData = this.exceldata;
        //   }
        // } catch (e) {
        //   exceldata = [];
        // }
        // if (exceldata.length == 0) {
        //   exceldata = this.orderData;
        // }
        const headers = Object.keys(this.orderData[0]);

        csvRows.push(headers.join(","));
        //alert(csvRows);
        //loop over the headers
        for (const row of this.orderData) {
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
      } else {
        //alert();
        doc.autoTable({
          head: head,
          body: body,
          //margin: { left: 5.5, top: 52 },
          startY: 44,
          margin: { horizontal: 1, left: 5.5, top: 0 },
          styles: {
            columnWidth: "wrap",
            fontSize: 6,
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
        // Simple html example
        //alert();
        // doc.autoTable({ html: "#table" });
        //alert(filename);
        //page numbering
        var height = 190;
        var width = 290;
        if (this.pl == "l") {
          height = 190;
          width = 200;
        }

        const pageCount = doc.internal.getNumberOfPages();
        for (var i = 1; i <= pageCount; i++) {
          doc.setPage(i);
          doc.text(
            "KeNHA/RD/M/WB/1886/2015                               Prepared by:" +
              window.$userid +
              "                                   Page " +
              String(i) +
              " of " +
              String(pageCount),
            height,
            width,
            null,
            null,
            "right"
          );
        }
        //this.showme = false;
        doc.save(this.title + "" + filename + ".pdf");
        doc.output("dataurlnewwindow");
        //this.$emit("close");
        this.$bvModal.hide("modal-Print");
      }
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
    getcurrentdate(mydate) {
      let d = new Date(mydate);
      let year = d.getFullYear();
      let month = this.getmonth(d.getMonth());
      let date = d.getDate();
      date = this.getv(date);
      //month = this.getv(month);

      let hour = d.getHours();
      let min = d.getMinutes();
      let sec = d.getSeconds();
      hour = this.getv(hour);
      min = this.getv(min);
      sec = this.getv(sec);

      //const msec = d.getMilliseconds();
      const datetime =
        date + "/" + month + "/" + year + " " + hour + ":" + min + ":" + sec;
      return datetime;
    },
    getv(val) {
      if (val < 10) {
        val = "0" + val;
      }
      return val;
    },
    async viewticket(nodata) {
      // alert(nodata.id);
      // var dat = {
      //     wbrg_ticket_no: this.WBTNO,
      //     axle_actualweight: orderData[i].actual,
      //     axle_permissibleweight: orderData[i].permissible,
      //     axle_time: this.weighdate,
      //     axle_number: axleno,
      //     axle_group: grp[i],
      //     axle_grouping: grp[i],
      //     Weighbridgetransactionsid: id,
      //     timestamp: this.getcurrentdate(),
      //   };
      var orderData = [];
      var orderData3 = [];
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      // var sp = [];
      // axios
      //   .get(
      //     window.$http + `specialrelease/search?wbtno=` + nodata.wbrg_ticket_no,
      //     {
      //       headers: window.$headers,
      //     }
      //   )
      //   .then((response) => {
      //     sp = response;
      //   })
      //   .catch((e) => {
      //     Swal.fire({
      //       position: "center",
      //       icon: "error",
      //       title: "Special Release Document " + e,
      //       showConfirmButton: true,
      //     }).then((e) => {
      //       Swal.close(e);
      //     });

      //     //throw e;
      //   });
      axios
        .get(
          window.$http + `users/search?email=` + nodata.wbrg_ticket_operator,
          {
            headers: window.$headers,
          }
        )
        .then((resp) => {
          var suser = resp.data;
          //alert(suser[0].fullnames);
          axios
            .get(window.$http + `axleweights/search?wbtno=` + nodata.id, {
              headers: window.$headers,
            })
            .then((res) => {
              //console.log(res);
              orderData = res.data;
              if (
                nodata.wbrg_ticket_type == "L" ||
                nodata.wbrg_ticket_type == "S"
              ) {
                orderData3 = res.data;
              }
              var weigbri = "";

              axios
                .get(
                  window.$http +
                    `ClusterWeighbridges/search?cluster_code=` +
                    nodata.wbrg_station,
                  {
                    headers: window.$headers,
                  }
                )
                .then((response) => {
                  weigbri = response.data[0].cluster_name;

                  var reweigh = [];
                  var status = "";
                  var state = "";
                  reweigh = nodata.wbrg_ticket_no.split("_");

                  if (nodata.wbrg_ticket_status == "P") {
                    status = "Incomplete";
                  }
                  if (nodata.wbrg_ticket_status == "O") {
                    status = "Overloaded";
                  }
                  if (nodata.wbrg_ticket_status == "L") {
                    status = "Legal";
                  }
                  if (nodata.wbrg_ticket_status == "W") {
                    status = "Warned";
                  }
                  if (nodata.wbrg_ticket_state == "S") {
                    state = "Special Release";
                  }
                  if (nodata.wbrg_ticket_state == "T") {
                    state = "To Redistribute";
                  }
                  if (nodata.wbrg_ticket_state == "R") {
                    state = "Redistribute";
                  }
                  if (nodata.wbrg_ticket_state == "C") {
                    state = "Charged";
                  }
                  //this.tno = this.prefix + "000000" + this.nplate;
                  //let pdfName = 'WeighbridgeReceipt';
                  var doc = new jsPDF();
                  //This is a key for printing
                  doc.addFileToVFS("tahoma.ttf", Tahoma);
                  doc.setFillColor(255, 255, 255);
                  doc.roundedRect(5, 5, 200, 38, 1, 1, "FD");

                  doc.setTextColor(0, 0, 0);
                  doc.setFontSize(12);
                  doc.setFont("Tahoma", "", "bold");
                  doc.text(60, 10, "KENYA NATIONAL HIGHWAYS AUTHORITY");
                  doc.text(70, 15, "Quality Highways, Better Connections");
                  doc.text(80, 25, "Axle Load Control");
                  doc.setFont("Tahoma", "", "normal");
                  doc.setFontSize(10);

                  doc.text(
                    150,
                    30,
                    "Print Date: " + this.getcurrentdate(new Date() + "")
                  );

                  doc.text(15, 40, "Weighbridge Ticket");
                  doc.text(70, 40, "Weighbridge Name:" + weigbri);

                  doc.addFont("Tahoma", "Tahoma", "bold");
                  doc.text(180, 35, state);
                  doc.setFont("Tahoma", "", "normal");
                  doc.setFontSize(12);
                  doc.text(180, 40, status);
                  //alert(this.tagged);

                  if (nodata.wbrg_ticket_tagged == 1) {
                    doc.setFontSize(8);
                    doc.text(180, 45, "Tagged");
                  }
                  //alert();

                  doc.setFontSize(10);
                  doc.text(5, 47, "Ticket No: " + nodata.wbrg_ticket_no);
                  //alert(reweigh[1]);
                  if (Number(reweigh[1]) > 0) {
                    doc.text(85, 47, "Re-Weighed:" + reweigh[1]);
                  }
                  if (nodata.reliefvehreg != "") {
                    doc.text(
                      70,
                      47,
                      "Relief Load from: " + nodata.reliefvehreg
                    );
                  }
                  doc.text(
                    135,
                    47,
                    "Weighed Date & Time:" +
                      this.getcurrentdate(nodata.wbrg_ticket_date)
                  );
                  doc.setFillColor(255, 255, 255);
                  var si = 70;

                  if (orderData.length == 3) {
                    si = 77;
                  }
                  if (orderData.length == 4) {
                    si = 82;
                  }
                  try {
                    if (orderData3.length >= 2) {
                      si = si + orderData3.length * 6;
                    }
                  } catch (e) {
                    console.log(e);
                  }
                  doc.roundedRect(5, 49, 200, si + 17, 0, 0, "FD");
                  //alert(orderData.length);

                  //doc.addImage(imgLogo.src, "PNG", 10, 10);
                  const logo = require("@/assets/images/client.png");
                  var imgLogo = new Image();
                  imgLogo.src = logo;
                  doc.addImage(imgLogo, "PNG", 10, 7, 40, 30);
                  var operator = suser[0].fullnames.toUpperCase();
                  // try {
                  //   var operator = await axios
                  //     .get(
                  //       window.$http +
                  //         `users/search?email=` +
                  //         nodata.wbrg_ticket_operator,
                  //       {
                  //         headers: window.$headers,
                  //       }
                  //     )
                  //     .then((response) => {
                  //       return response.data;
                  //     })
                  //     .catch(() => {});
                  // } catch (e) {
                  //   console.log(e);
                  // }
                  doc.text(
                    6,
                    53,
                    "Vehicle Registration No.: " + nodata.veh_reg
                  );
                  doc.addFont("bold");
                  doc.text(83.5, 53, "Transporter:" + nodata.wbrg_tx_code);
                  doc.text(160, 58, "Axle Type:" + nodata.wbrg_ticket_axel);
                  doc.text(
                    21.5,
                    58,
                    "Vehicle Make: " + nodata.wbrg_ticket_make
                  );
                  doc.text(
                    80,
                    58,
                    "Source/Origin:" + nodata.wbrg_ticket_source
                  );
                  doc.text(154, 62, "Scale Operator:");
                  doc.text(154, 66, operator);
                  doc.text(32, 62, "Driver: " + nodata.wbrg_ticket_driver);
                  doc.text(
                    84,
                    62,
                    "Destination:" + nodata.wbrg_ticket_destination
                  );
                  doc.text(
                    26.5,
                    66,
                    "Trailer No: " + nodata.wbgr_ticket_trailer_no
                  );
                  doc.text(91.5, 66, "Cargo:" + nodata.wbrg_product);
                  doc.line(5, 68, 205, 68);
                  doc.addFont("bold");
                  let lane1 = 81 - 8;
                  let lane2 = 85 - 8;
                  let lane3 = 87 - 8;
                  let lane4 = 91 - 8;
                  let inc = 6;
                  var str = "";
                  //var downline = 97;
                  let g = 1;
                  let axleoverl = 0;

                  if (
                    nodata.wbrg_ticket_type == "L" ||
                    nodata.wbrg_ticket_type == "S"
                  ) {
                    doc.setFont("Tahoma", "", "bold");
                    doc.text(10, lane1, "Item");
                    doc.text(25, lane1, "No of Axle Wheel");
                    doc.text(25, lane2, "Type and Distance");
                    doc.text(60, lane1, "Permissible");
                    doc.text(60, lane2, "Weight[KG]");
                    doc.text(88, lane1, "Permissible");
                    doc.text(88, lane2, "+Tolerance[KG]");
                    doc.text(120, lane1, "Actual");
                    doc.text(120, lane2, "Weight[KG]");
                    doc.text(140, lane1, "Overload");
                    doc.text(140, lane2, "[KG]");
                    doc.text(163, lane1, "Result");
                    doc.text(183, lane1, "PDF");
                    doc.setLineWidth(0.5);
                    doc.line(5, lane3, 205, lane3);
                    doc.text(85, lane4, "Single Axle Load");
                    doc.setLineWidth(0.5);
                    doc.setFont("Tahoma", "", "bold");

                    for (let i = 0; i < orderData3.length; i++) {
                      doc.line(5, lane3 + inc, 205, lane3 + inc);

                      doc.text(10, lane4 + inc, "Axle");
                      doc.text(25, lane4 + inc, g + "");

                      let leng = 0;
                      str = orderData3[i].permissible + "";
                      //alert(str.length);
                      leng = 8 - str.length;

                      doc.text(
                        58 + leng,
                        lane4 + inc,
                        "" + orderData3[i].permissible
                      );

                      doc.text(
                        84 + leng,
                        lane4 + inc,
                        "" + orderData3[i].permissible + ""
                      );
                      doc.text(120, lane4 + inc, "" + orderData3[i].actual);
                      doc.text(140, lane4 + inc, "" + orderData3[i].overload);
                      doc.text(163, lane4 + inc, "" + orderData3[i].result);
                      doc.text(183, lane4 + inc, "" + orderData3[i].pdf);
                      inc += 6;
                      axleoverl += Number(orderData3[i].overload);
                      g++;
                      doc.setLineWidth(0.1);
                    }
                    doc.setLineWidth(0.5);
                    doc.line(5, lane3 + inc, 205, lane3 + inc);

                    // alert(inc);
                    inc += g + 4;
                    lane1 += inc;
                    lane2 += inc;
                    lane3 += inc;
                    lane4 += inc;
                  }
                  doc.setFont("Tahoma", "", "bold");
                  doc.setFontSize(10);
                  doc.text(10, lane1, "Item");
                  doc.text(25, lane1, "No of Axle Wheel");
                  doc.text(25, lane2, "Type and Distance");
                  doc.text(60, lane1, "Permissible");
                  doc.text(60, lane2, "Weight[KG]");
                  doc.text(88, lane1, "Permissible");
                  doc.text(88, lane2, "+Tolerance[KG]");
                  doc.text(120, lane1, "Actual");
                  doc.text(120, lane2, "Weight[KG]");
                  doc.text(140, lane1, "Overload");
                  doc.text(140, lane2, "[KG]");
                  doc.text(163, lane1, "Result");
                  doc.text(183, lane1, "PDF");
                  doc.setLineWidth(0.5);
                  doc.line(5, lane3, 205, lane3);
                  doc.text(85, lane4, "Axle Group Load");
                  doc.setLineWidth(0.5);
                  doc.setFont("Tahoma", "", "normal");

                  inc = 6;
                  //var downline = 97;
                  g = 1;
                  axleoverl = 0;
                  var gvwperm = 0;
                  for (let i = 0; i < orderData.length; i++) {
                    doc.line(5, lane3 + inc, 205, lane3 + inc);

                    doc.text(10, lane4 + inc, "Group");
                    doc.text(25, lane4 + inc, g + "");

                    let leng = 0;
                    str = orderData[i].permissible + "";
                    //alert(str.length);
                    leng = 8 - str.length;

                    doc.text(
                      62 + leng,
                      lane4 + inc,
                      "" + orderData[i].axle_permissibleweight
                    );
                    var actual = Number(orderData[i].axle_actualweight);
                    var perm = Number(orderData[i].axle_permissibleweight);
                    var tollerance = (perm * 5) / 100 + perm;
                    doc.text(90 + leng, lane4 + inc, "" + tollerance + " 5%");
                    var overload = 0;
                    if (actual - tollerance > 0) {
                      overload = actual - tollerance;
                    }
                    var result = "Legal";
                    if (overload > 0) {
                      result = "Overload";
                    }
                    var pdf = "0";
                    let po = actual / perm;
                    pdf = Math.pow(4.5, po);
                    doc.text(
                      120,
                      lane4 + inc,
                      "" + orderData[i].axle_actualweight
                    );
                    doc.text(140, lane4 + inc, "" + overload);
                    doc.text(163, lane4 + inc, "" + result);
                    doc.text(
                      183,
                      lane4 + inc,
                      "" + new Intl.NumberFormat().format(pdf)
                    );
                    inc += 6;
                    axleoverl += Number(overload);
                    g++;
                    gvwperm += perm;
                    doc.setLineWidth(0.1);
                  }
                  inc -= 4;
                  doc.setLineWidth(1.0);
                  doc.line(5, lane4 + inc, 205, lane4 + inc);
                  doc.text(90, lane4 + inc + 4, "Vehicle Load");
                  doc.setLineWidth(0.5);
                  doc.line(5, lane4 + inc + 6, 205, lane4 + inc + 6);
                  doc.text(15, lane4 + inc + 10, "GVW");

                  doc.text(60, lane4 + inc + 10, gvwperm + "");
                  doc.text(88, lane4 + inc + 10, gvwperm + " 0%");
                  doc.text(
                    120,
                    lane4 + inc + 10,
                    "" + nodata.wbrg_ticket_grossweight
                  );
                  doc.text(
                    140,
                    lane4 + inc + 10,
                    nodata.wbrg_ticket_gvwload + ""
                  );
                  doc.text(
                    163,
                    lane4 + inc + 10,
                    nodata.wbrg_ticket_gvwload > 0 ? "Overload" : "Legal"
                  );
                  var gv = Number(nodata.wbrg_ticket_grossweight);

                  pdf = "0";
                  let po2 = gv / gvwperm;
                  pdf = Math.pow(4.5, po2);
                  doc.text(
                    183,
                    lane4 + inc + 10,
                    " " + new Intl.NumberFormat().format(pdf)
                  );
                  doc.setLineWidth(0.5);
                  doc.line(5, lane4 + inc + 12, 205, lane4 + inc + 12);
                  doc.text(
                    6,
                    lane4 + inc + 17,
                    "Legend:GVW:Gross Vehicle Weight, Wheels:S, Single Conventional Tyre D:Double,W Super Single (wide), Pavement Damage Factor"
                  );
                  doc.text(6, lane4 + inc + 21, "[PDF]");

                  doc.text(
                    5,
                    lane4 + inc + 27,
                    "Note: Axle group weights were checked, but axle weights were not checked. One or more axles in axle group can be overloaded even if the "
                  );

                  doc.text(
                    5,
                    lane4 + inc + 31,
                    "total weight of the axle group is reported as legal. It means the reonsibility of the owner to ensure that the vehicle is corecctly loaded."
                  );

                  doc.setFillColor(238, 238, 238);

                  doc.addFont("ComicSans", "Comic Sans", "bold");
                  doc.setFont("Tahoma");
                  doc.setFontSize(13);
                  if (axleoverl > nodata.wbrg_ticket_gvwload) {
                    doc.rect(5, lane4 + inc + 35, 200, 7, "F");
                    doc.text(
                      40,
                      lane4 + inc + 40,
                      "Excess Group Axle Weight.Remedial action required: Redistribute or offload " +
                        axleoverl +
                        "[KG]"
                    );
                  }
                  if (nodata.wbrg_ticket_gvwload > axleoverl) {
                    doc.rect(5, lane4 + inc + 35, 200, 7, "F");
                    doc.text(
                      40,
                      lane4 + inc + 40,
                      "Excess Group Vehicle Weight.Remedial action required: offload " +
                        nodata.wbrg_ticket_gvwload +
                        "[KG]"
                    );
                  }
                  var wbrg_ticket_directionarea = "";
                  try {
                    wbrg_ticket_directionarea =
                      nodata.wbrg_ticket_directionarea.trim();
                  } catch (e) {
                    console.log(e);
                    wbrg_ticket_directionarea = "";
                  }
                  doc.setFont("Tahoma", "", "bold");
                  doc.setFontSize(10);
                  doc.text(40, lane4 + inc + 50, wbrg_ticket_directionarea);

                  //alert();
                  // var reason = sp.data[0].reason;
                  // var description = sp.data[0].otherreason;
                  // var reasondescription =
                  //   "Special Release:" + reason + "; " + description;

                  //alert(nodata.id);
                  // const logofront =
                  //   "http://"+window.$localurl+"/kenload/dashboard/imgs/202110/19/16/FATMBA202110000156_2.jpg";
                  // const overview =
                  //   "http://"+window.$localurl+"/kenload/dashboard/imgs/202110/19/16/OATMBA202110000156_2.jpg";
                  // var imgfront = new Image();
                  // var imgoverview = new Image();
                  // imgfront.src = logofront;
                  // imgoverview.src = overview;

                  // doc.addImage(imgfront, "PNGs", 10, lane4 + inc + 60, 80, 60);
                  // doc.addImage(imgoverview, "PNG", 120, lane4 + inc + 60, 80, 60);
                  //alert("");

                  // var imgoverview = "";

                  var year = nodata.wbrg_ticket_date.substring(0, 4);
                  var month = nodata.wbrg_ticket_date.substring(5, 7);
                  var day = nodata.wbrg_ticket_date.substring(8, 10);
                  var hour = nodata.wbrg_ticket_date.substring(11, 13);
                  var dir = year + month + "/" + day + "/" + hour + "/";

                  //alert(theUrl);
                  // this.url +
                  // "/kenload/dashboard/getimg.php?string=http://192.168.5.53/jpg/image.jpg?size=3&filename=" +
                  // "O" +
                  // this.WBTNO +
                  // ".jpg&dir=" +
                  // this.dir;
                  //alert(theUrl);

                  const logFileText = async () => {
                    try {
                      var theUrl =
                        window.$url +
                        "/kenload/dashboard/getbase64.php?filename=F" +
                        nodata.wbrg_ticket_no +
                        ".jpg&dir=" +
                        dir +
                        "&string=";
                      //alert(theUrl);
                      // var theUrl =
                      //   "http://" +
                      //   "192.168.4.111/kenload/dashboard/getbase64.php?filename=F" +
                      //   nodata.wbrg_ticket_no +
                      //   ".jpg&dir=" +
                      //   dir +
                      //   "&string=";
                      var theUrl2 =
                        window.$url +
                        "/kenload/dashboard/getbase64.php?filename=O" +
                        nodata.wbrg_ticket_no +
                        ".jpg&dir=" +
                        dir +
                        "&string=";

                      const response = await fetch(theUrl);
                      var imgfront = await response.text();

                      const response2 = await fetch(theUrl2);
                      var imgfront2 = await response2.text();

                      doc.addImage(
                        imgfront,
                        "PNG",
                        10,
                        lane4 + inc + 60,
                        90,
                        60
                      );

                      doc.addImage(
                        imgfront2,
                        "PNG",
                        110,
                        lane4 + inc + 60,
                        90,
                        60
                      );
                      //alert("");
                    } catch (e) {
                      console.log(e);
                    }

                    doc.save(
                      nodata.wbrg_ticket_no + "-" + nodata.veh_reg + ".pdf"
                    );
                    doc.output("dataurlnewwindow");
                    Swal.close();
                  };
                  logFileText();
                  // doc.save(nodata.wbrg_ticket_no + "-" + nodata.veh_reg + ".pdf");
                  // doc.output("dataurlnewwindow");
                  // Swal.close();
                })
                .catch(() => {});
            })
            .catch((e) => {
              Swal.fire("axleweights" + e);
              //this.errors.push(e);
            });
        })
        .catch((e) => {
          Swal.fire("users" + e);
          //this.errors.push(e);
        });
    },
  },
};
</script>

<style>
</style>