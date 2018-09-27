<template>
  <section class="main-section">
    <header class='main-section__header'>
      <Search/>
      <More/>
    </header>
    <MusicView ref="Musics" :musics="library.musics" @music-played="playPlayQueue"/>
    <AlbumView ref="Albums" :albums="library.albums" @album-played="playPlayQueue"/>
    <GenreView ref="Genres" :genres="library.genres" @genre-played="playPlayQueue"/>
    <ArtistView ref="Artists"  :artists="library.artists" @artist-played="playPlayQueue"/>
  </section>
</template>

<script>
import Search from './Search.vue';
import More from './More.vue';

import AlbumView from './AlbumView.vue';
import GenreView from './GenreView.vue';
import ArtistView from './ArtistView.vue';
import MusicView from './MusicView.vue';

export default {
  name: 'MainSection',

  components: {
    Search,
    More,
    MusicView,
    AlbumView,
    GenreView,
    ArtistView,
  },

  props: {
    library: Object
  },

  data() {
    return {
      viewNameList: ['Musics', 'Albums', 'Genres', 'Artists']
    }
  },

  methods: {
    playPlayQueue(playQueue) {
      this.$emit('playQueue-played', playQueue);
    },

    showView(targetViewName) {
      for(let viewName of this.viewNameList) {
        const view = this.$refs[viewName];
        targetViewName === viewName ? view.activate() : view.inactivate();
      }
    }
  }
}
</script>

<style scoped lang="scss">
@import '../../index.scss';

.main-section {
  @include position(absolute, $top: 0px, $left: $main-menu-width);
  width: calc(100vw - #{$main-menu-width}); height: calc(100vh - #{$player-height});
  overflow: hidden;
}

.main-section__header {
  @include position(fixed, $top: 0px, $left: $main-menu-width);
  width: calc(100vw - #{$main-menu-width}); height: 70px;
  background: linear-gradient(#191919 50%, rgba(0,0,0,0));
  box-sizing: border-box;
  padding: 15px 30px 0 30px;
  z-index: 100;
}

</style>