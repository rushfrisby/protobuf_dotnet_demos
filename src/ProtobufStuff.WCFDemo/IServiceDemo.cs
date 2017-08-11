using System.ServiceModel;

namespace ProtobufStuff.WCFDemo
{
    [ServiceContract]
    public interface IServiceDemo
    {
        [OperationContract]
        ServiceResponse DoSomething(ServiceRequest request);
    }
}
