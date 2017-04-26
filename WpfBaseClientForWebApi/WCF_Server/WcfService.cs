namespace WCF_Server
{
    public class WcfService : IWcfService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public string GetValue()
        {
            return "Hello WCF Server";
        }
    }
}
