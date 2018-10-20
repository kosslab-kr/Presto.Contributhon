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

    const POINTER_PREFIX = '__Ptr';

    /**
     * Wrapping Object
     * @param {Object} object
     * @returns {Object} the object was passed as parameter.
     */
    function wrappingObject(object) {
        for (let key in object) {
            if (!object.hasOwnProperty(key)) continue;

            // Case normal
            if (!key.startsWith(POINTER_PREFIX)) {
                object[key] = wrapping(object[key]);
                continue;
            }

            // Case pointer
            let name = key.substr(POINTER_PREFIX.length);
            let pointer = object[key];

            // Define get property
            Object.defineProperty(object, name, {
                get () {
                    return query.get(pointer);
                },
                enumerable: true,
                configurable: false,
            });
        }

        return object;
    }

    /**
     * Wrapping Anything
     * @param {*} value
     * @returns {*}
     */
    function wrapping (value) {
        // Wrapping Array
        if (Array.isArray(value)) {
            return value.map(v => wrapping(v));
        }

        // Wrapping Object
        if (value && typeof value === 'object') {
            return wrappingObject(value);
        }

        // Wrapping Normal (Number, String, Boolean, ...)
        return value;
    }

    /**
     * Wrapping CefSharp Function
     * @param {Function} func The function that want to wrap.
     * @returns {Function} Wrapped function.
     */
    function wrappingCefSharpFunction(func) {
        return function () {
            let result = func.apply(this, arguments);
            return wrapping(result);
        };
    }

    const GETTER_PREFIX = 'get';
    const SETTER_PREFIX = 'set';

    function wrappingCefSharpObject(object) {
        object.__proto__ = new EventEmitter();

        let properties = {};

        for (let key in object) {
            if (!object.hasOwnProperty(key)) continue;

            let func = object[key];

            if (key.startsWith(GETTER_PREFIX)) {
                let name = key.substr(GETTER_PREFIX.length);
                properties[name] = Object.assign({}, properties[name], { get: func });
            }
            else if (key.startsWith(SETTER_PREFIX)) {
                let name = key.substr(SETTER_PREFIX.length);
                properties[name] = Object.assign({}, properties[name], { set: func });
            }

            object[key] = wrappingCefSharpFunction(func);
        }

        for (let name in properties) {
            let get = wrappingCefSharpFunction(properties[name].get);
            let set = properties[name].set;

            let desc = {
                get, set,
                configurable: false,
                enumerable: true,
            };

            Object.defineProperty(object, name, desc);
        }

        return object;
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
        window[name] = wrappingCefSharpObject(window[name]);
    }

    // Call load event
    Presto.emit('load');
})().catch(e => {
    throw e;
});