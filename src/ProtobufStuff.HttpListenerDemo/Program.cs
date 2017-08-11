using System;

namespace ProtobufStuff.HttpListenerDemo
{
    class Program
    {
        static void Main()
        {

            ApiManager.StartWebServer();

            Console.WriteLine("Web server is runnning. Hit <Enter> to exit.");
            Console.ReadLine();

            ApiManager.StopWebServer();
        }
    }
}
