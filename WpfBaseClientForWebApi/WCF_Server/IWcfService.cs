using System.ServiceModel;

namespace WCF_Server
{
    [ServiceContract]
    public interface IWcfService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        string GetValue();
    }
}
