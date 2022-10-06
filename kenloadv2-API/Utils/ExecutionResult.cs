namespace KenloadV2API.Utils
{
    public class ExecutionResult
    {
        public bool IsOkay { get; set; } = false;
        public string Message { get; set; }
        public object Result { get; set; }

        public ExecutionResult()
        {
        }

        public ExecutionResult(bool isokay) => IsOkay = isokay;

        public ExecutionResult(bool isokay, string message) : this(isokay) => Message = message;

        public ExecutionResult(bool isOkay, string message, object result) : this(isOkay, message) => Result = result;
    }
}
