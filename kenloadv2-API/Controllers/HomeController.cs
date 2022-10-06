using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mpesa.SDK.AspNetCore;

namespace KenloadV2API.Controllers{
    public class HomeController : ControllerBase
    {
        private readonly ILipaNaMpesa lnm;
        private readonly IC2B c2b;
        private readonly IB2C b2c;
        private readonly ILogger _logger;

        public HomeController(ILipaNaMpesa lnm, IC2B c2b, IB2C b2c, ILogger<HomeController> logger)
        {
            this.lnm = lnm;
            this.c2b = c2b;
            this.b2c = b2c;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // STK Push
            var stk = await lnm.PushStk("254743793901", "1", "My Account");

            // STK Query
            var query = await lnm.QueryStatus("ws_CO_031120201658538238");

            // Check C2B Account Balance. Can also do forn B2C.
            var c2bBalance = await c2b.QueryBalance();

            // Check C2B transaction status. Can also do forn B2C
            var c2bStatus = await c2b.QueryTransactionStatus("OJQERYZFSR", Mpesa.SDK.Account.IdentifierTypeEnum.Organization);

            // Reverse C2B Transaction. Can also do forn B2C
            var reverse = await c2b.RequestReversal("OFRJUI4TAZ", "700");

            // Send Money through B2C
            var sendB2C = await b2c.SendMoney("254743793901", "1");

            return Ok("Ok");
        }
    }
}