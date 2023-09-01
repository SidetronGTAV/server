import * as alt from 'alt-client'

export class Webview {
    public static Webview: alt.WebView;
    private static _isUiOpen: boolean;

    constructor() {
        Webview.Webview = new alt.WebView('http://resource/client/html/index.html')
    }

    public static OpenUi(emit: string, ...args: any[]): boolean {
        if (Webview._isUiOpen) return false;
        Webview.Webview.emit(emit, true, ...args);
        Webview._isUiOpen = true;
        Webview.HandleControls(true);
        Webview.Webview.focus();
        return true;
    }

    public static CloseUi(emit: string): boolean {
        if (!Webview._isUiOpen) return false;
        Webview.Webview.emit(emit, false);
        Webview._isUiOpen = false;
        Webview.HandleControls(false);
        Webview.Webview.focus();
        return true;
    }

    private static HandleControls(state: boolean): void {
        alt.showCursor(state);
        alt.toggleGameControls(!state);
    }
}