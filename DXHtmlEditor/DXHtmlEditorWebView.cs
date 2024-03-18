using System;
using System.Threading.Tasks;
using DevExpress.Utils;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace DXHtmlEditor {
    public class DXHtmlEditorWebView : WebView2, IDXHtmlEditorAPI {
        DXHtmlEditorClient client;
        public DXHtmlEditorWebView() {
            if(!DesignMode)
                Initialize();
        }
        #region API
        public async Task<string> GetHtmlText() {
            await EnsureIsLoaded();
            return await client.GetHtmlText();
        }
        public async Task<string> SetHtmlText(string htmlString) {
            await EnsureIsLoaded();
            return await client.SetHtmlText(htmlString);
        }
        public bool CanSetTheme(string themeName) {
            return client.ThemeName != themeName;
        }
        public async Task SetTheme(string themeName) {
            string htmlText = await GetHtmlText();
            StartLoading();
            client.ThemeName = themeName;
            await SetHtmlText(htmlText);
            Refresh();
        }
        #endregion API
        #region Custom API Sample
        public async Task Undo() {
            await EnsureIsLoaded();
            await client.Undo();
        }
        public async Task Redo() {
            await EnsureIsLoaded();
            await client.Redo();
        }
        #endregion
        #region Events
        static readonly object htmlChanged = new object();
        static readonly object htmlLoaded = new object();
        //
        public event EventHandler HtmlChanged {
            add { Events.AddHandler(htmlChanged, value); }
            remove { Events.RemoveHandler(htmlChanged, value); }
        }
        public event EventHandler HtmlLoaded {
            add { Events.AddHandler(htmlLoaded, value); }
            remove { Events.RemoveHandler(htmlLoaded, value); }
        }
        void IDXHtmlEditorAPI.RaiseOnHtmlChanged() {
            (Events[htmlChanged] as EventHandler)?.Invoke(this, EventArgs.Empty);
        }
        void IDXHtmlEditorAPI.RaiseOnLoaded() {
            loadedSource?.TrySetResult(true);
            (Events[htmlLoaded] as EventHandler)?.Invoke(this, EventArgs.Empty);
        }
        #endregion Events
        #region Internal
        void Initialize() {
            StartLoading();
            client = new DXHtmlEditorClient(this);
            CoreWebView2InitializationCompleted += OnCoreWebView2InitializationCompleted;
        }
        protected override void Dispose(bool disposing) {
            CoreWebView2InitializationCompleted -= OnCoreWebView2InitializationCompleted;
            loadedSource?.TrySetCanceled();
            loadedSource = null;
            DisposeHelper.Dispose(ref client);
            base.Dispose(disposing);
        }
        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);
            if(!DesignMode)
                EnsureCoreWebView2Async();
        }
        protected override void OnHandleDestroyed(EventArgs e) {
            base.OnHandleDestroyed(e);
            loadedSource?.TrySetCanceled();
        }
        void OnCoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e) {
            client.Initialize(CoreWebView2);
        }
        TaskCompletionSource<bool> loadedSource;
        void StartLoading() {
            var prevSource = loadedSource;
            loadedSource = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
            if(prevSource != null && !prevSource.Task.IsCompleted)
                prevSource.TrySetCanceled();
        }
        async Task EnsureIsLoaded() {
            try {
                Task<bool> loadingTask = loadedSource?.Task;
                bool loaded = await loadingTask;
                if(!loaded || loadingTask.IsFaulted)
                    throw new Exception("DXHtmlEditor loading failed", loadingTask?.Exception);
            }
            catch(TaskCanceledException e) {
                if(e.Task != loadedSource?.Task)
                    await EnsureIsLoaded();
            }
        }
        #endregion
    }
}