namespace ProtobufStuff.WCFDemo
{
    public class ServiceDemo : IServiceDemo
    {
        public ServiceResponse DoSomething(ServiceRequest request)
        {
            return new ServiceResponse
            {
                Total = request.Apples + request.Bananas + request.Coconuts
            };
        }
    }
}
