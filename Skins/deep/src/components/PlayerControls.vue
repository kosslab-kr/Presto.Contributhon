<template>
  <div class="player__controls">
    <div class="player__controls-wrap">
      <div class="player__button-wrap">
        <PlayerButtonShuffle
          @button-clicked="shuffle"
        />
        <PlayerButtonBack
          @button-clicked="playPrevious"
        />
        <div class="player__play-button">
          <BaseButtonPlayPause
            :onPlay="onPlay"
            @button-clicked="togglePlayPauseButton"
          />
        </div>
        <PlayerButtonNext
          @button-clicked="playNext"
        />
        <PlayerButtonRepeat
          @button-clicked="repeat"
        />
      </div>
      <div class="player__controls-core">
        <div class="player__current-time">{{formatTime(currentTime)}}</div>
        <div class='player__controls-slider'>
          <PlayerSliderHorizon
            ref="playerSliderHorizon"
            @slider-thumb-down='sliderDown'
            @slider-thumb-move='sliderMove'
            @slider-thumb-up='sliderUp'
          >
          </PlayerSliderHorizon>
        </div>
        <div class="player__total-time">{{formatTime(music.length)}}</div>
      </div>
    </div>
  </div>
</template>

<script>
import PlayerSliderHorizon from './PlayerSliderHorizon.vue';
import PlayerButtonShuffle from './PlayerButtonShuffle.vue';
import PlayerButtonBack from './PlayerButtonBack.vue';
import PlayerButtonNext from './PlayerButtonNext.vue';
import PlayerButtonRepeat from './PlayerButtonRepeat.vue';
import IPlayerService from './IPlayerService.js';

export default {
  name: 'PlayerControls',

  components: {
    PlayerSliderHorizon,
    PlayerButtonShuffle,
    PlayerButtonBack,
    PlayerButtonNext,
    PlayerButtonRepeat
  },

  data() {
    return {
      music: {length: 0},
      currentTime: 0,
      onPlay: false,
    }
  },

  mounted() {
    IPlayerService.addEventListener('onPositionChanged', this.setCurrentTime);
    IPlayerService.addEventListener('onStreamChanged', ({music}) => { this.setMusic(music); });
  },

  methods: {
    play(music) {
      this.setMusic(music);
      IPlayerService.play(music);
    },

    pause() {
      IPlayerService.pause();
    },

    resume() {
      IPlayerService.resume();
    },

    playPrevious() {
      IPlayerService.playPrevious();
    },

    playNext() {
      IPlayerService.playNext();
    },

    // @param {Boolean} isShuffled
    shuffle({isShuffled}) {
      // when shuffle button is clicked
      console.log(isShuffled);
    },

    // @param {Number} repeatMode - 0 is None, 1 is once repeat, 2 is infinite repeat
    repeat({repeatMode}) {
      // when repeat button is clicked
      console.log(repeatMode);
    },

    setCurrentTime({position}) {
      this.currentTime = position;
      this.setSliderWidthPercentage({time: this.currentTime, length: this.music.length});
    },

    setMusic(music) {
      this.onPlay = true;
      this.music = music;
      this.currentTime = 0;
      this.$emit('music-changed', music);
    },

    togglePlayPauseButton() {
      this.onPlay = !this.onPlay;
      this.onPlay ? this.resume() : this.pause();
    },

    setSliderWidthPercentage({time, length}) {
      this.$refs.playerSliderHorizon.widthPercentage = ( time / length) * 100;
    },

    sliderDown(widthPercentage) {
      this.onPlay && IPlayerService.removeEventListener('onPositionChanged', this.setCurrentTime);
      this.currentTime = this.calcTimeByPercentage({percentage: widthPercentage, length: this.music.length});
    },

    sliderMove(widthPercentage) {
      this.currentTime = this.calcTimeByPercentage({percentage: widthPercentage, length: this.music.length});
    },

    sliderUp() {
      IPlayerService.setPosition(this.currentTime);
      this.onPlay && IPlayerService.addEventListener('onPositionChanged', this.setCurrentTime);
    },

    calcTimeByPercentage({percentage, length}) {
      return (percentage / 100) * length;
    },

    formatTime(milliseconds) {
      const date = new Date(milliseconds);
      const minutes = date.getMinutes();
      const seconds = date.getSeconds();
      const formattedMinutes = minutes.toString();
      const formattedSeconds = seconds.toString().padStart(2, 0);

      return `${formattedMinutes}:${formattedSeconds}`;
    },
  },
}
</script>

<style scoped lang="scss">
@import '../index.scss';

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

</style>
