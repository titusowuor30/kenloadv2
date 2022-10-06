<script>
import countTo from "vue-count-to";
/**
 * Stat component
 */
export default {
    components: {
        countTo
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

<div class="row">
<div class="col-xl-2">
        <div class="card">
            <div class="card-body " style="background-color:black;padding:5px; border-radius:5px;" >
               
                <div>
                <p class="mb-0" style="color:yellow;">Platform</p>
                    <p class="actual">
                       
                        <span  class="localstyletitle1" >
                           Actual :
                        </span>
                    </p>
                    <hr style="background-color:#9cf502; color:#9cf502;" />
                    <p class="localstyletitle2">
                       
                        <span >
                            Permissibles:
                        </span>
                    </p>
                    
                </div>
                
            </div>
        </div>
    </div>
    <div class="col-md-6 col-xl-2">
        <div class="card">
            <div class="card-body " style="background-color:black;padding:5px;border-radius:5px;" >
               
                <div>
                <p class="mb-0" style="color:yellow;">Deck 1</p>
                    <p class="actual">
                       
                        <span  class="localstyle">
                            <countTo   :startVal="1000" :endVal="3770" :duration="1200"></countTo>Kg
                        </span>
                    </p>
                    <hr style="background-color:#9cf502; color:#9cf502;" />
                    <p class="localstyle2">
                       
                        <span >
                            <countTo   :startVal="1000" :endVal="4500" :duration="1200"></countTo>Kg
                        </span>
                    </p>
                </div>
                
            </div>
        </div>
    </div>
    <div class="col-md-6 col-xl-2">
        <div class="card">
            <div class="card-body" style="background-color:black;padding:5px;border-radius:5px;">
               
                <div>
                <p class="mb-0" style="color:yellow;">Deck 2</p>
                    <p class="actual">
                       
                        <span  class="localstyle">
                            <countTo   :startVal="1000" :endVal="4655" :duration="1200"></countTo>Kg
                        </span>
                    </p>
                    <hr style="background-color:#9cf502; color:#9cf502;" />
                    <p class="localstyle2">
                       
                        <span >
                            <countTo   :startVal="1000" :endVal="8000" :duration="1200"></countTo>Kg
                        </span>
                    </p>
                </div>
                
            </div>
        </div>
    </div>
    <!-- end col-->

    <div class="col-md-6 col-xl-2">
        <div class="card">
            <div class="card-body" style="background-color:black;padding:5px;border-radius:5px;">
               
                <div>
                <p class="mb-0" style="color:yellow;">Deck 3</p>
                    <p class="actual">
                       
                        <span  class="localstyle">
                            <countTo   :startVal="1000" :endVal="7688" :duration="1200"></countTo>Kg
                        </span>
                    </p>
                    <hr style="background-color:#9cf502; color:#9cf502;" />
                    <p class="localstyle2">
                       
                        <span >
                            <countTo   :startVal="1000" :endVal="12000" :duration="1200"></countTo>Kg
                        </span>
                    </p>
                </div>
               
            </div>
        </div>
    </div>
    <div class="col-md-6 col-xl-2">
        <div class="card">
            <div class="card-body" style="background-color:black;padding:5px;border-radius:5px;">
               
                <div>
                <p class="mb-0" style="color:yellow;">Deck 4</p>
                    <p class="actual">
                       
                        <span  class="localstyle">
                            <countTo   :startVal="1000" :endVal="6788" :duration="1200"></countTo>Kg
                        </span>
                    </p>
                    <hr style="background-color:#9cf502; color:#9cf502;" />
                    <p class="localstyle2">
                       
                        <span >
                            <countTo   :startVal="1000" :endVal="12000" :duration="1200"></countTo>Kg
                        </span>
                    </p>
                </div>
               
            </div>
        </div>
    </div>
    <!-- end col-->
     
    <div class="col-md-6 col-xl-2">
        <div class="card">
            <div class="card-body" style="background-color:black;padding:5px;border-radius:5px;">
               
               <div style="padding:0px;" >
                <p class="mb-0" style="color:yellow;">Total</p>
                    <p class="actual">
                       
                        <span  class="localstyle">
                            <countTo   :startVal="1000" :endVal="46666" :duration="1200"></countTo>Kg
                        </span>
                    </p>
                    <hr style="background-color:#9cf502; color:#9cf502;" />
                    <p class="localstyle2">
                       
                        <span >
                            <countTo   :startVal="1000" :endVal="48000" :duration="1200"></countTo>Kg
                        </span>
                    </p>
                </div>
               
            </div>
        </div>
    </div>
    <!-- end col-->

</div>
<!-- end row-->
</template>
<style scoped>
.localstyle{
font-family: "digital-7";
font-size: 16px;
color:#9cf502;
padding:0px;
}
.localstyle2{
font-family: "digital-7";
font-size: 16px;
color:#fa8900;
padding:0px;
}
.localstyletitle1{

font-size: 16px;
color:#9cf502;
padding:0px;
}
.localstyletitle2{

font-size: 16px;
color:#fa8900;
padding:0px;
}
</style>
