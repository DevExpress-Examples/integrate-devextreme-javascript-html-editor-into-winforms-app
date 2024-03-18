using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.Core;

namespace DXHtmlEditor {
    sealed class DXHtmlEditorClient : IDisposable {
        const string rootURI = @"https://htmleditor/index.html";
        const string rootURIFilter = @"https://htmleditor/*";
        const string ThemeNameReplace = "$ThemeName$";
        const string WebMessage_HtmlChanged = "\"HtmlChanged\"";
        const string WebMessage_Loaded = "\"Loaded\"";

        readonly IDXHtmlEditorAPI editorAPI;
        string themeName = "dx.light.css";
        CoreWebView2 webView;
        public DXHtmlEditorClient(IDXHtmlEditorAPI editorAPI) {
            this.editorAPI = editorAPI;
        }
        public void Dispose() {
            ReleaseWebView();
        }
        public string ThemeName {
            get => themeName;
            set {
                if(themeName == value)
                    return;
                themeName = value;
                webView?.Reload();
            }
        }
        async Task<string> CallDxHtmlEditClient(string method) {
            string result = await webView?.ExecuteScriptAsync($@"{method}");
            if(!string.IsNullOrEmpty(result) && result.Length > 3 && result[0] == '\"' && result[result.Length - 1] == '\"')
                return result.Substring(1, result.Length - 2);
            return result;
        }
        void OnWebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e) {
            if(e.WebMessageAsJson == WebMessage_Loaded)
                editorAPI?.RaiseOnLoaded();
            if(e.WebMessageAsJson == WebMessage_HtmlChanged)
                editorAPI.RaiseOnHtmlChanged();
        }
        void OnWebResourceRequested(object sender, CoreWebView2WebResourceRequestedEventArgs e) {
            var environment = webView?.Environment;
            if(environment == null)
                return;
            string asset = $@"{nameof(DXHtmlEditor)}.{nameof(DXHtmlEditor)}.Assets.{e.Request.Uri.Substring(rootURIFilter.Length - 1)}";
            try {
                Stream resourceStream = typeof(DXHtmlEditorClient).Assembly.GetManifestResourceStream(asset);
                if(resourceStream == null) {
                    e.Response = NotFoundResponse(environment);
                    return;
                }
                string headers = GetHeaders(asset, out bool allowSetTheme);
                Stream content = allowSetTheme ?
                    GetContent(resourceStream, ThemeName) : resourceStream;
                e.Response = ContentResponse(environment, headers, content);
            }
            catch {
                e.Response = NotFoundResponse(environment);
            }
        }
        void ReleaseWebView() {
            if(webView != null) {
                webView.WebResourceRequested -= OnWebResourceRequested;
                webView.WebMessageReceived -= OnWebMessageReceived;
                webView = null;
            }
        }
        public void Initialize(CoreWebView2 webView) {
            ReleaseWebView();
            this.webView = webView;
            if(webView != null) {
                webView.Settings.AreDefaultScriptDialogsEnabled = true;
                webView.Settings.IsScriptEnabled = true;
#if !DEBUG
                webView.Settings.AreDevToolsEnabled = false;
#endif
                webView.WebResourceRequested += OnWebResourceRequested;
                webView.WebMessageReceived += OnWebMessageReceived;
                webView.AddWebResourceRequestedFilter(rootURIFilter, CoreWebView2WebResourceContext.All);
                webView.Navigate(rootURI);
            }
        }
        public async Task<string> GetHtmlText() {
            string base64String = await CallDxHtmlEditClient("getHtmlBase64()");
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64String));
        }
        public async Task<string> SetHtmlText(string htmlString) {
            string base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(htmlString));
            return await CallDxHtmlEditClient($"setHtmlFromBase64(\"{base64String}\")");
        }
        public async Task Undo() {
            await CallDxHtmlEditClient("undo()");
        }
        public async Task Redo() {
            await CallDxHtmlEditClient("redo()");
        }
        #region Utils
        static CoreWebView2WebResourceResponse ContentResponse(CoreWebView2Environment environment, string headers, Stream content) {
            return environment.CreateWebResourceResponse(new ReadOnlyStream(content), 200, "OK", headers);
        }
        static CoreWebView2WebResourceResponse NotFoundResponse(CoreWebView2Environment environment) {
            return environment.CreateWebResourceResponse(null, 404, "Not found", string.Empty);
        }
        static string GetHeaders(string asset, out bool allowSetTheme) {
            allowSetTheme = false;
            if(asset.EndsWith(".html", StringComparison.OrdinalIgnoreCase)) {
                allowSetTheme = true;
                return "Content-Type: text/html";
            }
            else if(asset.EndsWith(".css", StringComparison.OrdinalIgnoreCase)) {
                return "Content-type: text/css";
            }
            else if(asset.EndsWith(".js", StringComparison.OrdinalIgnoreCase)) {
                return "Content-Type: application/javascript";
            }
            return string.Empty;
        }
        static Stream GetContent(Stream resourceStream, string theme) {
            using(var reader = new StreamReader(resourceStream)) {
                string content = reader.ReadToEnd()
                    .Replace(ThemeNameReplace, theme);
                resourceStream.Dispose();
                return new MemoryStream(Encoding.UTF8.GetBytes(content));
            }
        }
        sealed class ReadOnlyStream : Stream {
            readonly Stream resource;
            public ReadOnlyStream(Stream source) {
                this.resource = source;
            }
            public override bool CanRead => resource.CanRead;
            public override bool CanSeek => resource.CanSeek;
            public override bool CanWrite => false;
            public override long Length => resource.Length;
            public override long Position {
                get => resource.Position;
                set => resource.Position = value;
            }
            public override int Read(byte[] buffer, int offset, int count) {
                try {
                    int read = resource.Read(buffer, offset, count);
                    if(read == 0)
                        resource.Dispose();
                    return read;
                }
                catch {
                    resource.Dispose();
                    throw;
                }
            }
            public override long Seek(long offset, SeekOrigin origin) {
                return resource.Seek(offset, origin);
            }
            public override void SetLength(long value) {
                throw new NotSupportedException();
            }
            public override void Write(byte[] buffer, int offset, int count) {
                throw new NotSupportedException();
            }
            public override void Flush() {
                throw new NotSupportedException();
            }
        }
        #endregion Utils
    }
}