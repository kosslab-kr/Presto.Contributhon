<template>
  <section id='player' class="player">
    <div class="player__wrap">
      <PlayerCurrentMusic
        :music="currentMusic"
      />
      <div class="player__controls">
        <div class="player__controls-wrap">
          <div class="player__button-wrap">
            <PlayerButtonShuffle/>
            <PlayerButtonBack/>
            <div class="player__play-button">
              <BaseButtonPlayPause
                :onPlay="onPlay"
                @button-clicked="togglePlayPauseButton"
              />
            </div>
            <PlayerButtonNext/>
            <PlayerButtonRepeat/>
          </div>
          <div class="player__controls-core">
            <div class="player__current-time">{{formatTime(this.currentTime)}}</div>
            <div class='player__controls-slider'>
              <PlayerSliderHorizon
                ref="playerControlsSlider"
                v-on:slider-thumb-down='manipulateStart'
                v-on:slider-thumb-move='calcCurrentTime'
                v-on:slider-thumb-up='manipulateEnd'>
              </PlayerSliderHorizon>
            </div>
            <div class="player__total-time">{{formatTime(this.currentMusic.runningTime)}}</div>
          </div>
        </div>
      </div>
      <div class="player__option-wrap">
        <div class="player__option player__option--mute"></div>
        <div class="player__volume-slider">
          <PlayerSliderHorizon
            ref="playerVolumeSlider">
          </PlayerSliderHorizon>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import PlayerSliderHorizon from './PlayerSliderHorizon.vue';
import DummyCore from './dummyCore.js';
import PlayerCurrentMusic from './PlayerCurrentMusic.vue';
import PlayerButtonBack from './PlayerButtonBack.vue';
import PlayerButtonNext from './PlayerButtonNext.vue';
import PlayerButtonShuffle from './PlayerButtonShuffle.vue';
import PlayerButtonRepeat from './PlayerButtonRepeat.vue';

const core = new DummyCore({
  playQueue: {currentMusicIdx: 0, musics: []}
})

export default {
  name: 'Player',

  components: {
    PlayerSliderHorizon,
    PlayerCurrentMusic,
    PlayerButtonBack,
    PlayerButtonNext,
    PlayerButtonShuffle,
    PlayerButtonRepeat
  },

  data() {
    return {
      core: core,
      currentMusic: {
        title: 'untitled',
        artist: 'anonymous',
        runningTime: 0,
        album: {picture: require('../assets/album_picture/untitled.png')},
        genre: 'notFound'
      },
      currentTime: 0,
      onPlay: false,
    }
  },

  created() {
    this.core.onReturnCurrentTime = this.setCurrentTime.bind(this);
    this.core.onSetCurrentMusic = this.setCurrentMusic.bind(this);
    // this.$el.addEventListener('timeChanged', this.setCurrentTime.bind(this))
    // this.$el.addEventListener('musicChanged', this.setCurrentMusic.bind(this))
  },

  methods: {
    formatTime(milliseconds) {
      const date = new Date(milliseconds);
      const minutes = date.getMinutes();
      const seconds = date.getSeconds();
      const formattedMinutes = minutes.toString();
      const formattedSeconds = seconds.toString().padStart(2, 0);

      return `${formattedMinutes}:${formattedSeconds}`;
    },

    manipulateStart(widthPercentage) {
      this.core.pauseToReturnCurrentTime();
      this.calcCurrentTime(widthPercentage)
      // this.$el.removeEventListner('timeChanged', this.setCurrentTime.bind(this))
    },

    manipulateEnd() {
      this.onPlay && this.play();
      // this.$el.addEventListener('timeChanged', this.setCurrentTime.bind(this))
    },

    calcCurrentTime(widthPercentage) {
      this.currentTime = (widthPercentage / 100) * this.currentMusic.runningTime;
    },

    setCurrentTime(time) {
      this.currentTime = time;
      this.$refs.playerControlsSlider.widthPercentage = ( this.currentTime / this.currentMusic.runningTime) * 100;
    },
    
    togglePlayPauseButton() {
      this.onPlay = !this.onPlay;
      this.onPlay ? this.play() : this.pause();
    },

    play() {
      this.core.play(this.currentTime);
    },

    pause() {
      this.core.pause();
    },

    // 외부에서 음악 실행
    playPlayQueue(playQueue) {
      this.pause();

      this.core.setPlayQueue(playQueue);
      this.currentTime = 0;

      if(!this.onPlay) {
        this.onPlay = true;
      }

      this.play();
    },

    // 다음 음악 세팅
    setCurrentMusic(music) {
      this.currentMusic = music;
    }
  }
}
</script>

<style scoped lang="scss">
@import '../index.scss';

.player {
  box-sizing: border-box;
  position: fixed;
  width: 100vw; height: $player-height;
  bottom: 0px;
  background: #272727;
}

/* for child element(position absolute) */
.player__wrap {
  position: relative;
  width: 100%; height: 100%;
}

.player__controls {
  position: absolute;
  left: 50%;
  transform: translate3d(-50%,0,0);
  width: 40vw; height: 100%;
}

// for child element(position absolute)
.player__controls-wrap {
  position: relative;
}

.player__button-wrap {
  position: absolute;
  width: 100%;
  top: 10px; left: 50%;
  transform: translate3d(-50%, 0, 0);
  text-align: center;
  @include vertical-align-helper;
}

.player__play-button {
  display: inline-block;
  vertical-align: middle;
  width: 33px; height: 33px;
  margin-right: 23px;
}

.player__controls-core {
  box-sizing: border-box;
  position: absolute;
  width: 100%; height: 20px;
  top: 45px; left: 0px;
  white-space: nowrap;
  @include vertical-align-helper;

  &:hover {
    .player__slider-location {
      background: $signature-color;

      // drag-button
      &::before { display: block }
    }
  }
}

.player__current-time, .player__total-time {
  display: inline-block;
  width: 30px;
  vertical-align: middle;
  font-size: 0.8rem;
  text-align: center;
  color: #ccc;
}

.player__controls-slider {
  display: inline-block;
  vertical-align: middle;
  width: calc(100% - 80px); height: 20px;
  margin: 0 10px;
}

.player__option-wrap {
  position: absolute;
  right: 0px;
  width: 150px; height: 100%;
  @include vertical-align-helper;
}

.player__option {
  width: 22px; height: 22px;
  display: inline-block;
  vertical-align: middle;
  margin-right: 5px;
  
  &--mute {
    background: no-repeat top/100% url(../assets/mute.png);
  }

  &:nth-last-child(1) { margin-right: 5px; }
}

.player__volume-slider {
  display: inline-block;
  vertical-align: middle;
  width: 90px; height: 22px;
  margin-left: 2px;
}

</style>
