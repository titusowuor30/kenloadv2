<script>
/**
 * Stat component
 */
export default {
  methods: {
    goToVehicleReg() {
      this.$router.push("/lswim");
    },
  },
  data() {
    return {};
  },
};
</script>

<template>
  <div class="row">
    <div class="col-md-12 col-xl-12">
      <form class="row row-cols-lg-auto gx-3 gy-2 align-items-center">
        <div class="col-12" style="width: 50%">
          <label class="visually-hidden" for="specificSizeInputGroupUsername"
            >No Plate</label
          >
          <div class="input-group">
            <div class="input-group-text">Vehicle Registration No:</div>
            <input
              type="text"
              class="form-control"
              id="specificSizeInputGroupUsername"
              placeholder="No Plate"
            />
          </div>
        </div>

        <div class="col-12" style="width: 20%">
          <b-button
            variant="dark"
            style="background-color: black; color: yellow"
          >
            Edit Number Plate
            <i class="uil uil-edit ms-2"></i>
          </b-button>
        </div>
        <div class="col-12" style="width: 15%">
          <b-button
            variant="primary"
            style="background-color: black; color: yellow; float: right"
          >
            Weigh
            <i class="uil uil-arrows-shrink-h ms-2"></i>
          </b-button>
        </div>
        <div class="col-12" style="width: 15%">
          <b-button
            @click="goToVehicleReg()"
            variant="primary"
            style="background-color: black; color: yellow; float: right"
          >
            Next
            <i class="uil uil-angle-double-right ms-2"></i>
          </b-button>
        </div>
      </form>
    </div>
    <!-- End Form Layout -->
  </div>
  <!-- end row-->
</template>
