import store from "@/state/store";

export default [
  {
    path: "/login",
    name: "login",
    component: () => import("../views/pages/account/login"),
    meta: {
      beforeResolve(routeTo, routeFrom, next) {
        // If the user is already logged in
        if (store.getters["auth/loggedIn"]) {
          // Redirect to the home page instead
          next({
            name: "home",
          });
        } else {
          // Continue to the login page
          store.dispatch("screens/clearScreens");
          next();
        }
      },
    },
  },
  {
    path: "/register",
    name: "register",
    component: () => import("../views/pages/account/register"),
    meta: {
      beforeResolve(routeTo, routeFrom, next) {
        // If the user is already logged in
        if (store.getters["auth/loggedIn"]) {
          // Redirect to the home page instead
          next({
            name: "home",
          });
        } else {
          // Continue to the login page
          next();
        }
      },
    },
  },
  {
    path: "/forgot-password",
    name: "forgot-password",
    component: () => import("../views/pages/account/forgot-password"),
    meta: {
      beforeResolve(routeTo, routeFrom, next) {
        // If the user is already logged in
        if (store.getters["auth/loggedIn"]) {
          // Redirect to the home page instead
          next({
            name: "home",
          });
        } else {
          // Continue to the login page
          next();
        }
      },
    },
  },
  {
    path: "/logout",
    name: "logout",
    meta: {
      authRequired: true,
      beforeResolve(routeTo, routeFrom, next) {
        if (process.env.VUE_APP_DEFAULT_AUTH === "firebase") {
          store.dispatch("auth/logOut");
        } else if (process.env.VUE_APP_DEFAULT_AUTH === "kenloadv2auth") {
          store.dispatch("authfack/logout");
          store.dispatch("screens/clearScreens");
        }
        const authRequiredOnPreviousRoute = routeFrom.matched.some((route) =>
          route.push("/login"),
        );
        // Navigate back to previous page, or home as a fallback
        next(
          authRequiredOnPreviousRoute
            ? {
                name: "home",
              }
            : {
                ...routeFrom,
              },
        );
      },
    },
  },
  {
    path: "/",
    name: "home",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/dashboard/index"),
  },
  {
    path: "/weigh/:cat",
    name: "weigh",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/weigh/weigh"),
  },
  {
    path: "/PoliceReport",
    name: "PoliceReport",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/reporting/PoliceReport"),
  },
  {
    path: "/PoliceReport",
    name: "Report Issue",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/reporting/PoliceReport"),
  },
  {
    path: "/weigh/:cat",
    name: "lswim",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/weigh/weigh"),
  },
  {
    path: "/lswimvehicleregistration",
    name: "lswimvehicleregistration",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/weigh/lswimvehicleregistration"),
  },
  {
    path: "/hswim",
    name: "hswim",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/weigh/hswim"),
  },
  {
    path: "/manualweighing",
    name: "manualweighing",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/weigh/manualweighing"),
  },
  {
    path: "/scaletest",
    name: "scaletest",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/weigh/scaletest"),
  },
  {
    path: "/tolerance",
    name: "allowabletolerance",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/weigh/allowabletolerance"),
  },
  {
    path: "/anprassist",
    name: "anprassist",
    meta: {
      authRequired: true,
    },
    //component: () => import("../components/widgets/weigh/anprassist"),
    component: () => import("../views/pages/weigh/anpr"),
  },
  {
    path: "/healthcheck",
    name: "healthcheck",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/technical/healthcheck"),
  },
  {
    path: "/ticketlist",
    name: "ticketlist",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/weigh/ticketlist"),
  },
  {
    path: "/autoweigh",
    name: "autoweigh",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/weigh/autoweigh"),
  },
  {
    path: "/calibrationcert",
    name: "calibrationcert",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/technical/calibrationcert"),
  },
  {
    path: "/yardlist",
    name: "yardlist",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/weigh/yardlist"),
  },
  {
    path: "/hswimimages",
    name: "hswimImages",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/weigh/hswimimages"),
  },

  {
    path: "/vehicledetails/:nplates",
    name: "vehicledetails",
    props: true,
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/weigh/vehicledetails"),
  },
  {
    path: "/taglist/:nplates",
    name: "taglist",
    props: true,
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/weigh/taglist"),
  },
  {
    path: "/weigh-vehicle-direction",
    name: "weighVehicleDirection",
    props: true,
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/weigh/weigh-vehicle-direction"),
  },
  {
    path: "/roles",
    name: "roles",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/users/roleslist"),
  },
  {
    path: "/addusers",
    name: "addusers",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/users/addusers"),
  },
  {
    path: "/editroles/:id",
    name: "editroles",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/users/editroles"),
  },
  {
    path: "/listusers",
    name: "listusers",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/users/userslist"),
  },
  {
    path: "/listroles",
    name: "listroles",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/users/roleslist"),
  },
  {
    path: "/backupdb",
    name: "backupdb",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/users/backupdb"),
  },
  {
    path: "/users",
    name: "users",
    meta: {
      authRequired: true,
    },
    component: () => import("../components/widgets/security/addusers"),
  },
  {
    path: "/changepassword/:email",
    name: "changepassword",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/users/changepassword"),
  },
  {
    path: "/passwordpolicy",
    name: "passwordpolicy",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/users/passwordpolicy"),
  },
  {
    path: "/dollarrate",
    name: "dollarrate",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/data/dollarrate"),
  },
  {
    path: "/setscreenpassword",
    name: "setscreenpassword",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/users/setscreenpassword"),
  },
  {
    path: "/screens",
    name: "screens",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/data/screens"),
  },
  {
    path: "/routes",
    name: "routes",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/data/routes"),
  },
  {
    path: "/cargo",
    name: "cargo",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/data/cargo"),
  },
  {
    path: "/transporters",
    name: "transporters",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/data/transporters"),
  },
  {
    path: "/drivers",
    name: "drivers",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/data/drivers"),
  },
  {
    path: "/vehicles",
    name: "vehicles",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/data/vehicles"),
  },
  {
    path: "/axleconfig",
    name: "axleconfig",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/data/axleconfig"),
  },
  {
    path: "/vehiclemake",
    name: "vehiclemake",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/data/vehiclemake"),
  },
  {
    path: "/origindestination",
    name: "origindestination",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/data/origindestination"),
  },
  {
    path: "/roadauthority",
    name: "roadauthority",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/data/roadauthority"),
  },
  {
    path: "/organization",
    name: "organization",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/data/organization"),
  },
  {
    path: "/clusterweighbridge",
    name: "clusterweighbridge",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/data/clusterwb"),
  },
  {
    path: "/prohibit",
    name: "prohibit",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/prosecution/prohibit"),
  },
  {
    path: "/prosecuted",
    name: "prosecuted",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/prosecution/prosecuted"),
  },
  {
    path: "/overloaded",
    name: "overloaded",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/weigh/overloaded"),
  },
  {
    path: "/prohibitnotweighed",
    name: "prohibitnotweighed",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/prosecution/prohibitnotweighed"),
  },
  {
    path: "/redistribution",
    name: "redistribution",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/prosecution/redistribution"),
  },
  {
    path: "/prosecute",
    name: "prosecute",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/prosecution/prosecute"),
  },
  {
    path: "/prosecutenotweighed",
    name: "prosecutenotweighed",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/prosecution/prosecutenotweighed"),
  },
  {
    path: "/release",
    name: "release",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/prosecution/release"),
  },
  {
    path: "/releaseredistribution",
    name: "releaseredistribution",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/prosecution/releaseredistribution"),
  },
  {
    path: "/prosecutiondocs",
    name: "prosecutiondocs",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/prosecution/prosecutiondocs"),
  },
  {
    path: "/prohibition",
    name: "prohibition",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/prosecution/prohibition"),
  },
  {
    path: "/auditlogs",
    name: "auditlogs",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/users/auditlogs"),
  },
  {
    path: "/lcm",
    name: "lcm",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/prosecution/lcm"),
  },
  {
    path: "/clcm",
    name: "clcm",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/prosecution/clcm"),
  },
  {
    path: "/chargesheet",
    name: "chargesheet",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/prosecution/chargesheet"),
  },
  {
    path: "/ownerchargesheet",
    name: "ownerchargesheet",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/prosecution/ownerchargesheet"),
  },
  {
    path: "/eaccert",
    name: "eaccert",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/prosecution/eaccert"),
  },
  {
    path: "/censusclerk",
    name: "censusclerk",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/census/censusclerk"),
  },
  {
    path: "/wideload",
    name: "wideload",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/vehicleinspector/wideload"),
  },
  {
    path: "/yardinspection",
    name: "yardinspection",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/vehicleinspector/yardinspection"),
  },
  {
    path: "/specialrelease",
    name: "specialrelease",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/specialrelease/specialrelease"),
  },
  {
    path: "/specialr",
    name: "specialr",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/specialrelease/specialr"),
  },
  {
    path: "/srdoc",
    name: "srdoc",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/specialrelease/srdoc"),
  },
  {
    path: "/reporting",
    name: "reporting",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/reporting/reporting"),
  },
  {
    path: "/wbregisterreport",
    name: "wbregisterreport",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/reporting/wbregisterreport"),
  },

  //reporting
  {
    path: "/reporting-view",
    name: "reportingView",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/reporting/ReportingView"),
  },
  {
    path: "/daily-hour-statistics",
    name: "daily-hour-statistics",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/reporting/DailyHourStatistics"),
  },
  {
    path: "/hswim-configuration",
    name: "hswim-configuration",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/reporting/HSWIMConfiguration"),
  },
  {
    path: "/wideload2",
    name: "wideload2",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/reporting/WideLoad2"),
  },
  {
    path: "/transport-statement",
    name: "transport-statement",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/reporting/TransportStatement"),
  },
  {
    path: "/prosecution-court-fine",
    name: "prosecution-court-fine",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/reporting/ProsecutionCourtFine"),
  },

  {
    path: "/prosecution-process-list",
    name: "prosecution-process-list",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/reporting/ProsecutionProcessList"),
  },

  // {
  //   path: "/overloaded",
  //   name: "overloaded",
  //   meta: {
  //     authRequired: true,
  //   },
  //   component: () => import("../views/pages/reporting/Overloaded"),
  // },
  {
    path: "/impounded-and-prohibited",
    name: "impounded-and-prohibited",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/reporting/ImpoundedAndProhited"),
  },
  {
    path: "/daily-summary",
    name: "daily-summary",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/reporting/DailySummary"),
  },

  {
    path: "/utility/starter",
    name: "Starter-page",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/utility/starter"),
  },
  {
    path: "/utility/maintenance",
    name: "maintenance",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/utility/maintenance"),
  },
  {
    path: "/utility/comingsoon",
    name: "comingsoon",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/utility/comingsoon"),
  },
  {
    path: "/utility/timeline",
    name: "timeline",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/utility/timeline"),
  },
  {
    path: "/utility/faqs",
    name: "faqs",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/utility/faqs"),
  },
  {
    path: "/utility/pricing",
    name: "pricing",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/utility/pricing"),
  },
  {
    path: "*",
    name: "error-404",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/utility/404"),
  },
  {
    path: "/utility/500",
    name: "error-500",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/utility/500"),
  },
  {
    path: "/events",
    name: "Events",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/weigh/events"),
  },

  {
    path: "/tags",
    name: "tags",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/weigh/vehicletags"),
  },
  {
    path: "/createtag",
    name: "createtag",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/weigh/createtags"),
  },

  {
    path: "/profile",
    name: "profile",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/account/profile"),
  },
  {
    path: "/update-profile/:email",
    name: "update-profile",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/account/update-profile"),
  },

  {
    path: "/lockscreen",
    name: "lockscreen",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/account/lock-screen"),
  },
  {
    path: "/users-list",
    name: "users-list",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/account/users-list"),
  },

  {
    path: "/shifts",
    name: "shifts",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/shifts/shifts"),
  },
  {
    path: "/camerasettings",
    name: "camerasettings",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/settings/camera-settings"),
  },
  {
    path: "/prosecutionsettings",
    name: "prosecutionsettings",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/settings/prosecution-settings"),
  },
  {
    path: "/permitsettings",
    name: "permitsettings",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/settings/permit-settings"),
  },
  {
    path: "/systemsettings",
    name: "systemsettings",
    meta: {
      authRequired: true,
    },
    component: () => import("../views/pages/settings/systemsettings"),
  },
];
