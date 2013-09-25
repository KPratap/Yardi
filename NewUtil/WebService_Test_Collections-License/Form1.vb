Option Explicit On 
Option Strict On

Imports System.IO
Imports System.Net
Imports System.Xml
Imports System.Security.Cryptography.X509Certificates
Imports System.Text
Imports System.Diagnostics.Process

Public Class Form1
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        Dim hklm As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser
        Dim regKey As Microsoft.Win32.RegistryKey = hklm.CreateSubKey("Software\\Collections")

        If (regKey.GetValue("platform", "sql").ToString() = "sql") Then
            rad_SqlServer.Checked = True
        Else
            rad_Oracle.Checked = True
        End If

        LoadSettings(0)
        'Add any initialization after the InitializeComponent() call

        btn_Load1.Text = regKey.GetValue("Load1", "Load 1").ToString()
        btn_Load2.Text = regKey.GetValue("Load2", "Load 2").ToString()
        btn_Load3.Text = regKey.GetValue("Load3", "Load 3").ToString()
        btn_Load4.Text = regKey.GetValue("Load4", "Load 4").ToString()
        btn_Load5.Text = regKey.GetValue("Load5", "Load 5").ToString()
        btn_Load6.Text = regKey.GetValue("Load6", "Load 6").ToString()
        btn_Load7.Text = regKey.GetValue("Load7", "Load 7").ToString()
        btn_Load8.Text = regKey.GetValue("Load8", "Load 8").ToString()
        btn_Load9.Text = regKey.GetValue("Load9", "Load 9").ToString()
        btn_Load10.Text = regKey.GetValue("Load10", "Load 10").ToString()
        btn_Load11.Text = regKey.GetValue("Load11", "Load 11").ToString()
        btn_Load12.Text = regKey.GetValue("Load12", "Load 12").ToString()

        TimeElapsed.Text = "0:00"
        SpinningProgress1.TransistionSegment = 0
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Platform As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txt_Username As System.Windows.Forms.TextBox
    Friend WithEvents txt_Password As System.Windows.Forms.TextBox
    Friend WithEvents txt_DatabaseServer As System.Windows.Forms.TextBox
    Friend WithEvents txt_DatabaseName As System.Windows.Forms.TextBox
    Friend WithEvents txt_InterfaceEntity As System.Windows.Forms.TextBox
    Friend WithEvents txt_Url As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents rad_Oracle As System.Windows.Forms.RadioButton
    Friend WithEvents rad_SqlServer As System.Windows.Forms.RadioButton
    Friend WithEvents txt_Property As System.Windows.Forms.TextBox
    Friend WithEvents txt_TimeOut As System.Windows.Forms.TextBox
    Friend WithEvents lbl_Collections As System.Windows.Forms.Label
    Friend WithEvents txt_MessageBox As System.Windows.Forms.TextBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents Tooltip As System.Windows.Forms.ToolTip
    Friend WithEvents Elapsed As System.Windows.Forms.GroupBox
    Friend WithEvents TimeElapsed As System.Windows.Forms.TextBox
    Friend WithEvents LastRun As System.Windows.Forms.GroupBox
    Friend WithEvents txt_LastRun As System.Windows.Forms.TextBox
    Friend WithEvents SpinningProgress1 As SpinningProgress
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txt_InterfaceLicense As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btn_Load12 As System.Windows.Forms.Button
    Friend WithEvents btn_Load11 As System.Windows.Forms.Button
    Friend WithEvents btn_Load10 As System.Windows.Forms.Button
    Friend WithEvents btn_Load9 As System.Windows.Forms.Button
    Friend WithEvents btn_Load4 As System.Windows.Forms.Button
    Friend WithEvents btn_Load3 As System.Windows.Forms.Button
    Friend WithEvents btn_Load2 As System.Windows.Forms.Button
    Friend WithEvents btn_Load1 As System.Windows.Forms.Button
    Friend WithEvents btn_Load8 As System.Windows.Forms.Button
    Friend WithEvents btn_Load7 As System.Windows.Forms.Button
    Friend WithEvents btn_Load6 As System.Windows.Forms.Button
    Friend WithEvents btn_Load5 As System.Windows.Forms.Button
    Friend WithEvents gbx_Save As System.Windows.Forms.GroupBox
    Friend WithEvents btn_Save11 As System.Windows.Forms.Button
    Friend WithEvents btn_Save12 As System.Windows.Forms.Button
    Friend WithEvents btn_Save9 As System.Windows.Forms.Button
    Friend WithEvents btn_Save10 As System.Windows.Forms.Button
    Friend WithEvents btn_Save3 As System.Windows.Forms.Button
    Friend WithEvents btn_Save4 As System.Windows.Forms.Button
    Friend WithEvents btn_Save2 As System.Windows.Forms.Button
    Friend WithEvents btn_Save1 As System.Windows.Forms.Button
    Friend WithEvents txt_LoadTxt As System.Windows.Forms.TextBox
    Friend WithEvents btn_Save7 As System.Windows.Forms.Button
    Friend WithEvents btn_Save8 As System.Windows.Forms.Button
    Friend WithEvents btn_Save6 As System.Windows.Forms.Button
    Friend WithEvents btn_Save5 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txt_FileName As System.Windows.Forms.TextBox
    Friend WithEvents txt_FolderName As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btn_OpenFolder As System.Windows.Forms.Button
    Friend WithEvents lab_Import As System.Windows.Forms.Label
    Friend WithEvents but_ExportCollections As System.Windows.Forms.Button
    Friend WithEvents but_FileName As System.Windows.Forms.Button
    Friend WithEvents btn_PropertyConfig As System.Windows.Forms.Button
    Friend WithEvents ItfVersion_btn As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents btn_Ping As System.Windows.Forms.Button
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents txt_importFileName As System.Windows.Forms.TextBox
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txt_Username = New System.Windows.Forms.TextBox
        Me.txt_Password = New System.Windows.Forms.TextBox
        Me.txt_DatabaseServer = New System.Windows.Forms.TextBox
        Me.txt_DatabaseName = New System.Windows.Forms.TextBox
        Me.Platform = New System.Windows.Forms.GroupBox
        Me.rad_Oracle = New System.Windows.Forms.RadioButton
        Me.rad_SqlServer = New System.Windows.Forms.RadioButton
        Me.txt_Property = New System.Windows.Forms.TextBox
        Me.txt_InterfaceEntity = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.txt_MessageBox = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txt_Url = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.txt_TimeOut = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.lbl_Collections = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.Tooltip = New System.Windows.Forms.ToolTip(Me.components)
        Me.Elapsed = New System.Windows.Forms.GroupBox
        Me.TimeElapsed = New System.Windows.Forms.TextBox
        Me.LastRun = New System.Windows.Forms.GroupBox
        Me.txt_LastRun = New System.Windows.Forms.TextBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button7 = New System.Windows.Forms.Button
        Me.Label22 = New System.Windows.Forms.Label
        Me.txt_InterfaceLicense = New System.Windows.Forms.TextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btn_Load12 = New System.Windows.Forms.Button
        Me.btn_Load11 = New System.Windows.Forms.Button
        Me.btn_Load10 = New System.Windows.Forms.Button
        Me.btn_Load9 = New System.Windows.Forms.Button
        Me.btn_Load4 = New System.Windows.Forms.Button
        Me.btn_Load3 = New System.Windows.Forms.Button
        Me.btn_Load2 = New System.Windows.Forms.Button
        Me.btn_Load1 = New System.Windows.Forms.Button
        Me.btn_Load8 = New System.Windows.Forms.Button
        Me.btn_Load7 = New System.Windows.Forms.Button
        Me.btn_Load6 = New System.Windows.Forms.Button
        Me.btn_Load5 = New System.Windows.Forms.Button
        Me.gbx_Save = New System.Windows.Forms.GroupBox
        Me.btn_Save11 = New System.Windows.Forms.Button
        Me.btn_Save12 = New System.Windows.Forms.Button
        Me.btn_Save9 = New System.Windows.Forms.Button
        Me.btn_Save10 = New System.Windows.Forms.Button
        Me.btn_Save3 = New System.Windows.Forms.Button
        Me.btn_Save4 = New System.Windows.Forms.Button
        Me.btn_Save2 = New System.Windows.Forms.Button
        Me.btn_Save1 = New System.Windows.Forms.Button
        Me.txt_LoadTxt = New System.Windows.Forms.TextBox
        Me.btn_Save7 = New System.Windows.Forms.Button
        Me.btn_Save8 = New System.Windows.Forms.Button
        Me.btn_Save6 = New System.Windows.Forms.Button
        Me.btn_Save5 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.btn_PropertyConfig = New System.Windows.Forms.Button
        Me.txt_FileName = New System.Windows.Forms.TextBox
        Me.txt_FolderName = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.btn_OpenFolder = New System.Windows.Forms.Button
        Me.lab_Import = New System.Windows.Forms.Label
        Me.but_ExportCollections = New System.Windows.Forms.Button
        Me.but_FileName = New System.Windows.Forms.Button
        Me.ItfVersion_btn = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.Button8 = New System.Windows.Forms.Button
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.Button6 = New System.Windows.Forms.Button
        Me.txt_importFileName = New System.Windows.Forms.TextBox
        Me.Button4 = New System.Windows.Forms.Button
        Me.Label9 = New System.Windows.Forms.Label
        Me.Button5 = New System.Windows.Forms.Button
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.btn_Ping = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.SpinningProgress1 = New SpinningProgress
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.Platform.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Elapsed.SuspendLayout()
        Me.LastRun.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.gbx_Save.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.Label1.Location = New System.Drawing.Point(16, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(112, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Username"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.Label2.Location = New System.Drawing.Point(16, 96)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(112, 20)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Password"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.Label3.Location = New System.Drawing.Point(16, 120)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(112, 20)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Database Server"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.Label4.Location = New System.Drawing.Point(16, 144)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(112, 20)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Database Name"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_Username
        '
        Me.txt_Username.Location = New System.Drawing.Point(120, 72)
        Me.txt_Username.Name = "txt_Username"
        Me.txt_Username.Size = New System.Drawing.Size(152, 20)
        Me.txt_Username.TabIndex = 10
        Me.txt_Username.Text = ""
        '
        'txt_Password
        '
        Me.txt_Password.Location = New System.Drawing.Point(120, 96)
        Me.txt_Password.Name = "txt_Password"
        Me.txt_Password.PasswordChar = Microsoft.VisualBasic.ChrW(42)
        Me.txt_Password.Size = New System.Drawing.Size(152, 20)
        Me.txt_Password.TabIndex = 20
        Me.txt_Password.Text = ""
        '
        'txt_DatabaseServer
        '
        Me.txt_DatabaseServer.Location = New System.Drawing.Point(120, 120)
        Me.txt_DatabaseServer.Name = "txt_DatabaseServer"
        Me.txt_DatabaseServer.Size = New System.Drawing.Size(152, 20)
        Me.txt_DatabaseServer.TabIndex = 30
        Me.txt_DatabaseServer.Text = ""
        '
        'txt_DatabaseName
        '
        Me.txt_DatabaseName.Location = New System.Drawing.Point(120, 144)
        Me.txt_DatabaseName.Name = "txt_DatabaseName"
        Me.txt_DatabaseName.Size = New System.Drawing.Size(152, 20)
        Me.txt_DatabaseName.TabIndex = 40
        Me.txt_DatabaseName.Text = ""
        '
        'Platform
        '
        Me.Platform.Controls.Add(Me.rad_Oracle)
        Me.Platform.Controls.Add(Me.rad_SqlServer)
        Me.Platform.Location = New System.Drawing.Point(80, 248)
        Me.Platform.Name = "Platform"
        Me.Platform.Size = New System.Drawing.Size(152, 68)
        Me.Platform.TabIndex = 50
        Me.Platform.TabStop = False
        Me.Platform.Text = "Platform"
        '
        'rad_Oracle
        '
        Me.rad_Oracle.Location = New System.Drawing.Point(16, 40)
        Me.rad_Oracle.Name = "rad_Oracle"
        Me.rad_Oracle.Size = New System.Drawing.Size(120, 24)
        Me.rad_Oracle.TabIndex = 60
        Me.rad_Oracle.Text = "Oracle"
        '
        'rad_SqlServer
        '
        Me.rad_SqlServer.Location = New System.Drawing.Point(16, 16)
        Me.rad_SqlServer.Name = "rad_SqlServer"
        Me.rad_SqlServer.Size = New System.Drawing.Size(120, 24)
        Me.rad_SqlServer.TabIndex = 55
        Me.rad_SqlServer.Text = "SQL Server"
        '
        'txt_Property
        '
        Me.txt_Property.Location = New System.Drawing.Point(120, 168)
        Me.txt_Property.Name = "txt_Property"
        Me.txt_Property.Size = New System.Drawing.Size(152, 20)
        Me.txt_Property.TabIndex = 70
        Me.txt_Property.Text = ""
        '
        'txt_InterfaceEntity
        '
        Me.txt_InterfaceEntity.Location = New System.Drawing.Point(120, 192)
        Me.txt_InterfaceEntity.Name = "txt_InterfaceEntity"
        Me.txt_InterfaceEntity.Size = New System.Drawing.Size(152, 20)
        Me.txt_InterfaceEntity.TabIndex = 80
        Me.txt_InterfaceEntity.Text = ""
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(16, 168)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(104, 24)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Property Code"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.Label8.Location = New System.Drawing.Point(16, 192)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(104, 24)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "Interface Entity"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_MessageBox
        '
        Me.txt_MessageBox.Location = New System.Drawing.Point(288, 96)
        Me.txt_MessageBox.Multiline = True
        Me.txt_MessageBox.Name = "txt_MessageBox"
        Me.txt_MessageBox.Size = New System.Drawing.Size(296, 112)
        Me.txt_MessageBox.TabIndex = 0
        Me.txt_MessageBox.Text = ""
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(288, 80)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(112, 16)
        Me.Label15.TabIndex = 31
        Me.Label15.Text = "Messages"
        '
        'txt_Url
        '
        Me.txt_Url.Location = New System.Drawing.Point(120, 48)
        Me.txt_Url.Name = "txt_Url"
        Me.txt_Url.Size = New System.Drawing.Size(464, 20)
        Me.txt_Url.TabIndex = 185
        Me.txt_Url.Text = "http://localhost/Voyager60/Webservices/ItfCollections.asmx"
        '
        'Label18
        '
        Me.Label18.Location = New System.Drawing.Point(16, 48)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(104, 24)
        Me.Label18.TabIndex = 186
        Me.Label18.Text = "URL"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_TimeOut
        '
        Me.txt_TimeOut.Location = New System.Drawing.Point(8, 16)
        Me.txt_TimeOut.Name = "txt_TimeOut"
        Me.txt_TimeOut.Size = New System.Drawing.Size(56, 20)
        Me.txt_TimeOut.TabIndex = 193
        Me.txt_TimeOut.Text = "10"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txt_TimeOut)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Location = New System.Drawing.Point(160, 328)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(104, 48)
        Me.GroupBox1.TabIndex = 195
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Timeout"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(72, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(24, 24)
        Me.Label5.TabIndex = 196
        Me.Label5.Text = "sec"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbl_Collections
        '
        Me.lbl_Collections.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, CType(((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic) _
                        Or System.Drawing.FontStyle.Underline), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_Collections.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.lbl_Collections.Location = New System.Drawing.Point(8, 8)
        Me.lbl_Collections.Name = "lbl_Collections"
        Me.lbl_Collections.Size = New System.Drawing.Size(288, 24)
        Me.lbl_Collections.TabIndex = 196
        Me.lbl_Collections.Text = "Collections"
        Me.lbl_Collections.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Timer1
        '
        '
        'Timer2
        '
        '
        'Elapsed
        '
        Me.Elapsed.Controls.Add(Me.TimeElapsed)
        Me.Elapsed.Location = New System.Drawing.Point(416, 0)
        Me.Elapsed.Name = "Elapsed"
        Me.Elapsed.Size = New System.Drawing.Size(104, 48)
        Me.Elapsed.TabIndex = 252
        Me.Elapsed.TabStop = False
        Me.Elapsed.Text = "Time Elapsed"
        '
        'TimeElapsed
        '
        Me.TimeElapsed.Location = New System.Drawing.Point(8, 16)
        Me.TimeElapsed.Name = "TimeElapsed"
        Me.TimeElapsed.Size = New System.Drawing.Size(88, 20)
        Me.TimeElapsed.TabIndex = 195
        Me.TimeElapsed.Text = ""
        Me.TimeElapsed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LastRun
        '
        Me.LastRun.Controls.Add(Me.txt_LastRun)
        Me.LastRun.Location = New System.Drawing.Point(24, 328)
        Me.LastRun.Name = "LastRun"
        Me.LastRun.Size = New System.Drawing.Size(104, 48)
        Me.LastRun.TabIndex = 253
        Me.LastRun.TabStop = False
        Me.LastRun.Text = "Last Run"
        '
        'txt_LastRun
        '
        Me.txt_LastRun.Location = New System.Drawing.Point(8, 16)
        Me.txt_LastRun.Name = "txt_LastRun"
        Me.txt_LastRun.Size = New System.Drawing.Size(88, 20)
        Me.txt_LastRun.TabIndex = 194
        Me.txt_LastRun.Text = ""
        Me.txt_LastRun.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Button2
        '
        Me.Button2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button2.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(518, 216)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(30, 20)
        Me.Button2.TabIndex = 1000000052
        Me.Button2.Text = "---"
        '
        'Button7
        '
        Me.Button7.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button7.Location = New System.Drawing.Point(552, 216)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(40, 20)
        Me.Button7.TabIndex = 1000000053
        Me.Button7.Text = "Open"
        '
        'Label22
        '
        Me.Label22.ImageAlign = System.Drawing.ContentAlignment.BottomRight
        Me.Label22.Location = New System.Drawing.Point(16, 216)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(104, 24)
        Me.Label22.TabIndex = 1000000051
        Me.Label22.Text = "Itf License File"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txt_InterfaceLicense
        '
        Me.txt_InterfaceLicense.Location = New System.Drawing.Point(120, 216)
        Me.txt_InterfaceLicense.Name = "txt_InterfaceLicense"
        Me.txt_InterfaceLicense.Size = New System.Drawing.Size(392, 20)
        Me.txt_InterfaceLicense.TabIndex = 1000000050
        Me.txt_InterfaceLicense.Text = ""
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btn_Load12)
        Me.GroupBox2.Controls.Add(Me.btn_Load11)
        Me.GroupBox2.Controls.Add(Me.btn_Load10)
        Me.GroupBox2.Controls.Add(Me.btn_Load9)
        Me.GroupBox2.Controls.Add(Me.btn_Load4)
        Me.GroupBox2.Controls.Add(Me.btn_Load3)
        Me.GroupBox2.Controls.Add(Me.btn_Load2)
        Me.GroupBox2.Controls.Add(Me.btn_Load1)
        Me.GroupBox2.Controls.Add(Me.btn_Load8)
        Me.GroupBox2.Controls.Add(Me.btn_Load7)
        Me.GroupBox2.Controls.Add(Me.btn_Load6)
        Me.GroupBox2.Controls.Add(Me.btn_Load5)
        Me.GroupBox2.Location = New System.Drawing.Point(400, 264)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(184, 216)
        Me.GroupBox2.TabIndex = 1000000055
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "SQL Load Settings"
        '
        'btn_Load12
        '
        Me.btn_Load12.Location = New System.Drawing.Point(96, 184)
        Me.btn_Load12.Name = "btn_Load12"
        Me.btn_Load12.Size = New System.Drawing.Size(80, 23)
        Me.btn_Load12.TabIndex = 206
        Me.btn_Load12.Text = "Load 12"
        '
        'btn_Load11
        '
        Me.btn_Load11.Location = New System.Drawing.Point(8, 184)
        Me.btn_Load11.Name = "btn_Load11"
        Me.btn_Load11.Size = New System.Drawing.Size(80, 23)
        Me.btn_Load11.TabIndex = 205
        Me.btn_Load11.Text = "Load 11"
        '
        'btn_Load10
        '
        Me.btn_Load10.Location = New System.Drawing.Point(96, 152)
        Me.btn_Load10.Name = "btn_Load10"
        Me.btn_Load10.Size = New System.Drawing.Size(80, 23)
        Me.btn_Load10.TabIndex = 204
        Me.btn_Load10.Text = "Load 10"
        '
        'btn_Load9
        '
        Me.btn_Load9.Location = New System.Drawing.Point(8, 152)
        Me.btn_Load9.Name = "btn_Load9"
        Me.btn_Load9.Size = New System.Drawing.Size(80, 23)
        Me.btn_Load9.TabIndex = 203
        Me.btn_Load9.Text = "Load 9"
        '
        'btn_Load4
        '
        Me.btn_Load4.Location = New System.Drawing.Point(96, 56)
        Me.btn_Load4.Name = "btn_Load4"
        Me.btn_Load4.Size = New System.Drawing.Size(80, 23)
        Me.btn_Load4.TabIndex = 8
        Me.btn_Load4.Text = "Load 4"
        '
        'btn_Load3
        '
        Me.btn_Load3.Location = New System.Drawing.Point(8, 56)
        Me.btn_Load3.Name = "btn_Load3"
        Me.btn_Load3.Size = New System.Drawing.Size(80, 23)
        Me.btn_Load3.TabIndex = 7
        Me.btn_Load3.Text = "Load 3"
        '
        'btn_Load2
        '
        Me.btn_Load2.Location = New System.Drawing.Point(96, 24)
        Me.btn_Load2.Name = "btn_Load2"
        Me.btn_Load2.Size = New System.Drawing.Size(80, 23)
        Me.btn_Load2.TabIndex = 6
        Me.btn_Load2.Text = "Load 2"
        '
        'btn_Load1
        '
        Me.btn_Load1.Location = New System.Drawing.Point(8, 24)
        Me.btn_Load1.Name = "btn_Load1"
        Me.btn_Load1.Size = New System.Drawing.Size(80, 23)
        Me.btn_Load1.TabIndex = 5
        Me.btn_Load1.Text = "Load 1"
        '
        'btn_Load8
        '
        Me.btn_Load8.Location = New System.Drawing.Point(96, 120)
        Me.btn_Load8.Name = "btn_Load8"
        Me.btn_Load8.Size = New System.Drawing.Size(80, 23)
        Me.btn_Load8.TabIndex = 202
        Me.btn_Load8.Text = "Load 8"
        '
        'btn_Load7
        '
        Me.btn_Load7.Location = New System.Drawing.Point(8, 120)
        Me.btn_Load7.Name = "btn_Load7"
        Me.btn_Load7.Size = New System.Drawing.Size(80, 23)
        Me.btn_Load7.TabIndex = 201
        Me.btn_Load7.Text = "Load 7"
        '
        'btn_Load6
        '
        Me.btn_Load6.Location = New System.Drawing.Point(96, 88)
        Me.btn_Load6.Name = "btn_Load6"
        Me.btn_Load6.Size = New System.Drawing.Size(80, 23)
        Me.btn_Load6.TabIndex = 200
        Me.btn_Load6.Text = "Load 6"
        '
        'btn_Load5
        '
        Me.btn_Load5.Location = New System.Drawing.Point(8, 88)
        Me.btn_Load5.Name = "btn_Load5"
        Me.btn_Load5.Size = New System.Drawing.Size(80, 23)
        Me.btn_Load5.TabIndex = 199
        Me.btn_Load5.Text = "Load 5"
        '
        'gbx_Save
        '
        Me.gbx_Save.Controls.Add(Me.btn_Save11)
        Me.gbx_Save.Controls.Add(Me.btn_Save12)
        Me.gbx_Save.Controls.Add(Me.btn_Save9)
        Me.gbx_Save.Controls.Add(Me.btn_Save10)
        Me.gbx_Save.Controls.Add(Me.btn_Save3)
        Me.gbx_Save.Controls.Add(Me.btn_Save4)
        Me.gbx_Save.Controls.Add(Me.btn_Save2)
        Me.gbx_Save.Controls.Add(Me.btn_Save1)
        Me.gbx_Save.Controls.Add(Me.txt_LoadTxt)
        Me.gbx_Save.Controls.Add(Me.btn_Save7)
        Me.gbx_Save.Controls.Add(Me.btn_Save8)
        Me.gbx_Save.Controls.Add(Me.btn_Save6)
        Me.gbx_Save.Controls.Add(Me.btn_Save5)
        Me.gbx_Save.Location = New System.Drawing.Point(288, 240)
        Me.gbx_Save.Name = "gbx_Save"
        Me.gbx_Save.Size = New System.Drawing.Size(104, 240)
        Me.gbx_Save.TabIndex = 1000000054
        Me.gbx_Save.TabStop = False
        Me.gbx_Save.Text = "Save Settings"
        '
        'btn_Save11
        '
        Me.btn_Save11.Location = New System.Drawing.Point(8, 208)
        Me.btn_Save11.Name = "btn_Save11"
        Me.btn_Save11.Size = New System.Drawing.Size(40, 23)
        Me.btn_Save11.TabIndex = 206
        Me.btn_Save11.Text = "11"
        '
        'btn_Save12
        '
        Me.btn_Save12.Location = New System.Drawing.Point(56, 208)
        Me.btn_Save12.Name = "btn_Save12"
        Me.btn_Save12.Size = New System.Drawing.Size(40, 23)
        Me.btn_Save12.TabIndex = 205
        Me.btn_Save12.Text = "12"
        '
        'btn_Save9
        '
        Me.btn_Save9.Location = New System.Drawing.Point(8, 176)
        Me.btn_Save9.Name = "btn_Save9"
        Me.btn_Save9.Size = New System.Drawing.Size(40, 23)
        Me.btn_Save9.TabIndex = 204
        Me.btn_Save9.Text = "9"
        '
        'btn_Save10
        '
        Me.btn_Save10.Location = New System.Drawing.Point(56, 176)
        Me.btn_Save10.Name = "btn_Save10"
        Me.btn_Save10.Size = New System.Drawing.Size(40, 23)
        Me.btn_Save10.TabIndex = 203
        Me.btn_Save10.Text = "10"
        '
        'btn_Save3
        '
        Me.btn_Save3.Location = New System.Drawing.Point(8, 80)
        Me.btn_Save3.Name = "btn_Save3"
        Me.btn_Save3.Size = New System.Drawing.Size(40, 23)
        Me.btn_Save3.TabIndex = 4
        Me.btn_Save3.Text = "3"
        '
        'btn_Save4
        '
        Me.btn_Save4.Location = New System.Drawing.Point(56, 80)
        Me.btn_Save4.Name = "btn_Save4"
        Me.btn_Save4.Size = New System.Drawing.Size(40, 23)
        Me.btn_Save4.TabIndex = 3
        Me.btn_Save4.Text = "4"
        '
        'btn_Save2
        '
        Me.btn_Save2.Location = New System.Drawing.Point(56, 48)
        Me.btn_Save2.Name = "btn_Save2"
        Me.btn_Save2.Size = New System.Drawing.Size(40, 23)
        Me.btn_Save2.TabIndex = 2
        Me.btn_Save2.Text = "2"
        '
        'btn_Save1
        '
        Me.btn_Save1.Location = New System.Drawing.Point(8, 48)
        Me.btn_Save1.Name = "btn_Save1"
        Me.btn_Save1.Size = New System.Drawing.Size(40, 23)
        Me.btn_Save1.TabIndex = 1
        Me.btn_Save1.Text = "1"
        '
        'txt_LoadTxt
        '
        Me.txt_LoadTxt.Location = New System.Drawing.Point(8, 16)
        Me.txt_LoadTxt.Name = "txt_LoadTxt"
        Me.txt_LoadTxt.Size = New System.Drawing.Size(88, 20)
        Me.txt_LoadTxt.TabIndex = 0
        Me.txt_LoadTxt.Text = "Load #"
        '
        'btn_Save7
        '
        Me.btn_Save7.Location = New System.Drawing.Point(8, 144)
        Me.btn_Save7.Name = "btn_Save7"
        Me.btn_Save7.Size = New System.Drawing.Size(40, 23)
        Me.btn_Save7.TabIndex = 202
        Me.btn_Save7.Text = "7"
        '
        'btn_Save8
        '
        Me.btn_Save8.Location = New System.Drawing.Point(56, 144)
        Me.btn_Save8.Name = "btn_Save8"
        Me.btn_Save8.Size = New System.Drawing.Size(40, 23)
        Me.btn_Save8.TabIndex = 201
        Me.btn_Save8.Text = "8"
        '
        'btn_Save6
        '
        Me.btn_Save6.Location = New System.Drawing.Point(56, 112)
        Me.btn_Save6.Name = "btn_Save6"
        Me.btn_Save6.Size = New System.Drawing.Size(40, 23)
        Me.btn_Save6.TabIndex = 200
        Me.btn_Save6.Text = "6"
        '
        'btn_Save5
        '
        Me.btn_Save5.Location = New System.Drawing.Point(8, 112)
        Me.btn_Save5.Name = "btn_Save5"
        Me.btn_Save5.Size = New System.Drawing.Size(40, 23)
        Me.btn_Save5.TabIndex = 199
        Me.btn_Save5.Text = "5"
        '
        'Button1
        '
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.Location = New System.Drawing.Point(520, 16)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(48, 20)
        Me.Button1.TabIndex = 1000000064
        Me.Button1.Text = "Open"
        '
        'btn_PropertyConfig
        '
        Me.btn_PropertyConfig.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_PropertyConfig.Location = New System.Drawing.Point(112, 48)
        Me.btn_PropertyConfig.Name = "btn_PropertyConfig"
        Me.btn_PropertyConfig.Size = New System.Drawing.Size(94, 32)
        Me.btn_PropertyConfig.TabIndex = 1000000063
        Me.btn_PropertyConfig.Text = "Get Property Configuration"
        '
        'txt_FileName
        '
        Me.txt_FileName.Location = New System.Drawing.Point(104, 16)
        Me.txt_FileName.Name = "txt_FileName"
        Me.txt_FileName.Size = New System.Drawing.Size(360, 20)
        Me.txt_FileName.TabIndex = 1000000062
        Me.txt_FileName.Text = ""
        '
        'txt_FolderName
        '
        Me.txt_FolderName.Location = New System.Drawing.Point(104, 16)
        Me.txt_FolderName.Name = "txt_FolderName"
        Me.txt_FolderName.Size = New System.Drawing.Size(360, 20)
        Me.txt_FolderName.TabIndex = 1000000057
        Me.txt_FolderName.Text = ""
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(8, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 24)
        Me.Label6.TabIndex = 1000000061
        Me.Label6.Text = "Output File"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btn_OpenFolder
        '
        Me.btn_OpenFolder.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_OpenFolder.Location = New System.Drawing.Point(472, 56)
        Me.btn_OpenFolder.Name = "btn_OpenFolder"
        Me.btn_OpenFolder.Size = New System.Drawing.Size(88, 20)
        Me.btn_OpenFolder.TabIndex = 1000000060
        Me.btn_OpenFolder.Text = "Open Folder"
        '
        'lab_Import
        '
        Me.lab_Import.Location = New System.Drawing.Point(8, 16)
        Me.lab_Import.Name = "lab_Import"
        Me.lab_Import.Size = New System.Drawing.Size(84, 24)
        Me.lab_Import.TabIndex = 1000000056
        Me.lab_Import.Text = "Export File"
        Me.lab_Import.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'but_ExportCollections
        '
        Me.but_ExportCollections.Cursor = System.Windows.Forms.Cursors.Hand
        Me.but_ExportCollections.Location = New System.Drawing.Point(112, 48)
        Me.but_ExportCollections.Name = "but_ExportCollections"
        Me.but_ExportCollections.Size = New System.Drawing.Size(94, 32)
        Me.but_ExportCollections.TabIndex = 1000000058
        Me.but_ExportCollections.Text = "Get Collections"
        '
        'but_FileName
        '
        Me.but_FileName.Cursor = System.Windows.Forms.Cursors.Hand
        Me.but_FileName.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.but_FileName.Location = New System.Drawing.Point(480, 16)
        Me.but_FileName.Name = "but_FileName"
        Me.but_FileName.Size = New System.Drawing.Size(30, 20)
        Me.but_FileName.TabIndex = 1000000059
        Me.but_FileName.Text = "---"
        '
        'ItfVersion_btn
        '
        Me.ItfVersion_btn.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ItfVersion_btn.Location = New System.Drawing.Point(232, 48)
        Me.ItfVersion_btn.Name = "ItfVersion_btn"
        Me.ItfVersion_btn.Size = New System.Drawing.Size(94, 32)
        Me.ItfVersion_btn.TabIndex = 1000000065
        Me.ItfVersion_btn.Text = "Version Number"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(8, 480)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(584, 128)
        Me.TabControl1.TabIndex = 1000000066
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.CheckBox1)
        Me.TabPage1.Controls.Add(Me.Button8)
        Me.TabPage1.Controls.Add(Me.txt_FolderName)
        Me.TabPage1.Controls.Add(Me.btn_OpenFolder)
        Me.TabPage1.Controls.Add(Me.lab_Import)
        Me.TabPage1.Controls.Add(Me.but_ExportCollections)
        Me.TabPage1.Controls.Add(Me.but_FileName)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(576, 102)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Export"
        '
        'Button8
        '
        Me.Button8.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button8.Location = New System.Drawing.Point(520, 16)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(48, 20)
        Me.Button8.TabIndex = 1000000061
        Me.Button8.Text = "Open"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Button6)
        Me.TabPage3.Controls.Add(Me.txt_importFileName)
        Me.TabPage3.Controls.Add(Me.Button4)
        Me.TabPage3.Controls.Add(Me.Label9)
        Me.TabPage3.Controls.Add(Me.Button5)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(576, 102)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Import"
        '
        'Button6
        '
        Me.Button6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button6.Location = New System.Drawing.Point(112, 48)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(94, 32)
        Me.Button6.TabIndex = 1000000065
        Me.Button6.Text = "Import Collections"
        '
        'txt_importFileName
        '
        Me.txt_importFileName.Location = New System.Drawing.Point(104, 16)
        Me.txt_importFileName.Name = "txt_importFileName"
        Me.txt_importFileName.Size = New System.Drawing.Size(360, 20)
        Me.txt_importFileName.TabIndex = 1000000062
        Me.txt_importFileName.Text = ""
        '
        'Button4
        '
        Me.Button4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button4.Location = New System.Drawing.Point(520, 16)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(48, 20)
        Me.Button4.TabIndex = 1000000064
        Me.Button4.Text = "Open"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(8, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(84, 24)
        Me.Label9.TabIndex = 1000000061
        Me.Label9.Text = "Import File"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Button5
        '
        Me.Button5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button5.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.Location = New System.Drawing.Point(480, 16)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(30, 20)
        Me.Button5.TabIndex = 1000000063
        Me.Button5.Text = "---"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.btn_Ping)
        Me.TabPage2.Controls.Add(Me.Button3)
        Me.TabPage2.Controls.Add(Me.Button1)
        Me.TabPage2.Controls.Add(Me.btn_PropertyConfig)
        Me.TabPage2.Controls.Add(Me.txt_FileName)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.ItfVersion_btn)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(576, 102)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Common"
        '
        'btn_Ping
        '
        Me.btn_Ping.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btn_Ping.Location = New System.Drawing.Point(352, 48)
        Me.btn_Ping.Name = "btn_Ping"
        Me.btn_Ping.Size = New System.Drawing.Size(94, 32)
        Me.btn_Ping.TabIndex = 1000000067
        Me.btn_Ping.Text = "Ping"
        '
        'Button3
        '
        Me.Button3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button3.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(480, 16)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(30, 20)
        Me.Button3.TabIndex = 1000000066
        Me.Button3.Text = "---"
        '
        'SpinningProgress1
        '
        Me.SpinningProgress1.Location = New System.Drawing.Point(544, 8)
        Me.SpinningProgress1.Name = "SpinningProgress1"
        Me.SpinningProgress1.Size = New System.Drawing.Size(35, 35)
        Me.SpinningProgress1.TabIndex = 0
        Me.SpinningProgress1.TransistionSegment = 0
        '
        'CheckBox1
        '
        Me.CheckBox1.BackColor = System.Drawing.SystemColors.Control
        Me.CheckBox1.Location = New System.Drawing.Point(232, 48)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(104, 32)
        Me.CheckBox1.TabIndex = 1000000062
        Me.CheckBox1.Text = "View Only"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(600, 614)
        Me.Controls.Add(Me.SpinningProgress1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.SpinningProgress1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.gbx_Save)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.txt_InterfaceLicense)
        Me.Controls.Add(Me.LastRun)
        Me.Controls.Add(Me.Elapsed)
        Me.Controls.Add(Me.lbl_Collections)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.txt_Url)
        Me.Controls.Add(Me.txt_MessageBox)
        Me.Controls.Add(Me.txt_InterfaceEntity)
        Me.Controls.Add(Me.txt_Property)
        Me.Controls.Add(Me.txt_DatabaseName)
        Me.Controls.Add(Me.txt_DatabaseServer)
        Me.Controls.Add(Me.txt_Password)
        Me.Controls.Add(Me.txt_Username)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Platform)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.Text = "Collections Utility"
        Me.Platform.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.Elapsed.ResumeLayout(False)
        Me.LastRun.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.gbx_Save.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private myws As Collections.ItfCollections
    Private str_DBConneciton As String
    Private DatabasePlatform As String
    Private License As String

    ReadOnly Property ws() As Collections.ItfCollections
        Get
            If myws Is Nothing Then

                myws = New Collections.ItfCollections
                myws.CookieContainer = New CookieContainer
                ServicePointManager.CertificatePolicy = New CertificateValidation
            End If
            Return myws
        End Get
    End Property

