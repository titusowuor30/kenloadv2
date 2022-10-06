using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mpesa.SDK.AspNetCore;
using Mpesa.SDK.AspNetCore.Binders;

namespace KenloadV2API.Controllers{
    public class WebhookController : ControllerBase
    {
        private readonly ILipaNaMpesa lnm;
        private readonly IC2B c2b;
        private readonly IB2C b2c;
        private readonly ILogger logger;

        [HttpPost("{requestId}/balance")]
        public IActionResult QueryBalanceCallback(string requestId, BalanceQueryCallbackResponse response)
        {
            if (response == null)
                return Ok(new { ResultCode = 1, ResultDesc = "Rejecting the transaction" });

            if (response.Success)
                logger.LogInformation("Balance Callback Data: {@Balance}", response.Data);
            else
                logger.LogError("Balance Callback Error: {@Error}", response.Error);

            return Ok(new { ResultCode = "00000000", ResponseDesc = "success" });
        }

        [HttpPost("{requestId}/status")]
        public IActionResult QueryStatusCallback(string requestId, StatusQueryCallbackResponse response)
        {
            if (response == null)
                return Ok(new { ResultCode = 1, ResultDesc = "Rejecting the transaction" });

            if (response.Success)
                logger.LogInformation("Transaction Status Callback Data: {@Data}", response.Data);
            else
                logger.LogError("Transaction Status Callback Error: {@Error}", response.Error);

            return Ok(new { ResultCode = "00000000", ResponseDesc = "success" });
        }

        [HttpPost("{requestId}/reversal")]
        public IActionResult ReverseCallback(string requestId, ReverseCallbackResponse response)
        {
            if (response == null)
                return Ok(new { ResultCode = 1, ResultDesc = "Rejecting the transaction" });

            if (response.Success)
                logger.LogInformation("Reverse Transaction Callback Data: {@Data}", response.Data);
            else
                logger.LogError("Reverse transaction Callback Error: {@Error}", response.Error);

            return Ok(new { ResultCode = "00000000", ResponseDesc = "success" });
        }

        [HttpPost("{requestId}/b2c")]
        public IActionResult B2CCallback(string requestId, B2CCallbackResponse response)
        {
            if (response == null)
                return Ok(new { ResultCode = 1, ResultDesc = "Rejecting the transaction" });

            if (response.Success)
                logger.LogInformation("B2C Callback Data: {@Data}", response.Data);
            else
                logger.LogError("B2C Callback Error: {@Error}", response.Error);

            return Ok(new { ResultCode = "00000000", ResponseDesc = "success" });
        }

        [HttpPost("{requestId}/lnm")]
        public IActionResult LipaNaMpesaCallback(string requestId, LipaNaMpesaCallbackResponse response)
        {
            if (response == null)
                return Ok(new { ResultCode = 1, ResultDesc = "Rejecting the transaction" });

            if (response.Success)
                logger.LogInformation("Lipa Na Mpesa Callback Data: {@Data}", response.Data);
            else
                logger.LogError("Lipa Na Mpesa Callback Error: {@Error}", response.Error);

            return Ok(new { ResultCode = "00000000", ResponseDesc = "success" });
        }

        [HttpPost("c2b/confirmation")]
        public IActionResult C2BPaymentCallback(C2BCallbackResponse response)
        {
            if (response == null)
                return Ok(new { ResultCode = 1, ResultDesc = "Rejecting the transaction" });

            if (response.Success)
                logger.LogInformation("C2B Payment Confirmation: {@Data}", response.Data);

            return Ok(new { ResultCode = 0, ResponseDesc = "success" });
        }
    }
}