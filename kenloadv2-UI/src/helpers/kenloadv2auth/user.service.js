import { authHeader } from "./auth-header";
import store from "@/state/store";
var CryptoJS = require("crypto-js");
import axios from "axios";
import Swal from "sweetalert2";

export const userService = {
  login,
  logout,
  register,
  getAll,
  data() {
    return {
      user_email: "",
    };
  },
};

function login(email, password) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ email, password }),
  };
  store.dispatch("screens/clearScreens");
  const sanitizedScreens = [];
  return fetch(window.$http + "AuthManagement/Login", requestOptions)
    .then(handleResponse)
    .then((user) => {
      console.log(store.getters["screens/getUserScreens"]);
      // login successful if there's a jwt token in the response
      //set token here globally loadUserToken
      axios
        .get(window.$http + `Users/byMail/` + email, {
          headers: { Authorization: `Bearer ${user.token}` },
        })
        .then((response) => {
          //get userroles for the users and loop through
          let userData = response.data;
          let userRoles = userData.userRole.split(",");
          this.user_email = userData.email;
          var currentdate = new Date();
          const data = {
            datetime: currentdate,
            useremail: userData.email,
            application: window.navigator.userAgent,
            details: `Logged In`,
            computer: localStorage.clientip,
          };
          axios
            .post(window.$http + "AuditLogs", data, {
              headers: window.$headers,
            })
            .then((response) => {
              console.log(response.data);
            })
            .catch((e) => {
              console.log(e);
            }); //end
          for (var i = 0; i < userRoles.length; i++) {
            //get screens
            axios
              .get(window.$http + `UserRoles/RoleName/` + userRoles[i], {
                headers: { Authorization: `Bearer ${user.token}` },
              })
              .then((response) => {
                let screensData = response.data;

                let userScreens = screensData.screens.split(",");
                for (var j = 0; j < userScreens.length; j++) {
                  if (!sanitizedScreens.includes(userScreens[j])) {
                    sanitizedScreens.push(userScreens[j]);
                  }
                }
                store.dispatch("screens/loadUserScreens", {
                  userScreen: sanitizedScreens,
                });
              });
          }
        })
        .catch((e) => {
          Swal.fire({
            position: "center",
            icon: "error",
            title: "Could not load your Access Screens Contact Admin" + e,
            showConfirmButton: true,
          }).then((e) => {
            Swal.close(e);
          });
        });
      /*   let accessScreens = [
                        "dashboard",
                        "weighMultideck",
                        "weighLswim",
                        "weighbridgeTickets",
                        "hswim",
                        "auditEvents",
                        "tags",
                        "yardList",
                        "scaleTest",
                        "manualWeighing",
                        "setAllowableTolerance",
                        "anprAssist",
                        "roles",
                        "passwordPolicy",
                        "usersList",
                        "changePassword",
                        "auditLogs",
                        "backupDatabase",
                        "prosecution",
                        "census",
                        "wideLoadInspection",
                        "yardVehicleInspection",
                        "specialReleaseYardList",
                        "specialRelease",
                        "roadAuthority",
                        "clusterWeighbridge",
                        "cargo",
                        "vehicles",
                        "transporters",
                        "drivers",
                        "originDestination",
                        "shifts",
                        "vehicleMake",
                        "axleConfiguration",
                        "routes",
                        "screens",
                        "dollarrate",
                        "reports",
                        "cameraSettings",
                        "prosecutionSettings",
                        "permitSettings",
                        "systemSettings",
                        "healthCheck",
                        "calibrationCertificate"
                        ];
                       
                      for(var i=0;i<accessScreens.length;i++)
                        {
                           store.dispatch("screens/loadUserScreens",{userScreen:accessScreens[i]});
                        }*/

      let encryptToken = CryptoJS.AES.encrypt(
        user.token.trim(),
        "mnopqr",
      ).toString();
      if (user.token) {
        let responseJson = {
          id: user.id,
          username: email,
          name: user.fullname,
          email: email,
          token: encryptToken,
        };
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem("user", JSON.stringify(responseJson));
      }
      return user;
    })
    .then((error) => {
      return error;
    });
}

function logout() {
  // remove user from local storage to log user out
  var currentdate = new Date();
  const data = {
    datetime: currentdate,
    useremail: this.user_email,
    application: window.navigator.userAgent,
    details: `Logged Out`,
    computer: localStorage.clientip,
  };
  axios
    .post(window.$http + "AuditLogs", data, { headers: window.$headers })
    .then((response) => {
      console.log(response.data);
    })
    .catch((e) => {
      console.log(e);
    });
  localStorage.removeItem("user");
}

function register(user) {
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(user),
  };
  return fetch(`/users/register`, requestOptions).then(handleResponse);
}

function getAll() {
  const requestOptions = {
    method: "GET",
    headers: authHeader(),
  };
  return fetch(`/users`, requestOptions).then(handleResponse);
}

function handleResponse(response) {
  return response.text().then((text) => {
    const data = text && JSON.parse(text);
    if (!response.ok) {
      if (response.status === 401) {
        // auto logout if 401 response returned from api
        logout();
        location.reload(true);
      }
      const error = (data && data.message) || response.statusText;
      return Promise.reject(error);
    }
    return data;
  });
}
