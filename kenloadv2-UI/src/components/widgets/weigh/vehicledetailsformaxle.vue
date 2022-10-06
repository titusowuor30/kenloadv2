<script>

/**
 * Stat component
 */
export default {
    components: {
        
    },
    methods:{
        goToVehicleDir(){
   this.$router.push('/weigh-vehicle-direction'); 
      }

    },
    data() {
        return {
            series: [{
                data: [25, 66, 41, 89, 63, 25, 44, 20, 36, 40, 54]
            }],
            chartOptions: {
                fill: {
                    colors: ["#5b73e8"]
                },
                chart: {
                    type: "bar",
                    sparkline: {
                        enabled: true
                    }
                },
                plotOptions: {
                    bar: {
                        columnWidth: "50%"
                    }
                },
                labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11],
                xaxis: {
                    crosshairs: {
                        width: 1
                    }
                },
                tooltip: {
                    fixed: {
                        enabled: false
                    },
                    x: {
                        show: false
                    },
                    y: {
                        title: {
                            formatter: function () {
                                return "";
                            }
                        }
                    },
                    marker: {
                        show: false
                    }
                }
            },
            growthChartOptions: {
                fill: {
                    colors: ["#f1b44c"]
                },
                chart: {
                    type: "bar",
                    sparkline: {
                        enabled: true
                    }
                },
                plotOptions: {
                    bar: {
                        columnWidth: "50%"
                    }
                },
                labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11],
                xaxis: {
                    crosshairs: {
                        width: 1
                    }
                },
                tooltip: {
                    fixed: {
                        enabled: false
                    },
                    x: {
                        show: false
                    },
                    y: {
                        title: {
                            formatter: function () {
                                return "";
                            }
                        }
                    },
                    marker: {
                        show: false
                    }
                }
            },
            orderseries: [70],
            orderRadial: {
                fill: {
                    colors: ["#34c38f"]
                },
                chart: {
                    sparkline: {
                        enabled: true
                    }
                },
                dataLabels: {
                    enabled: false
                },
                plotOptions: {
                    radialBar: {
                        hollow: {
                            margin: 0,
                            size: "60%"
                        },
                        track: {
                            margin: 0
                        },
                        dataLabels: {
                            show: false
                        }
                    }
                }
            },
            customerseries: [55],
            customerRadial: {
                fill: {
                    colors: ["#5b73e8"]
                },
                chart: {
                    sparkline: {
                        enabled: true
                    }
                },
                dataLabels: {
                    enabled: false
                },
                plotOptions: {
                    radialBar: {
                        hollow: {
                            margin: 0,
                            size: "60%"
                        },
                        track: {
                            margin: 0
                        },
                        dataLabels: {
                            show: false
                        }
                    }
                }
            }
        };
    }
};
</script>

