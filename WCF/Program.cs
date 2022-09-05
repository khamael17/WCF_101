using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF
{
    internal class Program
    {
        [ServiceContract]
        public interface IMessageService
        {
            [OperationContract]
            string[] GetMessages();
        }

        [ServiceBehavior(InstanceContextMode =InstanceContextMode.Single)]
        public class MessageServ: IMessageService
        {
            public string[] GetMessages()
            {
                return new string[] {"dupain","beurre","coke"};
            }
        }
        static void Main(string[] args)
        {
            var uris = new Uri[1];
           
            string address = "net.tcp://localhost:6565/MessageServ";
            uris[0] = new Uri(address);
            IMessageService message =new MessageServ();
            ServiceHost host= new ServiceHost(message,uris);
            var binding = new NetTcpBinding(SecurityMode.None);
            host.AddServiceEndpoint(typeof(IMessageService), binding, "");
            host.Opened += Host_Opened;
            host.Open();
            Console.ReadLine();
        }

        private static void Host_Opened(object sender,EventArgs e)
        {
            Console.WriteLine("nessage service started");
        }
    }
}
