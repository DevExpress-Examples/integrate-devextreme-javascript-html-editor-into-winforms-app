Imports System
Imports System.IO
Imports System.Text
Imports System.Threading.Tasks
Imports Microsoft.Web.WebView2.Core
Imports System.Runtime.InteropServices

Namespace DXHtmlEditor

    Friend NotInheritable Class DXHtmlEditorClient
        Implements IDisposable

        Const rootURI As String = "https://htmleditor/index.html"

        Const rootURIFilter As String = "https://htmleditor/*"

        Const ThemeNameReplace As String = "$ThemeName$"

        Const WebMessage_HtmlChanged As String = """HtmlChanged"""

        Const WebMessage_Loaded As String = """Loaded"""

        Private ReadOnly editorAPI As IDXHtmlEditorAPI

        Private themeNameField As String = "dx.light.css"

        Private webView As CoreWebView2

        Public Sub New(ByVal editorAPI As IDXHtmlEditorAPI)
            Me.editorAPI = editorAPI
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            ReleaseWebView()
        End Sub

        Public Property ThemeName As String
            Get
                Return themeNameField
            End Get

            Set(ByVal value As String)
                If Equals(themeNameField, value) Then Return
                themeNameField = value
                webView?.Reload()
            End Set
        End Property

        Private Async Function CallDxHtmlEditClient(ByVal method As String) As Task(Of String)
            Dim result As String = Await webView?.ExecuteScriptAsync($"{method}")
            If Not String.IsNullOrEmpty(result) AndAlso result.Length > 3 AndAlso result(0) = """"c AndAlso result(result.Length - 1) = """"c Then Return result.Substring(1, result.Length - 2)
            Return result
        End Function

        Private Sub OnWebMessageReceived(ByVal sender As Object, ByVal e As CoreWebView2WebMessageReceivedEventArgs)
            If Equals(e.WebMessageAsJson, WebMessage_Loaded) Then editorAPI?.RaiseOnLoaded()
            If Equals(e.WebMessageAsJson, WebMessage_HtmlChanged) Then editorAPI.RaiseOnHtmlChanged()
        End Sub

        Private Sub OnWebResourceRequested(ByVal sender As Object, ByVal e As CoreWebView2WebResourceRequestedEventArgs)
            Dim environment = webView?.Environment
            If environment Is Nothing Then Return
            Dim asset As String = $"{NameOf(DXHtmlEditor)}.{NameOf(DXHtmlEditor)}.Assets.{e.Request.Uri.Substring(rootURIFilter.Length - 1)}"
            Dim allowSetTheme As Boolean = Nothing
            Try
                Dim resourceStream As Stream = GetType(DXHtmlEditorClient).Assembly.GetManifestResourceStream(asset)
                If resourceStream Is Nothing Then
                    e.Response = NotFoundResponse(environment)
                    Return
                End If

                Dim headers As String = GetHeaders(asset, allowSetTheme)
                Dim content As Stream = If(allowSetTheme, GetContent(resourceStream, ThemeName), resourceStream)
                e.Response = ContentResponse(environment, headers, content)
            Catch
                e.Response = NotFoundResponse(environment)
            End Try
        End Sub

        Private Sub ReleaseWebView()
            If webView IsNot Nothing Then
                RemoveHandler webView.WebResourceRequested, AddressOf OnWebResourceRequested
                RemoveHandler webView.WebMessageReceived, AddressOf OnWebMessageReceived
                webView = Nothing
            End If
        End Sub

        Public Sub Initialize(ByVal webView As CoreWebView2)
            ReleaseWebView()
            Me.webView = webView
            If webView IsNot Nothing Then
                webView.Settings.AreDefaultScriptDialogsEnabled = True
                webView.Settings.IsScriptEnabled = True
#If Not DEBUG
                webView.Settings.AreDevToolsEnabled = false;
