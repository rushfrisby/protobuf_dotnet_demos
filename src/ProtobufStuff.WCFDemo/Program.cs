using System;
using System.ServiceModel;

namespace ProtobufStuff.WCFDemo
{
    class Program
    {
        private static void Main()
        {
            Console.WriteLine("Starting WCF Service");
            var host = new ServiceHost(typeof(ServiceDemo));
            host.Open();

            var request = new ServiceRequest
            {
                Apples = 4,
                Bananas = 3,
                Coconuts = 5
            };

            Console.WriteLine("Calling DoSomething method");
            var response = ServiceHelper.Using<IServiceDemo, ServiceResponse>(x => x.DoSomething(request), "ServiceDemoEndpoint");

            host.Close();

            Console.WriteLine("Result: {0}", response.Total);
            Console.ReadLine();
        }
    }
}
