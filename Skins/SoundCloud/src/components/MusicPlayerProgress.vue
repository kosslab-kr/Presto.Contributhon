<template>
 <div class="progress__box">
    <div class="progressWrapper" 
      role="progressbar" 
      @mousemove="setProgressPosition"
      @mousedown="setActive"
      @mouseup="setDeactvie"
      @click="jumpToProgress"
    >
    <div class="progressBackground"></div>
    <div class="progressPassedBackground"  :style="style.bg"></div>
    <div class="progressHandler" :style="style.handler"></div>
    </div>
  </div>
</template>

<script>
import { musicTimeFormat } from '../helper.js';


export default {
  data() {
    return {
      style: {
        bg:{},
        handler: {}
      },
      length: 0,
      position: 0,
      percent: 0,
      active: false
    }
  },
  methods: {
    setActive(){
      this.active = true;
    },
    setDeactvie(){
      this.active = false;
    },
    setProgressPosition(e){
     if(this.active===false) return ;
     this.jumpToProgress(e);
    },
    calcPosition(e){
      const nowX = e.clientX
      const progressBar = this.$el
      const {left, right } = progressBar.getBoundingClientRect()
      this.length = right-left
      this.position = nowX-left > 0 ? nowX-left : 0;
     this.percent = ((this.position/this.length )*100).toFixed(4)
    },
    jumpToProgress(e){
      this.calcPosition(e)
      this.load(this.percent)
      this.$emit('changePercent', this.percent)
    },
    load(percent){
      this.loadProgress(percent)
      this.loadProgressHandler(percent)
    },
    loadProgress(percent){
      const style = {}
      style.width = percent+'%';
      this.style.bg = style;
    },
     loadProgressHandler(percent){
      const style = {}
      style.left = percent+'%';
      this.style.handler = style
    }
  },   
}
</script>
<style lang="scss" scoped>
.progress__box {
  min-width: 40%;
  display: flex;
  flex: 1;
  &:hover {
    .progressHandler {
      opacity: 1;
    }
  }
  align-items: center;
  .progressWrapper {
    position: relative;
    -ms-flex-positive: 1;
    -webkit-box-flex: 1;
    flex-grow: 1;
    padding: 10px 0;
    margin: 13px 10px 0 10p;
    cursor: pointer;
  }
  .timer,
  .runningTime {
    text-align: right;
    font-size: 11px;
    margin-right: 10px;
    margin-left: 10px;
  }
  .timer {
    color: #f50;
  }
  .progressBackground,
  .progressPassedBackground {
    height: 1px;
  }
  .progressBackground {
    background-color: #ccc;
  }
  .progressPassedBackground {
    width: 0%;
    background-color: #f50;
    z-index: 1000;
  }
  .progressHandler {
    position: absolute;
    border: 1px solid #f50;
    border-radius: 100%;
    height: 8px;
    width: 8px;
    background-color: #f50;
    -webkit-box-sizing: border-box;
    box-sizing: border-box;
    margin-top: -4px;
    margin-left: -4px;
    opacity: 0;
    -webkit-transition: opacity 150ms;
    transition: opacity 150ms;
  }
}
</style>