<template>
            <div class="row" >
             
                    <div class="col-md-6 col-xl-8">
                   
                   <h5 class="font-size-14 mb-4">
                    <i class="mdi mdi-arrow-right text-primary me-1"></i>
                    Ticket Details
                  </h5>

        <div class="card" style="font-size:10px;">
          <div class="card-body align-items-center" >
           <h5 class="font-size-14 mb-4" style="text-align:center;">
                  
                   Axle Grouping(2A)
                  </h5>
           
            <div class="table-responsive" >
              <table class="styled-table">
                <thead>
                  <tr>
                    <th>Axle</th>
                    <th>Permmissible (KG)</th>
                    <th>Permissible Tolerances (5%)(KG)</th>
                    <th>Actual(KG)</th>
                    <th>Overload(KG)</th>
                    <th>Pavement Damage Factor(PDF)</th>
                    <th>Results</th>
                  </tr>
                </thead>
                <tbody>
                  <tr>
                    <th scope="row">1</th>
                    <td>8000</td>
                    <td>8400</td>
                    <td>6800</td>
                    <td>0</td>
                    <td>0.26</td>
                    <td>Legal</td>
                  </tr>
                  <tr>
                    <th scope="row">2</th>
                    <td>10000</td>
                    <td>10500</td>
                    <td>6800</td>
                    <td>500</td>
                    <td>1.14</td>
                    <td style="background-color:red; color:yellow;">Overload</td>
                  </tr>
                 <tr>
                    <th scope="row">3</th>
                    <td>-</td>
                    <td>-</td>
                    <td>-</td>
                    <td>-</td>
                    <td>-</td>
                    <td>-</td>
                  </tr>
                   <tr>
                    <th scope="row">4</th>
                    <td>-</td>
                    <td>-</td>
                    <td>-</td>
                    <td>-</td>
                    <td>-</td>
                    <td>-</td>
                  </tr>
                   <tr>
                    <th scope="row" colspan="2">GVW(KG)  17800</th>
                    <td colspan="2">Permissible(KG)    18000</td>    
                    <td colspan="2">Result</td>
                     <td style="background-color:green; color:yellow;">LEGAL</td>
                    
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
     
      </div>
   <div class="col-md-6 col-xl-4" >
   <div class="col-md-12" >
    <h5 class="font-size-14 mb-4">
                    <i class="mdi mdi-arrow-right text-primary me-1"></i>
                   Status
                  </h5>
        <b-card style="background-color:green;color:yellow;">
         
          <p class="card-text" style="text-align:center;" >
            <i class="mdi mdi-check-all me-3"></i> LEGAL
          </p>
        </b-card>
      </div>
              <h5 class="font-size-14 mb-4">
                    <i class="mdi mdi-arrow-right text-primary me-1"></i>
                    Vehicle Details
                  </h5>
                <form class="form-horizontal" role="form" style="font-size:12px;">
                  <div class="row" id="formrow" >
                    <label for="exampleDataList" class="col-md-4 col-form-label"
                      >Registration No:</label>
                    <div class="col-md-8">
                      <input style="font-size:12px;"
                        class="form-control"
                        list="datalistOptions"
                        id="exampleDataList"
                        placeholder="Type to search..."
                      />
                      <datalist id="datalistOptions" style="font-size:12px;"    >
                        <option value="KBZ466G"></option>
                        <option value="KCR348D"></option>
                        <option value="KDA233S"></option>
                      </datalist>
                    </div>
                  </div>
                  <div class="row" id="formrow">
                    <label for="exampleDataList" class="col-md-4 col-form-label"
                      >Permit No:</label>
                    <div class="col-md-8">
                      <input style="font-size:12px;"
                        class="form-control"
                        list="datalistOptions"
                        id="exampleDataList"
                        placeholder="Type to search..."
                      />
                      <datalist id="datalistOptions">
                        <option value="7621028"></option>
                        <option value="8721872"></option>
                        <option value="861862"></option>
                        
                      </datalist>
                    </div>
                  </div>
                  <div class="row" id="formrow">
                    <label for="exampleDataList" class="col-md-4 col-form-label"
                      >Trailer No:</label>
                    <div class="col-md-8">
                      <input style="font-size:12px;"
                        class="form-control"
                        list="datalistOptions"
                        id="exampleDataList"
                        placeholder="Type to search..."
                      />
                      <datalist id="datalistOptions">
                        <option value="7621028"></option>
                        <option value="8721872"></option>
                        <option value="861862"></option>
                        
                      </datalist>
                    </div>
                  </div>
                  <div class="row" id="formrow">
                    <label for="exampleDataList" class="col-md-4 col-form-label"
                      >Driver Name:</label>
                    <div class="col-md-8">
                      <input style="font-size:12px;"
                        class="form-control"
                        list="datalistOptions"
                        id="exampleDataList"
                        placeholder="Type to search..."
                      />
                      <datalist id="datalistOptions">
                        <option value="7621028"></option>
                        <option value="8721872"></option>
                        <option value="861862"></option>
                        
                      </datalist>
                    </div>
                  </div>
                  <div class="row" id="formrow">
                    <label for="exampleDataList" class="col-md-4 col-form-label"
                      >Vehicle Make:</label>
                    <div class="col-md-8">
                      <input style="font-size:12px;"
                        class="form-control"
                        list="datalistOptions"
                        id="exampleDataList"
                        placeholder="Type to search..."
                      />
                      <datalist id="datalistOptions">
                        <option value="7621028"></option>
                        <option value="8721872"></option>
                        <option value="861862"></option>
                        
                      </datalist>
                    </div>
                  </div>
                  <div class="row" id="formrow">
                    <label for="exampleDataList" class="col-md-4 col-form-label"
                      >Cargo:</label>
                    <div class="col-md-8">
                      <input style="font-size:12px;"
                        class="form-control"
                        list="datalistOptions"
                        id="exampleDataList"
                        placeholder="Type to search..."
                      />
                      <datalist id="datalistOptions">
                        <option value="7621028"></option>
                        <option value="8721872"></option>
                        <option value="861862"></option>
                        
                      </datalist>
                    </div>
                  </div>
                  <div class="row" id="formrow">
                    <label for="exampleDataList" class="col-md-4 col-form-label"
                      >Axle Type:</label>
                    <div class="col-md-8">
                      <input style="font-size:12px;"
                        class="form-control"
                        list="datalistOptions"
                        id="exampleDataList"
                        placeholder="Type to search..."
                      />
                      <datalist id="datalistOptions">
                        <option value="7621028"></option>
                        <option value="8721872"></option>
                        <option value="861862"></option>
                        
                      </datalist>
                    </div>
                  </div>
                  <div class="row" id="formrow">
                    <label for="exampleDataList" class="col-md-4 col-form-label"
                      >Transporter:</label>
                    <div class="col-md-8">
                      <input style="font-size:12px;"
                        class="form-control"
                        list="datalistOptions"
                        id="exampleDataList"
                        placeholder="Type to search..."
                      />
                      <datalist id="datalistOptions" style="font-size:12px;"    >
                        <option value="KBZ466G"></option>
                        <option value="KCR348D"></option>
                        <option value="KDA233S"></option>
                      </datalist>
                    </div>
                  </div>
                  <div class="row" id="formrow" >
                    <label for="exampleDataList" class="col-md-4 col-form-label"
                      >Origin:</label>
                    <div class="col-md-8">
                      <input style="font-size:12px;"
                        class="form-control"
                        list="datalistOptions"
                        id="exampleDataList"
                        placeholder="Type to search..."
                      />
                      <datalist id="datalistOptions" style="font-size:12px;"    >
                        <option value="KBZ466G"></option>
                        <option value="KCR348D"></option>
                        <option value="KDA233S"></option>
                      </datalist>
                    </div>
                  </div>
                  <div class="row" id="formrow" >
                    <label for="exampleDataList" class="col-md-4 col-form-label"
                      >Destination:</label>
                    <div class="col-md-8">
                      <input style="font-size:12px;"
                        class="form-control"
                        list="datalistOptions"
                        id="exampleDataList"
                        placeholder="Type to search..."
                      />
                      <datalist id="datalistOptions" style="font-size:12px;"    >
                        <option value="KBZ466G"></option>
                        <option value="KCR348D"></option>
                        <option value="KDA233S"></option>
                      </datalist>
                    </div>
                  </div>
                  <div class="col-12 align-items-center" style="text-align:center; margin:10px;">
               <b-button @click="goToVehicleDir()" variant="primary" style="color:yellow;">
                Take Weight 
                <i class="uil uil-angle-double-right ms-2"></i>
              </b-button>
              </div>
                  </form>
                  </div>  
            </div>


