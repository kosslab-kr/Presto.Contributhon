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
import MainMusicsItem from './MainMusicsItem.vue';

export default {
  name: 'MainMusics',
  
  components: {
    MainView,
    MainMusicsItem
  },

  props: {
    name: String,
  },

  data() {
    return {
      itemsOrigin: null,
      items: null,
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
                  const newPlaylist = IPlaylistService.createPlaylist(this.contextMenu.music.Title)
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
    library.getMusics().then(musics => {
      this.itemsOrigin = musics;
      this.items = musics;
    });
  },

  methods: {
    playMusic(music) {
      this.$emit('music-played', music);
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

    filterItem(keyword) {
      this.items = this.itemsOrigin.filter(music => {
        const musicTitle = music.Title.toLowerCase();
        
        return musicTitle.includes(keyword);
      })
    },
  },
}
</script>

<style scope lang="scss">

</style>
