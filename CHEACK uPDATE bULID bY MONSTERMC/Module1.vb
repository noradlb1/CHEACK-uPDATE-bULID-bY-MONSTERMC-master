﻿Option Strict On

Imports System.Drawing.Drawing2D
Imports System.Drawing.Text

'<info>
' --------------------Butterscotch Theme--------------------
' Creator - SaketSaket (HF)
' UID - 8675309
' Inspiration & Credits to all Theme creators of HF
' Version - 1.0
' Date Created - 14st August 2014
' Date Modified - 17th August 2014
'
'
'Special Thanks to Aeonhack for RoundRect Functions...
'Special Thanks to Mephobia's for NoiseBrush Functions...
'AlertBox Control idea taken from iSynthesis' Flat UI theme
'
'
' For bugs & Constructive Criticism contact me on HF
' If you like it & want to DONATE then pm me on HF
' --------------------Butterscotch Theme--------------------
'<info>

'Please Leave Credits in Source, Do not redistribute

Enum MouseState As Byte
    None = 0
    Over = 1
    Down = 2
End Enum

Module Draw
    'Special Thanks to Aeonhack for RoundRect Functions... ;)
    Public Function RoundRect(ByVal rectangle As Rectangle, ByVal curve As Integer) As GraphicsPath
        Dim p As GraphicsPath = New GraphicsPath()
        Dim arcRectangleWidth As Integer = curve * 2
        p.AddArc(New Rectangle(rectangle.X, rectangle.Y, arcRectangleWidth, arcRectangleWidth), -180, 90)
        p.AddArc(New Rectangle(rectangle.Width - arcRectangleWidth + rectangle.X, rectangle.Y, arcRectangleWidth, arcRectangleWidth), -90, 90)
        p.AddArc(New Rectangle(rectangle.Width - arcRectangleWidth + rectangle.X, rectangle.Height - arcRectangleWidth + rectangle.Y, arcRectangleWidth, arcRectangleWidth), 0, 90)
        p.AddArc(New Rectangle(rectangle.X, rectangle.Height - arcRectangleWidth + rectangle.Y, arcRectangleWidth, arcRectangleWidth), 90, 90)
        p.AddLine(New Point(rectangle.X, rectangle.Height - arcRectangleWidth + rectangle.Y), New Point(rectangle.X, curve + rectangle.Y))
        Return p
    End Function
    Public Function RoundRect(ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal curve As Integer) As GraphicsPath
        Dim rectangle As Rectangle = New Rectangle(x, y, width, height)
        Dim p As GraphicsPath = New GraphicsPath()
        Dim arcRectangleWidth As Integer = curve * 2
        p.AddArc(New Rectangle(rectangle.X, rectangle.Y, arcRectangleWidth, arcRectangleWidth), -180, 90)
        p.AddArc(New Rectangle(rectangle.Width - arcRectangleWidth + rectangle.X, rectangle.Y, arcRectangleWidth, arcRectangleWidth), -90, 90)
        p.AddArc(New Rectangle(rectangle.Width - arcRectangleWidth + rectangle.X, rectangle.Height - arcRectangleWidth + rectangle.Y, arcRectangleWidth, arcRectangleWidth), 0, 90)
        p.AddArc(New Rectangle(rectangle.X, rectangle.Height - arcRectangleWidth + rectangle.Y, arcRectangleWidth, arcRectangleWidth), 90, 90)
        p.AddLine(New Point(rectangle.X, rectangle.Height - arcRectangleWidth + rectangle.Y), New Point(rectangle.X, curve + rectangle.Y))
        Return p
    End Function

    'Special Thanks to Mephobia's for NoiseBrush Functions...
    Function NoiseBrush(colors As Color()) As TextureBrush
        Dim b As New Bitmap(128, 128)
        Dim r As New Random(128)
        For x As Integer = 0 To b.Width - 1
            For y As Integer = 0 To b.Height - 1
                b.SetPixel(x, y, colors(r.Next(colors.Length)))
            Next
        Next
        Dim T As New TextureBrush(b)
        b.Dispose()
        Return T
    End Function
End Module

Public Class ButterscotchTheme : Inherits ContainerControl
    Dim WithEvents _mytimer As Windows.Forms.Timer
    Dim _myval As Integer = 0
    Private _mousepos As Point = New Point(0, 0)
    Private _drag As Boolean = False
    Private _icon As Icon

    Public Property Icon() As Icon
        Get
            Return _icon
        End Get
        Set(ByVal value As Icon)
            _icon = value
            Invalidate()
        End Set
    End Property

    Private _border As Boolean = True
    Public Property Border As Boolean
        Get
            Return _border
        End Get
        Set(value As Boolean)
            _border = value
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        If e.Button = Windows.Forms.MouseButtons.Left And New Rectangle(60, 7, Width - 141, 37).Contains(e.Location) Then
            _drag = True : _mousepos = e.Location
        End If
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        _drag = False
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        If _drag Then
            Parent.Location = New Point(MousePosition.X - _mousepos.X, MousePosition.Y - _mousepos.Y)
        End If
    End Sub

    Sub New()
        MyBase.New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)
        DoubleBuffered = True
        _mytimer = New Timer()
        _mytimer.Interval = 2000
    End Sub

    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
        ParentForm.FormBorderStyle = FormBorderStyle.None
        ParentForm.TransparencyKey = Color.Fuchsia
        Dock = DockStyle.Fill
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim b As Bitmap = New Bitmap(Width, Height)
        Dim g As Graphics = Graphics.FromImage(b)
        Dim rect As Rectangle = New Rectangle(0, 0, Width - 1, Height - 1)
        Dim headrect As Rectangle = New Rectangle(60, 7, Width - 141, 37)
        MyBase.OnPaint(e)
        g.Clear(Color.Fuchsia)
        Dim bodygb As TextureBrush = NoiseBrush({Color.FromArgb(34, 29, 23), Color.FromArgb(50, 45, 39)})
        g.FillPath(bodygb, RoundRect(rect, 3))
        Try
            g.DrawIcon(_icon, New Rectangle(8, 8, 38, 38))
        Catch : End Try
        If _myval = 0 Then
            g.DrawString(Text, New Font("Segoe UI", 12, FontStyle.Bold), New SolidBrush(Color.FromArgb(206, 209, 208)), headrect, New StringFormat() With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Center})
        ElseIf _myval = 1 Then
            g.DrawString(Text, New Font("Segoe UI", 12, FontStyle.Bold), New SolidBrush(Color.FromArgb(128, 128, 128)), headrect, New StringFormat() With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Center})
        End If
        If Border = True Then
            g.DrawPath(New Pen(Color.FromArgb(212, 212, 212), 3), RoundRect(rect, 3))
        End If
        If Not DesignMode Then _mytimer.Start()
        e.Graphics.DrawImage(b, New Point(0, 0))
        g.Dispose() : b.Dispose()
    End Sub

    Private Sub MyTimer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles _mytimer.Tick
        If _myval = 0 Then
            _myval = 1
        ElseIf _myval = 1 Then
            _myval = 0
        End If
        Invalidate()
    End Sub
End Class

