<template>
  <div
    class="artist-window-wrap"
    :class="{'artist-window-wrap--active': isActive, 'artist-window-wrap--inactive': !isActive}">
    <div
      class="artist-window"
      :class="{'artist-window--active': isActive, 'artist-window--inactive': !isActive}">
      <header class="artist-window__header">
        <div
        class="artist-window__picture"
        :style="{ background: 'no-repeat center/100% url(' + picture + ')' }">
        </div>
        <div class="artist-window__description">
          <div class="artist-window__name">{{name}}</div>
        </div>
        <div
          class="artist-window__play-button"
          :class="{'artist-window__play-button--pressed': isPlayButtonPressed}"
          @mousedown="isPlayButtonPressed = true"
          @mouseup="playArtistAllMusic">PLAY
        </div>
        <div class="artist-window__field">
          <div class="artist-window__field-number">#</div>
          <div class="artist-window__field-title">TITLE</div>
          <div class="artist-window__field-album">ALBUM</div>
          <div class="artist-window__field-time">TIME</div>
          <div class="artist-window__field-more"></div>
        </div>
      </header>
      <ul class="artist-window__music-list">
        <WindowMusicListItem
          v-for="(music, idx) in musics"
          :music="music"
          :key="idx"
          :index="idx"
          @music-played="playArtist"/>
      </ul>
    </div>
  </div>
</template>

<script>
import WindowMusicListItem from './WindowMusicListItem.vue';

export default {
  name: 'ArtistWindow',

  components: {
    WindowMusicListItem
  },

  data() {
    return {
      artist: null,
      isActive: false,
      isPlayButtonPressed: false
    }
  },

  // close ArtistWindow
  created() {
    const closeArtistWindow = function({target}) {
      if(target.closest('.artist-window')) return;

      this.isActive = false;
      this.artist = null;
    }

    document.body.addEventListener('mousedown', closeArtistWindow.bind(this));
  },

  methods: {
    open(artist) {
      this.isActive = true;
      this.artist = artist;
    },

    playArtistAllMusic() {
      this.playArtist(0);
    },

    playArtist(index) {
      this.isPlayButtonPressed = false;
      this.$emit('artist-played', {currentMusicIdx: index || 0, musics: this.artist.musics});
    },

    formatTime(milliseconds) {
      const date = new Date(milliseconds);
      const minutes = date.getMinutes();
      const seconds = date.getSeconds();
      const formattedMinutes = minutes.toString();
      const formattedSeconds = seconds.toString().padStart(2, 0);

      return `${formattedMinutes}:${formattedSeconds}`;
    }
  },

  computed: {
    picture() {
      return this.artist ? this.artist.picture : '';
    },

    name() {
      return this.artist ? this.artist.name : '';
    },

    musics() {
      return this.artist ? this.artist.musics : [];
    }
  }
}
</script>

<style scoped lang="scss">
@import '../../index.scss';

$picture-size: 100px;

.artist-window-wrap {
  position: fixed;
  top: 0px; left: $main-menu-width;
  z-index: 1000; // viewHeader(1) < mainHeader(100) < 1000
  width: calc(100vw - #{$main-menu-width}); height: calc(100vh - #{$player-height});
  background: rgba(0, 0, 0, 0.4);

  &--inactive {
    transition: visibility 0s .55s, opacity .5s;
    visibility: hidden;
    opacity: 0;
  }

  &--active {
    transition: visibility 0s, opacity .5s .05s;
    visibility: visible;
    opacity: 1;
  }
}


.artist-window {
  box-sizing: border-box;
  position: absolute;
  top: 50%; left: 50%;
  width: 60%; height: 70%;
  background: #191919;
  border-radius: 10px;
  padding: 0px 15px;
  overflow: scroll;
  box-shadow: 2px 2px 20px 5px #070707;
  transition: transform .5s;

  &--inactive { transform: translate3d(-50%, -60%, 0); }

  &--active { transform: translate3d(-50%, -50%, 0); }
}

.artist-window__header {
  position: sticky;
  top: 0px;
  width: 100%;
  background: #191919;
  border-bottom: 1px solid #333;
  padding-top: 15px;
  overflow: hidden;
  z-index: 1;
}

.artist-window__picture {
  float: left;
  width: $picture-size; height: $picture-size;
  border-radius: 50%;
}

.artist-window__description {
  box-sizing: border-box;
  position: relative;
  float: left;
  margin-left: 14px;
  width: 60%; height: $picture-size;
}

.artist-window__name {
  position: absolute;
  bottom: 0px;
  color: #fff;
  font-weight: 900;
  font-size: 2rem;
}

.artist-window__play-button {
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

.artist-window__field {
  float: left;
  width: 100%; height: 20px;
  margin-top: 25px;
}

@mixin field-props {
  color: #999;
  font-size: 0.9rem;
  float: left;
}

.artist-window__field-number {
  @include field-props;
  width: 10%;
  margin-right: 2%;
  text-align: center;
}

.artist-window__field-title {
  @include field-props;
  width: 40%;
  margin-right: 5%;
}

.artist-window__field-album {
  @include field-props;
  width: 20%;
  margin-right: 5%;
}

.artist-window__field-time {
  @include field-props;
  text-align: center;
  width: 9%;
}

.artist-window__field-more {
  @include field-props;
  width: 9%; height: 20px;
}

.artist-window__music-list {
  width: 100%;
  padding-bottom: 15px;
}

</style>
