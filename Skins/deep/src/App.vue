<template>
  <div id="app">
    <Menu ref="menu" @menu-selected="showView"/>
    <Main :library="library" @playQueue-played="playPlayQueue" ref="main"/>
    <Player ref="player"/>
  </div>
</template>

<script>
import Menu from './components/Menu.vue'
import Main from './components/Main.vue'
import Player from './components/Player.vue'
import library from './library.js'

export default {
  name: 'App',

  components: {
    Menu,
    Main,
    Player
  },

  data() {
    return {
      library: library,
      playQueue: null
    }
  },

  mounted() {
    this.$refs.menu.triggerClickEvent({menuIndex: 0});
  },
  
  methods: {
    playPlayQueue(playQueue) {
      this.$refs.player.playPlayQueue(playQueue);
    },

    showView(menuItemName) {
      this.$refs.main.showView(menuItemName);
    }
  }
}
</script>

<style scoped lang="scss">
</style>