<template>
  <div
    class="wrap"
    @dblclick="playMusic"
    @contextmenu="openContextMenu"
  >
    <div
      class="field"
      v-for="field in playButtonFields"
      :key="field.value"
      :style="{'width': field.width, 'text-align': field.textAlign, 'margin-right': field.marginRight}"
    >
      <span class="field__name field__name--play-button">{{item[field.value]}}</span>
      <div class="field__play-button">
        <BaseButtonPlayPause
          :background="'rgba(0,0,0,0.3)'"
          :hoverBackground="'rgba(0,0,0,0.6)'"
          @button-clicked="playMusic"
        />
      </div>
    </div>
    <div
      class="field"
      v-for="field in nonPlayButtonFields"
      :key="field.value"
      :style="{'width': field.width, 'text-align': field.textAlign, 'margin-right': field.marginRight}"
    >
      <span class="field__name">{{item[field.value]}}</span>
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
  name: 'MainViewListItem',

  props: {
    item: Object,
    fields: Array
  },

  data() {
    return {
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
              callback: () => { playlist.addMusic(this.item.source); },
            })
          }, [
            {
              name: 'New Playist',
              callback: () => {
                const newPlaylist = IPlaylistService.createPlaylist(this.item.title);
                newPlaylist.addMusic(this.item.source);
              }
            }
          ])
        }
      ]
    }
  },

  computed: {
    playButtonFields() {
      return this.fields.filter(field => field.playButton === true);
    },

    nonPlayButtonFields() {
      return this.fields.filter(field => field.playButton !== true);
    }
  },

  methods: {
    playMusic() {
      this.$emit('music-played', this.item.source)
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

.wrap {
  background: #191919;
  border-bottom: 1px solid #333;
  overflow: hidden;

  &:hover {
    cursor: default;
    background: #333;

    .field__play-button {visibility: visible;}
    .field__name--play-button {visibility: hidden}
  }
}

.field {
  position: relative;
  float: left;
  height: 40px;
  line-height: 40px;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  color: #fff;
}

.field__name {
  font-size: 0.9rem;
  font-weight: lighter;
}

.field__play-button {
  position: absolute;
  top: 50%; left: 50%;
  width: 29px; height: 29px;
  transform: translate3d(-50%, -50%, 0);
  visibility: hidden;
}


</style>
