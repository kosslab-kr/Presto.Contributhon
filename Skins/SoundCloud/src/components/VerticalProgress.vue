<template>
 <div class="verticalBox">
    <div class="progressWrapper"
      role="progressbar"  
      @mousemove="setProgressPosition"
      @click="jumpToProgress"
      @mousedown="setActive"
      @mouseup="setDeactvie"
    > 
      <div class="progressBackground"></div>
      <div class="progressPassedBackground"  :style="style.passed"></div>
    <div class="progressHandler" :style="style.handler"></div>
    </div>
  </div>
</template>

<script>

export default {
   data() {
    return {
      style: {
        passed:{},
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
      let nowY = e.clientY
      const volumeBar = this.$el.querySelector('.progressBackground')
      const {top, bottom } = volumeBar.getBoundingClientRect()
      if(nowY<=top) nowY= top;
      this.position = bottom - nowY > 0 ? bottom - nowY : 0;
     // clientY  738 858
     // 851 736 
     // 위로 올라갈수록 작아진다. 왜?
     this.percent = (this.position/120)*100
    },
    jumpToProgress(e){
      console.log(e)
      this.calcPosition(e)
      this.load(this.position)
      this.$emit('getVolumePercent', this.percent)
    },
    load(pos){
      this.loadProgress(pos)
      this.loadProgressHandler(pos)
    },
    loadProgress(pos){
      const style = {}
      style.height = (pos).toFixed(4)+'px';
      this.style.passed = style;
    },
     loadProgressHandler(pos){
      const style = {}
      const bottomFactor = 20
      style.bottom =(bottomFactor+(pos)).toFixed(4)+'px';
      this.style.handler = style
    }
  }
}
</script>
<style lang="scss" scoped>
.verticalBox {
  position: absolute;
  bottom: 35px;
  min-height: 160px;
  width: 35px;
  background: #f2f2f2;
  border-radius: 3px;
  z-index: 1000;
  border: 1px solid #ccc;
  right: -7px;
  bottom: 37px;
  &:hover {
    .progressHandler {
      opacity: 1;
    }
  }
}
.progressWrapper {
  cursor: pointer;
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
  padding: 10px;
  height: 160px;
}
.progressBackground {
  height: 120px;
  background: #ccc;
  width: 2px;
}
.progressPassedBackground {
  width: 1px;
  height: 0px;
  position: absolute;
  bottom: 20px;
  background-color: #f50;
  z-index: 1000;
}
.progressHandler {
  position: absolute;
  border: 2px solid #f50;
  border-radius: 100%;
  height: 10px;
  width: 10px;
  background-color: #f50;
  -webkit-box-sizing: border-box;
  box-sizing: border-box;
  opacity: 0;
  -webkit-transition: opacity 150ms;
  transition: opacity 150ms;
  left: 50%;
  transform: translateX(-50%);
  bottom: 20px;
}
</style>
