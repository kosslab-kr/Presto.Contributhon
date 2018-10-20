<template>
  <MainView :name="name">
    <template slot="body">
      <MainMusicsItem
        v-for="(item, index) in items"
        :key="index"
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
    playMusic(music) {
      this.$emit('music-played', music);
    }
  }
}
</script>

<style scope lang="scss">

</style>
