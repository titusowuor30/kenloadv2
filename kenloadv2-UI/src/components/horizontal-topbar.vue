<script>
import simplebar from "simplebar-vue";
/*import { menuItems } from "./horizontal-menu";*/
import store from "@/state/store";
import axios from "axios";

import { layoutComputed, kenloadv2authMethods } from "@/state/helpers";

/**
 * Horizontal-topbar component
 */
export default {
  components: { simplebar },
  props: {
    type: {
      type: String,
      required: true,
    },
    width: {
      type: String,
      required: true,
    },
  },
  data() {
    return {
      stationname: "",
      currentUsername:
        JSON.parse(localStorage.getItem("user"))
          .username.charAt(0)
          .toUpperCase() +
        JSON.parse(localStorage.getItem("user"))
          .username.slice(1)
          .split("@")[0],
      menuItems: store.getters["screens/getUserScreens"],
      languages: [
        {
          flag: require("@/assets/images/flags/kenya.jpg"),
          language: "en",
          title: "English",
        },
      ],
      current_language: this.$i18n.locale,
      text: null,
      flag: null,
      value: null,
    };
  },
  computed: {
    ...layoutComputed,
    getCurrentUserId() {
      return JSON.parse(localStorage.user).email;
    },
  },
  mounted() {
    //alert(this.getCurrentUserId);
    var tokenString = "";
    var CryptoJS = require("crypto-js");
    try {
      if (localStorage.user.trim() != "") {
        window.$userid = JSON.parse(localStorage.user).email;
        tokenString = CryptoJS.AES.decrypt(
          JSON.parse(localStorage.user).token,
          "mnopqr"
        )
          .toString(CryptoJS.enc.Utf8)
          .trim();
      }
    } catch (e) {
      tokenString = "";
    }

    window.$tokenString = tokenString;
    window.$headers = {
      Authorization: `Bearer ${window.$tokenString}`,
    };
    axios
      .get(window.$http + `permiturl`, {
        headers: window.$headers,
      })
      .then((response) => {
        let orderData = response.data;
        //alert(orderData[0].permiturl);
        window.$permiturl = orderData[0].permiturl;
      })
      .catch(() => {
        //alert(e);
      });

    axios
      .get(window.$http + `systemsettings`, { headers: window.$headers })
      .then((res) => {
        try {
          window.$cameratype = res.data[0].camera;
        } catch (Exception) {
          console.log(window.$cameratype);
        }

        try {
          window.$hswim = res.data[0].hswim;
        } catch (Exception) {
          console.log(window.$hswim);
        }

        try {
          window.$destination = res.data[0].destination;
        } catch (Exception) {
          console.log(window.$destination);
        }

        try {
          window.$origin = res.data[0].origin;
        } catch (Exception) {
          console.log(window.$origin);
        }

        // alert("res.data[0].stationcode2" + window.$station2);
        if (res.data[0].stationcode2.length > 2) {
          axios
            .get(
              window.$http +
                `ClusterWeighbridges/search?cluster_code=` +
                res.data[0].stationcode2,
              {
                headers: window.$headers,
              }
            )
            .then((response) => {
              window.$stationcode2 = res.data[0].stationcode2;

              window.$station2 = response.data[0].cluster_name;
              //alert("res.data[0].stationcode2" + window.$station2);
            })
            .catch(() => {});
        }

        axios
          .get(
            window.$http +
              `ClusterWeighbridges/search?cluster_code=` +
              res.data[0].stationcode,
            {
              headers: window.$headers,
            }
          )
          .then((response) => {
            window.$stationcode = res.data[0].stationcode;
            window.$station = response.data[0].cluster_name;
            this.stationname = window.$station;
          })
          .catch(() => {});
      })
      .catch(() => {});
    this.value = this.languages.find((x) => x.language === this.$i18n.locale);
    this.text = this.value.title;
    this.flag = this.value.flag;

    this.activateParentDropdown();

    this.$router.afterEach(() => {
      this.activateParentDropdown();
    });
  },
  methods: {
    ...kenloadv2authMethods,
    /**
     * remove active and mm-active class
     */
    _removeAllClass(className) {
      const els = document.getElementsByClassName(className);
      while (els[0]) {
        els[0].classList.remove(className);
      }
    },

    activateParentDropdown() {
      const resetParent = (el) => {
        const parent = el.parentElement;
        this._removeAllClass("mm-active");
        this._removeAllClass("mm-show");
        if (parent) {
          parent.classList.remove("active");
          const parent2 = parent.parentElement;
          if (parent2) {
            parent2.classList.remove("active");
            const parent3 = parent2.parentElement;
            if (parent3) {
              parent3.classList.remove("active");
              const parent4 = parent3.parentElement;
              if (parent4) {
                parent4.classList.remove("active");
                const parent5 = parent4.parentElement;
                if (parent5) {
                  parent5.classList.remove("active");
                  const menuelement = document.getElementById(
                    "topnav-menu-content"
                  );
                  if (menuelement !== null)
                    if (menuelement.classList.contains("show"))
                      document
                        .getElementById("topnav-menu-content")
                        .classList.remove("show");
                }
              }
            }
          }
        }
      };
      var links = document.getElementsByClassName("side-nav-link-ref");
      var matchingMenuItem = null;
      for (let i = 0; i < links.length; i++) {
        // reset menu
        resetParent(links[i]);
      }
      for (var i = 0; i < links.length; i++) {
        if (window.location.pathname === links[i].pathname) {
          matchingMenuItem = links[i];
          break;
        }
      }
      if (matchingMenuItem) {
        matchingMenuItem.classList.add("active");
        var parent = matchingMenuItem.parentElement;
        if (parent) {
          parent.classList.add("active");
          const parent2 = parent.parentElement;
          if (parent2) {
            parent2.classList.add("active");
          }
          const parent3 = parent2.parentElement;
          if (parent3) {
            parent3.classList.add("active");
            var childAnchor = parent3.querySelector(".has-dropdown");
            if (childAnchor) childAnchor.classList.add("active");
          }

          const parent4 = parent3.parentElement;
          if (parent4) parent4.classList.add("active");
          const parent5 = parent4.parentElement;
          if (parent5) parent5.classList.add("active");
        }
      }
    },
    /**
     * Full-screen
     */
    initFullScreen() {
      document.body.classList.toggle("fullscreen-enable");
      if (
        !document.fullscreenElement &&
        /* alternative standard method */
        !document.mozFullScreenElement &&
        !document.webkitFullscreenElement
      ) {
        // current working methods
        if (document.documentElement.requestFullscreen) {
          document.documentElement.requestFullscreen();
        } else if (document.documentElement.mozRequestFullScreen) {
          document.documentElement.mozRequestFullScreen();
        } else if (document.documentElement.webkitRequestFullscreen) {
          document.documentElement.webkitRequestFullscreen(
            Element.ALLOW_KEYBOARD_INPUT
          );
        }
      } else {
        if (document.cancelFullScreen) {
          document.cancelFullScreen();
        } else if (document.mozCancelFullScreen) {
          document.mozCancelFullScreen();
        } else if (document.webkitCancelFullScreen) {
          document.webkitCancelFullScreen();
        }
      }
    },
    /**
     * Toggle right-sidebar
     */
    toggleRightSidebar() {
      this.$parent.toggleRightSidebar();
    },
    /**
     * Menu clicked show the submenu
     */
    onMenuClick(event) {
      event.preventDefault();
      const nextEl = event.target.nextElementSibling;
      if (nextEl) {
        const parentEl = event.target.parentNode;
        if (parentEl) {
          parentEl.classList.remove("show");
        }
        nextEl.classList.toggle("show");
      }
      return false;
    },

    /**
     * Returns true or false if given menu item has child or not
     * @param item menuItem
     */
    hasItems(item) {
      return item.subItems !== undefined ? item.subItems.length > 0 : false;
    },
    /**
     * Language set
     */
    setLanguage(locale, country, flag) {
      this.$i18n.locale = locale;
      this.current_language = locale;
      this.text = country;
      this.flag = flag;
    },
    toggleMenu() {
      let element = document.getElementById("topnav-menu-content");
      element.classList.toggle("show");
    },
    logoutUser() {
      this.logout();
      this.$router.push({
        path: "/login",
      });
    },
  },
  watch: {
    type: {
      immediate: true,
      handler(newVal, oldVal) {
        if (newVal !== oldVal) {
          switch (newVal) {
            case "dark":
              document.body.setAttribute("data-topbar", "dark");
              break;
            case "light":
              document.body.setAttribute("data-topbar", "light");
              document.body.removeAttribute("data-layout-size", "boxed");
              break;
            case "colored":
              document.body.setAttribute("data-topbar", "colored");
              document.body.removeAttribute("data-layout-size", "boxed");
              break;
            default:
              document.body.setAttribute("data-topbar", "dark");
              break;
          }
        }
      },
    },
    width: {
      immediate: true,
      handler(newVal, oldVal) {
        if (newVal !== oldVal) {
          switch (newVal) {
            case "boxed":
              document.body.setAttribute("data-layout-size", "boxed");
              break;
            case "fluid":
              document.body.setAttribute("data-layout-mode", "fluid");
              document.body.removeAttribute("data-layout-size");
              break;
            default:
              document.body.setAttribute("data-layout-mode", "fluid");
              break;
          }
        }
      },
    },
  },
};
</script>

