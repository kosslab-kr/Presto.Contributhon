(() => {
    let global = window;
    if (global.CefSharp) return;

    const CefSharp = {};
    global.CefSharp = CefSharp;

    let objects = {};

    CefSharp.BindObjectAsync = async function (name) {
        global[name] = objects[name];
    };
})();