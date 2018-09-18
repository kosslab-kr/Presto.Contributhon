<template>
  <div id="app">
    <MainMenu/>
    <MainSection/>
    <Player/>
  </div>
</template>

<script>
import MainMenu from './components/mainMenu/MainMenu.vue'
import MainSection from './components/mainSection/MainSection.vue'
import Player from './components/player/Player.vue'

export default {
  name: 'App',
  components: {
    MainMenu,
    MainSection,
    Player
  }
}
</script>

<style scoped lang="scss">
</style>