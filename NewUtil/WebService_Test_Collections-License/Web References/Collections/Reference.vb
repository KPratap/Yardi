﻿'------------------------------------------------------------------------------
' <autogenerated>
'     This code was generated by a tool.
'     Runtime Version: 1.1.4322.2494
'
'     Changes to this file may cause incorrect behavior and will be lost if 
'     the code is regenerated.
' </autogenerated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'This source code was auto-generated by Microsoft.VSDesigner, Version 1.1.4322.2494.
'
Namespace Collections
    
    '<remarks/>
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="ItfCollectionsSoap", [Namespace]:="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections")>  _
    Public Class ItfCollections
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        '<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = "http://localhost/Voyager60_dev/Webservices/ItfCollections.asmx"
        End Sub
        
        '<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections/GetPropertyConfigura"& _ 
"tions", RequestNamespace:="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", ResponseNamespace:="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Overloads Function GetPropertyConfigurations(ByVal UserName As String, ByVal Password As String, ByVal ServerName As String, ByVal Database As String, ByVal Platform As String, ByVal InterfaceEntity As String, ByVal InterfaceLicense As String) As System.Xml.XmlNode
            Dim results() As Object = Me.Invoke("GetPropertyConfigurations", New Object() {UserName, Password, ServerName, Database, Platform, InterfaceEntity, InterfaceLicense})
            Return CType(results(0),System.Xml.XmlNode)
        End Function
        
        '<remarks/>
        Public Function BeginGetPropertyConfigurations(ByVal UserName As String, ByVal Password As String, ByVal ServerName As String, ByVal Database As String, ByVal Platform As String, ByVal InterfaceEntity As String, ByVal InterfaceLicense As String, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("GetPropertyConfigurations", New Object() {UserName, Password, ServerName, Database, Platform, InterfaceEntity, InterfaceLicense}, callback, asyncState)
        End Function
        
        '<remarks/>
        Public Function EndGetPropertyConfigurations(ByVal asyncResult As System.IAsyncResult) As System.Xml.XmlNode
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),System.Xml.XmlNode)
        End Function
        
        '<remarks/>
        <System.Web.Services.WebMethodAttribute(MessageName:="GetPropertyConfigurations1"),  _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections/(Deprecated)GetPrope"& _ 
"rtyConfigurations", RequestElementName:="(Deprecated)GetPropertyConfigurations", RequestNamespace:="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", ResponseElementName:="(Deprecated)GetPropertyConfigurationsResponse", ResponseNamespace:="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Overloads Function GetPropertyConfigurations(ByVal UserName As String, ByVal Password As String, ByVal ServerName As String, ByVal Database As String, ByVal Platform As String, ByVal InterfaceEntity As String) As <System.Xml.Serialization.XmlElementAttribute("(Deprecated)GetPropertyConfigurationsResult")> System.Xml.XmlNode
            Dim results() As Object = Me.Invoke("GetPropertyConfigurations1", New Object() {UserName, Password, ServerName, Database, Platform, InterfaceEntity})
            Return CType(results(0),System.Xml.XmlNode)
        End Function
        
        '<remarks/>
        Public Function BeginGetPropertyConfigurations1(ByVal UserName As String, ByVal Password As String, ByVal ServerName As String, ByVal Database As String, ByVal Platform As String, ByVal InterfaceEntity As String, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("GetPropertyConfigurations1", New Object() {UserName, Password, ServerName, Database, Platform, InterfaceEntity}, callback, asyncState)
        End Function
        
        '<remarks/>
        Public Function EndGetPropertyConfigurations1(ByVal asyncResult As System.IAsyncResult) As System.Xml.XmlNode
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),System.Xml.XmlNode)
        End Function
        
        '<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections/Get_CollectionsLease"& _ 
