<script>
import DatePicker from "vue2-datepicker";
export default {
  props: {
    vehicleTagsData: {
      type: Array,
      default: () => {
        return [];
      },
    },
  },
  components: {
    DatePicker,
  },
  data() {
    return {
      autoClose: "",
      totalRows: 1,
      currentPage: 1,
      perPage: 10,
      pageOptions: [1, 10, 25, 50, 100],
      filter: null,
      filterOn: [],
      sortBy: "id",
      sortDesc: false,
      fields: [
        {
          key: "id",
          label: "#",
          sortable: true,
        },
        {
          key: "registration_no",
          label: "Registration No",
          sortable: true,
        },
        {
          key: "date",
          label: "Date",
          sortable: true,
        },
        {
          key: "reason",
          label: "Reason",
          sortable: true,
        },
        {
          key: "status",
          label: "Status",
          sortable: true,
        },
        {
          key: "action",
          label: "Action",
          sortable: true,
        },
      ],
    };
  },
  computed: {
    /**
     * Total no. of records
     */
    rows() {
      return this.vehicleTagsData.length;
    },
  },

  methods: {
    /**
     * Search the table data with search input
     */
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
    },
    onSaveTagEditDetails() {},
    handleSubmit() {
      console.log("Error on submit");
    },
  },
};
</script>

<template>
  <div class="row">
    <div class="card p-0">
      <div class="col-sm-12">
        <div class="row">
          <div class="col-sm-3">
            <span>From:</span>
            <date-picker
              value="2021-05-17"
              v-model="autoClose"
              :first-day-of-week="1"
              lang="en"
            ></date-picker>
          </div>
          <div class="col-sm-3">
            <span>To:</span>
            <date-picker
              value="2021-05-17"
              v-model="autoClose"
              :first-day-of-week="1"
              lang="en"
            ></date-picker>
          </div>
        </div>
      </div>
      <div class="col-sm-12">
        <div class="card">
          <div class="card-body changebg">
            <b-card-title>
              <h5 class="card-title">Route (Nairobi-Mombasa);</h5>
            </b-card-title>

            <div class="row">
              <div class="col-12">
                <div>
                  <div class="float-end">
                    <form class="d-inline-flex mb-3"></form>
                  </div>
                </div>
                <div
                  class="
                    table table-centered
                    datatable
                    dt-responsive
                    nowrap
                    table-card-list
                    dataTable
                    no-footer
                    dtr-inline
                  "
                >
                  <div class="row">
                    <div class="col-sm-12 col-md-6">
                      <div id="tickets-table_length" class="dataTables_length">
                        <label
                          class="d-inline-flex align-items-center fw-normal"
                        >
                          Show&nbsp;
                          <b-form-select
                            v-model="perPage"
                            size="sm"
                            :options="pageOptions"
                          ></b-form-select
                          >&nbsp;entries
                        </label>
                      </div>
                    </div>
                    <!-- Search -->
                    <div class="col-sm-12 col-md-6">
                      <div
                        id="tickets-table_filter"
                        class="dataTables_filter text-md-end"
                      >
                        <label
                          class="d-inline-flex align-items-center fw-normal"
                        >
                          Search:
                          <b-form-input
                            v-model="filter"
                            type="search"
                            placeholder="Search..."
                            class="form-control form-control-sm ms-2"
                          ></b-form-input>
                        </label>
                      </div>
                    </div>
                    <!-- End search -->
                  </div>
                  <!-- Table -->

                  <b-table
                    striped
                    hover
                    :items="vehicleTagsData"
                    :fields="fields"
                    responsive="sm"
                    :per-page="perPage"
                    :current-page="currentPage"
                    :sort-by.sync="sortBy"
                    :sort-desc.sync="sortDesc"
                    :filter="filter"
                    :filter-included-fields="filterOn"
                    @filtered="onFiltered"
                  >
                    <template v-slot:cell(state)>
                      <ul class="list-inline mb-0">
                        <li class="list-inline-item">
                          <a
                            href="javascript:void(0);"
                            class="px-2 text-danger"
                            v-b-tooltip.hover
                            title="state"
                          >
                            <span>
                              <i
                                class="
                                  uil uil-check-circle
                                  d-block
                                  display-0
                                  mt-0
                                  mb-0
                                  text-success
                                "
                              ></i>
                            </span>
                          </a>
                        </li>
                      </ul>
                    </template>

                    <template v-slot:cell(action)>
                      <ul class="list-inline mb-0">
                        <li class="list-inline-item">
                          <button
                            v-b-modal.modal-1
                            style="
                              color: yellow;
                              background-color: black;
                              border-radius: 5px;
                            "
                          >
                            <i class="uil uil-edit me-2"></i>
                            Edit
                          </button>
                        </li>
                      </ul>
                    </template>
                  </b-table>
                </div>
                <div class="row">
                  <div class="col">
                    <div
                      class="
                        dataTables_paginate
                        paging_simple_numbers
                        float-end
                      "
                    >
                      <ul class="pagination pagination-rounded">
                        <!-- pagination -->
                        <b-pagination
                          v-model="currentPage"
                          :total-rows="rows"
                          :per-page="perPage"
                        ></b-pagination>
                      </ul>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <div class="row">
      <b-modal id="modal-1" title="Edit Tag" hide-footer size="xl" centered>
        <div class="card-body">
          <div class="input-group">
            <div class="input-group-text col-4">Action</div>
            <select name="reason" id="reason">
              <option value="1">Allow to proceed</option>
              <option value="2">Yard</option>
              <option value="3">Pay Fine</option>
              <option value="4">Re-distribute Load</option>
            </select>
          </div>
          <div class="input-group mt-2">
            <div class="input-group-text col-4">Reason:</div>
            <input
              type="text"
              height="500px"
              class="form-control"
              placeholder="Enter note"
              id="description"
              name="description"
              required
            />
          </div>

          <form>
            <div class="row">
              <div class="col-lg-12 mt-3 mb-3">
                <div class="text-end">
                  <b-button v-on:click="onSaveTagEditDetails()"> Save</b-button>
                </div>
              </div>
            </div>
          </form>
        </div>
      </b-modal>
    </div>
  </div>
</template>

<style scoped>
/* ::v-deep/ .table > tbody > tr > td { */
.table > tbody > tr > td {
  padding: 0px;
  margin: 0px;
  height: 2px;
}
</style>
