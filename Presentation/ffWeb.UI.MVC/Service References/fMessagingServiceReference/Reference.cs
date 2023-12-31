﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ffWeb.UI.MVC.fMessagingServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Message", Namespace="http://schemas.datacontract.org/2004/07/fMessagingSystem.Entities")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ffWeb.UI.MVC.fMessagingServiceReference.EmailMessage))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(ffWeb.UI.MVC.fMessagingServiceReference.SMSMessage))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.Collections.Generic.List<ffWeb.UI.MVC.fMessagingServiceReference.Message>))]
    public partial class Message : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private object BodyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string addressFromField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string addressToField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime messageDateField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public object Body {
            get {
                return this.BodyField;
            }
            set {
                if ((object.ReferenceEquals(this.BodyField, value) != true)) {
                    this.BodyField = value;
                    this.RaisePropertyChanged("Body");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string addressFrom {
            get {
                return this.addressFromField;
            }
            set {
                if ((object.ReferenceEquals(this.addressFromField, value) != true)) {
                    this.addressFromField = value;
                    this.RaisePropertyChanged("addressFrom");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string addressTo {
            get {
                return this.addressToField;
            }
            set {
                if ((object.ReferenceEquals(this.addressToField, value) != true)) {
                    this.addressToField = value;
                    this.RaisePropertyChanged("addressTo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime messageDate {
            get {
                return this.messageDateField;
            }
            set {
                if ((this.messageDateField.Equals(value) != true)) {
                    this.messageDateField = value;
                    this.RaisePropertyChanged("messageDate");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EmailMessage", Namespace="http://schemas.datacontract.org/2004/07/fMessagingSystem.Entities")]
    [System.SerializableAttribute()]
    public partial class EmailMessage : ffWeb.UI.MVC.fMessagingServiceReference.Message {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string passwordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string subjectField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string usernameField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string password {
            get {
                return this.passwordField;
            }
            set {
                if ((object.ReferenceEquals(this.passwordField, value) != true)) {
                    this.passwordField = value;
                    this.RaisePropertyChanged("password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string subject {
            get {
                return this.subjectField;
            }
            set {
                if ((object.ReferenceEquals(this.subjectField, value) != true)) {
                    this.subjectField = value;
                    this.RaisePropertyChanged("subject");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string username {
            get {
                return this.usernameField;
            }
            set {
                if ((object.ReferenceEquals(this.usernameField, value) != true)) {
                    this.usernameField = value;
                    this.RaisePropertyChanged("username");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SMSMessage", Namespace="http://schemas.datacontract.org/2004/07/fMessagingSystem.Entities")]
    [System.SerializableAttribute()]
    public partial class SMSMessage : ffWeb.UI.MVC.fMessagingServiceReference.Message {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="fMessagingServiceReference.IMessagingService")]
    public interface IMessagingService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessagingService/SendListMessage", ReplyAction="http://tempuri.org/IMessagingService/SendListMessageResponse")]
        void SendListMessage(System.Collections.Generic.List<ffWeb.UI.MVC.fMessagingServiceReference.Message> ms);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessagingService/SendListMessage", ReplyAction="http://tempuri.org/IMessagingService/SendListMessageResponse")]
        System.Threading.Tasks.Task SendListMessageAsync(System.Collections.Generic.List<ffWeb.UI.MVC.fMessagingServiceReference.Message> ms);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessagingService/SendMessage", ReplyAction="http://tempuri.org/IMessagingService/SendMessageResponse")]
        void SendMessage(ffWeb.UI.MVC.fMessagingServiceReference.Message m);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessagingService/SendMessage", ReplyAction="http://tempuri.org/IMessagingService/SendMessageResponse")]
        System.Threading.Tasks.Task SendMessageAsync(ffWeb.UI.MVC.fMessagingServiceReference.Message m);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessagingService/SendSms", ReplyAction="http://tempuri.org/IMessagingService/SendSmsResponse")]
        void SendSms(string message, string addressTo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessagingService/SendSms", ReplyAction="http://tempuri.org/IMessagingService/SendSmsResponse")]
        System.Threading.Tasks.Task SendSmsAsync(string message, string addressTo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessagingService/SendSMSMessage", ReplyAction="http://tempuri.org/IMessagingService/SendSMSMessageResponse")]
        void SendSMSMessage(ffWeb.UI.MVC.fMessagingServiceReference.SMSMessage message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessagingService/SendSMSMessage", ReplyAction="http://tempuri.org/IMessagingService/SendSMSMessageResponse")]
        System.Threading.Tasks.Task SendSMSMessageAsync(ffWeb.UI.MVC.fMessagingServiceReference.SMSMessage message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessagingService/SendEmailMessage", ReplyAction="http://tempuri.org/IMessagingService/SendEmailMessageResponse")]
        void SendEmailMessage(ffWeb.UI.MVC.fMessagingServiceReference.EmailMessage message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessagingService/SendEmailMessage", ReplyAction="http://tempuri.org/IMessagingService/SendEmailMessageResponse")]
        System.Threading.Tasks.Task SendEmailMessageAsync(ffWeb.UI.MVC.fMessagingServiceReference.EmailMessage message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessagingService/SendEmail", ReplyAction="http://tempuri.org/IMessagingService/SendEmailResponse")]
        void SendEmail(string addressTo, string Body);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessagingService/SendEmail", ReplyAction="http://tempuri.org/IMessagingService/SendEmailResponse")]
        System.Threading.Tasks.Task SendEmailAsync(string addressTo, string Body);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessagingService/SendEmailExt", ReplyAction="http://tempuri.org/IMessagingService/SendEmailExtResponse")]
        void SendEmailExt(string addressTo, string addressFrom, string subject, string Body);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessagingService/SendEmailExt", ReplyAction="http://tempuri.org/IMessagingService/SendEmailExtResponse")]
        System.Threading.Tasks.Task SendEmailExtAsync(string addressTo, string addressFrom, string subject, string Body);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessagingService/ReadMessageFromModem", ReplyAction="http://tempuri.org/IMessagingService/ReadMessageFromModemResponse")]
        void ReadMessageFromModem();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessagingService/ReadMessageFromModem", ReplyAction="http://tempuri.org/IMessagingService/ReadMessageFromModemResponse")]
        System.Threading.Tasks.Task ReadMessageFromModemAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMessagingServiceChannel : ffWeb.UI.MVC.fMessagingServiceReference.IMessagingService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MessagingServiceClient : System.ServiceModel.ClientBase<ffWeb.UI.MVC.fMessagingServiceReference.IMessagingService>, ffWeb.UI.MVC.fMessagingServiceReference.IMessagingService {
        
        public MessagingServiceClient() {
        }
        
        public MessagingServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MessagingServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MessagingServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MessagingServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void SendListMessage(System.Collections.Generic.List<ffWeb.UI.MVC.fMessagingServiceReference.Message> ms) {
            base.Channel.SendListMessage(ms);
        }
        
        public System.Threading.Tasks.Task SendListMessageAsync(System.Collections.Generic.List<ffWeb.UI.MVC.fMessagingServiceReference.Message> ms) {
            return base.Channel.SendListMessageAsync(ms);
        }
        
        public void SendMessage(ffWeb.UI.MVC.fMessagingServiceReference.Message m) {
            base.Channel.SendMessage(m);
        }
        
        public System.Threading.Tasks.Task SendMessageAsync(ffWeb.UI.MVC.fMessagingServiceReference.Message m) {
            return base.Channel.SendMessageAsync(m);
        }
        
        public void SendSms(string message, string addressTo) {
            base.Channel.SendSms(message, addressTo);
        }
        
        public System.Threading.Tasks.Task SendSmsAsync(string message, string addressTo) {
            return base.Channel.SendSmsAsync(message, addressTo);
        }
        
        public void SendSMSMessage(ffWeb.UI.MVC.fMessagingServiceReference.SMSMessage message) {
            base.Channel.SendSMSMessage(message);
        }
        
        public System.Threading.Tasks.Task SendSMSMessageAsync(ffWeb.UI.MVC.fMessagingServiceReference.SMSMessage message) {
            return base.Channel.SendSMSMessageAsync(message);
        }
        
        public void SendEmailMessage(ffWeb.UI.MVC.fMessagingServiceReference.EmailMessage message) {
            base.Channel.SendEmailMessage(message);
        }
        
        public System.Threading.Tasks.Task SendEmailMessageAsync(ffWeb.UI.MVC.fMessagingServiceReference.EmailMessage message) {
            return base.Channel.SendEmailMessageAsync(message);
        }
        
        public void SendEmail(string addressTo, string Body) {
            base.Channel.SendEmail(addressTo, Body);
        }
        
        public System.Threading.Tasks.Task SendEmailAsync(string addressTo, string Body) {
            return base.Channel.SendEmailAsync(addressTo, Body);
        }
        
        public void SendEmailExt(string addressTo, string addressFrom, string subject, string Body) {
            base.Channel.SendEmailExt(addressTo, addressFrom, subject, Body);
        }
        
        public System.Threading.Tasks.Task SendEmailExtAsync(string addressTo, string addressFrom, string subject, string Body) {
            return base.Channel.SendEmailExtAsync(addressTo, addressFrom, subject, Body);
        }
        
        public void ReadMessageFromModem() {
            base.Channel.ReadMessageFromModem();
        }
        
        public System.Threading.Tasks.Task ReadMessageFromModemAsync() {
            return base.Channel.ReadMessageFromModemAsync();
        }
    }
}
