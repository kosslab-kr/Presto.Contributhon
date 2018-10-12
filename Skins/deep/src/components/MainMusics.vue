<template>
  <MainView :name="name">
    <template slot="body">
      <MainMusicsItem
        v-for="(item, index) in items"
        :key="index"
        :index="index"
        :music="item"
        @music-played="playMusic"
      />
    </template>
  </MainView>
</template>

<script>
import MainView from './MainView.vue';
import MainMusicsItem from './MainMusicsItem.vue';

export default {
  name: 'MainMusics',
  
  components: {
    MainView,
    MainMusicsItem
  },

  props: {
    name: String,
    items: Array
  },

  methods: {
    playMusic(index) {
      this.$emit('music-played', {currentMusicIdx: index, musics: this.items});
    }
  }
}
</script>

<style scope lang="scss">

</style>
