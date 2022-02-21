Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports DAL.TrackITKTS
Namespace TrackITKTS
    Public Class clsReports
        Public Shared Function REPCustomerSOA(ByVal CompanyID As Int32, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal CustCode As String, strItemIDs As String, ByVal SchemaName As String) As DataSet
            If CompanyID <= 0 Then
                Return (Nothing)
            End If

            Return dalReports.REPCustomerSOA(CompanyID, FromDate, ToDate, CustCode, strItemIDs, SchemaName)
        End Function
        Public Shared Function REPCustomerSummary(ByVal ChannelName As String, ByVal FromDate As DateTime, ByVal ToDate As DateTime, strItemIDs As String, ByVal SchemaName As String) As DataSet
            'If CompanyID <= 0 Then
            '    Return (Nothing)
            'End If

            Return dalReports.REPCustomerSummary(ChannelName, FromDate, ToDate, strItemIDs, SchemaName)
        End Function
        Public Shared Function REPCustomerReconciliation(ByVal CompanyID As Int32, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal CustCode As String, strItemID As String, ByVal SchemaName As String) As DataSet
            If CompanyID <= 0 Then
                Return (Nothing)
            End If

            Return dalReports.REPCustomerReconciliation(CompanyID, FromDate, ToDate, CustCode, strItemID, SchemaName)
        End Function
        Public Shared Function REPDiscrepancyTimeDelay(ByVal CompanyID As Int32, ByVal CustCode As String, strItemID As String, ByVal SchemaName As String) As DataSet
            If CompanyID <= 0 Then
                Return (Nothing)
            End If

            Return dalReports.REPDiscrepancyTimeDelay(CompanyID, CustCode, strItemID, SchemaName)
        End Function
        Public Shared Function REPTotalKegEmpties(ByVal CompanyID As Int32, strItemID As String, ByVal SchemaName As String) As DataSet
            If CompanyID <= 0 Then
                Return (Nothing)
            End If

            Return dalReports.REPTotalKegEmpties(CompanyID, strItemID, SchemaName)
        End Function
        Public Shared Function REPTotalKegPopulation(ByVal CompanyID As Int32, strItemID As String, ByVal SchemaName As String) As DataSet
            If CompanyID <= 0 Then
                Return (Nothing)
            End If

            Return dalReports.REPTotalKegPopulation(CompanyID, strItemID, SchemaName)
        End Function
        Public Shared Function REPSupplierSOA(ByVal CompanyID As Int32, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal SuppCode As String, strItemIDs As String, ByVal SchemaName As String) As DataSet
            If CompanyID <= 0 Then
                Return (Nothing)
            End If

            Return dalReports.REPSupplierSOA(CompanyID, FromDate, ToDate, SuppCode, strItemIDs, SchemaName)
        End Function
        Public Shared Function REPSupplierReconciliation(ByVal CompanyID As Int32, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal SuppCode As String, strItemIDs As String, ByVal SchemaName As String) As DataSet
            If CompanyID <= 0 Then
                Return (Nothing)
            End If

            Return dalReports.REPSupplierReconciliation(CompanyID, FromDate, ToDate, SuppCode, strItemIDs, SchemaName)
        End Function
        Public Shared Function REPSupplierSummary(ByVal CompanyID As Int32, ByVal FromDate As DateTime, ByVal ToDate As DateTime, strItemIDs As String, ByVal SchemaName As String) As DataSet
            If CompanyID <= 0 Then
                Return (Nothing)
            End If

            Return dalReports.REPSupplierSummary(CompanyID, FromDate, ToDate, strItemIDs, SchemaName)
        End Function
        Public Shared Function REPDiscrepancy(ByVal ChannelName As String, ByVal FromDate As DateTime, ByVal ToDate As DateTime, strItemIDs As String, ByVal SchemaName As String) As DataSet
            'If CompanyID <= 0 Then
            '    Return (Nothing)
            'End If

            Return dalReports.REPDiscrepancy(ChannelName, FromDate, ToDate, strItemIDs, SchemaName)
        End Function
        Public Shared Function REPDiscrepancy1(ByVal ChannelName As String, ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal CustCode As String, strItemIDs As String, ByVal SchemaName As String) As DataSet
            'If CompanyID <= 0 Then
            '    Return (Nothing)
            'End If

            Return dalReports.REPDiscrepancy1(ChannelName, FromDate, ToDate, CustCode, strItemIDs, SchemaName)
        End Function
        Public Shared Function REPBPReconciliation(ByVal CompanyID As Int32, ByVal BPCode As String, ByVal SchemaName As String) As DataSet
            If CompanyID <= 0 Then
                Return (Nothing)
            End If

            Return dalReports.REPBPReconciliation(CompanyID, BPCode, SchemaName)
        End Function
        Public Shared Function REPCustomerEmptyKegCollection(ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal UserID As Int32, strItemID As String, ByVal SchemaName As String) As DataSet
            Return dalReports.REPCustomerEmptyKegCollection(FromDate, ToDate, UserID, strItemID, SchemaName)
        End Function
        Enum ReportZoom
            ZoomDefaultSize = 75
        End Enum
        Public Shared Function SubmitRequest(ByVal RequestName As String, ByVal Param1 As String, ByVal Param2 As String, ByVal Param3 As String, ByVal Param4 As String,
                                             ByVal Param5 As String, ByVal Param6 As String, ByVal Param7 As String, ByVal RequestedBy As Int32, ByVal IsMultiFileDownload As Boolean, ByVal SchemaName As String) As Integer
            If RequestName <> "" Then
                Dim TempId As Integer = dalReports.SubmitRequest(RequestName, Param1, Param2, Param3, Param4, Param5, Param6, Param7, RequestedBy, IsMultiFileDownload, SchemaName)
                If TempId > 0 Then
                    Return TempId
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        End Function

        Public Shared Function GetTabReportRequestsByCompanyID(ByVal CompanyID As Int32, ByVal EmployeeID As Int32, ByVal SchemaName As String) As DataSet
            Return dalReports.GetTabReportRequestsByCompanyID(CompanyID, EmployeeID, SchemaName)
        End Function
        Public Shared Function GetAllTabMRequestsByCompanyID(ByVal CompanyID As Int32, ByVal SchemaName As String) As DataSet
            Return dalReports.GetAllTabMRequestsByCompanyID(CompanyID, SchemaName)
        End Function
    End Class

End Namespace
