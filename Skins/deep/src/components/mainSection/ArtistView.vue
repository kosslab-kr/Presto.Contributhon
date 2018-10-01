<template>
  <section class="artist-view">
    <div class="artist-view__header">
      <div class="artist-view__title">{{title}}</div>
    </div>
    <section class='artist-view__body'>
      <Artist
        v-for="(artist, idx) in artists"
        :key="idx"
        :artist="artist"
        @artist-selected="_openArtistWindow"
        @artist-played="_playArtist"/>
    </section>
    <ArtistWindow
      ref="artistWindow"
      @artist-played="_playArtist"/>
  </section>
</template>

<script>
import Artist from './Artist.vue';
import ArtistWindow from './ArtistWindow.vue';

export default {
  name: 'ArtistView',
  
  components: {
    Artist,
    ArtistWindow
  },

  props: {
    artists: Array
  },

  data() {
    return {
      title: 'Artists'
    }
  },

  methods: {
    _openArtistWindow(artist) {
      this.$refs.artistWindow.open(artist)
    },

    _playArtist(playQueue) {
      this.$emit('artist-played', playQueue);
    }
  }
}
</script>

<style scope lang="scss">
@import '../../index.scss';

.artist-view {
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

.artist-view__header {
  position: sticky;
  top: 0px;
  border-bottom: 1px solid #333;
  z-index: 1;
}

.artist-view__title {
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

.artist-view__body {
  padding: 30px;
}

</style>
