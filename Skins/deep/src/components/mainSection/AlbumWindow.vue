<template>
  <div class="album-window-wrap">
    <div
      class="album-window"
      @mouseover="_startSlide"
      @mouseleave="_stopSlide">
      <header class="album-window__header">
        <div
        class="album-window__picture"
        :style="{ background: 'no-repeat center/100% url(' + picture + ')' }">
        </div>
        <div class="album-window__description">
          <div class="album-window__title">
            <InfiniteTextSlider ref="infiniteTextSlider" :text="title" :fontStyle="{'color': '#fff', 'font-size': '2rem', 'font-weight': '900'}" :delay="1"/>
          </div>
          <div class="album-window__artist"><span class="album-window__artist-by">By</span>{{artist}}</div>
        </div>
        <div
          class="album-window__play-button"
          :class="{'album-window__play-button--pressed': isPlayButtonPressed}"
          @mousedown="isPlayButtonPressed = true"
          @mouseup="_playAlbum">PLAY
        </div>
        <div class="album-window__field">
          <div class="album-window__field-number">#</div>
          <div class="album-window__field-title">TITLE</div>
          <div class="album-window__field-time">TIME</div>
        </div>
      </header>
      <ul class="album-window__music-list">
        <li
          v-for="(music, idx) in musics"
          :key="idx"
          class="album-window__music">
          <div class="album-window__music-number">{{idx+1}}</div>
          <div class="album-window__music-title">{{music.title}}</div>
          <div class="album-window__music-time">{{_formatTime(music.runningTime)}}</div>
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
import InfiniteTextSlider from './InfiniteTextSlider.vue';

export default {
  name: 'AlbumWindow',

  components: {
    InfiniteTextSlider
  },

  props: {
    album: Object
  },

  data() {
    return {
      isPlayButtonPressed: false
    }
  },

  // close AlbumWindow
  mounted() {
    const closeAlbumWindow = function({target}) {
      if(target.closest('.album-window')) return;

      this.$emit('window-closed');
    }

    document.body.addEventListener('mousedown', closeAlbumWindow.bind(this));
  },

  methods: {
    _playAlbum() {
      this.isPlayButtonPressed = false;
      this.$emit('album-played', {currentMusicIdx: 0, musics: this.album.musics});
    },

    _formatTime(milliseconds) {
      const date = new Date(milliseconds);
      const minutes = date.getMinutes();
      const seconds = date.getSeconds();
      const formattedMinutes = minutes.toString();
      const formattedSeconds = seconds.toString().padStart(2, 0);

      return `${formattedMinutes}:${formattedSeconds}`;
    },

    _startSlide() {
      if(!this.$refs.infiniteTextSlider) return;
      this.$refs.infiniteTextSlider.onPlay = true;
    },

    _stopSlide() {
      if(!this.$refs.infiniteTextSlider) return;
      this.$refs.infiniteTextSlider.onPlay = false;
    }
  },

  computed: {
    picture() {
      return this.album ? this.album.picture : '';
    },

    title() {
      return this.album ? this.album.title : '';
    },

    artist() {
      return this.album ? this.album.artist : '';
    },

    musics() {
      return this.album ? this.album.musics : [];
    }
  }
}
</script>

<style scoped lang="scss">
@import '../../index.scss';

$picture-size: 100px;

.album-window-wrap {
  position: fixed;
  top: 0px; left: $main-menu-width;
  z-index: 1000; // viewHeader(1) < mainHeader(100) < 1000
  width: calc(100vw - #{$main-menu-width}); height: calc(100vh - #{$player-height});
  text-align: center;
  @include vertical-align-helper;
}

.album-window {
  display: inline-block;
  vertical-align: middle;
  box-sizing: border-box;
  width: 60%; height: 70%;
  background: #191919;
  border-radius: 10px;
  padding: 0px 15px;
  overflow: scroll;
  box-shadow: 2px 2px 20px 5px #070707;
  z-index: 1000; // viewHeader(1) < mainHeader(100) < 1000
  text-align: left;
}

.album-window__header {
  position: sticky;
  top: 0px;
  width: 100%;
  background: #191919;
  border-bottom: 1px solid #333;
  padding-top: 15px;
  overflow: hidden;
}

.album-window__picture {
  float: left;
  width: $picture-size; height: $picture-size;
}

.album-window__description {
  box-sizing: border-box;
  float: left;
  margin-left: 14px;
  width: 60%; height: $picture-size;
  padding-top: 40px;
}

.album-window__title {
  line-height: 1.3;
}

.album-window__artist-by {
  color: #999;
  margin: 0 4px;
}

.album-window__artist {
  color: #fff;
  font-weight: lighter;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
}

.album-window__play-button {
  position: absolute;
  top: 20px; right: 15px;
  width: 80px;
  line-height: 25px;
  border-radius: 20px;
  background: $signature-color;
  color: #fff;
  font-size: 0.7rem;
  letter-spacing: 2px;
  text-align: center;
  overflow: hidden;

  &--pressed {
    transform: scale(1) !important;

    // cover
    &::after {
      position: absolute;
      top: 0px; left: 0px;
      content: '';
      width: 100%; height: 100%;
      background: rgba(0, 0, 0, 0.4);
    }
  }

  &:hover {
    cursor: default;
    transform: scale(1.05);
    background: #13f74c;
  }
}

.album-window__field {
  float: left;
  width: 100%; height: 20px;
  margin-top: 25px;
}

@mixin field-props {
  color: #999;
  font-size: 0.9rem;
  text-align: center;
}

.album-window__field-number {
  @include field-props;
  float: left;
  width: 40px;
  margin-right: 20px;
}

.album-window__field-title {
  @include field-props;
  float: left;
}

.album-window__field-time {
  @include field-props;
  float: right;
  width: 60px;
}

.album-window__music-list {
  width: 100%;
  padding-bottom: 15px;
}

.album-window__music {
  width: 100%;
  line-height: 2.3rem;
  border-bottom: 1px solid #333;
  overflow: hidden;

  &:hover {
    cursor: default;
    background: #333;
  }
}

@mixin music-props {
  font-size: 0.9rem;
  font-weight: lighter;
}

.album-window__music-number {
  @extend .album-window__field-number;
  @include music-props;
  color: #fff;
}

.album-window__music-title {
  @extend .album-window__field-title;
  @include music-props;
  color: #fff;
}

.album-window__music-time {
  @extend .album-window__field-time;
  @include music-props;
}

</style>