Public Class ButterscotchControlBox : Inherits Control
    Dim _state As MouseState = MouseState.None
    Dim _x As Integer
    ReadOnly _minrect As New Rectangle(5, 2, 24, 6)
    'ReadOnly _maxrect As New Rectangle(32, 2, 24, 1)
    ReadOnly _closerect As New Rectangle(59, 2, 24, 6)
    Private _minDisable As Boolean = False
    Public Property MinimumDisable As Boolean
        Get
            Return _minDisable
        End Get
        Set(value As Boolean)
            _minDisable = value
            Invalidate()
        End Set
    End Property

    Private _maxDisable As Boolean = False
    Public Property MaximumDisable As Boolean
        Get
            Return _maxDisable
        End Get
        Set(value As Boolean)
            _maxDisable = False 'To run it ples chane False To value
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnMouseDown(ByVal e As Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDown(e)
        If _x > 5 AndAlso _x < 29 Then
            If MinimumDisable = False Then
                FindForm.WindowState = FormWindowState.Minimized
            End If
        ElseIf _x > 32 AndAlso _x < 56 Then
            If MaximumDisable = True Then
                If FindForm.WindowState = FormWindowState.Maximized Then
                    FindForm.WindowState = FormWindowState.Minimized
                    FindForm.WindowState = FormWindowState.Normal
                Else
                    FindForm.WindowState = FormWindowState.Minimized
                    FindForm.WindowState = FormWindowState.Maximized
                End If
            End If
        ElseIf _x > 59 AndAlso _x < 83 Then
            FindForm.Close()
        End If
        _state = MouseState.Down : Invalidate()
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal e As Windows.Forms.MouseEventArgs)
        MyBase.OnMouseUp(e)
        _state = MouseState.Over : Invalidate()
    End Sub

    Protected Overrides Sub OnMouseEnter(ByVal e As EventArgs)
        MyBase.OnMouseEnter(e)
        _state = MouseState.Over : Invalidate()
    End Sub

    Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
        MyBase.OnMouseLeave(e)
        _state = MouseState.None : Invalidate()
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As Windows.Forms.MouseEventArgs)
        MyBase.OnMouseMove(e)
        _x = e.Location.X
        Invalidate()
    End Sub

    Sub New()
        MyBase.New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)
        DoubleBuffered = True
        BackColor = Color.Transparent
        Width = 85
        Height = 30
        Anchor = AnchorStyles.Top Or AnchorStyles.Right
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim g As Graphics = e.Graphics
        MyBase.OnPaint(e)
        g.SmoothingMode = SmoothingMode.HighQuality
        Dim minrdefault As New LinearGradientBrush(_minrect, Color.FromArgb(100, 90, 80), Color.FromArgb(25, 23, 22), 90S)
        g.FillRectangle(minrdefault, _minrect)
        g.DrawRectangle(Pens.Black, _minrect)
        'Dim maxrdefault As New LinearGradientBrush(_maxrect, Color.FromArgb(100, 90, 80), Color.FromArgb(25, 23, 22), 90S)
        'g.FillRectangle(maxrdefault, _maxrect)
        'g.DrawRectangle(Pens.Black, _maxrect)
        Dim crdefault As New LinearGradientBrush(_closerect, Color.FromArgb(100, 90, 80), Color.FromArgb(25, 23, 22), 90S)
        g.FillRectangle(crdefault, _closerect)
        g.DrawRectangle(Pens.Black, _closerect)
        Select Case _state
            Case MouseState.None
                Dim minrnone As New LinearGradientBrush(_minrect, Color.FromArgb(100, 90, 80), Color.FromArgb(25, 23, 22), 90S)
                g.FillRectangle(minrnone, _minrect)
                g.DrawRectangle(Pens.Black, _minrect)
                'Dim maxrnone As New LinearGradientBrush(_maxrect, Color.FromArgb(100, 90, 80), Color.FromArgb(25, 23, 22), 90S)
                'g.FillRectangle(maxrnone, _maxrect)
                'g.DrawRectangle(Pens.Black, _maxrect)
                Dim crnone As New LinearGradientBrush(_closerect, Color.FromArgb(100, 90, 80), Color.FromArgb(25, 23, 22), 90S)
                g.FillRectangle(crnone, _closerect)
                g.DrawRectangle(Pens.Black, _closerect)
            Case MouseState.Over
                If _x > 5 AndAlso _x < 29 Then
                    If MinimumDisable = False Then
                        Dim minrover As New LinearGradientBrush(_minrect, Color.FromArgb(25, 23, 22), Color.FromArgb(100, 90, 80), 90S)
                        g.FillRectangle(minrover, _minrect)
                        g.DrawRectangle(Pens.DimGray, _minrect)
                    End If
                ElseIf _x > 32 AndAlso _x < 56 Then
                    'If MaximumDisable = False Then
                    '    Dim maxrover As New LinearGradientBrush(_maxrect, Color.FromArgb(25, 23, 22), Color.FromArgb(100, 90, 80), 90S)
                    '    g.FillRectangle(maxrover, _maxrect)
                    '    g.DrawRectangle(Pens.DimGray, _maxrect)
                    'End If
                ElseIf _x > 59 AndAlso _x < 83 Then
                    Dim crover As New LinearGradientBrush(_closerect, Color.FromArgb(25, 23, 22), Color.FromArgb(100, 90, 80), 90S)
                    g.FillRectangle(crover, _closerect)
                    g.DrawRectangle(Pens.DimGray, _closerect)
                End If
            Case Else
                If MinimumDisable = False Then
                    Dim minrelse As New LinearGradientBrush(_minrect, Color.FromArgb(100, 90, 80), Color.FromArgb(25, 23, 22), 90S)
                    g.FillRectangle(minrelse, _minrect)
                    g.DrawRectangle(Pens.Silver, _minrect)
                End If
                'If MaximumDisable = False Then
                '    Dim maxrelse As New LinearGradientBrush(_maxrect, Color.FromArgb(100, 90, 80), Color.FromArgb(25, 23, 22), 90S)
                '    g.FillRectangle(maxrelse, _maxrect)
                '    g.DrawRectangle(Pens.Silver, _maxrect)
                'End If
                Dim crelse As New LinearGradientBrush(_closerect, Color.FromArgb(100, 90, 80), Color.FromArgb(25, 23, 22), 90S)
                g.FillRectangle(crelse, _closerect)
                g.DrawRectangle(Pens.Silver, _closerect)
        End Select
    End Sub
End Class

