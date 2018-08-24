if (!CefSharp) {
    throw 'CefSharp is required';
}

(async function () {
    await CefSharp.BindObjectAsync('player');
    await CefSharp.BindObjectAsync('library');

    if (typeof window.onplayerload === 'function') {
        window.onplayerload.call();
    }
})().catch(e => {
    throw e;
});