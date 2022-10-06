namespace KenloadV2API.Datatransfer
{
    public interface IDatatransfer
    {
        void FireAndForgetJob();
        void DataExchange();
        void DelayedJob();
        void ContinuationJob();
    }
}
