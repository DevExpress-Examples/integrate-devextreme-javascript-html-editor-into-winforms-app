Imports System
Imports System.Threading.Tasks
Imports DevExpress.Utils
Imports Microsoft.Web.WebView2.Core
Imports Microsoft.Web.WebView2.WinForms

Namespace DXHtmlEditor

    Public Class DXHtmlEditorWebView
        Inherits WebView2
        Implements IDXHtmlEditorAPI

        Private client As DXHtmlEditorClient

        Public Sub New()
            If Not DesignMode Then Initialize()
        End Sub

#Region "API"
        Public Async Function GetHtmlText() As Task(Of String)
            Await EnsureIsLoaded()
            Return Await client.GetHtmlText()
        End Function

        Public Async Function SetHtmlText(ByVal htmlString As String) As Task(Of String)
            Await EnsureIsLoaded()
            Return Await client.SetHtmlText(htmlString)
        End Function

        Public Function CanSetTheme(ByVal themeName As String) As Boolean
            Return Not Equals(client.ThemeName, themeName)
        End Function

        Public Async Function SetTheme(ByVal themeName As String) As Task
            Dim htmlText As String = Await GetHtmlText()
            StartLoading()
            client.ThemeName = themeName
            Await SetHtmlText(htmlText)
            Refresh()
        End Function

#End Region  ' API
#Region "Custom API Sample"
        Public Async Function Undo() As Task
            Await EnsureIsLoaded()
            Await client.Undo()
        End Function

        Public Async Function Redo() As Task
            Await EnsureIsLoaded()
            Await client.Redo()
        End Function

#End Region
#Region "Events"
        Private Shared ReadOnly htmlChangedField As Object = New Object()

        Private Shared ReadOnly htmlLoadedField As Object = New Object()

        '
        Public Custom Event HtmlChanged As EventHandler
            AddHandler(ByVal value As EventHandler)
                Events.AddHandler(htmlChangedField, value)
            End AddHandler

            RemoveHandler(ByVal value As EventHandler)
                Events.RemoveHandler(htmlChangedField, value)
            End RemoveHandler

            RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
            End RaiseEvent
        End Event

        Public Custom Event HtmlLoaded As EventHandler
            AddHandler(ByVal value As EventHandler)
                Events.AddHandler(htmlLoadedField, value)
            End AddHandler

            RemoveHandler(ByVal value As EventHandler)
                Events.RemoveHandler(htmlLoadedField, value)
            End RemoveHandler

            RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
            End RaiseEvent
        End Event

        Private Sub RaiseOnHtmlChanged() Implements IDXHtmlEditorAPI.RaiseOnHtmlChanged
            TryCast(Events(htmlChangedField), EventHandler)?.Invoke(Me, EventArgs.Empty)
        End Sub

        Private Sub RaiseOnLoaded() Implements IDXHtmlEditorAPI.RaiseOnLoaded
            loadedSource?.TrySetResult(True)
            TryCast(Events(htmlLoadedField), EventHandler)?.Invoke(Me, EventArgs.Empty)
        End Sub

#End Region  ' Events
#Region "Internal"
        Private Sub Initialize()
            StartLoading()
            client = New DXHtmlEditorClient(Me)
            AddHandler CoreWebView2InitializationCompleted, AddressOf OnCoreWebView2InitializationCompleted
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            RemoveHandler CoreWebView2InitializationCompleted, AddressOf OnCoreWebView2InitializationCompleted
            loadedSource?.TrySetCanceled()
            loadedSource = Nothing
            DisposeHelper.Dispose(client)
            MyBase.Dispose(disposing)
        End Sub

        Protected Overrides Sub OnHandleCreated(ByVal e As EventArgs)
            MyBase.OnHandleCreated(e)
            If Not DesignMode Then EnsureCoreWebView2Async()
        End Sub

        Protected Overrides Sub OnHandleDestroyed(ByVal e As EventArgs)
            MyBase.OnHandleDestroyed(e)
            loadedSource?.TrySetCanceled()
        End Sub

        Private Sub OnCoreWebView2InitializationCompleted(ByVal sender As Object, ByVal e As CoreWebView2InitializationCompletedEventArgs)
            client.Initialize(CoreWebView2)
        End Sub

        Private loadedSource As TaskCompletionSource(Of Boolean)

        Private Sub StartLoading()
            Dim prevSource = loadedSource
            loadedSource = New TaskCompletionSource(Of Boolean)(TaskCreationOptions.RunContinuationsAsynchronously)
            If prevSource IsNot Nothing AndAlso Not prevSource.Task.IsCompleted Then prevSource.TrySetCanceled()
        End Sub

        Private Async Function EnsureIsLoaded() As Task
            Try
                Dim loadingTask As Task(Of Boolean) = loadedSource?.Task
                Dim loaded As Boolean = Await loadingTask
                If Not loaded OrElse loadingTask.IsFaulted Then Throw New Exception("DXHtmlEditor loading failed", loadingTask?.Exception)
            Catch e As TaskCanceledException
                If e.Task IsNot loadedSource?.Task Then Await EnsureIsLoaded()
            End Try
        End Function
#End Region
    End Class
End Namespace
