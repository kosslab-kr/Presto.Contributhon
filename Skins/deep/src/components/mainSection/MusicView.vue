<template>
  <section class="music-view">
    <div class="music-view__header">
      <div class="music-view__title">{{title}}</div>
    </div>
    <section class='music-view__body'>
      <Music
        v-for="(music, idx) in musics"
        :key="idx"
        :index="idx"
        :music="music"
        ref="musicComponents"
        @music-played="playMusic"
      />
    </section>
  </section>
</template>

<script>
import Music from './Music.vue';

export default {
  name: 'MusicView',
  
  components: {
    Music
  },

  props: {
    musics: Array
  },

  data() {
    return {
      title: 'Musics'
    }
  },

  methods: {
    playMusic(index) {
      this.$emit('music-played', {currentMusicIdx: index, musics: this.musics});
    }
  }
}
</script>

<style scope lang="scss">
@import '../../index.scss';

.music-view {
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

.music-view__header {
  position: sticky;
  top: 0px;
  border-bottom: 1px solid #333;
  z-index: 1;
}

.music-view__title {
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

.music-view__body {
  padding: 30px;
}

</style>
