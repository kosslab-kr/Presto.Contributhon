<template>
  <div id="app">
    <MainMenu/>
    <MainSection :library="library"/>
    <Player/>
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
      library: library
    }
  }
}
</script>

<style scoped lang="scss">
</style>