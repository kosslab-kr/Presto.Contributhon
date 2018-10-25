<template>
  <li class="soundList__item">
    <div class="soundContext">
      <div class="genre-avatar textHide">Genre avatar</div>
            <div class="soundContext__box">
              <span class="genre-text">
                <a href="">EDMLead Selection</a>
              </span>
              <span class="timeline">reposted a trrack </span>
              <span class="lasttime">5</span>
              <span class="lasttime-prefix">hours a go</span>
            </div>
          </div>
          <div class="music__item">
            <div class="album__thumnail">
              <a href="">
                <img :src="cover" :alt="title">
              </a>
            </div>
            <div class="music__content">
              <div class="music__content-header">
                <button class="playPauseButton"      @click="handlePlayPauseButtonClicked"
                >
                  <img v-if="isPlayButton"
                src="../assets/icons/sc_play_button.svg" alt="play pause 표시 버튼" /> 
                 <img  v-else src="../assets/icons/sc_pasue._button.svg" alt="play 재생 표시 버튼" />
                </button>
                <div class="music__content-titleBox">
                  <span>R-wan</span>
                  <h2 class="musicTitle">{{ title }}</h2>
                </div>
                </div>
                  <div class="wave__progress">
                    <div class="timer">
                      
                    </div>
                </div>
                <div class="music__toolbar">
                  <div class="special__section">
                    <ul class="special__buttonlist">
                      <li>
                        <button>Love</button>
                      </li>
                      <li>
                        <button>Repost</button>
                      </li>
                      <li>
                        <button>Share</button>
                      </li>
                      <li>
                        <button>More</button>
                      </li>
                    </ul>
                    <div class="downLoadLink">
                      <a href="">Free DownLoad</a>
                    </div>
                    <div class="likeAndComment">
                      <a href="">Like</a>
                      <a href="">Comment</a>
                    </div>
                  </div>
                </div>   
            </div>
          </div>
        </li>
</template>

<script>
import MusicPlayerProgress from './MusicPlayerProgress.vue';
import {musicTimeFormat} from '../helper.js'

export default {
  data(){
    return{
      isPlayButton: true,
    }
 },
 props: ['title','cover', 'isPlaying'],
 watch: { 
      	isPlaying(newV,oldV){
          this.isPlayButton = !newV;
        }
  },
 methods: {
    loadRunningTime(time){
      return musicTimeFormat(time)
    },
    handlePlayPauseButtonClicked(e){
      if(this.isPlayButton) return this.handlePlayBtnClick(e)
      else return this.handlePauseBtnClick(e)
    },
    toggleButtonState(){
      return this.isPlayButton =!this.isPlayButton
    },
    handlePlayBtnClick(){
      this.$emit('play');
      this.toggleButtonState();
    },
    handlePauseBtnClick(){
      this.$emit('pause');  
      this.toggleButtonState();
    }
  }
}
</script>
<style lang="scss" scoped>
.music__content {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  padding-bottom: 20px;
}
.playPauseButton {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 36px;
  height: 36px;
  background: #f50 linear-gradient(#f70, #f30);
  border-radius: 50%;
  margin-right: 8px;
  img: {
    width: 18px;
    height: 24px;
  }
}

.musicTitle {
  max-width: 200px;
}

.wave__progress {
  .timer {
    width: 100%;
    display: flex;
    justify-content: space-between;
  }
}

.list__description {
  margin-top: 30px;
  margin-bottom: 40px;
  font-size: 18px;
  font-weight: 100;
  color: #999;
}

.wrapper__main {
  padding-left: 20px;
  padding-right: 20px;
}
</style>
