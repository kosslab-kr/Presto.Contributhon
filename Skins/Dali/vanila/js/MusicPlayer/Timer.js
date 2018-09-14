import { qs, $on } from "../helper/domHelper.js";

const minPerSec = 60;
const milliSecToSec = 1000;

class Timer {
  constructor({timer}){
    this.timerEl = qs(timer)
    this.spendTime =  0;
    this.timerId = null;
  }
  startTimer(){
    this.spendTime+=1
    this.timerId = setTimeout(()=>{
      this.startTimer()
    },milliSecToSec )
    this.displayTimer(this.spendTime)
  }
  displayTimer(spentTime, el=this.timerEl){
    const min = Math.floor(spentTime/minPerSec)
    const sec = spentTime-min*minPerSec
    const time = {
      min,
      sec,
    }
    qs('.min', el).innerText = this.makeTimeFormat(time.min)
    qs('.sec', el).innerText = this.makeTimeFormat(time.sec)
  }
  stopTimer(){
    clearTimeout(this.timerId);
  }

  makeTimeFormat(time){
    let timeString = time.toString();
    if(timeString.length<2) timeString = '0'+timeString;
    return timeString
  }
}

export default Timer 