"Info", RequestNamespace:="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", ResponseNamespace:="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Overloads Function Get_CollectionsLeaseInfo(ByVal UserName As String, ByVal Password As String, ByVal ServerName As String, ByVal Database As String, ByVal Platform As String, ByVal CollectionsAgency As String, ByVal InterfaceLicense As String, ByVal YardiPropertyId As String) As System.Xml.XmlNode
            Dim results() As Object = Me.Invoke("Get_CollectionsLeaseInfo", New Object() {UserName, Password, ServerName, Database, Platform, CollectionsAgency, InterfaceLicense, YardiPropertyId})
            Return CType(results(0),System.Xml.XmlNode)
        End Function
        
        '<remarks/>
        Public Function BeginGet_CollectionsLeaseInfo(ByVal UserName As String, ByVal Password As String, ByVal ServerName As String, ByVal Database As String, ByVal Platform As String, ByVal CollectionsAgency As String, ByVal InterfaceLicense As String, ByVal YardiPropertyId As String, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("Get_CollectionsLeaseInfo", New Object() {UserName, Password, ServerName, Database, Platform, CollectionsAgency, InterfaceLicense, YardiPropertyId}, callback, asyncState)
        End Function
        
        '<remarks/>
        Public Function EndGet_CollectionsLeaseInfo(ByVal asyncResult As System.IAsyncResult) As System.Xml.XmlNode
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),System.Xml.XmlNode)
        End Function
        
        '<remarks/>
        <System.Web.Services.WebMethodAttribute(MessageName:="Get_CollectionsLeaseInfo1"),  _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections/(Deprecated)Get_Coll"& _ 
"ectionsLeaseInfo", RequestElementName:="(Deprecated)Get_CollectionsLeaseInfo", RequestNamespace:="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", ResponseElementName:="(Deprecated)Get_CollectionsLeaseInfoResponse", ResponseNamespace:="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Overloads Function Get_CollectionsLeaseInfo(ByVal UserName As String, ByVal Password As String, ByVal ServerName As String, ByVal Database As String, ByVal Platform As String, ByVal CollectionsAgency As String, ByVal YardiPropertyId As String) As <System.Xml.Serialization.XmlElementAttribute("(Deprecated)Get_CollectionsLeaseInfoResult")> System.Xml.XmlNode
            Dim results() As Object = Me.Invoke("Get_CollectionsLeaseInfo1", New Object() {UserName, Password, ServerName, Database, Platform, CollectionsAgency, YardiPropertyId})
            Return CType(results(0),System.Xml.XmlNode)
        End Function
        
        '<remarks/>
        Public Function BeginGet_CollectionsLeaseInfo1(ByVal UserName As String, ByVal Password As String, ByVal ServerName As String, ByVal Database As String, ByVal Platform As String, ByVal CollectionsAgency As String, ByVal YardiPropertyId As String, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("Get_CollectionsLeaseInfo1", New Object() {UserName, Password, ServerName, Database, Platform, CollectionsAgency, YardiPropertyId}, callback, asyncState)
        End Function
        
        '<remarks/>
        Public Function EndGet_CollectionsLeaseInfo1(ByVal asyncResult As System.IAsyncResult) As System.Xml.XmlNode
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),System.Xml.XmlNode)
        End Function
        
        '<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections/Get_CollectionsLease"& _ 
"Info_View", RequestNamespace:="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", ResponseNamespace:="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Overloads Function Get_CollectionsLeaseInfo_View(ByVal UserName As String, ByVal Password As String, ByVal ServerName As String, ByVal Database As String, ByVal Platform As String, ByVal CollectionsAgency As String, ByVal InterfaceLicense As String, ByVal YardiPropertyId As String) As System.Xml.XmlNode
            Dim results() As Object = Me.Invoke("Get_CollectionsLeaseInfo_View", New Object() {UserName, Password, ServerName, Database, Platform, CollectionsAgency, InterfaceLicense, YardiPropertyId})
            Return CType(results(0),System.Xml.XmlNode)
        End Function
        
        '<remarks/>
        Public Function BeginGet_CollectionsLeaseInfo_View(ByVal UserName As String, ByVal Password As String, ByVal ServerName As String, ByVal Database As String, ByVal Platform As String, ByVal CollectionsAgency As String, ByVal InterfaceLicense As String, ByVal YardiPropertyId As String, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("Get_CollectionsLeaseInfo_View", New Object() {UserName, Password, ServerName, Database, Platform, CollectionsAgency, InterfaceLicense, YardiPropertyId}, callback, asyncState)
        End Function
        
        '<remarks/>
        Public Function EndGet_CollectionsLeaseInfo_View(ByVal asyncResult As System.IAsyncResult) As System.Xml.XmlNode
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),System.Xml.XmlNode)
        End Function
        
        '<remarks/>
        <System.Web.Services.WebMethodAttribute(MessageName:="Get_CollectionsLeaseInfo_View1"),  _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections/(Deprecated)Get_Coll"& _ 
