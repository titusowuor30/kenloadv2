import "mdb-vue-ui-kit/css/mdb.min.css";
import Vue from "vue";
import router from "./router";
import IdleVue from "idle-vue";
import vco from "v-click-outside";
import VueApexCharts from "vue-apexcharts";
import VueSweetalert2 from "vue-sweetalert2";
import VueSlideBar from "vue-slide-bar";
import Vuelidate from "vuelidate";
import i18n from "./i18n";
import store from "@/state/store";
import axios from "axios";
import "fullcalendar/dist/fullcalendar.css";
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/index.css';


//bootstrap
import { BootstrapVue, IconsPlugin } from "bootstrap-vue";
// Import Bootstrap an BootstrapVue CSS files (order is important)
import "bootstrap/dist/css/bootstrap.css";
import ProgressBar from "vuejs-progress-bar";

//styling for shifts calendar
import "fullcalendar/dist/fullcalendar.css";
var CryptoJS = require("crypto-js");
import App from "./App.vue";
// As a plugin
import VueMask from "v-mask";
Vue.config.productionTip = false;
//API ENDPOINT CONFIG
var href = window.location.href;
var arr = href.split("/");
var arr2 = arr[2].split(":");

window.$localurl = arr2[0] + "";
window.$weighstate = 0;

var ref = arr[0] + "//" + arr2[0] + ":4444";
window.$http = ref + "/api/";
window.$url = arr[0] + "//" + arr2[0] + ":5002";

//alert(ref);
//window.$url = "http://192.168.5.12" + ":5002";
//ref = "http://192.168.4.111:4444";
window.$http = "https://localhost:44365/api/";
//window.$url = "http://localhost" + ":82";
//ref = "http://212.49.81.13:4444";
//window.$url = arr[0] + "//" + arr2[0] + ":80";

//window.$url = "http://localhost" + ":82";
//window.$url = "http://212.49.81.13" + ":5002";
//window.$http = "http://212.49.81.13:4444/api/";
//window.$http = "https://localhost:44365/api/";
//window.$http = "http://192.168.4.22:4444/api/";
//window.$url = "http://192.168.3.22" + ":5002";
//window.$http = "http://209.50.58.236:4445/api/";
//window.$http = "https://localhost:44365/api/";
//window.$http = "http://212.49.81.13:4444/api/";
//window.$http = "http://192.168.50.112:4444/api/";
//window.$url = "http://192.168.50.112:5002";
//window.$url = "http://192.168.4.111:80";
//window.$http = "https://localhost:5001/api/";
window.addEventListener("beforeunload", function(e) {
    alert("loging out");
    delete e["returnValue"];
});
var tokenString = "";

try {
    if (localStorage.user.trim() != "") {
        tokenString = CryptoJS.AES.decrypt(
                JSON.parse(localStorage.user).token,
                "mnopqr",
            )
            .toString(CryptoJS.enc.Utf8)
            .trim();
    }
} catch (e) {
    tokenString = "";
}
//alert(tokenString);
window.$tokenString = tokenString;
window.$headers = {
    Authorization: `Bearer ${window.$tokenString}`,
};

window.$station = "";
window.$allstations = [];
window.$weighbriges = ["BOUNDA", "BOUNDB"];
window.$weighbrigescode = ["A", "B"];
//window.$permiturl = "";
// window.$weighb = "A";
window.$weighb = localStorage.weighb;
window.roadauthority = "KENHA";
//window.$userid = "michael@micna.co.ke";

import * as VueGoogleMaps from "vue2-google-maps";
import Lightbox from "vue-easy-lightbox";

// Make BootstrapVue available throughout your project
Vue.use(BootstrapVue);
// Optionally install the BootstrapVue icon components plugin
Vue.use(IconsPlugin);
Vue.use(Lightbox);
Vue.use(ProgressBar);
Vue.use(VueGoogleMaps, {
    load: {
        key: "AIzaSyAbvyBxmMbFhrzP9Z8moyYr6dCr-pzjhBE",
        libraries: "places",
    },
    installComponents: true,
});

