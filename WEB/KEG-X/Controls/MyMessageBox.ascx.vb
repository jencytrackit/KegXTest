Imports System
Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq

Partial Public Class MyMessageBox
    Inherits System.Web.UI.UserControl
#Region "Properties"
    Private _ShowCloseButton As Boolean
    Public Property ShowCloseButton() As Boolean
        Get
            Return _ShowCloseButton
        End Get
        Set(ByVal value As Boolean)
            _ShowCloseButton = value
        End Set
    End Property

#End Region

#Region "Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        If ShowCloseButton Then
            CloseButton.Attributes.Add("onclick", "document.getElementById('" & MessageBox.ClientID & "').style.display = 'none'")
        End If
    End Sub
#End Region

#Region "Wrapper methods"
    Public Sub ShowError(ByVal message As String)
        CloseButton.Attributes.Add("onclick", "document.getElementById('" & MessageBox.ClientID & "').style.display = 'none'")
        'CloseButton.Attributes.Add("onclick", "ctl00_cphMain_MessageBox_MessageBox.style.display = 'none'")
        Show(MessageType.[Error], message)
    End Sub

    Public Sub ShowInfo(ByVal message As String)
        CloseButton.Attributes.Add("onclick", "document.getElementById('" & MessageBox.ClientID & "').style.display = 'none'")
        Show(MessageType.Info, message)
    End Sub

    Public Sub ShowSuccess(ByVal message As String)
        CloseButton.Attributes.Add("onclick", "document.getElementById('" & MessageBox.ClientID & "').style.display = 'none'")
        Show(MessageType.Success, message)
    End Sub

    Public Sub ShowWarning(ByVal message As String)
        CloseButton.Attributes.Add("onclick", "document.getElementById('" & MessageBox.ClientID & "').style.display = 'none'")
        Show(MessageType.Warning, message)
    End Sub
#End Region

#Region "Show control"
    Public Sub Show(ByVal messageType As MessageType, ByVal message As String)
        CloseButton.Visible = ShowCloseButton
        litMessage.Text = message

        MessageBox.CssClass = messageType.ToString().ToLower()
        Me.Visible = True
    End Sub
#End Region

#Region "Enum"
    Public Enum MessageType
        [Error] = 1
        Info = 2
        Success = 3
        Warning = 4
    End Enum
#End Region
End Class 