#Region " Form Functions "

    Private Sub SubstituteUrl()
        ws.Url = txt_Url.Text
    End Sub

#End Region

    Public Class CertificateValidation
        Implements ICertificatePolicy

        Public Function CheckValidationResult(ByVal srvPoint As ServicePoint, _
        ByVal cert As X509Certificate, ByVal request As WebRequest, ByVal problem As Integer) _
           As Boolean Implements ICertificatePolicy.CheckValidationResult
            Return True
        End Function

    End Class

#Region "SaveLoad Functions"

    Private Sub SaveData(ByVal SaveLocation As Int32)
        Dim msg As String
        Dim title As String
        Dim style As MsgBoxStyle
        Dim response As MsgBoxResult
        msg = "Do you want to save these new settings?"
        style = MsgBoxStyle.YesNo
        title = "Save Settings?"

        response = MsgBox(msg, style, title)

        If response = MsgBoxResult.Yes Then
            LabelChange(SaveLocation)
            Dim hklm As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser
            Dim regKey As Microsoft.Win32.RegistryKey = hklm.CreateSubKey("Software\\Collections" & SaveLocation.ToString)

            If rad_SqlServer.Checked Then
                regKey.SetValue("URL", txt_Url.Text)
                regKey.SetValue("UserName", txt_Username.Text)
                regKey.SetValue("Password", txt_Password.Text)
                regKey.SetValue("dbServer", txt_DatabaseServer.Text)
                regKey.SetValue("dbName", txt_DatabaseName.Text)
                regKey.SetValue("exportFile", txt_FileName.Text)
                regKey.SetValue("Property", txt_Property.Text)
                regKey.SetValue("Vendor", txt_InterfaceEntity.Text)
                regKey.SetValue("platform", "sql")
                regKey.SetValue("ImportFile", txt_ImportFileName.Text)
                regKey.SetValue("txt_TimeOut", txt_TimeOut.Text)
                regKey.SetValue("InterfaceLicense", txt_InterfaceLicense.Text)
                regKey.SetValue("FolderName", txt_FolderName.Text)
                regKey.SetValue("ViewOnly", CheckBox1.Checked)
            Else
                regKey.SetValue("URL_O", txt_Url.Text)
                regKey.SetValue("UserName_O", txt_Username.Text)
                regKey.SetValue("Password_O", txt_Password.Text)
                regKey.SetValue("dbServer_O", txt_DatabaseServer.Text)
                regKey.SetValue("dbName_O", txt_DatabaseName.Text)
                regKey.SetValue("exportFile_O", txt_FileName.Text)
                regKey.SetValue("Property_O", txt_Property.Text)
                regKey.SetValue("Vendor_O", txt_InterfaceEntity.Text)
                regKey.SetValue("platform", "oracle")
                regKey.SetValue("ImportFile_O", txt_ImportFileName.Text)
                regKey.SetValue("txt_TimeOut_O", txt_TimeOut.Text)
                regKey.SetValue("InterfaceLicense_O", txt_InterfaceLicense.Text)
                regKey.SetValue("FolderName_O", txt_FolderName.Text)
                regKey.SetValue("ViewOnly_O", CheckBox1.Checked)
            End If
        End If
    End Sub

    Private Sub LoadSettings(ByVal SaveLocation As Int32)
        Dim msg As String
        Dim title As String
        Dim style As MsgBoxStyle
        Dim response As MsgBoxResult
        If SaveLocation <> 0 Then
            msg = "Do you want to load previous settings?"
            style = MsgBoxStyle.YesNo
            title = "Load Settings?"

            response = MsgBox(msg, style, title)
        Else
            response = MsgBoxResult.Yes
            SaveLocation = 1
        End If

        If response = MsgBoxResult.Yes Then
            Dim hklm As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser
            Dim regKey As Microsoft.Win32.RegistryKey = hklm.CreateSubKey("Software\\Collections" & SaveLocation.ToString)

            If rad_SQLServer.Checked Then
                txt_Url.Text = regKey.GetValue("URL", "https://www.iyardiasp.com/8223thirddev/webservices/itfcollections.asmx").ToString()
                txt_Username.Text = regKey.GetValue("UserName", "UserName").ToString()
                txt_Password.Text = regKey.GetValue("Password", "Password").ToString()
                txt_DatabaseServer.Text = regKey.GetValue("dbServer", "Server").ToString()
                txt_DatabaseName.Text = regKey.GetValue("dbName", "DatabaseName").ToString()
                txt_FileName.Text = regKey.GetValue("exportFile", "c:\Output.xml").ToString()
                txt_Property.Text = regKey.GetValue("Property", "Property").ToString()
                txt_InterfaceEntity.Text = regKey.GetValue("Vendor", "Vendor").ToString()
                txt_ImportFileName.Text = regKey.GetValue("ImportFile", "c:\Import.xml").ToString()
                txt_TimeOut.Text = regKey.GetValue("txt_TimeOut", "600").ToString()
                txt_InterfaceLicense.Text = regKey.GetValue("InterfaceLicense", "").ToString()
                txt_FolderName.Text = regKey.GetValue("FolderName", "").ToString()
                CheckBox1.Checked = Convert.ToBoolean(regKey.GetValue("ViewOnly", False))
            Else
                txt_Url.Text = regKey.GetValue("URL_O", "http://localhost/Voyager60/Webservices/ItfCollections.asmx").ToString()
                txt_Username.Text = regKey.GetValue("UserName_O", "UserName").ToString()
                txt_Password.Text = regKey.GetValue("Password_O", "Password").ToString()
                txt_DatabaseServer.Text = regKey.GetValue("dbServer_O", "Server").ToString()
                txt_DatabaseName.Text = regKey.GetValue("dbName_O", "DatabaseName").ToString()
                txt_FileName.Text = regKey.GetValue("exportFile_O", "c:\Output.xml").ToString()
                txt_Property.Text = regKey.GetValue("Property_O", "Property").ToString()
                txt_InterfaceEntity.Text = regKey.GetValue("Vendor_O", "Vendor").ToString()
                txt_ImportFileName.Text = regKey.GetValue("ImportFile_O", "c:\Import.xml").ToString()
                txt_TimeOut.Text = regKey.GetValue("txt_TimeOut_O", "600").ToString()
                txt_InterfaceLicense.Text = regKey.GetValue("InterfaceLicense_O", "").ToString()
                txt_FolderName.Text = regKey.GetValue("FolderName_O", "").ToString()
                CheckBox1.Checked = Convert.ToBoolean(regKey.GetValue("ViewOnly_O", False))
            End If

            hklm = Microsoft.Win32.Registry.CurrentUser
            regKey = hklm.CreateSubKey("Software\\Collections")
            txt_LoadTxt.Text = regKey.GetValue("Load" & SaveLocation.ToString, "Load " & SaveLocation.ToString).ToString()
        End If


    End Sub

    Private Sub LabelChange(ByVal SaveLocation As Int32)
        Dim hklm As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser
        Dim regKey As Microsoft.Win32.RegistryKey = hklm.CreateSubKey("Software\\Collections")

        If rad_SqlServer.Checked Then
            If txt_LoadTxt.Text = "Load #" Then
                regKey.SetValue("Load" & SaveLocation.ToString, "Load " & SaveLocation.ToString)
            Else
                regKey.SetValue("Load" & SaveLocation.ToString, txt_LoadTxt.Text)
            End If
        Else
            If txt_LoadTxt.Text = "Load #" Then
                regKey.SetValue("Load" & SaveLocation.ToString & "_O", "Load " & SaveLocation.ToString)
            Else
                regKey.SetValue("Load" & SaveLocation.ToString & "_O", txt_LoadTxt.Text)
            End If
        End If

        If rad_SqlServer.Checked Then
            Select Case SaveLocation
                Case 1
                    btn_Load1.Text = regKey.GetValue("Load" & SaveLocation.ToString, "Load 1").ToString()
                Case 2
                    btn_Load2.Text = regKey.GetValue("Load" & SaveLocation.ToString, "Load 2").ToString()
                Case 3
                    btn_Load3.Text = regKey.GetValue("Load" & SaveLocation.ToString, "Load 3").ToString()
                Case 4
                    btn_Load4.Text = regKey.GetValue("Load" & SaveLocation.ToString, "Load 4").ToString()
                Case 5
                    btn_Load5.Text = regKey.GetValue("Load" & SaveLocation.ToString, "Load 5").ToString()
                Case 6
                    btn_Load6.Text = regKey.GetValue("Load" & SaveLocation.ToString, "Load 6").ToString()
                Case 7
                    btn_Load7.Text = regKey.GetValue("Load" & SaveLocation.ToString, "Load 7").ToString()
                Case 8
                    btn_Load8.Text = regKey.GetValue("Load" & SaveLocation.ToString, "Load 8").ToString()
                Case 9
                    btn_Load9.Text = regKey.GetValue("Load" & SaveLocation.ToString, "Load 9").ToString()
                Case 10
                    btn_Load10.Text = regKey.GetValue("Load" & SaveLocation.ToString, "Load 10").ToString()
                Case 11
                    btn_Load11.Text = regKey.GetValue("Load" & SaveLocation.ToString, "Load 11").ToString()
                Case 12
                    btn_Load12.Text = regKey.GetValue("Load" & SaveLocation.ToString, "Load 12").ToString()
            End Select
        Else
            Select Case SaveLocation
                Case 1
                    btn_Load1.Text = regKey.GetValue("Load" & SaveLocation.ToString & "_O", "Load 1").ToString()
                Case 2
                    btn_Load2.Text = regKey.GetValue("Load" & SaveLocation.ToString & "_O", "Load 2").ToString()
                Case 3
                    btn_Load3.Text = regKey.GetValue("Load" & SaveLocation.ToString & "_O", "Load 3").ToString()
                Case 4
                    btn_Load4.Text = regKey.GetValue("Load" & SaveLocation.ToString & "_O", "Load 4").ToString()
                Case 5
                    btn_Load5.Text = regKey.GetValue("Load" & SaveLocation.ToString & "_O", "Load 5").ToString()
                Case 6
                    btn_Load6.Text = regKey.GetValue("Load" & SaveLocation.ToString & "_O", "Load 6").ToString()
                Case 7
                    btn_Load7.Text = regKey.GetValue("Load" & SaveLocation.ToString & "_O", "Load 7").ToString()
                Case 8
                    btn_Load8.Text = regKey.GetValue("Load" & SaveLocation.ToString & "_O", "Load 8").ToString()
                Case 9
                    btn_Load9.Text = regKey.GetValue("Load" & SaveLocation.ToString & "_O", "Load 9").ToString()
                Case 10
                    btn_Load10.Text = regKey.GetValue("Load" & SaveLocation.ToString & "_O", "Load 10").ToString()
                Case 11
                    btn_Load11.Text = regKey.GetValue("Load" & SaveLocation.ToString & "_O", "Load 11").ToString()
                Case 12
                    btn_Load12.Text = regKey.GetValue("Load" & SaveLocation.ToString & "_O", "Load 12").ToString()
            End Select
        End If

        Counter = 0
        Timer1 = New Timer
        Timer1.Interval = 150

        Counter = 0
        Timer2 = New Timer
        Timer2.Interval = 150
    End Sub

    Private Sub but_SaveSettings1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Save1.Click
        SaveData(1)
    End Sub

    Private Sub but_SaveSettings2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Save2.Click
        SaveData(2)
    End Sub

    Private Sub but_SaveSettings3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Save3.Click
        SaveData(3)
    End Sub

    Private Sub but_SaveSettings4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Save4.Click
        SaveData(4)
    End Sub

    Private Sub but_SaveSettings5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Save5.Click
        SaveData(5)
    End Sub

    Private Sub but_SaveSettings6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Save6.Click
        SaveData(6)
    End Sub

    Private Sub but_SaveSettings7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Save7.Click
        SaveData(7)
    End Sub

    Private Sub but_SaveSettings8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Save8.Click
        SaveData(8)
    End Sub

    Private Sub but_SaveSettings9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Save9.Click
        SaveData(9)
    End Sub

    Private Sub but_SaveSettings10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Save10.Click
        SaveData(10)
    End Sub

    Private Sub but_SaveSettings11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Save11.Click
        SaveData(11)
    End Sub

    Private Sub but_SaveSettings12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Save12.Click
        SaveData(12)
    End Sub

    Private Sub LoadSettings(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Load1.Click
        LoadSettings(1)
    End Sub

    Private Sub LoadSettings2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Load2.Click
        LoadSettings(2)
    End Sub

    Private Sub LoadSettings3(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Load3.Click
        LoadSettings(3)
    End Sub

    Private Sub LoadSettings4(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Load4.Click
        LoadSettings(4)
    End Sub

    Private Sub LoadSettings5(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Load5.Click
        LoadSettings(5)
    End Sub

    Private Sub LoadSettings6(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Load6.Click
        LoadSettings(6)
    End Sub

    Private Sub LoadSettings7(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Load7.Click
        LoadSettings(7)
    End Sub

    Private Sub LoadSettings8(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Load8.Click
        LoadSettings(8)
    End Sub

    Private Sub LoadSettings9(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Load9.Click
        LoadSettings(9)
    End Sub

    Private Sub LoadSettings10(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Load10.Click
        LoadSettings(10)
    End Sub

    Private Sub LoadSettings11(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Load11.Click
        LoadSettings(11)
    End Sub

    Private Sub LoadSettings12(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Load12.Click
        LoadSettings(12)
    End Sub

    Private Sub rad_Oracle_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rad_Oracle.CheckedChanged
        Dim hklm As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser
        Dim regKey As Microsoft.Win32.RegistryKey = hklm.CreateSubKey("Software\\Collections")

        If rad_Oracle.Checked Then
            btn_Load1.Text = regKey.GetValue("Load1_O", "Load 1").ToString()
            btn_Load2.Text = regKey.GetValue("Load2_O", "Load 2").ToString()
            btn_Load3.Text = regKey.GetValue("Load3_O", "Load 3").ToString()
            btn_Load4.Text = regKey.GetValue("Load4_O", "Load 4").ToString()
            btn_Load5.Text = regKey.GetValue("Load5_O", "Load 5").ToString()
            btn_Load6.Text = regKey.GetValue("Load6_O", "Load 6").ToString()
            GroupBox2.Text = "Oracle Load Settings"
        End If
    End Sub

    Private Sub rad_SQLServer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rad_SqlServer.CheckedChanged
        Dim hklm As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser
        Dim regKey As Microsoft.Win32.RegistryKey = hklm.CreateSubKey("Software\\Collections")

        If rad_SqlServer.Checked Then
            btn_Load1.Text = regKey.GetValue("Load1", "Load 1").ToString()
            btn_Load2.Text = regKey.GetValue("Load2", "Load 2").ToString()
            btn_Load3.Text = regKey.GetValue("Load3", "Load 3").ToString()
            btn_Load4.Text = regKey.GetValue("Load4", "Load 4").ToString()
            btn_Load5.Text = regKey.GetValue("Load5", "Load 5").ToString()
            btn_Load6.Text = regKey.GetValue("Load6", "Load 6").ToString()
            GroupBox2.Text = "SQL Load Settings"
        End If
    End Sub

#End Region

#Region "File/Folder Functions"
    Private Sub btn_OpenFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_OpenFolder.Click
        Dim FolderName As String
        FolderName = txt_FolderName.Text.Substring(0, txt_FolderName.Text.LastIndexOf("\"))
        'FolderName = txt_FolderName.Text
        Process.Start("explorer.exe", FolderName)
    End Sub

    Private Sub txt_OpenFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim myOutputFile As New FileInfo(txt_FileName.Text)
        If myOutputFile.Exists Then
            Process.Start(txt_FileName.Text)
        End If
    End Sub

    Private Sub btn_LicenseOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim myInputFile As New FileInfo(txt_InterfaceLicense.Text)
        If myInputFile.Exists Then
            Process.Start(txt_InterfaceLicense.Text)
        End If
    End Sub

    Private Sub btn_LicenseBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName().Length > 0 Then
            txt_InterfaceLicense.Text = OpenFileDialog1.FileName()
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim myOutputFile As New FileInfo(txt_FileName.Text)
        If myOutputFile.Exists Then
            Start(txt_FileName.Text)
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName().Length > 0 Then
            txt_FileName.Text = OpenFileDialog1.FileName()
        End If
    End Sub

    Private Sub but_FileName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles but_FileName.Click
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName().Length > 0 Then
            txt_FolderName.Text = OpenFileDialog1.FileName()
        End If

    End Sub

    Private Sub importBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName().Length > 0 Then
            txt_importFileName.Text = OpenFileDialog1.FileName()
        End If
    End Sub

    Private Sub importOpen(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim myOutputFile As New FileInfo(txt_FileName.Text)
        If myOutputFile.Exists Then
            Start(txt_importFileName.Text)
        End If
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim myOutputFile As New FileInfo(txt_FolderName.Text)
        If myOutputFile.Exists Then
            Start(txt_FolderName.Text)
        End If
    End Sub
#End Region

#Region "Export Collections"
    Private Sub but_ExportCollections_Hover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles but_ExportCollections.MouseHover
        If Not CheckBox1.Checked Then
            Tooltip.SetToolTip(but_ExportCollections, "Get_CollectionsLeaseInfo")
        Else
            Tooltip.SetToolTip(but_ExportCollections, "Get_CollectionsLeaseInfo_View")
        End If
    End Sub

    Private Sub but_ExportCollections_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles but_ExportCollections.Click
        ws.Timeout = CInt(txt_TimeOut.Text) * 1000
        WheelThread = New System.Threading.Thread(AddressOf Tick4)
        TimerThread = New System.Threading.Thread(AddressOf Tick5)
        SpinningProgress1.StartWheel()
        WheelThread.Start()
        TimerThread.Start()

        Dim XmlNodeResponse As XmlNode
        Dim XmlDocument As XmlDocument
        Dim readyforattempt As Boolean = True
        Dim FinalMessage As New StringBuilder

        txt_MessageBox.Clear()
        License = GetLicense(txt_InterfaceLicense.Text)

        If txt_Url.Text.Length < 1 Then
            txt_MessageBox.Text = txt_MessageBox.Text & "Missing Web Service URL "
            readyforattempt = False
        End If
        If txt_Username.Text.Length < 1 Then
            txt_MessageBox.Text = txt_MessageBox.Text & "Missing Username. "
            readyforattempt = False
        End If
        If txt_Password.Text.Length < 1 Then
            txt_MessageBox.Text = txt_MessageBox.Text & "Missing Password. "
            readyforattempt = False
        End If
        If txt_DatabaseServer.Text.Length < 1 Then
            txt_MessageBox.Text = txt_MessageBox.Text & "Missing Server Name. "
            readyforattempt = False
        End If
        If txt_DatabaseName.Text.Length < 1 Then
            txt_MessageBox.Text = txt_MessageBox.Text & "Missing Database Name. "
            readyforattempt = False
        End If

        If rad_SqlServer.Checked Then
            DatabasePlatform = "Sql Server"
        Else : DatabasePlatform = "Oracle"
        End If

        If txt_InterfaceEntity.Text.Length < 1 Then
            txt_MessageBox.Text = txt_MessageBox.Text & "Missing Interface Entity. "
            readyforattempt = False
        End If
        If txt_FolderName.Text.Length < 1 Then
            txt_MessageBox.Text = txt_MessageBox.Text & "Missing Output path. "
            readyforattempt = False
        End If
        If License Is Nothing Then
            txt_MessageBox.Text = txt_MessageBox.Text & "License File Does Not Exist. "
            readyforattempt = False
        End If

        If readyforattempt Then
            SubstituteUrl()
            ws.Timeout = CType(txt_TimeOut.Text, Int32) * 60000

            Try
                If Not CheckBox1.Checked Then
                    XmlNodeResponse = ws.Get_CollectionsLeaseInfo( _
                        txt_Username.Text, _
                        txt_Password.Text, _
                        txt_DatabaseServer.Text, _
                        txt_DatabaseName.Text, _
                        DatabasePlatform, _
                        txt_InterfaceEntity.Text, _
                        License, _
                        txt_Property.Text)
                Else
                    XmlNodeResponse = ws.Get_CollectionsLeaseInfo_View( _
                    txt_Username.Text, _
                    txt_Password.Text, _
                    txt_DatabaseServer.Text, _
                    txt_DatabaseName.Text, _
                    DatabasePlatform, _
                    txt_InterfaceEntity.Text, _
                    License, _
                    txt_Property.Text)
                End If

                If XmlNodeResponse Is Nothing Then
                    txt_MessageBox.Text = "Export Collections Info failed! "
                Else
                    Try
                        XmlDocument = New XmlDocument
                        XmlDocument.LoadXml(XmlNodeResponse.OuterXml)
                        XmlDocument.Save(txt_FolderName.Text)
                        txt_MessageBox.Text = "Export Collections Info: successfully received response; check output file."
                        Timer1.Start()
                    Catch ex As Exception
                        txt_MessageBox.Text = "Create output file failed! " & Chr(13) & Chr(10)
                        txt_MessageBox.Text += ex.Message & Chr(13) & Chr(10)
                        Abort_Stop_Refresh()
                    End Try
                End If
            Catch ex As Exception
                txt_MessageBox.Text = ex.Message & Chr(13) & Chr(10)
                txt_MessageBox.Text += ex.StackTrace
                Abort_Stop_Refresh()
            End Try
        End If
        'If txt_MessageBox.Text.Trim.Length = 0 Then
        '    txt_MessageBox.Text = FinalMessage.ToString
        'End If
        Abort_Stop_Refresh()
    End Sub

#End Region

#Region "Import Collections"
    Private Sub ImportCollections_Hover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.MouseHover
        Tooltip.SetToolTip(Button6, "Import_CollectionsInfo")
    End Sub

    Private Sub ImportCollections_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            ws.Timeout = CInt(txt_TimeOut.Text) * 1000
            WheelThread = New System.Threading.Thread(AddressOf Tick4)
            TimerThread = New System.Threading.Thread(AddressOf Tick5)
            SpinningProgress1.StartWheel()
            WheelThread.Start()
            TimerThread.Start()

            Dim XmlNodeResponse As XmlNode
            Dim XmlDocument As XmlDocument
            Dim myXmlRequest As New Xml.XmlDocument
            Dim readyforattempt As Boolean = True

            txt_MessageBox.Clear()
            License = GetLicense(txt_InterfaceLicense.Text)

            If txt_Url.Text.Length < 1 Then
                txt_MessageBox.Text = txt_MessageBox.Text & "Missing Web Service URL "
                readyforattempt = False
            End If
            If Not txt_Url.Text.ToLower.EndsWith("/webservices/itfcollections.asmx") Then
                txt_MessageBox.Text = txt_MessageBox.Text & "Invalid URL. Must end with '/webservices/itfcollections.asmx'. "
                readyforattempt = False
            End If
            If txt_Username.Text.Length < 1 Then
                txt_MessageBox.Text = txt_MessageBox.Text & "Missing Username. "
                readyforattempt = False
            End If
            If txt_Password.Text.Length < 1 Then
                txt_MessageBox.Text = txt_MessageBox.Text & "Missing Password. "
                readyforattempt = False
            End If
            If txt_DatabaseServer.Text.Length < 1 Then
                txt_MessageBox.Text = txt_MessageBox.Text & "Missing Server Name. "
                readyforattempt = False
            End If
            If txt_DatabaseName.Text.Length < 1 Then
                txt_MessageBox.Text = txt_MessageBox.Text & "Missing Database Name. "
                readyforattempt = False
            End If

            If rad_SqlServer.Checked Then
                DatabasePlatform = "Sql Server"
            Else : DatabasePlatform = "Oracle"
            End If

            If txt_InterfaceEntity.Text.Length < 1 Then
                txt_MessageBox.Text = txt_MessageBox.Text & "Missing Interface Entity. "
                readyforattempt = False
            End If
            If txt_importFileName.Text.Length < 1 Then
                txt_MessageBox.Text = txt_MessageBox.Text & "Missing import file. "
                readyforattempt = False
            End If

            If License Is Nothing Then
                txt_MessageBox.Text = txt_MessageBox.Text & "License File Does Not Exist. "
                readyforattempt = False
            End If

            If readyforattempt Then
                SubstituteUrl()

                Try
                    myXmlRequest.Load(txt_importFileName.Text)

                    XmlNodeResponse = ws.Import_CollectionsInfo( _
                        txt_Username.Text, _
                        txt_Password.Text, _
                        txt_DatabaseServer.Text, _
                        txt_DatabaseName.Text, _
                        DatabasePlatform, _
                        txt_InterfaceEntity.Text, _
                        License, _
                        myXmlRequest)

                    If XmlNodeResponse Is Nothing Then
                        txt_MessageBox.Text = "Import Collections failed! "
                    Else
                        Try
                            'XmlDocument = New XmlDocument
                            'XmlDocument.LoadXml(XmlNodeResponse.OuterXml)
                            'XmlDocument.Save(txt_FileName.Text)
                            txt_MessageBox.Text = "Import Collections Successful!"
                            'Timer2.Start()
                        Catch ex As Exception
                            txt_MessageBox.Text = "Import Collections failed!" & Chr(13) & Chr(10)
                            txt_MessageBox.Text += ex.Message & Chr(13) & Chr(10)
                            Abort_Stop_Refresh()
                        End Try
                    End If
                Catch ex As Exception
                    txt_MessageBox.Text = ex.Message & Chr(13) & Chr(10)
                    txt_MessageBox.Text += ex.StackTrace
                    Abort_Stop_Refresh()
                End Try
            End If
        Catch ex As Exception
            txt_MessageBox.Text = ex.Message
            Abort_Stop_Refresh()
        End Try
        Abort_Stop_Refresh()
    End Sub
#End Region

#Region "Common Web Methods"
    Private Sub btn_PropertyConfig_Hover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_PropertyConfig.MouseHover
        Tooltip.SetToolTip(btn_PropertyConfig, "GetPropertyConfigurations")
    End Sub

    Private Sub btn_PropertyConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_PropertyConfig.Click
        Try
            ws.Timeout = CInt(txt_TimeOut.Text) * 1000
            WheelThread = New System.Threading.Thread(AddressOf Tick4)
            TimerThread = New System.Threading.Thread(AddressOf Tick5)
            SpinningProgress1.StartWheel()
            WheelThread.Start()
            TimerThread.Start()

            Dim XmlNodeResponse As XmlNode
            Dim XmlDocument As XmlDocument
            Dim readyforattempt As Boolean = True

            txt_MessageBox.Clear()
            License = GetLicense(txt_InterfaceLicense.Text)

            If txt_Url.Text.Length < 1 Then
                txt_MessageBox.Text = txt_MessageBox.Text & "Missing Web Service URL "
                readyforattempt = False
            End If
            If Not txt_Url.Text.ToLower.EndsWith("/webservices/itfcollections.asmx") Then
                txt_MessageBox.Text = txt_MessageBox.Text & "Invalid URL. Must end with '/webservices/itfcollections.asmx'. "
                readyforattempt = False
            End If
            If txt_Username.Text.Length < 1 Then
                txt_MessageBox.Text = txt_MessageBox.Text & "Missing Username. "
                readyforattempt = False
            End If
            If txt_Password.Text.Length < 1 Then
                txt_MessageBox.Text = txt_MessageBox.Text & "Missing Password. "
                readyforattempt = False
            End If
            If txt_DatabaseServer.Text.Length < 1 Then
                txt_MessageBox.Text = txt_MessageBox.Text & "Missing Server Name. "
                readyforattempt = False
            End If
            If txt_DatabaseName.Text.Length < 1 Then
                txt_MessageBox.Text = txt_MessageBox.Text & "Missing Database Name. "
                readyforattempt = False
            End If

            If rad_SqlServer.Checked Then
                DatabasePlatform = "Sql Server"
            Else : DatabasePlatform = "Oracle"
            End If

            If txt_InterfaceEntity.Text.Length < 1 Then
                txt_MessageBox.Text = txt_MessageBox.Text & "Missing Interface Entity. "
                readyforattempt = False
            End If
            If txt_FileName.Text.Length < 1 Then
                txt_MessageBox.Text = txt_MessageBox.Text & "Missing Output path. "
                readyforattempt = False
            End If

            If License Is Nothing Then
                txt_MessageBox.Text = txt_MessageBox.Text & "License File Does Not Exist. "
                readyforattempt = False
            End If

            If readyforattempt Then
                SubstituteUrl()

                Try
                    XmlNodeResponse = ws.GetPropertyConfigurations( _
                        txt_Username.Text, _
                        txt_Password.Text, _
                        txt_DatabaseServer.Text, _
                        txt_DatabaseName.Text, _
                        DatabasePlatform, _
                        txt_InterfaceEntity.Text, _
                        License)

                    If XmlNodeResponse Is Nothing Then
                        txt_MessageBox.Text = "Export Property Configuration failed! "
                    Else
                        Try
                            XmlDocument = New XmlDocument
                            XmlDocument.LoadXml(XmlNodeResponse.OuterXml)
                            XmlDocument.Save(txt_FileName.Text)
                            txt_MessageBox.Text = "Export Property Configuration: successfully received response; check output file."
                            Timer2.Start()
                        Catch ex As Exception
                            txt_MessageBox.Text = "Create output file failed! " & Chr(13) & Chr(10)
                            txt_MessageBox.Text += ex.Message & Chr(13) & Chr(10)
                            Abort_Stop_Refresh()
                        End Try
                    End If
                Catch ex As Exception
                    txt_MessageBox.Text = ex.Message & Chr(13) & Chr(10)
                    txt_MessageBox.Text += ex.StackTrace
                    Abort_Stop_Refresh()
                End Try
            End If
        Catch ex As Exception
            txt_MessageBox.Text = ex.Message
            Abort_Stop_Refresh()
        End Try
        Abort_Stop_Refresh()
    End Sub

    Private Sub ItfVersion_btn_Hover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItfVersion_btn.MouseHover
        Tooltip.SetToolTip(ItfVersion_btn, "GetVersionNumber")
    End Sub

    Private Sub ItfVersion_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItfVersion_btn.Click
        Try
            ws.Timeout = CInt(txt_TimeOut.Text) * 1000
            WheelThread = New System.Threading.Thread(AddressOf Tick4)
            TimerThread = New System.Threading.Thread(AddressOf Tick5)
            SpinningProgress1.StartWheel()
            WheelThread.Start()
            TimerThread.Start()

            If Not txt_Url.Text.ToLower.EndsWith("/webservices/itfcollections.asmx") Then
                txt_MessageBox.Text = txt_MessageBox.Text & "Invalid URL. Must end with '/webservices/itfcollections.asmx'. "
            Else
                SubstituteUrl()
                txt_MessageBox.Text = "Interface Plug-In " & ws.GetVersionNumber
            End If
        Catch ex As Exception
            txt_MessageBox.Text = ex.Message
            Abort_Stop_Refresh()
        End Try
        Abort_Stop_Refresh()
    End Sub

    Private Sub Ping_btn_Hover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Ping.MouseHover
        Tooltip.SetToolTip(btn_Ping, "Ping")
    End Sub

    Private Sub Ping_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Ping.Click
        Try
            SubstituteUrl()
            ws.Timeout = CInt(txt_TimeOut.Text) * 1000
            WheelThread = New System.Threading.Thread(AddressOf Tick4)
            TimerThread = New System.Threading.Thread(AddressOf Tick5)
            SpinningProgress1.StartWheel()
            WheelThread.Start()
            TimerThread.Start()

            SubstituteUrl()
            txt_MessageBox.Text = ws.Ping()
        Catch ex As Exception
            txt_MessageBox.Text = ex.Message
            Abort_Stop_Refresh()
        End Try
        Abort_Stop_Refresh()
    End Sub
#End Region
    Private Counter As Integer
    Private ElapsedSeconds As Integer = 0
    Private ElapsedMinutes As Integer = 0
    Private WheelThread As System.Threading.Thread
    Private TimerThread As System.Threading.Thread

    Public Sub Tick1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If Counter = 0 Then
            Button8.BackColor = Color.Red
        ElseIf Counter = 1 Then
            Button8.BackColor = Color.White
        ElseIf Counter = 2 Then
            Button8.BackColor = Color.Blue
        ElseIf Counter = 3 Then
            Button8.BackColor = Color.Red
        ElseIf Counter = 4 Then
            Button8.BackColor = Color.White
        ElseIf Counter = 5 Then
            Button8.BackColor = Color.Blue
        ElseIf Counter > 5 Then
            Button8.BackColor = Color.Transparent
            Timer1.Stop()
            Counter = -1
            Button8.Focus()
        End If
        Counter += 1
    End Sub

    Public Sub Tick2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If Counter = 0 Then
            Button1.BackColor = Color.Red
        ElseIf Counter = 1 Then
            Button1.BackColor = Color.White
        ElseIf Counter = 2 Then
            Button1.BackColor = Color.Blue
        ElseIf Counter = 3 Then
            Button1.BackColor = Color.Red
        ElseIf Counter = 4 Then
            Button1.BackColor = Color.White
        ElseIf Counter = 5 Then
            Button1.BackColor = Color.Blue
        ElseIf Counter > 5 Then
            Button1.BackColor = Color.Transparent
            Timer2.Stop()
            Counter = -1
            Button1.Focus()
        End If
        Counter += 1
    End Sub

    Public Sub Tick4()
        Do
            WheelThread.Sleep(50)
            SpinningProgress1.Refresh()
        Loop
    End Sub

    Public Sub Tick5()
        ElapsedSeconds = 1
        ElapsedMinutes = 0
        Do
            If ElapsedSeconds = 60 Then
                ElapsedMinutes += 1
                ElapsedSeconds = 0
            End If
            If ElapsedSeconds < 10 Then
                TimeElapsed.Text = ElapsedMinutes.ToString & ":0" & ElapsedSeconds.ToString
            Else
                TimeElapsed.Text = ElapsedMinutes.ToString & ":" & ElapsedSeconds.ToString
            End If
            TimeElapsed.Refresh()
            TimerThread.Sleep(1000)
            ElapsedSeconds += 1
        Loop
    End Sub

    Public Sub Abort_Stop_Refresh()
        WheelThread.Abort()
        SpinningProgress1.StopWheel()
        SpinningProgress1.TransistionSegment = 0
        SpinningProgress1.Refresh()
        TimerThread.Abort()
        TimeElapsed.Refresh()
        txt_LastRun.Text = System.DateTime.Now.ToShortTimeString
        txt_LastRun.Refresh()
    End Sub

    Public Function GetLicense(ByVal FileName As String) As String
        Dim TextLine As String

        If System.IO.File.Exists(FileName) = True Then
            Dim objReader As New System.IO.StreamReader(FileName)
            Do While objReader.Peek() <> -1
                TextLine = TextLine & objReader.ReadLine()
            Loop
            If Not TextLine Is Nothing AndAlso TextLine.Trim.Length > 0 Then
                Return TextLine.Trim
            Else
                Return ""
            End If
        Else
            Return Nothing
        End If
    End Function
End Class
