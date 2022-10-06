
<script>
import anprassist from "@/components/widgets/weigh/anprassist";

import axios from "axios";
import Swal from "sweetalert2";
import jsPDF from "jspdf";
import Tahoma from "@/assets/fonts/tahoma.ttf";

export default {
  props: {
    src: String,
    hswimData: {
      type: Array,
      default: () => {
        return [];
      },
    },
  },
  components: {
    anprassist,
  },
  data() {
    return {
      nplate: "",
      id: "",
      navbuttonVisible: "",
      overweighting: "",
      wim: "",
      orderData: [],
      imae: require("@/assets/images/button.png"),
      imae2: require("@/assets/images/redbutton.jpg"),
      image: [this.imae, this.imae],
      //src: "@/assets/images/truckexample.png",
    };
  },
  // computed: {
  //   calledIn () {
  //     return this.hswimData.overload ? "Called In" : " -"
  //   }
  // }
  mounted() {
    // Set the initial number of items
    this.imae = require("@/assets/images/button.png");
    this.imae2 = require("@/assets/images/redbutton.jpg");
    this.image = [this.imae, this.imae2];
    //alert(this.nplate);
  },

  methods: {
    handleBackToDetails() {},
    anpr(anpr, id, overweighting, wim, index) {
      // alert(index);
      this.nplate = anpr;
      this.id = id;
      this.navbuttonVisible = false;
      this.overweighting = overweighting;
      this.wim = wim;
      this.orderData[0] = this.hswimData[index];

      //alert(this.hswimData[index].lp);
    },
    getimage(date, no, position) {
      var server = window.$url; // arr[0] + "//" + arr2[0] + ":82";
      var year = date.substring(0, 4);
      var month = date.substring(5, 7);
      var day = date.substring(8, 10);
      var hour = date.substring(11, 13);

      var dir = year + month + "/" + day + "/" + hour + "/";

      var theUrl = "";
      if (window.$hswim == "Camea") {
        if (position == "F") {
          position = "LF";
        }
        theUrl = server + "/" + dir + position + no + ".jpg";
      }

      if (window.$hswim == "Bosvark") {
        server = window.$url; //arr[0] + "//" + arr2[0] + ":82";
        year = date.substring(0, 4);
        month = date.substring(5, 7);
        day = date.substring(8, 10);
        hour = date.substring(11, 13);
        var min = date.substring(14, 16);
        var sec = date.substring(17, 19);
        dir = year + month + "/" + day + "/" + hour + "/";
        var number = "";
        if (position == "F") {
          number = "WIM1ANPR";
        }
        if (position == "O") {
          number = "WIM1SV";
        }
        theUrl =
          server +
          "/kenload/dashboard/imgs/" +
          dir +
          number +
          year.substring(2, 4) +
          "-" +
          month +
          "-" +
          day +
          "_" +
          hour +
          "-" +
          min +
          "-" +
          sec +
          "-00" +
          ".jpg";
      }
      //this.truckimae2;
      //alert("List " + theUrl);
      return theUrl;
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
      ///
      axios
        .get(window.$http + `users/search?email=` + window.$userid, {
          headers: window.$headers,
        })
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
                  if (nodata.wbrg_ticket_status == "X") {
                    status = "Cancelled";
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
                  if (
                    nodata.reliefvehreg != "" ||
                    nodata.reliefvehreg != "null"
                  ) {
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
                    "total weight of the axle group is reported as legal. It means the responsibility of the owner to ensure that the vehicle is correctly loaded."
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
                      var ur = window.$url;
                      // var href = window.location.href;
                      // var arr = href.split("/");
                      // var arr2 = arr[2].split(":");
                      // var ref = arr[0] + "//" + arr2[0] + ":82";
                      // var ur = window.$url;
                      // ur = ref;
                      // var theUrl =
                      //   ur +
                      //   "/kenload/dashboard/getbase64img.php?string=" +
                      //   window.$url +
                      //   "/kenload/dashboard/imgs/" +
                      //   dir +
                      //   "F" +
                      //   nodata.wbrg_ticket_no +
                      //   ".jpg";
                      // console.log(theUrl);
                      //alert(theUrl);
                      var theUrl =
                        ur +
                        "/kenload/dashboard/getbase64.php?filename=F" +
                        nodata.wbrg_ticket_no +
                        ".jpg&dir=" +
                        dir +
                        "&string=";
                      //alert(theUrl);

                      var theUrl2 =
                        ur +
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
                    //doc.autoPrint();
                    doc.output("dataurlnewwindow");
                    //doc.output("datauristring");
                    // window.open(
                    //   doc.output("datauristring"),
                    //   "_blank",
                    //   "toolbar=no,status=no,menubar=no,scrollbars=no,resizable=yes,modal=yes,top=200,left=350,width=600,height=400"
                    // );

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
              Swal.fire("Ticket Error -" + e);
              //this.errors.push(e);
            });
        })
        .catch((e) => {
          Swal.fire("Users - Ticket" + e);
          //this.errors.push(e);
        });
    },
  },
};
</script>

