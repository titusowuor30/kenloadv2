//Process this list here

//add screens to menu items
import store from "@/state/store";
const AccessScreens = store.getters["screens/getUserScreens"]

export const menuItems = AccessScreens;

/*[
  {
    id: 1,
    label: "menuitems.dashboard.text",
    icon: "uil-home-alt",
    link: "/",
  },
  {
    id: 86,
    label: "Weigh",
    icon: "uil-bus-alt",
    link: "/weigh",
    subItems: [
      {
        id: 88,
        label: "Multideck",
        link: "/weigh/multideck",
      },
      {
        id: 88,
        label: "LSWIM",
        link: "/weigh/lswim",
      },
      {
        id: 88,
        label: "Weighbridge Tickets",
        link: "/ticketlist",
      },
      {
        id: 88,
        label: "HSWIM",
        link: "/hswim",
        subItems: [
          {
            id: 89,
            label: "Images",
            link: "/hswimimages",
          },
          {
            id: 90,
            label: "List",
            link: "/hswim",
          },
        ],
      },
      {
        id: 98,
        label: "Audit Events",
        link: "/events",
      },
      {
        id: 98,
        label: "Tags",
        link: "/tags",
      },
      {
        id: 98,
        label: "Yard List",
        link: "/yardlist",
      },
      {
        id: 98,
        label: "Scale Test",
        link: "/scaletest",
      },
      {
        id: 98,
        label: "Manual Weighing",
        link: "/manualweighing",
      },
      {
        id: 98,
        label: "Set Allowable Tolerance",
        link: "/tolerance",
      },
      {
        id: 98,
        label: "ANPR Assist",
        link: "/anprassist",
      },
    ],
  },
  {
    id: 87,
    label: "Security",
    icon: "uil-lock-alt",
    link: "/roles",
    subItems: [
      {
        id: 88,
        label: "Roles",
        link: "/roles",
      },
      {
        id: 91,
        label: "Users",
        // link: "/listusers",
        link: "/users-list",
      },
      {
        id: 94,
        label: "Change Password",
        icon: "uil-bus-alt",
        link: "/changepassword",
      },
      {
        id: 95,
        label: "Password Policy",
        icon: "uil-bus-alt",
        link: "/passwordpolicy",
      },
      {
        id: 96,
        label: "Audit Logs",
        icon: "uil-bus-alt",
        link: "/auditlogs",
      },
      {
        id: 96,
        label: "BackUp Database",
        icon: "uil-bus-alt",
        link: "/backupdb",
      },
    ],
  },
  {
    id: 97,
    label: "Prosecution",
    icon: "uil-list-ui-alt",
    link: "/prosecuted",
  },
  {
    id: 100,
    label: "Census",
    icon: "uil-trees", //uil-label-alt
    link: "/censusclerk",
  },
  {
    id: 101,
    label: "Vehicle Inspection",
    icon: "uil-truck",
    link: "/wideload",
    subItems: [
      {
        id: 102,
        label: "Wide Load Inspection",
        link: "/wideload",
      },
      {
        id: 103,
        label: "Yard Vehicle Inspection",
        link: "/yardinspection",
      },
    ],
  },
  {
    id: 104,
    label: "Special Release",
    icon: "uil-arrow-circle-up",
    link: "/specialrelease",
    subItems: [
      {
        id: 88,
        label: "Yard List",
        link: "/specialrelease",
      },
      {
        id: 88,
        label: "Special Release",
        link: "/specialr",
      },
    ],
  },
  {
    id: 105,
    label: "Data",
    icon: "uil-data-sharing",
    link: "/",
    subItems: [
      {
        id: 106,
        label: "Road Authority",
        link: "/roadauthority",
      },
      {
        id: 107,
        label: "Cluster Weighbridge",
        link: "/clusterweighbridge",
      },
      {
        id: 107,
        label: "Cargo",
        link: "/cargo",
      },
      {
        id: 107,
        label: "Vehicles",
        link: "/vehicles",
      },
      {
        id: 107,
        label: "Transporters",
        link: "/transporters",
      },
      {
        id: 107,
        label: "Drivers",
        link: "/drivers",
      },
      {
        id: 107,
        label: "Origin & Destination",
        link: "/origindestination",
      },
      {
        id: 107,
        label: "Shifts",
        link: "/shifts",
      },
      {
        id: 107,
        label: "Vehicle Make",
        link: "/vehiclemake",
      },
      {
        id: 107,
        label: "Axle Configuration",
        link: "/axleconfig",
      },

      {
        id: 107,
        label: "Routes",
        link: "/routes",
      },
      {
        id: 107,
        label: "Screens",
        link: "/screens",
      },
    ],
  },
  {
    id: 108,
    label: "Reports",
    icon: "uil-paperclip",
    link: "/reporting",
  },
  {
    id: 108,
    label: "Settings",
    icon: "uil-plus-circle",
    link: "/",
    subItems: [
      {
        id: 109,
        label: "Camera Setting",
        link: "/camerasettings",
      },
      {
        id: 109,
        label: "Prosecution Setting",
        link: "/prosecutionsettings",
      },
      {
        id: 109,
        label: "Permit Setting",
        link: "/permitsettings",
      },
      {
        id: 109,
        label: "System Setting",
        link: "/systemsettings",
      },
    ],
  },
  {
    id: 101,
    label: "Technical",
    icon: "uil-technology ",
    link: "/",
    subItems: [
      {
        id: 102,
        label: "Health Check",
        link: "/healthcheck",
      },
      {
        id: 103,
        label: "Calibration Certificate",
        link: "/calibrationcert",
      },
    ],
  },

];*/
