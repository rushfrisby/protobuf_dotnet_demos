using System;
using System.ServiceModel;

namespace ProtobufStuff.WCFDemo
{
    public class ServiceHelper
    {
        /// <summary>
        /// WCF proxys do not clean up properly if they throw an exception. This method ensures that the service proxy is handled correctly.
        ///   Do not call TService.Close() or TService.Abort() within the action lambda.
        /// </summary>
        /// <typeparam name="TService">
        /// The type of the service to use
        /// </typeparam>
        /// <param name="action">
        /// Lambda of the action to perform with the service
        /// </param>
        public static void Using<TService>(Action<TService> action) where TService : ICommunicationObject, IDisposable, new()
        {
            using (var service = new TService())
            {
                var success = false;
                try
                {
                    action(service);
                    if (service.State != CommunicationState.Faulted)
                    {
                        service.Close();
                        success = true;
                    }
                }
                finally
                {
                    if (!success)
                    {
                        service.Abort();
                    }
                }
            }
        }

        /// <summary>
        /// WCF proxys do not clean up properly if they throw an exception. This method ensures that the service proxy is handled correctly.
        ///   Do not call TServiceContract.Close() or TServiceContract.Abort() within the action lambda.
        /// </summary>
        /// <typeparam name="TServiceContract">
        /// The service contract used to create the communication channel.
        /// </typeparam>
        /// <param name="action">
        /// Lambda of the action to perform with the service
        /// </param>
        /// <param name="endpointConfigurationName">
        /// The name of the endpoint configuration name.
        /// </param>
        public static void Using<TServiceContract>(Action<TServiceContract> action, string endpointConfigurationName)
        {
            Using<TServiceContract, bool>(x =>
            {
                action(x);
                return true;
            }, endpointConfigurationName);
        }

        public static TResult Using<TServiceContract, TResult>(Func<TServiceContract, TResult> action, string endpointConfigurationName)
        {
            TResult result;

            using (var channelFactory = new ChannelFactory<TServiceContract>(endpointConfigurationName))
            {
                channelFactory.Open();
                var success = false;
                try
                {
                    var client = channelFactory.CreateChannel();
                    result = action(client);

                    if (channelFactory.State != CommunicationState.Faulted)
                    {
                        channelFactory.Close();
                        success = true;
                    }
                }
                finally
                {
                    if (!success)
                    {
                        channelFactory.Abort();
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Use this for contracts which have a callback interface.
        /// </summary>
        public static void UsingDuplex<TServiceContract, TCallbackImplementationType>(Action<TServiceContract> action, string endpointConfigurationName)
        {
            var callbackImplementation = Activator.CreateInstance<TCallbackImplementationType>();
            UsingDuplex(action, callbackImplementation, endpointConfigurationName);
        }

        /// <summary>
        /// Use this for contracts which have a callback interface.
        /// </summary>
        public static void UsingDuplex<TServiceContract>(Action<TServiceContract> action, object callbackImplementation, string endpointConfigurationName)
        {
            using (var channelFactory = new DuplexChannelFactory<TServiceContract>(callbackImplementation, endpointConfigurationName))
            {
                channelFactory.Open();
                var success = false;
                try
                {
                    var client = channelFactory.CreateChannel();
                    action(client);

                    if (channelFactory.State != CommunicationState.Faulted)
                    {
                        channelFactory.Close();
                        success = true;
                    }
                }
                finally
                {
                    if (!success)
                    {
                        channelFactory.Abort();
                    }
                }
            }
        }
    }
}
