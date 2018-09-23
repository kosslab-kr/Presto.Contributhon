<template>
  <div id="app">
    <MainMenu/>
    <MainSection :library="library" @album-played="playAlbum"/>
    <Player ref="player"/>
  </div>
</template>

<script>
import MainMenu from './components/mainMenu/MainMenu.vue'
import MainSection from './components/mainSection/MainSection.vue'
import Player from './components/player/Player.vue'
import library from './library.js'

export default {
  name: 'App',

  components: {
    MainMenu,
    MainSection,
    Player
  },

  data() {
    return {
      library: library,
      playQueue: null
    }
  },
  
  methods: {
    playAlbum(album) {
      this.$refs.player.playPlayQueue(album.musics);
    }
  }
}
</script>

<style scoped lang="scss">
</style>