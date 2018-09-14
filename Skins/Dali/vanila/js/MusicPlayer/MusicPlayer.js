import { qs, $on } from "../helper/domHelper.js";
import Timer from './Timer.js';

const playSrc = './assets/icons/play-btn.svg'
const pauseSrc = './assets/icons/pause-button.svg'





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
    this.audioEl.currentTime
    
    this.musicTimer.startTimer();
  }
  pause(){
    this.audioEl.pause();
    qs('.controller-icon', this.playBtnEl).setAttribute('src', playSrc)
    this.playing = false;
    this.musicTimer.stopTimer();
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