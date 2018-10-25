<template>
  <section id="musicplayer">
    <ul class="player__controlbtn-list">
      <li>
        <button  @click="handlePrevButtonClick">
          <img src="../assets/icons/sc_prev.svg" alt="">
        </button>
      </li>
      <li>
        <button @click="handlePlayPauseButtonClick">
          <img v-if="isPlayButton" src="../assets/icons/sc_play.svg" alt="">
          <img v-else src="../assets/icons/sc_pause.svg" alt="">
        </button>
      </li>
      <li>
        <button  @click="handleNextButtonClick">
          <img src="../assets/icons/sc_next.svg" alt="">
        </button>
      </li>
       <li v-if="hasStop">
        <button  @click="handleStop">
          <img src="../assets/icons/sc_next.svg" alt="">
        </button>
      </li>
      <li>
        <button @click="shuffleList">
          <img src="../assets/icons/sc_shuffle.svg" alt="">
        </button>
      </li>
      <li>
        <button>
          <img src="../assets/icons/sc_continue.svg" alt="">
        </button>
      </li>
    </ul>
    <div class="progress__section">
     <div class="timer">{{currentTimeText}}</div>
     <MusicPlayerProgress
      ref="progressBar"
      @changePercent="updateTime"
    /> 
    <div class="runningTime">
      {{runningTimeText}}
    </div>
    </div>
    <div class="volumeSection">
      <VerticalProgress
        @getVolumePercent="upateVolume"
      />
      <button 
        @click="activeVolumBar"
        @mousemove="activeVolumBar"
        >
        <img src="../assets/icons/volumeBtn.svg" alt="">
      </button>
    </div>
    <div class="playingOne__box">
      <img class="cover__thumbnail" :src="cover" alt="">
      <span v-if="hasSoundBadGe">badge</span>
      <span >{{title}}</span>
    </div>
  </section>
</template>

<script>
import animation from '../animation.js';
import {musicTimeFormat, shuffle} from '../helper.js';
import MusicPlayerProgress from './MusicPlayerProgress.vue';
import VerticalProgress from './VerticalProgress';
//assets


export default {
 props: {
   title: {
     type: String,
     default: '',
   },
   cover: {
     type: String,
     default: '',
   },
 },
 components: {
  MusicPlayerProgress,
  VerticalProgress,
 },
 data(){
    return {
      isPlayButton: true,
      hasStop: false,
      hasSoundBadGe: false,
      currentTime: 0,
      timerId: null,
      volumBarActive: false,
      currentTimeText: '',
      runningTimeText: '',
    }
  },
  created() { 
     this.$EventBus.$on('startTimer', () => { 
        this.setPlayingView()
      }); 
    this.$EventBus.$on('pauseTimer', () => { 
        this.setPauseView()
    }); 
     this.startTimer();
  },
  destroyed () {
     this.stopTimer();
  },
  methods : {
    shuffleList(){
        shuffle(window.musicLibrary.library)
    },
    deactiveVolumBar(){
      this.volumBarActive = false;
    },
    activeVolumBar(){
      this.volumBarActive = true;
    },
    upateVolume(volumePercent){
       this.$store.commit('ChangeVolume',volumePercent)
    },
    updateTime(percent){
      const runningTime =this.$store.state.musicLibrary.mockGetRunningTime()
      this.currentTime = runningTime*percent/100
      console.log(this.currentTime)
      this.$store.commit('UpdateTime', this.currentTime)
    },
    handlePlayPauseButtonClick(){
      if(this.isPlayButton) return this.handlePlayBtnClick()
      else return this.handlePauseBtnClick()
    },
    setPlayingView(){
     this.setPlayState()
    },
    setPauseView(){
     this.setPauseState()
    },
    handlePlayBtnClick(){
     this.$store.commit('Play')
     this.setPlayingView();
    },
    handlePauseBtnClick(){
     this.$store.commit('PauseMusic')
     this.setPauseState()
    },
    startTimer() {
      let recursive = (params) => {
        let percent = this.$store.state.musicLibrary.getProgressPercent();
        
        let currentTime = this.$store.state.musicLibrary.getCurrentTime();

        let runningTime = this.$store.state.musicLibrary.getRunningTime();
        
        this.$refs.progressBar.load(percent);
        this.currentTimeText = musicTimeFormat(currentTime);
        this.runningTimeText = musicTimeFormat(runningTime);

        setTimeout(recursive, 100);
      };

      this.timerId = setTimeout(recursive, 100);
    },
    stopTimer(){
      clearTimeout(this.timerId)
    },
    togglePlayPauseState(){
      this.isPlayButton = !this.isPlayButton;
    },
    setPlayState(){
      this.isPlayButton = false;
    },
    setPauseState(){
      this.isPlayButton = true;
    },
    handleNextButtonClick(){
      this.stopTimer();
      this.setPlayState();
      this.$store.commit('PlayNextMusic')
      this.startTimer();
    },
    handlePrevButtonClick(){
      this.stopTimer();
      this.setPlayState();
      this.$store.commit('PlayPrevMusic')
      this.startTimer();
    },
  }
}
</script>

<style lang="scss">
#musicplayer {
  width: 100%;
  position: fixed;
  display: flex;
  bottom: 0;
  height: 50px;
  background: #f2f2f2;
  border-top: 1px solid #cecece;
  padding-left: 20px;
  padding-right: 20px;
}

.player__controlbtn-list,
.playingOne__box {
  display: flex;
  align-items: center;
  margin-right: 12px;
  margin-left: 12px;
}

.cover__thumbnail {
  max-width: 100%;
  width: 40px;
  height: 40px;
  margin-right: 18px;
  margin-left: 10px;
}

.progress__section {
  display: flex;
  justify-content: center;
  align-items: center;
  flex: 1;
  min-width: 300px;
  .timer {
    color: #f50;
    font-size: 12px;
    margin-right: 10px;
  }
  .runningTime {
    font-size: 12px;
    margin-right: 10px;
    margin-left: 10px;
  }
}

.volumeSection {
  display: flex;
  position: relative;
  .verticalBox {
    display: none;
  }
  &:hover {
    .verticalBox {
      display: block;
    }
  }
}
</style>
