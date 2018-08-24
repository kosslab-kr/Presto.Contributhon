if (!CefSharp) {
    throw 'CefSharp is required';
}

(async function () {
    /**
     * Create BindObjectAsync Promise
     * @param {string} name
     * @returns {Promise}
     */
    function bindObject(name) {
        return CefSharp.BindObjectAsync(name);
    }

    /**
     * boundObjects
     * @type {Array.<Promise>}
     */
    let boundObjects = [ 'player', 'library' ].map(bindObject);
    await Promise.all(boundObjects);

    if (typeof window.onplayerload === 'function') {
        window.onplayerload.call();
    }


})().catch(e => {
    throw e;
});