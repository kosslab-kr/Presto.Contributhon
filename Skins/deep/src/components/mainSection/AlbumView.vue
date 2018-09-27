<template>
  <section v-show="isActive" class="album-view">
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
    <AlbumWindow
      ref="albumWindow"
      @album-played="_playAlbum"/>
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
      isActive: false
    }
  },

  methods: {
    activate() {
      this.isActive = true;
      this.$refs.albumWindow.activate();
    },

    inactivate() {
      this.isActive = false;
      this.$refs.albumWindow.inactivate();
    },

    _openAlbumWindow(album) {
      this.$refs.albumWindow.open(album)
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

</style>
