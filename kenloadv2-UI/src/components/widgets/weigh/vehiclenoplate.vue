<script>
/**
 * Stat component
 */
import Swal from "sweetalert2";
import axios from "axios";
//import { image } from "@/assets/images/truckexample.png";

import Deckweights from "@/components/widgets/weigh/deckweights";
import Axleweights from "@/components/widgets/weigh/axleweights";
import Deckimages from "@/components/widgets/weigh/deckimages";
import store from "@/state/store";

//import Swal from "sweetalert2/dist/sweetalert2";

export default {
  components: {
    Deckweights,
    Deckimages,
    Axleweights,
  },
  beforeDestroy() {
    // Swal.fire({
    //   title: "Are you sure, you want close weighing ?",
    //   text: "You won't be able to revert this!",
    //   icon: "warning",
    //   showCancelButton: true,
    //   confirmButtonColor: "#000000",
    //   cancelButtonColor: "#f46a6a",
    //   confirmButtonText: "Yes, Cancel Weighing!",
    // });
  },
  watch: {
    scannedNplate() {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Scanned Number plate -> ${this.nplate}`,
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
    editedNplate() {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Edited Numberpalte -> ${this.nplate}`,
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
    clickedEditNplate() {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Clicked Edit Number plate button`,
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
    weighingStarted() {
      // var curentuser = JSON.parse(localStorage.user).email;
      // var currentdate = new Date();
      // const data = {
      //   datetime: currentdate,
      //   useremail: curentuser,
      //   application: window.navigator.userAgent,
      //   details: `Weighing started...`,
      //   computer: window.localStorage.clientip,
      // };
      // axios
      //   .post(window.$http + "AuditLogs", data, { headers: window.$headers })
      //   .then((response) => {
      //     console.log(response.data);
      //   })
      //   .catch((e) => {
      //     console.log(e);
      //   });
    },
  },
  methods: {
    editedNumberplate() {
      this.editedNumberplate = true;
    },
    genimganpr() {
      //this.getanpr();
      this.getimg();
      //this.$emit("getimage", this.filename, this.dir);
      this.getoverview();
      this.scannedNplate = true;
    },
    genimganprs(events) {
      this.scanned = events[0].scanned;
      this.genimganpr();
    },
    getanpr() {
      var ipaddress = "192.168.2.110";
      var port = "80";
      var wb = window.$weighb;
      var imagepath = "/jpg/image.jpg?size=3";
      var protocol = "http";
      this.filename = "anpr" + window.$weighb + ".jpg";
      for (let i = 0; i < this.cameras.length; i++) {
        if (
          this.cameras[i].position == "Front" &&
          this.cameras[i].weighbridge == wb
        ) {
          //alert(this.cameras[i].ipaddress);
          ipaddress = this.cameras[i].ipaddress;
          imagepath = this.cameras[i].file;
          protocol = this.cameras[i].protocol;
          port = this.cameras[i].port;
        }
      }
      var theUrl =
        window.$url +
        "/kenload/dashboard/getanpr.php?string=" +
        protocol +
        "://" +
        ipaddress +
        ":" +
        port +
        imagepath +
        "&filename=" +
        this.filename +
        "&dir=" +
        this.dir;
      if (store.getters["screens/getcameratype"] == "hikvision") {
        theUrl =
          window.$http +
          "UploadFile/GetANPR?url=" +
          protocol +
          "://" +
          ipaddress +
          ":" +
          port +
          imagepath +
          "&imageName=" +
          this.filename;
      }

      if (store.getters["screens/getBidirection"] == "Left") {
        if (store.getters["screens/getcameratype"] == "hikvision") {
          theUrl =
            window.$http +
            "UploadFile/GetANPR?url=" +
            protocol +
            "://" +
            ipaddress +
            ":" +
            port +
            imagepath +
            "&imageName=" +
            this.filename;
        }
      }

      if (store.getters["screens/getBidirection"] == "Right") {
        for (let i = 0; i < this.cameras.length; i++) {
          //alert(this.cameras[i].file);
          if (
            this.cameras[i].position == "Front" &&
            this.cameras[i].weighbridge == "B"
          ) {
            //alert(this.cameras[i].file);
            //alert(this.cameras[i].position);
            //alert(this.cameras[i].weighbridge);
            ipaddress = this.cameras[i].ipaddress;
            imagepath = this.cameras[i].file;
            protocol = this.cameras[i].protocol;
            port = this.cameras[i].port;
          }
        }
        //alert(store.getters["screens/getcameratype"]);
        if (store.getters["screens/getcameratype"] == "hikvision") {
          theUrl =
            window.$http +
            "UploadFile/GetANPR?url=" +
            protocol +
            "://" +
            ipaddress +
            ":" +
            port +
            imagepath +
            "&imageName=" +
            this.filename;
        } else {
          theUrl =
            window.$url +
            "/kenload/dashboard/getanpr.php?string=" +
            protocol +
            "://" +
            ipaddress +
            ":" +
            port +
            imagepath +
            "&filename=" +
            this.filename +
            "&dir=" +
            this.dir;
        }
        //alert(theUrl);
      }

      const logFileText = async (file) => {
        //alert(theUrl);
        // Swal.fire({
        //   title: "Please Wait !",
        //   html: "Scanning number plate...", // add html attribute if you want or remove
        //   allowOutsideClick: false,
        //   showConfirmButton: false,
        //   willOpen: () => {
        //     Swal.showLoading();
        //   },
        // });
        const response = await fetch(file);
        let text = await response.text();
        //alert(text);
        console.log(text);
        var np = text.indexOf('[{"plate":"');
        var npend = text.indexOf('","confidence":');
        //alert(np);
        //alert(npend);
        if (np > 0) {
          this.nplate = text
            .substring(np + '[{"plate":"'.length, npend)
            .replaceAll("O", "0");
          this.anpr = this.nplate;
        }
        Swal.close();
        return text;
      };
      this.nplate = "";
      console.log(theUrl);
      return logFileText(theUrl);
    },
    getimgs(WBTNO, dir) {
      const logFileText = async () => {
        // var theUrl =
        //   window.$url +
        //   "/kenload/dashboard/getimg.php?string=http://192.168.4.57/jpg/image.jpg?size=3&filename=" +
        //   "F" +
        //   WBTNO +
        //   ".jpg&dir=" +
        //   dir;
        //alert(theUrl);
        var ipaddress = "";
        var port = "80";
        var wb = window.$weighb;
        var imagepath = "";
        var protocol = "http";
        for (let i = 0; i < this.cameras.length; i++) {
          //alert(response.data[i].position);
          if (
            this.cameras[i].position == "Front" &&
            this.cameras[i].weighbridge == wb
          ) {
            ipaddress = this.cameras[i].ipaddress;
            imagepath = this.cameras[i].file;
            protocol = this.cameras[i].protocol;
            port = this.cameras[i].port;
          }
        }
        ///cgi-bin/viewer/video.jpg
        ///jpg/image.jpg?size=3
        var theUrl =
          window.$url +
          "/kenload/dashboard/getimg.php?string=" +
          protocol +
          "://" +
          ipaddress +
          ":" +
          port +
          imagepath +
          "&filename=" +
          "F" +
          WBTNO +
          ".jpg&dir=" +
          dir;

        if (store.getters["screens/getcameratype"] == "hikvision") {
          theUrl =
            window.$http +
            "UploadFile/CaptureImage?url=" +
            protocol +
            "://" +
            ipaddress +
            ":" +
            port +
            imagepath +
            dir +
            "&imageName=F" +
            WBTNO +
            ".jpg";
        }

        if (store.getters["screens/getBidirection"] == "Left") {
          if (store.getters["screens/getcameratype"] == "hikvision") {
            theUrl =
              window.$http +
              "UploadFile/CaptureImage?url=" +
              protocol +
              "://" +
              ipaddress +
              ":" +
              port +
              imagepath +
              dir +
              "&imageName=F" +
              WBTNO +
              ".jpg";
          }
        }

        if (store.getters["screens/getBidirection"] == "Right") {
          for (let i = 0; i < this.cameras.length; i++) {
            //alert(this.cameras[i].file);
            if (
              this.cameras[i].position == "Front" &&
              this.cameras[i].weighbridge == "B"
            ) {
              //alert(this.cameras[i].file);
              //alert(this.cameras[i].position);
              //alert(this.cameras[i].weighbridge);
              ipaddress = this.cameras[i].ipaddress;
              imagepath = this.cameras[i].file;
              protocol = this.cameras[i].protocol;
              port = this.cameras[i].port;
            }
          }
          theUrl =
            window.$http +
            "UploadFile/CaptureImage?url=" +
            protocol +
            "://" +
            ipaddress +
            ":" +
            port +
            imagepath +
            dir +
            "&imageName=F" +
            WBTNO +
            ".jpg";
        }
        //alert(theUrl);
        //alert(theUrl);

        const response = await fetch(theUrl);
        let text = await response.text();

        for (let i = 0; i < this.cameras.length; i++) {
          //alert(this.cameras[i].position);
          //alert(this.cameras[i].weighbridge + " -" + wb);
          if (
            this.cameras[i].position == "Side" &&
            this.cameras[i].weighbridge == wb
          ) {
            //alert(this.cameras[i].position);
            //alert(this.cameras[i].weighbridge);
            ipaddress = this.cameras[i].ipaddress;
            imagepath = this.cameras[i].file;
            protocol = this.cameras[i].protocol;
            port = this.cameras[i].port;
          }
        }

        // theUrl =
        //   window.$url +
        //   "/kenload/dashboard/getimg.php?string=" +
        //   protocol +
        //   "://" +
        //   ipaddress +
        //   ":" +
        //   port +
        //   imagepath;
        // "&filename=" + "O" + WBTNO + ".jpg&dir=" + dir;

        theUrl =
          window.$url +
          "/kenload/dashboard/getimg.php?string=" +
          protocol +
          "://" +
          ipaddress +
          ":" +
          port +
          imagepath +
          "&filename=" +
          "O" +
          WBTNO +
          ".jpg&dir=" +
          dir;

        // theUrl =
        //   window.$url +
        //   "/kenload/dashboard/getimg.php?string=http://192.168.4.57/jpg/image.jpg?size=3&filename=" +
        //   "O" +
        //   WBTNO +
        //   ".jpg&dir=" +
        //   dir;

        //alert(theUrl);
        const response2 = await fetch(theUrl);
        text = await response2.text();
        console.log(text);
        //alert(this.src2);
      };
      logFileText();
    },

    getimg() {
      var ipaddress = "";
      var port = "80";
      var wb = window.$weighb;
      //var vfile = "/jpg/image.jpg?size=3";
      var imagepath = "";
      var protocol = "http";
      //alert(this.cameras.length);
      //alert(wb);
      for (let i = 0; i < this.cameras.length; i++) {
        //alert(this.cameras[i].file);
        if (
          this.cameras[i].position == "Front" &&
          this.cameras[i].weighbridge == wb
        ) {
          //alert(this.cameras[i].file);
          //alert(this.cameras[i].position);
          //alert(this.cameras[i].weighbridge);
          ipaddress = this.cameras[i].ipaddress;
          imagepath = this.cameras[i].file;
          protocol = this.cameras[i].protocol;
          port = this.cameras[i].port;
        }
      }

      var theUrl =
        window.$url +
        "/kenload/dashboard/getimg.php?string=" +
        protocol +
        "://" +
        ipaddress +
        ":" +
        port +
        imagepath;
      "&filename=" + this.filename + "&dir=" + this.dir;

      if (store.getters["screens/getcameratype"] == "hikvision") {
        theUrl =
          window.$http +
          "UploadFile/CaptureImage?url=" +
          protocol +
          "://" +
          ipaddress +
          ":" +
          port +
          imagepath +
          "&imageName=" +
          this.filename;
      }

      //alert(store.getters["screens/getBidirection"]);
      if (store.getters["screens/getBidirection"] == "Left") {
        if (store.getters["screens/getcameratype"] == "hikvision") {
          theUrl =
            window.$http +
            "UploadFile/CaptureImage?url=" +
            protocol +
            "://" +
            ipaddress +
            ":" +
            port +
            imagepath +
            "&imageName=" +
            this.filename;
        } else {
          theUrl =
            window.$url +
            "/kenload/dashboard/getimg.php?string=" +
            protocol +
            "://" +
            ipaddress +
            ":" +
            port +
            imagepath;
          "&filename=" + this.filename + "&dir=" + this.dir;
        }
      }

      if (store.getters["screens/getBidirection"] == "Right") {
        for (let i = 0; i < this.cameras.length; i++) {
          //alert(this.cameras[i].file);
          if (
            this.cameras[i].position == "Front" &&
            this.cameras[i].weighbridge == "B"
          ) {
            //alert(this.cameras[i].file);
            //alert(this.cameras[i].position);
            //alert(this.cameras[i].weighbridge);
            ipaddress = this.cameras[i].ipaddress;
            imagepath = this.cameras[i].file;
            protocol = this.cameras[i].protocol;
            port = this.cameras[i].port;
          }
        }
        if (store.getters["screens/getcameratype"] == "hikvision") {
          theUrl =
            window.$http +
            "UploadFile/CaptureImage?url=" +
            protocol +
            "://" +
            ipaddress +
            ":" +
            port +
            imagepath +
            "&imageName=" +
            this.filename;
        } else {
          theUrl =
            window.$url +
            "/kenload/dashboard/getimg.php?string=" +
            protocol +
            "://" +
            ipaddress +
            ":" +
            port +
            imagepath;
          "&filename=" + this.filename + "&dir=" + this.dir;
        }
      }

      //https://localhost:44365/api/UploadFile/CaptureImage?http://192.168.0.64:80/ISAPI/Streaming/channels/1/picture&username=admin&password=Admin12345&folderpath=C:/xampp/htdocs/kenload/dashboard/imgs/&imageName=anprA.jpg
      //alert(theUrl);
      const logFileText = async (file) => {
        // Swal.fire({
        //   title: "Please Wait !",
        //   html: "Getting image...", // add html attribute if you want or remove
        //   allowOutsideClick: false,
        //   showConfirmButton: false,
        //   willOpen: () => {
        //     Swal.showLoading();
        //   },
        // });
        //alert(file);
        const response = await fetch(file);
        let text = await response.text();
        //alert(text);
        this.src2 = text;
        this.getanpr();
        Swal.close(text);
        //alert(this.src2);
      };
      console.log(theUrl);
      logFileText(theUrl);
    },
    openboom() {
      var theUrl =
        window.$url +
        "/kenload/dashboard/openboom.php?string=http://192.168.4.57/jpg/image.jpg?size=3&filename=" +
        this.filename +
        "&dir=" +
        this.dir;
      //alert(theUrl);
      const logFileText = async (file) => {
        Swal.fire({
          title: "Please Wait !",
          html: "Opening Boom...", // add html attribute if you want or remove
          allowOutsideClick: false,
          showConfirmButton: false,
          willOpen: () => {
            Swal.showLoading();
          },
        });

        const response = await fetch(file);
        let text = await response.text();
        //alert(text);
        Swal.close(text);

        //alert(this.src2);
      };
      logFileText(theUrl);
    },
    getoverview() {
      var ipaddress = "";
      var port = "80";
      var wb = window.$weighb;
      //var vfile = "/jpg/image.jpg?size=3";
      var imagepath = "";
      var protocol = "http";

      //alert(wb);

      for (let i = 0; i < this.cameras.length; i++) {
        //alert(this.cameras[i].position);
        //alert(this.cameras[i].weighbridge + " -" + wb);
        if (store.getters["screens/getBidirection"] == "Left") {
          if (
            this.cameras[i].position == "Side" &&
            this.cameras[i].weighbridge == "B"
          ) {
            //alert(this.cameras[i].position);
            //alert(this.cameras[i].weighbridge);
            ipaddress = this.cameras[i].ipaddress;
            imagepath = this.cameras[i].file;
            protocol = this.cameras[i].protocol;
            port = this.cameras[i].port;
          }
        } else {
          if (
            this.cameras[i].position == "Side" &&
            this.cameras[i].weighbridge == wb
          ) {
            //alert(this.cameras[i].position);
            //alert(this.cameras[i].weighbridge);
            ipaddress = this.cameras[i].ipaddress;
            imagepath = this.cameras[i].file;
            protocol = this.cameras[i].protocol;
            port = this.cameras[i].port;
          }
        }
      }
      ///cgi-bin/viewer/video.jpg
      var theUrl =
        window.$url +
        "/kenload/dashboard/getimg.php?string=" +
        protocol +
        "://" +
        ipaddress +
        ":" +
        port +
        imagepath;
      "&filename=S" + this.filename + "&dir=" + this.dir;
      //alert(theUrl);
      const logFileText = async (file) => {
        const response = await fetch(file);
        let text = await response.text();
        //alert(text);
        this.src3 = text;
      };
      logFileText(theUrl);
    },
    editnumberplate() {
      //alert(this.disabled);
      this.disabled = false;
      this.clickedEditNplate = true;
      this.$refs.nplate.focus();
      this.disabled = false;
      this.$refs.nplate.focus();
    },
    gettags() {
      return this.tags.filter(function (item) {
        return (item.vehreg = this.nplate);
      });
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
    ticketdate() {
      let d = new Date();
      let year = d.getFullYear();
      let month = d.getMonth() + 1;
      month = this.getv(month);
      //const msec = d.getMilliseconds();
      const datetime = year + "" + month + "";
      return datetime;
    },

    goToVehicleReg() {
      this.weighingStarted = true;
      if (window.$weighstate >= 1) {
        Swal.fire("You can't weigh twice, until the vehicle leaves.");
        return;
      }
      this.openEntranceDirection("reset", "Reset");
      this.getyardlist();
      //this.$nplate = this.nplate;

      var startdatetime = this.getcurrentdate();
      this.nplate = this.nplate
        .toUpperCase()
        .replaceAll("\\s", "")
        .replaceAll("[^a-zA-Z0-9]", "")
        .replaceAll(" ", "")
        .trim();

      if (this.nplate.trim() == "") {
        Swal.fire("Please enter Vehicle Registration Number!");
        this.$refs.nplate.focus();
        return;
      }

      if (this.nplate.length < 4) {
        Swal.fire("Please enter correct Vehicle Registration Number !");
        this.$refs.nplate.focus();
        return;
      }

      if (this.cat == "multideck") {
        let gvw =
          Number(this.$refs.child.deck1) +
          Number(this.$refs.child.deck2) +
          Number(this.$refs.child.deck3) +
          Number(this.$refs.child.deck4);

        //alert(gvw);
        //alert(this.$refs.child.deck3);
        if (gvw < 100) {
          Swal.fire("No Vehicle on Deck!");
          //this.$refs.nplate.focus();
          return;
        }
      }

      if (this.cat == "lswim") {
        let gvw = Number(this.$refs.axlew.deck1);

        //alert(gvw);
        //alert(this.$refs.child.deck3);
        if (gvw > 100) {
          Swal.fire("Vehicle on Scale");
          //this.$refs.nplate.focus();
          return;
        }
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
        .get(window.$http + `vehicles/search?vehicle_reg=` + this.nplate, {
          headers: window.$headers,
        })
        .then((response) => {
          //var ddatesp = response.data.permitexpiration_date.split("T").replaceAll("-","/");
          this.vehicles = response.data[0];
          Swal.close();
          //console.log(response);
          //alert(window.$http + `vehicles/` + this.vehicles.vehicle_reg);
          var vehreg = "";
          try {
            vehreg = this.vehicles.vehicle_reg;
          } catch (e) {
            vehreg = "";
            this.vehicles = [];
          }
          if (this.nplate != vehreg) {
            Swal.fire({
              title:
                this.nplate +
                " does not exits. Do you want to continue weighing " +
                this.nplate +
                "?",
              text: "You won't be able to revert this!",
              icon: "warning",
              showCancelButton: true,
              confirmButtonColor: "#000000",
              cancelButtonColor: "#f46a6a",
              confirmButtonText: "Yes, Weigh!",
            }).then((result) => {
              if (result.value) {
                //this.$delete(this.orderData, this.id - 1);

                this.weighing(startdatetime);
              }
            });
          } else {
            this.weighing(startdatetime);
          }
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Vehicles :" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });
          //throw e;
        });

      //this.$router.push("vehicledetails/" + this.nplate);
    },
    weighing(startdatetime) {
      Swal.close();
      Swal.fire({
        title: "Please Wait !",
        html: "Loading data...", // add html attribute if you want or remove
        allowOutsideClick: false,
        showConfirmButton: false,
        willOpen: () => {
          Swal.showLoading();
        },
      });
      var tagged = false;
      //alert(window.$http + `tag/search?vehreg=` + this.nplate + `&status=0`);
      //alert(window.$headers);
      axios
        .get(window.$http + `tag/search?vehreg=` + this.nplate + `&status=0`, {
          headers: window.$headers,
        })
        .then((response) => {
          // JSON responses are automatically parsed.
          //alert(window.$http + `tag/` + this.nplate);
          this.tags = response.data;
          console.log(response.data);

          //alert(this.tags[0].vehreg);
          //alert(this.tags.length);
          //for (let i = 0; i < this.tags.length; i++) {
          if (this.tags.length > 0) {
            if (this.nplate == this.tags[0].vehreg.trim()) {
              //console.log("sure what the hell");
              tagged = true;
            }
          }
          //}
          //alert(window.$http + `yardlist/search?vehreg=` + this.nplate);
          axios
            .get(
              window.$http + `yardlist/search?status=I&vehreg=` + this.nplate,
              { headers: window.$headers }
            )
            .then((res) => {
              this.yard = res.data;
              var yardtick = "";
              var reweighno = 0;
              //alert("nplate: " + this.nplate + " - yard" + this.yard.vehreg);
              if (this.yard.length > 0) {
                if (this.nplate == this.yard[0].vehreg) {
                  //console.log("sure what the hell");
                  this.inyard = true;
                  yardtick = this.yard[0].wbtno;
                  axios
                    .get(
                      window.$http +
                        `weighbridgetransactions/max?reweigh=` +
                        yardtick,
                      { headers: window.$headers }
                    )
                    .then((response) => {
                      //alert(response.data);
                      reweighno = Number(response.data);
                      this.getwbt(startdatetime, yardtick, reweighno, tagged);
                      //reweighno++;
                    });
                }
              } else {
                this.getwbt(startdatetime, yardtick, reweighno, tagged);
              }
            })
            .catch((e) => {
              Swal.fire({
                position: "center",
                icon: "error",
                title: "Taggs" + e,
                showConfirmButton: true,
              }).then((e) => {
                Swal.close(e);
              });
              //throw e;
            });
        })
        .catch((e) => {
          Swal.close();
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
    getwbt(startdatetime, yardtick, reweighno, tagged) {
      //alert(
      //   window.$http +
      //     `weighbridgetransactions/max?wbrg_ticket_no=` +
      //     window.$stationcode +
      //     window.$weighb +
      //     this.ticketdate()
      // );
      var stationcode = window.$stationcode;
      if (store.getters["screens/getBidirection"] == "Left") {
        stationcode = window.$stationcode2;
      }

      axios
        .get(
          window.$http +
            `weighbridgetransactions/max?wbrg_ticket_no=` +
            stationcode +
            window.$weighb +
            this.ticketdate(),
          { headers: window.$headers }
        )
        .then((response) => {
          //alert("tt");
          let WBTNO = "";

          if (this.inyard) {
            WBTNO = yardtick + "_" + reweighno;
          } else {
            let ticketmax = Number(response.data);
            ticketmax++;
            var ticketno = this.padLeadingZeros(ticketmax, 6);
            WBTNO = stationcode + window.$weighb + this.ticketdate() + ticketno;
          }
          var year = startdatetime.substring(0, 4);
          var month = startdatetime.substring(5, 7);
          var day = startdatetime.substring(8, 10);
          var hour = startdatetime.substring(11, 13);
          var dir = year + month + "/" + day + "/" + hour + "/";
          this.getimgs(WBTNO, dir);
          var wbrg_ticket_type = "L";
          if (this.cat == "multideck") {
            wbrg_ticket_type = "M";
          }
          const nodata = {
            wbrg_ticket_no: WBTNO,
            wbrg_ticket_type: wbrg_ticket_type,
            wbrg_station: stationcode,
            wbrg_ticket_weighbridge: window.$weighb,
            wbrg_ticket_date: startdatetime,
            wbrg_ticket_reweigh_no: 0,
            permit_no: "",
            wbrg_ticket_status: "P",
            wbrg_ticket_ref: yardtick,
            veh_reg: this.nplate,
            wbrg_roadauthority: window.roadauthority,
            anpr_veh_reg: this.anpr,
            wbrg_ticket_operator: window.$userid,
          };
          axios
            .post(window.$http + `weighbridgetransactions/`, nodata, {
              headers: window.$headers,
            })
            .then((response) => {
              // JSON responses are automatically parsed.
              console.log(response.data);
              Swal.close();
              Swal.fire({
                position: "center",
                icon: "success",
                title:
                  "Ticket No. " +
                  WBTNO +
                  " \n Created for registration number" +
                  this.nplate +
                  " \n  created successfully!",
                showConfirmButton: false,
                timer: 1500,
              }).then(() => {
                var data = {
                  events: "Weighing Started",
                  datetime: this.getcurrentdate(),
                  userid: window.$userid,
                  desc: this.nplate + " for ticket " + WBTNO,
                };

                axios.post(window.$http + `events/`, data, {
                  headers: window.$headers,
                });
                if (tagged) {
                  this.$router.push({
                    name: "taglist",
                    params: {
                      nplate: this.nplate,
                      cat: this.cat,
                      startdatetime: startdatetime,
                      WBTNO: WBTNO,
                      nodata: response.data,
                      inyard: this.inyard,
                      vehicles: this.vehicles,
                      reweighno: reweighno,
                      yard: this.yard,
                      mode: true,
                    },
                  });
                  return;
                } else {
                  this.$router.push({
                    name: "vehicledetails",
                    params: {
                      nplate: this.nplate,
                      cat: this.cat,
                      startdatetime: startdatetime,
                      WBTNO: WBTNO,
                      nodata: response.data,
                      inyard: this.inyard,
                      vehicles: this.vehicles,
                      reweighno: reweighno,
                      yard: this.yard,
                    },
                  });
                }
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
        });
    },
    getcameras() {
      //screen and roles combination
      //alert("mounted");

      axios
        .get(window.$http + `Camera`, { headers: window.$headers })
        .then((response) => {
          //alert(window.$http + `Camera`);
          // JSON responses are automatically parsed.
          this.cameras = response.data;
          //Swal.close();
          //console.log(this.orderData);
          // this.nocameras = response.data.length;
          // for (let i = 0; i < response.data.length; i++) {
          //   //alert(response.data[i].position);
          //   if (
          //     response.data[i].position == "Front" ||
          //     response.data[i].position == "Rear"
          //   ) {
          //     this.anprcamera++;
          //   }
          // }
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Cameras" + e,
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
    getmylongdate(mydate) {
      let d = new Date(mydate);
      let year = d.getFullYear();
      let month = this.getmonth(d.getMonth());
      let date = d.getDate();
      date = this.getv(date);
      //month = this.getv(month);
      let hour = d.getHours();
      let min = d.getMinutes();
      let sec = d.getSeconds();

      sec = this.getv(sec);

      var ampm = hour >= 12 ? "pm" : "am";
      hour = hour % 12;
      hour = hour ? hour : 12; // the hour '0' should be '12'
      min = min < 10 ? "0" + min : min;

      //01/Jan/0001 12:00:00 AM

      //const msec = d.getMilliseconds();
      const datetime =
        date +
        "/" +
        month +
        "/" +
        year +
        " " +
        hour +
        ":" +
        min +
        ":" +
        sec +
        " " +
        ampm;
      return datetime;
    },

    openEntranceDirection(direction, action) {
      //alert(this.yardlist[0].vehreg);
      //alert(window.$http + `yardlist/search?vehreg=` + this.nplate);
      //var yardtick = "ATMBA202109000032";]
      //store.dispatch("screens/setBiredirection", "");

      var stationcode = window.$stationcode;
      if (store.getters["screens/getBidirection"] == "Left") {
        stationcode = window.$stationcode2;
      }

      axios
        .get(
          window.$http +
            `weighbridgetransactions/max?wbrg_ticket_no=` +
            stationcode +
            window.$weighb +
            this.ticketdate(),
          { headers: window.$headers }
        )
        .then((response) => {
          //alert("tt");
          let WBTNO = "";

          let ticketmax = Number(response.data);
          ticketmax++;
          var ticketno = this.padLeadingZeros(ticketmax, 6);
          WBTNO = stationcode + window.$weighb + this.ticketdate() + ticketno;
          var startdatetime = this.getcurrentdate();
          var year = startdatetime.substring(0, 4);
          var month = startdatetime.substring(5, 7);
          var day = startdatetime.substring(8, 10);
          var hour = startdatetime.substring(11, 13);
          var dir = year + month + "/" + day + "/" + hour + "/";
          this.getimgs(WBTNO, dir);
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Cameras" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });

          //throw e;
        });

      console.log(store.getters["screens/getBidirection"]);

      if (direction == "left") {
        store.dispatch("screens/setBiredirection", "Left");
      }
      if (direction == "Right") {
        store.dispatch("screens/setBiredirection", "Right");
      }
      var data = {};
      axios
        .post(
          window.$http + `IOSettings/` + direction + `?Action=` + action,
          data,
          {
            headers: window.$headers,
          }
        )
        .then((response) => {
          if (!direction == "reset") {
            Swal.fire({
              position: "center",
              icon: "success",
              title: "Entrance Boom Open",
              showConfirmButton: true,
              timer: 3000,
            }).then(() => {
              Swal.close();
            });
            console.log(response.data);
            //this.awaitboom = false;
          }
          //alert(response.data[0].vehreg);
        })
        .catch((e) => {
          console.log(e);
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Could Not Open Boom!",
            showConfirmButton: true,
          }).then(() => {
            Swal.close();
          });
        });
    },

    openEntrance(comm) {
      //alert(this.yardlist[0].vehreg);
      //alert(window.$http + `yardlist/search?vehreg=` + this.nplate);
      //var yardtick = "ATMBA202109000032";]

      var data = {};
      axios
        .post(window.$http + `IOSettings/deckEntry?Action=` + comm, data, {
          headers: window.$headers,
        })
        .then((response) => {
          Swal.fire({
            position: "center",
            icon: "success",
            title: "Entrance Boom Open",
            showConfirmButton: true,
            timer: 3000,
          }).then(() => {
            Swal.close();
          });
          console.log(response.data);
          this.awaitboom = false;
          //alert(response.data[0].vehreg);
        })
        .catch((e) => {
          console.log(e);
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Could Not Open Boom!",
            showConfirmButton: true,
          }).then(() => {
            Swal.close();
          });
        });
    },
    getyardlist() {
      //alert(this.yardlist[0].vehreg);
      //alert(window.$http + `yardlist/search?vehreg=` + this.nplate);
      //var yardtick = "ATMBA202109000032";

      axios
        .get(window.$http + `yardlist/search?vehreg=` + this.nplate, {
          headers: window.$headers,
        })
        .then((response) => {
          this.yardlist = response.data;
          //alert(response.data[0].vehreg);
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Yard List : " + e,
            showConfirmButton: true,
          }).then(() => {
            Swal.close();
          });
        });
    },
    padLeadingZeros(num, size) {
      var s = num + "";
      while (s.length < size) s = "0" + s;
      return s;
    },
    loadtags() {
      //screen and roles combination
      //alert(window.$http);
    },
    handleSubmit() {
      console.log("Error on submit");
    },
    focusInput() {
      //const input = this.$refs.nplate;
      this.$refs.next.focus();
      //this.elments[1].focus();
    },
    stopstate() {
      this.stoped2 = !this.stoped2;
    },
    updatedata(events) {
      this.anprcheck = Number(events[0].anprcheck);
    },
  },
  created() {
    // setInterval(() => {
    //   //this.anprscan = true;
    //   //capture anpr when vehicle reaches DECK 1
    //   //alert(Number(this.$refs.child.deck1) > 300);
    //   if (this.anprcheck == 1) {
    //     //alert(this.anprcheck);
    //     //if (Number(this.$refs.child.deck1) > 300) {
    //     // this.scanned = true;
    //     Swal.fire({
    //       title: "Please Wait !",
    //       html: "Getting image...", // add html attribute if you want or remove
    //       allowOutsideClick: false,
    //       showConfirmButton: true,
    //       willOpen: () => {
    //         Swal.showLoading();
    //       },
    //     });
    //     this.genimganpr();
    //     var detail = {
    //       nplate: this.nplate,
    //       scanned: 1,
    //     };
    //     //"Content-Type": "application/x-www-form-urlencoded",
    //     var optionAxios = {
    //       headers: {
    //         "Content-Type": "application/x-www-form-urlencoded",
    //       },
    //     };
    //     axios
    //       .post(`http://127.0.0.1:3031/scan`, detail, optionAxios)
    //       .then(() => {
    //         //tt = false;
    //         this.anprcheck = 0;
    //       })
    //       .catch((e) => {
    //         Swal.fire("Send Weights" + e);
    //         console.log(e);
    //       });
    //     //}
    //   }
    // }, 1000);
    this.$refs.nplate.focus();
  },

  mounted() {
    // var href = window.location.href;

    // var arr = href.split("/");
    // var ref2 = arr[2].split(":");

    // var ref = arr[0] + "//" + ref2[0] + "";
    axios
      .get(window.$http + `systemsettings`, { headers: window.$headers })
      .then((res) => {
        //alert(res.data[0].camera);
        try {
          store.dispatch("screens/setCameratype", "" + res.data[0].camera);
          //alert(store.getters["screens/getcameratype"]);
        } catch (Exception) {
          //console.log("window.$cameratype:" + window.$cameratype);
          //alert("Error");
        }
      })
      .catch(() => {});

    this.station = window.$station;
    this.station2 = window.$station2;
    var HoursBefore = new Date();
    HoursBefore.setHours(HoursBefore.getHours() - 12);

    var HoursAfter = new Date();
    //alert(window.$station2.trim());
    try {
      if (window.$station2.trim() != "") {
        this.awaitboom = false;
      }
    } catch (Exception) {
      //
      console.log();
    }
    try {
      if (window.$station2.trim() != "") {
        this.bidirection = true;
      }
    } catch (Exception) {
      //
      console.log();
    }
    if (this.cat == "lswim") {
      this.editnumberplate();
      this.editnumberplate();
    }
    //01/Jan/0001 12:00:00 AM

    var fromdate = this.getmylongdate(HoursBefore);
    var todate = this.getmylongdate(HoursAfter);

    axios
      .get(
        window.$http +
          "scaletest/search?weighbridge=" +
          window.$weighb +
          "&fromdate=" +
          fromdate +
          "&todate=" +
          todate,
        { headers: window.$headers }
      )
      .then((response) => {
        // JSON responses are automatically parsed.
        // this.cargos = response.data.cargo_name;
        //alert(response.data[0].id);
        if (Number(response.data[0].id) > 0) {
          this.scaletest = false;
        }
      })
      .catch(() => {
        this.scaletest = true;

        //throw e;
      });

    this.getcameras();
    this.url = "http://" + window.$localurl + "";
    this.src2 = require("@/assets/images/truckexample.png");
    this.src3 = require("@/assets/images/truckexample.png");
    this.dir = this.ticketdate(new Date());

    this.filename = "anpr" + window.$weighb + ".jpg";
    this.weighb = window.$weighb;
    if (this.cat == "lswim") {
      this.stoped = true;
    }
    if (this.cat == "multideck") {
      this.stoped2 = true;
    }

    //this.genimganpr();
    this.focusInput();
  },
  updated() {
    this.weighb = window.$weighb;
  },

  props: {
    cat: String,
  },
  data() {
    return {
      filename: "",
      scaletest: false,
      scanned: false,
      scannedNplate: false,
      editedNplate: false,
      clickedEditNplate: false,
      weighingStarted: false,
      anprcheck: 0,
      cameras: [],
      timer: 2000,
      vehicles: [],
      decktimerun: 1000,
      axletimerun: 1000,
      stoped: false,
      stoped2: false,
      anprscan: true,
      nplate: "",
      url: "",
      anpr: "",
      station: "",
      station2: "",
      inyard: false,
      awaitboom: true,
      bidirection: false,
      weighb: "",
      dir: "",
      addstr: "",
      src2: require("@/assets/images/truckexample.png"),
      src3: require("@/assets/images/truckexample.png"),
      disabled: true,
      yardlist: [
        {
          id: 8,
          vehreg: "KBY315N",
          status: "I",
          userid: "michael@micna.co.ke",
          datetime: "2021-09-23T17:45:28",
          wbtno: "ATMBA202109000065",
          wbrg_tickt_no: null,
          specialrelease: null,
          spreleaseno: null,
          station: "ATMB",
          caseid: null,
          yardtimeout: "0001-01-01T00:00:00",
          inspected: 0,
        },
      ],
      tags: [
        // {
        //   id: "1",
        //   vehreg: "KBA857V",
        // },
      ],
      yard: [],
    };
  },
};
</script>

<template>
  <div class="row">
    <Deckweights
      ref="child"
      :scanned="scanned"
      :stoped2="stoped2"
      :anprcheck="anprcheck"
      v-show="cat == 'multideck'"
      v-on:updatedata="updatedata($event)"
      v-on:genimganprs="genimganprs($event)"
    />
    <Axleweights
      ref="axlew"
      :stoped="stoped"
      v-show="cat == 'lswim'"
      :scanned="scanned"
      v-on:updatedata="updatedata($event)"
      v-on:genimganprs="genimganprs($event)"
    />
    <Deckimages :src2="src2" :src3="src3" />
    <div class="row mb-2" v-show="bidirection">
      <div class="col-8 mb-0 font-text-size-20">
        <b-button
          pill
          style="background-color: green; color: yellow; float: left"
          @click="openEntranceDirection('left', 'Open')"
        >
          {{ station2 }}
        </b-button>
      </div>
      <div class="col-4">
        <b-button
          class="align-right"
          pill
          style="background-color: green; color: yellow; float: right"
          @click="openEntranceDirection('Right', 'Open')"
        >
          {{ station }}
        </b-button>
      </div>
    </div>

    <div class="col-md-12 col-xl-12">
      <form
        class="row row-cols-lg-auto gx-3 gy-2 align-items-center"
        @submit.prevent="goToVehicleReg"
      >
        <div class="col-12 mb-2" style="width: 50%">
          <div class="input-group">
            <div class="input-group-text col-4">Vehicle Registration No:</div>
            <div class="col-5">
              <input
                v-if="disabled"
                disabled
                ref="nplate"
                type="text"
                class="form-control textBig"
                id="nplate"
                placeholder=""
                v-model="nplate"
                @change="editedNumberplate()"
              />
              <input
                v-if="!disabled"
                ref="nplate"
                type="text"
                class="form-control textBig"
                id="nplate"
                placeholder=""
                @submit="goToVehicleReg()"
                v-model="nplate"
              />
            </div>
            <div class="col-2">
              <div class="col-1">
                <b-button
                  @click="genimganpr()"
                  variant="primary"
                  style="background-color: black; color: yellow; float: right"
                >
                  Scan
                  <i class="uil uil-arrows-shrink-h ms-2"></i>
                </b-button>
              </div>
            </div>
          </div>
        </div>

        <div class="col-12" style="width: 20%">
          <!-- <b-button
            pill
            variant="primary"
            @click="stopstate()"
            style="color: yellow"
            >{{ stoped2 }}
            <i class="uil uil-edit-alt ms-2"></i>
          </b-button> -->
          <b-button
            v-show="scanned || cat == 'lswim'"
            pill
            variant="primary"
            @click="editnumberplate()"
            style="color: yellow"
          >
            Edit Number Plate
            <i class="uil uil-edit-alt ms-2"></i>
          </b-button>
        </div>
        <div class="col-12" style="width: 15%">
          <div>
            <!-- @click="openboom()" -->
            <b-button
              v-show="awaitboom"
              @click="openEntrance('Open')"
              pill
              variant="primary"
              style="background-color: black; color: yellow; float: right"
            >
              Weigh
              <i class="uil uil-arrows-shrink-h ms-2"></i>
            </b-button>
          </div>
        </div>

        <div class="col-12" style="width: 15%">
          <b-button
            @click="goToVehicleReg()"
            variant="success"
            style="background-color: green; color: yellow; float: right"
            ref="next"
          >
            Next
            <i class="uil uil-angle-double-right ms-2"></i>
          </b-button>
        </div>
      </form>
    </div>
    <div class="col-2 mb-1 font-text-size-20">
      <b-button v-b-modal.modal-Edit pill variant="primary"
        >Change Weighbridge
      </b-button>
      {{ weighb }}
    </div>

    <div
      v-show="scaletest"
      class="
        col-3
        mb-2
        badge
        pill
        bg-soft-danger
        font-size-20 font-text-size-100
      "
    >
      SCALE TEST REQUIRED (SCALE TEST NOT DONE).
    </div>
    <!-- End Form Layout -->
  </div>
  <!-- end row-->
</template>
<style scoped>
.textBig {
  text-align: left;
  font-weight: bold;
  font-size: 30px;
}
</style>
