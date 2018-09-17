<template>
  <section id='player' class="player">
    <div class="player__wrap">
      <div class="player__current-song">
        <div class="player__album-cover"></div>
        <div class="player__song-title">Title</div>
        <div class="player__singer">singer</div>
      </div>
      <div class="player__controls">
        <div class="player__controls-wrap">
          <div class="player__button-wrap">
            <div class="player__button player__button--shuffle"></div>
            <div class="player__button player__button--back"></div>
            <div
              class='player__button'
              :class='onPlay ? "player__button--pause" : "player__button--play"'
              @click='togglePlay'>
            </div>
            <div class="player__button player__button--next"></div>
            <div class="player__button player__button--repeat"></div>
          </div>
          <div class="player__controls-core">
            <div class="player__current-time">{{formatTime(this.currentTime)}}</div>
            <div class='player__controls-slider'>
              <HorizonSlider
                ref="slider"
                v-on:hold-slider-thumb='calcCurrentTime'>
              </HorizonSlider>
            </div>
            <div class="player__total-time">{{formatTime(this.currentMusic.runningTime)}}</div>
          </div>
        </div>
      </div>
      <div class="player__option-wrap">
        <div class="player__option player__option--queue"></div>
        <div class="player__option player__option--devices"></div>
        <div class="player__option player__option--mute"></div>
        <div class="player__volume">
          <div style='width: 80%;' class="player__volume-location"></div>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import HorizonSlider from './HorizonSlider.vue';
import DummyCore from './dummyCore.js';

const core = new DummyCore({
  musicList: [{runningTime: 210000}]
})

export default {
  name: 'Player',

  components: {
    HorizonSlider
  },

  data() {
    return {
      core: core,
      currentMusic: null,
      currentTime: 0,
      onPlay: false
    }
  },

  created() {
    this.currentMusic = this.core.currentMusic;
    this.core.onReturnCurrentTime = this.setCurrentTime.bind(this);
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

    calcCurrentTime(widthPercentage) {
      this.currentTime = (widthPercentage / 100) * this.currentMusic.runningTime;
    },

    setCurrentTime(time) {
      this.currentTime = time;
      this.$refs.slider.widthPercentage = ( this.currentTime / this.currentMusic.runningTime) * 100;
    },
    
    togglePlay() {
      this.onPlay = !this.onPlay;
      this.onPlay ? this.run() : this.pause();
    },

    run() {
      this.core.play();
    },

    pause() {
      this.core.pause();
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

.player__current-song {
  position: absolute;
  left: 0px;
  width: 270px; height: 100%;
}

.player__album-cover {
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
  padding-top: 5px;
  vertical-align: middle;
  @include vertical-align-helper;
}

.player__singer {
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
  width: 220px;
  top: 10px; left: 50%;
  transform: translate3d(-50%, 0, 0);
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

  &--play {
    box-sizing: border-box;
    width: 33px; height: 33px;
    border: 1px solid #ccc;
    border-radius: 50%;
    position: relative;
    transition: transform .05s;
    text-align: center;
    @include vertical-align-helper;

    &::after {
      content: '';
      display: inline-block;
      top: 50%; left: 50%;
      vertical-align: middle;
      @include arrow($direction: right, $width: 11px, $height: 14px, $color: #fff);
      transform: translate3d(2px, -1px, 0);
    }

    &:hover {
      transform: scale(1.1);
      border-color: #fff
    }
  }

  &--pause {
    box-sizing: border-box;
    width: 33px; height: 33px;
    border: 1px solid #ccc;
    border-radius: 50%;
    position: relative;
    transition: transform .05s;
    text-align: center;
    @include vertical-align-helper;

    &::before, &::after {
      content: '';
      width: 3px; height: 13px; background: #fff;
      display: inline-block;
      vertical-align: middle;
      transform: translate3d(0, 8px, 0);
      margin-right: 5px;
    }

    &::after {
      margin-right: 0px;
    }

    &:hover {
      transform: scale(1.1);
      border-color: #fff
    }
  }

  &:nth-last-child(1) { margin: 0 };
}

.player__controls-core {
  box-sizing: border-box;
  position: absolute;
  width: 100%; height: 20px;
  top: 45px;
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
  width: $player-time-size; height: 10px;
  vertical-align: middle;
  font-size: 0.8rem;
  text-align: center;
  color: #ccc;
}

.player__option-wrap {
  position: absolute;
  right: 0px;
  width: 200px; height: 100%;
  @include vertical-align-helper;
}

.player__option {
  width: 22px; height: 22px;
  display: inline-block;
  vertical-align: middle;
  margin-right: 4px;
  
  &--queue {
    background: no-repeat top/100% url(../assets/queue.png);
  }
  
  &--devices {
    background: no-repeat top/100% url(../assets/devices.png);
  }
  
  &--mute {
    background: no-repeat top/100% url(../assets/mute.png);
  }

  &:nth-last-child(1) { margin-right: 5px; }
}

.player__volume {
  display: inline-block;
  vertical-align: middle;
  width: 75px; height: 4px; background: #3a3a3a;
  border-radius: 4px;

  &:hover {
    .player__volume-location {
      background: $signature-color;

      // drag button
      &::before { display: block }
    }
  }
}

.player__volume-location {
  height: 100%; background: #ccc;
  border-radius: 4px;
  position: relative;

  // drag button
  &::before {
    content:'';
    width: 12px; height: 12px; background: #fff;
    display: none;
    position: absolute;
    top: 0px; left: 100%;
    border-radius: 50%;
    transform: translate3d(-50%, -35%, 0);
  }
}

.player__controls-slider {
  display: inline-block;
  vertical-align: middle;
  width: 300px; height: 20px;
}

</style>
