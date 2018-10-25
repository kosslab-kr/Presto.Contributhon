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
      :src="
        repeatMode === 0 ? require('../assets/repeat.png') : 
        repeatMode === 1 ? require('../assets/repeat_once.png') : 
        require('../assets/repeat_infinite.png')
      "
      :style="{'opacity': isHover ? '1' : '0.7'}"
      alt="repeat_icon"
    >
  </div>
</template>

<script>
export default {
  name: 'PlayerButtonRepeat',

  data() {
    return {
      repeatMode: 0,
      isHover: false
    }
  },

  methods: {
    clickButton() {
      this.repeatMode = (++this.repeatMode) % 3;
      this.isHover = true;
      this.$emit('button-clicked', {repeatMode: this.repeatMode});
    }
  }
}
</script>

<style scoped lang="scss">
@import '../index.scss';

.button-wrap {
  position: relative;
  display: inline-block;
  width: 16px; height: 16px;
  vertical-align: middle;
}

.button__icon {
  position: absolute;
  top: 50%; left: 50%;
  transform: translate3d(-53%, -55%, 0);
  width: 120%; height: 120%;
}

</style>