Public Class ButterscotchRadioButton : Inherits Control
    Private _check As Boolean
    Public Property Checked As Boolean
        Get
            Return _check
        End Get
        Set(value As Boolean)
            _check = value
            Invalidate()
        End Set
    End Property

    Sub New()
        MyBase.New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
        DoubleBuffered = True
        Size = New Size(180, 25)
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        Invalidate()
    End Sub

    Protected Overrides Sub OnClick(ByVal e As EventArgs)
        MyBase.OnClick(e)
        If Not Checked Then Checked = True
        For Each ctrl As ButterscotchRadioButton In From ctrl1 In Parent.Controls.OfType(Of ButterscotchRadioButton)() Where ctrl1.Handle <> Handle Where ctrl1.Enabled
            ctrl.Checked = False
        Next
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim b As Bitmap = New Bitmap(Width, Height)
        Dim g As Graphics = Graphics.FromImage(b)
        Dim selectionrect As Rectangle = New Rectangle(3, 3, 18, 18)
        Dim innerselectionrect As Rectangle = New Rectangle(4, 4, 17, 17)
        Dim selectrect As Rectangle = New Rectangle(8, 8, 8, 8)
        MyBase.OnPaint(e)
        g.SmoothingMode = SmoothingMode.HighQuality
        g.InterpolationMode = InterpolationMode.HighQualityBicubic
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        g.Clear(BackColor)
        g.DrawString(Text, New Font("Segoe UI", 11, FontStyle.Regular), New SolidBrush(Color.FromArgb(245, 245, 245)), New Rectangle(25, 4, Width, 16), New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
        If Checked Then
            g.FillEllipse(New SolidBrush(Color.FromArgb(0, 0, 0)), selectionrect)
            g.FillEllipse(New SolidBrush(Color.FromArgb(40, 37, 33)), innerselectionrect)
            g.FillEllipse(New SolidBrush(Color.FromArgb(246, 180, 12)), selectrect)
        Else
            g.FillEllipse(New SolidBrush(Color.FromArgb(0, 0, 0)), selectionrect)
            g.FillEllipse(New SolidBrush(Color.FromArgb(40, 37, 33)), innerselectionrect)
            g.FillEllipse(New SolidBrush(Color.FromArgb(20, 18, 17)), selectrect)
        End If
        e.Graphics.DrawImage(b, New Point(0, 0))
        g.Dispose() : b.Dispose()
    End Sub
End Class

Public Class ButterscotchCheckBox : Inherits Control
    Private _check As Boolean
    Public Property Checked As Boolean
        Get
            Return _check
        End Get
        Set(value As Boolean)
            _check = value
            Invalidate()
        End Set
    End Property

    Sub New()
        MyBase.New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
        DoubleBuffered = True
        Size = New Size(180, 25)
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        Invalidate()
    End Sub

    Protected Overrides Sub OnClick(ByVal e As EventArgs)
        MyBase.OnClick(e)
        If Not Checked Then Checked = True Else Checked = False
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim b As Bitmap = New Bitmap(Width, Height)
        Dim g As Graphics = Graphics.FromImage(b)
        Dim selectionrect As Rectangle = New Rectangle(3, 3, 18, 18)
        Dim innerselectionrect As Rectangle = New Rectangle(4, 4, 17, 17)
        Dim selectrect As Rectangle = New Rectangle(6, 6, 16, 16)
        MyBase.OnPaint(e)
        g.SmoothingMode = SmoothingMode.HighQuality
        g.InterpolationMode = InterpolationMode.HighQualityBicubic
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        g.Clear(BackColor)
        g.DrawString(Text, New Font("Segoe UI", 11, FontStyle.Regular), New SolidBrush(Color.FromArgb(245, 245, 245)), New Rectangle(20, 4, Width, 16), New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
        If Checked Then
            g.FillRectangle(New SolidBrush(Color.FromArgb(0, 0, 0)), selectionrect)
            g.FillRectangle(New SolidBrush(Color.FromArgb(40, 37, 33)), innerselectionrect)
            g.DrawString("b", New Font("Marlett", 15, FontStyle.Bold), New SolidBrush(Color.FromArgb(246, 180, 12)), selectrect, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
        Else
            g.FillRectangle(New SolidBrush(Color.FromArgb(0, 0, 0)), selectionrect)
            g.FillRectangle(New SolidBrush(Color.FromArgb(40, 37, 33)), innerselectionrect)
            g.DrawString("b", New Font("Marlett", 15, FontStyle.Bold), New SolidBrush(Color.FromArgb(20, 18, 17)), selectrect, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
        End If
        e.Graphics.DrawImage(b, New Point(0, 0))
        g.Dispose() : b.Dispose()
    End Sub
End Class

Public Class ButterscotchButton : Inherits Control
    Dim _state As MouseState
    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        _state = MouseState.Down
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        _state = MouseState.Over
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseEnter(ByVal e As EventArgs)
        MyBase.OnMouseEnter(e)
        _state = MouseState.Over
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
        MyBase.OnMouseLeave(e)
        _state = MouseState.None
        Invalidate()
    End Sub

    Sub New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
        DoubleBuffered = True
        Size = New Size(160, 35)
        _state = MouseState.None
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim b As Bitmap = New Bitmap(Width, Height)
        Dim g As Graphics = Graphics.FromImage(b)
        Dim rect As Rectangle = New Rectangle(0, 0, Width - 1, Height - 1)
        Dim innerrect As Rectangle = New Rectangle(3, 3, Width - 7, Height - 7)
        Dim btnfont As New Font("Verdana", 10, FontStyle.Regular)
        MyBase.OnPaint(e)
        g.SmoothingMode = SmoothingMode.HighQuality
        g.InterpolationMode = InterpolationMode.HighQualityBicubic
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        g.Clear(BackColor)
        Dim buttonrect As New LinearGradientBrush(rect, Color.FromArgb(100, 90, 80), Color.FromArgb(48, 43, 39), LinearGradientMode.Vertical)
        g.FillPath(New SolidBrush(Color.FromArgb(26, 25, 21)), RoundRect(rect, 3))
        g.FillPath(buttonrect, RoundRect(innerrect, 3))
        Select Case _state
            Case MouseState.None
                Dim buttonrectnone As New LinearGradientBrush(innerrect, Color.FromArgb(100, 90, 80), Color.FromArgb(48, 43, 39), LinearGradientMode.Vertical)
                g.FillPath(New SolidBrush(Color.FromArgb(26, 25, 21)), RoundRect(rect, 3))
                g.FillPath(buttonrectnone, RoundRect(innerrect, 3))
                g.DrawString(Text, btnfont, Brushes.White, innerrect, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
            Case MouseState.Down
                Dim buttonrectdown As New LinearGradientBrush(innerrect, Color.FromArgb(48, 43, 39), Color.FromArgb(100, 90, 80), LinearGradientMode.Vertical)
                g.FillPath(New SolidBrush(Color.FromArgb(26, 25, 21)), RoundRect(rect, 3))
                g.FillPath(buttonrectdown, RoundRect(innerrect, 3))
                g.DrawString(Text, btnfont, Brushes.White, innerrect, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
            Case MouseState.Over
                Dim buttonrectover As New LinearGradientBrush(innerrect, Color.FromArgb(48, 43, 39), Color.FromArgb(100, 90, 80), LinearGradientMode.Vertical)
                g.FillPath(New SolidBrush(Color.FromArgb(26, 25, 21)), RoundRect(rect, 3))
                g.FillPath(buttonrectover, RoundRect(innerrect, 3))
                g.DrawString(Text, btnfont, Brushes.White, innerrect, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
        End Select
        e.Graphics.DrawImage(b, New Point(0, 0))
        g.Dispose() : b.Dispose()
    End Sub
End Class

Public Class ButterscotchPanel : Inherits ContainerControl

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
        Invalidate()
    End Sub

    Sub New()
        MyBase.New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
        DoubleBuffered = True
        Size = New Size(240, 160)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim b As Bitmap = New Bitmap(Width, Height)
        Dim g As Graphics = Graphics.FromImage(b)
        Dim outerrect As Rectangle = New Rectangle(0, 0, Width - 1, Height - 1)
        Dim innerrect As Rectangle = New Rectangle(3, 3, Width - 7, Height - 7)
        MyBase.OnPaint(e)
        g.SmoothingMode = SmoothingMode.HighQuality
        g.InterpolationMode = InterpolationMode.HighQualityBicubic
        g.Clear(BackColor)
        g.FillPath(New SolidBrush(Color.FromArgb(26, 25, 21)), RoundRect(outerrect, 3))
        Dim panelgb As New LinearGradientBrush(innerrect, Color.FromArgb(100, 90, 80), Color.FromArgb(48, 43, 39), 90S)
        g.FillPath(panelgb, RoundRect(innerrect, 3))
        e.Graphics.DrawImage(b, New Point(0, 0))
        g.Dispose() : b.Dispose()
    End Sub
End Class

Public Class ButterscotchGroupBox : Inherits ContainerControl

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
        Invalidate()
    End Sub

    Sub New()
        MyBase.New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
        DoubleBuffered = True
        Size = New Size(240, 160)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim b As Bitmap = New Bitmap(Width, Height)
        Dim g As Graphics = Graphics.FromImage(b)
        Dim outerrect As Rectangle = New Rectangle(0, 0, Width - 1, Height - 1)
        Dim innerrect As Rectangle = New Rectangle(3, 3, Width - 7, Height - 7)
        Dim underlinepen As New Pen(Color.FromArgb(255, 255, 255), 2)
        MyBase.OnPaint(e)
        g.SmoothingMode = SmoothingMode.HighQuality
        g.InterpolationMode = InterpolationMode.HighQualityBicubic
        g.Clear(BackColor)
        g.FillPath(New SolidBrush(Color.FromArgb(26, 25, 21)), RoundRect(outerrect, 3))
        Dim panelgb As New LinearGradientBrush(innerrect, Color.FromArgb(100, 90, 80), Color.FromArgb(48, 43, 39), 90S)
        g.FillPath(panelgb, RoundRect(innerrect, 3))
        g.DrawString(Text, New Font("Segoe UI", 11, FontStyle.Bold), New SolidBrush(Color.FromArgb(255, 255, 255)), New Rectangle(0, 3, Width - 1, 30), New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
        g.DrawLine(underlinepen, 20, 30, Width - 21, 30)
        e.Graphics.DrawImage(b, New Point(0, 0))
        g.Dispose() : b.Dispose()
    End Sub
End Class

Public Class ButterscotchProgressBar : Inherits Control
    Private _val As Integer
    Public Property Value() As Integer
        Get
            Return _val
        End Get
        Set(ByVal v As Integer)
            If v > _max Then
                _val = _max
            ElseIf v < 0 Then
                _val = 0
            Else
                _val = v
            End If
            Invalidate()
        End Set
    End Property

    Private _max As Integer
    Public Property Maximum() As Integer
        Get
            Return _max
        End Get
        Set(ByVal v As Integer)
            If v < 1 Then
                _max = 1
            Else
                _max = v
            End If
            If v < _val Then
                _val = _max
            End If
            Invalidate()
        End Set
    End Property

    Private _showPercentage As Boolean = False
    Public Property ShowPercentage() As Boolean
        Get
            Return _showPercentage
        End Get
        Set(ByVal v As Boolean)
            _showPercentage = v
            Invalidate()
        End Set
    End Property

    Sub New()
        MyBase.New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
        Size = New Size(250, 20)
        DoubleBuffered = True
        _max = 100
    End Sub

    Protected Overrides Sub OnPaint(e As Windows.Forms.PaintEventArgs)
        Dim b As Bitmap = New Bitmap(Width, Height)
        Dim g As Graphics = Graphics.FromImage(b)
        Dim percent As Integer = CInt((Width - 1) * (_val / _max))
        Dim outerrect As Rectangle = New Rectangle(0, 0, Width - 1, Height - 1)
        Dim maininnerrect As Rectangle = New Rectangle(7, 7, Width - 15, Height - 15)
        Dim innerrect As Rectangle = New Rectangle(4, 4, percent - 9, Height - 9)
        MyBase.OnPaint(e)
        g.Clear(BackColor)
        g.SmoothingMode = SmoothingMode.HighQuality
        g.FillPath(New SolidBrush(Color.FromArgb(40, 37, 33)), RoundRect(outerrect, 5))
        g.DrawPath(New Pen(Color.FromArgb(0, 0, 0)), RoundRect(outerrect, 5))
        g.FillPath(New SolidBrush(Color.FromArgb(26, 25, 21)), RoundRect(maininnerrect, 3))
        g.DrawPath(New Pen(Color.FromArgb(0, 0, 0)), RoundRect(maininnerrect, 3))
        If percent <> 0 Then
            Dim progressgb As New LinearGradientBrush(innerrect, Color.FromArgb(91, 82, 73), Color.FromArgb(57, 52, 46), 90S)
            g.FillPath(progressgb, RoundRect(innerrect, 7))
        End If
        If _showPercentage Then
            g.DrawString(String.Format("{0}%", _val), New Font("Segoe UI", 11, FontStyle.Regular), New SolidBrush(Color.FromArgb(246, 180, 12)), New Rectangle(10, 1, Width - 1, Height - 1), New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
        End If
        e.Graphics.DrawImage(b, New Point(0, 0))
        g.Dispose() : b.Dispose()
    End Sub
End Class

Public Class ButterscotchLabel : Inherits Control
    Private _border As Boolean = True
    Public Property Border As Boolean
        Get
            Return _border
        End Get
        Set(value As Boolean)
            _border = value
            Invalidate()
        End Set
    End Property

    Sub New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
        DoubleBuffered = True
        Size = New Size(150, 30)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As Windows.Forms.PaintEventArgs)
        Dim b As Bitmap = New Bitmap(Width, Height)
        Dim g As Graphics = Graphics.FromImage(b)
        Dim rect As Rectangle = New Rectangle(0, 0, Width - 1, Height - 1)
        Dim innerrect As Rectangle = New Rectangle(3, 3, Width - 7, Height - 7)
        MyBase.OnPaint(e)
        g.Clear(BackColor)
        g.SmoothingMode = SmoothingMode.HighQuality
        g.InterpolationMode = InterpolationMode.HighQualityBicubic
        g.FillPath(New SolidBrush(Color.FromArgb(40, 37, 33)), RoundRect(rect, 3))
        If Border = True Then
            g.FillPath(New SolidBrush(Color.FromArgb(26, 25, 21)), RoundRect(rect, 3))
            g.FillPath(New SolidBrush(Color.FromArgb(40, 37, 33)), RoundRect(innerrect, 3))
        End If
        g.DrawString(Text, New Font("Segoe UI", 11, FontStyle.Regular), New SolidBrush(Color.FromArgb(255, 255, 255)), rect, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
        e.Graphics.DrawImage(b, New Point(0, 0))
        g.Dispose() : b.Dispose()
    End Sub
End Class

Public Class ButterscotchTabControl : Inherits TabControl

    Sub New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
        DoubleBuffered = True
        ItemSize = New Size(100, 35)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As Windows.Forms.PaintEventArgs)
        Dim b As Bitmap = New Bitmap(Width, Height)
        Dim g As Graphics = Graphics.FromImage(b)
        Dim rect As Rectangle = New Rectangle(0, 0, Width - 1, Height - 1)
        Try : SelectedTab.BackColor = Color.FromArgb(100, 90, 80) : Catch : End Try
        MyBase.OnPaint(e)
        g.Clear(BackColor)
        g.FillRectangle(New SolidBrush(Color.FromArgb(40, 37, 33)), rect)
        g.DrawRectangle(New Pen(Brushes.Black), rect)
        For i = 0 To TabCount - 1
            Dim textRectangle As Rectangle = New Rectangle(New Point(GetTabRect(i).Location.X + 3, GetTabRect(i).Location.Y), New Size(GetTabRect(i).Width - 7, GetTabRect(i).Height))
            If i = SelectedIndex Then
                Dim tabrect As Rectangle = New Rectangle(New Point(GetTabRect(i).Location.X + 1, GetTabRect(i).Location.Y), New Size(GetTabRect(i).Width - 2, GetTabRect(i).Height))
                Dim buttonrect As New LinearGradientBrush(tabrect, Color.FromArgb(100, 90, 80), Color.FromArgb(48, 43, 39), 90S)
                g.FillPath(buttonrect, RoundRect(tabrect, 5))
                g.DrawString(TabPages(i).Text, New Font("Segoe UI", 10, FontStyle.Bold), New SolidBrush(Color.FromArgb(25, 23, 22)), textRectangle, New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Near})
            Else
                Dim tabrect As Rectangle = New Rectangle(New Point(GetTabRect(i).Location.X + 1, GetTabRect(i).Location.Y), New Size(GetTabRect(i).Width - 2, GetTabRect(i).Height))
                Dim buttonrect As New LinearGradientBrush(tabrect, Color.FromArgb(57, 52, 46), Color.FromArgb(92, 83, 74), 90S)

                g.FillPath(buttonrect, RoundRect(tabrect, 5))
                g.DrawString(TabPages(i).Text, New Font("Segoe UI", 10, FontStyle.Regular), New SolidBrush(Color.FromArgb(255, 255, 255)), textRectangle, New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Near})
            End If
        Next
        e.Graphics.DrawImage(b, New Point(0, 0))
        g.Dispose() : b.Dispose()
    End Sub
End Class

Public Class ButterscotchVerticalTabControl : Inherits TabControl

    Sub New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
        DoubleBuffered = True
        SizeMode = TabSizeMode.Fixed
        Alignment = TabAlignment.Left
        ItemSize = New Size(35, 100)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As Windows.Forms.PaintEventArgs)
        Dim b As Bitmap = New Bitmap(Width, Height)
        Dim g As Graphics = Graphics.FromImage(b)
        Dim rect As Rectangle = New Rectangle(0, 0, Width - 1, Height - 1)
        Try : SelectedTab.BackColor = Color.FromArgb(100, 90, 80) : Catch : End Try
        MyBase.OnPaint(e)
        g.Clear(BackColor)
        g.FillRectangle(New SolidBrush(Color.FromArgb(40, 37, 33)), rect)
        g.DrawRectangle(New Pen(Brushes.Black), rect)
        For i = 0 To TabCount - 1
            Dim textRectangle As Rectangle = New Rectangle(New Point(GetTabRect(i).Location.X + 7, GetTabRect(i).Location.Y + 2), New Size(GetTabRect(i).Width - 15, GetTabRect(i).Height - 5))
            If i = SelectedIndex Then
                Dim tabrect As Rectangle = New Rectangle(New Point(GetTabRect(i).Location.X + 2, GetTabRect(i).Location.Y + 1), New Size(GetTabRect(i).Width - 2, GetTabRect(i).Height - 3))
                Dim buttonrect As New LinearGradientBrush(tabrect, Color.FromArgb(100, 90, 80), Color.FromArgb(48, 43, 39), 90S)
                g.FillPath(buttonrect, RoundRect(tabrect, 5))
                g.DrawString(TabPages(i).Text, New Font("Segoe UI", 10, FontStyle.Bold), New SolidBrush(Color.FromArgb(25, 23, 22)), textRectangle, New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Center})
            Else
                Dim tabrect As Rectangle = New Rectangle(New Point(GetTabRect(i).Location.X + 2, GetTabRect(i).Location.Y + 1), New Size(GetTabRect(i).Width - 2, GetTabRect(i).Height - 3))
                Dim buttonrect As New LinearGradientBrush(tabrect, Color.FromArgb(57, 52, 46), Color.FromArgb(92, 83, 74), 90S)
                g.FillPath(buttonrect, RoundRect(tabrect, 5))
                g.DrawString(TabPages(i).Text, New Font("Segoe UI", 10, FontStyle.Regular), New SolidBrush(Color.FromArgb(255, 255, 255)), textRectangle, New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Center})
            End If
        Next
        e.Graphics.DrawImage(b, New Point(0, 0))
        g.Dispose() : b.Dispose()
    End Sub
End Class

Public Class ButterscotchToggle : Inherits Control

    Private _check As Boolean
    Public Property Checked As Boolean
        Get
            Return _check
        End Get
        Set(value As Boolean)
            _check = value
            Invalidate()
        End Set
    End Property

    Sub New()
        MyBase.New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)
        DoubleBuffered = True
        BackColor = Color.Transparent
        Size = New Size(80, 25)
    End Sub

    Protected Overrides Sub OnClick(ByVal e As EventArgs)
        MyBase.OnClick(e)
        If Not Checked Then Checked = True Else Checked = False
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim b As Bitmap = New Bitmap(Width, Height)
        Dim g As Graphics = Graphics.FromImage(b)
        Dim outerrect As Rectangle = New Rectangle(0, 0, Width - 1, Height - 1)
        Dim maininnerrect As Rectangle = New Rectangle(7, 7, Width - 15, Height - 15)
        Dim buttonrect As New LinearGradientBrush(outerrect, Color.FromArgb(100, 90, 80), Color.FromArgb(48, 43, 39), 90S)
        MyBase.OnPaint(e)
        g.Clear(BackColor)
        g.SmoothingMode = SmoothingMode.HighQuality
        g.InterpolationMode = InterpolationMode.HighQualityBicubic
        g.FillPath(New SolidBrush(Color.FromArgb(40, 37, 33)), RoundRect(outerrect, 5))
        g.DrawPath(New Pen(Color.FromArgb(0, 0, 0)), RoundRect(outerrect, 5))
        g.FillPath(New SolidBrush(Color.FromArgb(26, 25, 21)), RoundRect(maininnerrect, 3))
        g.DrawPath(New Pen(Color.FromArgb(0, 0, 0)), RoundRect(maininnerrect, 3))
        If Checked Then
            g.FillPath(buttonrect, RoundRect(New Rectangle(3, 3, CInt((Width / 2) - 3), Height - 7), 7))
            g.DrawString("ON", New Font("Segoe UI", 10, FontStyle.Bold), New SolidBrush(Color.FromArgb(246, 180, 12)), New Rectangle(2, 2, CInt((Width / 2) - 1), Height - 5), New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
        Else
            g.FillPath(buttonrect, RoundRect(New Rectangle(CInt((Width / 2) - 3), 3, CInt((Width / 2) - 3), Height - 7), 7))
            g.DrawString("OFF", New Font("Segoe UI", 10, FontStyle.Bold), New SolidBrush(Color.FromArgb(246, 180, 12)), New Rectangle(CInt((Width / 2) - 2), 2, CInt((Width / 2) - 1), Height - 5), New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
        End If
        e.Graphics.DrawImage(b, New Point(0, 0))
        g.Dispose() : b.Dispose()
    End Sub
End Class

Public Class ButterscotchComboBox : Inherits ComboBox
    Private _startIndex As Integer = 0
    Private Property StartIndex As Integer
        Get
            Return _startIndex
        End Get
        Set(ByVal value As Integer)
            _startIndex = value
            Try
                SelectedIndex = value
            Catch
            End Try
            Invalidate()
        End Set
    End Property

    Sub ReplaceItem(ByVal sender As System.Object, ByVal e As Windows.Forms.DrawItemEventArgs) Handles Me.DrawItem
        e.DrawBackground()
        Dim sitemrect As New LinearGradientBrush(New Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height), Color.FromArgb(100, 90, 80), Color.FromArgb(48, 43, 39), 90S)
        Dim itemrect As New LinearGradientBrush(New Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height), Color.FromArgb(57, 52, 46), Color.FromArgb(92, 83, 74), 90S)
        Try
            If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                e.Graphics.FillPath(sitemrect, RoundRect(New Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height), 3))
                e.Graphics.DrawPath(New Pen(Brushes.Black), RoundRect(New Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height), 3))
            Else
                e.Graphics.FillPath(itemrect, RoundRect(New Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height), 3))
                e.Graphics.DrawPath(New Pen(Brushes.Black), RoundRect(New Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height), 3))
            End If
            e.Graphics.DrawString(GetItemText(Items(e.Index)), e.Font, New SolidBrush(Color.FromArgb(212, 212, 212)), New Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height), New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
        Catch : End Try
    End Sub

    Sub New()
        MyBase.New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)
        DrawMode = Windows.Forms.DrawMode.OwnerDrawFixed
        BackColor = Color.Transparent
        DropDownStyle = ComboBoxStyle.DropDownList
        StartIndex = 0
        ItemHeight = 25
        DoubleBuffered = True
        Width = 200
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Height = 20
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim b As Bitmap = New Bitmap(Width, Height)
        Dim g As Graphics = Graphics.FromImage(b)
        Dim outerrect As Rectangle = New Rectangle(0, 0, Width - 1, Height - 1)
        Dim innerrect As Rectangle = New Rectangle(2, 2, Width - 5, Height - 5)
        MyBase.OnPaint(e)
        g.Clear(BackColor)
        g.FillPath(New SolidBrush(Color.FromArgb(26, 25, 21)), RoundRect(outerrect, 3))
        g.DrawPath(New Pen(Brushes.Black), RoundRect(outerrect, 3))
        Dim rectgb As New LinearGradientBrush(innerrect, Color.FromArgb(100, 90, 80), Color.FromArgb(48, 43, 39), 90S)
        g.FillPath(rectgb, RoundRect(innerrect, 3))
        g.DrawPath(New Pen(Brushes.Black), RoundRect(innerrect, 3))
        g.SetClip(RoundRect(innerrect, 3))
        g.FillPath(rectgb, RoundRect(innerrect, 3))
        g.DrawPath(New Pen(Brushes.Black), RoundRect(innerrect, 3))
        g.ResetClip()
        g.DrawLine(New Pen(Color.FromArgb(246, 180, 12)), Width - 9, 10, Width - 22, 10)
        g.DrawLine(New Pen(Color.FromArgb(246, 180, 12)), Width - 9, 11, Width - 22, 11)
        g.DrawLine(New Pen(Color.FromArgb(246, 180, 12)), Width - 9, 15, Width - 22, 15)
        g.DrawLine(New Pen(Color.FromArgb(246, 180, 12)), Width - 9, 16, Width - 22, 16)
        g.DrawLine(New Pen(Color.FromArgb(246, 180, 12)), Width - 9, 20, Width - 22, 20)
        g.DrawLine(New Pen(Color.FromArgb(246, 180, 12)), Width - 9, 21, Width - 22, 21)
        g.DrawLine(New Pen(Color.FromArgb(246, 180, 12)), New Point(Width - 29, 7), New Point(Width - 29, Height - 7))
        g.DrawLine(New Pen(Color.FromArgb(246, 180, 12)), New Point(Width - 30, 7), New Point(Width - 30, Height - 7))
        Try
            g.DrawString(Text, New Font("Segoe UI", 11, FontStyle.Bold), New SolidBrush(Color.FromArgb(212, 212, 212)), innerrect, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
        Catch : End Try
        e.Graphics.DrawImage(b, New Point(0, 0))
        g.Dispose() : b.Dispose()
    End Sub
End Class

Public Class ButterscotchTextBox : Inherits Control
    Dim WithEvents _tb As New TextBox
    Private _allowpassword As Boolean = False
    Public Shadows Property UseSystemPasswordChar() As Boolean
        Get
            Return _allowpassword
        End Get
        Set(ByVal value As Boolean)
            _tb.UseSystemPasswordChar = UseSystemPasswordChar
            _allowpassword = value
            Invalidate()
        End Set
    End Property

    Private _maxChars As Integer = 32767
    Public Shadows Property MaxLength() As Integer
        Get
            Return _maxChars
        End Get
        Set(ByVal value As Integer)
            _maxChars = value
            _tb.MaxLength = MaxLength
            Invalidate()
        End Set
    End Property

    Private _textAlignment As HorizontalAlignment
    Public Shadows Property TextAlign() As HorizontalAlignment
        Get
            Return _textAlignment
        End Get
        Set(ByVal value As HorizontalAlignment)
            _textAlignment = value
            Invalidate()
        End Set
    End Property

    Private _multiLine As Boolean = False
    Public Shadows Property MultiLine() As Boolean
        Get
            Return _multiLine
        End Get
        Set(ByVal value As Boolean)
            _multiLine = value
            _tb.Multiline = value
            OnResize(EventArgs.Empty)
            Invalidate()
        End Set
    End Property

    Private _readOnly As Boolean = False
    Public Shadows Property [ReadOnly]() As Boolean
        Get
            Return _readOnly
        End Get
        Set(ByVal value As Boolean)
            _readOnly = value
            If _tb IsNot Nothing Then
                _tb.ReadOnly = value
            End If
        End Set
    End Property

    Protected Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        Invalidate()
    End Sub

    Protected Overrides Sub OnBackColorChanged(ByVal e As EventArgs)
        MyBase.OnBackColorChanged(e)
        Invalidate()
    End Sub

    Protected Overrides Sub OnForeColorChanged(ByVal e As EventArgs)
        MyBase.OnForeColorChanged(e)
        _tb.ForeColor = ForeColor
        Invalidate()
    End Sub

    Protected Overrides Sub OnFontChanged(ByVal e As EventArgs)
        MyBase.OnFontChanged(e)
        _tb.Font = Font
    End Sub

    Protected Overrides Sub OnGotFocus(ByVal e As EventArgs)
        MyBase.OnGotFocus(e)
        _tb.Focus()
    End Sub

    Private Sub TextChangeTb() Handles _tb.TextChanged
        Text = _tb.Text
    End Sub

    Private Sub TextChng() Handles MyBase.TextChanged
        _tb.Text = Text
    End Sub

    Public Sub NewTextBox()
        With _tb
            .Text = String.Empty
            .BackColor = Color.FromArgb(48, 43, 39)
            .ForeColor = ForeColor
            .TextAlign = HorizontalAlignment.Center
            .BorderStyle = BorderStyle.None
            .Location = New Point(3, 3)
            .Font = New Font("Segoe UI", 11, FontStyle.Regular)
            .Size = New Size(Width - 3, Height - 3)
            .UseSystemPasswordChar = UseSystemPasswordChar
        End With
    End Sub

    Sub New()
        MyBase.New()
        NewTextBox()
        Controls.Add(_tb)
        SetStyle(ControlStyles.UserPaint Or ControlStyles.SupportsTransparentBackColor, True)
        DoubleBuffered = True
        BackColor = Color.FromArgb(48, 43, 39)
        ForeColor = Color.FromArgb(255, 255, 255)
        Font = New Font("Segoe UI", 11, FontStyle.Regular)
        Size = New Size(200, 30)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As Windows.Forms.PaintEventArgs)
        Dim b As Bitmap = New Bitmap(Width, Height)
        Dim g As Graphics = Graphics.FromImage(b)
        Dim rect As Rectangle = New Rectangle(0, 0, Width - 1, Height - 1)
        MyBase.OnPaint(e)
        g.SmoothingMode = SmoothingMode.HighQuality
        g.InterpolationMode = InterpolationMode.HighQualityBicubic
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        With _tb
            .TextAlign = TextAlign
            .UseSystemPasswordChar = UseSystemPasswordChar
        End With
        g.FillPath(New SolidBrush(Color.FromArgb(48, 43, 39)), RoundRect(rect, 1))
        g.DrawPath(New Pen(Brushes.Black), RoundRect(rect, 1))
        e.Graphics.DrawImage(b, New Point(0, 0))
        g.Dispose() : b.Dispose()
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        If Not MultiLine Then
            Dim tbheight As Integer = _tb.Height
            _tb.Location = New Point(10, CType(((Height / 2) - (tbheight / 2) - 1), Integer))
            _tb.Size = New Size(Width - 20, tbheight)
        Else
            _tb.Location = New Point(10, 10)
            _tb.Size = New Size(Width - 20, Height - 20)
        End If
    End Sub
