if (!CefSharp) {
    throw 'CefSharp is required';
}


class EventEmitter {
    constructor() {
        this.events = {};
    }

    on(event, listener) {
        if (typeof this.events[event] !== 'object') {
            this.events[event] = [];
        }
        this.events[event].push(listener);
        return () => this.removeListener(event, listener);
    }

    removeListener(event, listener) {
        if (typeof this.events[event] === 'object') {
            const idx = this.events[event].indexOf(listener);
            if (idx > -1) {
                this.events[event].splice(idx, 1);
            }
        }
    }

    emit(event, ...args) {
        if (typeof this.events[event] === 'object') {
            this.events[event].forEach(listener => listener.apply(this, args));
        }
    }

    once(event, listener) {
        const remove = this.on(event, (...args) => {
            remove();
            listener.apply(this, args);
        });
    }
}

const Presto = new EventEmitter();

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