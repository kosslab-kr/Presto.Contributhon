<template>
  <div
    class="music"
    @mouseover="isMouseOvered = true"
    @mouseleave="isMouseOvered = false"
    @dblclick="playMusic"
    @contextmenu="openContextMenu"
  >
    <div class="music__picture-wrap">
      <img
        class="music__picture"
        :src="music.Album.Picture"
        alt="music_picture"
      >
      <div class="music__play-button">
        <BaseButtonPlayPause
          :background="'rgba(0,0,0,0.5)'"
          :hoverBackground="'rgba(0,0,0,0.7)'"
          @button-clicked="playMusic"
        />
      </div>
      <div class="music__picture-cover"></div>
    </div>
    <div class="music__description">
      <div class="music__title">
        <BaseTextRolling
          :text="music.Title"
          :onPlay="isMouseOvered"
        />
      </div>
      <div class="music__artist">{{music.Artist.Name}}</div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'MainMusicsItem',

  props: {
    music: Object,
  },

  data() {
    return {
      isMouseOvered: false,
    }
  },

  methods: {
    playMusic() {
      this.$emit('music-played', this.music);
    },

    openContextMenu(e) {
      e.preventDefault();

      const contextMenuOption = {
        music: this.music,
        style: {
          top: `${e.clientY}px`,
          left: `${e.clientX}px`,
        },
      };

      this.$emit('context-menu-opened', contextMenuOption);
    },
  },
}
</script>

<style scoped lang="scss">
@import '../index.scss';

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

.music__picture-wrap {
  float: left;
  position: relative;
  width: $picture-size; height: $picture-size;
}

.music__picture {
  width: $picture-size; height: $picture-size;
}

.music__picture-cover {
  visibility: hidden;
  position: absolute;
  top: 0px; left: 0px;
  width: 100%; height: 100%;
  background: rgba(0,0,0,0.6);
}

.music__play-button {
  visibility: hidden;
  position: absolute;
  top: 50%; left: 50%;
  transform: translate3d(-50%, -50%, 0);
  width: 60%; height: 60%;
  z-index: 1;
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
