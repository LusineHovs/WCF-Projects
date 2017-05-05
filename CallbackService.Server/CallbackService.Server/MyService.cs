using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CallbackService.Server
{
    [ServiceContract(CallbackContract = typeof(IMyServiceCallback))]
    public interface IMyService
    {
        [OperationContract]
        void OpenSession();
    }

    public interface IMyServiceCallback
    {
        [OperationContract]
        void OnCallback();
    }

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class MyService : IMyService
    {
        public static IMyServiceCallback Callback;
        public static Timer Timer;

        public void OpenSession()
        {
            Console.WriteLine("> Session opened at {0}", DateTime.Now);
            Callback = OperationContext.Current.GetCallbackChannel<IMyServiceCallback>();

            Timer = new Timer(1000);
            Timer.Elapsed += OnTimerElapsed;
            Timer.Enabled = true;
        }

        void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            Callback.OnCallback();
        }
    }
}