</template>
<style scoped>
.localstyle{
font-family: "digital-7";
font-size: 18px;
color:#9cf502;
}
.localstyletitle{
font-size: 18px;
color:#9cf502;
}

/* table {
  font-family: arial, sans-serif;
  border-collapse: collapse;
  width: 100%;
}

td, th {
  border: 1px solid #dddddd;
  text-align: left;
  padding: 8px;
}

tr:nth-child(even) {
  background-color: #dddddd;
}*/
.styled-table {
    border-collapse: collapse;
    margin: 25px 0;
    font-size: 11px;
    font-family: sans-serif;
    font-weight: bold;
    min-width: 400px;
    box-shadow: 0 0 20px rgba(0, 0, 0, 0.15);
}
.styled-table thead tr {
    background-color: lightgray;
    color: black;
    text-align: left;
}
.styled-table th,
.styled-table td {
    padding: 12px 15px;
    border: 1px solid gray;
}
.styled-table tbody tr {
    border-bottom: 1px solid #dddddd;
}

.styled-table tbody tr:nth-of-type(even) {
    background-color: #f3f3f3;
}

.styled-table tbody tr:last-of-type {
    border-bottom: 4px solid yellow;
}

.styled-table tbody tr.active-row {
    font-weight: bold;
    color: yellow;
}
#formrow{
font-size:12px; 
padding:8px 0px 8px 0px;
}


</style>
