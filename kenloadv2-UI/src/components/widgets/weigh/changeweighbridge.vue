<template>
  <b-modal
    id="modal-Edit"
    ref="dialog"
    :title="modaltitle"
    hide-footer
    size="xs"
    centered
  >
    <div class="row">
      <div class="col-md-12">
        <div class="card">
          <div class="card-body">
            <form @submit.prevent="handleSubmit">
              <div class="row">
                <div class="input-group">
                  <div class="input-group-text col-sm-6">Weighbridge Name:</div>
                  <multiselect
                    v-model="wb"
                    :options="wbs"
                    class="form-control"
                  ></multiselect>
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
        <b-button variant="dark" @click="updatewb()"
          >Change Weighbridge
        </b-button>
      </div>
    </div>
  </b-modal>
</template>
<script>
import Swal from "sweetalert2";
import Multiselect from "vue-multiselect";
import axios from "axios";
export default {
  data() {
    return {
      modaltitle: "Select Weighbridge",
      wb: "",
      editWb:false,
      wbs: ["A", "B"],
    };
  },
  components: {
    Multiselect,
  },
  watch:{
    editWb(){
      var curentuser=JSON.parse(localStorage.user).email
      var currentdate=new Date();
    const data={
      datetime:currentdate,
      useremail:curentuser,
      application:window.navigator.userAgent,
      details:`Changed Weighbridge to '${this.wb}'`,
      computer:window.localStorage.clientip
    }
     axios
     .post(window.$http + "AuditLogs",data, { headers: window.$headers })
     .then(response=>{
       console.log(response.data)
     })
     .catch(e=>{
       console.log(e)
     })
    },
  },
  mounted() {
    this.wb = window.$weighb;
    //alert(this.wb);
    if (typeof this.wb === "undefined") {
      //alert(this.wb);
      this.$root.$emit("bv::show::modal", "modal-Edit");
    }
  },
  methods: {
    updatewb() {
      window.$weighb = this.wb;
      localStorage.weighb = window.$weighb;
      Swal.fire("Weighbridge Change to " + this.wb).then(() => {
        this.$root.$emit("bv::hide::modal", "modal-Edit");
        this.editWb=true;
      });
    },
  },
};
</script>