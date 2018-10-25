<template>
  <div
    class="button-wrap"
    @mouseenter="isHover = true"
    @mouseleave="isHover = false"
    @mousedown="isHover = false"
    @click="clickButton"
  >
    <img
      class="button__icon"
      :src="isActive ? require('../assets/shuffle_active.png') : require('../assets/shuffle.png')"
      :style="{'opacity': isHover ? '1' : '0.7'}"
      alt="shuffle_icon"
      
    >
  </div>
</template>

<script>
export default {
  name: 'PlayerButtonShuffle',

  data() {
    return {
      isActive: false,
      isHover: false
    }
  },

  methods: {
    clickButton() {
      this.isActive = !this.isActive;
      this.isHover = true;
      this.$emit('button-clicked', {isShuffled: this.isActive});
    }
  }
}
</script>

<style scoped lang="scss">
@import '../index.scss';

.button-wrap {
  display: inline-block;
  width: 16px; height: 16px;
  vertical-align: middle;
  margin-right: 23px;
}

.button__icon {
  width: 100%; height: 100%;
}

</style>
