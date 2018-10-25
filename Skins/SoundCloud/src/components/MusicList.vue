<template>
 <ul>
   <MusicItem 
    v-for="music in normalizeItems" 
     v-bind="music"
     ref="musicItem"
    :key=music.id 
    :isPlaying="music === playingMusic"
    @saveActiveIdx="saveActiveIdx"
    @play="onPlay(music)"
    @pause="onPause(music)"
  />
  </ul>
</template>

<script>
import MusicPlayerProgress from './MusicPlayerProgress.vue';
import MusicItem from './MusicItem.vue';
import {musicTimeFormat} from '../helper.js'

export default {
  components: {
    MusicItem,
  },
  props: ['musicList'],
  watch: {
    musicList (musicList) {
      this.items = musicList;
    }
  },
  computed: {
    normalizeItems () {
      return this.items.map(music => {
        return {
          music: music,
          title: music.Title,
          artist: music.Artist.Name,
          cover: music.Album.Picture,
        };
      });
    }
  },
  data() {
    return {
      playingMusic: null,
      items: this.musicList,
    };
  },
  methods: {
    onPause(music) {
      this.playingMusic = null;
    },
    onPlay (music) {
      this.playingMusic = music;

      this.$store.state.musicLibrary.setPlayingOne(music.music);
    }
  }
}
</script>
<style >
</style>
