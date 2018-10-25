(() => {
    let global = window;
    if (global.CefSharp) return;

    const CefSharp = {};
    global.CefSharp = CefSharp;

    function wait() {
        return new Promise((resolve, reject) => {
            setTimeout(() => resolve, 1);
        });
    }

    class Player extends EventEmitter {
        constructor () {
            super();

            this._bitrate = 0;
            this._currentMusic = null;
            this._currentPlaylist = [];
            this._frequency = 0;
            this._pan = 0;
            this._playbackState = 0;
            this._repeatMode = 0;
            this._shuffleMode = 0;
            this._spectrum = 0;
            this._supportExtensions = 0;

            this._audio = document.createElement('audio');
            document.body.appendChild(this._audio);
        }

        /**
         * Get Bitrate
         * @returns {Promise.<number>}
         */
        async getBitrate () {
            return this._bitrate;
        }

        /**
         * Get CurrentMusic
         * @returns {Promise.<object>}
         */
        async getCurrentMusic () {
            return this._currentMusic;
        }

        /**
         * Get CurrentPlaylist
         * @returns {Promise.<Array>}
         */
        async getCurrentPlaylist () {
            return this._currentPlaylist;
        }

        /**
         * Get Frequency
         * @returns {Promise.<number>}
         */
        async getFrequency () {
            return this._frequency;
        }

        /**
         * Get Length
         * @returns {Promise.<number>}
         */
        async getLength () {
            return this._audio.duration;
        }

        /**
         * Get Pan
         * @returns {Promise.<number>}
         */
        async getPan () {
            await wait();

            return this._pan;
        }

        /**
         * Get PlaybackState
         * @returns {Promise.<number>}
         */
        async getPlaybackState () {
            await wait();

            return this._playbackState;
        }

        /**
         * Get Position
         * @returns {Promise.<number>}
         */
        async getPosition () {
            await wait();

            return this._audio.currentTime * 1000;
        }

        /**
         * Set Position
         * @param {number} value
         * @returns {Promise.<void>}
         */
        async setPosition (value) {
            await wait();

            this._audio.currentTime = value / 1000;
        }

        /**
         * Get RepeatMode
         * @returns {Promise.<number>}
         */
        async getRepeatMode () {
            await wait();

            return this._repeatMode;
        }

        /**
         * Set RepeatMode
         * @param {number} value
         * @returns {Promise.<void>}
         */
        async setRepeatMode (value) {
            await wait();

            this._repeatMode = value;
        }

        /**
         * Get ShuffleMode
         * @returns {Promise.<number>}
         */
        async getShuffleMode () {
            await wait();

            return this._shuffleMode;
        }

        /**
         * Set ShuffleMode
         * @param {number} value
         * @returns {Promise.<void>}
         */
        async setShuffleMode (value) {
            await wait();

            this._shuffleMode = value;
        }

        /**
         * Get Spectrum
         * @returns {Promise.<number>}
         */
        async getSpectrum () {
            await wait();

            return this._spectrum;
        }

        /**
         * Get SupportExtensions
         * @returns {Promise.<number>}
         */
        async getSupportExtensions () {
            await wait();

            return this._supportExtensions;
        }

        /**
         * Get Volume
         * @returns {Promise.<number>}
         */
        async getVolume () {
            await wait();

            return this._audio.volume;
        }

        /**
         * Set Volume
         * @param {number} value
         * @returns {Promise.<void>}
         */
        async setVolume (value) {
            await wait();

            this._audio.volume = value;
        }

        /**
         * Play music
         * @returns {Promise.<void>}
         */
        async play () {
            await wait();

            this._audio.play();
        }

        /**
         * Play next music
         * @returns {Promise.<void>}
         */
        async playNext () {
            await wait();

            this._audio.play();
        }

        /**
         * Play previous music
         * @returns {Promise.<void>}
         */
        async playPrevious () {
            await wait();

        }

        /**
         * Pause current playing music
         * @returns {Promise.<void>}
         */
        async pause () {
            await wait();

            this._audio.pause();
        }

        /**
         * Get Musics
         * @returns {Promise.<Array>}
         */
        async getMusics () {
            await wait();

            return [];
        }
    }

    let objects = {
        player: new Player(),
    };

    CefSharp.BindObjectAsync = async function (name) {
        global[name] = objects[name];
    };
})();