#End If
                AddHandler webView.WebResourceRequested, AddressOf OnWebResourceRequested
                AddHandler webView.WebMessageReceived, AddressOf OnWebMessageReceived
                webView.AddWebResourceRequestedFilter(rootURIFilter, CoreWebView2WebResourceContext.All)
                webView.Navigate(rootURI)
            End If
        End Sub

        Public Async Function GetHtmlText() As Task(Of String)
            Dim base64String As String = Await CallDxHtmlEditClient("getHtmlBase64()")
            Return Encoding.UTF8.GetString(Convert.FromBase64String(base64String))
        End Function

        Public Async Function SetHtmlText(ByVal htmlString As String) As Task(Of String)
            Dim base64String As String = Convert.ToBase64String(Encoding.UTF8.GetBytes(htmlString))
            Return Await CallDxHtmlEditClient($"setHtmlFromBase64(""{base64String}"")")
        End Function

        Public Async Function Undo() As Task
            Await CallDxHtmlEditClient("undo()")
        End Function

        Public Async Function Redo() As Task
            Await CallDxHtmlEditClient("redo()")
        End Function

#Region "Utils"
        Private Shared Function ContentResponse(ByVal environment As CoreWebView2Environment, ByVal headers As String, ByVal content As Stream) As CoreWebView2WebResourceResponse
            Return environment.CreateWebResourceResponse(New ReadOnlyStream(content), 200, "OK", headers)
        End Function

        Private Shared Function NotFoundResponse(ByVal environment As CoreWebView2Environment) As CoreWebView2WebResourceResponse
            Return environment.CreateWebResourceResponse(Nothing, 404, "Not found", String.Empty)
        End Function

        Private Shared Function GetHeaders(ByVal asset As String, <Out> ByRef allowSetTheme As Boolean) As String
            allowSetTheme = False
            If asset.EndsWith(".html", StringComparison.OrdinalIgnoreCase) Then
                allowSetTheme = True
                Return "Content-Type: text/html"
            ElseIf asset.EndsWith(".css", StringComparison.OrdinalIgnoreCase) Then
                Return "Content-type: text/css"
            ElseIf asset.EndsWith(".js", StringComparison.OrdinalIgnoreCase) Then
                Return "Content-Type: application/javascript"
            End If

            Return String.Empty
        End Function

        Private Shared Function GetContent(ByVal resourceStream As Stream, ByVal theme As String) As Stream
            Using reader = New StreamReader(resourceStream)
                Dim content As String = reader.ReadToEnd().Replace(ThemeNameReplace, theme)
                resourceStream.Dispose()
                Return New MemoryStream(Encoding.UTF8.GetBytes(content))
            End Using
        End Function

        Private NotInheritable Class ReadOnlyStream
            Inherits Stream

            Private ReadOnly resource As Stream

            Public Sub New(ByVal source As Stream)
                resource = source
            End Sub

            Public Overrides ReadOnly Property CanRead As Boolean
                Get
                    Return resource.CanRead
                End Get
            End Property

            Public Overrides ReadOnly Property CanSeek As Boolean
                Get
                    Return resource.CanSeek
                End Get
            End Property

            Public Overrides ReadOnly Property CanWrite As Boolean
                Get
                    Return False
                End Get
            End Property

            Public Overrides ReadOnly Property Length As Long
                Get
                    Return resource.Length
                End Get
            End Property

            Public Overrides Property Position As Long
                Get
                    Return resource.Position
                End Get

                Set(ByVal value As Long)
                    resource.Position = value
                End Set
            End Property

            Public Overrides Function Read(ByVal buffer As Byte(), ByVal offset As Integer, ByVal count As Integer) As Integer
                Try
                    Dim lRead As Integer = resource.Read(buffer, offset, count)
                    If lRead = 0 Then resource.Dispose()
                    Return lRead
                Catch
                    resource.Dispose()
                    Throw
                End Try
            End Function

            Public Overrides Function Seek(ByVal offset As Long, ByVal origin As SeekOrigin) As Long
                Return resource.Seek(offset, origin)
            End Function

            Public Overrides Sub SetLength(ByVal value As Long)
                Throw New NotSupportedException()
            End Sub

            Public Overrides Sub Write(ByVal buffer As Byte(), ByVal offset As Integer, ByVal count As Integer)
                Throw New NotSupportedException()
            End Sub

            Public Overrides Sub Flush()
                Throw New NotSupportedException()
            End Sub
        End Class
#End Region  ' Utils
    End Class
End Namespace
