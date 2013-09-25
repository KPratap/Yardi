﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.269.
// 
#pragma warning disable 1591

namespace ConsWebSvcTest.YardiQA {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ItfCollectionsSoap", Namespace="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections")]
    public partial class ItfCollections : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetPropertyConfigurationsOperationCompleted;
        
        private System.Threading.SendOrPostCallback Get_CollectionsLeaseInfoOperationCompleted;
        
        private System.Threading.SendOrPostCallback Import_CollectionsInfoOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetVersionNumber_StrOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetVersionNumberOperationCompleted;
        
        private System.Threading.SendOrPostCallback PingOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public ItfCollections() {
            this.Url = global::ConsWebSvcTest.Properties.Settings.Default.ConsWebSvcTest_com_iyardiasp_www_ItfCollections;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetPropertyConfigurationsCompletedEventHandler GetPropertyConfigurationsCompleted;
        
        /// <remarks/>
        public event Get_CollectionsLeaseInfoCompletedEventHandler Get_CollectionsLeaseInfoCompleted;
        
        /// <remarks/>
        public event Import_CollectionsInfoCompletedEventHandler Import_CollectionsInfoCompleted;
        
        /// <remarks/>
        public event GetVersionNumber_StrCompletedEventHandler GetVersionNumber_StrCompleted;
        
        /// <remarks/>
        public event GetVersionNumberCompletedEventHandler GetVersionNumberCompleted;
        
        /// <remarks/>
        public event PingCompletedEventHandler PingCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections/GetPropertyConfigura" +
            "tions", RequestNamespace="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", ResponseNamespace="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetPropertyConfigurations(string UserName, string Password, string ServerName, string Database, string Platform, string InterfaceEntity) {
            object[] results = this.Invoke("GetPropertyConfigurations", new object[] {
                        UserName,
                        Password,
                        ServerName,
                        Database,
                        Platform,
                        InterfaceEntity});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void GetPropertyConfigurationsAsync(string UserName, string Password, string ServerName, string Database, string Platform, string InterfaceEntity) {
            this.GetPropertyConfigurationsAsync(UserName, Password, ServerName, Database, Platform, InterfaceEntity, null);
        }
        
        /// <remarks/>
        public void GetPropertyConfigurationsAsync(string UserName, string Password, string ServerName, string Database, string Platform, string InterfaceEntity, object userState) {
            if ((this.GetPropertyConfigurationsOperationCompleted == null)) {
                this.GetPropertyConfigurationsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetPropertyConfigurationsOperationCompleted);
            }
            this.InvokeAsync("GetPropertyConfigurations", new object[] {
                        UserName,
                        Password,
                        ServerName,
                        Database,
                        Platform,
                        InterfaceEntity}, this.GetPropertyConfigurationsOperationCompleted, userState);
        }
        
