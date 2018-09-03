{
    let global = window;

    if (!global.CefSharp) {
        const CefSharp = {};
        global.CefSharp = CefSharp;
    }
}