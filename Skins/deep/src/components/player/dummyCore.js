export default class DummyCore{
  constructor({playQueue}) {
    this.musics = playQueue.musics;
    this.currentMusicIdx = playQueue.currentMusicIdx;
    this.currentMusic = this.musics[this.currentMusicIdx] || null;
    this.currentTime = 0;
    this.intervalID = null;
    this.onReturnCurrentTime = null;
    this.onSetCurrentMusic = null;
  }

  play(time) {
    if(!this.currentMusic) return;

    if(time !== undefined) this._setCurrentTime(time);

    this.intervalID = setInterval(() => {
      if(this.currentTime >= this.currentMusic.runningTime) {
        this.currentTime = 0;
        this.pause();
        this.next();
        this.play();
      }
      else {
        this.currentTime += 100;
        this.onReturnCurrentTime(this.currentTime);
      }
    }, 100);
  }

  pause() {
    clearInterval(this.intervalID);
  }

  pauseToReturnCurrentTime() {
    clearInterval(this.intervalID);
  }

  previous() {
    if(this.currentTime > 1000) {
      this.currentTime = 0;
      return;
    }

    this.currentMusicIdx = ((this.currentMusicIdx - 1) + this.musics.length) % this.musics.length;
    this._setCurrentMusic();
  }

  next() {
    this.currentMusicIdx = (this.currentMusicIdx + 1) % this.musics.length;
    this._setCurrentMusic();
  }

  shuffle() {}

  repeat() {}

  setVolume() {}

  setPlayQueue({currentMusicIdx, musics}) {
    this.musics = musics;
    this.currentMusicIdx = currentMusicIdx;
    this._setCurrentMusic();
  }

  _setCurrentTime(time) {
    this.currentTime = time;
  }

  _setCurrentMusic() {
    this.currentMusic = this.musics[this.currentMusicIdx];
    this.onSetCurrentMusic(this.currentMusic);
  }
}