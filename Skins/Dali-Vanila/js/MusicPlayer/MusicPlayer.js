import { qs, $on } from "../helper/domHelper.js";
import Timer from './Timer.js';
import ProgressBar from "./ProgressBar.js";

const playSrc = './assets/icons/play-btn.svg'
const pauseSrc = './assets/icons/pause-button.svg'


const minPerSec = 60;
const milliSecToSec = 1000;


class MusicPlayer {
  constructor({audio, playBtn, timer, progressBar, endTime, rangeBar}){
  
    this.audioEl = qs(audio);
    this.playBtnEl = qs(playBtn);
    this.musicTimer = new Timer({timer})
    this.progressBarEl = new ProgressBar({progressBar, rangeBar});
    this.endTimeEl = qs(endTime);
    this.playing = false;
    this.bindEvents();
  }
  bindEvents(){
    $on(this.playBtnEl, 'click', ()=>this.togglePlay())
    $on(this.audioEl, 'loadeddata', ()=>this.displayEndTime())  
  }
  syncToTime(){

  }
  displayEndTime(){
    const endTime = Math.floor(this.audioEl.duration)
    this.musicTimer.displayTimer(Math.floor(this.audioEl.duration), this.endTimeEl)
    this.progressBarEl.getEndTime(endTime);
  }
  togglePlay(){
    if(this.playing) this.pause();
    else this.play();
  }
  play(){
    this.audioEl.play();
    qs('.controller-icon', this.playBtnEl).setAttribute('src', pauseSrc)
    this.playing = true;
    this.audioEl.currentTime
    this.musicTimer.startTimer();
    this.progressBarEl.start();
  }
  pause(){
    this.audioEl.pause();
    qs('.controller-icon', this.playBtnEl).setAttribute('src', playSrc)
    this.playing = false;
    this.musicTimer.stopTimer();
    this.progressBarEl.stop();
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