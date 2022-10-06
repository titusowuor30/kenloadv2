<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";

import appConfig from "@/app.config";

/**
 * Product-detail component
 */
export default {
  components: { Layout, PageHeader },
  page: {
    title: "Vehicle-detail",
    meta: [
      {
        name: "description",
        content: appConfig.description,
      },
    ],
  },
  created() {},
  data() {
    return {
      title: "Vehicle-detail",
      items: [
        {
          text: "Vehicle List",
        },
        {
          text: "Vehicle-detail",
          active: true,
        },
      ],
    };
  },
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <div class="row">
      <div class="col-lg-12">
        <div class="card">
          <div class="card-body">
            <div class="row">
              <div class="col-xl-5">
                <div class="product-detail">
                  <b-tabs pills vertical nav-wrapper-class="col-3">
                    <b-tab>
                      <template v-slot:title>
                        <img
                          src="@/assets/images/vehicles/truck.jpg"
                          alt
                          class="img-fluid mx-auto d-block tab-img rounded"
                        />
                      </template>
                      <div class="product-img">
                        <img
                          src="@/assets/images/vehicles/truck.jpg"
                          alt
                          class="img-fluid mx-auto d-block"
                        />
                      </div>
                    </b-tab>
                    <b-tab>
                      <template v-slot:title>
                        <img
                          src="@/assets/images/vehicles/truck.jpg"
                          alt
                          class="img-fluid mx-auto d-block tab-img rounded"
                        />
                      </template>
                      <div class="product-img">
                        <img
                          src="@/assets/images/vehicles/truck.jpg"
                          alt
                          class="img-fluid mx-auto d-block"
                        />
                      </div>
                    </b-tab>
                    <b-tab>
                      <template v-slot:title>
                        <img
                          src="@/assets/images/vehicles/truck.jpg"
                          alt
                          class="img-fluid mx-auto d-block tab-img rounded"
                        />
                      </template>
                      <div class="product-img">
                        <img
                          src="@/assets/images/vehicles/truck.jpg"
                          alt
                          class="img-fluid mx-auto d-block"
                        />
                      </div>
                    </b-tab>
                    <b-tab>
                      <template v-slot:title>
                        <img
                          src="@/assets/images/vehicles/truck.jpg"
                          alt
                          class="img-fluid mx-auto d-block tab-img rounded"
                        />
                      </template>
                      <div class="product-img">
                        <img
                          src="@/assets/images/vehicles/truck.jpg"
                          alt
                          class="img-fluid mx-auto d-block"
                        />
                      </div>
                    </b-tab>
                  </b-tabs>
                </div>
              </div>

              <div class="col-xl-7">
                <div class="mt-4 mt-xl-3 pl-xl-4">
                  <h5 class="font-size-14">
                    <a href="#" class="text-muted">TRUCK</a>
                  </h5>
                  <h4 class="font-size-20 mb-3">KBC 982Q</h4>
                  <div class="text-muted">
                    <span class="badge bg-success font-size-14 me-1"
                      >charged</span
                    >
                  </div>

                  <p class="text-muted mb-4 mb-4 pt-2">
                    The truck was overweight
                  </p>

                  <div class="mt-3">
                    <h5 class="font-size-14 mb-3">
                      <i
                        class="
                          uil uil-location-pin-alt
                          font-size-20
                          text-primary
                          align-middle
                          me-2
                        "
                      ></i>
                      Delivery location
                    </h5>

                    <div class="form-inline">
                      <div class="input-group mb-3">
                        <input
                          type="text"
                          class="form-control"
                          placeholder="Enter Delivery pincode "
                        />
                        <div class="input-group-append">
                          <button class="btn btn-light" type="button">
                            Check
                          </button>
                        </div>
                      </div>
                    </div>
                  </div>

                  <div class="row">
                    <div class="col-lg-5 col-sm-4">
                      <div class="mt-3">
                        <h5 class="font-size-14 mb-3">Change Status :</h5>

                        <div class="form-inline">
                          <select class="custom-select w-sm">
                            <option
                              value="prosecuted"
                              class="badge bg-danger font-size-12 m-3"
                            >
                              Prosecuted
                            </option>
                            <option
                              value="charged"
                              selected
                              class="badge bg-success font-size-12 m-3"
                            >
                              Charged
                            </option>
                            <option
                              value="prohibited"
                              class="badge bg-warning font-size-12 m-3"
                            >
                              Prohibited
                            </option>
                          </select>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <!-- end row -->

            <div class="mt-4">
              <h5 class="font-size-14 mb-3">Vehicle description:</h5>
              <div class="product-desc">
                <b-tabs
                  class="nav-tabs-custom"
                  content-class="border border-top-0 p-4"
                >
                  <b-tab title="Description">
                    <div class="row">
                      <div class="col-sm-3 col-md-2">
                        <div>
                          <img
                            :src="'@/assets/images/product/img-6.png'"
                            alt=""
                            class="img-fluid mx-auto d-block"
                          />
                        </div>
                      </div>
                      <div class="col-sm-9 col-md-10">
                        <div class="text-muted p-2">
                          <p>Reasons for being prosecuted</p>
                          <p>Conditons checked</p>
                          <p>Printing a ticket for the same</p>

                          <div>
                            <ul
                              class="list-unstyled product-desc-list text-muted"
                            >
                              <li>
                                <i
                                  class="
                                    mdi mdi-circle-medium
                                    me-1
                                    align-middle
                                  "
                                ></i>
                                No side mirrors
                              </li>
                              <li>
                                <i
                                  class="
                                    mdi mdi-circle-medium
                                    me-1
                                    align-middle
                                  "
                                ></i>
                                No authorized tyres
                              </li>
                              <li>
                                <i
                                  class="
                                    mdi mdi-circle-medium
                                    me-1
                                    align-middle
                                  "
                                ></i>
                                overloading the axels
                              </li>
                            </ul>
                          </div>
                        </div>
                      </div>
                    </div>
                  </b-tab>
                  <b-tab title="Specifications" active>
                    <div class="table-responsive">
                      <table class="table table-nowrap mb-0">
                        <tbody>
                          <tr>
                            <th scope="row" style="width: 20%">Drivers Name</th>
                            <td>Ambrose Mbayi</td>
                          </tr>
                          <tr>
                            <th scope="row">Date</th>
                            <td>12 May 2021</td>
                          </tr>

                          <tr>
                            <th scope="row">Duty Manager</th>
                            <td>Zeph Adar</td>
                          </tr>
                        </tbody>
                      </table>
                    </div>
                  </b-tab>
                </b-tabs>
              </div>
            </div>
          </div>
        </div>
        <!-- end card -->
      </div>
    </div>
    <!-- end row -->
  </Layout>
</template>
