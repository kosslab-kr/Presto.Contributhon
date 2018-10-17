<template>
  <div
    class="music"
    @mouseover="isMouseOvered = true"
    @mouseleave="isMouseOvered = false"
    @dblclick="playMusic"
    @contextmenu="openContextMenu"
  >
    <div
      class="music__picture"
      :style="{background: 'no-repeat center/100% url(' + music.album.picture + ')'}">
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
          :text="music.title"
          :onPlay="isMouseOvered"
        />
      </div>
      <div class="music__artist">{{music.artist.name}}</div>
    </div>
    <BaseContextMenu
      v-if="isContextMenuOpened"
      :style="contextMenuStyle"
      :items="contextMenuItems"
      @outside-clicked="closeContextMenu"
      @item-clicked="closeContextMenu"
    />
  </div>
</template>

<script>
import IPlaylistService from './IPlaylistService.js';

export default {
  name: 'MainMusicsItem',

  props: {
    music: Object,
  },

  data() {
    return {
      isMouseOvered: false,
      isContextMenuOpened: false,
      contextMenuStyle: {
        top: '0px',
        left: '0px',
      },
      contextMenuItems: [
        {
          name: '음악 재생',
          callback: this.playMusic.bind(this),
        },
        {
          name: '플레이리스트에 추가',
          subItems: IPlaylistService.playlists.reduce((items, playlist) => {
            return items.concat({
              name: playlist.name,
              callback: () => { playlist.addMusic(this.music); },
            })
          }, [
            {
              name: 'New Playist',
              callback: () => {
                const newPlaylist = IPlaylistService.createPlaylist(this.music.title);
                newPlaylist.addMusic(this.music);
              }
            }
          ])
        },
      ],
    }
  },

  methods: {
    playMusic() {
      this.$emit('music-played', this.music);
    },

    openContextMenu(e) {
      this.isContextMenuOpened = true;
      this.contextMenuStyle = {
        top: `${e.clientY}px`,
        left: `${e.clientX}px`,
      }
      e.preventDefault();
    },

    closeContextMenu() {
      this.isContextMenuOpened = false;
    }
  }
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
