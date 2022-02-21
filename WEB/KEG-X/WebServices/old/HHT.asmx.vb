'***************************************************************
'Application        :Kegs Tracking System
'File Name          :Web service file
'Created By         :
'File navigation    :
'Created Date       :
'Description        :
'Modified By        :
'Modified Date      :
'***************************************************************
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data.SqlServerCe
Imports System.Collections
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Core
Imports System.Data.SqlClient
Imports [Property].TrackITKTS
Imports BLL.TrackITKTS
Imports DAL.TrackITKTS
Namespace TrackITKTS
    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    ' <System.Web.Script.Services.ScriptService()> _
    <System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
    <System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
    <ToolboxItem(False)> _
    Public Class HHT
        Inherits System.Web.Services.WebService
        Dim cln_con As SqlCeConnection
        Dim cln_com As SqlCeCommand
        Dim cln_dap As SqlCeDataAdapter

        Dim Query As String = ""

        <WebMethod()> _
        Public Function Get_SyncData(ByVal LoginName As String, ByVal Password As String) As Byte()
           Dim ds As New DataSet
            Dim str As String = ""
            Dim SDF_Dest As String
            Try
                Dim SDFGid As Guid = Guid.NewGuid()
                SDF_Dest = Build_Data(SDFGid.ToString, LoginName, Password)
                CreateZip(SDF_Dest)
                Dim Get_fs As New System.IO.FileStream(SDF_Dest & ".zip", IO.FileMode.Open)
                Dim by(Get_fs.Length - 1) As Byte
                Get_fs.Read(by, 0, by.Length)
                Get_fs.Close()
                File.Delete(SDF_Dest)
                Return by
                'Dim fs As New System.IO.FileStream("C:\TrackService\Track_AssetHHTDB.sdf", FileMode.Create)
                'fs.Write(by, 0, by.Length)
                'fs.Close()
            Catch ex As Exception
                Log_Error("Sync_Service{Get_SyncData}:" & ex.Message & " - " & DateTime.Now.ToShortDateString)
            End Try
        End Function
        Private Sub CreateZip(ByVal zipFileName As String)
            Try
                Dim targetName As String = zipFileName & ".zip"

                'Dim astrFileNames() As String = Directory.GetFiles(SDF_File_Dest)
                Dim strmZipOutputStream As ZipOutputStream

                strmZipOutputStream = New ZipOutputStream(File.Create(targetName))

                strmZipOutputStream.SetLevel(9)

                Dim strmFile As FileStream = File.OpenRead(zipFileName)
                Dim abyBuffer(strmFile.Length - 1) As Byte

                strmFile.Read(abyBuffer, 0, abyBuffer.Length)
                Dim objZipEntry As ZipEntry = New ZipEntry(zipFileName)

                objZipEntry.DateTime = DateTime.Now
                objZipEntry.Size = strmFile.Length
                strmFile.Close()
                strmZipOutputStream.PutNextEntry(objZipEntry)
                strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)

                strmZipOutputStream.Finish()
                strmZipOutputStream.Close()
            Catch ex As Exception
                Log_Error("Data_Sync{CreateZip}:" & ex.Message & " - " & DateTime.Now.ToShortDateString)
            End Try

        End Sub

        Public Function Build_Data(ByVal SDFGid As String, ByVal LoginName As String, ByVal Password As String) As String

            Dim Path As String
            Dim SDF_File_Dest As String
            Dim SDF_File_Source As String
            Dim userroledetails As New DataSet()
            Path = Server.MapPath("../")
            Dim pos As Int32 = Path.LastIndexOf("\")
            Path = Path.Substring(0, pos)
            pos = Path.LastIndexOf("\")
            Path = Path.Substring(0, pos)

            SDF_File_Dest = Path & "\HHTDB_Dest\"
            SDF_File_Dest = SDF_File_Dest & "Track_KegsHHTDB" & "-" & SDFGid & ".sdf"
            SDF_File_Dest.Replace("\", "/")

            Dim Query As String = ""
            Try
                SDF_File_Source = Path & "\HHTDB_Source\KegsTracking.sdf"
                'MsgBox(SDF_File_Source)
                SDF_File_Source.Replace("\", "/")
                File.Copy(SDF_File_Source, SDF_File_Dest, True)

                AppDomain.CurrentDomain.SetData("SQLServerCompactEditionUnderWebHosting", True)
                cln_con = New SqlCeConnection("Data Source=" & SDF_File_Dest)
                cln_con.Open()

                'Get User Details
                Dim objUser As New PropertyTabMEmployees
                Dim objUserDetails As New DataSet()
                Dim strPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(Trim(Password), "SHA1")
                objUser.UserName = LoginName
                objUser.Password = strPassword
                objUserDetails = clsTabMEmployees.ChkUserValidityHHT(objUser)

                If Not objUserDetails Is Nothing Then
                    Dim SchemaName As String = objUserDetails.Tables(0).Rows(0).Item("schemaName").ToString
                    userroledetails = clsTabMEmployees.GetEmployeeCompanyAndRolesHHT(objUser, SchemaName)
                    If userroledetails.Tables(0).Rows.Count > 0 Then

                        Dim EmpID As Integer = Convert.ToInt32(userroledetails.Tables(0).Rows(0).Item("EmployeeID"))
                        DownloadEmployees(objUserDetails, userroledetails, objUser)
                        DownLoadEmpComp(EmpID, SchemaName)
                        ' DownLoadOrganizations(objUserDetails("empID"))
                        ' DownLoadUserPrivilages(EmpID, SchemaName)
                        DownLoadBranchPlants(EmpID, SchemaName)
                        DownLoadCustomers(EmpID, SchemaName)
                        'DownLoadSuppliers(objUserDetails("empID"))
                        DownLoadItems(EmpID, SchemaName)
                        ' DownLoadUOMMaster(objUserDetails("empID"))
                        DownLoadSaleOrder(EmpID, SchemaName)
                        DownLoadSaleOrderBarcode(EmpID, SchemaName)
                        ' DownloadEmptyInventory(EmpID, SchemaName)
                    End If

                End If

                cln_con.Close()
                cln_con.Dispose()
                GC.Collect()
            Catch ex As Exception
                'Log an error in text file if exception occurs
                Log_Error("Data_Sync{Build_Data}in Main:" & ex.Message & " - " & DateTime.Now.ToShortDateString & " - " & Query)
                cln_con.Close()
                cln_con.Dispose()
                GC.Collect()
            End Try
            Return SDF_File_Dest
        End Function
        Public Sub Log_Error(ByVal Msg As String)
            Dim Path As String
            Try
                Path = Server.MapPath("../")

                Dim pos As Int32 = Path.LastIndexOf("\")
                Path = Path.Substring(0, pos)

                pos = Path.LastIndexOf("\")
                Path = Path.Substring(0, pos)
                Path = Path & "\TK_Web_Log.txt"
                Path.Replace("\", "/")
                Dim Strm As New StreamWriter(Path, True)
                Strm.WriteLine(Msg)
                Strm.Flush()
                Strm.Close()
            Catch ex As Exception
            End Try
        End Sub

        Private Sub DownloadEmployees(ByVal objuserdetails As DataSet, ByVal objuserroledetails As DataSet, ByVal objATSUsers As PropertyTabMEmployees)

            Try
                Query = "insert into TABEmployee(EmployeeID,UserName,Password,EmployeeName, IsOrganisation,SchemaName,Disable) values(" & objuserroledetails.Tables(0).Rows(0).Item("EmployeeID") & ",N'" & objATSUsers.UserName & "','" & objATSUsers.Password & "',N'" & objuserdetails.Tables(0).Rows(0).Item("EmployeeName") & "',N'" & objuserdetails.Tables(0).Rows(0).Item("IsOrganisation") & "','" & objuserdetails.Tables(0).Rows(0).Item("SchemaName") & "','" & objuserdetails.Tables(0).Rows(0).Item("disabled") & "')"
                cln_com = New SqlCeCommand(Query, cln_con)
                cln_com.ExecuteNonQuery()
            Catch ex As Exception
                Log_Error("Data_Sync{PutData_Client}in Download TABEmployee:" & ex.Message & " - " & Query)
            End Try
        End Sub

        Private Sub DownLoadEmpComp(ByVal EmpID As Integer, ByVal schemaname As String)
            'Download TABCompEmp 
            Dim dsCompEmp = clsTabMCompEmp.GetTabMCompEmpByEmployeeID(EmpID, schemaname)
            If Not dsCompEmp Is Nothing And dsCompEmp.Tables(0).Rows.Count > 0 Then
                Dim i As Int32
                For i = 0 To dsCompEmp.Tables(0).Rows.Count - 1
                    Try
                        Query = "insert into TABCompEmp(EmployeeID,CompanyID,CompanyCode) values(N'" & dsCompEmp.Tables(0).Rows(i)("EmployeeID") & "',N'" & dsCompEmp.Tables(0).Rows(i)("CompanyID") & "',N'" & dsCompEmp.Tables(0).Rows(i)("CompanyCode") & "')"
                        cln_com = New SqlCeCommand(Query, cln_con)
                        cln_com.ExecuteNonQuery()
                    Catch ex As Exception
                        Log_Error("Data_Sync{PutData_Client}in Download TABCompEmp:" & ex.Message & " - " & Query)
                    End Try
                Next
            End If
        End Sub

        Private Sub DownLoadOrganizations(ByVal EmployeeID As String)
            'Download TABOrganisation for which logged in user is related
            'Dim dsOrg = clsTabMOrganization.GetAllTabMOrganizationByEmployeeID(EmployeeID)
            'If Not dsOrg Is Nothing And dsOrg.Tables(0).Rows.Count > 0 Then
            '    Dim j As Int32
            '    For j = 0 To dsOrg.Tables(0).Rows.Count - 1
            '        Try
            '            Query = "insert into TABOrganization(CompanyID,CompanyCode,CompanyName) values(N'" & dsOrg.Tables(0).Rows(j)("CompanyID") & "',N'" & dsOrg.Tables(0).Rows(j)("CompanyCode") & "',N'" & dsOrg.Tables(0).Rows(j)("CompanyName") & "')"
            '            cln_com = New SqlCeCommand(Query, cln_con)
            '            cln_com.ExecuteNonQuery()
            '        Catch ex As Exception
            '            Log_Error("Data_Sync{PutData_Client}in Download TABOrganization:" & ex.Message & " - " & Query)
            '        End Try
            '    Next
            'End If
        End Sub

        Private Sub DownLoadUserPrivilages(ByVal EmployeeID As String, ByVal SchemaName As String)
            'Download TABUserPrivilages based on logged in user role
            Dim dsUserPriv = clsTABRolePrivileges.GetAllTABRolePrivilegesByEmployeeID_HHT(EmployeeID)
            If Not dsUserPriv Is Nothing And dsUserPriv.Tables(0).Rows.Count > 0 Then
                Dim k As Int32
                For k = 0 To dsUserPriv.Tables(0).Rows.Count - 1
                    Try
                        Query = "insert into TABUserPrivilages(MenuID,PrivilageName_en,UserID,AllowView,RoleID,CompanyID) values(N'" & dsUserPriv.Tables(0).Rows(k)("MenuID") & "',N'" & dsUserPriv.Tables(0).Rows(k)("Feature_en") & "',N'" & EmployeeID & "',N'" & dsUserPriv.Tables(0).Rows(k)("AllowView") & "',N'" & dsUserPriv.Tables(0).Rows(k)("RoleID") & "',N'" & dsUserPriv.Tables(0).Rows(k)("CompanyID") & "')"
                        cln_com = New SqlCeCommand(Query, cln_con)
                        cln_com.ExecuteNonQuery()
                    Catch ex As Exception
                        Log_Error("Data_Sync{PutData_Client}in Download TABUserPrivilages:" & ex.Message & " - " & Query)
                    End Try
                Next
            End If
        End Sub

        Private Sub DownLoadBranchPlants(ByVal EmpID As Integer, ByVal schemaname As String)
            'Download TABBranchPlant based on logged in user associated companies
            Dim dsBP = clsTabMBranchPlant.GetTabMBranchPlantByCompanyID(0, EmpID, schemaname)
            If Not dsBP Is Nothing And dsBP.Tables(0).Rows.Count > 0 Then
                Dim k As Int32
                For k = 0 To dsBP.Tables(0).Rows.Count - 1
                    Try
                        Query = "insert into TABBranchPlant(BranchID,BranchCode,BranchName,Batch,CompanyID) values(" & dsBP.Tables(0).Rows(k)("BranchID") & ",N'" & dsBP.Tables(0).Rows(k)("BranchCode") & "',N'" & dsBP.Tables(0).Rows(k)("BranchName") & "',N'" & dsBP.Tables(0).Rows(k)("Batch") & "'," & dsBP.Tables(0).Rows(k)("CompanyID") & ")"
                        cln_com = New SqlCeCommand(Query, cln_con)
                        cln_com.ExecuteNonQuery()
                    Catch ex As Exception
                        Log_Error("Data_Sync{PutData_Client}in Download TABBranchPlant:" & ex.Message & " - " & Query)
                    End Try
                Next
            End If
        End Sub

        Private Sub DownLoadCustomers(ByVal EmpID As Integer, ByVal schemaname As String)
            ' Download TABCustomers based on logged in user associated companies
            Dim dsCustomers = clsTabMCustomers.GetTabMCustomersByCompanyID(0, EmpID, schemaname)
            If Not dsCustomers Is Nothing And dsCustomers.Tables(0).Rows.Count > 0 Then
                Dim k As Int32
                For k = 0 To dsCustomers.Tables(0).Rows.Count - 1
                    Try
                        'Query = "insert into TABCustomers(CustomerID,CustomerCode,CustomerName,CompanyID,Barcode) values(" & dsCustomers.Tables(0).Rows(k)("CustomerID") & ",N'" & dsCustomers.Tables(0).Rows(k)("CustomerCode") & "',N'" & dsCustomers.Tables(0).Rows(k)("CustomerName") & "'," & dsCustomers.Tables(0).Rows(k)("CompanyID") & ",'" & dsCustomers.Tables(0).Rows(k)("Barcode") & "')"
                        Query = "insert into TABCustomers(CustomerID,CustomerCode,CustomerName,CompanyID,Barcode) values(@CustomerID,@CustomerCode,@CustomerName,@CompanyID,@Barcode)"
                        cln_com = New SqlCeCommand(Query, cln_con)
                        cln_com.Parameters.Add("@CustomerID", dsCustomers.Tables(0).Rows(k)("CustomerID"))
                        cln_com.Parameters.Add("@CustomerCode", dsCustomers.Tables(0).Rows(k)("CustomerCode"))
                        cln_com.Parameters.Add("@CustomerName", dsCustomers.Tables(0).Rows(k)("CustomerName"))
                        cln_com.Parameters.Add("@CompanyID", dsCustomers.Tables(0).Rows(k)("CompanyID"))
                        cln_com.Parameters.Add("@Barcode", dsCustomers.Tables(0).Rows(k)("Barcode"))
                        cln_com.ExecuteNonQuery()
                    Catch ex As Exception
                        Log_Error("Data_Sync{PutData_Client}in Download TABCustomers:" & ex.Message & " - " & Query)
                    End Try
                Next
            End If
        End Sub

        Private Sub DownLoadSuppliers(ByVal EmployeeID As String)
            'Download TABSupplier based on logged in user associated companies
            'Dim dsSuppliers = clsTabMSuppliers.GetTabMSuppliersByCompanyID(0, EmployeeID, objuserInfo("schemaName"))
            'If Not dsSuppliers Is Nothing And dsSuppliers.Tables(0).Rows.Count > 0 Then
            '    Dim k As Int32
            '    For k = 0 To dsSuppliers.Tables(0).Rows.Count - 1
            '        Try
            '            Query = "insert into TABCustomers(SupplierID,SupplierCode,SupplierName,CompanyID) values(N'" & dsSuppliers.Tables(0).Rows(k)("SupplierID") & "',N'" & dsSuppliers.Tables(0).Rows(k)("SupplierCode") & "',N'" & dsSuppliers.Tables(0).Rows(k)("SupplierName") & "',N'" & dsSuppliers.Tables(0).Rows(k)("CompanyID") & "')"
            '            cln_com = New SqlCeCommand(Query, cln_con)
            '            cln_com.ExecuteNonQuery()
            '        Catch ex As Exception
            '            Log_Error("Data_Sync{PutData_Client}in Download TABSuppliers:" & ex.Message & " - " & Query)
            '        End Try
            '    Next
            'End If
        End Sub

        Private Sub DownLoadItems(ByVal EmployeeID As Integer, ByVal schemaname As String)
            '  Download TABItems based on logged in user associated companies
            Dim dsItems = clsTabItems.GetTabItemsByEmployeeIDHHT(EmployeeID, schemaname)
            If Not dsItems Is Nothing And dsItems.Tables(0).Rows.Count > 0 Then
                Dim k As Int32
                For k = 0 To dsItems.Tables(0).Rows.Count - 1
                    Try
                        Query = "insert into TABItems(ItemID,ItemCode,ItemName1,ItemName2,CompanyID,UOM) values(" & dsItems.Tables(0).Rows(k)("ItemID") & ",N'" & dsItems.Tables(0).Rows(k)("ItemCode") & "',N'" & dsItems.Tables(0).Rows(k)("ItemName1") & "',N'" & dsItems.Tables(0).Rows(k)("ItemName2") & "'," & dsItems.Tables(0).Rows(k)("CompanyID") & ",N'" & dsItems.Tables(0).Rows(k)("UOM") & "')"
                        cln_com = New SqlCeCommand(Query, cln_con)
                        cln_com.ExecuteNonQuery()
                    Catch ex As Exception
                        Log_Error("Data_Sync{PutData_Client}in Download TABItems:" & ex.Message & " - " & Query)
                    End Try
                Next
            End If
        End Sub



        Private Sub DownLoadSaleOrder(ByVal EmployeeID As Integer, ByVal schemaname As String)
            ' Download TABSaleOrder based on logged in user associated companies
            Dim dsSO = clsTabKSaleOrders.GetAllTabKSaleOrdersByEmployeeIDHHT(EmployeeID, schemaname)
            If Not dsSO Is Nothing And dsSO.Tables(0).Rows.Count > 0 Then
                Dim k As Int32
                For k = 0 To dsSO.Tables(0).Rows.Count - 1
                    Try
                        ' Query = "insert into TABSaleOrder(SOrderID,OrderNumber,Barcode) values(" & dsSO.Tables(0).Rows(k)("SOrderID") & ",'" & dsSO.Tables(0).Rows(k)("OrderNumber") & "',N'" & dsSO.Tables(0).Rows(k)("Barcode") & "')"

                        Query = "insert into TABSaleOrder(SOrderID,CustomerID,CompanyID,BranchID,OrderNumber,Barcode,Status) values(" & dsSO.Tables(0).Rows(k)("SOrderID") & "," & dsSO.Tables(0).Rows(k)("CustomerID") & "," & dsSO.Tables(0).Rows(k)("CompanyID") & "," & dsSO.Tables(0).Rows(k)("BranchID") & ",N'" & dsSO.Tables(0).Rows(k)("OrderNumber") & "',N'" & dsSO.Tables(0).Rows(k)("Barcode") & "',N'" & dsSO.Tables(0).Rows(k)("Status") & "')"
                        cln_com = New SqlCeCommand(Query, cln_con)
                        cln_com.ExecuteNonQuery()
                    Catch ex As Exception
                        Log_Error("Data_Sync{PutData_Client}in Download TABSaleOrder:" & ex.Message & " - " & Query)
                    End Try
                Next
            End If
        End Sub


        Private Sub DownLoadSaleOrderBarcode(ByVal EmployeeID As Integer, ByVal schemaname As String)
            'Download TABSaleOrderBarcode based on logged in user associated companies
            Dim dsSOB = clsTabKSaleOrderBarcode.GetAllTabKSaleOrderBarcodeByEmployeeIDHHT(EmployeeID, schemaname)
            If Not dsSOB Is Nothing And dsSOB.Tables(0).Rows.Count > 0 Then
                Dim k As Int32
                For k = 0 To dsSOB.Tables(0).Rows.Count - 1
                    Try
                        Query = "insert into TABSaleOrderVerification(SOrderID,ItemID,Barcode,Quantity,Verified,Returned,SOBID) values(" & dsSOB.Tables(0).Rows(k)("SOrderID") & "," & dsSOB.Tables(0).Rows(k)("ItemID") & ",N'" & dsSOB.Tables(0).Rows(k)("Barcode") & "',N'" & dsSOB.Tables(0).Rows(k)("Quantity") & "',N'" & dsSOB.Tables(0).Rows(k)("Verified") & "'," & dsSOB.Tables(0).Rows(k)("Returned") & "," & dsSOB.Tables(0).Rows(k)("SOBID") & ")"
                        cln_com = New SqlCeCommand(Query, cln_con)
                        cln_com.ExecuteNonQuery()
                    Catch ex As Exception
                        Log_Error("Data_Sync{PutData_Client}in Download TABSaleOrderBarcode:" & ex.Message & " - " & Query)
                    End Try
                Next
                End If
        End Sub

        Private Sub DownloadEmptyInventory(ByVal EmployeeID As Integer, ByVal schemaname As String)
            'Download TABSaleOrderBarcode based on logged in user associated companies
            Dim dsINV = clsTabkEmptyInventory.GetAllTabKEmptyInventoryHHT(EmployeeID, schemaname)
            If Not dsINV Is Nothing And dsINV.Tables(0).Rows.Count > 0 Then
                Dim k As Int32
                For k = 0 To dsINV.Tables(0).Rows.Count - 1
                    Try
                        Query = "insert into TABEmptyInventory(InventoryID,CompanyID,EntityID,EntityType,ItemID,OnHandQuantity,DamagedQuantity,InTransitQuantity,TransactionDate) Values(" & dsINV.Tables(0).Rows(k)("InventoryID") & ", " & dsINV.Tables(0).Rows(k)("CompanyID") & "," & dsINV.Tables(0).Rows(k)("EntityID") & "," & dsINV.Tables(0).Rows(k)("EntityType") & ", " & dsINV.Tables(0).Rows(k)("ItemID") & "," & dsINV.Tables(0).Rows(k)("OnHandQuantity") & ", " & dsINV.Tables(0).Rows(k)("DamagedQuantity") & "," & dsINV.Tables(0).Rows(k)("InTransitQuantity") & ",'" & dsINV.Tables(0).Rows(k)("TransactionDate") & "' )"
                        cln_com = New SqlCeCommand(Query, cln_con)
                        cln_com.ExecuteNonQuery()
                    Catch ex As Exception
                        Log_Error("Data_Sync{PutData_Client}in Download TABSaleOrderBarcode:" & ex.Message & " - " & Query)
                    End Try
                Next
            End If
        End Sub
        <WebMethod()>
        Public Function ChkUserValidityHHT(ByVal username As String, ByVal password As String) As DataSet
            Dim objUserDetails As New DataSet()
            Dim objUser As New PropertyTabMEmployees
            objUser.UserName = username
            objUser.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(Trim(password), "SHA1") 'Encript the password entered for security purpose

            objUserDetails = clsTabMEmployees.ChkUserValidityHHT(objUser) 'Get The logged in user details based on usernname and password entered
            Return objUserDetails
        End Function

        <WebMethod()>
        Public Function GetEmployeeCompanyAndRolesHHT(ByVal username As String, ByVal password As String, ByVal SchemaName As String) As DataSet
            Dim objUser As New PropertyTabMEmployees
            objUser.UserName = username
            objUser.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(Trim(password), "SHA1") 'Encript the password entered for security purpose

            Dim dsUserRoleDetails As New DataSet
            dsUserRoleDetails = clsTabMEmployees.GetEmployeeCompanyAndRolesHHT(objUser, SchemaName)
            If dsUserRoleDetails.Tables(0).Rows.Count > 0 Then
                Return dsUserRoleDetails
            Else
                Return (Nothing)
            End If

        End Function

        <WebMethod()>
        Public Function GetAllTABUserPrivilegesByEmployeeID_HHTMenu(ByVal EmployeeID As Int32, ByVal CompanyID As Int32, ByVal OrganizationID As Int32, ByVal SchemaName As String) As DataSet

            Dim ds As New DataSet
            ds = clsTabMEmployees.GetAllTABUserPrivilegesByEmployeeID_HHTMenu(EmployeeID, CompanyID, OrganizationID, SchemaName)
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds
            Else
                Return (Nothing)
            End If
        End Function
        <WebMethod()>
        Public Function GetAllTabMOrganizationByEmployeeIDHHT(ByVal EmployeeID As Int32, ByVal SchemaName As String) As DataSet
            If EmployeeID <= 0 Then
                Return (Nothing)
            End If
            Dim ds As New DataSet
            ds = clsTabMOrganization.GetAllTabMOrganizationByEmployeeID(EmployeeID, SchemaName)
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds
            Else
                Return (Nothing)
            End If
        End Function
        <WebMethod()>
        Public Function GetSaleOrderNoByBarcodeHHT(ByVal barcode As String, ByVal CompanyID As Integer, ByVal schema As String) As Integer

            Dim i As Integer = 0

            i = clsTabKSaleOrders.GetSaleOrderNoByBarcodeHHT(barcode, CompanyID, schema)
            Return i

        End Function
        <WebMethod()>
        Public Function GetSaleOrderNoByCompanyIDHHT(ByVal CompanyID As Integer, ByVal schema As String) As DataSet

            Dim ds As New DataSet

            ds = clsTabKSaleOrders.GetSaleOrderNoByCompanyIDHHT(CompanyID, schema)
            Return ds

        End Function
        <WebMethod()>
        Public Function GetSaleOrderNoByEmployeeIDHHT(ByVal valEmployeeID As Integer, ByVal schema As String) As DataSet

            Dim ds As New DataSet

            ds = clsTabKSaleOrders.GetAllTabKSaleOrdersByEmployeeID(valEmployeeID, schema)
            Return ds

        End Function
        <WebMethod()>
        Public Function GetItemsByCompBarcodeDHHT(ByVal Barcode As String, ByVal CompanyID As Integer, ByVal schema As String) As DataSet

            Dim ds As New DataSet

            ds = clsTabKSaleOrders.GetItemsByCompBarcodeDHHT(Barcode, CompanyID, schema)
            Return ds

        End Function
        <WebMethod()>
        Public Function GetItemsByCompBarcodeNewHHT(ByVal Barcode As String, ByVal EmployeeID As Integer, ByVal schema As String) As DataSet

            Dim ds As New DataSet

            ds = clsTabKSaleOrders.GetItemsByCompBarcodeNewHHT(Barcode, EmployeeID, schema)
            Return ds

        End Function
        <WebMethod()>
        Public Function GetSaleOrderDetailsByOrderIDHHT(ByVal CompanyID As Int32, ByVal SchemaName As String, ByVal OrderID As Integer) As DataSet
            Dim ds As New DataSet
            ds = clsTabKSaleOrders.GetSaleOrderDetailsByOrderIDHHT(CompanyID, SchemaName, OrderID)
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds
            Else
                Return (Nothing)
            End If

        End Function
        <WebMethod()>
        Public Function GetSaleOrderItemDetailsByBarcodeHHT(ByVal barcode As String, ByVal schema As String) As DataSet
            Dim ds As New DataSet
            ds = clsTabKSaleOrderBarcode.GetSaleOrderItemDetailsByBarcodeHHT(barcode, schema)
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds
            Else
                Return (Nothing)
            End If
        End Function
        <WebMethod()>
        Public Function VerifySaleOrderHHT(ByVal CompanyID As Integer, ByVal EntityID As Integer, ByVal OnHandQuantity As Integer, ByVal SOrderID As Integer, ByVal ItemID As Integer, ByVal schema As String, ByVal BarCode As String) As Integer
            Dim i As Integer = 0
            i = clsTabKSaleOrders.VerifySaleOrderHHT(CompanyID, EntityID, OnHandQuantity, SOrderID, ItemID, schema, BarCode)
            Return i
        End Function
        <WebMethod()>
        Public Function GetCountVerifiedSaleOrderHHT(ByVal SOrderID As Integer, ByVal schema As String) As Integer
            Dim i As Integer = 0
            i = clsTabKSaleOrders.GetCountVerifiedSaleOrderHHT(SOrderID, schema)
            Return i
        End Function

        <WebMethod()>
        Public Function GetTabMBranchPlantByCompanyID(ByVal CompanyID As Integer, ByVal EmployeeID As Integer, ByVal SchemaName As String) As DataSet
            Dim ds As New DataSet
            ds = clsTabMBranchPlant.GetTabMBranchPlantByCompanyID(CompanyID, EmployeeID, SchemaName)
            Return ds
        End Function
        'Upload

        <WebMethod()> _
        Public Function Post_SyncDataTest(ByVal EmployeeID As Integer, ByVal SchemaName As String) As Boolean


            Dim Path1 = Server.MapPath("../")
            Dim pos1 As Int32 = Path1.LastIndexOf("\")
            Path1 = Path1.Substring(0, pos1)

            pos1 = Path1.LastIndexOf("\")
            Path1 = Path1.Substring(0, pos1)
            Dim dbpath As String = Path1 & "\HHTDB_Dest\trckKegs.sdf"
            dbpath.Replace("\", "/")
            Dim Get_fs As New System.IO.FileStream(dbpath, IO.FileMode.Open)
            Dim Data_By(Get_fs.Length - 1) As Byte
            Get_fs.Read(Data_By, 0, Data_By.Length)
            Get_fs.Close()

            Dim Path As String
            Dim SDF_File_Sync As String

            Try
                Path = Server.MapPath("../")
                Dim pos As Int32 = Path.LastIndexOf("\")
                Path = Path.Substring(0, pos)

                pos = Path.LastIndexOf("\")
                Path = Path.Substring(0, pos)
                SDF_File_Sync = Path & "\HHTDB_Sync\"
                SDF_File_Sync.Replace("\", "/")
                Dim TransacID As String = Guid.NewGuid.ToString()
                Dim SendFileName As String = "Track_KegsHHTDB" & "-" & TransacID & ".sdf"
                Dim pathFileName As String = SDF_File_Sync & "Track_KegsHHTDB" & "-" & TransacID & ".sdf"

                ' Dim ZipFileName As String = "Track_KegsHHTDB" & "-" & TransacID & ".zip"
                Dim fs As New System.IO.FileStream(pathFileName, FileMode.Create)
                fs.Write(Data_By, 0, Data_By.Length)
                fs.Close()
                ' DECompress_File(SDF_File_Sync & ZipFileName, FileName)
                UploadVerificationData(SendFileName, EmployeeID, SchemaName)
                UploadEmptyKegReturnData(SendFileName, EmployeeID, SchemaName)
                '  Update_Data(SendFileName, EmployeeID, SchemaName)

            Catch ex As Exception
                ' Post_SyncData = False
                'cl.Log_Error("Sync_Service(Post_SyncData):" & ex.Message & " - " & DateTime.Now.ToShortDateString)
            End Try
        End Function
        <WebMethod()> _
        Public Function Post_SyncData(ByVal Data_By() As Byte, ByVal EmployeeID As Integer, ByVal SchemaName As String) As Boolean

            Dim Path As String
            Dim SDF_File_Sync As String

            Try
                Path = Server.MapPath("../")
                Dim pos As Int32 = Path.LastIndexOf("\")
                Path = Path.Substring(0, pos)

                pos = Path.LastIndexOf("\")
                Path = Path.Substring(0, pos)
                SDF_File_Sync = Path & "\HHTDB_Sync\"
                SDF_File_Sync.Replace("\", "/")
                Dim TransacID As String = Guid.NewGuid.ToString()
                Dim FileName As String = SDF_File_Sync & "Track_KegsHHTDB" & "-" & TransacID & ".sdf"

                'Dim ZipFileName As String = "Track_KegsHHTDB" & "-" & TransacID & ".zip"
                Dim fs As New System.IO.FileStream(FileName, FileMode.Create)
                fs.Write(Data_By, 0, Data_By.Length)
                fs.Close()
                Dim SendFileName As String = "Track_KegsHHTDB" & "-" & TransacID & ".sdf"
                'DECompress_File(SDF_File_Sync & ZipFileName, FileName)
                UploadVerificationData(SendFileName, EmployeeID, SchemaName)
                UploadEmptyKegReturnData(SendFileName, EmployeeID, SchemaName)
                '  Update_Data(SendFileName, EmployeeID, SchemaName)
                Post_SyncData = True
            Catch ex As Exception
                Post_SyncData = False
                'cl.Log_Error("Sync_Service(Post_SyncData):" & ex.Message & " - " & DateTime.Now.ToShortDateString)
            End Try
        End Function
        '<WebMethod()> _
        'Public Function Post_SyncData(ByVal Data_By() As Byte, ByVal EmployeeID As Integer, ByVal SchemaName As String) As Boolean




        '    Dim Path As String
        '    Dim SDF_File_Sync As String

        '    Try
        '        Path = Server.MapPath("../")
        '        Dim pos As Int32 = Path.LastIndexOf("\")
        '        Path = Path.Substring(0, pos)

        '        pos = Path.LastIndexOf("\")
        '        Path = Path.Substring(0, pos)
        '        SDF_File_Sync = Path & "\HHTDB_Sync\"
        '        SDF_File_Sync.Replace("\", "/")

        '        Dim TransacID As String = Guid.NewGuid.ToString()
        '        Dim ZipFileName As String = "Track_KegsHHTDB" & "-" & TransacID & ".zip"
        '        Dim fs As New System.IO.FileStream(SDF_File_Sync & ZipFileName, FileMode.Create)
        '        fs.Write(Data_By, 0, Data_By.Length)
        '        fs.Close()

        '        Dim FileName As String = SDF_File_Sync & "Track_KegsHHTDB" & "-" & TransacID & ".sdf"
        '        Dim SendFileName As String = "Track_KegsHHTDB" & "-" & TransacID & ".sdf"
        '        DECompress_File(SDF_File_Sync & ZipFileName, FileName)
        '        UploadVerificationData(SendFileName, EmployeeID, SchemaName)
        '        UploadEmptyKegReturnData(SendFileName, EmployeeID, SchemaName)
        '        '  Update_Data(SendFileName, EmployeeID, SchemaName)
        '        Post_SyncData = True
        '    Catch ex As Exception
        '        Post_SyncData = False
        '        'cl.Log_Error("Sync_Service(Post_SyncData):" & ex.Message & " - " & DateTime.Now.ToShortDateString)
        '    End Try
        'End Function
        Private Sub DECompress_File(ByVal ZipFile As String, ByVal FileName As String)
            Try
                Dim strmZipInputStream As ZipInputStream = New ZipInputStream(File.OpenRead(ZipFile))
                Dim outStream As FileStream
                Dim entry As ZipEntry
                Dim buff(2047) As Byte
                Dim bytes As Integer

                Do While True
                    entry = strmZipInputStream.GetNextEntry()
                    If entry Is Nothing Then
                        Exit Do
                    End If
                    outStream = File.Create(FileName, 2048)
                    Do While True
                        bytes = strmZipInputStream.Read(buff, 0, 2048)
                        If bytes = 0 Then
                            Exit Do
                        End If
                        outStream.Write(buff, 0, bytes)
                    Loop
                    outStream.Close()
                Loop
                strmZipInputStream.Close()
            Catch ex As Exception
                'MsgBox("Zip File Exception: ", ex.Message)
            End Try

        End Sub
        <WebMethod()> _
        Public Function Post_SyncData1(ByVal EmployeeID As Integer, ByVal SchemaName As String) As Boolean
            Try
                Dim FileName As String = "trckKegs.sdf"
                'DECompress_File("E:\Projects\TrackAsset\WEB\HHTDB_Sync\Track_AssetHHTDB-0da266cc-8c55-4338-aad4-1fde481bb2c5.zip", FileName)
                UploadEmptyKegReturnData(FileName, EmployeeID, SchemaName)
                UploadVerificationData(FileName, EmployeeID, SchemaName)
                '  Update_Data(FileName, EmployeeID, SchemaName)
                Post_SyncData1 = True
            Catch ex As Exception
                Post_SyncData1 = False
                'cl.Log_Error("Sync_Service(Post_SyncData):" & ex.Message & " - " & DateTime.Now.ToShortDateString)
            End Try
        End Function

        Public Function UploadEmptyKegReturnData(ByVal FileName As String, ByVal EmployeeID As Integer, ByVal SchemaName As String) As Boolean

            Dim Path As String
            Dim SDF_File_Sync As String
            Try
                Path = Server.MapPath("../")
                Dim pos As Int32 = Path.LastIndexOf("\")
                Path = Path.Substring(0, pos)

                pos = Path.LastIndexOf("\")
                Path = Path.Substring(0, pos)
                SDF_File_Sync = Path & "\HHTDB_Sync\"
                SDF_File_Sync.Replace("\", "/")

                AppDomain.CurrentDomain.SetData("SQLServerCompactEditionUnderWebHosting", True)
                cln_con = New SqlCeConnection("Data Source=" & SDF_File_Sync & FileName)
                cln_con.Open()

                Dim i As Integer
                Dim sqlSTR As String
                Dim Dt1 As New DataTable
                'Dim TRHash As New Hashtable()
                Dim TRDic As New Dictionary(Of String, String)
                Dt1 = GetData_Client_Gid("Select * from TABEmptyKegCustomerReturn ", FileName, cln_con)
                If Dt1.Rows.Count > 0 Then
                    For i = 0 To Dt1.Rows.Count - 1
                        sqlSTR = ""
                        Try
                            Dim objTabKEmptyCustomer As New PropertyTabKEmptyCustomer
                            objTabKEmptyCustomer.CustomerID = Convert.ToInt32(Dt1.Rows(i).Item("FromCustomerID"))
                            objTabKEmptyCustomer.CompanyID = Convert.ToInt32(Dt1.Rows(i).Item("FromCompanyID"))
                            objTabKEmptyCustomer.BranchID = Convert.ToInt32(Dt1.Rows(i).Item("ToBranchID"))
                            objTabKEmptyCustomer.ItemID = Convert.ToInt32(Dt1.Rows(i).Item("FromItemID"))
                            objTabKEmptyCustomer.Quantity = Convert.ToInt32(Dt1.Rows(i).Item("Quantity"))
                            objTabKEmptyCustomer.Batch = True
                            objTabKEmptyCustomer.Barcode = Dt1.Rows(i).Item("ItemBarcode").ToString()
                            objTabKEmptyCustomer.ReceiveBy = Convert.ToInt32(Dt1.Rows(i).Item("ReceiveBy"))
                            objTabKEmptyCustomer.CollectionDate = CDate(Dt1.Rows(i).Item("ReceiveDate").ToString())
                            objTabKEmptyCustomer.SerialNumber = ""
                            objTabKEmptyCustomer.ToCompanyID = CInt(Dt1.Rows(i).Item("ToCompanyID"))
                            objTabKEmptyCustomer.ToItemID = CInt(Dt1.Rows(i).Item("ToItemID"))
                            objTabKEmptyCustomer.Status = "Done"

                            Dim TRNUM As String = Dt1.Rows(i).Item("TransactionNum").ToString()

                            If TRDic.ContainsKey(TRNUM) Then
                                objTabKEmptyCustomer.TransactionNumber = TRDic.Item(TRNUM)
                            Else
                                objTabKEmptyCustomer.TransactionNumber = Nothing
                            End If

                            'Dim result As Integer = 0
                            'result = clsTabKEmptyCustomer.validateCustomerOpeningBalance(objTabKEmptyCustomer, SchemaName)

                            'If result = 2 Then
                            ' Dim k As Boolean = clsTabKEmptyCustomer.SaveHHT(objTabKEmptyCustomer, SchemaName)
                            Dim TranNo As String = clsTabKEmptyCustomer.SaveApprovalHHT(objTabKEmptyCustomer, SchemaName)
                            If TRDic.ContainsKey(TRNUM) Then
                            Else
                                TRDic.Add(TRNUM, TranNo)
                            End If

                            '  Dim dt2 As DataTable = GetData_Client_Gid("Delete From TabEmptyKegReturn_Customer", FileName, cln_con)
                            'End If

                        Catch ex As Exception
                            Log_Error("Data_Sync{GetData_Client}:" & ex.Message & " - " & DateTime.Now.ToShortDateString & "-")
                        End Try

                    Next
                End If


                GC.Collect()
            Catch ex As Exception
                Log_Error("Data_Sync{GetData_Client}:" & ex.Message & " - " & DateTime.Now.ToShortDateString & "-")
                GC.Collect()
            End Try

        End Function
        Public Function UploadVerificationData(ByVal FileName As String, ByVal EmployeeID As Integer, ByVal SchemaName As String) As Boolean

            Dim Path As String
            Dim SDF_File_Sync As String
            Try
                Path = Server.MapPath("../")
                Dim pos As Int32 = Path.LastIndexOf("\")
                Path = Path.Substring(0, pos)

                pos = Path.LastIndexOf("\")
                Path = Path.Substring(0, pos)
                SDF_File_Sync = Path & "\HHTDB_Sync\"
                SDF_File_Sync.Replace("\", "/")

                AppDomain.CurrentDomain.SetData("SQLServerCompactEditionUnderWebHosting", True)
                cln_con = New SqlCeConnection("Data Source=" & SDF_File_Sync & FileName)
                cln_con.Open()

                Dim i As Integer
                Dim sqlSTR As String
                Dim Dt1 As New DataTable
                Dt1 = GetData_Client_Gid("Select * from TABSaleOrder where IsUpdated=1 ", FileName, cln_con)

                Dim con As New SqlConnection()
                Dim sqlcmd As SqlCommand


                Dim objConnectionStringSettings As ConnectionStringSettings = ConfigurationManager.ConnectionStrings("connectionstring")
                con.ConnectionString = objConnectionStringSettings.ConnectionString
                Try
                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
                If Dt1.Rows.Count > 0 Then
                    For i = 0 To Dt1.Rows.Count - 1
                        sqlSTR = ""
                        Try
                            Dim SOrderID As Integer = Convert.ToInt32(Dt1.Rows(i).Item("SOrderID"))

                            Try
                                Query = "Update " + SchemaName + ".TabKSaleOrders set Status='Done' where SOrderID=" & SOrderID & ""

                           

                                sqlcmd = New SqlCommand(Query, con)
                                sqlcmd.ExecuteNonQuery()
                            Catch ex As Exception
                                sqlcmd = Nothing
                                con.Close()

                                Log_Error("Data_Sync{PutData_Client}in Upload TabSale Order:" & ex.Message & " - " & Query)
                            End Try


                        Catch ex As Exception
                            Log_Error("Data_Sync{Upload_Client}:" & ex.Message & " - " & DateTime.Now.ToShortDateString & "-")
                        End Try

                    Next
                End If
                Dim Dt2 As New DataTable
                Dt2 = GetData_Client_Gid("Select * from TabSaleOrderVerification where IsUpdated=1 ", FileName, cln_con)
                If Dt2.Rows.Count > 0 Then
                    For i = 0 To Dt2.Rows.Count - 1
                        sqlSTR = ""
                        sqlcmd = Nothing

                        Dim SOBID As Integer = Convert.ToInt32(Dt2.Rows(i).Item("SOBID"))
                        Try
                            Query = "Update " + SchemaName + ".TabKSaleOrderBarcode set Verified='True' where SOBID=" & SOBID & ""
                            If con.State = ConnectionState.Closed Then
                                con.Open()
                            End If
                            sqlcmd = New SqlCommand(Query, con)
                            sqlcmd.ExecuteNonQuery()
                        Catch ex As Exception
                            sqlcmd = Nothing
                            con.Close()
                        End Try

                    Next
                End If
                GC.Collect()
                con.Close()
            Catch ex As Exception

                Log_Error("Data_Sync{Updload_Client}:" & ex.Message & " - " & DateTime.Now.ToShortDateString & "-")
                GC.Collect()
            End Try

        End Function
        Public Function Update_Data(ByVal FileName As String, ByVal EmployeeID As Integer, ByVal SchemaName As String) As Boolean


            Dim Path As String
            Dim SDF_File_Sync As String
            Try
                Path = Server.MapPath("../")
                Dim pos As Int32 = Path.LastIndexOf("\")
                Path = Path.Substring(0, pos)

                pos = Path.LastIndexOf("\")
                Path = Path.Substring(0, pos)
                SDF_File_Sync = Path & "\HHTDB_Sync\"
                SDF_File_Sync.Replace("\", "/")

                AppDomain.CurrentDomain.SetData("SQLServerCompactEditionUnderWebHosting", True)
                cln_con = New SqlCeConnection("Data Source=" & SDF_File_Sync & FileName)
                cln_con.Open()

                Dim i As Integer
                Dim sqlSTR As String
                Dim Dt1 As New DataTable
                'Dim TRHash As New Hashtable()
                Dim TRDic As New Dictionary(Of String, String)
                Dt1 = GetData_Client_Gid("Select * from TabEmptyKegReturnCustomer ", FileName, cln_con)
                If Dt1.Rows.Count > 0 Then
                    For i = 0 To Dt1.Rows.Count - 1
                        sqlSTR = ""
                        Try
                            Dim objTabKEmptyCustomer As New PropertyTabKEmptyCustomer
                            objTabKEmptyCustomer.CustomerID = Convert.ToInt32(Dt1.Rows(i).Item("CustomerID"))
                            objTabKEmptyCustomer.CompanyID = Convert.ToInt32(Dt1.Rows(i).Item("CompanyID"))
                            objTabKEmptyCustomer.BranchID = Convert.ToInt32(Dt1.Rows(i).Item("ReceiveBranch"))
                            objTabKEmptyCustomer.ItemID = Convert.ToInt32(Dt1.Rows(i).Item("ItemID"))
                            objTabKEmptyCustomer.Quantity = Convert.ToInt32(Dt1.Rows(i).Item("Quantity"))
                            objTabKEmptyCustomer.Batch = Convert.ToBoolean(Dt1.Rows(i).Item("Batch"))
                            objTabKEmptyCustomer.Barcode = Dt1.Rows(i).Item("Barcode").ToString()
                            objTabKEmptyCustomer.ReceiveBy = Convert.ToInt32(Dt1.Rows(i).Item("ReceiveBy"))
                            objTabKEmptyCustomer.CollectionDate = CDate(Dt1.Rows(i).Item("ReceiveDate").ToString())
                            objTabKEmptyCustomer.SerialNumber = Dt1.Rows(i).Item("SerialNumber").ToString()
                            objTabKEmptyCustomer.ToCompanyID = CInt(Dt1.Rows(i).Item("ToCompanyID"))
                            objTabKEmptyCustomer.ToItemID = CInt(Dt1.Rows(i).Item("ToItemID"))
                            objTabKEmptyCustomer.Status = "Done"

                            Dim TRNUM As String = Dt1.Rows(i).Item("TransactionNum").ToString()

                            If TRDic.ContainsKey(TRNUM) Then
                                objTabKEmptyCustomer.TransactionNumber = TRDic.Item(TRNUM)
                            Else
                                objTabKEmptyCustomer.TransactionNumber = Nothing
                            End If

                            'Dim result As Integer = 0
                            'result = clsTabKEmptyCustomer.validateCustomerOpeningBalance(objTabKEmptyCustomer, SchemaName)

                            'If result = 2 Then
                            ' Dim k As Boolean = clsTabKEmptyCustomer.SaveHHT(objTabKEmptyCustomer, SchemaName)
                            Dim TranNo As String = clsTabKEmptyCustomer.SaveHHT(objTabKEmptyCustomer, SchemaName)
                            If TRDic.ContainsKey(TRNUM) Then
                            Else
                                TRDic.Add(TRNUM, TranNo)
                            End If

                            '  Dim dt2 As DataTable = GetData_Client_Gid("Delete From TabEmptyKegReturn_Customer", FileName, cln_con)
                            'End If

                        Catch ex As Exception
                            Log_Error("Data_Sync{GetData_Client}:" & ex.Message & " - " & DateTime.Now.ToShortDateString & "-")
                        End Try

                    Next
                End If


                GC.Collect()
            Catch ex As Exception
                Log_Error("Data_Sync{GetData_Client}:" & ex.Message & " - " & DateTime.Now.ToShortDateString & "-")
                GC.Collect()
            End Try
        End Function
        Private Function GetData_Client_Gid(ByVal Query As String, ByVal FileName As String, ByVal cln_con As SqlCeConnection) As DataTable
            Dim dt As New DataTable
            Try
                cln_com = New SqlCeCommand(Query, cln_con)
                cln_dap = New SqlCeDataAdapter(cln_com)
                cln_dap.Fill(dt)
            Catch ex As Exception
                Log_Error("Data_Sync{GetData_Client}:" & ex.Message & " - " & DateTime.Now.ToShortDateString & "-" & Query)
            Finally

            End Try
            Return dt
        End Function

        <WebMethod()>
        Public Function CreateNewTabKEmptyTransferBPHHT(ByVal FromCompanyID As Int32, ByVal ToCompanyID As Int32, ByVal Barcode As String, ByVal FromBP As Int32, ByVal ToBP As Int32, ByVal QTY As Int32, ByVal ItemID As Int32, ByVal batch As Boolean, ByVal TransferDate As Date, ByVal TransferedBy As Int32, ByVal SchemaName As String, ByVal Status As String, ByVal InTransitQuantity As Int32, ByVal TRNUM As String) As Integer

            Try
                Dim i As Integer
                Dim objTransfers As New PropertyTabKEmptyTransferBP
                objTransfers.CompanyID = FromCompanyID
                objTransfers.ToCompanyID = ToCompanyID
                objTransfers.Barcode = Barcode
                objTransfers.FromBranchID = FromBP
                objTransfers.ToBranchID = ToBP
                objTransfers.ItemID = ItemID

                objTransfers.Quantity = QTY
                objTransfers.Batch = batch
                objTransfers.TransferDate = TransferDate
                objTransfers.TransferBy = TransferedBy
                objTransfers.Status = Status

                objTransfers.InTransitQuantity = InTransitQuantity
                objTransfers.TransactionNumber = TRNUM
                Dim result As Integer = 0
                result = clsTabKEmptyTransferBP.validateBPOpeningBalance(objTransfers, SchemaName)


                Dim Count = clsTabKEmptyTransferBP.ValidateforOpeningBal(FromCompanyID, objTransfers.FromBranchID, 1, ItemID, QTY, SchemaName)
                If Count = 0 Then
                    Return 2 '  "Please enter Opening balance for selected branch plant and selected Item"
                    Exit Function
                ElseIf Count = 1 Then
                    Return 3 '"Quantity entered is greater than latest onhand quantity for slected branch plant and item.Enter correct quantity."
                    Exit Function
                End If

                Dim CNT = clsTabKEmptyTransferBP.ValidateforNegativeOpeningBal(FromCompanyID, FromBP, 1, ItemID, QTY, TransferDate, SchemaName)
                If CNT = 0 Then
                    Return 2 '  "Please enter Opening balance for selected BranchPlant and selected Item for the  Transfer date"
                    Exit Function

                ElseIf Count = 1 Then
                    Return 3 '"Quantity entered is greater than latest onhand quantity for slected Branch Plant and item.Enter correct quantity."
                    Exit Function
                End If



                ' process 



                i = clsTabKEmptyTransferBP.SaveHHT(objTransfers, SchemaName)
                Return (i)
            Catch ex As Exception
                Return -1
            End Try
           






        End Function
        <WebMethod()>
        Public Function ValidateItemCheckOpeningBalance(ByVal FromCompanyID As Integer, ByVal FromBP As Integer, ByVal ItemID As Integer, ByVal qty As Integer, ByVal Transferdate As Date, ByVal schemaname As String) As Integer

            Dim Count = clsTabKEmptyTransferBP.ValidateforOpeningBal(FromCompanyID, FromBP, 1, ItemID, qty, schemaname)
            If Count = 0 Then
                Return 2 '  "Please enter Opening balance for selected branch plant and selected Item"
                Exit Function
            ElseIf Count = 1 Then
                Return 3 '"Quantity entered is greater than latest onhand quantity for slected branch plant and item.Enter correct quantity."
                Exit Function
            End If

            Dim CNT = clsTabKEmptyTransferBP.ValidateforNegativeOpeningBal(FromCompanyID, FromBP, 1, ItemID, qty, Transferdate, SchemaName)
            If CNT = 0 Then
                Return 2 '  "Please enter Opening balance for selected BranchPlant and selected Item for the  Transfer date"
                Exit Function

            ElseIf Count = 1 Then
                Return 3 '"Quantity entered is greater than latest onhand quantity for slected Branch Plant and item.Enter correct quantity."
                Exit Function
            End If
        End Function
        <WebMethod()>
        Public Function GetTabKEmptyTransferBPByFromToBranchID(ByVal FromBranchID As Int32, ByVal ToBranchID As Int32, ByVal SchemaName As String, ByVal TransactionNumber As String) As DataSet


            Return clsTabKEmptyTransferBP.GetTabKEmptyTransferBPByFromToBranchID(FromBranchID, ToBranchID, SchemaName, TransactionNumber)

        End Function

        <WebMethod()>
        Public Function GetTabKEmptyTransferBPByFromToBranchIDHHT(ByVal FromBranchID As Int32, ByVal ToBranchID As Int32, ByVal SchemaName As String) As DataSet


            Return clsTabKEmptyTransferBP.GetTabKEmptyTransferBPByFromToBranchIDHHT(FromBranchID, ToBranchID, SchemaName)

        End Function
        <WebMethod()>
        Public Function UpdateTabKEmptyReceiveBPtoBP(ByVal EBPOrderID As Integer, ByVal FromBranchID As Int32, ByVal ToBranchID As Int32, ByVal CompanyID As Int32, ByVal ItemID As Int32, ByVal QTY As Int32, ByVal ReceiveDate As Date, ByVal EmployeeID As Int32, ByVal ReceiveQTY As Int32, ByVal schemaName As String, ByVal Status As String) As Boolean

            Dim i As Integer
            Dim objTransfers As New PropertyTabKEmptyTransferBP
            objTransfers.CompanyID = CompanyID
            objTransfers.FromBranchID = FromBranchID
            objTransfers.ToBranchID = ToBranchID
            objTransfers.ItemID = ItemID
            objTransfers.Quantity = QTY
            objTransfers.EBPOrderID = EBPOrderID
            objTransfers.ReceiveDate = ReceiveDate
            objTransfers.ReceiveBy = EmployeeID
            objTransfers.ReceiveQty = ReceiveQTY
            objTransfers.Status = Status
            If Not objTransfers Is Nothing Then

                Return clsTabKEmptyTransferBP.UpdateTabKEmptyReceiveBPtoBP_Edit(objTransfers, schemaName, 1)
                ' Return clsTabKEmptyTransferBP.UpdateTabKEmptyReceiveBPtoBP(objTransfers, schemaName)
            Else
                Return False
            End If
        End Function

        <WebMethod()>
        Public Function GetMaxTransactionNumber(ByVal schemaName As String, ByVal type As Int32, ByVal CompanyID As Integer) As DataSet
            If schemaName.Length = 0 Then
                Return Nothing
            End If
            Dim ds As New DataSet
            ds = clsTabKEmptyCustomer.GetMaxTransactionNumber(schemaName, type, CompanyID)

            Return ds
        End Function
        <WebMethod()>
        Public Function GetBranchCompByBranchID(ByVal schemaName As String, ByVal BranchID As Integer) As Integer
            If schemaName.Length = 0 Then
                Return Nothing
            End If

            Return clsTabMBranchPlant.GetTabMBranchPlantCompByBranchID(BranchID, schemaName)


        End Function

        <WebMethod()>
        Public Function GetTabItemsBySearchHHT(ByVal schemaName As String, ByVal CompanyID As Integer, ByVal SearchKey As String) As DataSet
            If schemaName.Length = 0 Then
                Return Nothing
            End If
            Dim ds As New DataSet
            ds = clsTabItems.GetTabItemsBySearchHHT(schemaName, CompanyID, SearchKey)

            Return ds
        End Function

        <WebMethod()>
        Public Function ValidateDeviceDate(ByVal DeviceDate As String) As Boolean
            Dim valid As Boolean
            valid = clsValidations.ValidateDeviceDate(DeviceDate)
            Return valid
        End Function

    End Class
End Namespace
