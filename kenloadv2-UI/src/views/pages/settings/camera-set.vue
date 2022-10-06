<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";

import appConfig from "@/app.config";
import Multiselect from "vue-multiselect";

/**
 * Product-detail component
 */
export default {
  components: { Layout, PageHeader, Multiselect },
  page: {
    title: "Camera Settings",
    meta: [
      {
        name: "camera settings",
        content: appConfig.description,
      },
    ],
  },
  
  created() {},
  data() {
    return {
      wb: "",
      wbs: ["A", "B"],
      position: "",
      positions: ["Front", "Rear", "Side"],
      cameraname: "",
      ipcamera: "",
      port: 80,
      Protocol: "",
      Protocols: ["http", "https", "rtsp", "rtp", "udp"],

      title: "Camera Settings",
      items: [
        {
          text: "Camera Settings",
        },
        {
          text: "Camera Settings",
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
      <div class="col-md-4">
        <b-card bg-variant="primary" class="text-white-50">
          <h5 class="mt-0 mb-4 text-white">
            <i class="mdi mdi-bullseye-arrow me-3"></i>40
          </h5>
          <p class="text-white">ANPR Cameras</p>
        </b-card>
      </div>
      <div class="col-md-4">
        <b-card bg-variant="warning" class="text-black-50">
          <h5 class="mt-0 mb-4 text-black">
            <i class="mdi mdi-alert-outline me-3"></i>2
          </h5>
          <p class="text-black">Number of Camera(s)</p>
        </b-card>
      </div>
      <div class="col-md-4">
        <b-card bg-variant="info" class="text-white-50">
          <h5 class="mt-0 mb-4 text-white">
            <i class="mdi mdi-alert-circle-outline me-3"></i>
            23
          </h5>
          <p class="text-white">Number of plates</p>
        </b-card>
      </div>
    </div>
    <div class="row">
      <div class="card changebg">
        <div class="card-body">
          <div class="row justify-content-between">
            <div class="col-sm-6">
              <button
                class="btn btn-primary waves-effect waves-light uil-export"
                @click="getrpt()"
              >
                Export to CSV
              </button>
            </div>

            <div class="col-sm-2">
              <button
                @click="printpdf('l')"
                v-b-modal.modal-Print
                class="btn btn-primary waves-effect waves-light mdi-file-pdf"
              >
                Print PDF
              </button>
            </div>

            <div class="col-sm-2">
              <button
                v-b-tooltip.hover
                v-b-modal.modal-AddCamera
                @click="clearvalues()"
                class="btn btn-primary waves-effect waves-light mdi-file-pdf"
              >
                Add Camera
              </button>
            </div>
          </div>
        </div>
      </div>
      <b-modal
        id="modal-AddCamera"
        ref="dialog"
        :title="modaltitle"
        hide-footer
        size="xl"
        centered
      >
        <div class="card">
          <div class="card-body">
            <div class="row">
              <div class="col-xl-5">
                <div class="mt-4 mt-xl-3 pl-xl-4">
                  <h4 class="font-size-20 mb-3">{{ cameraname }}</h4>
                  <div class="text-muted">
                    <span class="badge bg-success font-size-14 me-1"
                      ><i class="mdi mdi-star"></i> {{ position }} View</span
                    >
                  </div>

                  <div class="mt-4">
                    <div class="col-lg-12 ms-lg-auto">
                      <div class="mt-5 mt-lg-4">
                        <form>
                          <div class="input-group mb-1">
                            <div class="input-group-text row col-sm-6">
                              Camera Position:
                            </div>
                            <multiselect
                              v-model="position"
                              :options="positions"
                              class="form-control"
                            ></multiselect>
                          </div>

                          <div class="input-group mb-1">
                            <div class="input-group-text row col-sm-6">
                              Weighbridge Name:
                            </div>
                            <multiselect
                              v-model="wb"
                              :options="wbs"
                              class="form-control"
                            ></multiselect>
                          </div>

                          <div class="input-group mb-1">
                            <div class="input-group-text row col-sm-6">
                              Camera Name:
                            </div>
                            <input
                              v-model="cameraname"
                              type="text"
                              class="form-control"
                              id="horizontal-firstname-input"
                              placeholder="Camera Name"
                            />
                          </div>

                          <div class="input-group mb-1">
                            <div class="input-group-text row col-sm-6">
                              IP Camera:
                            </div>
                            <input
                              v-model="ipcamera"
                              type="text"
                              class="form-control"
                              id="horizontal-firstname-input"
                              placeholder="192.89.168.102"
                            />
                          </div>

                          <div class="input-group mb-1">
                            <div class="input-group-text row col-sm-6">
                              Port:
                            </div>
                            <input
                              v-model="port"
                              type="number"
                              class="form-control"
                              id="horizontal-firstname-input"
                              placeholder="554"
                            />
                          </div>

                          <div class="input-group mb-1">
                            <div class="input-group-text row col-sm-6">
                              File:
                            </div>
                            <input
                              v-model="file"
                              type="number"
                              class="form-control"
                              id="horizontal-firstname-input"
                              placeholder="/jpg/image.jpg?size=3"
                            />
                          </div>

                          <div class="input-group mb-1">
                            <div class="input-group-text row col-sm-6">
                              Protocol:
                            </div>
                            <multiselect
                              v-model="Protocol"
                              :options="Protocols"
                              class="form-control"
                            ></multiselect>
                          </div>

                          <div class="row justify-content-end">
                            <div class="col-sm-9">
                              <div class="mb-4">
                                <button
                                  type="submit"
                                  class="btn btn-primary w-md"
                                >
                                  Add Camera
                                </button>
                              </div>
                            </div>
                          </div>
                        </form>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="col-xl-7">
                <div class="product-detail">
                  <b-tabs pills vertical nav-wrapper-class="col-3">
                    <b-tab>
                      <template v-slot:title>
                        <img
                          src="@/assets/images/settings/cameraview.jpg"
                          alt
                          class="img-fluid mx-auto d-block tab-img rounded"
                        />
                      </template>
                      <div class="product-img">
                        <img
                          src="@/assets/images/settings/cameraview.jpg"
                          alt
                          class="img-fluid mx-auto d-block"
                        />
                      </div>
                    </b-tab>
                    <b-tab>
                      <template v-slot:title>
                        <img
                          src="@/assets/images/settings/cameraview.jpg"
                          alt
                          class="img-fluid mx-auto d-block tab-img rounded"
                        />
                      </template>
                      <div class="product-img">
                        <img
                          src="@/assets/images/settings/cameraview.jpg"
                          alt
                          class="img-fluid mx-auto d-block"
                        />
                      </div>
                    </b-tab>
                    <b-tab>
                      <template v-slot:title>
                        <img
                          src="@/assets/images/settings/cameraview.jpg"
                          alt
                          class="img-fluid mx-auto d-block tab-img rounded"
                        />
                      </template>
                      <div class="product-img">
                        <img
                          src="@/assets/images/settings/cameraview.jpg"
                          alt
                          class="img-fluid mx-auto d-block"
                        />
                      </div>
                    </b-tab>
                    <b-tab>
                      <template v-slot:title>
                        <img
                          src="@/assets/images/settings/cameraview.jpg"
                          alt
                          class="img-fluid mx-auto d-block tab-img rounded"
                        />
                      </template>
                      <div class="product-img">
                        <img
                          src="@/assets/images/settings/cameraview.jpg"
                          alt
                          class="img-fluid mx-auto d-block"
                        />
                      </div>
                    </b-tab>
                  </b-tabs>
                </div>
              </div>
            </div>
            <!-- end row -->
          </div>
        </div>
        <!-- end card -->
      </b-modal>
      <div class="col-lg-12"></div>
      <div class="col-lg-12">
        <div class="row mt-4">
          <div class="col-lg-12">
            <div class="card">
              <div class="card-body">
                <h4 class="card-title">Cameras List</h4>

                <div class="table-responsive">
                  <table class="table table-hover mb-4">
                    <thead>
                      <tr>
                        <th>#</th>
                        <th>Name</th>
                        <th>IP</th>
                        <th>Port</th>
                        <th>File</th>
                        <th>Proto</th>
                      </tr>
                    </thead>
                    <tbody>
                      <tr>
                        <th scope="row">1</th>
                        <td>ANRP</td>
                        <td>192.168.82.102</td>
                        <td>554</td>
                        <td>live.sdp</td>
                        <td>rtsp</td>
                      </tr>
                      <tr>
                        <th scope="row">2</th>
                        <td>ANRP</td>
                        <td>192.168.82.102</td>
                        <td>554</td>
                        <td>live.sdp</td>
                        <td>rtsp</td>
                      </tr>
                      <tr>
                        <th scope="row">3</th>
                        <td>ANRP</td>
                        <td>192.168.82.102</td>
                        <td>554</td>
                        <td>live.sdp</td>
                        <td>rtsp</td>
                      </tr>
                    </tbody>
                  </table>
                </div>
              </div>
            </div>

            <!-- end row -->
          </div>
        </div>
        <!-- end card -->
      </div>
    </div>
    <!-- end row -->
  </Layout>
</template>