End Class

Public Class ButterscotchVerticalProgressBar : Inherits Control
    Private _val As Integer
    Public Property Value() As Integer
        Get
            Return _val
        End Get
        Set(ByVal v As Integer)
            If v > _max Then
                _val = _max
            ElseIf v < 0 Then
                _val = 0
            Else
                _val = v
            End If
            Invalidate()
        End Set
    End Property

    Private _max As Integer
    Public Property Maximum() As Integer
        Get
            Return _max
        End Get
        Set(ByVal v As Integer)
            If v < 1 Then
                _max = 1
            Else
                _max = v
            End If
            If v < _val Then
                _val = _max
            End If
            Invalidate()
        End Set
    End Property

    Private _showPercentage As Boolean = False
    Public Property ShowPercentage() As Boolean
        Get
            Return _showPercentage
        End Get
        Set(ByVal v As Boolean)
            _showPercentage = v
            Invalidate()
        End Set
    End Property

    Sub New()
        MyBase.New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
        Size = New Size(20, 250)
        DoubleBuffered = True
        _max = 100
    End Sub

    Protected Overrides Sub OnPaint(e As Windows.Forms.PaintEventArgs)
        Dim b As Bitmap = New Bitmap(Width, Height)
        Dim g As Graphics = Graphics.FromImage(b)
        Dim percent As Integer = CInt((Height - 1) * (_val / _max))
        Dim outerrect As Rectangle = New Rectangle(0, 0, Width - 1, Height - 1)
        Dim maininnerrect As Rectangle = New Rectangle(7, 7, Width - 15, Height - 15)
        Dim innerrect As Rectangle = New Rectangle(4, (Height - percent) + 4, Width - 9, percent - 9)
        MyBase.OnPaint(e)
        g.Clear(BackColor)
        g.SmoothingMode = SmoothingMode.HighQuality
        g.FillPath(New SolidBrush(Color.FromArgb(40, 37, 33)), RoundRect(outerrect, 5))
        g.DrawPath(New Pen(Color.FromArgb(0, 0, 0)), RoundRect(outerrect, 5))
        g.FillPath(New SolidBrush(Color.FromArgb(26, 25, 21)), RoundRect(maininnerrect, 3))
        g.DrawPath(New Pen(Color.FromArgb(0, 0, 0)), RoundRect(maininnerrect, 3))
        If percent <> 0 Then
            Try
                Dim progressgb As New LinearGradientBrush(innerrect, Color.FromArgb(91, 82, 73), Color.FromArgb(57, 52, 46), 90S)
                g.FillPath(progressgb, RoundRect(innerrect, 7))
            Catch : End Try
        End If
        If _showPercentage Then
            g.DrawString(String.Format("{0}%", _val), New Font("Segoe UI", 8, FontStyle.Bold), New SolidBrush(Color.FromArgb(246, 180, 12)), outerrect, New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
        End If
        e.Graphics.DrawImage(b, New Point(0, 0))
        g.Dispose() : b.Dispose()
    End Sub
End Class

Public Class ButterscotchHorizontalSeperator : Inherits Control

    Sub New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)
        DoubleBuffered = True
        BackColor = Color.Transparent
        Size = New Size(200, 3)
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
        Height = 3
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim b As Bitmap = New Bitmap(Width, Height)
        Dim g As Graphics = Graphics.FromImage(b)
        Dim rect As Rectangle = New Rectangle(0, 0, Width - 1, Height - 1)
        MyBase.OnPaint(e)
        g.Clear(BackColor)
        g.SmoothingMode = SmoothingMode.HighQuality
        g.InterpolationMode = InterpolationMode.HighQualityBicubic
        g.FillPath(New SolidBrush(Color.FromArgb(26, 25, 21)), RoundRect(rect, 2))
        g.DrawPath(New Pen(Color.FromArgb(0, 0, 0)), RoundRect(rect, 2))
        e.Graphics.DrawImage(b, New Point(0, 0))
        g.Dispose() : b.Dispose()
    End Sub
