<template>
  <MainView :name="name">
    <template slot="body">
      <MainViewGroup
        v-for="(item, index) in items"
        :key="index"
        type="album"
        :group="item"
        @picture-selected="selectAlbum"
        @group-played="playMusic"
      />
    </template>
    <template slot="popup">
      <transition name="fade">
        <div
          v-if="!!selectedAlbum"
          class="popup-background"
        />
      </transition>
      <transition name="slide-fade">
        <div
          v-if="!!selectedAlbum"
          class="popup-wrap"
        >
          <div class="popup">
            <MainViewPopup
              type="album"
              :group="selectedAlbum"
              @popup-closed="selectedAlbum = null"
              @group-played="$emit('music-played', $event)"
            >
              <template slot="body">
                <MainViewList
                  :items="albumListItems"
                  :fields="albumListFields"
                  :height="'100%'"
                  @music-played="playMusic"
                  @context-menu-opened="openContextMenu"
                />
              </template>
            </MainViewPopup>
          </div>
        </div>
      </transition>
    </template>
    <template slot="context-menu">
      <BaseContextMenu
        v-if="isContextMenuOpened"
        :style="contextMenu.style"
        :items="contextMenu.items"
        @outside-clicked="closeContextMenu"
        @item-clicked="closeContextMenu"
      />
    </template>
  </MainView>
</template>

<script>
import IPlaylistService from './IPlaylistService.js';
import MainView from './MainView.vue';
import MainViewGroup from './MainViewGroup';
import MainViewPopup from './MainViewPopup.vue';
import MainViewList from './MainViewList.vue';

export default {
  name: 'MainAlbums',

  components: {
    MainView,
    MainViewGroup,
    MainViewPopup,
    MainViewList
  },

  props: {
    name: String,
  },

  data() {
    return {
      itemsOrigin: null,
      items: null,
      selectedAlbum: null,
      albumListItems: null,
      albumListFields: [
        {
          name: '#',
          value: 'number',
          width: '10%',
          textAlign: 'center',
          marginRight: '3%',
          playButton: true
        },
        {
          name: 'TITLE',
          value: 'title',
          width: '70%',
          textAlign: 'left',
          marginRight: '0%'
        },
        {
          name: 'TIME',
          value: 'time',
          width: '17%',
          textAlign: 'center',
          marginRight: '0%'
        }
      ],
      isContextMenuOpened: false,
      contextMenu: {
        music: null,
        style: {
          top: '',
          left: '',
        },
        items: [
          {
            name: '음악 재생',
            callback: () => { this.playMusic(this.contextMenu.music); },
          },
          {
            name: '플레이리스트에 추가',
            subItems: IPlaylistService.playlists.reduce((items, playlist) => {
              return items.concat({
                name: playlist.Name,
                callback: () => { playlist.addMusic(this.contextMenu.music); },
              })
            }, [
              {
                name: 'New Playist',
                border: true,
                callback: () => {
                  const newPlaylist = IPlaylistService.createPlaylist(this.contextMenu.music.Title);
                  newPlaylist.addMusic(this.contextMenu.music);
                  this.addPlaylistOnContextMenu(newPlaylist);
                }
              }
            ])
          },
        ],
      }
    }
  },

  created () {
    album.getAlbums().then(albums => {
      this.itemsOrigin = albums;
      this.items = albums;
    });
  },

  methods: {
    playMusic(music) {
      this.$emit('music-played', music);
    },

    formatTime(milliseconds) {
      const date = new Date(milliseconds);
      const minutes = date.getMinutes();
      const seconds = date.getSeconds();
      const formattedMinutes = minutes.toString();
      const formattedSeconds = seconds.toString().padStart(2, 0);

      return `${formattedMinutes}:${formattedSeconds}`;
    },

    openContextMenu(contextMenuOption) {
      this.contextMenu.music = contextMenuOption.music;
      this.contextMenu.style = contextMenuOption.style;
      this.isContextMenuOpened = true;
    },

    closeContextMenu() {
      this.isContextMenuOpened = false;
    },

    addPlaylistOnContextMenu(playlist) {
      const subItem = {
        name: playlist.Name,
        callback: () => {
          playlist.addMusic(this.contextMenu.music);
        },
      };
      this.contextMenu.items[1].subItems.push(subItem);
    },

    async selectAlbum(album) {
      const musics = await album.getMusics();

      this.albumListItems = musics.map((music, index) => {
        return {number: index+1, title: music.Title, time: this.formatTime(0), source: music};
      });
      this.selectedAlbum = album;
    },

    filterItem(keyword) {
      this.items = this.itemsOrigin.filter(album => {
        const albumName = album.Name.toLowerCase();
        
        return albumName.includes(keyword);
      })
    },
  },
}
</script>

<style scoped lang="scss">
@import '../index.scss';

.popup-background {
  position: fixed;
  top: 0px; left: $main-menu-width;
  width: calc(100vw - #{$main-menu-width}); height: calc(100vh - #{$player-height});
  background: rgba(0,0,0,0.6);
  z-index: 100;
}

.popup-wrap {
  @extend .popup-background;
  background: transparent;
  text-align: center;
  
  // vertical align helper
  &::before {
    content: '';
    display: inline-block;
    height: 100%;
    vertical-align: middle;
  }
}

.popup {
  display: inline-block;
  vertical-align: middle;
}

.fade-enter, .fade-leave-to {
  opacity: 0;
}

.fade-enter-active, .fade-leave-active {
  transition: opacity .5s;
}

.slide-fade-enter, .slide-fade-leave-to {
  opacity: 0;
  transform: translate3d(0, -70px, 0);
}

.slide-fade-enter-active, .slide-fade-leave-active {
  transition: opacity .5s, transform .5s;
}

</style>
