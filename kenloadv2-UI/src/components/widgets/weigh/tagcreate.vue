<script>
//import vue2Dropzone from "vue2-dropzone";
import "vue2-dropzone/dist/vue2Dropzone.min.css";

import Swal from "sweetalert2";
import axios from "axios";
const newheaders=window.$headers;
newheaders["Content-Type"] = 'multipart/form-data';
//alert(JSON.stringify(newheaders));
export default {
  props: {
    nplate2: String,
    reason: String,
  },

  data() {
    return {
      files: "",
     // dropzoneOptions: {
     //   url: window.$http + "UploadFile/uploadSingle?folderPath=C://&prefix=TEST",///api/UploadFile/uploadMultiple
     //  uploadMultiple: true,
     //   addRemoveLinks: true,
      //  maxFilesize: 2, // MB
      //  maxFiles: 4,
     //   thumbnailWidth: 150,
     //   headers:newheaders,
    //  },
      nplate: "",
      tagImages: new FormData(),
      tagImagesCount:0,
      vehicleList: [],
      transporterList: [],
      tagData: {
        vehicleNumber: "",
        createTag: false,
        trailerNumber: "",
        transactionNumber: "",
        reason: "",
        transporterList: [],
        transporter: "",
        driverName: "",
        sendSms: false,
        sendEmail: false,
        image: [],
        status: 1,
      },
    };
  },
  components: {
   // vueDropzone: vue2Dropzone,
  },
  watch: {
    createTag() {
      var curentuser = JSON.parse(localStorage.user).email;
      var currentdate = new Date();
      const data = {
        datetime: currentdate,
        useremail: curentuser,
        application: window.navigator.userAgent,
        details: `Created a Tag for ${this.nplate}`,
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
  methods: {
 previewFiles(event) {
   var output = document.getElementById('output');
   output.src = URL.createObjectURL(event.target.files[0]);
   this.tagImages.append('files',event.target.files[0]);
   this.tagImagesCount++;
  
   },
    previewFiles2(event) {
   var output2 = document.getElementById('output2');
   output2.src = URL.createObjectURL(event.target.files[0]);
   this.tagImages.append('files',event.target.files[0]);
    this.tagImagesCount++;
      //console.log(event.target.file);
   },
    previewFiles3(event) {
   var output3 = document.getElementById('output3');
   output3.src = URL.createObjectURL(event.target.files[0]);
      //console.log(event.target.file);
   this.tagImages.append('files',event.target.files[0]);
    this.tagImagesCount++;
   },
    previewFiles4(event) {
   var output4 = document.getElementById('output4');
   output4.src = URL.createObjectURL(event.target.files[0]);
   this.tagImages.append('files',event.target.files[0]);
    this.tagImagesCount++;
   },
     removeAllFiles() {
      this.$refs.myVueDropzone.removeAllFiles();
    },
    handleSaveTag() {},

    handleBackToDetails() {},
    submit() {
      
     // alert(JSON.stringify(this.tagImages));
    
      
      if (this.nplate.trim() == "") {
        Swal.fire("Please enter Vehicle Registration Number!");
        return;
      }
      // if (this.tagImagesCount<2) {
      //  Swal.fire("Please Attach Atleast 2 Evidence Images!");
      //  return;
      //}

      if (this.nplate.length < 4) {
        Swal.fire("Please enter correct Vehicle Registration Number !");
        return;
      }
      if (this.tagData.reason.length < 4) {
        Swal.fire("Please enter reason");
        return;
      }

      axios
        .get(window.$http + `users/search?email=` + window.$userid, {
          headers: window.$headers,
        })
        .then((resp) => {
          var data = {
            vehreg: this.nplate,
            reason: this.tagData.reason,
            datetime: this.getcurrentdate(),
            userid: window.$userid,
            station: window.$stationcode,
            type: "M",
            sms: resp.data[0].phone,
            email: window.$userid,
            exported: "N",
            status: 0,
          };
          var strs = "<div></div>";

          Swal.fire({
            title: "Are you sure you want to tag " + this.nplate + "?",
            html: "<pre>" + strs + "</pre>" + "",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "alert alert-danger",
            cancelButtonColor: "#f46a6a",
            confirmButtonText: "Yes, Tag " + this.nplate,
          }).then((result) => {
            if (result.value) {
              axios
                .post(window.$http + `tag/`, data, { headers: window.$headers })
                .then((response) => {
                  this.$root.$emit("bv::hide::modal", "modal-AxleConfig");
                  this.submitFiles(response.data.taguid);
                  this.createTag = true;
                  Swal.fire("Tag Created for" + this.nplate);
                  Swal.fire({
                    position: "center",
                    icon: "success",
                    title: "Tag Created for" + this.nplate,
                    showConfirmButton: false,
                    timer: 1500,
                  }).then(() => {
                    this.nplate = "";
                    this.tagData.reason = "";
                  });
                })
                .catch((e) => {
                  Swal.fire("" + e);
                  //this.errors.push(e);
                });
              //alert(JSON.stringify(this.tagData), null, 2)
              //Data to send to server
            }
          });
        })
        .catch((e) => {
          Swal.fire("users" + e);
          //this.errors.push(e);
        });
    },
    submitFiles(tagid) {
      // axios.defaults.headers.common["Access-Control-Allow-Origin"] = "*";
      const folderpath = "C:\\kenloadimg\\tags";
      axios
        .post(window.$http + "UploadFile/uploadMultiple?folderPath="+folderpath+"&prefix="+tagid, this.tagImages, {
          headers: newheaders,
        })
        .then((response) => {
         //alert(JSON.stringify(response.data));
         console.log(response.data);
        })
        .catch((e) => {
         // alert(e);
         console.log(e);
        });
    },

    uploadSuccess() {},
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
  },
  middleware: "authentication",
};
</script>

<template>
  <div class="row">
    <div class="col-12">
      <div class="card">
        <div class="card-body">
          <!-- <h4 class="card-title">Create Tag</h4> -->

          <form @submit.prevent="submit" encType="multipart/form-data" >
            <div class="row">
              <!-- tag details form -->
              <div class="col-lg-5">
                <div class="mt-5 mt-lg-4">
                  <h5 class="font-size-14 mb-4">
                    <i class="mdi mdi-arrow-right text-primary me-1"></i>
                    Create Tag
                  </h5>
                  <div class="form-group row mb-4">
                    <label for="vehicle_number" class="col-sm-3 col-form-label"
                      >Vehicle Number</label
                    >
                    <div class="col-sm-9">
                      <input
                        class="form-control"
                        type="text"
                        v-model="nplate"
                      />
                    </div>

                    <label for="vehicle_number" class="col-sm-3 col-form-label"
                      >Reason</label
                    >
                    <div class="col-sm-9">
                      <textarea
                        v-model="tagData.reason"
                        class="form-control"
                        id="reason"
                        rows="12"
                      ></textarea>
                    </div>
                  </div>

                  <!-- <div class="form-group row mb-4">
                    <label for="transporter" class="col-sm-3 col-form-label"
                      >Action / Status</label
                    >
                    <div class="col-sm-9">
                      <multiselect
                        :options="transporterList"
                        v-model="tagData.transporter"
                        aria-required="Required"
                        name="transporter"
                        id="transporter"
                      />
                    </div>
                  </div> -->
                  <!-- 
                  <div class="row justify-content-end">
                    <div class="col-sm-9">
                      <div class="form-check mb-4">
                        <input
                          type="checkbox"
                          class="form-check-input"
                          v-model="tagData.sendSms"
                          id="horizontal-customCheck"
                        />
                        <label
                          class="form-check-label"
                          for="horizontal-customCheck"
                          >Send Sms</label
                        >
                      </div>

                      <div class="form-check mb-4">
                        <input
                          type="checkbox"
                          class="form-check-input"
                          v-model="tagData.sendEmail"
                          id="horizontal-customCheck"
                        />
                        <label
                          class="form-check-label"
                          for="horizontal-customCheck"
                          >Send Email</label
                        >
                      </div>
                    </div>
                  </div> -->
                </div>
              </div>
              <!-- end tag details col-1 -->

              <!-- column 2 - [tag description] -->
              <div class="col-lg-4">
                <div class="mt-5 mt-lg-4">
                  <h5 class="font-size-14 mb-4">
                    <i class="mdi mdi-arrow-right text-primary me-1"></i>
                    Attachements -Atleast 2 Images
                  </h5>
                  

                  <div class="row">
                    <div class="col-10">
                  <img class="zoom" id="output" width="50%" style="padding:2px;" />
                    </div>
                    <div class="col-2">
                 <input type="file" @change="previewFiles" class="chooseBtn">
                    </div>
                  </div>
                  <div class="row">
                      <div class="col-10">
                  <img class="rounded zoom" id="output2" width="50%" style="padding:2px;"/>
                      </div>
                        <div class="col-2">
                 <input type="file" @change="previewFiles2" class="chooseBtn" >
                        </div>
                  </div>
                 <div class="row">
                     <div class="col-10">
                  <img class="rounded zoom" id="output3" width="50%" style="padding:2px;"/>
                     </div>
                       <div class="col-2">
                 <input type="file" @change="previewFiles3" class="chooseBtn" >
                       </div>
                 </div>
                 <div class="row">
                     <div class="col-10">
                  <img class="zoom" id="output4" width="50%" style="padding:2px;"/>
                     </div>
                       <div class="col-2">
                 <input type="file" @change="previewFiles4" class="chooseBtn" >
                       </div>
                 </div>
                
                  <!-- file upload
                  <vue-dropzone
                    id="dropzone"
                    ref="myVueDropzone"
                    
                    :use-custom-slot="true"
                    :options="dropzoneOptions"
                    v-on:vdropzone-success="uploadSuccess"
                    v-on:vdropzone-error="uploadError"
                    v-on:vdropzone-removed-file="fileRemoved"
                  >
                    <div class="dropzone-custom-content">
                      <i class="display-5 text-muted bx bxs-cloud-upload"></i>
                      <h4>Drop image here or click to upload.</h4>
                    </div>
                  </vue-dropzone> -->
                  <!-- <div class="text-center mt-2">
                    <b-button size="sm" variant="danger">Delete Image</b-button>
                  </div> -->
                </div>
              </div>
              <!-- end column 2 -->

              <!-- column 3 - upload image -->
              <div class="col-lg-3 ms-lg-auto">
                <div class="mt-5 mt-lg-4"></div>
              </div>
              <!-- col-3 end upload image -->
            </div>
          </form>
          <!-- end tag form -->
        </div>
        <div class="d-flex justify-content-between">
          <b-button
            @click="submit"
            variant="primary"
            style="background-color: black; color: yellow; float:right"
          >
            <i class="uil uil-save ms-2"></i>
            Create Tag for {{ nplate }}
          </b-button>
        </div>
      </div>
    </div>
  </div>
</template>
<style scoped>
.zoom {
  transition: transform 0.2s; /* Animation */
  margin: 0 auto;
}

.zoom:hover {
  transform: scale(5); /* (150% zoom)*/
}
.chooseBtn{
  background-color:black;
  color:yellow
}


</style>