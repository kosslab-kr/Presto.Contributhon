import { qs, $on } from "../helper/domHelper.js";

const playSrc = './assets/icons/play-btn.svg'
const pauseSrc = './assets/icons/pause-button.svg'


class MusicPlayer {
  constructor({audio, playBtn}){
    this.audioEl = qs(audio);
    this.playBtnEl = qs(playBtn);
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
  }
  pause(){
    this.audioEl.pause();
    qs('.controller-icon', this.playBtnEl).setAttribute('src', playSrc)
    this.playing = false;
  }
}

export default MusicPlayer;