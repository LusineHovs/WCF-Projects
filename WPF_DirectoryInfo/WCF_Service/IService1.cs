using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        IEnumerable<string> GetAllFiles();

        [OperationContract]
        string GetByName(string fileName);

        [OperationContract]
        void DeleteFile(string fileName);

        [OperationContract]
        void Post(string name, string content);

        [OperationContract]
        void Put(string name, string content);


    }
}
