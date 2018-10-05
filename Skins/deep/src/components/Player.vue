<template>
  <section id='player' class="player">
    <div class="player__wrap">
      <div class="player__current-music">
        <img
          class="player__album-picture"
          :src="currentMusic.album.picture"
          alt="album-picture">
        <div class="player__song-title">{{currentMusic.title}}</div>
        <div class="player__artist">{{currentMusic.artist}}</div>
      </div>
      <div class="player__controls">
        <div class="player__controls-wrap">
          <div class="player__button-wrap">
            <div class="player__button player__button--shuffle"></div>
            <div class="player__button player__button--back"></div>
            <div class="player__button" style="width: 33px; height: 33px">
              <BaseButtonPlayPause
                ref="playPauseButton"
                @button-clicked="togglePlayPauseButton"/>
            </div>
            <div class="player__button player__button--next"></div>
            <div class="player__button player__button--repeat"></div>
          </div>
          <div class="player__controls-core">
            <div class="player__current-time">{{formatTime(this.currentTime)}}</div>
            <div class='player__controls-slider'>
              <PlayerSliderHorizon
                ref="playerControlsSlider"
                v-on:touch-slider-thumb='manipulateStart'
                v-on:hold-slider-thumb='calcCurrentTime'
                v-on:release-slider-thumb='manipulateEnd'>
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

const core = new DummyCore({
  playQueue: {currentMusicIdx: 0, musics: []}
})

export default {
  name: 'Player',

  components: {
    PlayerSliderHorizon,
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

    manipulateStart() {
      this.core.pauseToReturnCurrentTime();
    },

    manipulateEnd() {
      this.onPlay && this.play();
    },

    calcCurrentTime(widthPercentage) {
      this.currentTime = (widthPercentage / 100) * this.currentMusic.runningTime;
    },

    setCurrentTime(time) {
      this.currentTime = time;
      this.$refs.playerControlsSlider.widthPercentage = ( this.currentTime / this.currentMusic.runningTime) * 100;
    },
    
    togglePlayPauseButton() {
      this.$refs.playPauseButton.onPlay = !this.$refs.playPauseButton.onPlay;
      this.onPlay = !this.onPlay;
      this.onPlay ? this.play() : this.pause();
    },

    play() {
      this.core.play(this.currentTime);
    },

    pause() {
      this.core.pause();
    },

    playPlayQueue(playQueue) {
      this.pause();

      this.core.setPlayQueue(playQueue);
      this.currentTime = 0;

      if(!this.onPlay) {
        this.onPlay = true;
        this.$refs.playPauseButton.onPlay = true;
      }

      this.play();
    },

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

.player__current-music {
  position: absolute;
  left: 0px;
  width: 270px; height: 100%;
}

.player__album-picture {
  display: block;
  float: left;
  $album-cover-size: 50px !global;
  width: $album-cover-size; height: $album-cover-size;
  background: #fff;
  margin: ($player-height - $album-cover-size)/2;
}

.player__song-title {
  box-sizing: border-box;
  float: left;
  width: 190px; height: $album-cover-size/2;
  color: #fff;
  margin-top: ($player-height - $album-cover-size)/2;
  padding-top: 10px;
  vertical-align: middle;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  @include vertical-align-helper;
}

.player__artist {
  box-sizing: border-box;
  float: left;
  width: 190px; height: $album-cover-size/2;
  color: #ccc;
  font-size: 0.8rem;
  vertical-align: middle;
  @include vertical-align-helper;
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

.player__button {
  display: inline-block;
  width: 18px; height: 18px;
  vertical-align: middle;
  margin-right: 23px;

  &--shuffle {
    background: no-repeat center/100% url(../assets/shuffle.png);
  }

  &--back {
    position: relative;

    $icon-transforom: translate3d(3px, 3px, 0);

    &::before {
      position: absolute;
      content: '';
      display: block;
      transform: $icon-transforom;
      width: 2px; height: 12px; background: #ccc;
    }

    &::after {
      position: absolute;
      content: '';
      display: block;
      transform: $icon-transforom;
      @include arrow($direction: left, $width: 10px, $height: 12px, $color: #ccc);
    }
  }

  &--next {
    @extend .player__button--back;
    transform: rotate(180deg);
  }

  &--repeat {
    background: no-repeat center/100% url(../assets/repeat.png);
  }

  &:nth-last-child(1) { margin: 0 };
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
