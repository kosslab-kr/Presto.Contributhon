<template>
  <div id="app">
    <Menu ref="menu" @menu-selected="showView"/>
    <Main ref="main" @music-played="playMusic"/>
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
    window.addEventListener('load', () => {
      this.$refs.menu.triggerClickEvent({menuIndex: 0});
    });
  },
  
  methods: {
    playMusic(music) {
      this.$refs.player.play(music);
    },

    showView(view) {
      this.$refs.main.showView(view);
    }
  }
}
</script>

<style scoped lang="scss">
</style>