<template>
  <div class="music" @mouseover="_startSlide" @mouseleave="_stopSlide">
    <div
      class="music__picture"
      :style="{background: 'no-repeat center/100% url(' + music.album.picture + ')'}">
      <div class="music__play-button">
        <PlayPauseButton
          :background="'rgba(0,0,0,0.5)'"
          :hoverBackground="'rgba(0,0,0,0.7)'"
          @button-clicked="playMusic"
        />
      </div>
      <div class="music__picture-cover"></div>
    </div>
    <div class="music__description">
      <div class="music__title">
        <InfiniteTextSlider ref="infiniteTextSlider" :text="music.title"/>
      </div>
      <div class="music__artist">{{music.artist}}</div>
    </div>
  </div>
</template>

<script>
import PlayPauseButton from '../PlayPauseButton.vue';
import InfiniteTextSlider from './InfiniteTextSlider.vue';

export default {
  name: 'Music',

  components: {
    PlayPauseButton,
    InfiniteTextSlider
  },

  props: {
    music: Object,
    index: Number
  },

  methods: {
    _startSlide() {
      this.$refs.infiniteTextSlider.onPlay = true;
    },

    _stopSlide() {
      this.$refs.infiniteTextSlider.onPlay = false;
    },

    playMusic() {
      this.$emit('music-played', this.index);
    }
  }
}
</script>

<style scoped lang="scss">
@import '../../index.scss';

$picture-size: 60px;

.music {
  box-sizing: border-box;
  float: left;
  width: 33%; height: 70px;
  margin-right: 0.5%; margin-bottom: 1%;
  padding: 5px;

  &:nth-child(3n) {
    margin-right: 0px;
  }

  &:hover {
    cursor: default;
    background: #111;

    .music__picture-cover { visibility: visible; }
    .music__play-button { visibility: visible; }
  }
}

.music__picture {
  float: left;
  position: relative;
  width: $picture-size; height: $picture-size;
}

.music__picture-cover {
  visibility: hidden;
  width: 100%; height: 100%;
  background: rgba(0,0,0,0.6);
}

.music__play-button {
  visibility: hidden;
  position: absolute;
  top: 50%; left: 50%;
  transform: translate3d(-50%, -50%, 0);
  width: 60%; height: 60%;
}

.music__description {
  box-sizing: border-box;
  float: left;
  width: calc(100% - #{$picture-size}); height: 100%;
  padding: 5px 8px;
}

.music__artist {
  color: #bbb;
  white-space: nowrap;
  overflow: hidden;
  font-weight: lighter;
  line-height: 1.3;
}

</style>
