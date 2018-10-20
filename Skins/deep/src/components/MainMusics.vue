<template>
  <MainView :name="name">
    <template slot="body">
      <MainMusicsItem
        v-for="(item, index) in items"
        :key="index"
        :music="item"
        @music-played="playMusic"
        @context-menu-opened="openContextMenu"
      />
    </template>
    <template slot="context-menu">
      <BaseContextMenu
        v-if="isContextMenuOpened"
<<<<<<< HEAD
        :style="contextMenu.style"
        :items="contextMenu.items"
=======
        :style="contextMenuStyle"
        :items="contextMenuItems"
>>>>>>> refactor: contextMenu Component move to MainView Component
        @outside-clicked="closeContextMenu"
        @item-clicked="closeContextMenu"
      />
    </template>
  </MainView>
</template>

<script>
import IPlaylistService from './IPlaylistService.js';
import MainView from './MainView.vue';
import MainMusicsItem from './MainMusicsItem.vue';

export default {
  name: 'MainMusics',
  
  components: {
    MainView,
    MainMusicsItem
  },

  props: {
    name: String,
    items: Array
  },

  data() {
    return {
<<<<<<< HEAD
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
                name: playlist.name,
                callback: () => { playlist.addMusic(this.contextMenu.music); },
              })
            }, [
              {
                name: 'New Playist',
                border: true,
                callback: () => {
                  const newPlaylist = IPlaylistService.createPlaylist(this.contextMenu.music.title)
                  newPlaylist.addMusic(this.contextMenu.music);
                  this.addPlaylistOnContextMenu(newPlaylist);
                }
              }
            ])
          },
        ],
      }
=======
      playlistService: IPlaylistService,
      isContextMenuOpened: false,
      contextMenuStyle: null,
      music: null,
    }
  },

  computed: {
    contextMenuItems() {
      return [
        {
          name: '음악 재생',
          callback: () => { this.playMusic(this.music); },
        },
        {
          name: '플레이리스트에 추가',
          subItems: this.playlistService.playlists.reduce((items, playlist) => {
            return items.concat({
              name: playlist.name,
              callback: () => { playlist.addMusic(this.music); },
            })
          }, [
            {
              name: 'New Playist',
              callback: () => {
                const newPlaylist = this.playlistService.createPlaylist(this.music.title);
                newPlaylist.addMusic(this.music);
              }
            }
          ])
        },
      ];
>>>>>>> refactor: contextMenu Component move to MainView Component
    }
  },

  methods: {
    playMusic(music) {
      this.$emit('music-played', music);
    },

<<<<<<< HEAD
    openContextMenu(contextMenuOption) {
      this.contextMenu.music = contextMenuOption.music;
      this.contextMenu.style = contextMenuOption.style;
      this.isContextMenuOpened = true;
=======
    openContextMenu(e) {
      this.isContextMenuOpened = true;
      this.contextMenuStyle = {
        top: e.top,
        left: e.left,
      };
      this.music = e.music;
>>>>>>> refactor: contextMenu Component move to MainView Component
    },

    closeContextMenu() {
      this.isContextMenuOpened = false;
    },
<<<<<<< HEAD

    addPlaylistOnContextMenu(playlist) {
      const subItem = {
        name: playlist.name,
        callback: () => {
          playlist.addMusic(this.contextMenu.music);
        },
      };
      this.contextMenu.items[1].subItems.push(subItem);
    }
  },
=======
  }
>>>>>>> refactor: contextMenu Component move to MainView Component
}
</script>

<style scope lang="scss">

</style>
