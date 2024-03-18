$(() => {
    htmlEditor = $('.html-editor').dxHtmlEditor({
        height: '100vh',
        //value: markup,
        imageUpload: {
            tabs: ['file', 'url'],
            fileUploadMode: 'base64',
        },
        toolbar: {
            items: [
                'undo', 'redo', 'separator',
                {
                    name: 'size',
                    acceptedValues: ['8pt', '10pt', '12pt', '14pt', '18pt', '24pt', '36pt'],
                    options: { inputAttr: { 'aria-label': 'Font size' } },
                },
                {
                    name: 'font',
                    acceptedValues: ['Arial', 'Courier New', 'Georgia', 'Impact', 'Lucida Console', 'Tahoma', 'Times New Roman', 'Verdana'],
                    options: { inputAttr: { 'aria-label': 'Font family' } },
                },
                'separator', 'bold', 'italic', 'strike', 'underline', 'separator',
                'alignLeft', 'alignCenter', 'alignRight', 'alignJustify', 'separator',
                'orderedList', 'bulletList', 'separator',
                {
                    name: 'header',
                    acceptedValues: [false, 1, 2, 3, 4, 5],
                    options: { inputAttr: { 'aria-label': 'Header' } },
                }, 'separator',
                'color', 'background', 'separator',
                'link', 'image', 'separator',
                'clear', 'codeBlock', 'blockquote', 'separator',
                'insertTable', 'deleteTable',
                'insertRowAbove', 'insertRowBelow', 'deleteRow',
                'insertColumnLeft', 'insertColumnRight', 'deleteColumn',
            ],
        },
        mediaResizing: {
            enabled: true,
        },
        onInitialized: function (e) {
            window.chrome.webview.postMessage("Loaded");
        },
        onValueChanged: function (e) {
            window.chrome.webview.postMessage("HtmlChanged");
        }
    }).dxHtmlEditor('instance');
});
function getHtmlBase64() {
    const base64 = htmlEditor.getQuillInstance().getSemanticHTML();
    return utf8_to_b64(base64);
}
function setHtmlFromBase64(htmlBase64) {
    const html = b64_to_utf8(htmlBase64);
    htmlEditor.getQuillInstance().clipboard.dangerouslyPasteHTML(html);
}
function undo() {
    // https://js.devexpress.com/jQuery/Documentation/ApiReference/UI_Components/dxHtmlEditor/Methods/#undo
    htmlEditor.undo();
}
function redo() {
    // https://js.devexpress.com/jQuery/Documentation/ApiReference/UI_Components/dxHtmlEditor/Methods/#redo
    htmlEditor.redo();
}
//https://developer.mozilla.org/en-US/docs/Web/API/btoa#unicode_strings
function utf8_to_b64(str) {
    return window.btoa(unescape(encodeURIComponent(str)));
}
function b64_to_utf8(str) {
    return decodeURIComponent(escape(window.atob(str)));
}
