﻿Imports System.ComponentModel
Imports System.Text.RegularExpressions
Imports System.Net
Imports System.Web.Script.Serialization
Imports System.Dynamic
Imports System.Text
Imports System.IO

<Runtime.InteropServices.ComVisible(True)>
Public Class ucParamList
    'Start Property
    Private vTitleValue As String
    Private vCode As String

    Dim objApiService As New clsAPIService

    Private vParameterName As String
    Public Property parameter() As String
        Get
            Return vParameterName
        End Get
        Set(ByVal value_ As String)
            vParameterName = value_
        End Set
    End Property

    Private vId As String
    Public Property id() As String
        Get
            Return vId
        End Get
        Set(ByVal value_ As String)
            vId = value_
        End Set
    End Property

    Public Property title() As String
        Get
            Return vTitleValue
        End Get
        Set(ByVal value As String)
            vTitleValue = value
        End Set
    End Property

    Private vMessage As String
    Public Property message() As String
        Get
            Return vMessage
        End Get
        Set(ByVal value As String)
            vMessage = value
        End Set
    End Property

    Private vDefaultValue As String

    <DefaultValue(True)>
    Public Property value() As String
        Get
            Return vDefaultValue
        End Get
        Set(ByVal value As String)
            vDefaultValue = value
            Try
                Dim list1 As ComboBox = CType(Me.Controls(vParameterName), ComboBox)
                list1.SelectedValue = value
            Catch ex As Exception

            End Try
        End Set
    End Property

    Private vRegEx As String
    Public Property regExpress() As String
        Get
            Return vRegEx
        End Get
        Set(ByVal value As String)
            vRegEx = value
        End Set
    End Property

    Private vSlug As String
    Public Property slug() As String
        Get
            Return vSlug
        End Get
        Set(ByVal value As String)
            vSlug = value
        End Set
    End Property

    Private vUrl As String
    Public Property url() As String
        Get
            Return vUrl
        End Get
        Set(ByVal value As String)
            vUrl = value
        End Set
    End Property

    Private vCacheUrl As String
    Public Property cache_url() As String
        Get
            Return vCacheUrl
        End Get
        Set(ByVal value As String)
            vCacheUrl = value
            'objApiService.Url = value
        End Set
    End Property

    Private vAbsoluteUrl As String
    Public Property absolute_url() As String
        Get
            Return vAbsoluteUrl
        End Get
        Set(ByVal value As String)
            vAbsoluteUrl = value
        End Set
    End Property

    Dim vAccessToken As String
    Public Property access_token() As String
        Get
            Return vAccessToken
        End Get
        Set(ByVal Value As String)
            Me.vAccessToken = Value
            objApiService.access_token = Value
        End Set
    End Property

    Private vRequired As Boolean
    Public Property required() As Boolean
        Get
            Return vRequired
        End Get
        Set(ByVal value As Boolean)
            vRequired = value
        End Set
    End Property


    'Function getJsonObject(ByVal address As String) As Object
    '    'Support request with Token
    '    Dim request As WebRequest = WebRequest.Create(address)
    '    request.Method = "GET"
    '    Dim byteArray As Byte() = Encoding.UTF8.GetBytes("")
    '    request.PreAuthenticate = True
    '    request.Headers.Add("Authorization", "Bearer " + access_token)


    '    Dim myHttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
    '    Dim myWebSource As New StreamReader(myHttpWebResponse.GetResponseStream())
    '    Dim myPageSource As String = myWebSource.ReadToEnd()

    '    Dim jss = New JavaScriptSerializer()
    '    Dim data As Object = jss.Deserialize(Of Object)(myPageSource)
    '    Return data


    'End Function

    'Private Function getSerialNumber(vSerialNumber As String) As Object
    '    Dim json As Object
    '    json = getJsonObject(vUrl + "/api/serialnumber/?q=" + vSerialNumber)
    '    getSerialNumber = json
    'End Function


    'Private Function getObjectByUrl(vUrl As String) As Object
    '    Dim json As Object
    '    json = getJsonObject(vUrl)
    '    getObjectByUrl = json
    'End Function

    'Private Function getObjectBySlug(vApp As String, vSlug As String) As Object
    '    Dim json As Object
    '    Dim vUrlSlug As String
    '    vUrlSlug = vUrl + "/api/" + vApp + "/" + vSlug & "/"
    '    json = getJsonObject(vUrlSlug)
    '    getObjectBySlug = json
    'End Function

    Private Sub ucParamList_Load(sender As Object, e As EventArgs) Handles Me.Load

        objApiService.Url = vUrl

        Me.AutoSize = True
        Dim label As New Label
        Dim text As New TextBox
        Dim clist As New ComboBox
        Dim labelMsg As New Label

        With label
            .Name = "caption"
            .Text = If(vRequired, "*" + vTitleValue, vTitleValue)
            '.Anchor = AnchorStyles.Left + AnchorStyles.Top
            .Dock = DockStyle.Left
        End With
        'With labelMsg
        '    .Name = "lblMsg"
        '    .Text = vMessage
        '    .Dock = DockStyle.Fill
        'End With

        With clist
            .Name = vParameterName '"list"
            '.Text = vDefaultValue
            .Size = New Size(200, 30)
            .Anchor = AnchorStyles.Left
            .Dock = DockStyle.Right
            .Margin = New Padding(5)

        End With
        AddHandler clist.SelectionChangeCommitted, AddressOf clist_SelectionChangeCommitted
        AddHandler label.MouseClick, AddressOf open_help
        'AddHandler text.Validating, AddressOf text_Validating
        'AddHandler text.KeyPress, AddressOf text_KeyPress
        'AddHandler text.GotFocus, AddressOf text_GotFocus



        Me.Controls.Add(label)
        Me.Controls.Add(clist)

        'Get List------
        Dim vR As String
        vR = getItem(vSlug, clist)
        '--------------
    End Sub

    Friend Sub open_help(sender As Object, e As EventArgs)
        Dim lbl = DirectCast(sender, Label)
        Process.Start(vUrl + vAbsoluteUrl)
    End Sub

    Friend Sub clist_SelectionChangeCommitted(sender As Object, e As EventArgs)
        Dim clist = DirectCast(sender, ComboBox)
        vDefaultValue = clist.SelectedValue
        executeScript(vParameterName, clist.SelectedValue)


    End Sub

    'End Property

    'Private Function executeScript() As Boolean
    '    Dim vCls As New clsMPFlex
    '    'initial
    '    vCls.Form = vCurrentFormIn
    '    vCls.Url = vUrl
    '    vCls.access_token = vAccessToken

    '    Dim vReturn As String
    '    vCode = getCode(vSlug)
    '    vReturn = vCls.executeScritp(vCode)
    '    'MsgBox(vCls.Url)
    '    'lblSuccess.Text = vCls.success
    '    'lblMsg.Text = vCls.message
    '    If Not vCls.success Then
    '        MsgBox(vCls.error_message)
    '    End If

    '    Return False
    'End Function

    Private Function executeScript(Optional key As String = "",
                                   Optional value As String = "") As Boolean
        Dim vCls As New clsMPFlex
        'initial
        vCls.Form = vCurrentFormIn
        vCls.Url = vUrl
        vCls.access_token = vAccessToken
        vCls.code_name = vSlug
        'for parameter
        vCls.selectedKey = key
        vCls.selectedValue = value
        vCls.cacheUrl = vCacheUrl
        '------------
        objApiService.Url = vUrl
        objApiService.access_token = vAccessToken
        Dim vReturn As String
        vCode = getCode(vSlug)
        If vCode <> "" Then
            vReturn = vCls.executeScritp(vCode)

            If Not vCls.success Then
                MsgBox(vCls.error_message, MsgBoxStyle.Critical, "Snippet code Error")
                Return False
            End If
            Return vReturn
        End If
        Return True
    End Function

    Private Function getCode(vSlug_ As String) As String
        'Dim iObject As Object
        'iObject = getItemBySlug(vSlug_)
        Dim iObject As Object
        iObject = objApiService.getObjectBySlug("item", vSlug_)

        If iObject("snippet") Is Nothing Then
            getCode = ""
        Else
            ' getCode = getSnippetBySlug(iObject("snippet"))("code")
            getCode = objApiService.getObjectByUrl(iObject("snippet")("url"))("code")
        End If

    End Function
    'Private Function getCode(vSlug_ As String) As String
    '    Dim iObject As Object
    '    iObject = objApiService.getObjectBySlug("item", vSlug_)
    '    If iObject("snippet") Is Nothing Then
    '        getCode = ""
    '    Else
    '        getCode = objApiService.getObjectBySlug("snippet", iObject("snippet"))("code")
    '    End If

    'End Function

    Private Function getItem(vSlug_ As String, ucList As ComboBox) As String
        Dim iObject As Object
        Dim iListObj As Object
        Dim vName As String
        Dim vTile As String
        Dim vValue As String
        Dim vDefault As Boolean
        Dim vOrdered As Integer
        Dim vStatus As String

        iObject = objApiService.getObjectBySlug("item", vSlug_)
        iListObj = iObject("lists")
        Dim comboSource As New Dictionary(Of String, String)()
        'Dim vDefaultValue As String = ""
        Dim objEachChoice As Object
        For Each aa In iListObj
            objEachChoice = objApiService.getObjectByUrl(aa)
            vName = objEachChoice("name")
            vTile = objEachChoice("title")
            vValue = objEachChoice("value")
            vDefault = objEachChoice("default")
            If vDefault Then
                vDefaultValue = vValue
            End If
            vOrdered = objEachChoice("ordered")
            vStatus = objEachChoice("status")
            If vStatus = "A" Then
                comboSource.Add(vName & ":" & vTile, vValue)
            End If

        Next
        With ucList
            .DropDownStyle = ComboBoxStyle.DropDownList
            .DataSource = New BindingSource(comboSource, Nothing)
            .DisplayMember = "Key"
            .ValueMember = "Value"
            .SelectedValue = vDefaultValue
        End With

        Return "s"
        'getCode = getSnippetBySlug(iObject("snippet"))("code")
    End Function

    '--------Mandatory Funtion for all User Controls-------------
    '--------DO NOT change---------------------------------------
    Dim toolTip1 As New ToolTip()
    Dim vParentObjectName As String
    Private vCurrentFormIn As Form

    Public Property ParentObjectName() As String
        Get
            ParentObjectName = vParentObjectName
        End Get
        Set(vValue As String)
            vParentObjectName = vValue
        End Set
    End Property

    Public Property getLocalObjectValue(ByVal vObjectName As String) As String
        Get
            Dim vStep() As String
            vStep = Split(vObjectName, ".")
            If UBound(vStep) > 0 Then
                Dim nControl As Control
                nControl = Controls(vStep(0))
                'getLocalObjectValue = nControl.getLocalObjectValue(vStep(1)).text
                getLocalObjectValue = CallByName(nControl, "", Microsoft.VisualBasic.CallType.Get, vStep(1))
            Else
                getLocalObjectValue = Controls(vObjectName).Text
            End If
        End Get
        Set(value As String)

        End Set
    End Property

    Public Property localControls(vObjName As String) As Object
        Get
            localControls = Me.Controls(vObjName)
        End Get
        Set(value As Object)

        End Set
    End Property

    Public Property CurrentForm() As Form
        Get
            CurrentForm = vCurrentFormIn
        End Get
        Set(vCurrForm As Form)
            vCurrentFormIn = vCurrForm
        End Set
    End Property
    '    Public Property Get localControls(vObjName As String) As Object
    '        Set localControls = UserControl.Controls(vObjName)
    'End Property

    '    Public Property Let CurrentForm(ByVal vCurrForm As Form)
    '        Set vCurrentFormIn = vCurrForm
    'End Property

    '    Public Function initODCSEscript()
    '        objOdcsys.setCustomForm = vCurrentFormIn
    '        objOdcsys.setAllowUI = True
    '        objOdcsys.setOnCustomControl
    '    End Function

    Private Sub showObjectName()
        Dim nControl As Control
        'Comment by Chutchai S on March 2,2009
        'To fix program Crash , because below this command.
        '    For Each nControl In UserControl.Controls
        '        If Not TypeOf UserControl.ParentControls.Item(0) Is Form Then
        '            nControl.ToolTipText = UserControl.ParentControls.Item(0).Name & "." & Extender.Name & "." & nControl.Name
        '        Else
        '            nControl.ToolTipText = Extender.Name & "." & nControl.Name
        '        End If
        '    Next

        'Added by Chutchai S on March 2,2009
        Dim strTooltrip As String
        For Each nControl In Me.Controls
            'nControl.ToolTipText = Extender.Name & "." & nControl.Name

            'If vParentObjectName = "" Then
            '    strTooltrip = Me.Name & "." & nControl.Name
            'Else
            '    strTooltrip = vParentObjectName & "." & Me.Name & "." & nControl.Name
            'End If

            Dim vParentName As String

            If TypeOf Me.Parent Is Form Then
                strTooltrip = Me.Name & "." & nControl.Name
            Else

                vParentName = Me.Parent.Name
                strTooltrip = vParentName & "." & Me.Name & "." & nControl.Name
            End If
            toolTip1.SetToolTip(nControl, strTooltrip)

        Next

    End Sub
    Private Function getExtenderName(vObjName As String) As String
        'Added by Tuk on July 4,2008
        'To protect "send error to MS" box
        'Still not sure.

        On Error GoTo HasError

        'Comment by Chutchai S on March 2,2009
        'To fix program Crash , because below this command.
        '    If Not TypeOf UserControl.ParentControls.Item(0) Is Form Then
        '        getExtenderName = UserControl.ParentControls.Item(0).Name & "." & Extender.Name & "." & vObjName
        '    Else
        '        getExtenderName = Extender.Name & "." & vObjName
        '    End If


        'Added by Chutchai S on March 2,2009
        If vParentObjectName = "" Then
            getExtenderName = Me.Name & "." & vObjName
        Else
            getExtenderName = vParentObjectName & "." & Me.Name & "." & vObjName
        End If

HasError:
    End Function
    Public Sub showOpject()
        showObjectName()

    End Sub
End Class