        private void OnGetPropertyConfigurationsOperationCompleted(object arg) {
            if ((this.GetPropertyConfigurationsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetPropertyConfigurationsCompleted(this, new GetPropertyConfigurationsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections/Get_CollectionsLease" +
            "Info", RequestNamespace="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", ResponseNamespace="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode Get_CollectionsLeaseInfo(string UserName, string Password, string ServerName, string Database, string Platform, string CollectionsAgency, string YardiPropertyId) {
            object[] results = this.Invoke("Get_CollectionsLeaseInfo", new object[] {
                        UserName,
                        Password,
                        ServerName,
                        Database,
                        Platform,
                        CollectionsAgency,
                        YardiPropertyId});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void Get_CollectionsLeaseInfoAsync(string UserName, string Password, string ServerName, string Database, string Platform, string CollectionsAgency, string YardiPropertyId) {
            this.Get_CollectionsLeaseInfoAsync(UserName, Password, ServerName, Database, Platform, CollectionsAgency, YardiPropertyId, null);
        }
        
        /// <remarks/>
        public void Get_CollectionsLeaseInfoAsync(string UserName, string Password, string ServerName, string Database, string Platform, string CollectionsAgency, string YardiPropertyId, object userState) {
            if ((this.Get_CollectionsLeaseInfoOperationCompleted == null)) {
                this.Get_CollectionsLeaseInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGet_CollectionsLeaseInfoOperationCompleted);
            }
            this.InvokeAsync("Get_CollectionsLeaseInfo", new object[] {
                        UserName,
                        Password,
                        ServerName,
                        Database,
                        Platform,
                        CollectionsAgency,
                        YardiPropertyId}, this.Get_CollectionsLeaseInfoOperationCompleted, userState);
        }
        
        private void OnGet_CollectionsLeaseInfoOperationCompleted(object arg) {
            if ((this.Get_CollectionsLeaseInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Get_CollectionsLeaseInfoCompleted(this, new Get_CollectionsLeaseInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections/Import_CollectionsIn" +
            "fo", RequestNamespace="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", ResponseNamespace="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode Import_CollectionsInfo(string UserName, string Password, string ServerName, string Database, string Platform, string CollectionsAgency, System.Xml.XmlNode XmlDoc) {
            object[] results = this.Invoke("Import_CollectionsInfo", new object[] {
                        UserName,
                        Password,
                        ServerName,
                        Database,
                        Platform,
                        CollectionsAgency,
                        XmlDoc});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void Import_CollectionsInfoAsync(string UserName, string Password, string ServerName, string Database, string Platform, string CollectionsAgency, System.Xml.XmlNode XmlDoc) {
            this.Import_CollectionsInfoAsync(UserName, Password, ServerName, Database, Platform, CollectionsAgency, XmlDoc, null);
        }
        
        /// <remarks/>
        public void Import_CollectionsInfoAsync(string UserName, string Password, string ServerName, string Database, string Platform, string CollectionsAgency, System.Xml.XmlNode XmlDoc, object userState) {
            if ((this.Import_CollectionsInfoOperationCompleted == null)) {
                this.Import_CollectionsInfoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnImport_CollectionsInfoOperationCompleted);
            }
            this.InvokeAsync("Import_CollectionsInfo", new object[] {
                        UserName,
                        Password,
                        ServerName,
                        Database,
                        Platform,
                        CollectionsAgency,
                        XmlDoc}, this.Import_CollectionsInfoOperationCompleted, userState);
        }
        
        private void OnImport_CollectionsInfoOperationCompleted(object arg) {
            if ((this.Import_CollectionsInfoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Import_CollectionsInfoCompleted(this, new Import_CollectionsInfoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections/GetVersionNumber_Str" +
            "", RequestNamespace="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", ResponseNamespace="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetVersionNumber_Str() {
            object[] results = this.Invoke("GetVersionNumber_Str", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetVersionNumber_StrAsync() {
            this.GetVersionNumber_StrAsync(null);
        }
        
        /// <remarks/>
        public void GetVersionNumber_StrAsync(object userState) {
            if ((this.GetVersionNumber_StrOperationCompleted == null)) {
                this.GetVersionNumber_StrOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetVersionNumber_StrOperationCompleted);
            }
            this.InvokeAsync("GetVersionNumber_Str", new object[0], this.GetVersionNumber_StrOperationCompleted, userState);
        }
        
        private void OnGetVersionNumber_StrOperationCompleted(object arg) {
            if ((this.GetVersionNumber_StrCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetVersionNumber_StrCompleted(this, new GetVersionNumber_StrCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections/GetVersionNumber", RequestNamespace="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", ResponseNamespace="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetVersionNumber() {
            object[] results = this.Invoke("GetVersionNumber", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetVersionNumberAsync() {
            this.GetVersionNumberAsync(null);
        }
        
        /// <remarks/>
        public void GetVersionNumberAsync(object userState) {
            if ((this.GetVersionNumberOperationCompleted == null)) {
                this.GetVersionNumberOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetVersionNumberOperationCompleted);
            }
            this.InvokeAsync("GetVersionNumber", new object[0], this.GetVersionNumberOperationCompleted, userState);
        }
        
        private void OnGetVersionNumberOperationCompleted(object arg) {
            if ((this.GetVersionNumberCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetVersionNumberCompleted(this, new GetVersionNumberCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections/Ping", RequestNamespace="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", ResponseNamespace="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Ping() {
            object[] results = this.Invoke("Ping", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void PingAsync() {
            this.PingAsync(null);
        }
        
        /// <remarks/>
        public void PingAsync(object userState) {
            if ((this.PingOperationCompleted == null)) {
                this.PingOperationCompleted = new System.Threading.SendOrPostCallback(this.OnPingOperationCompleted);
            }
            this.InvokeAsync("Ping", new object[0], this.PingOperationCompleted, userState);
        }
        
        private void OnPingOperationCompleted(object arg) {
            if ((this.PingCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.PingCompleted(this, new PingCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetPropertyConfigurationsCompletedEventHandler(object sender, GetPropertyConfigurationsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetPropertyConfigurationsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetPropertyConfigurationsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void Get_CollectionsLeaseInfoCompletedEventHandler(object sender, Get_CollectionsLeaseInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Get_CollectionsLeaseInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal Get_CollectionsLeaseInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void Import_CollectionsInfoCompletedEventHandler(object sender, Import_CollectionsInfoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Import_CollectionsInfoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal Import_CollectionsInfoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetVersionNumber_StrCompletedEventHandler(object sender, GetVersionNumber_StrCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetVersionNumber_StrCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetVersionNumber_StrCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetVersionNumberCompletedEventHandler(object sender, GetVersionNumberCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetVersionNumberCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetVersionNumberCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void PingCompletedEventHandler(object sender, PingCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class PingCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal PingCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591