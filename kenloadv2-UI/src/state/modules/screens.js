export const state = {
  userScreens: ["0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0"],
  userToken: "token",
  bidirection: "",
  cameratype: "",
};
export const getters = {
  getUserScreens(state) {
    return state.userScreens;
  },
  getUserToken(state) {
    return state.userToken;
  },
  getBidirection(state) {
    return state.bidirection;
  },
  getcameratype(state) {
    //alert("getcameratype state" + state.cameratype);
    return state.cameratype;
  },
};

export const mutations = {
  SET_BIDIRECTION(state, bidirection) {
    state.bidirection = bidirection;
  },

  SET_CAMERATYPE(state, cameratype) {
    state.cameratype = cameratype;
    //alert("state.cameratype state" + state.cameratype);
  },

  LOAD_USER_SCREENS(state, userScreen) {
    const allscreens = new Set();
    let index = 0;
    const createdMenus = new Set();
    for (let i = 0; i < userScreen.length; i++) allscreens.add(userScreen[i]);

    //parse array
    //store in hashset check if exist in hashset
    //check if array exists then set
    //
    if (allscreens.has("dashboard")) {
      let newObjectScreen = {
        id: 1,
        label: "menuitems.dashboard.text",
        icon: "uil-home-alt",
        link: "/",
        subItems: [
          {
            id: 2,
            color: "white",
            icon: "fa fa-home",
            label: "Dashboard",
            link: "/",
          },
          {
            id: 3,
            color: "white",
            icon: "fa fa-question",
            label: "Need Help?",
            link: "\\/kenloadv2helpdocs.masterspace.co.ke/",
            target: "_blank",
          },
          {
            id: 4,
            color: "white",
            icon: "fa fa-list",
            label: "Police Reports",
            link: "/PoliceReport",
          },
        ],
      };

      state.userScreens[0] = newObjectScreen;
    }

    //////////////////////////////////////////////////////////PROSECUTION////////////////////////////////////////////////////////////////////////////

    //////////////////////////////////////////////////////////PROSECUTION////////////////////////////////////////////////////////////////////////////
    /*if(userScreen=="dashboard")
            { 
               let newObjectScreen =  {
            id: 1,
            label: "menuitems.dashboard.text",
            icon: "uil-home-alt",
            link: "/",
             };
            //add sub item 
            state.userScreens.push(newObjectScreen);
             
            }*/

    if (allscreens.has("weighMultideck")) {
      if (createdMenus.has("Weigh")) {
        let newObjectScreen = {
          id: 80,
          label: "Multideck",
          link: "/weigh/multideck",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Weigh");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 86,
          label: "Weigh",
          icon: "uil-bus-alt",
          link: "/weigh",
          subItems: [
            {
              id: 80,
              label: "Multideck",
              link: "/weigh/multideck",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Weigh");
      }
    }
    if (allscreens.has("weighLswim")) {
      if (state.userScreens[1]) {
        if (state.userScreens[1].id == 86) {
          let newObjectScreen = {
            id: 88,
            label: "LSWIM",
            link: "/weigh/lswim",
          };
          ///add to subitem array
          index = state.userScreens
            .map(function(e) {
              return e.label;
            })
            .indexOf("Weigh");
          state.userScreens[index].subItems.push(newObjectScreen);
        }
      } else {
        let newObjectScreen = {
          id: 86,
          label: "Weigh",
          icon: "uil-bus-alt",
          link: "/weigh",
          subItems: [
            {
              id: 88,
              label: "LSWIM",
              link: "/weigh/lswim",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Weigh");
      }
    }

    if (allscreens.has("weighbridgeTickets")) {
      if (createdMenus.has("Weigh")) {
        let newObjectScreen = {
          id: 89,
          label: "Weighbridge Tickets",
          link: "/ticketlist",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Weigh");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 86,
          label: "Weigh",
          icon: "uil-bus-alt",
          link: "/weigh",
          subItems: [
            {
              id: 89,
              label: "Weighbridge Tickets",
              link: "/ticketlist",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Weigh");
      }
    }
    if (allscreens.has("hswim")) {
      if (createdMenus.has("Weigh")) {
        let newObjectScreen = {
          id: 90,
          label: "HSWIM",
          link: "/hswim",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Weigh");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 86,
          label: "Weigh",
          icon: "uil-bus-alt",
          link: "/weigh",
          subItems: [
            {
              id: 90,
              label: "HSWIM",
              link: "/hswim",
              subItems: [
                {
                  id: 91,
                  label: "Images",
                  link: "/hswimimages",
                },
                {
                  id: 92,
                  label: "List",
                  link: "/hswim",
                },
              ],
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Weigh");
      }
    }
    if (allscreens.has("auditEvents")) {
      if (createdMenus.has("Weigh")) {
        let newObjectScreen = {
          id: 93,
          label: "Audit Events",
          link: "/events",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Weigh");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 86,
          label: "Weigh",
          icon: "uil-bus-alt",
          link: "/weigh",
          subItems: [
            {
              id: 93,
              label: "Audit Events",
              link: "/events",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Weigh");
      }
    }
    if (allscreens.has("tags")) {
      if (createdMenus.has("Weigh")) {
        let newObjectScreen = {
          id: 94,
          label: "Tags",
          link: "/tags",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Weigh");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 86,
          label: "Weigh",
          icon: "uil-bus-alt",
          link: "/weigh",
          subItems: [
            {
              id: 94,
              label: "Tags",
              link: "/tags",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Weigh");
      }
    }
    if (allscreens.has("yardList")) {
      if (createdMenus.has("Weigh")) {
        let newObjectScreen = {
          id: 95,
          label: "Yard List",
          link: "/yardlist",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Weigh");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 86,
          label: "Weigh",
          icon: "uil-bus-alt",
          link: "/weigh",
          subItems: [
            {
              id: 95,
              label: "Yard List",
              link: "/yardlist",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Weigh");
      }
    }
    if (allscreens.has("scaleTest")) {
      if (createdMenus.has("Weigh")) {
        let newObjectScreen = {
          id: 96,
          label: "Scale Test",
          link: "/scaletest",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Weigh");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 86,
          label: "Weigh",
          icon: "uil-bus-alt",
          link: "/weigh",
          subItems: [
            {
              id: 96,
              label: "Scale Test",
              link: "/scaletest",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Weigh");
      }
    }
    if (allscreens.has("manualWeighing")) {
      if (createdMenus.has("Weigh")) {
        let newObjectScreen = {
          id: 97,
          label: "Manual Weighing",
          link: "/manualweighing",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Weigh");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 86,
          label: "Weigh",
          icon: "uil-bus-alt",
          link: "/weigh",
          subItems: [
            {
              id: 97,
              label: "Manual Weighing",
              link: "/manualweighing",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("weigh");
      }
    }
    if (allscreens.has("autoweigh")) {
      if (createdMenus.has("Weigh")) {
        let newObjectScreen = {
          id: 139,
          label: "Auto Weigh",
          link: "/autoweigh",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Weigh");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 138,
          label: "Weigh",
          icon: "uil-airplay-alt",
          link: "/autoweigh",
          subItems: [
            {
              id: 139,
              label: "Auto Weigh",
              link: "/autoweigh",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("weigh");
      }
    }
    if (allscreens.has("setAllowableTolerance")) {
      if (createdMenus.has("Weigh")) {
        let newObjectScreen = {
          id: 98,
          label: "Set Allowable Tolerance",
          link: "/tolerance",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Weigh");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 86,
          label: "Weigh",
          icon: "uil-bus-alt",
          link: "/weigh",
          subItems: [
            {
              id: 98,
              label: "Set Allowable Tolerance",
              link: "/tolerance",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Weigh");
      }
    }

    if (allscreens.has("anprAssist")) {
      if (createdMenus.has("Weigh")) {
        let newObjectScreen = {
          id: 100,
          label: "ANPR Assist",
          link: "/anprassist",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Weigh");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 86,
          label: "Weigh",
          icon: "uil-bus-alt",
          link: "/weigh",
          subItems: [
            {
              id: 100,
              label: "ANPR Assist",
              link: "/anprassist",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Weigh");
      }
    }

    ////////////////////////////////////////////////SECURITY///////////////////////////////////////////////////////////////////

    if (allscreens.has("SetScreenPassword")) {
      //console.log(allscreens);
      if (createdMenus.has("Security")) {
        let newObjectScreen = {
          id: 102,
          label: "Set Screen Password",
          link: "/setscreenpassword",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Security");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 101,
          label: "Security",
          icon: "uil-lock-alt",
          link: "/setscreenpassword",
          subItems: [
            {
              id: 102,
              label: "Set Screen Password",
              link: "/setscreenpassword",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Security");
        console.log(state.userScreens);
      }
    }

    if (allscreens.has("roles")) {
      //console.log(allscreens);
      if (createdMenus.has("Security")) {
        let newObjectScreen = {
          id: 102,
          label: "Roles",
          link: "/roles",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Security");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 101,
          label: "Security",
          icon: "uil-lock-alt",
          link: "/roles",
          subItems: [
            {
              id: 102,
              label: "Roles",
              link: "/roles",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Security");
        console.log(state.userScreens);
      }
    }
    if (allscreens.has("passwordPolicy")) {
      //console.log(allscreens);
      if (createdMenus.has("Security")) {
        let newObjectScreen = {
          id: 105,
          label: "Password Policy",
          icon: "uil-bus-alt",
          link: "/passwordpolicy",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Security");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 101,
          label: "Security",
          icon: "uil-lock-alt",
          link: "/roles",
          subItems: [
            {
              id: 105,
              label: "Password Policy",
              icon: "uil-bus-alt",
              link: "/passwordpolicy",
            },
          ],
        };
        //add sub item c
        createdMenus.add("Security");
        state.userScreens.push(newObjectScreen);
      }
    }
    if (allscreens.has("usersList")) {
      if (createdMenus.has("Security")) {
        let newObjectScreen = {
          id: 103,
          label: "Users",
          // link: "/listusers",
          link: "/users-list",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Security");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 101,
          label: "Security",
          icon: "uil-lock-alt",
          link: "/roles",
          subItems: [
            {
              id: 103,
              label: "Users",
              // link: "/listusers",
              link: "/users-list",
            },
          ],
        };
        //add sub item
        createdMenus.add("Security");
        state.userScreens.push(newObjectScreen);
      }
    }
    if (allscreens.has("changePassword")) {
      if (createdMenus.has("Security")) {
        let newObjectScreen = {
          id: 104,
          label: "Change Password",
          icon: "uil-bus-alt",
          link: "/update-profile/" + JSON.parse(localStorage.user).email,
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Security");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 101,
          label: "Security",
          icon: "uil-lock-alt",
          link: "/roles",
          subItems: [
            {
              id: 104,
              label: "Change Password",
              icon: "uil-bus-alt",
              link: "/update-profile/" + JSON.parse(localStorage.user).email,
            },
          ],
        };
        //add sub item
        createdMenus.add("Security");
        state.userScreens.push(newObjectScreen);
      }
    }

    if (allscreens.has("auditLogs")) {
      if (createdMenus.has("Security")) {
        let newObjectScreen = {
          id: 106,
          label: "Audit Logs",
          icon: "uil-bus-alt",
          link: "/auditlogs",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Security");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 101,
          label: "Security",
          icon: "uil-lock-alt",
          link: "/roles",
          subItems: [
            {
              id: 106,
              label: "Audit Logs",
              icon: "uil-bus-alt",
              link: "/auditlogs",
            },
          ],
        };
        //add sub item
        createdMenus.add("Security");
        state.userScreens.push(newObjectScreen);
      }
    }

    if (allscreens.has("backupDatabase")) {
      if (createdMenus.has("Security")) {
        let newObjectScreen = {
          id: 107,
          label: "BackUp Database",
          icon: "uil-bus-alt",
          link: "/backupdb",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Security");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 101,
          label: "Security",
          icon: "uil-lock-alt",
          link: "/roles",
          subItems: [
            {
              id: 107,
              label: "BackUp Database",
              icon: "uil-bus-alt",
              link: "/backupdb",
            },
          ],
        };
        //add sub item
        createdMenus.add("Security");
        state.userScreens.push(newObjectScreen);
      }
    }
    //////////////////////////////////////////////////////////PROSECUTION////////////////////////////////////////////////////////////////////////////
    if (allscreens.has("prosecution")) {
      let newObjectScreen = {
        id: 108,
        label: "Prosecution",
        icon: "uil-list-ui-alt",
        link: "/prosecuted",
      };
      //add sub item
      state.userScreens.push(newObjectScreen);
      createdMenus.add("prosecution");
    }

    //////////////////////////////////////////////////////////CENSUN////////////////////////////////////////////////////////////////////////////
    if (allscreens.has("census")) {
      let newObjectScreen = {
        id: 109,
        label: "Census",
        icon: "uil-trees", //uil-label-alt
        link: "/censusclerk",
      };
      //add sub item
      state.userScreens.push(newObjectScreen);
      createdMenus.add("census");
    }

    ///////////////////////////////////////////////////////////////////VEHICLE INSPECTION///////////////////////////////////////////////////////////////////
    if (allscreens.has("wideLoadInspection")) {
      if (createdMenus.has("Vehicle Inspection")) {
        let newObjectScreen = {
          id: 111,
          label: "Wide Load Inspection",
          link: "/wideload",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Vehicle Inspection");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 110,
          label: "Vehicle Inspection",
          icon: "uil-truck",
          link: "/wideload",
          subItems: [
            {
              id: 111,
              label: "Wide Load Inspection",
              link: "/wideload",
            },
          ],
        };
        //add sub item
        createdMenus.add("Vehicle Inspection");
        state.userScreens.push(newObjectScreen);
      }
    }
    if (allscreens.has("yardVehicleInspection")) {
      if (createdMenus.has("Vehicle Inspection")) {
        let newObjectScreen = {
          id: 112,
          label: "Heavy Vehicles Inspection",
          link: "/yardinspection",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Vehicle Inspection");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 110,
          label: "Vehicle Inspection",
          icon: "uil-truck",
          link: "/wideload",
          subItems: [
            {
              id: 112,
              label: "Heavy Vehicles Inspection",
              link: "/yardinspection",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Vehicle Inspection");
      }
    }

    ////////////////////////////////////////////////SPECIAL RELEASE ////////////////////////////////////////////////////////////////

    if (allscreens.has("specialReleaseYardList")) {
      if (createdMenus.has("Special Release")) {
        let newObjectScreen = {
          id: 114,
          label: "Yard List",
          link: "/specialrelease",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Special Release");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 113,
          label: "Special Release",
          icon: "uil-arrow-circle-up",
          link: "/specialrelease",
          subItems: [
            {
              id: 114,
              label: "Yard List",
              link: "/specialrelease",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Special Release");
      }
    }

    if (allscreens.has("specialRelease")) {
      if (createdMenus.has("Special Release")) {
        let newObjectScreen = {
          id: 115,
          label: "Special Release",
          link: "/specialr",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Special Release");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 113,
          label: "Special Release",
          icon: "uil-arrow-circle-up",
          link: "/specialrelease",
          subItems: [
            {
              id: 115,
              label: "Special Release",
              link: "/specialr",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Special Release");
      }
    }
    ////////////////////////////////////////////////DATA////////////////////////////////////////////////////////////////

    if (allscreens.has("roadAuthority")) {
      if (createdMenus.has("Setup")) {
        let newObjectScreen = {
          id: 117,
          label: "Organization",
          link: "/organization",
        };
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Setup");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 116,
          label: "Setup",
          icon: "uil-data-sharing",
          link: "/",
          subItems: [
            {
              id: 117,
              label: "Organization",
              link: "/organization",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Setup");
      }
    }
    if (allscreens.has("organization")) {
      if (createdMenus.has("Setup")) {
        let newObjectScreen = {
          id: 117,
          label: "Road Authority",
          link: "/roadauthority",
        };
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Setup");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 116,
          label: "Setup",
          icon: "uil-data-sharing",
          link: "/",
          subItems: [
            {
              id: 117,
              label: "Road Authority",
              link: "/roadauthority",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Setup");
      }
    }

    if (allscreens.has("clusterWeighbridge")) {
      if (createdMenus.has("Setup")) {
        let newObjectScreen = {
          id: 118,
          label: "Cluster Weighbridge",
          link: "/clusterweighbridge",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Setup");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 116,
          label: "Setup",
          icon: "uil-data-sharing",
          link: "/",
          subItems: [
            {
              id: 118,
              label: "Cluster Weighbridge",
              link: "/clusterweighbridge",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Setup");
      }
    }

    if (allscreens.has("cargo")) {
      if (createdMenus.has("Setup")) {
        let newObjectScreen = {
          id: 119,
          label: "Cargo",
          link: "/cargo",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Setup");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 116,
          label: "Setup",
          icon: "uil-data-sharing",
          link: "/",
          subItems: [
            {
              id: 119,
              label: "Cargo",
              link: "/cargo",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Setup");
      }
    }
    if (allscreens.has("vehicles")) {
      if (createdMenus.has("Setup")) {
        let newObjectScreen = {
          id: 120,
          label: "Vehicles",
          link: "/vehicles",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Setup");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 116,
          label: "Setup",
          icon: "uil-data-sharing",
          link: "/",
          subItems: [
            {
              id: 120,
              label: "Vehicles",
              link: "/vehicles",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Setup");
      }
    }

    if (allscreens.has("transporters")) {
      if (createdMenus.has("Setup")) {
        let newObjectScreen = {
          id: 121,
          label: "Transporters",
          link: "/transporters",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Setup");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 116,
          label: "Setup",
          icon: "uil-data-sharing",
          link: "/",
          subItems: [
            {
              id: 121,
              label: "Transporters",
              link: "/transporters",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Setup");
      }
    }
    if (allscreens.has("drivers")) {
      if (createdMenus.has("Setup")) {
        let newObjectScreen = {
          id: 122,
          label: "Drivers",
          link: "/drivers",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Setup");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 116,
          label: "Setup",
          icon: "uil-data-sharing",
          link: "/",
          subItems: [
            {
              id: 122,
              label: "Drivers",
              link: "/drivers",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Setup");
      }
    }
    if (allscreens.has("originDestination")) {
      if (createdMenus.has("Setup")) {
        let newObjectScreen = {
          id: 123,
          label: "Origin & Destination",
          link: "/origindestination",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Setup");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 116,
          label: "Setup",
          icon: "uil-data-sharing",
          link: "/",
          subItems: [
            {
              id: 123,
              label: "Origin & Destination",
              link: "/origindestination",
            },
          ],
        };
        //add sub item
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Setup");
        state.userScreens[index].subItems.push(newObjectScreen);
      }
    }
    if (allscreens.has("shifts")) {
      if (createdMenus.has("Setup")) {
        let newObjectScreen = {
          id: 124,
          label: "Shifts",
          link: "/shifts",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Setup");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 116,
          label: "Setup",
          icon: "uil-data-sharing",
          link: "/",
          subItems: [
            {
              id: 124,
              label: "Shifts",
              link: "/shifts",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Setup");
      }
    }

    if (allscreens.has("vehicleMake")) {
      if (createdMenus.has("Setup")) {
        let newObjectScreen = {
          id: 125,
          label: "Vehicle Make",
          link: "/vehiclemake",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Setup");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 116,
          label: "Setup",
          icon: "uil-data-sharing",
          link: "/",
          subItems: [
            {
              id: 125,
              label: "Vehicle Make",
              link: "/vehiclemake",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Setup");
      }
    }

    if (allscreens.has("axleConfiguration")) {
      if (createdMenus.add("Setup")) {
        let newObjectScreen = {
          id: 126,
          label: "Axle Configuration",
          link: "/axleconfig",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Setup");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 116,
          label: "Setup",
          icon: "uil-data-sharing",
          link: "/",
          subItems: [
            {
              id: 126,
              label: "Axle Configuration",
              link: "/axleconfig",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Setup");
      }
    }

    if (allscreens.has("routes")) {
      if (createdMenus.has("Setup")) {
        let newObjectScreen = {
          id: 127,
          label: "Routes",
          link: "/routes",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Setup");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 116,
          label: "Setup",
          icon: "uil-data-sharing",
          link: "/",
          subItems: [
            {
              id: 127,
              label: "Routes",
              link: "/routes",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Setup");
      }
    }

    if (allscreens.has("screens")) {
      if (createdMenus.has("Setup")) {
        let newObjectScreen = {
          id: 128,
          label: "Screens",
          link: "/screens",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Setup");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 116,
          label: "Setup",
          icon: "uil-data-sharing",
          link: "/",
          subItems: [
            {
              id: 128,
              label: "Screens",
              link: "/screens",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Setup");
      }
    }
    if (allscreens.has("dollarrate")) {
      if (createdMenus.has("Setup")) {
        let newObjectScreen = {
          id: 129,
          label: "Dollar Rate",
          link: "/dollarrate",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Setup");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 116,
          label: "Setup",
          icon: "uil-data-sharing",
          link: "/",
          subItems: [
            {
              id: 129,
              label: "Dollar Rate",
              link: "/dollarrate",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Setup");
      }
    }

    if (allscreens.has("reports")) {
      let newObjectScreen = {
        id: 129,
        label: "Reports",
        icon: "uil-paperclip",
        link: "/reporting",
      };
      ///add to subitem array
      state.userScreens.push(newObjectScreen);
      createdMenus.add("reports");
    }

    if (allscreens.has("prosecutionSettings")) {
      if (createdMenus.has("Settings")) {
        let newObjectScreen = {
          id: 132,
          label: "Prosecution Setting",
          link: "/prosecutionsettings",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Settings");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 130,
          label: "Settings",
          icon: "uil-plus-circle",
          link: "/",
          subItems: [
            {
              id: 132,
              label: "Prosecution Setting",
              link: "/prosecutionsettings",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Settings");
      }
    }
    if (allscreens.has("permitSettings")) {
      if (createdMenus.has("Settings")) {
        let newObjectScreen = {
          id: 133,
          label: "Permit Setting",
          link: "/permitsettings",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Settings");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 130,
          label: "Settings",
          icon: "uil-plus-circle",
          link: "/",
          subItems: [
            {
              id: 133,
              label: "Permit Setting",
              link: "/permitsettings",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Settings");
      }
    }
    if (allscreens.has("systemSettings")) {
      if (createdMenus.has("Settings")) {
        let newObjectScreen = {
          id: 134,
          label: "System Setting",
          link: "/systemsettings",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Settings");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 130,
          label: "Settings",
          icon: "uil-plus-circle",
          link: "/",
          subItems: [
            {
              id: 134,
              label: "System Setting",
              link: "/systemsettings",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Settings");
      }
    }

    if (allscreens.has("cameraSettings")) {
      if (createdMenus.has("Settings")) {
        let newObjectScreen = {
          id: 131,
          label: "Camera Setting",
          link: "/camerasettings",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Settings");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 130,
          label: "Settings",
          icon: "uil-plus-circle",
          link: "/",
          subItems: [
            {
              id: 131,
              label: "Camera Setting",
              link: "/camerasettings",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Settings");
      }
    }

    if (allscreens.has("healthCheck")) {
      if (createdMenus.has("Technical")) {
        let newObjectScreen = {
          id: 136,
          label: "Health Check",
          link: "/healthcheck",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Technical");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 135,
          label: "Technical",
          icon: "uil-technology ",
          link: "/",
          subItems: [
            {
              id: 136,
              label: "Health Check",
              link: "/healthcheck",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Technical");
      }
    }

    if (allscreens.has("calibrationCertificate")) {
      if (createdMenus.has("Technical")) {
        let newObjectScreen = {
          id: 137,
          label: "Calibration Certificate",
          link: "/calibrationcert",
        };
        ///add to subitem array
        index = state.userScreens
          .map(function(e) {
            return e.label;
          })
          .indexOf("Technical");
        state.userScreens[index].subItems.push(newObjectScreen);
      } else {
        let newObjectScreen = {
          id: 135,
          label: "Technical",
          icon: "uil-technology ",
          link: "/",
          subItems: [
            {
              id: 137,
              label: "Calibration Certificate",
              link: "/calibrationcert",
            },
          ],
        };
        //add sub item
        state.userScreens.push(newObjectScreen);
        createdMenus.add("Technical");
      }
    }
    state.userScreens = state.userScreens.sort((a, b) =>
      a.id > b.id ? 1 : b.id > a.id ? -1 : 0,
    );
  },
  SET_TOKEN_VALUE(state, userToken) {
    state.userToken = userToken;
  },
  REMOVE_SCREENS(state) {
    state.userScreens.length = 0;
  },
};
export const actions = {
  loadUserScreens({ commit }, { userScreen }) {
    commit("LOAD_USER_SCREENS", userScreen);
  },
  loadUserToken({ commit }, { userToken }) {
    commit("SET_TOKEN_VALUE", userToken);
  },
  clearScreens({ commit }) {
    commit("REMOVE_SCREENS");
  },
  setBiredirection({ commit }, bidirection) {
    commit("SET_BIDIRECTION", bidirection);
  },
  setCameratype({ commit }, cameratype) {
    commit("SET_CAMERATYPE", cameratype);
    //alert("cameratype" + cameratype);
  },
};
