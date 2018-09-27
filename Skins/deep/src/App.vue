<template>
  <div id="app">
    <MainMenu ref="mainMenu" @menu-selected="showView"/>
    <MainSection :library="library" @playQueue-played="playPlayQueue" ref="mainSection"/>
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

  mounted() {
    this.$refs.mainMenu.triggerClickEvent({menuIndex: 0});
  },
  
  methods: {
    playPlayQueue(playQueue) {
      this.$refs.player.playPlayQueue(playQueue);
    },

    showView(menuItemName) {
      this.$refs.mainSection.showView(menuItemName);
    }
  }
}
</script>

<style scoped lang="scss">
</style>