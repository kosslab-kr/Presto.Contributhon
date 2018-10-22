<template>
  <MainView :name="name">
    <template slot="navigator">
      <MainViewNavigator
        :itemNames="items.map(item => item.Name)"
        @item-selected="selectGenre"
      />
    </template>
    <template slot="body">
      <MainViewList
        v-if="selectedGenre"
        :items="genreListItems"
        :fields="genreListFields"
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
import MainViewNavigator from './MainViewNavigator.vue';
import MainViewList from './MainViewList.vue';

export default {
  name: 'MainGenres',

  components: {
    MainView,
    MainViewNavigator,
    MainViewList
  },

  props: {
    name: String,
  },

  data() {
    return {
      items: null,
      selectedGenreName: '',
      genreListFields: [
        {
          name: '',
          value: 'number',
          width: '6%',
          textAlign: 'center',
          marginRight: '2%',
          playButton: true
        },
        {
          name: 'TITLE',
          value: 'title',
          width: '39%',
          textAlign: 'left',
          marginRight: '5%'
        },
        {
          name: 'ARTIST',
          value: 'artist',
          width: '14%',
          textAlign: 'left',
          marginRight: '5%'
        },
        {
          name: 'ALBUM',
          value: 'album',
          width: '14%',
          textAlign: 'left',
          marginRight: '5%'
        },
        {
          name: 'TIME',
          value: 'time',
          width: '5%',
          textAlign: 'center',
          marginRight: '0%'
        },
        {
          name: '',
          value: 'more',
          width: '5%',
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
                name: playlist.name,
                callback: () => { playlist.addMusic(this.contextMenu.music); },
              })
            }, [
              {
                name: 'New Playist',
                border: true,
                callback: () => {
                  const newPlaylist = IPlaylistService.createPlaylist(this.contextMenu.music.title);
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
    genre.getGenres().then(genres => {
      this.items = genres;
    });
  },

  methods: {
    playMusic(music) {
      this.$emit('music-played', music);
    },

    selectGenre(genreName) {
      this.selectedGenreName = genreName;
      this.$el.scrollTo(0,0);
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
        name: playlist.name,
        callback: () => {
          playlist.addMusic(this.contextMenu.music);
        },
      };
      this.contextMenu.items[1].subItems.push(subItem);
    },
  },

  computed: {
    selectedGenre() {
      for(let genre of this.items) {
        if(genre.Name === this.selectedGenreName) return genre;
      }

      return null;
    },

    genreListItems() {
      const musics = this.selectedGenre.getMusics();

      return musics.map((music) => {
        return {
          number: '',
          title: music.Title,
          artist: music.Artist.Name,
          album: music.Album.Name,
          time: this.formatTime(music.getLength()),
          more: '',
          source: music
        };
      })
    }
  }
}
</script>

<style scoped lang="scss">

</style>
