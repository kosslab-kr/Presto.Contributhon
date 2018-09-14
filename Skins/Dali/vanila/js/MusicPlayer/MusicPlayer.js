import { qs, $on } from "../helper/domHelper.js";

const playSrc = './assets/icons/play-btn.svg'
const pauseSrc = './assets/icons/pause-button.svg'
const minPerSec = 60;
const milliSecToSec = 1000;

class Timer {
  constructor({timer}){
    this.timerEl = qs(timer)
    this.spendTime =  0;
    this.timerId = null;
  }
  startTimer(){
    this.timerId = setTimeout(() => this.displayTime(this.startTimer.bind(this)), milliSecToSec);
  }
  updateTime(){
    return this.spendTime +=1
  }
  displayTime(startTimer){
    const spentTime = this.updateTime();

    const min = Math.floor(spentTime/minPerSec)
    const sec = spentTime-min*minPerSec
    const time = {
      min,
      sec,
    }
    qs('.min', this.timerEl).innerText = this.makeTimeFormat(time.min)
    qs('.sec', this.timerEl).innerText = this.makeTimeFormat(time.sec)
    startTimer();
  }
  makeTimeFormat(time){
    let timeString = time.toString();
    if(timeString.length<2) timeString = '0'+timeString;
    return timeString
  }
}

class MusicPlayer {
  constructor({audio, playBtn, timer, progressBar, endTime}){
    this.audioEl = qs(audio);
    this.playBtnEl = qs(playBtn);
    this.musicTimer = new Timer({timer})
    this.progressBarEl = qs(progressBar);
    this.endTimeEl = qs(endTime);
    this.bindEvents();
    this.playing = false;
  }
  bindEvents(){
    $on(this.playBtnEl, 'click', ()=>this.togglePlay()) 
  }
  togglePlay(){
    if(this.playing) this.pause();
    else this.play();
  }
  play(){
    this.audioEl.play();
    qs('.controller-icon', this.playBtnEl).setAttribute('src', pauseSrc)
    this.playing = true;
    this.musicTimer.startTimer();
  }
  pause(){
    this.audioEl.pause();
    qs('.controller-icon', this.playBtnEl).setAttribute('src', playSrc)
    this.musicTimer.startTimer();
  }
  updateTimer(){
    this.calcTime(this.currentTime)
  }
  calcTime(currentTime){
    const min = Math.floor(currentTime)%minPerSec
    const sec = currentTime-min*minPerSec
    return {min, sec}
  }
}

export default MusicPlayer;