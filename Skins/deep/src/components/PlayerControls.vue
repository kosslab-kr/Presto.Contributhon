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
        <div class="player__total-time">{{formatTime(length)}}</div>
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
      music: {length: 100000},
      currentTime: 0,
      onPlay: false,
      length: 0,
      isSliderPressed: false,
    };
  },

  mounted() {
    window.addEventListener('load', () => {
      player.on('positionChanged', this.changePosition);
      player.on('currentMusicChanged', this.changeCurrentMusic);
    })
  },

  methods: {
    play(music) {
      this.setMusic(music);
      player.playByMusic(music);
    },

    pause() {
      player.pause();
    },

    resume() {
      player.resume();
    },

    playPrevious() {
      player.playPrevious();
    },

    playNext() {
      player.playNext();
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

    changePosition() {
      player.getPosition().then(position => {
        if(this.isSliderPressed) return;
        this.setCurrentTime(position);
      });
    },

    async changeCurrentMusic() {
      let music = await player.getCurrentMusic();
      let length = await player.getLength();

      this.length = length;
      this.setMusic(music);
    },

    setCurrentTime(position) {
      this.currentTime = position;
      this.setSliderWidthPercentage({time: this.currentTime, length: this.length});
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
      this.isSliderPressed = true;
      this.onPlay && player.off('positionChanged', this.changePosition);
      this.currentTime = this.calcTimeByPercentage({percentage: widthPercentage, length: this.length});
    },

    sliderMove(widthPercentage) {
      this.currentTime = this.calcTimeByPercentage({percentage: widthPercentage, length: this.length});
    },

    sliderUp() {
      this.isSliderPressed = false;
      player.setPosition(this.currentTime);
      this.onPlay && player.on('positionChanged', this.changePosition);
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
