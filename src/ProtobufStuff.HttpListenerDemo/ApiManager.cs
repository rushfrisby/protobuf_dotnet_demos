namespace ProtobufStuff.HttpListenerDemo
{
    public class ApiManager
    {
        private static bool _started;
        private static WebServer _ws;

        public static void StartWebServer()
        {
            if (_started)
            {
                return;
            }

            //setup routes
            RouteManager.AddRoute("/Now", RouteHandlers.Now);

            _ws = new WebServer();
            _ws.Start();

            _started = true;
        }

        public static void StopWebServer()
        {
            if (!_started)
                return;

            _ws.Stop();
            _started = false;
        }
    }
}