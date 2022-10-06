//const HttpsProxyAgent = require('https-proxy-agent')

//const proxy = new HttpsProxyAgent('http://1.2.3.4:3128')

module.exports = (env) => ({
  devServer: {
    //proxy: "http://192.168.4.60:3031/",
    proxy: /*{
      "/weights": {
        target: "http://192.168.4.111:3031/",
        changeOrigin: true,
        pathRewrite: { "^/weights": "/weights" },
        logLevel: "debug",
        ws: false,
      },*/
      {
      "^/weights": {
        target: "http://192.168.4.111:3031",
        changeOrigin: true,
        pathRewrite: { "^/weights": "/weights" },
        secure: false,
        logLevel:'debug',
      },
      "/ApplicantForms": {
        target: "https://permits.kenha.co.ke:7080/",
        changeOrigin: true,
        pathRewrite: { "^/ApplicantForms": "/ApplicantForms" },
        secure: true,
        ws: false,
      },
      //https://94.237.94.104:5001/api/
      //https://localhost:44365/
      "/api": {
        target: "http://94.237.94.104:",
        changeOrigin: true,
        pathRewrite: { "^/api": "/api" },
        secure: true,
        rejectUnauthorized: false,
        ws: false,
      },

      "/kenload": {
        target: "http://http://192.168.4.111:",
        changeOrigin: true,
        pathRewrite: { "^/kenload": "/kenload" },
        secure: true,
        rejectUnauthorized: false,
        ws: false,
      },
    },
  },
});
