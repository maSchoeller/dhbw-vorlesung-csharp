﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MaSchoeller.Dublin.Client.Proxies.Calculations
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MaSchoeller.Dublin.Client.Proxies.Calculations.ICalculationService")]
    public interface ICalculationService
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculationService/GetTestData", ReplyAction="http://tempuri.org/ICalculationService/GetTestDataResponse")]
        string GetTestData();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculationService/GetTestData", ReplyAction="http://tempuri.org/ICalculationService/GetTestDataResponse")]
        System.Threading.Tasks.Task<string> GetTestDataAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public interface ICalculationServiceChannel : MaSchoeller.Dublin.Client.Proxies.Calculations.ICalculationService, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class CalculationServiceClient : System.ServiceModel.ClientBase<MaSchoeller.Dublin.Client.Proxies.Calculations.ICalculationService>, MaSchoeller.Dublin.Client.Proxies.Calculations.ICalculationService
    {
        
        public CalculationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public string GetTestData()
        {
            return base.Channel.GetTestData();
        }
        
        public System.Threading.Tasks.Task<string> GetTestDataAsync()
        {
            return base.Channel.GetTestDataAsync();
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
    }
}
