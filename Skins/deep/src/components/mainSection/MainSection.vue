<template>
  <section class="main-section">
    <header class='main-section__header'>
      <Search/>
      <More/>
    </header>
    <AlbumView v-if="viewDict.Albums" :albums="library.albums" @album-played="playPlayQueue"/>
    <GenreView v-if="viewDict.Genres" :genres="library.genres" @genre-played="playPlayQueue"/>
  </section>
</template>

<script>
import Search from './Search.vue';
import More from './More.vue';

import AlbumView from './AlbumView.vue';
import GenreView from './GenreView.vue';

export default {
  name: 'MainSection',

  components: {
    Search,
    More,
    AlbumView,
    GenreView
  },

  props: {
    library: Object
  },

  data() {
    return {
      viewDict: {
        'Albums': false,
        'Genres': false
      }
    }
  },

  methods: {
    playPlayQueue(playQueue) {
      this.$emit('playQueue-played', playQueue);
    },

    showView(viewName) {
      for(let view in this.viewDict) {
        this.viewDict[view] = (view === viewName) ? true : false;
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