End Class

Public Class ButterscotchVerticalSeperator : Inherits Control

    Sub New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)
        DoubleBuffered = True
        BackColor = Color.Transparent
        Size = New Size(3, 200)
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
        Width = 3
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim b As Bitmap = New Bitmap(Width, Height)
        Dim g As Graphics = Graphics.FromImage(b)
        Dim rect As Rectangle = New Rectangle(0, 0, Width - 1, Height - 1)
        MyBase.OnPaint(e)
        g.Clear(BackColor)
        g.SmoothingMode = SmoothingMode.HighQuality
        g.InterpolationMode = InterpolationMode.HighQualityBicubic
        g.FillPath(New SolidBrush(Color.FromArgb(26, 25, 21)), RoundRect(rect, 2))
        g.DrawPath(New Pen(Color.FromArgb(0, 0, 0)), RoundRect(rect, 2))
        e.Graphics.DrawImage(b, New Point(0, 0))
        g.Dispose() : b.Dispose()
    End Sub
End Class

Public Class ButterscotchListBox : Inherits ListBox
    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or
                 ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
        DoubleBuffered = True
        DrawMode = Windows.Forms.DrawMode.OwnerDrawFixed
        ForeColor = Color.White
        BackColor = Color.FromArgb(100, 90, 80)
        BorderStyle = Windows.Forms.BorderStyle.None
        ItemHeight = 20
    End Sub
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim b As Bitmap = New Bitmap(Width, Height)
        Dim g As Graphics = Graphics.FromImage(b)
        Dim rect As Rectangle = New Rectangle(0, 0, Width - 1, Height - 1)
        MyBase.OnPaint(e)
        g.Clear(Color.Transparent)
        g.FillPath(New SolidBrush(Color.FromArgb(100, 90, 80)), RoundRect(rect, 3))
        g.DrawPath(New Pen(Color.FromArgb(26, 25, 21)), RoundRect(rect, 1))
        e.Graphics.DrawImage(b, New Point(0, 0))
        g.Dispose() : b.Dispose()
    End Sub
    Protected Overrides Sub OnDrawItem(e As DrawItemEventArgs)
        Dim b As Bitmap = New Bitmap(Width, Height)
        Dim g As Graphics = Graphics.FromImage(b)
        g.TextRenderingHint = TextRenderingHint.AntiAlias
        g.SmoothingMode = SmoothingMode.HighQuality
        g.SetClip(RoundRect(New Rectangle(0, 0, Width, Height), 3))
        g.Clear(Color.Transparent)
        g.FillRectangle(New SolidBrush(BackColor), New Rectangle(e.Bounds.X, e.Bounds.Y - 1, e.Bounds.Width, e.Bounds.Height + 3))
        If e.State.ToString().Contains("Selected,") Then
            Dim selectgb As New LinearGradientBrush(New Rectangle(e.Bounds.X, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height), Color.FromArgb(100, 90, 80), Color.FromArgb(48, 43, 39), 90S)
            g.FillRectangle(selectgb, New Rectangle(e.Bounds.X, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height))
            g.DrawRectangle(New Pen(Color.FromArgb(212, 212, 212)), New Rectangle(e.Bounds.X, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height))
        Else
            Dim nonselectgb As New LinearGradientBrush(New Rectangle(e.Bounds.X, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height), Color.FromArgb(48, 43, 39), Color.FromArgb(100, 90, 80), 90S)
            g.FillRectangle(nonselectgb, e.Bounds)
        End If
        Try
            g.DrawString(Items(e.Index).ToString(), New Font("Segoe UI", 10, FontStyle.Regular), New SolidBrush(Color.FromArgb(255, 255, 255)), New Rectangle(e.Bounds.X + 3, e.Bounds.Y + 1, e.Bounds.Width, e.Bounds.Height), New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Center})
        Catch : End Try
        g.DrawPath(New Pen(Color.FromArgb(26, 25, 21), 2), RoundRect(New Rectangle(0, 0, Width - 1, Height - 1), 1))
        e.Graphics.DrawImage(b, New Point(0, 0))
        g.Dispose() : b.Dispose()
    End Sub
