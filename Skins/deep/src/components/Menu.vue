<template>
  <nav class='menu'>
    <ul class='menu__category'>
      <li class="menu__category-title">YOUR LIBRARY</li>
      <MenuItem
        ref="libraries"
        v-for="(library, index) in libraries"
        :key="index"
        :name="library.name"
        :componentName="library.componentName"
        :items="library.items"
        @menu-clicked="showContent"
      />
    </ul>
    <ul class='menu__category'>
      <li class="menu__category-title">PLAYLISTS</li>
      <MenuItem
        ref="playlists"
        v-for="(playlist, index) in playlists"
        :key="index"
        :name="playlist.name"
        :componentName="'MainPlaylist'"
        :items="playlist.musics"
        @menu-clicked="showContent"
      />
    </ul>
    <MenuButtonPlaylist
      @playlist-name-submitted="createPlaylist"
    />
  </nav>
</template>

<script>
import MenuItem from './MenuItem.vue';
import MenuButtonPlaylist from './MenuButtonPlaylist.vue';
import IPlaylistService from './IPlaylistService.js';
// import library from './library.js';
import ILibraryService from './ILibraryService.js';
import IAlbumService from './IAlbumService.js';
import IGenreService from './IGenreService.js';
import IArtistService from './IArtistService.js';

export default {
  name: 'Menu',

  components: {
    MenuItem,
    MenuButtonPlaylist,
  },

  data() {
    return {
      libraries: [
        {
          name: 'Musics',
          componentName: 'MainMusics',
          items: ILibraryService.musics,
        },
        {
          name: 'Albums',
          componentName: 'MainAlbums',
          items: IAlbumService.albums,
        },
        {
          name: 'Genres',
          componentName: 'MainGenres',
          items: IGenreService.genres,
        },
        {
          name: 'Artists',
          componentName: 'MainArtists',
          items: IArtistService.artists,
        },
      ],

      playlists: IPlaylistService.playlists,
    }
  },

  methods: {
    triggerClickEvent({menuIndex} = {}) {
      const clickEvent = new Event('click');
      const menuItemsPlaylist = this.$refs.playlists || [];
      const menuItems = this.$refs.libraries.concat(menuItemsPlaylist);
      if(menuIndex === undefined) menuIndex = menuItems.length-1;
      const targetMenuItem = menuItems[menuIndex];

      targetMenuItem.$el.dispatchEvent(clickEvent);
    },

    showContent(view) {
      this.activateMenu(view);
      this.$emit('menu-selected', view);
    },

    activateMenu({name}) {
      const menuItemsPlaylist = this.$refs.playlists || [];
      const menuItems = this.$refs.libraries.concat(menuItemsPlaylist);

      menuItems.forEach(menuItem => {
        menuItem.isActive = (menuItem.name === name) ? true : false;
      })
    },

    createPlaylist(playlistName) {
      IPlaylistService.createPlaylist(playlistName);
      this.$nextTick(() => {
        this.triggerClickEvent();
      })
    }
  }
}
</script>

<style scoped lang="scss">
@import '../index.scss';

.menu {
  @include position(absolute, $top: 0px, $left: 0px);
  @include size($width: $main-menu-width, $height: calc(100vh - #{$player-height}));
  box-sizing: border-box;
  background: #131313;
  padding-top: 35px;
  font-size: 0.9rem;

  &:hover { cursor: default; }
}

.menu__category {
  color: #aaa;
  margin-top: 33px;
}

.menu__category-title {
  font-weight: lighter;
  letter-spacing: 1px;
  padding: 0px 25px;
  font-size: 0.8rem;
}

</style>