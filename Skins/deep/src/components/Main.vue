<template>
  <section class="main">
    <header class='main__header'>
      <MainSearch/>
      <BaseMore/>
    </header>
    <component :is="currentView" :items="currentViewItems" @music-played="playPlayQueue"/>
  </section>
</template>

<script>
import MainSearch from './MainSearch.vue';

import MainMusics from './MainMusics.vue';
import MainAlbums from './MainAlbums.vue';
import MainGenres from './MainGenres.vue';
import MainArtists from './MainArtists.vue';

export default {
  name: 'Main',

  components: {
    MainSearch,
    MainMusics,
    MainAlbums,
    MainGenres,
    MainArtists,
  },

  props: {
    library: Object
  },

  data() {
    return {
      currentView: ''
    }
  },

  methods: {
    playPlayQueue(playQueue) {
      this.$emit('playQueue-played', playQueue);
    },

    showView(targetViewName) {
      this.currentView = 'Main' + targetViewName;
    }
  },

  computed: {
    currentViewItems() {
      if(this.currentView === 'MainMusics') return this.library.musics;
      else if(this.currentView === 'MainAlbums') return this.library.albums;
      else if(this.currentView === 'MainGenres') return this.library.genres;
      else if(this.currentView === 'MainArtists') return this.library.artists;
    }
  }
}
</script>

<style scoped lang="scss">
@import '../index.scss';

.main {
  @include position(absolute, $top: 0px, $left: $main-menu-width);
  width: calc(100vw - #{$main-menu-width}); height: calc(100vh - #{$player-height});
  overflow: hidden;
}

.main__header {
  @include position(fixed, $top: 0px, $left: $main-menu-width);
  width: calc(100vw - #{$main-menu-width}); height: 70px;
  background: linear-gradient(#191919 50%, rgba(0,0,0,0));
  box-sizing: border-box;
  padding: 15px 30px 0 30px;
  z-index: 100;
}

</style>