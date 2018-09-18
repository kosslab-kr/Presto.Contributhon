export default class DummyCore{
  constructor({musicList}) {
    this.musicList = musicList;
    this.currentMusic = this.musicList[0];
    this.currentTime = 0;
    this.intervalID = null;
    this.onReturnCurrentTime = null;
  }

  play(time) {
    time && this._setCurrentTime(time);

    this.intervalID = setInterval(() => {
      if(this.currentTime >= this.runningTime) {
        this.currentTime = this.runningTime;
        this.pause(); // later next()
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

  _setCurrentTime(time) {
    this.currentTime = time;
  }
}