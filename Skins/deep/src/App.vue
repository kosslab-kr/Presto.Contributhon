<template>
  <div id="app">
    <Menu ref="menu" @menu-selected="showView"/>
    <Main ref="main" @playQueue-played="playPlayQueue"/>
    <Player ref="player"/>
  </div>
</template>

<script>
import Menu from './components/Menu.vue'
import Main from './components/Main.vue'
import Player from './components/Player.vue'

export default {
  name: 'App',

  components: {
    Menu,
    Main,
    Player
  },

  data() {
    return {
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

    showView(view) {
      this.$refs.main.showView(view);
    }
  }
}
</script>

<style scoped lang="scss">
</style>