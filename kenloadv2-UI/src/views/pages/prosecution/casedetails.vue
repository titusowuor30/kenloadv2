<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <div class="row">
      <div class="col-lg-12">
        <div class="card">
          <div class="card-body">
            <h4 class="card-title">Users List</h4>

            <div class="modal-body" edit-content>
              <form
                method="post"
                id="EditCompanyForm"
                name="EditCompanyForm"
                enctype="application/x-www-form-urlencoded"
              >
                <ul class="nav nav-tabs">
                  <li class="active">
                    <a data-toggle="tab" href="#home">Driver Details</a>
                  </li>
                  <li><a data-toggle="tab" href="#menu1">Owners Details</a></li>
                  <li>
                    <a data-toggle="tab" href="#menu2">Location Details</a>
                  </li>
                  <li><a data-toggle="tab" href="#menu3">Court Details </a></li>
                  <li>
                    <a data-toggle="tab" href="#menu4">Prohibition Details</a>
                  </li>
                  <li>
                    <a data-toggle="tab" href="#menu5">Transporter's Details</a>
                  </li>
                </ul>

                <div class="tab-content">
                  <div id="home" class="tab-pane fade in active">
                    <h3>Driver Details</h3>
                    <hr />
                    <input
                      type="hidden"
                      class="form-control"
                      placeholder="sesid"
                      id="sesid"
                    />
                    <input
                      type="hidden"
                      class="form-control"
                      placeholder="lp"
                      id="lp2"
                    />
                    <div class="form-group">
                      <label> ID / Passport No. </label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="ID"
                        id="driverid"
                        name="driverid"
                        required
                      />
                    </div>
                    <div class="form-group">
                      <label> Full Names. </label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Driver Full Names"
                        id="dnames"
                        name="dnames"
                        required
                      />
                    </div>
                    <div class="form-group">
                      <label> Driver Surname/Fathers name. </label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Driver Surname"
                        id="dsurname"
                        name="dsurname"
                        required
                      />
                    </div>
                    <div class="form-group">
                      <label> Gender: </label>
                      <select name="dgender" id="dgender">
                        <option value="Male">Male</option>
                        <option value="Female">Female</option>
                      </select>
                    </div>
                    <div class="form-group">
                      <label> Nationality: </label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Driver Nationality"
                        id="dnationality"
                        name="dnationality"
                        required
                      />
                    </div>
                    <div class="form-group">
                      <label> Age: </label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Driver Age"
                        id="dage"
                        name="dage"
                        required
                      />
                    </div>
                    <div class="form-group">
                      <label> Driver Address: </label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Driver Address"
                        id="daddress"
                        name="daddress"
                        required
                      />
                    </div>
                    <!-- <div>
                                                            <input type="hidden" name="id-passport" id="driverid" value="">
                                                            <input type="hidden" name="names" id="dnames" value="">
                                                            <input type="hidden" name="gender" id="dgender" value="">
                                                            <input type="hidden" name="nationality" id="dnationality" value="">
                                                            <input type="hidden" name="age" id="dage" value="">
                                                            <input type="hidden" name="address" id="daddress" value="">
                                                        </div> -->
                  </div>

                  <div id="menu1" class="tab-pane fade in active">
                    <h3>Owners Details</h3>
                    <hr />
                    <div class="form-group">
                      <label> ID / Passport No. </label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Owners id"
                        id="oid"
                        name="oid"
                        required
                      />
                    </div>
                    <div class="form-group">
                      <label> Full Names. </label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Owners Name"
                        id="onames"
                        name="onames"
                        required
                      />
                    </div>
                    <div class="form-group">
                      <label> Owner Surname/Fathers name. </label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Owners Surname"
                        id="osurname"
                        name="osurname"
                        required
                      />
                    </div>
                    <div class="form-group">
                      <label> Gender: </label>
                      <select name="ogender" id="ogender">
                        <option value="Male">Male</option>
                        <option value="Female">Female</option>
                      </select>
                    </div>
                    <div class="form-group">
                      <label> Nationality: </label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Owners Nationality"
                        id="onationality"
                        name="onationality"
                        required
                      />
                    </div>
                    <div class="form-group">
                      <label> Age: </label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Owners Age"
                        id="oage"
                        name="oage"
                        required
                      />
                    </div>
                    <div class="form-group">
                      <label> Owner Address: </label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Owner Address"
                        id="oaddress"
                        name="oaddress"
                        required
                      />
                    </div>
                  </div>
                  <!-- <div>
                                                        <input type="hidden" name="ownerid-passport" id="oid" value="">
                                                        <input type="hidden" name="ownernames" id="onames" value="">
                                                        <input type="hidden" name="ownergender" id="ogender" value="">
                                                        <input type="hidden" name="ownernationality" id="onationality" value="">
                                                        <input type="hidden" name="ownerage" id="oage" value="">
                                                        <input type="hidden" name="owneraddress" id="oaddresss" value="">
                                                    </div> -->

                  <div id="menu2" class="tab-pane fade in active">
                    <h3>Location Details</h3>
                    <hr />
                    <div class="form-group">
                      <label> Vehicle Used Along Road: </label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Vehicle Used Along Road"
                        id="vroad"
                        name="vroad"
                        required
                      />
                    </div>
                    <div class="form-group">
                      <label> District </label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="District"
                        id="district"
                        name="district"
                        required
                      />
                    </div>
                    <div class="form-group">
                      <label> Weighbridge: </label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Weighbridge"
                        id="weighbridge"
                        name="weighbridge"
                        required
                      />
                    </div>
                    <div class="form-group">
                      <label> County: </label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="county"
                        id="county"
                        name="county"
                        required
                      />
                    </div>
                  </div>
                  <!-- <div>
                                                        <input type="hidden" name="vehicleroad" id="vroad" value="">
                                                        <input type="hidden" name="district" id="district" value="">
                                                        <input type="hidden" name="weighbridge" id="weighbridge" value="">
                                                        <input type="hidden" name="county" id="county" value="">
                                                    </div> -->
                  <div id="menu3" class="tab-pane fade in active">
                    <h3>Court Details</h3>
                    <hr />
                    <div class="form-group">
                      <label> Court: </label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Court"
                        id="court"
                        name="court"
                        required
                      />
                    </div>
                    <div class="form-group">
                      <label> Complainant (Police): </label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Complainant (Police)"
                        id="police"
                        name="police"
                        required
                      />
                    </div>
                    <div class="form-group">
                      <label> Police / Traffic Officer: </label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Police / Traffic Officer"
                        id="tpolice"
                        name="tpolice"
                        required
                      />
                    </div>
                    <!-- <div>
                                                        <input type="hidden" name="court" id="court" value="">
                                                        <input type="hidden" name="police" id="police" value="">
                                                        <input type="hidden" name="trafficofficer" id="tpolice" value="">
                                                    </div> -->
                  </div>
                  <div id="menu4" class="tab-pane fade in active">
                    <h3>Prohibition Details</h3>
                    <hr />
                    <div class="form-group">
                      <label> Vehicle Inspector: </label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Vehicle Inspector"
                        id="vi"
                        name="vi"
                      />
                    </div>
                    <div class="form-group">
                      <label> Move Vehicle to: </label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Move Vehicle to"
                        id="moveto"
                        name="moveto"
                      />
                    </div>
                    <div class="form-group">
                      <label> Repair's Needed: </label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Repair's Needed"
                        id="rneeded"
                        name="rneeded"
                      />
                    </div>
                    <div class="form-group">
                      <label> Offload Excess to Reg. No.: </label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Offload Excess to Reg. No."
                        id="offload"
                        name="offload"
                      />
                    </div>
                    <!-- <div>
                                                        <input type="hidden" name="vehicleinspector" id="vi" value="">
                                                        <input type="hidden" name="movevehicle" id="moveto" value="">
                                                        <input type="hidden" name="repairsneeded" id="rneeded" value="">
                                                        <input type="hidden" name="offloadexcesssto" id="offload" value="">
                                                    </div> -->
                  </div>
                  <div id="menu5" class="tab-pane fade in active">
                    <h3>Transporter's Details</h3>
                    <hr />
                    <div class="form-group">
                      <label> Transporter Names: </label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Transporter Names"
                        id="Tnames"
                        name="Tnames"
                        required
                      />
                    </div>
                    <div class="form-group">
                      <label>Transporter Address: </label>
                      <input
                        type="text"
                        class="form-control"
                        placeholder="Transporter Address"
                        id="Taddress"
                        name="Taddress"
                        required
                      />
                    </div>
                  </div>
                </div>
                <button>Prosecute</button>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  </Layout>
</template>

<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

export default {
  name: "Prosecute",
  page: {
    title: "Prosecute",
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
  },
  data() {
    return {
      title: "Prosecute",
      items: [
        {
          text: "USER: Admin",
        },
        {
          text: "List Roles",
          active: true,
        },
      ],
    };
  },
};
</script>

<style>
</style>