<template>
  <div class="row mb-12">
    <!-- :class="data.calledin == 1 ? 'bg_green' : 'bg_red'" -->
    <div
      class="d-flex col-md-1 col-xl-3"
      v-for="(data, index) in hswimData"
      :key="data.cid"
      :class="data.calledin == 0 ? 'bodgreen' : 'bodred'"
    >
      <div
        class="d-flex card"
        style="width: 100%"
        :class="data.calledin == 0 ? 'bodgreen' : 'bodred'"
      >
        <div class="card-header">
          <div class="d-flex justify-content-between divider">
            <h5>{{ data.cid }}</h5>
            <b-button
              @click="
                anpr(data.lp, data.id, data.overweighting, data.wim, index)
              "
              variant="secondary w-sm"
              v-b-modal.modal-ANPR
              v-show="data.calledin == 0 ? false : true"
            >
              ANPR Assist
            </b-button>
            <h5>Axles: {{ data.axles_count }}</h5>
          </div>
          <div class="d-flex justify-content-between mt-1">
            <img
              class="img-fluid rounded"
              v-bind:src="image[data.calledin]"
              alt="error"
              width="70"
            />

            <div class="justify-content-between mt-1">
              <h5
                style="
                  color: yellow;
                  text-align: center;
                  background-color: black;
                  padding: 18px;
                  border-radius: 5px;
                "
              >
                {{ data.lp }}
                <!-- {{ data.anprassist.value() == "" ? data.lp : data.anprassist }} -->
              </h5>
            </div>
            <div>
              <div class="textSmall">
                Date: {{ data.date_time_local.substring(0, 10) }}
              </div>
              <div class="textSmall">
                Time: {{ data.date_time_local.substring(11, 19) }}
              </div>
              <div class="textSmall">Speed: {{ data.velocity }} Km/h</div>
              <div class="textSmall">Gap: {{ data.gap }} Sec(s)</div>
              <div class="textSmall">Length: {{ data.gap }})</div>
            </div>
          </div>
        </div>
        <!-- :src="getimage(data.date_time_local, data.cid, 'F')" -->
        <div>
          <img
            :src="getimage(data.date_time_local, data.cid, 'O')"
            alt="image not found"
            class="rounded zoom"
            height="350"
            style="width: 100%"
          />
        </div>
        <div class="card-footer">
          <div>
            <div v-show="data.calledin == 0 ? false : true">
              <!-- Called In - GVW Overload {{ data.gvw }} or Axle Overload
              {{ data.avw }} -->
              {{ data.calledinreason }}
            </div>
            <b-button
              @click="handleBackToDetails"
              variant="primary w-sm"
              style="background-color: black; color: yellow; float: right"
            >
              <i class="uil uil-search ms-2"></i>
              Ticket
            </b-button>
            <b-button
              v-show="data.calledin == 0 ? false : true"
              @click="handleBackToDetails"
              variant="secondary w-sm"
              v-b-modal.modal-Tag
            >
              Tag
            </b-button>
            <b-button
              :style="
                data.calledin == 0
                  ? 'background-color:  green;'
                  : 'background-color:  red;'
              "
              @click="handleBackToDetails"
            >
              {{ data.total_weight }} [KG]
            </b-button>
          </div>
        </div>
      </div>
    </div>
    <b-modal id="modal-ANPR" title="ANPR Assist" hide-footer size="xl" centered>
      <anprassist
        :nplate="nplate"
        :navbuttonVisible="navbuttonVisible"
        :id="id"
        :anpr="nplate"
        :overweighting="overweighting"
        :wim="wim"
        :orderData="orderData"
      >
      </anprassist>
    </b-modal>
  </div>
</template>

<style scoped>
.divider {
  border-bottom: 2px solid #000;
}
.avatar {
  vertical-align: middle;
  width: 50px;
  height: 50px;
  border-radius: 50%;
}
.bg_red {
  background: #ad4221;
}
.bg_green {
  background: #73ad21;
}
.bodgreen {
  /* border-radius: 25px;
  border: 2px solid green;
  padding: 7px; */
}
.bodred {
  /* border-style: solid;
  border-color: red; */
  /* border-radius: 25px;
  border: 1px solid red;
  padding: 7px; */
}
.textSmall {
  text-align: center;
  font-weight: bold;
  font-size: 12px;
}
.zoom {
  transition: transform 0.2s; /* Animation */
  width: 400px;
  height: 350px;
  margin: 0 auto;
}

.zoom:hover {
  transform: scale(2); /* (150% zoom)*/
}
</style>