"ectionsLeaseInfo_View", RequestElementName:="(Deprecated)Get_CollectionsLeaseInfo_View", RequestNamespace:="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", ResponseElementName:="(Deprecated)Get_CollectionsLeaseInfo_ViewResponse", ResponseNamespace:="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Overloads Function Get_CollectionsLeaseInfo_View(ByVal UserName As String, ByVal Password As String, ByVal ServerName As String, ByVal Database As String, ByVal Platform As String, ByVal CollectionsAgency As String, ByVal YardiPropertyId As String) As <System.Xml.Serialization.XmlElementAttribute("(Deprecated)Get_CollectionsLeaseInfo_ViewResult")> System.Xml.XmlNode
            Dim results() As Object = Me.Invoke("Get_CollectionsLeaseInfo_View1", New Object() {UserName, Password, ServerName, Database, Platform, CollectionsAgency, YardiPropertyId})
            Return CType(results(0),System.Xml.XmlNode)
        End Function
        
        '<remarks/>
        Public Function BeginGet_CollectionsLeaseInfo_View1(ByVal UserName As String, ByVal Password As String, ByVal ServerName As String, ByVal Database As String, ByVal Platform As String, ByVal CollectionsAgency As String, ByVal YardiPropertyId As String, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("Get_CollectionsLeaseInfo_View1", New Object() {UserName, Password, ServerName, Database, Platform, CollectionsAgency, YardiPropertyId}, callback, asyncState)
        End Function
        
        '<remarks/>
        Public Function EndGet_CollectionsLeaseInfo_View1(ByVal asyncResult As System.IAsyncResult) As System.Xml.XmlNode
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),System.Xml.XmlNode)
        End Function
        
        '<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections/Import_CollectionsIn"& _ 
"fo", RequestNamespace:="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", ResponseNamespace:="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Overloads Function Import_CollectionsInfo(ByVal UserName As String, ByVal Password As String, ByVal ServerName As String, ByVal Database As String, ByVal Platform As String, ByVal CollectionsAgency As String, ByVal InterfaceLicense As String, ByVal XmlDoc As System.Xml.XmlNode) As System.Xml.XmlNode
            Dim results() As Object = Me.Invoke("Import_CollectionsInfo", New Object() {UserName, Password, ServerName, Database, Platform, CollectionsAgency, InterfaceLicense, XmlDoc})
            Return CType(results(0),System.Xml.XmlNode)
        End Function
        
        '<remarks/>
        Public Function BeginImport_CollectionsInfo(ByVal UserName As String, ByVal Password As String, ByVal ServerName As String, ByVal Database As String, ByVal Platform As String, ByVal CollectionsAgency As String, ByVal InterfaceLicense As String, ByVal XmlDoc As System.Xml.XmlNode, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("Import_CollectionsInfo", New Object() {UserName, Password, ServerName, Database, Platform, CollectionsAgency, InterfaceLicense, XmlDoc}, callback, asyncState)
        End Function
        
        '<remarks/>
        Public Function EndImport_CollectionsInfo(ByVal asyncResult As System.IAsyncResult) As System.Xml.XmlNode
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),System.Xml.XmlNode)
        End Function
        
        '<remarks/>
        <System.Web.Services.WebMethodAttribute(MessageName:="Import_CollectionsInfo1"),  _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections/(Deprecated)Import_C"& _ 
