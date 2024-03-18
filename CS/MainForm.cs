using System;
using DevExpress.XtraSplashScreen;

namespace DXHtmlEditor {
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm {
        public MainForm() {
            InitializeComponent();
            dxHtmlEditorWebView.HtmlChanged += OnDxHtmlEditorWebViewHtmlChanged;
            dxHtmlEditorWebView.HtmlLoaded += OnDxHtmlEditorWebViewHtmlLoaded;
        }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            ShowOverlay();
            sourceMemoEdit.Text = "Hello DevExpress Html Editor!";
        }
        async void OnSourceMemoEditTextChanged(object sender, EventArgs e) {
            await dxHtmlEditorWebView.SetHtmlText(sourceMemoEdit.Text);
        }
        async void OnUndo(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            await dxHtmlEditorWebView.Undo();
        }
        async void OnRedo(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            await dxHtmlEditorWebView.Redo();
        }
        protected override void OnLookAndFeelChangedCore() {
            UpdateDxHtmlEditorWebViewTheme();
            base.OnLookAndFeelChangedCore();
        }
        bool UseDarkTheme {
            get { return DevExpress.Utils.Frames.FrameHelper.IsDarkSkin(GetActiveLookAndFeel()); }
        }
        async void UpdateDxHtmlEditorWebViewTheme() {
            string themeName = UseDarkTheme ? "dx.dark.css" : "dx.light.css";
            if(dxHtmlEditorWebView.CanSetTheme(themeName)) {
                ShowOverlay();
                await dxHtmlEditorWebView.SetTheme(themeName);
            }
        }
        async void OnDxHtmlEditorWebViewHtmlChanged(object sender, EventArgs e) {
            targetMemoEdit.Text = await dxHtmlEditorWebView.GetHtmlText();
        }
        void OnDxHtmlEditorWebViewHtmlLoaded(object sender, EventArgs e) {
            CloseOverlay();
        }
        IOverlaySplashScreenHandle overlayHandle;
        readonly OverlayWindowOptions options = new OverlayWindowOptions(opacity: 100d / 255);
        void ShowOverlay() {
            overlayHandle = overlayHandle ?? SplashScreenManager.ShowOverlayForm(layoutControl, options);
        }
        void CloseOverlay() {
            overlayHandle?.Close();
            overlayHandle = null;
        }
    }
}