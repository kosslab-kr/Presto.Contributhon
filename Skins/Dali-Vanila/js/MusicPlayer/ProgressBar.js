import { qs, $on } from "../helper/domHelper.js";

const milliSecToSec = 1000;

class ProgressBar {
  constructor({progressBar, rangeBar}){
   this.progressBarEl = qs(progressBar)
   this.endTime = null;
   this.spendTime = 0;
   this.rangeBarEl = qs(rangeBar)
   this.notify = null;
   this.bindEvents();
  }
  bindEvents(){
    $on(this.rangeBarEl, 'click', (e)=>this.moveToClicked(e))
  }
  moveToClicked({target}){
    const value = target.value
    this.updateSeekBarbyValue(value)
  }
  getEndTime(endTime){
    this.endTime = endTime;
  }
  start(){
    this.spendTime +=1;
    this.timerId = setTimeout(()=>{
      this.start()
    }, milliSecToSec)
    this.updateSeekBar(this.spendTime)
  }
  updateSeekBar(time){  
    const seekbarPlace = (time/this.endTime)*100+'%'
    const seekBarEl = qs('.seek-bar', this.progressBarEl)
    seekBarEl.style.width = seekbarPlace;
  }
  updateSeekBarbyValue(value){
    this.spendTime = this.endTime*Number(value)/100;
    const seekBarEl = qs('.seek-bar', this.progressBarEl)
    seekBarEl.style.width = value+'%';
  }
  stop(){
    clearTimeout(this.timerId);
  }
}

export default ProgressBar;