End Class

Public Class ButterscotchProgressButton : Inherits Control
    Dim _state As MouseState
    Private _val As Integer
    Public Property Value() As Integer
        Get
            Return _val
        End Get
        Set(ByVal v As Integer)
            If v > _max Then
                _val = _max
            ElseIf v < 0 Then
                _val = 0
            Else
                _val = v
            End If
            Invalidate()
        End Set
    End Property

    Private _max As Integer
    Public Property Maximum() As Integer
        Get
            Return _max
        End Get
        Set(ByVal v As Integer)
            If v < 1 Then
                _max = 1
            Else
                _max = v
            End If
            If v < _val Then
                _val = _max
            End If
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        _state = MouseState.Down
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        _state = MouseState.Over
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseEnter(ByVal e As EventArgs)
        MyBase.OnMouseEnter(e)
        _state = MouseState.Over
        Invalidate()
    End Sub

    Protected Overrides Sub OnMouseLeave(ByVal e As EventArgs)
        MyBase.OnMouseLeave(e)
        _state = MouseState.None
        Invalidate()
    End Sub
    Sub New()
        MyBase.New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
        Size = New Size(160, 50)
        DoubleBuffered = True
        _max = 100
    End Sub

    Protected Overrides Sub OnPaint(e As Windows.Forms.PaintEventArgs)
        Dim b As Bitmap = New Bitmap(Width, Height)
        Dim g As Graphics = Graphics.FromImage(b)
        Dim percent As Integer = CInt((Width - 1) * (_val / _max))
        Dim rect As Rectangle = New Rectangle(0, 0, Width - 1, Height - 1)
        Dim innerrect As Rectangle = New Rectangle(3, 3, Width - 7, Height - 7)
        Dim progressinnerrect As Rectangle = New Rectangle(4, 4, percent - 9, Height - 9)
        Dim btnfont As New Font("Segoe UI", 10, FontStyle.Regular)
        MyBase.OnPaint(e)
        g.SmoothingMode = SmoothingMode.HighQuality
        g.InterpolationMode = InterpolationMode.HighQualityBicubic
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        g.Clear(BackColor)
        Dim buttonrect As New LinearGradientBrush(rect, Color.FromArgb(100, 90, 80), Color.FromArgb(48, 43, 39), LinearGradientMode.Vertical)
        g.FillPath(New SolidBrush(Color.FromArgb(26, 25, 21)), RoundRect(rect, 3))
        g.FillPath(buttonrect, RoundRect(innerrect, 3))
        Select Case _state
            Case MouseState.None
                Dim buttonrectnone As New LinearGradientBrush(innerrect, Color.FromArgb(100, 90, 80), Color.FromArgb(48, 43, 39), LinearGradientMode.Vertical)
                g.FillPath(New SolidBrush(Color.FromArgb(26, 25, 21)), RoundRect(rect, 3))
                g.FillPath(buttonrectnone, RoundRect(innerrect, 3))
                g.DrawString(Text, btnfont, Brushes.White, innerrect, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
            Case MouseState.Down
                Dim buttonrectdown As New LinearGradientBrush(innerrect, Color.FromArgb(48, 43, 39), Color.FromArgb(100, 90, 80), LinearGradientMode.Vertical)
                g.FillPath(New SolidBrush(Color.FromArgb(26, 25, 21)), RoundRect(rect, 3))
                g.FillPath(buttonrectdown, RoundRect(innerrect, 3))
                g.DrawString(Text, btnfont, Brushes.White, innerrect, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
            Case MouseState.Over
                Dim buttonrectover As New LinearGradientBrush(innerrect, Color.FromArgb(48, 43, 39), Color.FromArgb(100, 90, 80), LinearGradientMode.Vertical)
                g.FillPath(New SolidBrush(Color.FromArgb(26, 25, 21)), RoundRect(rect, 3))
                g.FillPath(buttonrectover, RoundRect(innerrect, 3))
                g.DrawString(Text, btnfont, Brushes.White, innerrect, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
        End Select
        If percent <> 0 Then
            Dim progressgb As New LinearGradientBrush(progressinnerrect, Color.FromArgb(91, 82, 73), Color.FromArgb(57, 52, 46), 180S)
            g.FillPath(progressgb, RoundRect(progressinnerrect, 3))
            g.DrawPath(New Pen(Color.FromArgb(0, 0, 0)), RoundRect(progressinnerrect, 3))
            g.DrawString(Text, btnfont, Brushes.White, innerrect, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
        End If
        e.Graphics.DrawImage(b, New Point(0, 0))
        g.Dispose() : b.Dispose()
    End Sub
End Class

Public Class ButterscotchAlertBox : Inherits Control
    Dim WithEvents _mytimer As Windows.Forms.Timer

    Enum AlertStyle
        [Info]
        [Success]
        [Error]
    End Enum

    Private _style As AlertStyle
    Public Property Style As AlertStyle
        Get
            Return _style
        End Get
        Set(value As AlertStyle)
            _style = value
        End Set
    End Property

    Private _text As String
    Overrides Property Text As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)
            MyBase.Text = value
            If _text IsNot Nothing Then
                _text = value
            End If
        End Set
    End Property

    Shadows Property Visible As Boolean
        Get
            Return MyBase.Visible = False
        End Get
        Set(value As Boolean)
            MyBase.Visible = value
        End Set
    End Property

    Protected Overrides Sub OnTextChanged(e As EventArgs)
        MyBase.OnTextChanged(e) : Invalidate()
    End Sub

    Public Sub ShowAlertBox(mystyle As AlertStyle, str As String, interval As Integer)
        _style = mystyle
        Text = str
        Visible = True
        _mytimer.Interval = interval
        _mytimer.Enabled = True
    End Sub

    Sub New()
        MyBase.New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.ResizeRedraw Or ControlStyles.SupportsTransparentBackColor, True)
        BackColor = Color.Transparent
        Size = New Size(450, 30)
        DoubleBuffered = True
        _mytimer = New Timer()
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim b As Bitmap = New Bitmap(Width, Height)
        Dim g As Graphics = Graphics.FromImage(b)
        Dim rect As New Rectangle(0, 0, Width - 1, Height - 1)
        Dim textrect As New Rectangle(20, 10, Width - 21, Height - 21)
        Dim logorect As New Rectangle(7, 7, 20, 20)
        Dim logocirclerect As New Rectangle(5, 5, 20, 20)
        g.TextRenderingHint = TextRenderingHint.AntiAlias
        g.SmoothingMode = SmoothingMode.HighQuality
        g.Clear(Color.Transparent)
        MyBase.OnPaint(e)
        Select Case _style
            Case AlertStyle.Success
                g.FillRectangle(New SolidBrush(Color.FromArgb(40, 0, 255, 0)), rect)
                g.DrawRectangle(New Pen(Color.FromArgb(0, 0, 0)), rect)
                g.DrawString(Text, New Font("Segoe UI", 10, FontStyle.Bold), New SolidBrush(Color.FromArgb(255, 255, 255)), textrect, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                g.DrawString("ü", New Font("Wingdings", 14), New SolidBrush(Color.FromArgb(255, 255, 255)), logorect, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
            Case AlertStyle.Error
                g.FillRectangle(New SolidBrush(Color.FromArgb(40, 255, 0, 0)), rect)
                g.DrawRectangle(New Pen(Color.FromArgb(0, 0, 0)), rect)
                g.DrawString(Text, New Font("Segoe UI", 10, FontStyle.Bold), New SolidBrush(Color.FromArgb(255, 255, 255)), textrect, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                g.DrawString("r", New Font("Marlett", 10), New SolidBrush(Color.FromArgb(255, 255, 255)), logorect, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
            Case AlertStyle.Info
                g.FillRectangle(New SolidBrush(Color.FromArgb(40, 0, 0, 255)), rect)
                g.DrawRectangle(New Pen(Color.FromArgb(0, 0, 0)), rect)
                g.DrawString(Text, New Font("Segoe UI", 10, FontStyle.Bold), New SolidBrush(Color.FromArgb(255, 255, 255)), textrect, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                g.DrawString("i", New Font("Segoe UI", 12), New SolidBrush(Color.FromArgb(255, 255, 255)), logorect, New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
        End Select
        g.DrawEllipse(New Pen(Color.FromArgb(255, 255, 255)), logocirclerect)
        e.Graphics.DrawImage(b, New Point(0, 0))
        g.Dispose() : b.Dispose()
    End Sub

    Private Sub MyTimer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles _mytimer.Tick
        Visible = False
        _mytimer.Enabled = False
    End Sub
End Class