<template>
  <div class="music">
    <div class="music__play">
      <div class="music__play-icon">
        <PlayPauseButton
          :background="'rgba(0,0,0,0.3)'"
          :hoverBackground="'rgba(0,0,0,0.6)'"
          @button-clicked="playMusic"/>
      </div>
    </div>
    <div class="music__title">{{music.title}}</div>
    <div class="music__artist">{{music.artist}}</div>
    <div class="music__album">{{music.album.title}}</div>
    <div class="music__running-time">{{runningTime}}</div>
    <div class="music__more">
      <svg class="music__more-icon"
        version="1.1"
        baseProfile="full"
        width="25" height="15" viewBox="0 0 100 100"
        xmlns="http://www.w3.org/2000/svg">
      <circle cx="10" cy="50" r="10" fill="#fff"/>
      <circle cx="50" cy="50" r="10" fill="#fff"/>
      <circle cx="90" cy="50" r="10" fill="#fff"/>
      </svg>
    </div>
  </div>
</template>

<script>
import PlayPauseButton from "../PlayPauseButton.vue"

export default {
  name: 'MusicListItem',

  components: {
    PlayPauseButton
  },

  props: {
    music: Object,
    index: Number
  },

  computed: {
    runningTime() {
      const date = new Date(this.music.runningTime);
      const minutes = date.getMinutes();
      const seconds = date.getSeconds();
      const formattedMinutes = minutes.toString();
      const formattedSeconds = seconds.toString().padStart(2, 0);

      return `${formattedMinutes}:${formattedSeconds}`;
    }
  },

  methods: {
    playMusic() {
      this.$emit('music-played', this.index);
    }
  }
}
</script>

<style scoped lang="scss">
@import '../../index.scss';

.music {
  width: 100%; line-height: 2.6rem;
  border-bottom: 1px solid #333;
  overflow: hidden;

  &:hover {
    cursor: default;
    background: #333;

    .music__more-icon { display: block };
    .music__play-icon { display: block };
  }
}

@mixin music-props {
  float: left;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  color: #fff;
  font-size: 0.9rem;
}

.music__play {
  @include music-props;
  position: relative;
  width: 6%; height: 2.5rem;
  margin-right: 2%;
}

.music__play-icon {
  display: none;
  position: absolute;
  top: 50%; left: 50%;
  transform: translate3d(-50%, -50%, 0);
  width: 29px; height: 29px;
}

.music__title {
  @include music-props;
  width: 39%;
  margin-right: 5%;
}

.music__artist {
  @include music-props;
  width: 14%;
  margin-right: 5%;
}

.music__album {
  @include music-props;
  width: 14%;
  margin-right: 5%;
}

.music__running-time {
  @include music-props;
  text-align: center;
  color: #999;
  width: 5%;
}

.music__more {
  @include music-props;
  position: relative;
  width: 5%; height: 2.6rem;
}

.music__more-icon {
  display: none;
  position: absolute;
  top: 50%; left: 50%;
  transform: translate3d(-50%, -50%, 0);
}

</style>
