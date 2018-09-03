if (!CefSharp) {
    throw 'CefSharp is required';
}

/**
 * EventEmitter Class
 */
class EventEmitter {
    /**
     * Constructor of EventEmitter
     * @constructor
     * @public
     */
    constructor() {
        this.events = {};
    }

    /**
     * Add a listener for a given event.
     *
     * @param {(String)} event The event name.
     * @param {Function} listener The listener function.
     * @returns {EventEmitter} `this`.
     * @public
     */
    on(event, listener) {
        if (!this.events[event]) this.events[event] = [];

        this.events[event].push(listener);
        return this;
    }

    /**
     * Remove the listeners of a given event.
     *
     * @param {String} event The event name.
     * @param {Function} listener Only remove the listeners that match this function.
     * @returns {EventEmitter} `this`.
     * @public
     */
    off(event, listener) {
        if (!this.events[event]) return this;

        const idx = this.events[event].indexOf(listener);
        if (idx < 0) return this;

        this.events[event].splice(idx, 1);
        return this;
    }

    /**
     * Calls each of the listeners registered for a given event.
     *
     * @param {String} event The event name.
     * @param {...*} args The event arguments.
     * @public
     */
    emit(event, ...args) {
        if (!this.events[event]) return;

        this.events[event].forEach(listener => listener.apply(this, args));
    }

    /**
     * Add a one-time listener for a given event.
     *
     * @param {String} event The event name.
     * @param {Function} listener The listener function.
     * @returns {EventEmitter} `this`.
     * @public
     */
    once(event, listener) {
        let wrapper = (...args) => {
            listener.apply(this, args);
            this.off(event, wrapper);
        };

        this.on(event, wrapper);
        return this;
    }
}

const Presto = new EventEmitter();

(async function () {
    /**
     * Create BindObjectAsync Promise
     * @param {string} name
     * @returns {Promise}
     */
    function bind(name) {
        return CefSharp.BindObjectAsync(name);
    }

    /**
     * Binding object names
     * @type {[string]}
     */
    let objectNames = [ 'album', 'artist', 'genre', 'player', 'playlist' ];

    // Wait for binding
    await Promise.all(objectNames.map(bind));

    // Make bound objects to EventEmitter
    for (let name of objectNames) {
        window[name].__proto__ = new EventEmitter();
    }

    // Call load event
    Presto.emit('load');
})().catch(e => {
    throw e;
});