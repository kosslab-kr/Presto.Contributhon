<template>
  <section class="album-view">
    <div class="album-view__header">
      <div class="album-view__title">{{title}}</div>
    </div>
    <section class='album-view__body'>
      <Album
        v-for="(album, idx) in albums"
        :key="idx"
        :album="album"
        @album-selected="_openAlbumWindow"
        @album-played="_playAlbum"/>
    </section>
    <transition name="fade">
      <div v-if="isAlbumSelected" class="album-window-background"></div>
    </transition>
    <transition name="slide-fade">
      <AlbumWindow
        v-if="isAlbumSelected"
        ref="albumWindow"
        :album="selectedAlbum"
        @album-played="_playAlbum"
        @window-closed="_closeAlbumWindow"/>
    </transition>
  </section>
</template>

<script>
import Album from './Album.vue';
import AlbumWindow from './AlbumWindow.vue';

export default {
  name: 'AlbumView',
  
  components: {
    Album,
    AlbumWindow
  },

  props: {
    albums: Array
  },

  data() {
    return {
      title: 'Albums',
      isAlbumSelected: false,
      selectedAlbum: null
    }
  },

  methods: {
    _openAlbumWindow(album) {
      this.isAlbumSelected = true;
      this.selectedAlbum = album;
    },

    _closeAlbumWindow() {
      this.isAlbumSelected = false;
      this.selectedAlbum = null;
    },

    _playAlbum(playQueue) {
      this.$emit('album-played', playQueue);
    }
  }
}
</script>

<style scope lang="scss">
@import '../../index.scss';

.album-view {
  width: 100%;
  height: 100%;
  overflow: scroll;
  background: #191919;

  &:before {
    content: '';
    display: block;
    height: 100px;
  }
}

.album-view__header {
  position: sticky;
  top: 0px;
  border-bottom: 1px solid #333;
  z-index: 1;
}

.album-view__title {
  box-sizing: border-box;
  color: #fff;
  padding: 0px 30px 20px 30px;
  font-size: 2.8rem;
  font-weight: 900;
  background: #191919;

  &:hover {
    cursor: default;
  }
}

.album-view__body {
  padding: 30px;
}

.album-window-background {
  position: fixed;
  top: 0px; left: $main-menu-width;
  z-index: 1000; // viewHeader(1) < mainHeader(100) < 1000
  width: calc(100vw - #{$main-menu-width}); height: calc(100vh - #{$player-height});
  background: rgba(0,0,0,0.5);
}

.fade-enter-active, .fade-leave-active {
  transition: opacity .5s;
}

.fade-enter, .fade-leave-to {
  opacity: 0;
}

.slide-fade-enter-active, .slide-fade-leave-active {
  transition: opacity .5s, transform .5s;
}

.slide-fade-enter, .slide-fade-leave-to {
  transform: translate3d(0, -50px, 0);
  opacity: 0;
}

</style>
