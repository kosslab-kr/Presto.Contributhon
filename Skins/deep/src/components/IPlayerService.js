export default {
  currentMusic: null,
  position: 0,
  events: {},
  intervalID: null,

  play(music) {
    clearInterval(this.intervalID);
    
    this.position = 0;
    this.currentMusic = music;

    this.intervalID = setInterval(this._play.bind(this), 100);
  },

  pause() {
    clearInterval(this.intervalID);
  },

  resume() {
    this.intervalID = setInterval(this._play.bind(this), 100);
  },

  playNext() {
    const nextMusic = this.currentMusic.next;
    const event = new PlayerEvent('onStreamChanged', {music: nextMusic});

    this.dispatchEvent(event);
    this.play(nextMusic);
  },

  playPrevious() {
    const previousMusic = this.currentMusic.previous;
    const event = new PlayerEvent('onStreamChanged', {music: previousMusic});

    this.dispatchEvent(event);
    this.play(previousMusic);
  },

  setPosition(position) {
    this.position = position;
  },

  setVolume(volume) {
    console.log(volume);
  },

  addEventListener(eventName, callback) {
    const event = new PlayerEvent(eventName);
    event.registerCallback(callback);
    this.events[eventName] = event;
  },

  removeEventListener(eventName, targetCallback) {
    const targetEvent = this.events[eventName];

    if(!targetEvent) return;

    targetEvent.callbacks = targetEvent.callbacks.filter(callback => callback !== targetCallback);
  },

  // @param {PlayerEvent} event
  // @param {Object} eventArg
  dispatchEvent(event) {
    const targetEvent = this.events[event.name];

    if(!targetEvent) return;

    targetEvent.callbacks.forEach(callback => {
      callback(event.option);
    })
  },

  _play() {
    this.position += 100;

    if(this.position > this.currentMusic.length) this.position = this.currentMusic.length;

    const event = new PlayerEvent('onPositionChanged', {position: this.position});
    this.dispatchEvent(event);

    if(this.position === this.currentMusic.length) this._playNext();
  },

  _playNext() {
    const nextMusic = this.currentMusic.next;
    const event = new PlayerEvent('onStreamChanged', {music: nextMusic})
    this.dispatchEvent(event);
    this.play(nextMusic);
  },
}

class PlayerEvent {
  // @param {Object} option
  constructor(name, option) {
    this.name = name;
    this.option = option;
    this.callbacks = [];
  }

  registerCallback(callback) {
    this.callbacks.push(callback);
  }
}