<template>
  <header id="page-topbar">
    <div class="navbar-header">
      <div class="d-flex">
        <!-- LOGO -->
        <div class="navbar-brand-box">
          <router-link to="/" class="logo logo-dark">
            <span class="logo-sm">
              <img src="@/assets/images/logo-sm.png" alt height="35" />
            </span>
            <span class="logo-lg">
              <img src="@/assets/images/logo-dark.png" alt height="20" />
            </span>
          </router-link>

          <router-link to="/" class="logo logo-light">
            <span class="logo-sm">
              <img src="@/assets/images/logo-sm.png" alt height="22" />
            </span>
            <span class="logo-lg">
              <img src="@/assets/images/logo-light.png" alt height="60" />
            </span>
          </router-link>
        </div>

        <button
          type="button"
          class="btn btn-sm px-3 font-size-16 d-lg-none header-item"
          data-toggle="collapse"
          data-target="#topnav-menu-content"
          @click="toggleMenu"
        >
          <i class="fa fa-fw fa-bars"></i>
        </button>

        <!-- App Search
        <form class="app-search d-none d-lg-block">
          <div class="position-relative">
            <input
              type="text"
              class="form-control"
              :placeholder="$t('navbar.search.text')"
            />
            <span class="uil-search"></span>
          </div>
        </form>-->
      </div>
      <div class="mt-2">
        <h2 class="bridge__name">
          {{ stationname }}
        </h2>
      </div>
      <div class="d-flex">
        <b-dropdown
          variant="white"
          class="d-inline-block d-lg-none ms-2"
          toggle-class="header-item noti-icon"
          right
          menu-class="dropdown-menu-lg p-0"
        >
          <template v-slot:button-content>
            <i class="uil-search"></i>
          </template>
          <form class="p-3">
            <div class="form-group m-0">
              <div class="input-group">
                <input
                  type="text"
                  class="form-control"
                  :placeholder="$t('navbar.search.text')"
                  aria-label="Recipient's username"
                />
                <div class="input-group-append">
                  <button class="btn btn-primary" type="submit">
                    <i class="mdi mdi-magnify"></i>
                  </button>
                </div>
              </div>
            </div>
          </form>
        </b-dropdown>

        <div class="dropdown d-none d-lg-inline-block ms-1">
          <button
            type="button"
            class="btn header-item noti-icon waves-effect"
            data-toggle="fullscreen"
            @click="initFullScreen"
          >
            <i class="uil-minus-path"></i>
          </button>
        </div>
        <b-dropdown
          variant="white"
          class="dropdown d-inline-block"
          toggle-class="header-item noti-icon"
          right
          menu-class="dropdown-menu-lg p-0 dropdown-menu-end"
        >
          <template v-slot:button-content>
            <i class="uil-bell"></i>
            <span class="badge bg-danger rounded-pill">1</span>
          </template>

          <div class="p-3">
            <div class="row align-items-center">
              <div class="col">
                <h5 class="m-0 font-size-16">
                  {{ $t("navbar.dropdown.notification.text") }}
                </h5>
              </div>
              <div class="col-auto">
                <a href="#!" class="small">{{
                  $t("navbar.dropdown.notification.subtext")
                }}</a>
              </div>
            </div>
          </div>
          <simplebar style="max-height: 230px" data-simplebar>
            <a href class="text-reset notification-item">
              <div class="media">
                <img
                  src="@/assets/images/users/user.jpg"
                  class="me-3 rounded-circle avatar-xs"
                  alt="user-pic"
                />
                <div class="media-body">
                  <h6 class="mt-0 mb-1">Welcome User</h6>
                  <div class="font-size-12 text-muted">
                    This is Kenload version 2
                    <p class="mb-0">
                      <i class="mdi mdi-clock-outline"></i>
                      Now
                    </p>
                  </div>
                </div>
              </div>
            </a>
          </simplebar>
          <div class="p-2 border-top">
            <a
              class="btn btn-sm btn-link font-size-14 btn-block text-center"
              href="javascript:void(0)"
            >
              <i class="uil-arrow-circle-right me-1"></i>
              {{ $t("navbar.dropdown.notification.button") }}
            </a>
          </div>
        </b-dropdown>

        <b-dropdown
          class="d-inline-block"
          toggle-class="header-item"
          right
          variant="white"
        >
          <template v-slot:button-content>
            <img
              class="rounded-circle header-profile-user"
              src="@/assets/images/users/user.jpg"
              alt="Header Avatar"
            />
            <span
              class="d-none d-xl-inline-block ms-1 fw-medium font-size-15"
              >{{ currentUsername }}</span
            >
            <i class="uil-angle-down d-none d-xl-inline-block font-size-15"></i>
          </template>

          <!-- item-->
          <router-link
            class="dropdown-item"
            :to="{
              name: 'update-profile',
              params: { email: getCurrentUserId },
            }"
          >
            <i
              class="
                uil uil-user-circle
                font-size-18
                align-middle
                text-muted
                me-1
              "
            ></i>
            <span class="align-middle">{{
              $t("navbar.dropdown.marcus.list.profile")
            }}</span>
          </router-link>

          <a class="dropdown-item d-block" href="#">
            <i
              class="uil uil-cog font-size-18 align-middle me-1 text-muted"
            ></i>
            <span class="align-middle">{{
              $t("navbar.dropdown.marcus.list.settings")
            }}</span>
            <span class="badge badge-soft-success badge-pill mt-1 ms-2"></span>
          </a>
          <router-link class="dropdown-item" to="/lockscreen">
            <i
              class="uil uil-lock-alt font-size-18 align-middle me-1 text-muted"
            ></i>
            <span class="align-middle">{{
              $t("navbar.dropdown.marcus.list.lockscreen")
            }}</span>
          </router-link>
          <a
            class="dropdown-item"
            href="javascript: void(0);"
            @click="logoutUser"
          >
            <i
              class="
                uil uil-sign-out-alt
                font-size-18
                align-middle
                me-1
                text-muted
              "
            ></i>
            <span class="align-middle">{{
              $t("navbar.dropdown.marcus.list.logout")
            }}</span>
          </a>
        </b-dropdown>

        <!--  <div class="dropdown d-inline-block">
          <button
            type="button"
            class="btn header-item noti-icon right-bar-toggle toggle-right"
            @click="toggleRightSidebar"
          >
            <i class="uil-cog toggle-right"></i>
          </button>
        </div> -->
      </div>
    </div>

    <div class="container-fluid">
      <div class="topnav">
        <nav class="navbar navbar-light navbar-expand-lg topnav-menu">
          <div class="collapse navbar-collapse" id="topnav-menu-content">
            <ul class="navbar-nav">
              <li
                class="nav-item dropdown"
                v-for="(item, index) of menuItems"
                :key="index"
              >
                <router-link
                  tag="a"
                  v-if="!item.subItems"
                  :to="item.link"
                  class="
                    nav-link
                    dropdown-toggle
                    arrow-none
                    side-nav-link-ref
                    me-4
                  "
                >
                  <i :class="`${item.icon} me-2`"></i>
                  {{ $t(item.label) }}
                </router-link>

                <a
                  v-if="item.subItems"
                  class="nav-link dropdown-toggle arrow-none"
                  @click="onMenuClick"
                  href="javascript: void(0);"
                  id="topnav-components"
                  role="button"
                >
                  <i :class="`${item.icon} me-2`"></i>
                  {{ $t(item.label) }}
                  <div class="arrow-down me-2"></div>
                </a>
                <div
                  class="dropdown-menu"
                  aria-labelledby="topnav-dashboard"
                  v-if="hasItems(item)"
                >
                  <template v-for="(subitem, index) of item.subItems">
                    <router-link
                      :key="index"
                      class="col dropdown-item side-nav-link-ref"
                      v-if="!hasItems(subitem)"
                      :to="subitem.link"
                      >{{ $t(subitem.label) }}</router-link
                    >
                    <div class="dropdown" v-if="hasItems(subitem)" :key="index">
                      <a
                        class="dropdown-item"
                        href="javascript: void(0);"
                        @click="onMenuClick"
                      >
                        {{ $t(subitem.label) }}
                        <div class="arrow-down"></div>
                      </a>
                      <div class="dropdown-menu">
                        <router-link
                          v-for="(subSubitem, index) of subitem.subItems"
                          :key="index"
                          :to="subSubitem.link"
                          class="dropdown-item side-nav-link-ref"
                          >{{ $t(subSubitem.label) }}</router-link
                        >
                      </div>
                    </div>
                  </template>
                </div>
              </li>
            </ul>
          </div>
        </nav>
      </div>
    </div>
  </header>
</template>
