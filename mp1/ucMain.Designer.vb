﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucMain
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.UcTest1 = New mp.ucTest()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.param1 = New mp.ucParaText()
        Me.SuspendLayout()
        '
        'UcTest1
        '
        Me.UcTest1.CurrentForm = Nothing
        Me.UcTest1.Location = New System.Drawing.Point(27, 70)
        Me.UcTest1.Name = "UcTest1"
        Me.UcTest1.ParentObjectName = Nothing
        Me.UcTest1.Size = New System.Drawing.Size(280, 91)
        Me.UcTest1.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(88, 40)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(95, 24)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(88, 14)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(160, 20)
        Me.TextBox1.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(43, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Label1"
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(349, 180)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(27, 23)
        Me.btnShow.TabIndex = 4
        Me.btnShow.Text = "Button2"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'param1
        '
        Me.param1.absolute_url = Nothing
        Me.param1.access_token = Nothing
        Me.param1.AutoSize = True
        Me.param1.BackColor = System.Drawing.SystemColors.Control
        Me.param1.cache_url = Nothing
        Me.param1.CurrentForm = Nothing
        Me.param1.Location = New System.Drawing.Point(18, 167)
        Me.param1.message = Nothing
        Me.param1.Name = "param1"
        Me.param1.parameter = Nothing
        Me.param1.ParentObjectName = Nothing
        Me.param1.regExpress = Nothing
        Me.param1.required = False
        Me.param1.Size = New System.Drawing.Size(315, 26)
        Me.param1.slug = Nothing
        Me.param1.TabIndex = 5
        Me.param1.title = "ABC"
        Me.param1.url = Nothing
        Me.param1.value = Nothing
        '
        'ucMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.param1)
        Me.Controls.Add(Me.btnShow)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.UcTest1)
        Me.Name = "ucMain"
        Me.Size = New System.Drawing.Size(379, 203)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents UcTest1 As ucTest
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnShow As Button
    Friend WithEvents param1 As ucParaText
End Class
