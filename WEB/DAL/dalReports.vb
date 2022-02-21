Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Collections.Specialized
Imports System.Configuration

Namespace TrackITKTS

    Public Class dalReports
        Private ConnectionString As String

        Public Shared Function REPCustomerSOA(ByVal valCompanyID As Int32, ByVal valFromDate As DateTime, ByVal valToDate As DateTime, ByVal valCustCode As String, valItemIDs As String, ByVal SchemaName As String) As DataSet
            Dim db As DBAccess = New DBAccess
            db.Parameters.Add(New SqlParameter("@CompanyID", valCompanyID))
            db.Parameters.Add(New SqlParameter("@FromDate", valFromDate))
            db.Parameters.Add(New SqlParameter("@ToDate", valToDate))
            db.Parameters.Add(New SqlParameter("@MultipleCustomerIDs", valCustCode))
            db.Parameters.Add(New SqlParameter("@MultipleItemIDs", valItemIDs))
            Dim ds As DataSet = db.ExecuteDataSet(SchemaName + ".REPCustomerSOA")
            Return ds
        End Function
        Public Shared Function REPCustomerSummary(ByVal valChannelName As String, ByVal valFromDate As DateTime, ByVal valToDate As DateTime, valItemIDs As String, ByVal SchemaName As String) As DataSet
            Dim db As DBAccess = New DBAccess
            db.Parameters.Add(New SqlParameter("@ChannelName", valChannelName))
            db.Parameters.Add(New SqlParameter("@FromDate", valFromDate))
            db.Parameters.Add(New SqlParameter("@ToDate", valToDate))
            db.Parameters.Add(New SqlParameter("@MultipleItemIDs", valItemIDs))
            Dim ds As DataSet = db.ExecuteDataSet(SchemaName + ".REPCustomerSummary")
            Return ds
        End Function
        Public Shared Function REPCustomerReconciliation(ByVal valCompanyID As Int32, ByVal valFromDate As DateTime, ByVal valToDate As DateTime, ByVal valCustCode As String, valItemIDs As String, ByVal SchemaName As String) As DataSet
            Dim db As DBAccess = New DBAccess
            db.Parameters.Add(New SqlParameter("@CompanyID", valCompanyID))
            db.Parameters.Add(New SqlParameter("@FromDate", valFromDate))
            db.Parameters.Add(New SqlParameter("@ToDate", valToDate))
            db.Parameters.Add(New SqlParameter("@MultipleCustomerIDs", valCustCode))
            db.Parameters.Add(New SqlParameter("@MultipleItemIDs", valItemIDs))
            Dim ds As DataSet = db.ExecuteDataSet(SchemaName + ".REPCustomerReconciliation")
            Return ds
        End Function
        Public Shared Function REPDiscrepancyTimeDelay(ByVal valCompanyID As Int32, ByVal valCustCode As String, valItemIDs As String, ByVal SchemaName As String) As DataSet
            Dim db As DBAccess = New DBAccess
            db.Parameters.Add(New SqlParameter("@CompanyID", valCompanyID))
            db.Parameters.Add(New SqlParameter("@CustomerID", valCustCode))
            db.Parameters.Add(New SqlParameter("@MultipleItemIDs", valItemIDs))
            Dim ds As DataSet = db.ExecuteDataSet(SchemaName + ".REPDiscrepancyTimeDelay")
            Return ds
        End Function
        Public Shared Function REPTotalKegEmpties(ByVal valCompanyID As Int32, valItemIDs As String, ByVal SchemaName As String) As DataSet
            Dim db As DBAccess = New DBAccess
            db.Parameters.Add(New SqlParameter("@CompanyID", valCompanyID))
            db.Parameters.Add(New SqlParameter("@MultipleItemIDs", valItemIDs))
            Dim ds As DataSet = db.ExecuteDataSet(SchemaName + ".REPEmptiesinyard")
            Return ds
        End Function
        Public Shared Function REPTotalKegPopulation(ByVal valCompanyID As Int32, valItemIDs As String, ByVal SchemaName As String) As DataSet
            Dim db As DBAccess = New DBAccess
            db.Parameters.Add(New SqlParameter("@CompanyID", valCompanyID))
            db.Parameters.Add(New SqlParameter("@MultipleItemIDs", valItemIDs))
            Dim ds As DataSet = db.ExecuteDataSet(SchemaName + ".REPTotalKegPopulation")
            Return ds
        End Function
        Public Shared Function REPSupplierSOA(ByVal valCompanyID As Int32, ByVal valFromDate As DateTime, ByVal valToDate As DateTime, ByVal valSuppCode As String, valItemIDs As String, ByVal SchemaName As String) As DataSet
            Dim db As DBAccess = New DBAccess
            db.Parameters.Add(New SqlParameter("@CompanyID", valCompanyID))
            db.Parameters.Add(New SqlParameter("@FromDate", valFromDate))
            db.Parameters.Add(New SqlParameter("@ToDate", valToDate))
            db.Parameters.Add(New SqlParameter("@MultipleSupplierIDs", valSuppCode))
            db.Parameters.Add(New SqlParameter("@MultipleItemIDs", valItemIDs))
            Dim ds As DataSet = db.ExecuteDataSet(SchemaName + ".REPSupplierSOA")
            Return ds
        End Function
        Public Shared Function REPSupplierReconciliation(ByVal valCompanyID As Int32, ByVal valFromDate As DateTime, ByVal valToDate As DateTime, ByVal valSuppCode As String, valItemIDs As String, ByVal SchemaName As String) As DataSet
            Dim db As DBAccess = New DBAccess
            db.Parameters.Add(New SqlParameter("@CompanyID", valCompanyID))
            db.Parameters.Add(New SqlParameter("@FromDate", valFromDate))
            db.Parameters.Add(New SqlParameter("@ToDate", valToDate))
            db.Parameters.Add(New SqlParameter("@MultipleSupplierIDs", valSuppCode))
            db.Parameters.Add(New SqlParameter("@MultipleItemIDs", valItemIDs))
            Dim ds As DataSet = db.ExecuteDataSet(SchemaName + ".REPSupplierReconciliation")
            Return ds
        End Function
        Public Shared Function REPSupplierSummary(ByVal valCompanyID As Int32, ByVal valFromDate As DateTime, ByVal valToDate As DateTime, valItemIDs As String, ByVal SchemaName As String) As DataSet
            Dim db As DBAccess = New DBAccess
            db.Parameters.Add(New SqlParameter("@CompanyID", valCompanyID))
            db.Parameters.Add(New SqlParameter("@FromDate", valFromDate))
            db.Parameters.Add(New SqlParameter("@ToDate", valToDate))
            db.Parameters.Add(New SqlParameter("@MultipleItemIDs", valItemIDs))
            Dim ds As DataSet = db.ExecuteDataSet(SchemaName + ".REPSupplierSummary")
            Return ds
        End Function
        Public Shared Function REPDiscrepancy(ByVal valChannelName As String, ByVal valFromDate As DateTime, ByVal valToDate As DateTime, valItemIDs As String, ByVal SchemaName As String) As DataSet
            Dim db As DBAccess = New DBAccess
            db.Parameters.Add(New SqlParameter("@ChannelName", valChannelName))
            db.Parameters.Add(New SqlParameter("@FromDate", valFromDate))
            db.Parameters.Add(New SqlParameter("@ToDate", valToDate))
            db.Parameters.Add(New SqlParameter("@MultipleItemIDs", valItemIDs))
            Dim ds As DataSet = db.ExecuteDataSet(SchemaName + ".REPDiscrepancy")
            Return ds
        End Function
        Public Shared Function REPDiscrepancy1(ByVal ChannelName As String, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal valCustCode As String, strItemIDs As String, ByVal SchemaName As String) As DataSet
            Dim db As DBAccess = New DBAccess
            db.Parameters.Add(New SqlParameter("@ChannelName", ChannelName))
            db.Parameters.Add(New SqlParameter("@FromDate", FromDate))
            db.Parameters.Add(New SqlParameter("@ToDate", ToDate))
            db.Parameters.Add(New SqlParameter("@MultipleCustomerIDs", valCustCode))
            db.Parameters.Add(New SqlParameter("@MultipleItemIDs", strItemIDs))
            Dim ds As DataSet = db.ExecuteDataSet(SchemaName + ".REPDiscrepancy1")
            Return ds
        End Function

        Public Shared Function REPBPReconciliation(ByVal valCompanyID As Int32, ByVal valBPCode As String, ByVal SchemaName As String) As DataSet
            Dim db As DBAccess = New DBAccess
            db.Parameters.Add(New SqlParameter("@CompanyID", valCompanyID))
            db.Parameters.Add(New SqlParameter("@BPCode", valBPCode))
            Dim ds As DataSet = db.ExecuteDataSet(SchemaName + ".REPBPReconciliation")
            Return ds
        End Function

        Public Shared Function REPCustomerEmptyKegCollection(ByVal valFromDate As DateTime, ByVal valToDate As DateTime, ByVal UserID As Int32, valItemIDs As String, ByVal SchemaName As String) As DataSet
            Dim db As DBAccess = New DBAccess
            db.Parameters.Add(New SqlParameter("@FromDate", valFromDate))
            db.Parameters.Add(New SqlParameter("@ToDate", valToDate))
            db.Parameters.Add(New SqlParameter("@UserID", UserID))
            db.Parameters.Add(New SqlParameter("@MultipleItemCodes", valItemIDs))
            Dim ds As DataSet = db.ExecuteDataSet(SchemaName + ".REPCustomerKegReturnHHT")
            Return ds
        End Function

        Public Shared Function SubmitRequest(ByVal RequestName As String, ByVal Param1 As String, ByVal Param2 As String, ByVal Param3 As String, ByVal Param4 As String,
                                             ByVal Param5 As String, ByVal Param6 As String, ByVal Param7 As String, ByVal RequestedBy As Int32, ByVal IsMultiFileDownload As Boolean, ByVal SchemaName As String) As Integer

            Dim db As DBAccess = New DBAccess
            Dim sqlCmd As SqlCommand = New SqlCommand()
            db.AddParamToSQLCmd(sqlCmd, "@ReturnValue", SqlDbType.Int, 0, ParameterDirection.ReturnValue, Nothing)
            db.AddParamToSQLCmd(sqlCmd, "@RequestName", SqlDbType.NVarChar, 100, ParameterDirection.Input, RequestName)
            db.AddParamToSQLCmd(sqlCmd, "@CompanyID", SqlDbType.Int, 0, ParameterDirection.Input, Param1)
            db.AddParamToSQLCmd(sqlCmd, "@Param1", SqlDbType.NVarChar, 4000, ParameterDirection.Input, Param1)
            db.AddParamToSQLCmd(sqlCmd, "@Param2", SqlDbType.NVarChar, 4000, ParameterDirection.Input, Param2)
            db.AddParamToSQLCmd(sqlCmd, "@Param3", SqlDbType.NVarChar, 4000, ParameterDirection.Input, Param3)
            db.AddParamToSQLCmd(sqlCmd, "@Param4", SqlDbType.NVarChar, 4000, ParameterDirection.Input, Param4)
            db.AddParamToSQLCmd(sqlCmd, "@Param5", SqlDbType.NVarChar, 4000, ParameterDirection.Input, Param5)
            db.AddParamToSQLCmd(sqlCmd, "@Param6", SqlDbType.NVarChar, 4000, ParameterDirection.Input, Param6)
            db.AddParamToSQLCmd(sqlCmd, "@Param7", SqlDbType.NVarChar, 4000, ParameterDirection.Input, Param7)
            db.AddParamToSQLCmd(sqlCmd, "@RequestedBy", SqlDbType.Int, 0, ParameterDirection.Input, RequestedBy)
            db.AddParamToSQLCmd(sqlCmd, "@IsMultiFileDownload", SqlDbType.Bit, 0, ParameterDirection.Input, IsMultiFileDownload)

            db.SetCommandType(sqlCmd, CommandType.StoredProcedure, SchemaName + ".TIT_SubmitProcessRequest")
            db.ExecuteScalarCmd(sqlCmd)
            Return CInt(sqlCmd.Parameters("@ReturnValue").Value)
        End Function

        Public Shared Function GetTabReportRequestsByCompanyID(ByVal valCompanyID As Int32, ByVal valEmployeeID As Int32, ByVal SchemaName As String) As DataSet
            Dim db As DBAccess = New DBAccess
            db.Parameters.Add(New SqlParameter("@CompanyID", valCompanyID))
            db.Parameters.Add(New SqlParameter("@EmployeeID", valEmployeeID))
            Dim ds As DataSet = db.ExecuteDataSet(SchemaName + ".TIT_GetAllTabReportRequestsByCompanyID")
            Return ds
        End Function

        Public Shared Function GetAllTabMRequestsByCompanyID(ByVal valCompanyID As Int32, ByVal SchemaName As String) As DataSet
            Dim db As DBAccess = New DBAccess
            db.Parameters.Add(New SqlParameter("@CompanyID", valCompanyID))
            Dim ds As DataSet = db.ExecuteDataSet(SchemaName + ".TIT_GetAllTabMRequestsByCompanyID")
            Return ds
        End Function
    End Class
End Namespace
