using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CallbackService.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new ServiceHost(typeof(MyService));
            host.Open();
            Console.WriteLine("Service started at {0}", DateTime.Now);
            Console.WriteLine("Press key to stop the service.");
            Console.ReadLine();
            host.Close();
        }
    }
}
