<template>
  <svg class="button"
    version="1.1"
    baseProfile="full"
    width="100%" height="100%" viewBox="0 0 100 100"
    xmlns="http://www.w3.org/2000/svg"
    :class="{'button--pressed': isButtonPressed}"
    :style="{'--background': background, '--hoverBackground': hoverBackground}"
    @mousedown.stop="isButtonPressed = true"
    @mouseup.stop="releaseButton">
    <polygon v-if="!onPlay" points="38 30, 73 50, 38 70" fill="#fff"/>
    <line v-if="onPlay" x1="39" y1="31" x2="39" y2="69" stroke="#fff" stroke-width="10"/>
    <line v-if="onPlay" x1="61" y1="31" x2="61" y2="69" stroke="#fff" stroke-width="10"/>
    <rect v-if="isButtonPressed" width="100%" height="100%" fill="rgba(0,0,0,0.3)"/>
  </svg>
</template>

<script>
export default {
  name: 'PlayPauseButton',

  props: {
    background: {
      type: String,
      default: 'transparent'
    },

    hoverBackground: {
      type: String,
      default: 'transparent'
    }
  },

  data() {
    return {
      isButtonPressed: false,
      onPlay: false
    }
  },

  methods: {
    releaseButton() {
      this.isButtonPressed = false;
      this.$emit('button-clicked');
    }
  }
}
</script>

<style scoped lang="scss">
@import "../index.scss";

.button {
  border: 1px solid #bbb;
  border-radius: 50%;
  transition: transform 0.05s;
  background: var(--background);

  &:hover {
    border-color: #fff;
    transform: scale(1.1);
    cursor: default;
    background: var(--hoverBackground);
  }
}

.button--pressed {
  transform: scale(1) !important;
  border-color: #bbb !important;
}

</style>
