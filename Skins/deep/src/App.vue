<template>
  <div id="app">
    <MainMenu/>
    <MainSection/>
    <Player/>
  </div>
</template>

<script>
import MainMenu from './components/MainMenu.vue'
import MainSection from './components/MainSection.vue'
import Player from './components/Player.vue'

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