"ollectionsInfo", RequestElementName:="(Deprecated)Import_CollectionsInfo", RequestNamespace:="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", ResponseElementName:="(Deprecated)Import_CollectionsInfoResponse", ResponseNamespace:="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Overloads Function Import_CollectionsInfo(ByVal UserName As String, ByVal Password As String, ByVal ServerName As String, ByVal Database As String, ByVal Platform As String, ByVal CollectionsAgency As String, ByVal XmlDoc As System.Xml.XmlNode) As <System.Xml.Serialization.XmlElementAttribute("(Deprecated)Import_CollectionsInfoResult")> System.Xml.XmlNode
            Dim results() As Object = Me.Invoke("Import_CollectionsInfo1", New Object() {UserName, Password, ServerName, Database, Platform, CollectionsAgency, XmlDoc})
            Return CType(results(0),System.Xml.XmlNode)
        End Function
        
        '<remarks/>
        Public Function BeginImport_CollectionsInfo1(ByVal UserName As String, ByVal Password As String, ByVal ServerName As String, ByVal Database As String, ByVal Platform As String, ByVal CollectionsAgency As String, ByVal XmlDoc As System.Xml.XmlNode, ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("Import_CollectionsInfo1", New Object() {UserName, Password, ServerName, Database, Platform, CollectionsAgency, XmlDoc}, callback, asyncState)
        End Function
        
        '<remarks/>
        Public Function EndImport_CollectionsInfo1(ByVal asyncResult As System.IAsyncResult) As System.Xml.XmlNode
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),System.Xml.XmlNode)
        End Function
        
        '<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections/(Deprecated)GetVersi"& _ 
"onNumber_Str", RequestElementName:="(Deprecated)GetVersionNumber_Str", RequestNamespace:="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", ResponseElementName:="(Deprecated)GetVersionNumber_StrResponse", ResponseNamespace:="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function GetVersionNumber_Str() As <System.Xml.Serialization.XmlElementAttribute("(Deprecated)GetVersionNumber_StrResult")> String
            Dim results() As Object = Me.Invoke("GetVersionNumber_Str", New Object(-1) {})
            Return CType(results(0),String)
        End Function
        
        '<remarks/>
        Public Function BeginGetVersionNumber_Str(ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("GetVersionNumber_Str", New Object(-1) {}, callback, asyncState)
        End Function
        
        '<remarks/>
        Public Function EndGetVersionNumber_Str(ByVal asyncResult As System.IAsyncResult) As String
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),String)
        End Function
        
        '<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections/GetVersionNumber", RequestNamespace:="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", ResponseNamespace:="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function GetVersionNumber() As String
            Dim results() As Object = Me.Invoke("GetVersionNumber", New Object(-1) {})
            Return CType(results(0),String)
        End Function
        
        '<remarks/>
        Public Function BeginGetVersionNumber(ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("GetVersionNumber", New Object(-1) {}, callback, asyncState)
        End Function
        
        '<remarks/>
        Public Function EndGetVersionNumber(ByVal asyncResult As System.IAsyncResult) As String
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),String)
        End Function
        
        '<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections/Ping", RequestNamespace:="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", ResponseNamespace:="http://tempuri.org/YSI.Interfaces.WebServices/ItfCollections", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function Ping() As String
            Dim results() As Object = Me.Invoke("Ping", New Object(-1) {})
            Return CType(results(0),String)
        End Function
        
        '<remarks/>
        Public Function BeginPing(ByVal callback As System.AsyncCallback, ByVal asyncState As Object) As System.IAsyncResult
            Return Me.BeginInvoke("Ping", New Object(-1) {}, callback, asyncState)
        End Function
        
        '<remarks/>
        Public Function EndPing(ByVal asyncResult As System.IAsyncResult) As String
            Dim results() As Object = Me.EndInvoke(asyncResult)
            Return CType(results(0),String)
        End Function
    End Class
End Namespace
