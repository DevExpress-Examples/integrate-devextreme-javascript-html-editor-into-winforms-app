Imports System
Imports DevExpress.XtraSplashScreen

Namespace DXHtmlEditor

    Public Partial Class MainForm
        Inherits DevExpress.XtraBars.Ribbon.RibbonForm

        Public Sub New()
            InitializeComponent()
            AddHandler dxHtmlEditorWebView.HtmlChanged, AddressOf OnDxHtmlEditorWebViewHtmlChanged
            AddHandler dxHtmlEditorWebView.HtmlLoaded, AddressOf OnDxHtmlEditorWebViewHtmlLoaded
        End Sub

        Protected Overrides Sub OnLoad(ByVal e As EventArgs)
            MyBase.OnLoad(e)
            ShowOverlay()
            sourceMemoEdit.Text = "Hello DevExpress Html Editor!"
        End Sub

        Private Async Sub OnSourceMemoEditTextChanged(ByVal sender As Object, ByVal e As EventArgs)
            Await dxHtmlEditorWebView.SetHtmlText(sourceMemoEdit.Text)
        End Sub

        Private Async Sub OnUndo(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
            Await dxHtmlEditorWebView.Undo()
        End Sub

        Private Async Sub OnRedo(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs)
            Await dxHtmlEditorWebView.Redo()
        End Sub

        Protected Overrides Sub OnLookAndFeelChangedCore()
            UpdateDxHtmlEditorWebViewTheme()
            MyBase.OnLookAndFeelChangedCore()
        End Sub

        Private ReadOnly Property UseDarkTheme As Boolean
            Get
                Return DevExpress.Utils.Frames.FrameHelper.IsDarkSkin(GetActiveLookAndFeel())
            End Get
        End Property

        Private Async Sub UpdateDxHtmlEditorWebViewTheme()
            Dim themeName As String = If(UseDarkTheme, "dx.dark.css", "dx.light.css")
            If dxHtmlEditorWebView.CanSetTheme(themeName) Then
                ShowOverlay()
                Await dxHtmlEditorWebView.SetTheme(themeName)
            End If
        End Sub

        Private Async Sub OnDxHtmlEditorWebViewHtmlChanged(ByVal sender As Object, ByVal e As EventArgs)
            targetMemoEdit.Text = Await dxHtmlEditorWebView.GetHtmlText()
        End Sub

        Private Sub OnDxHtmlEditorWebViewHtmlLoaded(ByVal sender As Object, ByVal e As EventArgs)
            CloseOverlay()
        End Sub

        Private overlayHandle As IOverlaySplashScreenHandle

        Private ReadOnly options As OverlayWindowOptions = New OverlayWindowOptions(opacity:=100R / 255)

        Private Sub ShowOverlay()
            overlayHandle = If(overlayHandle, SplashScreenManager.ShowOverlayForm(layoutControl, options))
        End Sub

        Private Sub CloseOverlay()
            overlayHandle?.Close()
            overlayHandle = Nothing
        End Sub
    End Class
End Namespace
