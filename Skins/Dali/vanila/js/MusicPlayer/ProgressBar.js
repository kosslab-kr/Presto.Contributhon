import { qs, $on } from "../helper/domHelper.js";

const milliSecToSec = 1000;

class ProgressBar {
  constructor({progressBar}){
   this.progressBarEl = qs(progressBar)
   this.endTime = null;
   this.spendTime = 0;
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
}

export default ProgressBar;