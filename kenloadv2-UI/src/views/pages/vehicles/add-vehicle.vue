<script>
import vue2Dropzone from "vue2-dropzone";
//import Multiselect from "vue-multiselect";

import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

import "vue2-dropzone/dist/vue2Dropzone.min.css";
import "vue-multiselect/dist/vue-multiselect.min.css";

/**
 * Add-product component
 */
export default {
  page: {
    title: "Add Product",
    meta: [
      {
        name: "description",
        content: appConfig.description,
      },
    ],
  },
  components: {
    vueDropzone: vue2Dropzone,
    //Multiselect,
    Layout,
    PageHeader,
  },
  data() {
    return {
      title: "Vehicles",
      items: [
        {
          text: "Vehicles",
        },
        {
          text: "Add Vehicle",
          active: true,
        },
      ],
      dropzoneOptions: {
        url: "https://httpbin.org/post",
        thumbnailWidth: 150,
        maxFilesize: 0.5,
        headers: {
          "My-Awesome-Header": "header value",
        },
      },
      value1: null,
      options: [
        "High Quality",
        "Leather",
        "Notifications",
        "Sizes",
        "Different Color",
      ],
    };
  },
  middleware: "authentication",
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />
    <div class="row">
      <div class="col-lg-12">
        <div id="addproduct-accordion" class="custom-accordion">
          <div class="card">
            <a href="#" class="text-dark">
              <div class="p-4">
                <div class="media align-items-center">
                  <div class="me-3"></div>
                  <div class="media-body overflow-hidden">
                    <h5 class="font-size-16 mb-1">Vehicle Details</h5>
                    <p class="text-muted text-truncate mb-0">
                      Fill all information below
                    </p>
                  </div>
                </div>
              </div>
            </a>

            <div class="p-4 border-top">
              <form>
                <div class="row">
                  <div class="col-lg-4">
                    <div class="mb-3">
                      <label for="manufacturername">Vehicle Type</label>
                      <input
                        id="manufacturername"
                        name="manufacturername"
                        type="text"
                        class="form-control"
                      />
                    </div>
                  </div>
                  <div class="col-lg-4">
                    <div class="mb-3">
                      <label for="manufacturerbrand">Vehicle Number</label>
                      <input
                        id="manufacturerbrand"
                        name="manufacturerbrand"
                        type="text"
                        class="form-control"
                      />
                    </div>
                  </div>
                  <div class="col-lg-4">
                    <div class="mb-3">
                      <label for="price">Driver's Name</label>
                      <input
                        id="price"
                        name="price"
                        type="text"
                        class="form-control"
                      />
                    </div>
                  </div>
                </div>
                <div class="row">
                  <div class="col-md-6">
                    <div class="mb-3">
                      <label class="control-label">Status</label>
                      <select class="form-control select2">
                        <option>Select</option>
                        <option value="EL">Charged</option>
                        <option value="FA">Prohibited</option>
                        <option value="FI">Prosecuted</option>
                      </select>
                    </div>
                  </div>
                  <div class="col-md-6">
                    <div class="mb-3">
                      <label class="control-label">Date</label>
                      <input name="date" type="date" class="form-control" />
                    </div>
                  </div>
                </div>

                <div class="mb-3 mb-0">
                  <label for="productdesc">Condition Description</label>
                  <textarea
                    class="form-control"
                    id="productdesc"
                    rows="4"
                  ></textarea>
                </div>
                <div class="col-lg-12">
                  <a href="#" class="text-dark collapsed">
                    <div class="media align-items-center">
                      <div class="media-body overflow-hidden">
                        <h5 class="font-size-16 mb-4">Vehicle Image</h5>
                      </div>
                    </div>
                  </a>

                  <div class="border-top">
                    <vue-dropzone
                      id="dropzone"
                      ref="myVueDropzone"
                      :use-custom-slot="true"
                      :options="dropzoneOptions"
                    >
                      <div class="dropzone-custom-content">
                        <i class="display-4 text-muted bx bxs-cloud-upload"></i>
                        <h4>Drop files here or click to upload.</h4>
                      </div>
                    </vue-dropzone>
                  </div>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="row mb-4">
      <div class="col text-end ms-1">
        <a href="#" class="btn btn-danger">
          <i class="uil uil-times me-1"></i> Cancel
        </a>
        <a href="#" class="btn btn-success ms-1">
          <i class="uil uil-file-alt me-1"></i> Save
        </a>
      </div>
      <!-- end col -->
    </div>
  </Layout>
</template>
