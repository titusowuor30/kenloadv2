<script>
import Layout from "../../layouts/main";
import PageHeader from "@/components/page-header";
import appConfig from "@/app.config";

/**
 * User list component
 */
export default {
  components: { Layout, PageHeader },
  page: {
    title: "Special Release",
    meta: [
      {
        name: "description",
        content: appConfig.description,
      },
    ],
  },
  data() {
    return {
      title: "Special Release",
      items: [
        {
          text: "Contacts",
        },
        {
          text: "User List",
          active: true,
        },
      ],
      userList: [
        {
          id: 1,
          profile: require("@/assets/images/users/avatar-2.jpg"),
          name: "Simon Ryles",
          position: "Full Stack Developer",
          email: "SimonRyles@minible.com",
        },
        {
          id: 2,
          profile: require("@/assets/images/users/avatar-3.jpg"),
          name: "Marion Walker",
          position: "Frontend Developer",
          email: "MarionWalker@minible.com",
        },
        {
          id: 3,
          name: "Frederick White",
          position: "Frontend Developer",
          email: "MarionWalker@minible.com",
        },
        {
          id: 4,
          profile: require("@/assets/images/users/avatar-4.jpg"),
          name: "Shanon Marvin",
          position: "Backend Developer",
          email: "ShanonMarvin@minible.com",
        },
        {
          id: 5,
          name: "Mark Jones",
          position: "Frontend Developer",
          email: "MarkJones@minible.com",
        },
        {
          id: 6,
          profile: require("@/assets/images/users/avatar-7.jpg"),
          name: "Patrick Petty",
          position: "UI/UX Designer",
          email: "PatrickPetty@minible.com",
        },
        {
          id: 7,
          profile: require("@/assets/images/users/avatar-8.jpg"),
          name: "Marilyn Horton",
          position: "Backend Developer",
          email: "MarilynHorton@minible.com",
        },
        {
          id: 8,
          profile: require("@/assets/images/users/avatar-2.jpg"),
          name: "Neal Womack",
          position: "Full Stack Developer",
          email: "NealWomack@minible.com",
        },
        {
          id: 9,
          profile: require("@/assets/images/users/avatar-2.jpg"),
          name: "Steven Williams",
          position: "Frontend Developer",
          email: "StevenWilliams@minible.com",
        },
      ],
      totalRows: 1,
      currentPage: 1,
      perPage: 10,
      pageOptions: [10, 25, 50, 100],
      filter: null,
      filterOn: [],
      sortBy: "age",
      sortDesc: false,
      fields: [
        {
          key: "check",
          label: "",
        },
        {
          key: "name",
        },
        {
          key: "position",
        },
        {
          key: "email",
        },
        "action",
      ],
    };
  },
  computed: {
    /**
     * Total no. of records
     */
    rows() {
      return this.userList.length;
    },
  },
  mounted() {
    // Set the initial number of items
    this.totalRows = this.items.length;
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
  },
  middleware: "authentication",
};
</script>

<template>
  <Layout>
    <PageHeader :title="title" :items="items" />

    <div class="row">
      <div class="col-12">
        <div class="card">
          <div class="card-body">
            <div class="row">
              <div class="col-md-6">
                <div class="mb-3">
                  <a
                    href="/add-vehicle"
                    class="btn"
                    style="background-color: yellow"
                    ><i class="mdi mdi-plus me-2"></i> Add New</a
                  >
                </div>
              </div>
            </div>
            <div class="row mt-4">
              <div class="col-sm-12 col-md-6">
                <div id="tickets-table_length" class="dataTables_length">
                  <label
                    class="d-inline-flex align-items-center"
                    style="color: black"
                  >
                    Show&nbsp;
                    <b-form-select
                      style="color: black"
                      class="m-2"
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
                  <label class="d-inline-flex align-items-center">
                    <b-form-input
                      v-model="filter"
                      type="search"
                      placeholder="Search..."
                      class="form-control rounded bg-light border-0 ms-2"
                    ></b-form-input>
                  </label>
                </div>
              </div>
              <!-- End search -->
            </div>
            <!-- Table -->
            <div>
              <div class="table-responsive">
                <table
                  class="table table-nowrap table-hover mb-0"
                  style="color: black"
                >
                  <thead>
                    <tr>
                      <th scope="col">#</th>
                      <th scope="col">Duty Manager</th>
                      <th scope="col">Date</th>
                      <th scope="col">Vehicle Type</th>
                      <th scope="col">Vehicle Number</th>
                      <th scope="col">Status</th>

                      <th scope="col" style="width: 120px">Action</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr>
                      <th scope="row">01</th>
                      <td>
                        <a href="/profile" class="text-dark">Ambrose Mbayi</a>
                      </td>
                      <td>18 Jun, 2020</td>
                      <td>Truck</td>
                      <td>KCW 120Q</td>

                      <td>
                        <span class="badge bg-danger ms-1">Prosecuted</span>
                      </td>
                      <td>
                        <b-dropdown
                          right
                          toggle-class="text-muted font-size-18 px-2 p-0"
                          variant="white"
                          menu-class="dropdown-menu-end"
                        >
                          <template v-slot:button-content>
                            <i class="uil uil-ellipsis-v"></i>
                          </template>

                          <a class="dropdown-item" href="/vehicle-details"
                            >View
                          </a>
                          <a class="dropdown-item" href="#">Delete</a>
                        </b-dropdown>
                      </td>
                    </tr>
                    <tr>
                      <th scope="row">02</th>
                      <td>
                        <a href="#" class="text-dark">Ambrose Mbayi</a>
                      </td>
                      <td>18 Jun, 2020</td>
                      <td>Truck</td>
                      <td>KCW 120Q</td>

                      <td>
                        <span class="badge bg-success ms-1">Charged</span>
                      </td>
                      <td>
                        <b-dropdown
                          right
                          toggle-class="text-muted font-size-18 px-2 p-0"
                          variant="white"
                          menu-class="dropdown-menu-end"
                        >
                          <template v-slot:button-content>
                            <i class="uil uil-ellipsis-v"></i>
                          </template>

                          <a class="dropdown-item" href="/vehicle-details"
                            >View</a
                          >
                          <a class="dropdown-item" href="#">Delete</a>
                        </b-dropdown>
                      </td>
                    </tr>
                    <tr>
                      <th scope="row">03</th>
                      <td>
                        <a href="#" class="text-dark">Ambrose Mbayi</a>
                      </td>
                      <td>18 Jun, 2020</td>
                      <td>Truck</td>
                      <td>KCW 120Q</td>

                      <td>
                        <span class="badge bg-warning ms-1">Prohibited</span>
                      </td>
                      <td>
                        <b-dropdown
                          right
                          toggle-class="text-muted font-size-18 px-2 p-0"
                          variant="white"
                          menu-class="dropdown-menu-end"
                        >
                          <template v-slot:button-content>
                            <i class="uil uil-ellipsis-v"></i>
                          </template>

                          <a class="dropdown-item" href="/vehicle-details"
                            >View</a
                          >
                          <a class="dropdown-item" href="#">Delete</a>
                        </b-dropdown>
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>

            <div class="row">
              <div class="col">
                <div
                  class="dataTables_paginate paging_simple_numbers float-end"
                >
                  <ul class="pagination pagination-rounded mb-0">
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
  </Layout>
</template>
