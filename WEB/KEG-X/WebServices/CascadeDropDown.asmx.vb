Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports AjaxControlToolkit
Imports System.Data
Imports System.Data.SqlClient
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Imports System.Collections.Generic
Namespace TrackITKTS
    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    ' <System.Web.Script.Services.ScriptService()> _
    <System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class CascadeDropDown
        Inherits System.Web.Services.WebService
      
        <WebMethod()> _
        Public Function HelloWorld() As String
            Return "Hello World"
        End Function

        <WebMethod(EnableSession:=True)> _
        Public Function GetCustomer( _
          ByVal knownCategoryValues As String, _
          ByVal category As String) As  _
          CascadingDropDownNameValue()


            Dim kv As StringDictionary = _
                CascadingDropDown. _
                ParseKnownCategoryValuesString( _
                knownCategoryValues)

          
            Dim values As New  _
              System.Collections.Generic.List( _
              Of AjaxControlToolkit.CascadingDropDownNameValue)

            Dim objuserInfo As New Hashtable()
            Dim objuserrole As New Hashtable()
            objuserInfo = Session("UserDetails")
            objuserrole = Session("UserRoleDetails")

            Dim ds As DataSet
            ds = clsTabMCustomers.GetRptCustomersDrop(kv("Company Names"), objuserrole("employeeID"), objuserInfo("schemaName"))

            For Each row As DataRow In ds.Tables(0).Rows
                values.Add(New CascadingDropDownNameValue( _
                   row("CustomerName"), row("CustomerID")))
            Next

            Return values.ToArray

        End Function
    End Class
End Namespace