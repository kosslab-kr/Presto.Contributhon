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
      :src="isMuted || volume === 0 ? require('../assets/mute_active.png') :
            volume < 0.3 ? require('../assets/mute_1.png') : 
            volume < 0.6 ? require('../assets/mute_2.png') : 
            require('../assets/mute.png')
           "
      :style="{'opacity': isHover ? '1' : '0.7'}"
      alt="mute_icon"
    >
  </div>
</template>

<script>
export default {
  name: 'PlayerButtonMute',

  props: {
    volume: Number,
  },

  data() {
    return {
      isMuted: false,
      isHover: false,
    }
  },

  methods: {
    clickButton() {
      this.isMuted = !this.isMuted;
      this.isHover = true;
      this.$emit('button-clicked', this.isMuted);
    }
  }
}
</script>

<style scoped lang="scss">
@import '../index.scss';

.button-wrap {
  position: relative;
  display: inline-block;
  vertical-align: middle;
  width: 22px; height: 22px;
  margin-right: 5px;
}

.button__icon {
  position: absolute;
  top: 50%; left: 50%;
  transform: translate3d(-50%, -50%, 0);
  width: 90%; height: 90%;
}

</style>
