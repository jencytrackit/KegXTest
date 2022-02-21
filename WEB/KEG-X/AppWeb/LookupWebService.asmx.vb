Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

Imports BLL.TrackITKTS
Imports Telerik.Web.UI

Namespace TrackITKTS
    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    <System.Web.Script.Services.ScriptService()> _
    <System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class LookupWebService
        Inherits System.Web.Services.WebService

        <WebMethod()> _
        Public Function HelloWorld(ByVal prefixText As String, ByVal count As Integer) As String
            Return "Hello World"
        End Function

        <System.Web.Script.Services.ScriptMethod(), _
    System.Web.Services.WebMethod()> _
        Public Function ItemsLookup(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

            Dim SearchList As List(Of String) = New List(Of String)
            Dim CIDSchema As String()
            Try
                'objuserrole = 
                If contextKey = "" Then
                    SearchList.Add("Please select the Company")
                    Return SearchList
                End If
            Catch ex As Exception
                SearchList.Add("Please select the Company")
                Return SearchList
            End Try

            SearchList.Clear()
            CIDSchema = contextKey.Split(",") '0 for CompanyID, 1 for Schema

            Dim SearchItems As DataSet
            SearchItems = clsValidations.GetLookupItems(prefixText, CIDSchema(0), CIDSchema(1), count)
            Dim cnt As Integer = 1

            For Each row As DataRow In SearchItems.Tables(0).Rows
                If cnt > count Then Exit For
                SearchList.Add(row("ItemName").ToString)
                cnt = cnt + 1
            Next

            Return SearchList
        End Function

        <System.Web.Script.Services.ScriptMethod(), _
    System.Web.Services.WebMethod()> _
        Public Function CustomerLookup(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

            Dim SearchList As List(Of String) = New List(Of String)
            Dim CIDSchema As String()
            Try
                'objuserrole = 
                If contextKey = "" Then
                    SearchList.Add("Please select the Company")
                    Return SearchList
                End If
            Catch ex As Exception
                SearchList.Add("Please select the Company")
                Return SearchList
            End Try

            SearchList.Clear()
            CIDSchema = contextKey.Split(",") '0 for CompanyID, 1 for Schema

            Dim SearchItems As DataSet
            SearchItems = clsValidations.GetCustomerLookup(prefixText, CIDSchema(0), CIDSchema(1), count)
            Dim cnt As Integer = 1

            For Each row As DataRow In SearchItems.Tables(0).Rows
                If cnt > count Then Exit For
                SearchList.Add(row("CustomerName").ToString)
                cnt = cnt + 1
            Next

            Return SearchList
        End Function

        <System.Web.Script.Services.ScriptMethod(), _
    System.Web.Services.WebMethod()> _
        Public Function SupplierLookup(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

            Dim SearchList As List(Of String) = New List(Of String)
            Dim CIDSchema As String()
            Try
                'objuserrole = 
                If contextKey = "" Then
                    SearchList.Add("Please select the Company")
                    Return SearchList
                End If
            Catch ex As Exception
                SearchList.Add("Please select the Company")
                Return SearchList
            End Try

            SearchList.Clear()
            CIDSchema = contextKey.Split(",") '0 for CompanyID, 1 for Schema

            Dim SearchItems As DataSet
            SearchItems = clsValidations.GetSupplierLookup(prefixText, CIDSchema(0), CIDSchema(1), count)
            Dim cnt As Integer = 1

            For Each row As DataRow In SearchItems.Tables(0).Rows
                If cnt > count Then Exit For
                SearchList.Add(row("SupplierName").ToString)
                cnt = cnt + 1
            Next

            Return SearchList
        End Function

        <System.Web.Script.Services.ScriptMethod(), _
   System.Web.Services.WebMethod()> _
        Public Function BranchPlantLookup(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

            Dim SearchList As List(Of String) = New List(Of String)
            Dim CIDSchema As String()
            Try
                'objuserrole = 
                If contextKey = "" Then
                    SearchList.Add("Please select the Company")
                    Return SearchList
                End If
            Catch ex As Exception
                SearchList.Add("Please select the Company")
                Return SearchList
            End Try

            SearchList.Clear()
            CIDSchema = contextKey.Split(",") '0 for CompanyID, 1 for Schema

            Dim SearchItems As DataSet
            SearchItems = clsValidations.GetBPLookup(prefixText, CIDSchema(0), CIDSchema(1), count)
            Dim cnt As Integer = 1

            For Each row As DataRow In SearchItems.Tables(0).Rows
                If cnt > count Then Exit For
                SearchList.Add(row("BranchName").ToString)
                cnt = cnt + 1
            Next

            Return SearchList
        End Function

        <System.Web.Script.Services.ScriptMethod(), _
    System.Web.Services.WebMethod()> _
        Public Function BranchPlantLookupbyEmpID(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

            Dim SearchList As List(Of String) = New List(Of String)
            Dim CIDSchema As String()
            
            SearchList.Clear()
            CIDSchema = contextKey.Split(",") '0 for EmployeeID, 1 for Schema

            Dim SearchItems As DataSet
            SearchItems = clsValidations.GetBPLookupbyEmpID(prefixText, CIDSchema(0), CIDSchema(1), count)
            Dim cnt As Integer = 1

            For Each row As DataRow In SearchItems.Tables(0).Rows
                If cnt > count Then Exit For
                SearchList.Add(row("BranchName").ToString)
                cnt = cnt + 1
            Next

            Return SearchList
        End Function

        <System.Web.Script.Services.ScriptMethod(), _
  System.Web.Services.WebMethod()> _
        Public Function ItemsLookupByEmpID(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

            Dim SearchList As List(Of String) = New List(Of String)
            Dim CIDSchema As String()
            Try
                'objuserrole =
                If contextKey = "" Then
                    SearchList.Add("No employee details found")
                    Return SearchList
                End If
            Catch ex As Exception
                SearchList.Add("No employee details found")
                Return SearchList
            End Try

            SearchList.Clear()
            CIDSchema = contextKey.Split(",") '0 for CompanyID, 1 for Schema

            Dim SearchItems As DataSet
            SearchItems = clsValidations.GetLookupItemsByEmpID(prefixText, CIDSchema(0), CIDSchema(1), count)
            Dim cnt As Integer = 1

            For Each row As DataRow In SearchItems.Tables(0).Rows
                If cnt > count Then Exit For
                SearchList.Add(row("ItemName").ToString)
                cnt = cnt + 1
            Next

            Return SearchList
        End Function

        <System.Web.Script.Services.ScriptMethod(), _
System.Web.Services.WebMethod()> _
        Public Function CustomerLookupByEmpID(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

            Dim SearchList As List(Of String) = New List(Of String)
            Dim CIDSchema As String()
            Try
                'objuserrole = 
                If contextKey = "" Then
                    SearchList.Add("No Employee details ")
                    Return SearchList
                End If
            Catch ex As Exception
                SearchList.Add("No Employee details")
                Return SearchList
            End Try

            SearchList.Clear()
            CIDSchema = contextKey.Split(",") '0 for CompanyID, 1 for Schema

            Dim SearchItems As DataSet
            SearchItems = clsValidations.GetCustomerLookupByEmpID(prefixText, CInt(CIDSchema(0)), CIDSchema(1), count)
            Dim cnt As Integer = 1

            For Each row As DataRow In SearchItems.Tables(0).Rows
                If cnt > count Then Exit For
                SearchList.Add(row("CustomerName").ToString)
                cnt = cnt + 1
            Next

            Return SearchList
        End Function

        <System.Web.Script.Services.ScriptMethod(), _
     System.Web.Services.WebMethod()> _
        Public Function SupplierLookupEmp(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As List(Of String)

            Dim SearchList As List(Of String) = New List(Of String)
            Dim CIDSchema As String()
            Try
                'objuserrole = 
                If contextKey = "" Then
                    SearchList.Add("No Employee Details found")
                    Return SearchList
                End If
            Catch ex As Exception
                SearchList.Add("No Employee Details found")
                Return SearchList
            End Try

            SearchList.Clear()
            CIDSchema = contextKey.Split(",") '0 for CompanyID, 1 for Schema

            Dim SearchItems As DataSet
            SearchItems = clsValidations.GetSupplierLookupByEmpID(prefixText, CIDSchema(0), CIDSchema(1), count)
            Dim cnt As Integer = 1

            For Each row As DataRow In SearchItems.Tables(0).Rows
                If cnt > count Then Exit For
                SearchList.Add(row("SupplierName").ToString)
                cnt = cnt + 1
            Next

            Return SearchList
        End Function

    End Class

End Namespace