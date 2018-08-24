if (!CefSharp) {
    throw 'CefSharp is required';
}

(async function () {
    await CefSharp.BindObjectAsync('player');
    await CefSharp.BindObjectAsync('library');
})().catch(e => {
    throw e;
});