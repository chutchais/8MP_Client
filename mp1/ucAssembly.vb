﻿Imports System.ComponentModel
Imports System.Text.RegularExpressions
Imports System.Net
Imports System.Web.Script.Serialization
Imports System.Dynamic

<Runtime.InteropServices.ComVisible(True)>
Public Class ucAssembly
    '----------------Start Standard Property------------------

    Dim objApiService As New clsAPIService
    Dim objCurrentAssembly As Object

    'RegEx for all Item
    Dim strDateCodeRegEx As String = String.Empty
    Dim strLotCodeRegEx As String = String.Empty
    Dim strSupplyCodeRegEx As String = String.Empty
    Dim strSerialNumberRegEx As String = String.Empty

    Private routingDetailSlug As String 'Rotuing Detail URL
    Public Property routing() As String
        Get
            Return routingDetailSlug
        End Get
        Set(ByVal value As String)
            routingDetailSlug = value
            '---Get Routing URL---
            If routingDetailSlug = "" Then
                Exit Property
            End If
            Dim objRoutingDetail As Object
            objRoutingDetail = objApiService.getObjectByUrl(value)
            showAssemblyProfile(objRoutingDetail("assembly_usages"))
            '---------------------
        End Set
    End Property

    Private vproductName As String
    Public Property product() As String
        Get
            Return vproductName
        End Get
        Set(ByVal value As String)
            vproductName = value
        End Set
    End Property


    Private vUrl As String
    Public Property url() As String
        Get
            Return vUrl
        End Get
        Set(ByVal value As String)
            vUrl = value
            objApiService.Url = value
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

            objApiService.access_token = Value
        End Set
    End Property
    '-------------End Standard Property--------------


    Sub showAssemblyProfile(objAssemblyUsage As Object)
        If objAssemblyUsage.length > 0 Then
            'first level "assembly"
            Dim comboSource As New Dictionary(Of String, String)()
            comboSource.Add("", "---")
            For Each assembly In objAssemblyUsage
                comboSource.Add(assembly("assembly")("name") & ":" &
                        assembly("assembly")("title"), assembly("assembly")("url"))

            Next
            With cbAssemblyProfile
                .DropDownStyle = ComboBoxStyle.DropDownList
                .DataSource = New BindingSource(comboSource, Nothing)
                .DisplayMember = "Key"
                .ValueMember = "Value"
                '.SelectedValue = vDefaultValue
                '.Items.Insert(0, String.Empty)
            End With




        End If

    End Sub


    Public Shared Function getJsonString(ByVal address As String) As String

        Dim client As WebClient = New WebClient()
        Dim reply As String = client.DownloadString(address)
        Return reply
    End Function

    Public Shared Function getJsonObject(ByVal address As String) As Object
        Dim client As WebClient = New WebClient()
        Dim json As String = client.DownloadString(address)
        Dim jss = New JavaScriptSerializer()
        Dim data As Object = jss.Deserialize(Of Object)(json)
        Return data
    End Function


    Private Function getSnippetBySlug(vSnippetSlug As String) As Object
        Dim json As Object
        json = getJsonObject(vUrl + "/api/snippet/" + vSnippetSlug)
        Return json
    End Function

    Private Function getSnippetByUrl(url As String) As Object
        Dim json As Object
        json = getJsonObject(url)
        Return json
    End Function


    Private Function executeScript(Optional key As String = "",
                                   Optional value As String = "") As Boolean
        Dim vCls As New clsMPFlex
        'initial
        vCls.Form = vCurrentFormIn
        vCls.Url = vUrl
        vCls.access_token = vAccessToken
        'vCls.code_name = vSlug
        'for parameter
        vCls.selectedKey = key
        vCls.selectedValue = value
        vCls.cacheUrl = vCacheUrl
        '------------
        objApiService.Url = vUrl
        objApiService.access_token = vAccessToken
        'Dim vReturn As String
        'vCode = getCode(vSlug)
        'If vCode <> "" Then
        '    vReturn = vCls.executeScritp(vCode)

        '    If Not vCls.success Then
        '        MsgBox(vCls.error_message, MsgBoxStyle.Critical, "Snippet code Error")
        '        Return False
        '    End If
        '    Return vReturn
        'End If
        Return True
    End Function

    'Private Function getCode(vSlug_ As String) As String
    '    'Dim iObject As Object
    '    'iObject = getItemBySlug(vSlug_)
    '    Dim iObject As Object
    '    iObject = objApiService.getObjectBySlug("item", vSlug_)

    '    If iObject("snippet") Is Nothing Then
    '        getCode = ""
    '    Else
    '        ' getCode = getSnippetBySlug(iObject("snippet"))("code")
    '        getCode = objApiService.getObjectByUrl(iObject("snippet")("url"))("code")
    '    End If

    'End Function

    Private Sub cbAssemblyProfile_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbAssemblyProfile.SelectionChangeCommitted
        Dim key As String = DirectCast(cbAssemblyProfile.SelectedItem, KeyValuePair(Of String, String)).Key
        Dim AssemblySlug As String = DirectCast(cbAssemblyProfile.SelectedItem, KeyValuePair(Of String, String)).Value 'slug

        If key <> "" Then
            objCurrentAssembly = objApiService.getObjectByUrl(AssemblySlug & "items/")
            dgProfile.Rows.Clear()
            For Each part In objCurrentAssembly
                With dgProfile
                    Dim n As Integer = .Rows.Add()
                    .Rows.Item(n).Cells(0).Value = part("part")("rd")
                    .Rows.Item(n).Cells(1).Value = part("part")("pn")
                    .Rows.Item(n).Cells(2).Value = part("part")("pn_type")
                    .Rows.Item(n).Cells(3).Value = part("part")("title")
                End With
            Next
        End If
    End Sub


    Private Sub dgProfile_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgProfile.CellContentClick

    End Sub

    Private Sub dgProfile_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgProfile.CellClick
        ' Get the current cell location.
        Dim y As Integer = dgProfile.CurrentCellAddress.Y
        Dim x As Integer = dgProfile.CurrentCellAddress.X

        ' Write coordinates to console.
        lblRD.Text = dgProfile.Rows(y).Cells(x).Value
        'find RD in objCurrentAssembly , to get all item's RegEx
        setItemRegEx(lblRD.Text)
    End Sub

    Sub setItemRegEx(rd As String)
        strDateCodeRegEx = ""
        strLotCodeRegEx = ""
        strSupplyCodeRegEx = ""
        strSerialNumberRegEx = ""
        For Each part In objCurrentAssembly
            If part("part")("rd") = rd Then
                If part("part")("pn_type") = "COMPONENT" Then
                    txtPartSerial.Enabled = False
                    lblSerialnumber.Enabled = False
                Else
                    txtPartSerial.Enabled = True
                    lblSerialnumber.Enabled = True
                End If

                '---Clear all text
                txtDatecode.Text = ""
                txtLotcode.Text = ""
                txtSupplycode.Text = ""
                txtPartSerial.Text = ""
                '--set RegEx on all Item
                strDateCodeRegEx = IIf(part("datecode_regexp") Is Nothing, "\w*", part("datecode_regexp"))
                strLotCodeRegEx = IIf(part("lotcode_regexp") Is Nothing, "\w*", part("lotcode_regexp"))
                strSupplyCodeRegEx = IIf(part("supplycode_regexp") Is Nothing, "\w*", part("supplycode_regexp"))
                strSerialNumberRegEx = IIf(part("sn_regexp") Is Nothing, "\w*", part("sn_regexp"))
                '-----------------------
                txtDatecode.Select()
                Exit For
            End If
        Next
    End Sub

    Private Sub txtDatecode_TextChanged(sender As Object, e As EventArgs) Handles txtDatecode.TextChanged
        Dim reg_exp As New Regex("^" & strDateCodeRegEx & "$")
        If reg_exp.IsMatch(txtDatecode.Text) Then
            txtDatecode.BackColor = Color.White
        Else
            txtDatecode.BackColor = Color.Yellow
        End If
    End Sub

    Private Sub txtLotcode_TextChanged(sender As Object, e As EventArgs) Handles txtLotcode.TextChanged
        Dim reg_exp As New Regex("^" & strLotCodeRegEx & "$")
        If reg_exp.IsMatch(txtLotcode.Text) Then
            txtLotcode.BackColor = Color.White
        Else
            txtLotcode.BackColor = Color.Yellow
        End If
    End Sub

    Private Sub txtSupplycode_TextChanged(sender As Object, e As EventArgs) Handles txtSupplycode.TextChanged
        Dim reg_exp As New Regex("^" & strSupplyCodeRegEx & "$")
        If reg_exp.IsMatch(txtSupplycode.Text) Then
            txtSupplycode.BackColor = Color.White
        Else
            txtSupplycode.BackColor = Color.Yellow
        End If
    End Sub

    Private Sub txtPartSerial_TextChanged(sender As Object, e As EventArgs) Handles txtPartSerial.TextChanged
        Dim reg_exp As New Regex("^" & strSerialNumberRegEx & "$")
        If reg_exp.IsMatch(txtPartSerial.Text) Then
            txtPartSerial.BackColor = Color.White
        Else
            txtPartSerial.BackColor = Color.Yellow
        End If
    End Sub


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

    'Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
    '    showObjectName()
    'End Sub

    Public Sub showOpject()
        showObjectName()

    End Sub







    '--------End Mandatory Funtion for all User Controls-------------

End Class