import { initFirebaseBackend } from "./helpers/firebase/authUtils";

import { configurekenloadv2auth } from "./helpers/kenloadv2auth/kenloadv2auth";

const firebaseConfig = {
    apiKey: process.env.VUE_APP_APIKEY,
    authDomain: process.env.VUE_APP_AUTHDOMAIN,
    databaseURL: process.env.VUE_APP_VUE_APP_DATABASEURL,
    projectId: process.env.VUE_APP_PROJECTId,
    storageBucket: process.env.VUE_APP_STORAGEBUCKET,
    messagingSenderId: process.env.VUE_APP_MESSAGINGSENDERID,
    appId: process.env.VUE_APP_APPId,
    measurementId: process.env.VUE_APP_MEASUREMENTID,
};

if (process.env.VUE_APP_DEFAULT_AUTH === "firebase") {
    initFirebaseBackend(firebaseConfig);
} else if (process.env.VUE_APP_DEFAULT_AUTH === "kenloadv2auth") {
    configurekenloadv2auth();
}

//window.$stationcode = "ATMB";
//alert(tokenString);
console.log(
    window.$http +
    `ClusterWeighbridges/search?cluster_code=` +
    window.$stationcode, {
        headers: {
            Authorization: `Bearer ${tokenString}`,
        },
    },
);
var apireq = arr[0] + "//" + arr2[0] + ":5003";
axios
    .get(apireq, { headers: { Authorization: `Bearer ${tokenString}` } })
    .then(() => {})
    .catch(() => {});

apireq = arr[0] + "//" + arr2[0] + ":5004";
axios
    .get(apireq, { headers: { Authorization: `Bearer ${tokenString}` } })
    .then(() => {})
    .catch(() => {});

apireq = arr[0] + "//" + arr2[0] + ":5004";
axios
    .get(apireq, { headers: { Authorization: `Bearer ${tokenString}` } })
    .then(() => {})
    .catch(() => {});

apireq = arr[0] + "//192.168.5.20:5005";
axios
    .get(apireq, { headers: { Authorization: `Bearer ${tokenString}` } })
    .then(() => {})
    .catch(() => {});

apireq = arr[0] + "//" + arr2[0] + ":5005";
axios
    .get(apireq, { headers: { Authorization: `Bearer ${tokenString}` } })
    .then(() => {})
    .catch(() => {});

apireq = arr[0] + "//" + arr2[0] + ":5006";
axios
    .get(apireq, { headers: { Authorization: `Bearer ${tokenString}` } })
    .then(() => {})
    .catch(() => {});

apireq = arr[0] + "//" + arr2[0] + ":5007";
axios
    .get(apireq, { headers: { Authorization: `Bearer ${tokenString}` } })
    .then(() => {})
    .catch(() => {});

axios
    .get(
        window.$http +
        `ClusterWeighbridges/search?cluster_code=` +
        window.$stationcode, { headers: { Authorization: `Bearer ${tokenString}` } },
    )
    .then((response) => {
        let orderData = response.data;
        //alert(this.orderData[0].id);
        window.$allstations = response.data;
        window.$station = orderData[0].cluster_name;
    })
    .catch(() => {});

import "@/assets/scss/app.scss";
const eventsHub = new Vue();
Vue.use(IdleVue, {
    eventEmitter: eventsHub,
    store,
    idleTime: 2000000000, // 3 seconds,
    startAtIdle: true,
});

Vue.component("VueSlideBar", VueSlideBar);
Vue.use(BootstrapVue);
Vue.use(DatePicker)
Vue.use(vco);
Vue.component("apexchart", VueApexCharts);
Vue.use(Vuelidate);
Vue.use(VueSweetalert2);

Vue.use(require("vue-chartist"));
Vue.use(VueMask);

new Vue({
    router,
    store,
    i18n,
    render: (h) => h(App),
}).$mount("#app");